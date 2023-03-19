using System;
using System.Collections.Generic;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using static FlowParser;

namespace Flow
{
    public class FlowListener : FlowBaseListener
    {
        public ASTNode AST { get; private set; }

        private Stack<ASTNode> nodeStack = new Stack<ASTNode>();

        private StringBuilder stringBuilder = new StringBuilder();
        private TargetBackend backend = TargetBackend.CSharp;

        public string GetCodeGenString()
        {
            return stringBuilder.ToString();
        }

        public override void VisitErrorNode(IErrorNode node)
        {
            int line = node.Symbol.Line;
            int column = node.Symbol.Column;
            Console.Error.WriteLine($"Error at line {line}, column {column}: {node.GetText()}");
        }

        public override void EnterProgram([NotNull] ProgramContext context)
        {
            var children = new List<ASTNode>();
            AST = new ProgramNode("program", children, context);
            nodeStack.Push(AST);
            
            CodeGen.GenerateCodeForOpenContext(context, AST, stringBuilder, backend);
        }

        public override void ExitProgram([NotNull] ProgramContext context)
        {
            nodeStack.Pop();
            
            CodeGen.GenerateCodeForClosedContext(context, AST, stringBuilder, backend);
        }
        
        public override void EnterImport_list([NotNull] Import_listContext context)
        {
            var importListNode = new ImportListNode("import_list", new List<ASTNode>(), context);
            nodeStack.Peek().Children.Add(importListNode);
            nodeStack.Push(importListNode);
            
            CodeGen.GenerateCodeForOpenContext(context, nodeStack.Peek(), stringBuilder, backend);
        }

        public override void ExitImport_list([NotNull] Import_listContext context)
        {
            var node = nodeStack.Pop();
            
            CodeGen.GenerateCodeForClosedContext(context, node, stringBuilder, backend);
        }

        public override void EnterImport_statement([NotNull] Import_statementContext context)
        {
            var importStatementNode = new ImportStatementNode("import_statement", new List<ASTNode>(), context);
            nodeStack.Peek().Children.Add(importStatementNode);
            nodeStack.Push(importStatementNode);
    
            CodeGen.GenerateCodeForOpenContext(context, nodeStack.Peek(), stringBuilder, backend);
        }

        public override void ExitImport_statement([NotNull] Import_statementContext context)
        {
            var node = nodeStack.Pop();
            
            CodeGen.GenerateCodeForClosedContext(context, node, stringBuilder, backend);
        }

        public override void EnterModule_declaration([NotNull] Module_declarationContext context)
        {
            var children = new List<ASTNode>();
            var moduleDeclarationNode = new ModuleDeclarationNode("module_declaration", children, context);
            nodeStack.Peek().Children.Add(moduleDeclarationNode);
            nodeStack.Push(moduleDeclarationNode);

            CodeGen.GenerateCodeForOpenContext(context, nodeStack.Peek(), stringBuilder, backend);
        }

        public override void ExitModule_declaration([NotNull] Module_declarationContext context)
        {
            var node = nodeStack.Pop();

            CodeGen.GenerateCodeForClosedContext(context, node, stringBuilder, backend);
        }
        
        public override void EnterConstant_declaration(Constant_declarationContext context)
        {
            var constantDeclarationNode =
                new ConstantDeclarationNode("constant_declaration", new List<ASTNode>(), context);
            nodeStack.Peek().Children.Add(constantDeclarationNode);
            nodeStack.Push(constantDeclarationNode);
            
            CodeGen.GenerateCodeForOpenContext(context, nodeStack.Peek(), stringBuilder, backend);
        }

        public override void ExitConstant_declaration(Constant_declarationContext context)
        {
            var node = nodeStack.Pop();
            
            CodeGen.GenerateCodeForClosedContext(context, node, stringBuilder, backend);
        }

        public override void EnterVariable_declaration([NotNull] Variable_declarationContext context)
        {
            var children = new List<ASTNode>();
            var variableDeclarationNode = new VariableDeclarationNode("variable_declaration", children, context);
            nodeStack.Peek().Children.Add(variableDeclarationNode);
            nodeStack.Push(variableDeclarationNode);
            
            CodeGen.GenerateCodeForOpenContext(context, nodeStack.Peek(), stringBuilder, backend);
        }

