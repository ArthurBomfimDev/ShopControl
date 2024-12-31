using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;

namespace ProjetoTeste.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class BaseController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public BaseController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _unitOfWork.BeginTransactionAsync().Wait();
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        await _unitOfWork.BeginTransactionAsync();
        var executedContext = await next();

        if (executedContext.Exception == null)
        {
            await _unitOfWork.CommitAsync();
        }
    }
}