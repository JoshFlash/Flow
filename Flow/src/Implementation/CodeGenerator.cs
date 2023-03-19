using System;
using System.Data;
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