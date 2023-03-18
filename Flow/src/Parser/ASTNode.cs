using System.Collections.Generic;
using System.Text;
using Antlr4.Runtime;

namespace Flow
{
    public abstract class ASTNode
    {
        public string Text { get; }
        public List<ASTNode> Children { get; }
        public ParserRuleContext Context { get; }

        public ASTNode(string text, List<ASTNode> children, ParserRuleContext context)
        {
            Text = text;
            Children = children;
            Context = context;
        }
        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            ToStringHelper(sb, "", true);
            return sb.ToString();
        }
        
        private void ToStringHelper(StringBuilder sb, string prefix, bool isTail)
        {
            var postScript = "";
            var litContext = Context?.GetRuleContext<FlowParser.LiteralContext>(0);
            var expContext = Context?.GetRuleContext<FlowParser.ExpressionContext>(0);
            var stContext = Context?.GetRuleContext<FlowParser.StatementContext>(0);

            if (!(this is ProgramNode || this is BlockStatementNode))
            {
                if (litContext != null)
                {
                    postScript = $" ({litContext.GetText()})";
                }
                else if (expContext != null)
                {
                    postScript = $" ({expContext.GetText()})";
                }
                else if (stContext != null)
                {
                    postScript = $" ({stContext.GetText()})";
                }
            }

            string displayText = Text + postScript;

            sb.AppendLine($"{prefix}{(isTail ? "└── " : "├── ")}{displayText}");

            for (int i = 0; i < Children.Count - 1; i++)
            {
                if (Children[i] is null) continue;

                Children[i].ToStringHelper(sb, $"{prefix}{(isTail ? "    " : "│   ")}", false);
            }

            if (Children.Count > 0 && Children[Children.Count - 1] != null)
            {
                Children[Children.Count - 1].ToStringHelper(sb, $"{prefix}{(isTail ? "    " : "│   ")}", true);
            }
        }

        public abstract void Accept(IFlowListener listener);
    }
}