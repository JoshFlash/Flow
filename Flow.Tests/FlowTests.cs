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
        FlowDriver driver = new FlowDriver();
        var result = driver.Parse(input);
        _testOutputHelper.WriteLine(result);
        Console.WriteLine(result);
        Assert.False(string.IsNullOrEmpty(result));
    }
}