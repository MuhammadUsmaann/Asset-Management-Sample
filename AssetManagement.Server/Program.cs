using ServicesDataLayer.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

ConfigurationManager configuration = builder.Configuration;


services.AddIdentityDbContext(configuration);
services.CorsConfiguration();
services.RegisterServices(configuration);
services.AuthenticationConfiguration(configuration);
services.ControllerConfiguration();
services.SwaggerConfiguration();

var app = builder.Build();


app.UseCors("AllowAll");
app.MigrateDatabaseAsync().ConfigureAwait(false).GetAwaiter();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Asset Manager API v1");
    c.RoutePrefix = "";
});


app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
