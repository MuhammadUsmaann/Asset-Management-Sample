using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Globalization;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using AssetManagement.Server.Infrastructure.Extensions;
using AssetManagement.Server.Infrastructure.Exceptions;

namespace AssetManagement.Server.Infrastructure.Extensions
{
    public static class MethodsExt
    {
        #region COMMON


        public static string ExtensionReplace(this string str, string currentChar, string newChar, bool isToUpper = false)
        {
            if (str.IsContainedInIgnoreCase(currentChar))
                str = str.Replace(currentChar, newChar);

            if (isToUpper)
                str = str.ToUpper();

            return str;
        }

        public static bool IsContainedInIgnoreCase(this string str, string input) =>
            str.Contains(input, StringComparison.OrdinalIgnoreCase);



        public static byte[] GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(salt);

            return salt;
        }

        public static string HashPassword(string password, byte[] salt)
        {
            int iterations = 10000;
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
            {
                byte[] hashedPassword = pbkdf2.GetBytes(32);
                return Convert.ToBase64String(hashedPassword);
            }
        }

        public static async Task CreateTransactionAsync(this DatabaseFacade databaseFacade, Func<Task> operation, Func<Task> rollback = null,
            CancellationToken token = default)
        {
            var strategy = databaseFacade.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async (cancellationToken) =>
            {
                using var transaction = await databaseFacade.BeginTransactionAsync(cancellationToken);
                try
                {
                    await operation();
                    await transaction.CommitAsync(cancellationToken);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(token);
                    if (rollback != null)
                        await rollback();

                    var customExceptionType = GetException(ex).GetType();
                    var customMessage = $"Transaction Failed: {ex.Message}";
                    throw (Exception)Activator.CreateInstance(customExceptionType, customMessage, ex);
                }
            }, token);
        }

        public static Exception GetException(Exception ex)
        {
            ex = ex switch
            {
                AlreadyExistHelperException _ => ex as AlreadyExistHelperException,
                BadRequestHelperException _ => ex as BadRequestHelperException,
                ConflictHelperException _ => ex as ConflictHelperException,
                KeyNotFoundHelperException _ => ex as KeyNotFoundHelperException,
                NotModifiedHelperException _ => ex as NotModifiedHelperException,
                UnprocessableEntityHelperException _ => ex as UnprocessableEntityHelperException,
                _ => ex as Exception
            };
            return ex;
        }

        public static async Task<List<T>> MapDataAsync<T>(IAsyncEnumerable<T> entities) where T : class
        {
            List<T> list = new();
            await foreach (var item in entities)
                list.Add(item);

            return list;
        }

        public static string ConstructExceptionMessage(ExceptionEnum exceptionEnumType, ExceptionErrorCodes errorCode, string data) =>
            $"{exceptionEnumType.ToString().ExtensionReplace("_", " ", true)} {errorCode.ToString().ExtensionReplace("_", " ", true)}: {data}";

        public static string ConstructExceptionMessage(ExceptionEnum exceptionEnumType, ExceptionErrorCodes errorCode, string data = null, object messages = null) =>
            $"{exceptionEnumType.ToString().ExtensionReplace("_", " ", true)} {errorCode.ToString().ExtensionReplace("_", " ", true)}: ({data}) {messages.ToString()}";


        #endregion


    }
}
