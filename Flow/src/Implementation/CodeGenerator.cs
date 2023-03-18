using System;
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
        {
            if (context == null) return;
            
            switch (context)
            {
                case ProgramContext program:
                    sb.AppendLine($"// This file is auto-generated from Flow module {program.module_declaration().identifier().GetText()}");
                    break;
                
                case Import_statementContext import:
                    sb.AppendLine($"using {import.identifier().GetText()};");
                    break;
                
                case Module_declarationContext module:
                    sb.AppendLine("namespace FlowGenerated");
                    sb.AppendLine("{");
                    sb.AppendLine($"public static class {module.identifier().GetText()}");
                    sb.AppendLine("{");
                    break;
                                
                case Print_statementContext print:
                    sb.AppendLine($"Console.WriteLine({print.expression().GetText()});");
                    break;
                
                case Constant_declarationContext constant:
                    string qualifier = "";
                    if (node.HasParent<Statement_blockContext>() && !node.HasParent<Function_declarationContext>())
                        qualifier = " public static";
                    sb.Append($"{qualifier} const int {constant.identifier().GetText()} = {constant.variable_value().GetText()}");
                    break;
            }
        }

        private static void GenerateCSharpForClosedContext<T>(T context, ASTNode node, StringBuilder sb)
        {
            switch (context)
            {
                case ProgramContext program:
                    sb.AppendLine("}");
                    break;
                
                case Import_listContext imports:
                    sb.AppendLine("\n");
                    break;
                
                case Module_declarationContext module:
                    sb.AppendLine("}");
                    break;
                
                case Constant_declarationContext constant:
                    sb.Append(";\n ");
                    break;

            }
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