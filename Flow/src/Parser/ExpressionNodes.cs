using System;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using static FlowParser;

namespace Flow
{
    public abstract class ExpressionNode : ASTNode
    {
        public new List<ExpressionNode> Children;
        
        public ExpressionNode(string text, List<ExpressionNode> children, ParserRuleContext context)
            : base(text, new List<ASTNode>(), context)
        {
            Children = children;
        }

        public static ExpressionNode CreateExpressionNodeFromContext(ExpressionContext context)
        {
            return CreateLogicalOrNodeFromContext(context.logical_or());
        }

        private static ExpressionNode CreateLogicalOrNodeFromContext(Logical_orContext context)
        {
            var children = context.logical_and()
                .Select(CreateLogicalAndNodeFromContext).ToList();
            return new LogicalOrNode("logical_or", children, context);
        }

        private static ExpressionNode CreateLogicalAndNodeFromContext(Logical_andContext context)
        {
            var children = context.equality()
                .Select(CreateEqualityNodeFromContext).ToList();
            return new LogicalAndNode("logical_and", children, context);
        }

        private static ExpressionNode CreateEqualityNodeFromContext(EqualityContext context)
        {
            var children = context.relational()
                .Select(CreateRelationalNodeFromContext).ToList();
            return new EqualityNode("equality", children, context);
        }

        private static ExpressionNode CreateRelationalNodeFromContext(RelationalContext context)
        {
            var children = context.additive()
                .Select(CreateAdditiveNodeFromContext).ToList();
            return new RelationalNode("relational", children, context);
        }

        private static ExpressionNode CreateAdditiveNodeFromContext(AdditiveContext context)
        {
            var children = context.multiplicative()
                .Select(CreateMultiplicativeNodeFromContext).ToList();
            return new AdditiveNode("additive", children, context);
        }

        private static ExpressionNode CreateMultiplicativeNodeFromContext(MultiplicativeContext context)
        {
            var children = context.expression_value()
                .Select(CreateExpressionValueNodeFromContext).ToList();
            return new MultiplicativeNode("multiplicative", children, context);
        }

        private static ExpressionNode CreateExpressionValueNodeFromContext(Expression_valueContext context)
        {
            var children = new List<ExpressionNode>();
            if (context.literal() != null)
            {
                return new LiteralNode(
                    "literal", 
                    children, 
                    context.literal()
                );
            }
            if (context.identifier() != null)
            {
                return new IdentifierNode(
                    "identifier", 
                    children, 
                    context.identifier()
                );
            }
            if (context.unary_operation() != null)
            {
                return new UnaryOperationNode(
                    "unary_operation", 
                    children, 
                    context.unary_operation()
                );
            }
            if (context.function_call_expression() != null)
            {
                return new FunctionCallExpressionNode(
                    "function_call_expression", 
                    children,
                    context.function_call_expression()
                );
            }

            if (context.element_access_expression() != null)
            {
                return new ElementAccessExpressionNode(
                    "element_access_expression",
                    children,
                    context.element_access_expression()
                );
            }
            if (context.expression() != null)
            {
                return CreateExpressionNodeFromContext(context.expression());
            }
            
            throw new InvalidOperationException("Unexpected expression value context");
        }
    }

    public class FunctionCallExpressionNode : ExpressionNode
    {
        public FunctionCallExpressionNode(string text, List<ExpressionNode> children, Function_call_expressionContext context)
            : base(text, children, context)
        {
        }

        public override void Accept(IFlowListener listener)
        {
            var context = Context as Function_call_expressionContext;
            listener.EnterFunction_call_expression(context);
            foreach (var child in Children)
            {
                child.Accept(listener);
            }

            listener.ExitFunction_call_expression(context);
        }
    }

    public class ElementAccessExpressionNode : ExpressionNode
    {
        public ElementAccessExpressionNode(string text, List<ExpressionNode> children, Element_access_expressionContext context)
            : base(text, children, context)
        {
        }
        
        public override void Accept(IFlowListener listener)
        {
            var context = Context as Element_access_expressionContext;
            listener.EnterElement_access_expression(context);
            foreach (var child in Children)
            {
                child.Accept(listener);
            }

            listener.EnterElement_access_expression(context);
        }
    }

