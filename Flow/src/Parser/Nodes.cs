using System.Collections.Generic;

namespace Flow
{
    public class ProgramNode : ASTNode
    {
        public ProgramNode(string text, List<ASTNode> children) : base(text, children) { }

        public override void Accept(IFlowListener listener)
        {
            listener.EnterProgram(null);
        }
    }

    public class ModuleDeclarationNode : ASTNode
    {
        public ModuleDeclarationNode(string text, List<ASTNode> children) : base(text, children) { }

        public override void Accept(IFlowListener listener)
        {
            listener.EnterModule_declaration(null);
        }
    }    
}