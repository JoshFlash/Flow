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
  const m:int = 10;
  
  let Main(args:array[string]) = {
    const l:int = 11;
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
        
/* This should print the following:

└── program
     ├── module_declaration
     └── function_declaration
         ├── parameter_list
         └── block_statement
             ├── variable_declaration
             │   └── variable_value (n)
             ├── variable_declaration
             │   └── variable_value (array[bool](n + 1))
             ├── for_statement
             │   ├── range_clause (2 -> n)
             │   └── block_statement
             │       └── assignment_statement
             │           └── variable_value (true)
             ├── for_statement
             │   ├── range_clause (2 -> n)
             │   ├── unary_operation (i * i < n)
             │   └── block_statement
             │       ├── if_statement
             │       │   └── block_statement
             │       │       ├── variable_declaration
             │       │       │   └── variable_value (i * i)
             │       │       ├── while_statement
             │       │       │   ├── unary_operation (j < n)
             │       │       │   └── block_statement
             │       │       │       ├── assignment_statement
             │       │       │       │   └── variable_value (false)
             │       │       │       └── assignment_statement
             │       │       │           └── variable_value (j + i)
             │       │       └── function_call_statement (Print)
             │       └── block_statement
             └── block_statement

*/
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