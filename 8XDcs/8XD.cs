//////////////
//// 8XD
//// TI-8X Program Decompiler
//// (C) Lambda 2016
//////////////

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _8XDcs
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: 8XD <file> <dest>");
                return;
            }
            string path = args[0];
            Console.WriteLine("8XD Version 1.0");
            Console.WriteLine("(c) Lambda 2016");

            IEnumerable<byte> buffer = Detokenizer.Open(path);
            if (buffer == null)
            {
                Console.WriteLine("Error: Buffer did not load.");
                return;
            }

            //Console.WriteLine(Detokenizer.GetDataSection(buffer));

            Detokenizer.OpenDest();
            Detokenizer.Detokenize(buffer.Skip(74).ToArray());
        }
    }
}
