using System.Collections.Generic;
using Antlr4.Runtime;
using static FlowParser;

namespace Flow
{
    public class ProgramNode : ASTNode
    {
        public ProgramNode(string text, List<ASTNode> children, ProgramContext context)
            : base(text, children, context)
        {
        }

        public override void Accept(IFlowListener listener)
        {
            var context = Context as ProgramContext;
            listener.EnterProgram(context);
            foreach (var child in Children)
            {
                child.Accept(listener);
            }

            listener.ExitProgram(context);
        }
    }

    public class ImportListNode : ASTNode
    {
        public ImportListNode(string text, List<ASTNode> children, ParserRuleContext context) : base(text, children, context)
        {
        }

        public override void Accept(IFlowListener listener)
        {
            var context = Context as Import_listContext;
            listener.EnterImport_list(context);
            foreach (var child in Children)
            {
                child.Accept(listener);
            }
            listener.ExitImport_list(context);
        }
    }
    
    public class ImportStatementNode : ASTNode
    {
        public ImportStatementNode(string text, List<ASTNode> children, ParserRuleContext context) : base(text, children, context)
        {
        }

        public override void Accept(IFlowListener listener)
        {
            var context = Context as Import_listContext;
            listener.EnterImport_list(context);
            foreach (var child in Children)
            {
                child.Accept(listener);
            }
            listener.ExitImport_list(context);
        }
    }

    public class ModuleDeclarationNode : ASTNode
    {
        public ModuleDeclarationNode(string text, List<ASTNode> children, Module_declarationContext context)
            : base(text, children, context)
        {
        }

        public override void Accept(IFlowListener listener)
        {
            var context = Context as Module_declarationContext;
            listener.EnterModule_declaration(context);
            foreach (var child in Children)
            {
                child.Accept(listener);
            }

            listener.ExitModule_declaration(context);
        }
    }

    public class VariableValueNode : ASTNode
    {
        public VariableValueNode(string text, List<ASTNode> children, Variable_valueContext context)
            : base(text, children, context)
        {
        }

        public override void Accept(IFlowListener listener)
        {
            var context = Context as Variable_valueContext;
            listener.EnterVariable_value(context);
            foreach (var child in Children)
            {
                child.Accept(listener);
            }

            listener.ExitVariable_value(context);
        }
    }

    public class RangeClauseNode : ASTNode
    {
        public RangeClauseNode(string text, List<ASTNode> children, Range_clauseContext context)
            : base(text, children, context)
        {
        }

        public override void Accept(IFlowListener listener)
        {
            var context = Context as Range_clauseContext;
            listener.EnterRange_clause(context);
            foreach (var child in Children)
            {
                child.Accept(listener);
            }

            listener.ExitRange_clause(context);
        }
    }

    public class TypeNode : ASTNode
    {
        public string TypeName { get; set; }

        public TypeNode(string name, List<ASTNode> children, TypeContext context)
            : base(name, children, context)
        {
            TypeName = context.GetText();
        }

        public override void Accept(IFlowListener listener)
        {
            listener.EnterType((TypeContext)Context);
            foreach (var child in Children)
            {
                child.Accept(listener);
            }

            listener.ExitType((TypeContext)Context);
        }
    }
    
    public class ParameterListNode : ASTNode
    {
        public ParameterListNode(string text, List<ASTNode> children, ParserRuleContext context)
            : base(text, children, context)
        {
        }

        public override void Accept(IFlowListener listener)
        {
            var context = Context as Parameter_listContext;
            listener.EnterParameter_list(context);
            foreach (var child in Children)
            {
                child.Accept(listener);
            }

            listener.ExitParameter_list(context);
        }
    }

    public class ParameterNode : ASTNode
    {

        public ParameterNode(string text, List<ASTNode> children, ParameterContext context)
            : base(text, children, context)
        {
        }

        public override void Accept(IFlowListener listener)
        {
            var context = Context as ParameterContext;
            listener.EnterParameter(context);
            foreach (var child in Children)
            {
                child.Accept(listener);
            }

            listener.ExitParameter(context);
        }
    }

}