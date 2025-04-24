using Ambev.DeveloperEvaluation.Domain.Common;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application;

public abstract class BaseHandler(ILogger logger)
{

    protected async Task<OperationResult<T>> TryCatchAsync<T>(Func<Task<OperationResult<T>>> action, string operation)
    {
        try
        {
            logger.LogInformation("Initiating operation: {Operation}", operation);
            return await action();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error during operation: {Operation}", operation);
            return OperationResult<T>.Failure($"Internal error while performing '{operation}': {ex.Message}");
        }
    }

    protected async Task<OperationResult> TryCatchAsync(Func<Task<OperationResult>> action, string operation)
    {
        try
        {
            logger.LogInformation("Initiating operation: {Operation}", operation);
            return await action();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error during operation: {Operation}", operation);
            return OperationResult.Failure($"Internal error while performing '{operation}': {ex.Message}");
        }
    }

}