        public override void ExitVariable_declaration([NotNull] Variable_declarationContext context)
        {
            var node = nodeStack.Pop();
            
            CodeGen.GenerateCodeForClosedContext(context, node, stringBuilder, backend);
        }

        public override void EnterVariable_value([NotNull] Variable_valueContext context)
        {
            var children = new List<ASTNode>();
            var variableValueNode = new VariableValueNode(context.GetText(), children, context);
            nodeStack.Peek().Children.Add(variableValueNode);
            nodeStack.Push(variableValueNode);
            
            CodeGen.GenerateCodeForOpenContext(context, nodeStack.Peek(), stringBuilder, backend);
        }

        public override void ExitVariable_value([NotNull] Variable_valueContext context)
        {
            var node = nodeStack.Pop();
            
            CodeGen.GenerateCodeForClosedContext(context, node, stringBuilder, backend);
        }

        public override void EnterRange_clause([NotNull] Range_clauseContext context)
        {
            var children = new List<ASTNode>();
            var rangeClauseNode = new RangeClauseNode("range_clause", children, context);
            nodeStack.Peek().Children.Add(rangeClauseNode);
            nodeStack.Push(rangeClauseNode);
            
            CodeGen.GenerateCodeForOpenContext(context, nodeStack.Peek(), stringBuilder, backend);
        }

        public override void ExitRange_clause([NotNull] Range_clauseContext context)
        {
            var node = nodeStack.Pop();
            
            CodeGen.GenerateCodeForClosedContext(context, node, stringBuilder, backend);
        }

        public override void EnterUnary_operation([NotNull] Unary_operationContext context)
        {
            var children = new List<ExpressionNode>();
            var unaryOperationNode = new UnaryOperationNode("unary_operation", children, context);
            nodeStack.Peek().Children.Add(unaryOperationNode);
            nodeStack.Push(unaryOperationNode);
            
            CodeGen.GenerateCodeForOpenContext(context, nodeStack.Peek(), stringBuilder, backend);
        }

        public override void ExitUnary_operation([NotNull] Unary_operationContext context)
        {
            var node = nodeStack.Pop();
            
            CodeGen.GenerateCodeForClosedContext(context, node, stringBuilder, backend);
        }

        public override void EnterStatement_block(Statement_blockContext context)
        {
            var blockStatementNode = new BlockStatementNode("statement_block", new List<ASTNode>(), context);
            nodeStack.Peek().Children.Add(blockStatementNode);
            nodeStack.Push(blockStatementNode);
            
            CodeGen.GenerateCodeForOpenContext(context, nodeStack.Peek(), stringBuilder, backend);
        }

        public override void ExitStatement_block(Statement_blockContext context)
        {
            var node = nodeStack.Pop();
            
            CodeGen.GenerateCodeForClosedContext(context, node, stringBuilder, backend);
        }
        
        public override void EnterFunction_call_statement([NotNull] Function_call_statementContext context)
        {
            var children = new List<ASTNode>();
            var functionCallStatementNode = new FunctionCallStatementNode("function_call_statement", children, context);
            nodeStack.Peek().Children.Add(functionCallStatementNode);
            nodeStack.Push(functionCallStatementNode);
            
            CodeGen.GenerateCodeForOpenContext(context, nodeStack.Peek(), stringBuilder, backend);
        }

        public override void ExitFunction_call_statement([NotNull] Function_call_statementContext context)
        {
            var node = nodeStack.Pop();
            
            CodeGen.GenerateCodeForClosedContext(context, node, stringBuilder, backend);
        }

        public override void EnterFunction_call_expression([NotNull] Function_call_expressionContext context)
        {
            var children = new List<ExpressionNode>();
            var functionCallExpressionNode =
                new FunctionCallExpressionNode("function_call_expression", children, context);
            nodeStack.Peek().Children.Add(functionCallExpressionNode);
            nodeStack.Push(functionCallExpressionNode);
            
            CodeGen.GenerateCodeForOpenContext(context, nodeStack.Peek(), stringBuilder, backend);
        }

        public override void ExitFunction_call_expression([NotNull] Function_call_expressionContext context)
        {
            var node = nodeStack.Pop();
            
            CodeGen.GenerateCodeForClosedContext(context, node, stringBuilder, backend);
        }

