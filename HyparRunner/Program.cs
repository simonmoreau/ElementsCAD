using System;

namespace HyparRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = args[0];
            FunctionRunner functionRunner = new FunctionRunner();
            functionRunner.RunFunction(args[0], "EnvelopeByCenterline");

        }
    }
}
