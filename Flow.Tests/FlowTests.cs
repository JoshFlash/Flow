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
    public void TestDriver()
    {
        string input = "var x: int = 5;";
        FlowDriver driver = new FlowDriver(input);
        var tree = driver.ParseVariableDecl();
        string result = tree.ToStringTree(driver.Parser);
        _testOutputHelper.WriteLine(result);
        Console.WriteLine(result);
        Assert.False(string.IsNullOrEmpty(result));
    }

    [Fact]
    public void TestAST()
    {
        string input = "import module1; module test { var x: int = 10; }";
        FlowDriver driver = new FlowDriver(input);

        var listener = driver.WalkTree();
        _testOutputHelper.WriteLine(listener.AST.ToString());
    }
}