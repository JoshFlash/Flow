using System;
using System.Collections.Generic;
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
        
        public override void EnterExpression(FlowParser.ExpressionContext context)
        {
            var expressionNode = new ExpressionNode("expression", new List<ASTNode>(), context);
            nodeStack.Peek().Children.Add(expressionNode);
            nodeStack.Push(expressionNode);
        }

        public override void ExitExpression(FlowParser.ExpressionContext context)
        {
            nodeStack.Pop();
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

        public override void EnterFunction_call_expression([NotNull] FlowParser.Function_call_expressionContext context)
        {
            var children = new List<ASTNode>();
            var functionCallExpressionNode = new FunctionCallExpressionNode("function_call_expression", children, context);
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
            var parameterListNode = new ParameterListNode("parameter_list", new List<ASTNode>(), context.parameter_list());
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

    }
}