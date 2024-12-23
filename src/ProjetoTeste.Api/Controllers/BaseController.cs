using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProjetoTeste.Api.Controllers
{
    public class BaseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork
        public override async void OnActionExecuted(ActionExecutedContext context)
        {
            await _unitOfWork.Commit();
            base.OnActionExecuted(context);
        }
    }
}
