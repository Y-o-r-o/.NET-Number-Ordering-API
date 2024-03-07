using Microsoft.CodeAnalysis;
using System.Diagnostics;

namespace Core.Extensions;

public static class StackTraceExtensions
{
    private static readonly List<string> projectNames = GetSolutionProjectNames();
    private static readonly string solutionDirectory = GetSolutionDirectory();
    

    public static StackFrame? GeFirstFrame(this StackTrace stackTrace)
    {
        foreach (var stackFrame in stackTrace.GetFrames())
        {
            var methodAssembly = stackFrame.GetMethod()?.DeclaringType?.Assembly;

            if (methodAssembly == null)
            {
                break;
            }

            var assemblyProjectName = methodAssembly.FullName?.Split(',').FirstOrDefault();
            //Assuming that local projects are in the same folder as .sln file. If not this code needs to be modified.
            var assemblyLocation = methodAssembly.Location;

            if (projectNames.Contains(assemblyProjectName!) && assemblyLocation.Contains(solutionDirectory))
            {
                return stackFrame;
            }
        }

        return null;
    }

    private static List<string> GetSolutionProjectNames()
    {
        var solutionDirectory = GetSolutionDirectory();
        var names = new List<string>();

        if (solutionDirectory != null)
        {
            var csprojFiles = Directory.GetFiles(solutionDirectory, "*.csproj", SearchOption.AllDirectories);
            names.AddRange(csprojFiles.Select(csprojFile => Path.GetFileNameWithoutExtension(csprojFile)));
        }

        return names;
    }

    private static string GetSolutionDirectory()
    {
        var currentDirectory = AppContext.BaseDirectory;
        var directoryInfo = new DirectoryInfo(currentDirectory);

        while (directoryInfo != null && !directoryInfo.GetFiles("*.sln").Any())
        {
            directoryInfo = directoryInfo.Parent;
        }

        return directoryInfo?.FullName!;
    }
}