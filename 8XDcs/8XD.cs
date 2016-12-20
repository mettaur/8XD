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
                Console.WriteLine("Usage: 8XD <file> <dest> -c");
                return;
            }

            string path = args[0];

            if (!File.Exists(path))
            {
                Console.WriteLine("The 8XP program path supplied does not exist.");
                return;
            }

            Console.WriteLine("8XD Version 1.0");
            Console.WriteLine("(c) Lambda 2016");

            IEnumerable<byte> buffer = Detokenizer.Open(path);
            if (buffer == null)
            {
                return;
            }

            if(args.Length < 2)
            {
                Detokenizer.OpenDest();
            } else
            {
                Detokenizer.OpenDest(args[1]);
            }
            
            Detokenizer.Detokenize(buffer.Skip(74).ToArray());
        }
    }
}
