using AssetManagement.Server.Infrastructure.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.Server.Controllers
{
    [ApiController]
    public class ControllerBaseApi : ControllerBase
    {
        protected IActionResult CreateResponse<T>(T obj, string messageHeader = null, string messageDescripton = null)
        {
            var result = new ResponseTypeView<T>
            {
                StatusCode = 200,
                MessageDescription = messageDescripton ?? "Operation Successfully completed",
                MessageHeader = messageHeader ?? "Success",
                Result = obj
            };
            return Ok(result);
        }
    }
}
