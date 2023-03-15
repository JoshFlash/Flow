using System;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace Flow
{
    public class FlowListener : FlowBaseListener
    {
        public ASTNode AST { get; private set; }

        private Stack<ASTNode> nodeStack = new Stack<ASTNode>();

        public override void VisitErrorNode(IErrorNode node)
        {
            Console.Error.WriteLine("Error: " + node.GetText());
        }

        public override void VisitTerminal([NotNull] ITerminalNode node)
        {
            Console.WriteLine($"Terminal node: {node.Symbol.Type} = {node.Symbol.Text}");
            base.VisitTerminal(node);
        }

        public override void EnterProgram([NotNull] FlowParser.ProgramContext context)
        {
            var children = new List<ASTNode>();
            AST = new ProgramNode("program", children, context);
            nodeStack.Push(AST);
        }

        public override void ExitProgram([NotNull] FlowParser.ProgramContext context)
        {
            nodeStack.Pop();
        }

        public override void EnterModule_declaration([NotNull] FlowParser.Module_declarationContext context)
        {
            var children = new List<ASTNode>();
            var moduleDeclarationNode = new ModuleDeclarationNode("module_declaration", children, context);
            nodeStack.Peek().Children.Add(moduleDeclarationNode);
            nodeStack.Push(moduleDeclarationNode);
        }

        public override void ExitModule_declaration([NotNull] FlowParser.Module_declarationContext context)
        {
            nodeStack.Pop();
        }

        public override void EnterConstant_declaration(FlowParser.Constant_declarationContext context)
        {
            var constantDeclarationNode =
                new ConstantDeclarationNode("constant_declaration", new List<ASTNode>(), context);
            nodeStack.Peek().Children.Add(constantDeclarationNode);
            nodeStack.Push(constantDeclarationNode);
        }

        public override void ExitConstant_declaration(FlowParser.Constant_declarationContext context)
        {
            nodeStack.Pop();
        }

        public override void EnterVariable_declaration([NotNull] FlowParser.Variable_declarationContext context)
        {
            var children = new List<ASTNode>();
            var variableDeclarationNode = new VariableDeclarationNode("variable_declaration", children, context);
            nodeStack.Peek().Children.Add(variableDeclarationNode);
            nodeStack.Push(variableDeclarationNode);
        }

        public override void ExitVariable_declaration([NotNull] FlowParser.Variable_declarationContext context)
        {
            nodeStack.Pop();
        }

        public override void EnterVariable_value([NotNull] FlowParser.Variable_valueContext context)
        {
            var children = new List<ASTNode>();
            var variableValueNode = new VariableValueNode(context.GetText(), children, context);
            nodeStack.Peek().Children.Add(variableValueNode);
            nodeStack.Push(variableValueNode);
        }

        public override void ExitVariable_value([NotNull] FlowParser.Variable_valueContext context)
        {
            nodeStack.Pop();
        }

        public override void EnterRange_clause([NotNull] FlowParser.Range_clauseContext context)
        {
            var children = new List<ASTNode>();
            var rangeClauseNode = new RangeClauseNode("range_clause", children, context);
            nodeStack.Peek().Children.Add(rangeClauseNode);
            nodeStack.Push(rangeClauseNode);
        }

        public override void ExitRange_clause([NotNull] FlowParser.Range_clauseContext context)
        {
            nodeStack.Pop();
        }

        public override void EnterUnary_operation([NotNull] FlowParser.Unary_operationContext context)
        {
            var children = new List<ASTNode>();
            var unaryOperationNode = new UnaryOperationNode("unary_operation", children, context);
            nodeStack.Peek().Children.Add(unaryOperationNode);
            nodeStack.Push(unaryOperationNode);
        }

        public override void ExitUnary_operation([NotNull] FlowParser.Unary_operationContext context)
        {
            nodeStack.Pop();
        }

        public override void EnterStatement_block(FlowParser.Statement_blockContext context)
        {
            var blockStatementNode = new BlockStatementNode("block_statement", new List<ASTNode>(), context);
            nodeStack.Peek().Children.Add(blockStatementNode);
            nodeStack.Push(blockStatementNode);
        }

        public override void ExitStatement_block(FlowParser.Statement_blockContext context)
        {
            nodeStack.Pop();
        }
        
        public override void EnterFunction_call_statement([NotNull] FlowParser.Function_call_statementContext context)
        {
            var children = new List<ASTNode>();
            var functionCallStatementNode = new FunctionCallStatementNode("function_call_statement", children, context);
            nodeStack.Peek().Children.Add(functionCallStatementNode);
            nodeStack.Push(functionCallStatementNode);
        }

        public override void ExitFunction_call_statement([NotNull] FlowParser.Function_call_statementContext context)
        {
            nodeStack.Pop();
        }

        public override void EnterFunction_call_expression([NotNull] FlowParser.Function_call_expressionContext context)
        {
            var children = new List<ExpressionNode>();
            var functionCallExpressionNode =
                new FunctionCallExpressionNode("function_call_expression", children, context);
            nodeStack.Peek().Children.Add(functionCallExpressionNode);
            nodeStack.Push(functionCallExpressionNode);
        }

        public override void ExitFunction_call_expression([NotNull] FlowParser.Function_call_expressionContext context)
        {
            nodeStack.Pop();
        }

        public override void EnterFunction_declaration(FlowParser.Function_declarationContext context)
        {
            var identifierNode = new IdentifierNode("identifier", new List<ASTNode>(), context.identifier());
            var parameterListNode =
                new ParameterListNode("parameter_list", new List<ASTNode>(), context.parameter_list());
            var returnTypeNode = context.type() != null
                ? new TypeNode("type", new List<ASTNode>(), context.type())
                : null;
            var blockStatementNode = new BlockStatementNode("block", new List<ASTNode>(), context.statement_block());

            var children = new List<ASTNode> { identifierNode, parameterListNode, returnTypeNode, blockStatementNode };
            var functionDeclarationNode = new FunctionDeclarationNode("function_declaration", children, context);
            nodeStack.Peek().Children.Add(functionDeclarationNode);
            nodeStack.Push(functionDeclarationNode);
        }

        public override void ExitFunction_declaration(FlowParser.Function_declarationContext context)
        {
            nodeStack.Pop();
        }

        public override void EnterParameter_list(FlowParser.Parameter_listContext context)
        {
            var parameterListNode = nodeStack.Peek().FindDescendantOfType<ParameterListNode>();
            if (parameterListNode == null)
            {
                throw new InvalidOperationException("Parameter list not found in the function declaration");
            }

            foreach (var parameterContext in context.parameter())
            {
                var parameterNode = new ParameterNode("parameter", new List<ASTNode>(), parameterContext);
                parameterListNode.Children.Add(parameterNode);
                nodeStack.Push(parameterNode);
            }
        }

        public override void ExitParameter_list(FlowParser.Parameter_listContext context)
        {
            foreach (var _ in context.parameter())
            {
                nodeStack.Pop();
            }
        }

        public override void EnterParameter(FlowParser.ParameterContext context)
        {
            var parameterNode = new ParameterNode("parameter", new List<ASTNode>(), context);
            var identifierNode = new IdentifierNode("identifier", new List<ASTNode>(), context.identifier());
            var typeNode = new TypeNode("type", new List<ASTNode>(), context.type());

            parameterNode.Children.Add(identifierNode);
            parameterNode.Children.Add(typeNode);

            // Add the created ParameterNode to the Parameters list of the ParameterListNode
            var parameterListNode = nodeStack.Peek().FindDescendantOfType<ParameterListNode>();
            if (parameterListNode == null)
            {
                throw new InvalidOperationException("ParameterListNode not found in the node stack");
            }

            parameterListNode.Parameters.Add(parameterNode);
        }

        public override void ExitParameter(FlowParser.ParameterContext context)
        {
            // No action required for now
            base.ExitParameter(context);
        }

        public override void EnterFor_statement(FlowParser.For_statementContext context)
        {
            var forStatementNode = new ForStatementNode("for_statement", new List<ASTNode>(), context);
            nodeStack.Peek().Children.Add(forStatementNode);
            nodeStack.Push(forStatementNode);
        }

        public override void ExitFor_statement(FlowParser.For_statementContext context)
        {
            nodeStack.Pop();
        }

        public override void EnterIf_statement(FlowParser.If_statementContext context)
        {
            var ifStatementNode = new IfStatementNode("if_statement", new List<ASTNode>(), context);
            nodeStack.Peek().Children.Add(ifStatementNode);
            nodeStack.Push(ifStatementNode);
        }

        public override void ExitIf_statement(FlowParser.If_statementContext context)
        {
            nodeStack.Pop();
        }

        public override void EnterWhile_statement(FlowParser.While_statementContext context)
        {
            var whileStatementNode = new WhileStatementNode("while_statement", new List<ASTNode>(), context);
            nodeStack.Peek().Children.Add(whileStatementNode);
            nodeStack.Push(whileStatementNode);
        }

        public override void ExitWhile_statement(FlowParser.While_statementContext context)
        {
            nodeStack.Pop();
        }
        
        public override void EnterAssignment_statement(FlowParser.Assignment_statementContext context)
        {
            var assignmentStatementNode = new AssignmentStatementNode("assignment_statement", new List<ASTNode>(), context);
            nodeStack.Peek().Children.Add(assignmentStatementNode);
            nodeStack.Push(assignmentStatementNode);
        }

        public override void ExitAssignment_statement(FlowParser.Assignment_statementContext context)
        {
            nodeStack.Pop();
        }
        
        public override void EnterReturn_statement([NotNull] FlowParser.Return_statementContext context)
        {
            var children = new List<ASTNode>();
            var returnStatementNode = new ReturnStatementNode("return_statement", children, context);
            nodeStack.Peek().Children.Add(returnStatementNode);
            nodeStack.Push(returnStatementNode);
        }

        public override void ExitReturn_statement([NotNull] FlowParser.Return_statementContext context)
        {
            nodeStack.Pop();
        }

        public override void EnterLogical_or(FlowParser.Logical_orContext context)
        {
            if (context.ChildCount > 1)
            {
                var logicalOrNode = new LogicalOrNode("logical_or", new List<ExpressionNode>(), context);
                nodeStack.Peek().Children.Add(logicalOrNode);
                nodeStack.Push(logicalOrNode);
            }
        }

        public override void ExitLogical_or(FlowParser.Logical_orContext context)
        {
            // Only pop the node stack if the logical_or node was actually added
            if (context.ChildCount != 1)
            {
                nodeStack.Pop();
            }
        }

        public override void EnterLogical_and(FlowParser.Logical_andContext context)
        {
            if (context.ChildCount > 1)
            {
                var logicalOrNode = new LogicalAndNode("logical_or", new List<ExpressionNode>(), context);
                nodeStack.Peek().Children.Add(logicalOrNode);
                nodeStack.Push(logicalOrNode);
            }
        }

        public override void ExitLogical_and(FlowParser.Logical_andContext context)
        {
            // Only pop the node stack if the logical_or node was actually added
            if (context.ChildCount > 1)
            {
                nodeStack.Pop();
            }
        }

        public override void EnterEquality(FlowParser.EqualityContext context)
        {
            if (context.ChildCount > 1)
            {
                var equalityNode = new EqualityNode("equality", new List<ExpressionNode>(), context);
                nodeStack.Peek().Children.Add(equalityNode);
                nodeStack.Push(equalityNode);
            }
        }

        public override void ExitEquality(FlowParser.EqualityContext context)
        {
            if (context.ChildCount > 1)
            {
                nodeStack.Pop();
            }
        }

        public override void EnterRelational(FlowParser.RelationalContext context)
        {
            if (context.ChildCount > 1)
            {
                var relationalNode = new RelationalNode("relational", new List<ExpressionNode>(), context);
                nodeStack.Peek().Children.Add(relationalNode);
                nodeStack.Push(relationalNode);
            }
        }

        public override void ExitRelational(FlowParser.RelationalContext context)
        {
            if (context.ChildCount > 1)
            {
                nodeStack.Pop();
            }
        }

        public override void EnterAdditive(FlowParser.AdditiveContext context)
        {
            if (context.ChildCount > 1)
            {
                var additiveNode = new AdditiveNode("additive", new List<ExpressionNode>(), context);
                nodeStack.Peek().Children.Add(additiveNode);
                nodeStack.Push(additiveNode);
            }
        }

        public override void ExitAdditive(FlowParser.AdditiveContext context)
        {
            if (context.ChildCount > 1)
            {
                nodeStack.Pop();
            }
        }

        public override void EnterMultiplicative(FlowParser.MultiplicativeContext context)
        {
            if (context.ChildCount > 1)
            {
                var multiplicativeNode = new MultiplicativeNode("multiplicative", new List<ExpressionNode>(), context);
                nodeStack.Peek().Children.Add(multiplicativeNode);
                nodeStack.Push(multiplicativeNode);
            }
        }

        public override void ExitMultiplicative(FlowParser.MultiplicativeContext context)
        {
            if (context.ChildCount > 1)
            {
                nodeStack.Pop();
            }
        }
    }
}