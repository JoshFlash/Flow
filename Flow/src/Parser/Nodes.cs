using System.Collections.Generic;
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
            foreach (ASTNode child in Children)
            {
                child.Accept(listener);
            }

            listener.ExitProgram(context);
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
            foreach (ASTNode child in Children)
            {
                child.Accept(listener);
            }

            listener.ExitModule_declaration(context);
        }
    }

    public class VariableDeclarationNode : ASTNode
    {
        public VariableDeclarationNode(string text, List<ASTNode> children, Variable_declarationContext context)
            : base(text, children, context)
        {
        }

        public override void Accept(IFlowListener listener)
        {
            var context = Context as Variable_declarationContext;
            listener.EnterVariable_declaration(context);
            foreach (ASTNode child in Children)
            {
                child.Accept(listener);
            }

            listener.ExitVariable_declaration(context);
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
            foreach (ASTNode child in Children)
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
            foreach (ASTNode child in Children)
            {
                child.Accept(listener);
            }

            listener.ExitRange_clause(context);
        }
    }

    public class ParameterListNode : ASTNode
    {
        public List<ParameterNode> Parameters { get; set; }

        public ParameterListNode(string type, List<ASTNode> children, Parameter_listContext context)
            : base(type, children, context)
        {
            Parameters = new List<ParameterNode>();
            foreach (var parameter in context.parameter())
            {
                Parameters.Add(new ParameterNode("parameter", null, parameter));
            }
        }

        public override void Accept(IFlowListener listener)
        {
            var context = Context as Parameter_listContext;
            listener.EnterParameter_list(context);
            foreach (ASTNode child in Children)
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
            foreach (ASTNode child in Children)
            {
                child.Accept(listener);
            }

            listener.ExitParameter(context);
        }
    }
    
    public class UnaryOperationNode : ASTNode
    {
        public UnaryOperationNode(string text, List<ASTNode> children, FlowParser.Unary_operationContext context)
            : base(text, children, context) { }

        public override void Accept(IFlowListener listener)
        {
            var context = Context as FlowParser.Unary_operationContext;
            listener.EnterUnary_operation(context);
            foreach (ASTNode child in Children)
            {
                child.Accept(listener);
            }
            listener.ExitUnary_operation(context);
        }
    }

    public class FunctionCallExpressionNode : ASTNode
    {
        public FunctionCallExpressionNode(string text, List<ASTNode> children, FlowParser.Function_call_expressionContext context)
            : base(text, children, context) { }

        public override void Accept(IFlowListener listener)
        {
            var context = Context as Function_call_expressionContext;
            listener.EnterFunction_call_expression(context);
            foreach (ASTNode child in Children)
            {
                child.Accept(listener);
            }
            listener.ExitFunction_call_expression(context);
        }
    }
    
    public class BlockStatementNode : ASTNode
    {
        public BlockStatementNode(string text, List<ASTNode> children, Statement_blockContext context)
            : base(text, children, context)
        {
        }

        public override void Accept(IFlowListener listener)
        {
            var context = Context as Statement_blockContext;
            listener.EnterStatement_block(context);
            foreach (ASTNode child in Children)
            {
                child.Accept(listener);
            }

            listener.ExitStatement_block(context);
        }
    }
    
    public class FunctionDeclarationNode : ASTNode
    {
        public string Name { get; set; }
        public ParameterListNode Parameters { get; set; }
        public BlockStatementNode Body { get; set; }

        public FunctionDeclarationNode(string text, List<ASTNode> children, Function_declarationContext context)
            : base(text, children, context)
        {
            Name = context.identifier().GetText();
            Parameters = new ParameterListNode("parameter_list", null, context.parameter_list());
            Body = new BlockStatementNode("block", null, context.statement_block());
        }

        public override void Accept(IFlowListener listener)
        {
            var context = Context as FlowParser.Function_declarationContext;
            listener.EnterFunction_declaration(context);
            foreach (ASTNode child in Children)
            {
                child.Accept(listener);
            }
            listener.ExitFunction_declaration(context);
        }
    }


}