using System;
using System.Collections.Generic;
using TKM.BoolFunction;
using TKM.Core;

namespace TKM_BFA
{
    class Manager
    {
        static void Main(string[] args)
        {
            TKM.IO.IIO io;
            do
            {
                Console.WriteLine("Press 1 to console input/output mode");
                Console.WriteLine("Press 0 to exit");
                ConsoleKeyInfo kc = Console.ReadKey(true);
                switch (kc.KeyChar)
                {
                    case '0':
                        return;
                    default:
                        io = new TKM.IO.IOConsole();
                        break;
                }
                do
                {
                    Function f = new Function(io.Get());
                    Analyzer ar = new Analyzer(f);
                    io.Set(
                        "Truth table\n\n"
                        + MyConvert.StringArrayToString(f.Names) + "f\n"
                        + MyConvert.BoolArrayToString(f.Table) + "\n"
                        + "Pascal's triangle\n"
                        + MyConvert.BoolArrayToString(ar.Triangle) + "\n"
                        + "Gegalkin Polynom\n"
                        + MyConvert.StringArrayToString(ar.GegalkinPolynome, "+") + "\n"
                        );
                    io.Set("Derivatives weights");
                    for (int arg = 0; arg < f.Names.Length; arg++)
                    {
                        io.Set(
                            "\n"
                            + "|" + f.Names[arg] + "| = " + ar.DerivativesWeights[arg]
                            );
                    }
                    io.Set(
                        "Variables priority\n"
                        + MyConvert.StringArrayToString(ar.VarStek) + "\n"
                        + "Values at nodes(in contact circuit)\n"
                        + MyConvert.StringArrayToString(ar.Shannon.ValueInNodes, "\n", true) + "\n"
                        + "Adjacency matrix\n"
                        + MyConvert.StringArrayToString(ar.Shannon.AMatrix, "  ") + "\n"
                        );
                    Console.WriteLine("Press Y to main menu/N to continue");
                    kc = Console.ReadKey(true);
                    if (kc.KeyChar == 'y' || kc.KeyChar == 'Y')
                    {
                        break;
                    }
                } while (true);
                Console.WriteLine("Press Y to exit/N to continue");
                kc = Console.ReadKey(true);
                if (kc.KeyChar == 'y' || kc.KeyChar == 'Y')
                {
                    break;
                }
            } while (true);
        }
    }
}
