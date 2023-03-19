using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Antlr4.Runtime;
using static FlowParser;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Flow
{
    public enum TargetBackend { CSharp }
    
    public static class CodeGen
    {
        private static Queue<string> queuedExpressions = new Queue<string>();
        public static void GenerateCodeForOpenContext<T>(T context, ASTNode node, StringBuilder sb, TargetBackend codeTargetBackend)
            where T: ParserRuleContext
        {
            switch (codeTargetBackend)
            {
                case TargetBackend.CSharp:
                    GenerateCSharpForOpenContext(context, node, sb);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(codeTargetBackend), codeTargetBackend, null);
            }
        }
        
        public static void GenerateCodeForClosedContext<T>(T context, ASTNode node, StringBuilder sb, TargetBackend codeTargetBackend)
        {
            switch (codeTargetBackend)
            {
                case TargetBackend.CSharp:
                    GenerateCSharpForClosedContext(context, node, sb);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(codeTargetBackend), codeTargetBackend, null);
            }
        }

        private static void GenerateCSharpForOpenContext<T>(T context, ASTNode node, StringBuilder sb)
            where T : ParserRuleContext
        {
            if (context == null) return;

            if (!node.HasParent<Module_declarationContext>())
            {
                bool expected =    context is ProgramContext
                                || context is Import_statementContext
                                || context is Import_listContext
                                || context is Module_declarationContext
                                || context is IdentifierContext;
                if (!expected)
                {
                    throw new SyntaxErrorException($"Invalid syntax: {context.GetText()} must be declared within a module.");
                }
            }

            switch (context)
            {
                case ProgramContext program:
                    sb.AppendLine($"// This file is auto-generated from Flow module {program.module_declaration().identifier().GetText()}");
                    sb.AppendLine("using System;");
                    sb.AppendLine("using System.Collections.Generic;");
                    break;
                
                case Import_statementContext import:
                    sb.AppendLine($"using {import.identifier().GetText()};");
                    break;
                
                case Module_declarationContext module:
                    sb.AppendLine("namespace FlowGenerated");
                    sb.AppendLine("{");
                    sb.AppendLine($"public static class {module.identifier().GetText()}");
                    break;
                                
                case Print_statementContext print:
                    sb.AppendLine($"Console.WriteLine({print.expression().GetText()})");
                    break;
                
                case Constant_declarationContext constant:
                    string q = "";
                    if (node.HasParent<Statement_blockContext>() && !node.HasParent<Function_declarationContext>())
                        q = " public static";
                    sb.Append($"{q} const {GetTypeTextCSharp(constant.type())} {constant.identifier().GetText()} = " +
                              $"{GetValueTextCsharp(constant)}");
                    break;
                
                case Variable_declarationContext variable:
                    string p = "";
                    if (node.HasParent<Statement_blockContext>() && !node.HasParent<Function_declarationContext>())
                        p = " public static";
                    sb.Append($"{p} {GetTypeTextCSharp(variable.type())} {variable.identifier().GetText()} = " +
                              $"{GetValueTextCsharp(variable)}");
                    break;
                
                case Statement_blockContext statement:
                    sb.AppendLine("{");
                    if (queuedExpressions.Count > 0)
                    {
                        string whereExpression = queuedExpressions.Dequeue();
                        sb.AppendLine($"if (!({whereExpression})) continue;");
                    }
                    break;
                
                case Function_declarationContext functionDecl:
                    var typeString = functionDecl.type() != null ? functionDecl.type()?.GetText() : "void";
                    sb.AppendLine($"public {typeString} {functionDecl.identifier().GetText()}");
                    if (functionDecl.parameter_list() == null) sb.Append("()");
                    break;
                
                case Parameter_listContext parameterList:
                    string parameterText = "";
                    foreach (var paramCtx in parameterList.parameter())
                    {
                        parameterText += $"{GetTypeTextCSharp(paramCtx.type())} {paramCtx.identifier().GetText()},";
                    }
                    parameterText = parameterText.TrimEnd(',', ' ');
                    sb.Append($"({parameterText})");
                    break;
                
                case For_statementContext forStatement:
                    string rangeStart = forStatement.range_clause().expression(0).GetText();
                    string rangeEnd = forStatement.range_clause().expression(1).GetText();
                    string loopVariable = forStatement.identifier().GetText();
                    if (forStatement.expression() != null)
                    {
                        queuedExpressions.Enqueue(forStatement.expression().GetText());
                    }
                    
                    sb.AppendLine(
                        $"for (int {loopVariable} = {rangeStart}; {loopVariable} <= {rangeEnd}; {loopVariable}++)");
                    break;

                case If_statementContext ifStatement:
                    sb.Append($"if ({BuildCSharpExpression(ifStatement.expression())})");
                    break;
            }
        }

        private static string BuildCSharpExpression(ExpressionContext expressionContext)
        {
            var root = expressionContext.logical_or();
            return BuildCSharpLogicalOr(root);

            string BuildCSharpLogicalOr(Logical_orContext logicalOr)
            {
                var logicalAnds = logicalOr.logical_and();
                var logicalAndStrings = logicalAnds.Select(BuildCSharpLogicalAnd);
                return string.Join(" || ", logicalAndStrings);
            }

            string BuildCSharpLogicalAnd(Logical_andContext logicalAnd)
            {
                var equalities = logicalAnd.equality();
                var equalityStrings = equalities.Select(BuildCSharpEquality);
                return string.Join(" && ", equalityStrings);
            }

            string BuildCSharpEquality(EqualityContext equality)
            {
                var relationals = equality.relational();
                var relationalStrings = relationals.Select(BuildCSharpRelational);

                List<string> operators = new List<string>();
                foreach (var eq in equality.EQ()) operators.Add(eq.GetText());
                foreach (var neq in equality.NEQ()) operators.Add(neq.GetText());

                if (operators.Count + 1 != relationals.Length)
                {
                    throw new InvalidOperationException("Mismatch between the number of relationals and operators in the equality context");
                }

                var zipped = Enumerable.Zip(relationalStrings, operators.Concat(new[] { "" }), (r, op) => $"{r} {op}");
                return string.Join(" ", zipped).Replace("is","==");
            }

            string BuildCSharpRelational(RelationalContext relational)
            {
                var additives = relational.additive();
                var additiveStrings = additives.Select(BuildCSharpAdditive);

                List<string> operators = new List<string>();
                foreach (var lt in relational.LT()) operators.Add(lt.GetText());
                foreach (var lte in relational.LTE()) operators.Add(lte.GetText());
                foreach (var gt in relational.GT()) operators.Add(gt.GetText());
                foreach (var gte in relational.GTE()) operators.Add(gte.GetText());

                if (operators.Count + 1 != additives.Length)
                {
                    throw new InvalidOperationException("Mismatch between the number of additives and operators in the relational context");
                }

                var zipped = Enumerable.Zip(additiveStrings, operators.Concat(new[] { "" }), (a, op) => $"{a} {op}");
                return string.Join(" ", zipped);
            }
            
            string BuildCSharpAdditive(AdditiveContext additive)
            {
                var multiplicatives = additive.multiplicative();
                var multiplicativeStrings = multiplicatives.Select(BuildCSharpMultiplicative);

                List<string> operators = new List<string>();
                foreach (var add in additive.ADD()) operators.Add(add.GetText());
                foreach (var sub in additive.SUB()) operators.Add(sub.GetText());

                if (operators.Count + 1 != multiplicatives.Length)
                {
                    throw new InvalidOperationException("Mismatch between the number of multiplicatives and operators in the additive context");
                }

                var zipped = Enumerable.Zip(multiplicativeStrings, operators.Concat(new[] { "" }), (m, op) => $"{m} {op}");
                return string.Join(" ", zipped);
            }

            string BuildCSharpMultiplicative(MultiplicativeContext multiplicative)
            {
                var expressionValues = multiplicative.expression_value();
                var expressionValueStrings = expressionValues.Select(BuildCSharpExpressionValue);

                List<string> operators = new List<string>();
                foreach (var mul in multiplicative.MUL()) operators.Add(mul.GetText());
                foreach (var div in multiplicative.DIV()) operators.Add(div.GetText());
                foreach (var mod in multiplicative.MOD()) operators.Add(mod.GetText());

                if (operators.Count + 1 != expressionValues.Length)
                {
                    throw new InvalidOperationException("Mismatch between the number of expression values and operators in the multiplicative context");
                }

                var zipped = Enumerable.Zip(expressionValueStrings, operators.Concat(new[] { "" }), (ev, op) => $"{ev} {op}");
                return string.Join(" ", zipped);
            }
            
            string BuildCSharpExpressionValue(Expression_valueContext expressionValue)
            {
                if (expressionValue.literal() != null)
                {
                    return expressionValue.literal().GetText();
                }

                if (expressionValue.identifier() != null)
                {
                    return expressionValue.identifier().GetText();
                }
    
                if (expressionValue.unary_operation() != null)
                {
                    return "!";
                }

                if (expressionValue.function_call_expression() != null)
                {
                    return expressionValue.function_call_expression().GetText();
                }

                if (expressionValue.element_access_expression() != null)
                {
                    return expressionValue.element_access_expression().GetText();
                }

                if (expressionValue.expression() != null)
                {
                    return BuildCSharpExpression(expressionValue.expression());
                }

                return string.Empty;
            }
        }


        private static void GenerateCSharpForClosedContext<T>(T context, ASTNode node, StringBuilder sb)
        {
            switch (context)
            {
                case ProgramContext program:
                case Statement_blockContext block:
                    sb.AppendLine("}");
                    break;
                
                case Import_listContext imports:
                    sb.AppendLine("\n");
                    break;
                
                case Module_declarationContext module:
                    break;

                case Constant_declarationContext constant:
                case Variable_declarationContext variable:
                case Print_statementContext print:
                    sb.Append(";\n ");
                    break;

            }
        }
        
        private static string GetTypeTextCSharp(TypeContext typeContext)
        {
            if (typeContext.GetText().StartsWith("array"))
            {
                return $"{typeContext.type(0).GetText()}[]";
            }

            if (typeContext.GetText().StartsWith("map"))
            {
                return $"Dictionary<{typeContext.type(0).GetText()},{typeContext.type(1).GetText()}>";
            }

            return typeContext.GetText();
        }

        private static string GetValueTextCsharp(ParserRuleContext ruleContext)
        {
            var value = ruleContext.GetRuleContext<Variable_valueContext>(0);
            if (value != null)
            {
                var expression = value.expression();
                if (expression != null)
                {
                    return BuildCSharpExpression(expression);
                }
                
                var valueText = value.GetText();
                if (valueText.Contains("array"))
                {
                    return $"new {value.type(0).GetText()}[{value.expression().GetText()}]";
                }
                if (valueText.Contains("map"))
                {
                    return $"new Dictionary<{value.type(0).GetText()},{value.type(1).GetText()}]";
                }
                if (value.expression() != null)
                {
                    return value.expression().GetText();
                }

                return value.GetText();
            }

            return ruleContext.GetText();
        }
        
        public static string FormatCSharpCode(string code)
        {
            var tree = CSharpSyntaxTree.ParseText(code);
            var root = tree.GetRoot().NormalizeWhitespace();
            var formattedCode = root.ToFullString();

            return formattedCode;
        }
    }
}