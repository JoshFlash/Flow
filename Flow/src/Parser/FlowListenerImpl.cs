using System;
using System.Collections.Generic;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace Flow
{
    public class FlowListener : FlowBaseListener
    {
        public ASTNode AST { get; set; }
        
        private Stack<ASTNode> nodeStack = new Stack<ASTNode>();
        
        public override void EnterProgram([NotNull] FlowParser.ProgramContext context)
        {
            var children = new List<ASTNode>();
            AST = new ProgramNode("program", children);
            nodeStack.Push(AST);
        }

        public override void ExitProgram([NotNull] FlowParser.ProgramContext context)
        {
            nodeStack.Pop();
        }
        
        public override void EnterModule_declaration([NotNull] FlowParser.Module_declarationContext context)
        {
            var children = new List<ASTNode>();
            var moduleDeclarationNode = new ModuleDeclarationNode("module_declaration", children);
            nodeStack.Peek().Children.Add(moduleDeclarationNode);
            nodeStack.Push(moduleDeclarationNode);
        }

        public override void ExitModule_declaration([NotNull] FlowParser.Module_declarationContext context)
        {
            nodeStack.Pop();
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