        public override void EnterElement_access_expression([NotNull] Element_access_expressionContext context)
        {
            var children = new List<ExpressionNode>();
            var functionCallExpressionNode =
                new ElementAccessExpressionNode("element_access_expression", children, context);
            nodeStack.Peek().Children.Add(functionCallExpressionNode);
            nodeStack.Push(functionCallExpressionNode);
            
            CodeGen.GenerateCodeForOpenContext(context, nodeStack.Peek(), stringBuilder, backend);
        }

        public override void ExitElement_access_expression([NotNull] Element_access_expressionContext context)
        {
            var node = nodeStack.Pop();
            
            CodeGen.GenerateCodeForClosedContext(context, node, stringBuilder, backend);
        }
        
        public override void EnterFunction_declaration(Function_declarationContext context)
        {
            var children = new List<ASTNode>();
            var functionDeclarationNode = new FunctionDeclarationNode("function_declaration", children, context);
            nodeStack.Peek().Children.Add(functionDeclarationNode);
            nodeStack.Push(functionDeclarationNode);

            CodeGen.GenerateCodeForOpenContext(context, nodeStack.Peek(), stringBuilder, backend);
        }
        
        public override void ExitFunction_declaration(Function_declarationContext context)
        {
            var node = nodeStack.Pop();
            CodeGen.GenerateCodeForClosedContext(context, node, stringBuilder, backend);
        }
        
        public override void EnterParameter_list(Parameter_listContext context)
        {
            var children = new List<ASTNode>();
            var parameterListNode = new ParameterListNode("parameter_list", children, context);
            nodeStack.Peek().Children.Add(parameterListNode);
            nodeStack.Push(parameterListNode);
            
            CodeGen.GenerateCodeForOpenContext(context, nodeStack.Peek(), stringBuilder, backend);
        }

        public override void ExitParameter_list(Parameter_listContext context)
        {
            var node = nodeStack.Pop();
            
            CodeGen.GenerateCodeForClosedContext(context, node, stringBuilder, backend);
        }

        public override void EnterParameter(ParameterContext context)
        {
            var typeNode = new TypeNode("type", new List<ASTNode>(), context.type());
            var identifierNode = new IdentifierNode("identifier", new List<ExpressionNode>(), context.identifier());
    
            var children = new List<ASTNode> { typeNode, identifierNode };
            var parameterNode = new ParameterNode("parameter", children, context);
    
            nodeStack.Peek().Children.Add(parameterNode);
            nodeStack.Push(parameterNode);
            
            CodeGen.GenerateCodeForOpenContext(context, nodeStack.Peek(), stringBuilder, backend);
        }

        public override void ExitParameter(ParameterContext context)
        {
            var node = nodeStack.Pop();
            
            CodeGen.GenerateCodeForClosedContext(context, node, stringBuilder, backend);
        }

        public override void EnterFor_statement(For_statementContext context)
        {
            var forStatementNode = new ForStatementNode("for_statement", new List<ASTNode>(), context);
            nodeStack.Peek().Children.Add(forStatementNode);
            nodeStack.Push(forStatementNode);
            
            CodeGen.GenerateCodeForOpenContext(context, nodeStack.Peek(), stringBuilder, backend);
        }

        public override void ExitFor_statement(For_statementContext context)
        {
            var node = nodeStack.Pop();
            
            CodeGen.GenerateCodeForClosedContext(context, node, stringBuilder, backend);
        }

        public override void EnterIf_statement(If_statementContext context)
        {
            var ifStatementNode = new IfStatementNode("if_statement", new List<ASTNode>(), context);
            nodeStack.Peek().Children.Add(ifStatementNode);
            nodeStack.Push(ifStatementNode);
            
            CodeGen.GenerateCodeForOpenContext(context, nodeStack.Peek(), stringBuilder, backend);
        }

        public override void ExitIf_statement(If_statementContext context)
        {
            var node = nodeStack.Pop();
            
            CodeGen.GenerateCodeForClosedContext(context, node, stringBuilder, backend);
        }

        public override void EnterWhile_statement(While_statementContext context)
        {
            var whileStatementNode = new WhileStatementNode("while_statement", new List<ASTNode>(), context);
            nodeStack.Peek().Children.Add(whileStatementNode);
            nodeStack.Push(whileStatementNode);
            
            CodeGen.GenerateCodeForOpenContext(context, nodeStack.Peek(), stringBuilder, backend);
        }

        public override void ExitWhile_statement(While_statementContext context)
        {
            var node = nodeStack.Pop();
            
            CodeGen.GenerateCodeForClosedContext(context, node, stringBuilder, backend);
        }
        
