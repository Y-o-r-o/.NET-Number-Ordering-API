using BusinessLayer.DTOs;
using Core.Extensions;
using System.Diagnostics;

namespace API.Extensions;

public static class ExceptionExtensions
{
    public static AdditionalDetailsDTO? AnalyzeException(this Exception ex)
    {
        var trace = new StackTrace(ex, true);

        var stackFrame = trace.GeFirstFrame();

        if (stackFrame == null)
        {
            return null;
        }

        return new AdditionalDetailsDTO()
        {
            InnerMessage = ex.InnerException == null ? "There is no inner message." : ex.InnerException.Message,
            Column = stackFrame.GetFileColumnNumber(),
            Line = stackFrame.GetFileLineNumber(),
            MethodName = stackFrame.GetMethod()?.ReflectedType?.Name,
            File = stackFrame.GetFileName()
        };
    }
}