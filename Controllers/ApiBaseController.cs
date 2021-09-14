using DevsApi.API;
using Microsoft.AspNetCore.Mvc;

namespace DevsApi.Controllers
{   
    public abstract class ApiBaseController : ControllerBase
    {
        protected OkObjectResult ApiOk<T>(T Results) =>
            Ok(CustomResponse<T>(Results));

        protected OkObjectResult ApiOk(string Message = "") =>
            Ok(CustomResponse(true, Message));

        protected CreatedResult ApiCreated(string uri,string Message = "") =>
            Created(uri, CustomResponse(true, Message));

        protected NotFoundObjectResult ApiNotFound(string Message = "") =>
            NotFound(CustomResponse(false, Message));

        protected BadRequestObjectResult ApiBadRequest<T>(T Results, string Message = "") =>
            BadRequest(CustomResponse<T>(Results, false, Message));


        #region MetodosPrivados 

        APIResponse<T> CustomResponse<T>(T Results, bool Succeed = true, string Message = "") =>
        new APIResponse<T>()
        {
            Results = Results,
            Succeed = Succeed,
            Message = Message
        };

        APIResponse<string> CustomResponse(bool Succeed = true, string Message = "") =>
        new APIResponse<string>()
        {
            Succeed = Succeed,
            Message = Message
        };

        #endregion

    }
}