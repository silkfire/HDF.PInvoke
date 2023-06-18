namespace HDF.PInvoke.HDF5;

using System;
using System.IO;
using System.Reflection;

public static class NativeDependencies
{
    public static void ResolvePathToExternalDependencies()
    {
        AddPathStringToEnvironment();
    }

    internal static string GetAssemblyPath()
    {
        //string myPath = new Uri(Assembly.GetExecutingAssembly().Location).AbsolutePath;
        //myPath = Uri.UnescapeDataString(myPath);
        //return myPath;

        return Assembly.GetExecutingAssembly().Location;
    }

    private static void AddPathStringToEnvironment()
    {
        const string pathEnvironmentVariableName = "PATH";

        var assemblyPath = Path.GetDirectoryName(GetAssemblyPath())!;

        string currentPathEnvironmentVariableValue = Environment.GetEnvironmentVariable(pathEnvironmentVariableName)!;
        if (currentPathEnvironmentVariableValue.Contains(assemblyPath)) return;

        Environment.SetEnvironmentVariable(pathEnvironmentVariableName, $"{assemblyPath};{currentPathEnvironmentVariableValue}");
    }
}
