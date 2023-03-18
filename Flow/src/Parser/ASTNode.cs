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

        public bool HasParent<T>() where T : ParserRuleContext
        {
            var parent = Context.Parent;
            while (parent != null)
            {
                if (parent is T) return true;

                parent = parent.Parent;
            }

            return false;
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
            var idContext = Context?.GetRuleContext<FlowParser.IdentifierContext>(0);

            bool showContext = litContext != null || expContext != null || stContext != null || idContext != null;
            showContext &= !(this is ProgramNode 
                             || this is BlockStatementNode 
                             || this is WhileStatementNode 
                             || this is ForStatementNode
                             || this is ModuleDeclarationNode);
            if (Context != null && showContext)
            {
                postScript = $" ({Context?.GetText()})";
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