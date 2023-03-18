using System.Collections.Generic;
using Antlr4.Runtime;
using static FlowParser;

namespace Flow
{
    public abstract class StatementNode : ASTNode
    {
        public string Name { get; set; }
        
        public StatementNode(string name, List<ASTNode> children, ParserRuleContext context)
            : base(name, children, context)
        {
        }
    }
    
    public class BlockStatementNode : StatementNode
    {
        public BlockStatementNode(string text, List<ASTNode> children, Statement_blockContext context)
            : base(text, children, context)
        {
        }

        public override void Accept(IFlowListener listener)
        {
            var context = Context as Statement_blockContext;
            listener.EnterStatement_block(context);
            foreach (var child in Children)
            {
                child.Accept(listener);
            }

            listener.ExitStatement_block(context);
        }
    }
    
    public class ConstantDeclarationNode : StatementNode
    {
        public TypeNode Type { get; set; }
        public VariableValueNode Value { get; set; }

        public ConstantDeclarationNode(string text, List<ASTNode> children, Constant_declarationContext context)
            : base(text, children, context)
        {
            Name = context.identifier().GetText();
            Type = new TypeNode("type", new List<ASTNode>(), context.type());
            Value = new VariableValueNode("value", new List<ASTNode>(), context.variable_value());
        }

        public override void Accept(IFlowListener listener)
        {
            var context = Context as Constant_declarationContext;
            listener.EnterConstant_declaration(context);
            foreach (var child in Children)
            {
                child.Accept(listener);
            }

            listener.ExitConstant_declaration(context);
        }
    }

    public class VariableDeclarationNode : StatementNode
    {
        public VariableDeclarationNode(string text, List<ASTNode> children, Variable_declarationContext context)
            : base(text, children, context)
        {
            Name = context.identifier().GetText();
        }

        public override void Accept(IFlowListener listener)
        {
            var context = Context as Variable_declarationContext;
            listener.EnterVariable_declaration(context);
            foreach (var child in Children)
            {
                child.Accept(listener);
            }

            listener.ExitVariable_declaration(context);
        }
    }
    
    public class ForStatementNode : StatementNode
    {
        public IdentifierNode Identifier { get; set; }
        public RangeClauseNode RangeClause { get; set; }
        public ExpressionNode Condition { get; set; }
        public BlockStatementNode Body { get; set; }

        public ForStatementNode(string text, List<ASTNode> children, For_statementContext context)
            : base(text, children, context)
        {
            Identifier = new IdentifierNode("identifier", null, context.identifier());
            RangeClause = new RangeClauseNode("range_clause", null, context.range_clause());
            Condition = context.expression() != null
                ? ExpressionNode.CreateExpressionNodeFromContext(context.expression())
                : null;
            Body = new BlockStatementNode("block", null, context.statement_block());
        }

        public override void Accept(IFlowListener listener)
        {
            var context = Context as For_statementContext;
            listener.EnterFor_statement(context);
            foreach (var child in Children)
            {
                child.Accept(listener);
            }

            listener.ExitFor_statement(context);
        }
    }

    public class IfStatementNode : StatementNode
    {
        public ExpressionNode Condition { get; set; }
        public BlockStatementNode TrueBlock { get; set; }
        public BlockStatementNode FalseBlock { get; set; }

        public IfStatementNode(string text, List<ASTNode> children, If_statementContext context)
            : base(text, children, context)
        {
            Condition = ExpressionNode.CreateExpressionNodeFromContext(context.expression());
            TrueBlock = new BlockStatementNode("block", null, context.statement_block(0));
            FalseBlock = context.statement_block().Length > 1
                ? new BlockStatementNode("block", null, context.statement_block(1))
                : null;
        }

        public override void Accept(IFlowListener listener)
        {
            var context = Context as If_statementContext;
            listener.EnterIf_statement(context);
            foreach (var child in Children)
            {
                child.Accept(listener);
            }

            listener.ExitIf_statement(context);
        }
    }

    public class WhileStatementNode : StatementNode
    {
        public ExpressionNode Condition { get; set; }
        public BlockStatementNode Body { get; set; }

        public WhileStatementNode(string text, List<ASTNode> children, While_statementContext context)
            : base(text, children, context)
        {
            Condition = ExpressionNode.CreateExpressionNodeFromContext(context.expression());
            Body = new BlockStatementNode("block", null, context.statement_block());
        }

        public override void Accept(IFlowListener listener)
        {
            var context = Context as While_statementContext;
            listener.EnterWhile_statement(context);
            foreach (var child in Children)
            {
                child.Accept(listener);
            }

            listener.ExitWhile_statement(context);
        }
    }
    
    public class FunctionCallStatementNode : StatementNode
    {
        public FunctionCallStatementNode(string text, List<ASTNode> children, Function_call_statementContext context)
            : base(text, children, context)
        {
            Name = context.identifier().GetText();
        }

        public override void Accept(IFlowListener listener)
        {
            var context = Context as Function_call_statementContext;
            listener.EnterFunction_call_statement(context);
            foreach (var child in Children)
            {
                child.Accept(listener);
            }

            listener.ExitFunction_call_statement(context);
        }
    }
    
    public class FunctionDeclarationNode : StatementNode
    {
        public BlockStatementNode Body { get; set; }

        public FunctionDeclarationNode(string text, List<ASTNode> children, Function_declarationContext context)
            : base(text, children ?? new List<ASTNode>(), context)
        {
            Name = context.identifier().GetText();
            Body = new BlockStatementNode("block", null, context.statement_block());
        }

        public override void Accept(IFlowListener listener)
        {
            var context = Context as Function_declarationContext;
            listener.EnterFunction_declaration(context);
            foreach (var child in Children)
            {
                child.Accept(listener);
            }

            listener.ExitFunction_declaration(context);
        }
    }
    
    public class AssignmentStatementNode : StatementNode
    {
        public AssignmentStatementNode(string text, List<ASTNode> children, Assignment_statementContext context)
            : base(text, children, context)
        {
        }

        public override void Accept(IFlowListener listener)
        {
            var context = Context as Assignment_statementContext;
            listener.EnterAssignment_statement(context);
            foreach (var child in Children)
            {
                child.Accept(listener);
            }

            listener.ExitAssignment_statement(context);
        }
    }
    
    public class ReturnStatementNode : StatementNode
    {
        public ReturnStatementNode(string text, List<ASTNode> children, Return_statementContext context)
            : base(text, children, context)
        {
        }

        public override void Accept(IFlowListener listener)
        {
            var context = Context as Return_statementContext;
            listener.EnterReturn_statement(context);
            foreach (var child in Children)
            {
                child.Accept(listener);
            }

            listener.ExitReturn_statement(context);
        }
    }
    
    public class PrintStatementNode : StatementNode
    {
        public PrintStatementNode(string text, List<ASTNode> children, Print_statementContext context)
            : base(text, children, context)
        {
        }

        public override void Accept(IFlowListener listener)
        {
            var context = Context as Print_statementContext;
            listener.EnterPrint_statement(context);
            foreach (var child in Children)
            {
                child.Accept(listener);
            }
            listener.ExitPrint_statement(context);
        }
    }
    
}