    public class LogicalOrNode : ExpressionNode
    {
        public LogicalOrNode(string type, List<ExpressionNode> children, Logical_orContext context)
            : base(type, children, context)
        {
        }

        public override void Accept(IFlowListener listener)
        {
            var context = Context as Logical_orContext;
            listener.EnterLogical_or(context);
            foreach (var child in Children)
            {
                child.Accept(listener);
            }

            listener.ExitLogical_or(context);
        }
    }

    public class LogicalAndNode : ExpressionNode
    {
        public LogicalAndNode(string type, List<ExpressionNode> children, Logical_andContext context)
            : base(type, children, context)
        {
        }

        public override void Accept(IFlowListener listener)
        {
            var context = Context as Logical_andContext;
            listener.EnterLogical_and(context);
            foreach (var child in Children)
            {
                child.Accept(listener);
            }

            listener.ExitLogical_and(context);
        }
    }

    public class EqualityNode : ExpressionNode
    {
        public EqualityNode(string type, List<ExpressionNode> children, EqualityContext context)
            : base(type, children, context)
        {
        }

        public override void Accept(IFlowListener listener)
        {
            var context = Context as EqualityContext;
            listener.EnterEquality(context);
            foreach (var child in Children)
            {
                child.Accept(listener);
            }

            listener.ExitEquality(context);
        }
    }

    public class RelationalNode : ExpressionNode
    {
        public RelationalNode(string type, List<ExpressionNode> children, RelationalContext context)
            : base(type, children, context)
        {
        }

        public override void Accept(IFlowListener listener)
        {
            var context = Context as RelationalContext;
            listener.EnterRelational(context);
            foreach (var child in Children)
            {
                child.Accept(listener);
            }

            listener.ExitRelational(context);
        }
    }

    public class AdditiveNode : ExpressionNode
    {
        public AdditiveNode(string type, List<ExpressionNode> children, AdditiveContext context)
            : base(type, children, context)
        {
        }

        public override void Accept(IFlowListener listener)
        {
            var context = Context as AdditiveContext;
            listener.EnterAdditive(context);
            foreach (var child in Children)
            {
                child.Accept(listener);
            }

            listener.ExitAdditive(context);
        }
    }

    public class MultiplicativeNode : ExpressionNode
    {
        public MultiplicativeNode(string type, List<ExpressionNode> children, MultiplicativeContext context)
            : base(type, children, context)
        {
        }

        public override void Accept(IFlowListener listener)
        {
            var context = Context as MultiplicativeContext;
            listener.EnterMultiplicative(context);
            foreach (var child in Children)
            {
                child.Accept(listener);
            }

            listener.ExitMultiplicative(context);
        }
    }
    
    public class UnaryOperationNode : ExpressionNode
    {
        public UnaryOperationNode(string text, List<ExpressionNode> children, Unary_operationContext context)
            : base(text, children, context)
        {
        }

        public override void Accept(IFlowListener listener)
        {
            var context = Context as Unary_operationContext;
            listener.EnterUnary_operation(context);
            foreach (var child in Children)
            {
                child.Accept(listener);
            }

            listener.ExitUnary_operation(context);
        }
    }
    
    public class ExpressionValueNode : ExpressionNode
    {
        public ExpressionValueNode(string text, List<ExpressionNode> children, Expression_valueContext context)
            : base(text, children, context)
        {
        }

        public override void Accept(IFlowListener listener)
        {
            var context = Context as Expression_valueContext;
            listener.EnterExpression_value(context);
            foreach (var child in Children)
            {
                child.Accept(listener);
            }

            listener.ExitExpression_value(context);
        }
    }
    
    public class IdentifierNode : ExpressionNode
    {
        public string Identifier { get; set; }

        public IdentifierNode(string name, List<ExpressionNode> children, IdentifierContext context)
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
    
    public class LiteralNode : ExpressionNode
    {
        public LiteralNode(string text, List<ExpressionNode> children, LiteralContext context)
            : base(text, children, context)
        {
        }

        public override void Accept(IFlowListener listener)
        {
            var context = Context as LiteralContext;
            listener.EnterLiteral(context);
            foreach (var child in Children)
            {
                child.Accept(listener);
            }

            listener.ExitLiteral(context);
        }
    }

}