        public override void EnterAssignment_statement(Assignment_statementContext context)
        {
            var assignmentStatementNode = new AssignmentStatementNode("assignment_statement", new List<ASTNode>(), context);
            nodeStack.Peek().Children.Add(assignmentStatementNode);
            nodeStack.Push(assignmentStatementNode);
            
            CodeGen.GenerateCodeForOpenContext(context, nodeStack.Peek(), stringBuilder, backend);
        }

        public override void ExitAssignment_statement(Assignment_statementContext context)
        {
            var node = nodeStack.Pop();
            
            CodeGen.GenerateCodeForClosedContext(context, node, stringBuilder, backend);
        }

        public override void EnterElement_assignment_statement(Element_assignment_statementContext context)
        {
            var assignmentStatementNode = new ElementAssignmentStatementNode("element_assignment_statement", new List<ASTNode>(), context);
            nodeStack.Peek().Children.Add(assignmentStatementNode);
            nodeStack.Push(assignmentStatementNode);
            
            CodeGen.GenerateCodeForOpenContext(context, nodeStack.Peek(), stringBuilder, backend);
        }
        
        public override void ExitElement_assignment_statement(Element_assignment_statementContext context)
        {
            var node = nodeStack.Pop();
            
            CodeGen.GenerateCodeForClosedContext(context, node, stringBuilder, backend);
        }
        
        public override void EnterReturn_statement([NotNull] Return_statementContext context)
        {
            var children = new List<ASTNode>();
            var returnStatementNode = new ReturnStatementNode("return_statement", children, context);
            nodeStack.Peek().Children.Add(returnStatementNode);
            nodeStack.Push(returnStatementNode);
            
            CodeGen.GenerateCodeForOpenContext(context, nodeStack.Peek(), stringBuilder, backend);
        }

        public override void ExitReturn_statement([NotNull] Return_statementContext context)
        {
            nodeStack.Pop();
        }

        public override void EnterLogical_or(Logical_orContext context)
        {
            if (context.ChildCount > 1)
            {
                var logicalOrNode = new LogicalOrNode("logical_or", new List<ExpressionNode>(), context);
                nodeStack.Peek().Children.Add(logicalOrNode);
                nodeStack.Push(logicalOrNode);
                
                CodeGen.GenerateCodeForOpenContext(context, nodeStack.Peek(), stringBuilder, backend);
            }
        }

        public override void ExitLogical_or(Logical_orContext context)
        {
            // Only pop the node stack if the logical_or node was actually added
            if (context.ChildCount > 1)
            {
                var node = nodeStack.Pop();
                
                CodeGen.GenerateCodeForClosedContext(context, node, stringBuilder, backend);
            }
        }

        public override void EnterLogical_and(Logical_andContext context)
        {
            if (context.ChildCount > 1)
            {
                var logicalOrNode = new LogicalAndNode("logical_or", new List<ExpressionNode>(), context);
                nodeStack.Peek().Children.Add(logicalOrNode);
                nodeStack.Push(logicalOrNode);
                
                CodeGen.GenerateCodeForOpenContext(context, nodeStack.Peek(), stringBuilder, backend);
            }
        }

        public override void ExitLogical_and(Logical_andContext context)
        {
            // Only pop the node stack if the logical_or node was actually added
            if (context.ChildCount > 1)
            {
                var node = nodeStack.Pop();
                
                CodeGen.GenerateCodeForClosedContext(context, node, stringBuilder, backend);
            }
        }

        public override void EnterEquality(EqualityContext context)
        {
            if (context.ChildCount > 1)
            {
                var equalityNode = new EqualityNode("equality", new List<ExpressionNode>(), context);
                nodeStack.Peek().Children.Add(equalityNode);
                nodeStack.Push(equalityNode);
                
                CodeGen.GenerateCodeForOpenContext(context, nodeStack.Peek(), stringBuilder, backend);
            }
        }

        public override void ExitEquality(EqualityContext context)
        {
            if (context.ChildCount > 1)
            {
                var node = nodeStack.Pop();
                
                CodeGen.GenerateCodeForClosedContext(context, node, stringBuilder, backend);
            }
        }

