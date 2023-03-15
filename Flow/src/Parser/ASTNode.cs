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
            sb.AppendLine($"{prefix}{(isTail ? "└── " : "├── ")}{Text}");

            for (int i = 0; i < Children.Count - 1; i++)
            {
                if (Children[i] is null) continue;
                
                Children[i].ToStringHelper(sb, $"{prefix}{(isTail ? "    " : "│   ")}", false);
            }

            if (Children.Count > 0)
            {
                Children[Children.Count - 1].ToStringHelper(sb, $"{prefix}{(isTail ? "    " : "│   ")}", true);
            }
        }
        
        public T FindDescendantOfType<T>() where T : ASTNode
        {
            foreach (var child in Children)
            {
                if (child is T descendant)
                {
                    return descendant;
                }

                var result = child.FindDescendantOfType<T>();
                if (result != null)
                {
                    return result;
                }
            }

            return default(T);
        }

        public abstract void Accept(IFlowListener listener);
    }
}