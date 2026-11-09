using Microsoft.AspNetCore.Mvc;

namespace GerenciadorLivros.Api.Middleware;

/// <summary>
/// Captura qualquer exceção não tratada e devolve uma resposta JSON padronizada (ProblemDetails)
/// em vez de expor o erro interno ao cliente.
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _proximo;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate proximo, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _proximo = proximo;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext contexto)
    {
        try
        {
            await _proximo(contexto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro não tratado ao processar {Metodo} {Caminho}",
                contexto.Request.Method, contexto.Request.Path);

            var problema = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Erro interno do servidor",
                Detail = "Ocorreu um erro inesperado. Tente novamente mais tarde."
            };

            contexto.Response.StatusCode = StatusCodes.Status500InternalServerError;
            contexto.Response.ContentType = "application/problem+json";
            await contexto.Response.WriteAsJsonAsync(problema);
        }
    }
}
