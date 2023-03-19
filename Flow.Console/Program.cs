using System;

namespace FlowTranspiler
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please provide a path to the Flow file as an argument.");
                return;
            }

            string flowFilePath = args[0];

            CSharpCodeGen.TranspileFlow(flowFilePath);
        }
    }
}