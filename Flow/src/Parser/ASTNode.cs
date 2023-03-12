﻿using System.Collections.Generic;
using System.Text;

namespace Flow
{
    public abstract class ASTNode
    {
        public string Text { get; }
        public List<ASTNode> Children { get; }

        public ASTNode(string text, List<ASTNode> children)
        {
            Text = text;
            Children = children;
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
                Children[i].ToStringHelper(sb, $"{prefix}{(isTail ? "    " : "│   ")}", false);
            }

            if (Children.Count > 0)
            {
                Children[Children.Count - 1].ToStringHelper(sb, $"{prefix}{(isTail ? "    " : "│   ")}", true);
            }
        }

        public abstract void Accept(IFlowListener listener);
    }
}