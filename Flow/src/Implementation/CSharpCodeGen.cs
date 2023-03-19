using System;
using System.Diagnostics;
using System.IO;
using Flow;

public class CSharpCodeGen
{
    private const string ProjectFileName = "FlowGeneratedProject.csproj";
    
    public static void TranspileFlow(string flowFilePath)
    {
        string filePath = flowFilePath;
        string input = File.ReadAllText(filePath);
        FlowDriver driver = new FlowDriver(input);

        var listener = driver.WalkTree();
        var outputCode = listener.GetCodeGenString();
        outputCode = CodeGen.FormatCSharpCode(outputCode);

        string fileName = Path.GetFileNameWithoutExtension(filePath);
        var projectPath = Path.Combine(Path.GetDirectoryName(filePath), "GeneratedProject");
        Directory.CreateDirectory(projectPath);

        CreateCsProject(projectPath);

        var generatedCodePath = Path.Combine(projectPath, $"{fileName}.cs");
        File.WriteAllText(generatedCodePath, outputCode);

        BuildGeneratedProject(projectPath);
    }


    
    private static void CreateCsProject(string projectPath)
    {
        string csprojContent = @"
<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net7.0</TargetFramework>
    <EnableDefaultCompileItems>false</EnableDefaultCompileItems>
  </PropertyGroup>
  <ItemGroup>
    <Compile Include=""**\*.cs"" />
  </ItemGroup>
</Project>
";

        string csprojPath = Path.Combine(projectPath, "FlowGeneratedProject.csproj");
        File.WriteAllText(csprojPath, csprojContent);
    }
    
    private static void BuildGeneratedProject(string projectPath)
    {
        var projectFilePath = Path.Combine(projectPath, ProjectFileName);

        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = $"build \"{projectFilePath}\" --configuration Release",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            }
        };

        process.Start();
        process.WaitForExit();

        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();

        Console.WriteLine(output);

        if (!string.IsNullOrWhiteSpace(error))
        {
            Console.Error.WriteLine(error);
        }
    }


}