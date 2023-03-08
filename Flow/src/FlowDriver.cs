using System;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

public class FlowDriver
{
    public string Parse(string input)
    {
        AntlrInputStream inputStream = new AntlrInputStream(input);
        FlowLexer lexer = new FlowLexer(inputStream);
        CommonTokenStream tokens = new CommonTokenStream(lexer);
        FlowParser parser = new FlowParser(tokens);
        IParseTree tree = parser.variable_declaration();
        return tree.ToStringTree(parser);
    }
}