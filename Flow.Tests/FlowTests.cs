using Xunit.Abstractions;

namespace Flow.Tests;

using Flow;

public class FlowTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public FlowTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    [Trait("Category", "AST")]
    public void TestAST_HelloWorld()
    {
        string input = 
"""
import System;
import Math;
            
module Program {
  
  let Main(args:array[string]) = {
    Print("Hello Flow!");
  }
}

""";
        FlowDriver driver = new FlowDriver(input);

        var listener = driver.WalkTree();
        string result = listener.AST.ToString();
        _testOutputHelper.WriteLine(result);
    }

    // [Fact]
    // [Trait("Category", "AST")]
    public void TestAST()
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "../../../../Flow/test.flo");
        string input = File.ReadAllText(filePath);
        FlowDriver driver = new FlowDriver(input);

        var listener = driver.WalkTree();
        string result = listener.AST.ToString();
        _testOutputHelper.WriteLine(result);
    }

    [Fact]
    [Trait("Category", "Codegen")]
    public void TestCodeGenerator()
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "../../../../Flow/test.flo");
        string input = File.ReadAllText(filePath);
        FlowDriver driver = new FlowDriver(input);

        var listener = driver.WalkTree();
        var outputCode = listener.GetCodeGenString();
        outputCode = CodeGen.FormatCSharpCode(outputCode);

        _testOutputHelper.WriteLine(outputCode);
    }
}