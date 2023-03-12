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

            // Add VariableValueNode as child of VariableDeclarationNode
            var variableValueNode = new VariableValueNode("variable_value", new List<ASTNode>(), context.variable_value());
            variableDeclarationNode.Children.Add(variableValueNode);
            nodeStack.Push(variableValueNode);
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
            var blockStatementNode = new BlockStatementNode(context.GetText(), new List<ASTNode>(), context);
            nodeStack.Peek().Children.Add(blockStatementNode);
            nodeStack.Push(blockStatementNode);;
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
            var parameterListContext = context.parameter_list();
            var parameterListNode = new ParameterListNode("parameter_list", new List<ASTNode>(), parameterListContext);

            var bodyContext = context.statement_block();
            var bodyNode = new BlockStatementNode("block_statement", new List<ASTNode>(), bodyContext);

            var functionDeclarationNode = new FunctionDeclarationNode("function_declaration", new List<ASTNode> { parameterListNode, bodyNode }, context);
            nodeStack.Push(functionDeclarationNode);
        }

        public override void ExitFunction_declaration(FlowParser.Function_declarationContext context)
        {
            var functionDeclarationNode = nodeStack.Pop() as FunctionDeclarationNode;
            // Do something with the functionDeclarationNode
        }
        
        public override void VisitErrorNode(IErrorNode node)
        {
            Console.Error.WriteLine("Error: " + node.GetText());
        }
        
        public override void VisitTerminal([NotNull] ITerminalNode node)
        {
            Console.WriteLine($"Terminal node: {node.Symbol.Type} = {node.Symbol.Text}");
            base.VisitTerminal(node);
        }
    }
}