        public override void EnterRelational(RelationalContext context)
        {
            if (context.ChildCount > 1)
            {
                var relationalNode = new RelationalNode("relational", new List<ExpressionNode>(), context);
                nodeStack.Peek().Children.Add(relationalNode);
                nodeStack.Push(relationalNode);
                
                CodeGen.GenerateCodeForOpenContext(context, nodeStack.Peek(), stringBuilder, backend);
            }
        }

        public override void ExitRelational(RelationalContext context)
        {
            if (context.ChildCount > 1)
            {
                var node = nodeStack.Pop();
                
                CodeGen.GenerateCodeForClosedContext(context, node, stringBuilder, backend);
            }
        }

        public override void EnterAdditive(AdditiveContext context)
        {
            if (context.ChildCount > 1)
            {
                var additiveNode = new AdditiveNode("additive", new List<ExpressionNode>(), context);
                nodeStack.Peek().Children.Add(additiveNode);
                nodeStack.Push(additiveNode);
                
                CodeGen.GenerateCodeForOpenContext(context, nodeStack.Peek(), stringBuilder, backend);
            }
        }

        public override void ExitAdditive(AdditiveContext context)
        {
            if (context.ChildCount > 1)
            {
                var node = nodeStack.Pop();
                
                CodeGen.GenerateCodeForClosedContext(context, node, stringBuilder, backend);
            }
        }

        public override void EnterMultiplicative(MultiplicativeContext context)
        {
            if (context.ChildCount > 1)
            {
                var multiplicativeNode = new MultiplicativeNode("multiplicative", new List<ExpressionNode>(), context);
                nodeStack.Peek().Children.Add(multiplicativeNode);
                nodeStack.Push(multiplicativeNode);
                
                CodeGen.GenerateCodeForOpenContext(context, nodeStack.Peek(), stringBuilder, backend);
            }
        }

        public override void ExitMultiplicative(MultiplicativeContext context)
        {
            if (context.ChildCount > 1)
            {
                var node = nodeStack.Pop();
                
                CodeGen.GenerateCodeForClosedContext(context, node, stringBuilder, backend);
            }
        }

        public override void EnterExpression_value(Expression_valueContext context)
        {
            var expressionValueNode = new ExpressionValueNode("expression_value", new List<ExpressionNode>(), context);
            nodeStack.Peek().Children.Add(expressionValueNode);
            nodeStack.Push(expressionValueNode);
            
            CodeGen.GenerateCodeForOpenContext(context, nodeStack.Peek(), stringBuilder, backend);
        }

        public override void ExitExpression_value(Expression_valueContext context)
        {
            var node = nodeStack.Pop();
            
            CodeGen.GenerateCodeForClosedContext(context, node, stringBuilder, backend);
        }

        public override void EnterIdentifier(IdentifierContext context)
        {
            var identifierNode = new IdentifierNode("identifier", new List<ExpressionNode>(), context);
            nodeStack.Peek().Children.Add(identifierNode);
            nodeStack.Push(identifierNode);
            
            CodeGen.GenerateCodeForOpenContext(context, nodeStack.Peek(), stringBuilder, backend);
        }

        public override void ExitIdentifier(IdentifierContext context)
        {
            var node = nodeStack.Pop();
            
            CodeGen.GenerateCodeForClosedContext(context, node, stringBuilder, backend);
        }

        public override void EnterLiteral(LiteralContext context)
        {
            var literalNode = new LiteralNode("literal", new List<ExpressionNode>(), context);
            nodeStack.Peek().Children.Add(literalNode);
            nodeStack.Push(literalNode);
            
            CodeGen.GenerateCodeForOpenContext(context, nodeStack.Peek(), stringBuilder, backend);
        }

        public override void ExitLiteral(LiteralContext context)
        {
            var node = nodeStack.Pop();
            
            CodeGen.GenerateCodeForClosedContext(context, node, stringBuilder, backend);
        }
        
        public override void EnterPrint_statement(Print_statementContext context)
        {
            var printStatementNode = new PrintStatementNode("print_statement", new List<ASTNode>(), context);
            nodeStack.Peek().Children.Add(printStatementNode);
            nodeStack.Push(printStatementNode);
            
            CodeGen.GenerateCodeForOpenContext(context, nodeStack.Peek(), stringBuilder, backend);
        }

        public override void ExitPrint_statement(Print_statementContext context)
        {
            var node = nodeStack.Pop();
            
            CodeGen.GenerateCodeForClosedContext(context, node, stringBuilder, backend);
        }
        
        //TODO implement Parenthetical precedence 
    }
}