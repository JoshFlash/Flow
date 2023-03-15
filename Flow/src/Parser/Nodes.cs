using System.Collections.Generic;
using Antlr4.Runtime;
using static FlowParser;

namespace Flow
{
    public class ExpressionNode : ASTNode
    {
        public ExpressionContext ExpressionContext { get; set; }
        public bool IsEmpty { get; set; } = false;

        public ExpressionNode(string text, List<ASTNode> children, ExpressionContext context)
            : base(text, children, context)
        {
            ExpressionContext = context;
        }

        public override void Accept(IFlowListener listener)
        {
            if (IsEmpty) return;

            var context = Context as ExpressionContext;
            listener.EnterExpression(context);

            // Recursively visit all children
            foreach (var child in ExpressionContext.children)
            {
                if (child is ParserRuleContext parserRuleContext)
                {
                    ASTNode childNode = null;

                    // TODO
                    // Check the type of parserRuleContext and create the corresponding ASTNode
                    if (parserRuleContext is Logical_orContext logicalOrContext)
                    {
                        // childNode = new LogicalOrNode("logical_or", new List<ASTNode>(), logicalOrContext);
                    }
                    else if (parserRuleContext is Logical_andContext logicalAndContext)
                    {
                        // childNode = new LogicalAndNode("logical_and", new List<ASTNode>(), logicalAndContext);
                    }
                    // Add more cases for other types of contexts, e.g., EqualityNode, RelationalNode, etc.

                    if (childNode != null)
                    {
                        childNode.Accept(listener);
                    }
                }
            }

            listener.ExitExpression(context);
        }
    }

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
        }

        public override void Accept(IFlowListener listener)
        {
            var context = Context as Parameter_listContext;
            listener.EnterParameter_list(context);

            // Here, we add the code to create ParameterNode instances.
            // The EnterParameter method should create the ParameterNode and add it to the Parameters list.
            foreach (var parameterContext in context.parameter())
            {
                listener.EnterParameter(parameterContext);
            }

            // After all parameters are processed, call the ExitParameter_list method.
            listener.ExitParameter_list(context);
        }
    }


    public class ParameterNode : ASTNode
    {
        public ParameterNode(string name, List<ASTNode> children, ParserRuleContext context)
            : base(name, children, context)
        {
        }

        public override void Accept(IFlowListener listener)
        {
            listener.EnterParameter((ParameterContext)Context);
            foreach (var child in Children)
            {
                child.Accept(listener);
            }

            listener.ExitParameter((ParameterContext)Context);
        }
    }

    public class IdentifierNode : ASTNode
    {
        public string Identifier { get; set; }

        public IdentifierNode(string name, List<ASTNode> children, IdentifierContext context)
            : base(name, children, context)
        {
            Identifier = context.GetText();
        }

        public override void Accept(IFlowListener listener)
        {
            listener.EnterIdentifier((IdentifierContext)Context);
            foreach (var child in Children)
            {
                child.Accept(listener);
            }

            listener.ExitIdentifier((IdentifierContext)Context);
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


    public class UnaryOperationNode : ASTNode
    {
        public UnaryOperationNode(string text, List<ASTNode> children, Unary_operationContext context)
            : base(text, children, context)
        {
        }

        public override void Accept(IFlowListener listener)
        {
            var context = Context as Unary_operationContext;
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
        public FunctionCallExpressionNode(string text, List<ASTNode> children, Function_call_expressionContext context)
            : base(text, children, context)
        {
        }

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

    public class LogicalOrNode : ASTNode
    {
        public LogicalOrNode(string type, List<ASTNode> children, Logical_orContext context)
            : base(type, children, context)
        {
        }

        public override void Accept(IFlowListener listener)
        {
            var context = Context as Logical_orContext;
            listener.EnterLogical_or(context);
            foreach (ASTNode child in Children)
            {
                child.Accept(listener);
            }

            listener.ExitLogical_or(context);
        }
    }

    public class LogicalAndNode : ASTNode
    {
        public LogicalAndNode(string type, List<ASTNode> children, Logical_andContext context)
            : base(type, children, context)
        {
        }

        public override void Accept(IFlowListener listener)
        {
            var context = Context as Logical_andContext;
            listener.EnterLogical_and(context);
            foreach (ASTNode child in Children)
            {
                child.Accept(listener);
            }

            listener.ExitLogical_and(context);
        }
    }

    public class EqualityNode : ASTNode
    {
        public EqualityNode(string type, List<ASTNode> children, EqualityContext context)
            : base(type, children, context)
        {
        }

        public override void Accept(IFlowListener listener)
        {
            var context = Context as EqualityContext;
            listener.EnterEquality(context);
            foreach (ASTNode child in Children)
            {
                child.Accept(listener);
            }

            listener.ExitEquality(context);
        }
    }

    public class RelationalNode : ASTNode
    {
        public RelationalNode(string type, List<ASTNode> children, RelationalContext context)
            : base(type, children, context)
        {
        }

        public override void Accept(IFlowListener listener)
        {
            var context = Context as RelationalContext;
            listener.EnterRelational(context);
            foreach (ASTNode child in Children)
            {
                child.Accept(listener);
            }

            listener.ExitRelational(context);
        }
    }

    public class AdditiveNode : ASTNode
    {
        public AdditiveNode(string type, List<ASTNode> children, AdditiveContext context)
            : base(type, children, context)
        {
        }

        public override void Accept(IFlowListener listener)
        {
            var context = Context as AdditiveContext;
            listener.EnterAdditive(context);
            foreach (ASTNode child in Children)
            {
                child.Accept(listener);
            }

            listener.ExitAdditive(context);
        }
    }

    public class MultiplicativeNode : ASTNode
    {
        public MultiplicativeNode(string type, List<ASTNode> children, MultiplicativeContext context)
            : base(type, children, context)
        {
        }

        public override void Accept(IFlowListener listener)
        {
            var context = Context as MultiplicativeContext;
            listener.EnterMultiplicative(context);
            foreach (ASTNode child in Children)
            {
                child.Accept(listener);
            }

            listener.ExitMultiplicative(context);
        }
    }
}