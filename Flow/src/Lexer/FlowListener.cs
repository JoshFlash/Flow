//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.12.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from Flow.g4 by ANTLR 4.12.0

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="FlowParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.12.0")]
[System.CLSCompliant(false)]
public interface IFlowListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="FlowParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterProgram([NotNull] FlowParser.ProgramContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FlowParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitProgram([NotNull] FlowParser.ProgramContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FlowParser.module_declaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterModule_declaration([NotNull] FlowParser.Module_declarationContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FlowParser.module_declaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitModule_declaration([NotNull] FlowParser.Module_declarationContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FlowParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStatement([NotNull] FlowParser.StatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FlowParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStatement([NotNull] FlowParser.StatementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FlowParser.variable_declaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterVariable_declaration([NotNull] FlowParser.Variable_declarationContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FlowParser.variable_declaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitVariable_declaration([NotNull] FlowParser.Variable_declarationContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FlowParser.variable_value"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterVariable_value([NotNull] FlowParser.Variable_valueContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FlowParser.variable_value"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitVariable_value([NotNull] FlowParser.Variable_valueContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FlowParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterType([NotNull] FlowParser.TypeContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FlowParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitType([NotNull] FlowParser.TypeContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FlowParser.generic_type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterGeneric_type([NotNull] FlowParser.Generic_typeContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FlowParser.generic_type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitGeneric_type([NotNull] FlowParser.Generic_typeContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FlowParser.type_parameter_list"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterType_parameter_list([NotNull] FlowParser.Type_parameter_listContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FlowParser.type_parameter_list"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitType_parameter_list([NotNull] FlowParser.Type_parameter_listContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FlowParser.type_parameter"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterType_parameter([NotNull] FlowParser.Type_parameterContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FlowParser.type_parameter"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitType_parameter([NotNull] FlowParser.Type_parameterContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FlowParser.assignment_statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAssignment_statement([NotNull] FlowParser.Assignment_statementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FlowParser.assignment_statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAssignment_statement([NotNull] FlowParser.Assignment_statementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FlowParser.print_statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPrint_statement([NotNull] FlowParser.Print_statementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FlowParser.print_statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPrint_statement([NotNull] FlowParser.Print_statementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FlowParser.if_statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIf_statement([NotNull] FlowParser.If_statementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FlowParser.if_statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIf_statement([NotNull] FlowParser.If_statementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FlowParser.while_statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterWhile_statement([NotNull] FlowParser.While_statementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FlowParser.while_statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitWhile_statement([NotNull] FlowParser.While_statementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FlowParser.for_statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFor_statement([NotNull] FlowParser.For_statementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FlowParser.for_statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFor_statement([NotNull] FlowParser.For_statementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FlowParser.range_clause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterRange_clause([NotNull] FlowParser.Range_clauseContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FlowParser.range_clause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitRange_clause([NotNull] FlowParser.Range_clauseContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FlowParser.for_expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFor_expression([NotNull] FlowParser.For_expressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FlowParser.for_expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFor_expression([NotNull] FlowParser.For_expressionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FlowParser.return_statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterReturn_statement([NotNull] FlowParser.Return_statementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FlowParser.return_statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitReturn_statement([NotNull] FlowParser.Return_statementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FlowParser.function_call_statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFunction_call_statement([NotNull] FlowParser.Function_call_statementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FlowParser.function_call_statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFunction_call_statement([NotNull] FlowParser.Function_call_statementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FlowParser.argument_list"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterArgument_list([NotNull] FlowParser.Argument_listContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FlowParser.argument_list"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitArgument_list([NotNull] FlowParser.Argument_listContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FlowParser.statement_block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStatement_block([NotNull] FlowParser.Statement_blockContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FlowParser.statement_block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStatement_block([NotNull] FlowParser.Statement_blockContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FlowParser.unary_operation"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterUnary_operation([NotNull] FlowParser.Unary_operationContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FlowParser.unary_operation"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitUnary_operation([NotNull] FlowParser.Unary_operationContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FlowParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression([NotNull] FlowParser.ExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FlowParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression([NotNull] FlowParser.ExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FlowParser.logical_or"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLogical_or([NotNull] FlowParser.Logical_orContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FlowParser.logical_or"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLogical_or([NotNull] FlowParser.Logical_orContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FlowParser.logical_and"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLogical_and([NotNull] FlowParser.Logical_andContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FlowParser.logical_and"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLogical_and([NotNull] FlowParser.Logical_andContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FlowParser.equality"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterEquality([NotNull] FlowParser.EqualityContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FlowParser.equality"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitEquality([NotNull] FlowParser.EqualityContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FlowParser.relational"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterRelational([NotNull] FlowParser.RelationalContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FlowParser.relational"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitRelational([NotNull] FlowParser.RelationalContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FlowParser.additive"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAdditive([NotNull] FlowParser.AdditiveContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FlowParser.additive"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAdditive([NotNull] FlowParser.AdditiveContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FlowParser.multiplicative"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMultiplicative([NotNull] FlowParser.MultiplicativeContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FlowParser.multiplicative"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMultiplicative([NotNull] FlowParser.MultiplicativeContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FlowParser.expression_value"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression_value([NotNull] FlowParser.Expression_valueContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FlowParser.expression_value"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression_value([NotNull] FlowParser.Expression_valueContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FlowParser.function_declaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFunction_declaration([NotNull] FlowParser.Function_declarationContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FlowParser.function_declaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFunction_declaration([NotNull] FlowParser.Function_declarationContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FlowParser.parameter_list"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterParameter_list([NotNull] FlowParser.Parameter_listContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FlowParser.parameter_list"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitParameter_list([NotNull] FlowParser.Parameter_listContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FlowParser.parameter"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterParameter([NotNull] FlowParser.ParameterContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FlowParser.parameter"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitParameter([NotNull] FlowParser.ParameterContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FlowParser.function_call_expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFunction_call_expression([NotNull] FlowParser.Function_call_expressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FlowParser.function_call_expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFunction_call_expression([NotNull] FlowParser.Function_call_expressionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FlowParser.literal"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLiteral([NotNull] FlowParser.LiteralContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FlowParser.literal"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLiteral([NotNull] FlowParser.LiteralContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FlowParser.identifier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIdentifier([NotNull] FlowParser.IdentifierContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FlowParser.identifier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIdentifier([NotNull] FlowParser.IdentifierContext context);
}
