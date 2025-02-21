using Microsoft.AspNetCore.Mvc;
using ProjetoTeste.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureJson();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureContext(builder.Configuration);
builder.Services.ConfigureInjectionDependency();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Select(kvp =>
            {
                string fieldName = kvp.Key; // Nome do campo com erro, ex: listTInputCreateDTO[2].Code
                string errorMessage = kvp.Value.Errors.First().ErrorMessage; // Pega a mensagem de erro

                // Expressão regular para encontrar a posição do array dentro de colchetes []
                var match = System.Text.RegularExpressions.Regex.Match(fieldName, @"\[(\d+)\]");
                string position = match.Success ? $"(Posição {int.Parse(match.Groups[1].Value) + 1})" : ""; // Incrementa 1 no índice

                return $"{errorMessage} {position}".Trim();
            })
            .ToList();

        return new BadRequestObjectResult(new { mensagem = errors });
    };
});



var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();