using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace Flow
{
    public class FlowDriver
    {
        public FlowDriver(string input)
        {
            inputStream = new AntlrInputStream(input);
            lexer = new FlowLexer(inputStream);
            tokens = new CommonTokenStream(lexer);
            parser = new FlowParser(tokens);
        }

        private readonly AntlrInputStream inputStream;
        private readonly FlowLexer lexer;
        private readonly CommonTokenStream tokens;
        private readonly FlowParser parser;

        public FlowParser Parser => parser;
        
        public IParseTree ParseVariableDecl()
        {
            IParseTree tree = parser.variable_declaration();
            return tree;
        }

        public FlowListener WalkTree()
        {
            var listener = new FlowListener();
            var context = parser.program();

            ParseTreeWalker.Default.Walk(listener, context);

            return listener;
        }
    }
}