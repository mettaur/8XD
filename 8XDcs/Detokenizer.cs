using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _8XDcs
{
    class Detokenizer
    {
        private static string path = "";
        private static int size = 0;
        public static Dictionary<UInt32, string> Tokens = new Dictionary<UInt32, string>()
        {
            {0x00 , ""},
            {0x01 , "►DMS"},
            {0x02 , "►Dec"},
            {0x03 , "►Frac"},
            {0x04 , "→"},
            {0x05 , "Boxplot"},
            {0x06 , "["},
            {0x07 , "]"},
            {0x08 , "{"},
            {0x09 , "}"},
            {0x0A , "ʳ"},
            {0x0B , "°"},
            {0x0C , "ˉ¹"},
            {0x0D , "²"},
            {0x0E , "ᵀ"},
            {0x0F , "³"},
            {0x10 , "("},
            {0x11 , ")"},
            {0x12 , "round("},
            {0x13 , "pxl-Test("},
            {0x14 , "augment("},
            {0x15 , "rowSwap("},
            {0x16 , "row+("},
            {0x17 , "*row("},
            {0x18 , "*row+("},
            {0x19 , "max("},
            {0x1A , "min("},
            {0x1B , "R►Pr("},
            {0x1C , "R►Pθ("},
            {0x1D , "P►Rx("},
            {0x1E , "P►Ry("},
            {0x1F , "median("},
            {0x20 , "randM("},
            {0x21 , "mean("},
            {0x22 , "solve("},
            {0x23 , "seq("},
            {0x24 , "fnInt("},
            {0x25 , "nDeriv("},
            {0x27 , "fMin("},
            {0x28 , "fMax("},
            {0x29 , " "},
            {0x2A , "\""},
            {0x2B , ","},
            {0x2C , "[i]"},
            {0x2D , "!"},
            {0x2E , "CubicReg "},
            {0x2F , "QuartReg "},
            {0x30 , "0"},
            {0x31 , "1"},
            {0x32 , "2"},
            {0x33 , "3"},
            {0x34 , "4"},
            {0x35 , "5"},
            {0x36 , "6"},
            {0x37 , "7"},
            {0x38 , "8"},
            {0x39 , "9"},
            {0x3A , "."},
            {0x3B , "ᴇ"},
            {0x3C , " or "},
            {0x3D , " xor "},
            {0x3E , ":"},
            {0x3F , "\n"},
            {0x40 , " and "},
            {0x41 , "A"},
            {0x42 , "B"},
            {0x43 , "C"},
            {0x44 , "D"},
            {0x45 , "E"},
            {0x46 , "F"},
            {0x47 , "G"},
            {0x48 , "H"},
            {0x49 , "I"},
            {0x4A , "J"},
            {0x4B , "K"},
            {0x4C , "L"},
            {0x4D , "M"},
            {0x4E , "N"},
            {0x4F , "O"},
            {0x50 , "P"},
            {0x51 , "Q"},
            {0x52 , "R"},
            {0x53 , "S"},
            {0x54 , "T"},
            {0x55 , "U"},
            {0x56 , "V"},
            {0x57 , "W"},
            {0x58 , "X"},
            {0x59 , "Y"},
            {0x5A , "Z"},
            {0x5B , "θ"},
            {0x5F , "prgm"},
            {0x64 , "Radian"},
            {0x65 , "Degree"},
            {0x66 , "Normal"},
            {0x67 , "Sci"},
            {0x68 , "Eng"},
            {0x69 , "Float"},
            {0x6A , "="},
            {0x6B , "<"},
            {0x6C , ">"},
            {0x6D , "≤"},
            {0x6E , "≥"},
            {0x6F , "≠"},
            {0x70 , "+"},
            {0x71 , "-"},
            {0x72 , "Ans"},
            {0x73 , "Fix "},
            {0x74 , "Horiz"},
            {0x75 , "Full"},
            {0x76 , "Func"},
            {0x77 , "Param"},
            {0x78 , "Polar"},
            {0x79 , "Seq"},
            {0x7A , "IndpntAuto"},
            {0x7B , "IndpntAsk"},
            {0x7C , "DependAuto"},
            {0x7D , "DependAsk"},
            {0x7F , "□"},
            {0x80 , "﹢"},
            {0x81 , "·"},
            {0x82 , "*"},
            {0x83 , "/"},
            {0x84 , "Trace"},
            {0x85 , "ClrDraw"},
            {0x86 , "ZStandard"},
            {0x87 , "ZTrig"},
            {0x88 , "ZBox"},
            {0x89 , "Zoom In"},
            {0x8A , "Zoom Out"},
            {0x8B , "ZSquare"},
            {0x8C , "ZInteger"},
            {0x8D , "ZPrevious"},
            {0x8E , "ZDecimal"},
            {0x8F , "ZoomStat"},
            {0x90 , "ZoomRcl"},
            {0x91 , "PrintScreen"},
            {0x92 , "ZoomSto"},
            {0x93 , "Text("},
            {0x94 , " nPr "},
            {0x95 , " nCr "},
            {0x96 , "FnOn "},
            {0x97 , "FnOff "},
            {0x98 , "StorePic "},
            {0x99 , "RecallPic "},
            {0x9A , "StoreGDB "},
            {0x9B , "RecallGDB "},
            {0x9C , "Line("},
            {0x9D , "Vertical "},
            {0x9E , "Pt-On("},
            {0x9F , "Pt-Off("},
            {0xA0 , "Pt-Change("},
            {0xA1 , "Pxl-On("},
            {0xA2 , "Pxl-Off("},
            {0xA3 , "Pxl-Change("},
            {0xA4 , "Shade("},
            {0xA5 , "Circle("},
            {0xA6 , "Horizontal "},
            {0xA7 , "Tangent("},
            {0xA8 , "DrawInv "},
            {0xA9 , "DrawF "},
            {0xAB , "rand"},
            {0xAC , "π"},
            {0xAD , "getKey"},
            {0xAE , "'"},
            {0xAF , "?"},
            {0xB0 , "⁻"},
            {0xB1 , "int "},
            {0xB2 , "abs "},
            //{0xB3 , "det "},
            {0xB4 , "identity "},
            {0xB5 , "dim "},
            {0xB6 , "sum "},
            {0xB7 , "prod("},
            {0xB8 , "not("},
            {0xB9 , "iPart "},
            {0xBA , "fPart "},
            {0xBC , "√"},
            {0xBD , "³√"},
            {0xBE , "ln "},
            {0xBF , "e^ "},
            {0xC0 , "log "},
            {0xC1 , "₁₀^("},
            {0xC2 , "sin "},
            {0xC3 , "sin⁻¹ "},
            {0xC4 , "cos "},
            {0xC5 , "cos⁻¹ "},
            {0xC6 , "Tan "},
            {0xC7 , "tan⁻¹ "},
            {0xC8 , "sinh "},
            {0xC9 , "sinh⁻¹ "},
            {0xCA , "cosh "},
            {0xCB , "soch⁻¹ "},
            {0xCC , "tanh "},
            {0xCD , "tanh⁻¹ "},
            {0xCE , "If "},
            {0xCF , "Then"},
            {0xD0 , "Else"},
            {0xD1 , "While "},
            {0xD2 , "Repeat "},
            {0xD3 , "For("},
            {0xD4 , "End"},
            {0xD5 , "Return"},
            {0xD6 , "Lbl "},
            {0xD7 , "Goto "},
            {0xD8 , "Pause "},
            {0xD9 , "Stop"},
            {0xDA , "IS>("},
            {0xDB , "DS<("},
            {0xDC , "Input "},
            {0xDD , "Prompt "},
            {0xDE , "Disp "},
            {0xDF , "DispGraph"},
            {0xE0 , "Output("},
            {0xE1 , "ClrHome"},
            {0xE2 , "Fill("},
            {0xE3 , "SortA("},
            {0xE4 , "SortD("},
            {0xE5 , "DispTable"},
            {0xE6 , "Menu("},
            {0xE7 , "Send("},
            {0xE8 , "Get("},
            {0xE9 , "PlotsOn "},
            {0xEA , "PlotsOff "},
            {0xEB , "ʟ"},
            {0xEC , "Plot1("},
            {0xED , "Plot2("},
            {0xEE , "Plot3("},
            {0xF0 , "^"},
            {0xF1 , "ˣ√"},
            {0xF2 , "1-Var Stats "},
            {0xF3 , "2-Var Stats"},
            {0xF4 , "LinReg(a+bx) "},
            {0xF5 , "ExpReg "},
            {0xF6 , "LnReg "},
            {0xF7 , "PwrReg "},
            {0xF8 , "Med-Med "},
            {0xF9 , "QuadReg "},
            {0xFA , "ClrList "},
            {0xFB , "ClrTable"},
            {0xFC , "Histogram"},
            {0xFD , "xyLine"},
            {0xFE , "Scatter"},
            {0xFF , "LinReg(ax+b)"}
        };

        public static IEnumerable<byte> Open(string path)
        {
            IEnumerable<byte> filebuf = File.ReadAllBytes(path);
            IEnumerable<byte> correct = new byte[] { 0x2A, 0x2A, 0x54, 0x49, 0x38, 0x33, 0x46, 0x2A, 0x1A, 0x0A, 0x00 };
            IEnumerable<byte> header = filebuf.Take(11);
            int tsize = (filebuf.ElementAt(73) + filebuf.ElementAt(74));
            int protect = filebuf.ElementAt(60);

            if (protect == 6)
            {
                Console.WriteLine("This file is in protected mode.");
                Console.ReadKey();
            }

            if (filebuf == null || !header.SequenceEqual(correct))
            {
                Console.WriteLine("Error: File either does not exist or is not an TI-8X program.");
                return null;
            }

            size = tsize;
            return filebuf;
        }

        private static Random random = new Random();
        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static void OpenDest(string p = null)
        {
            FileStream dest;
            if (p == null || !File.Exists(p))
            {
                Console.WriteLine("Destination path was either not set or doesn't exist.");
                Console.Write("Would you like to create this file? (y/n*): ");
                var ans = Console.ReadKey();
                if (ans.Key == ConsoleKey.Y | ans.Key == ConsoleKey.Enter)
                {
                    if (p == null)
                    {
                        p = RandomString(5);
                        path = p;
                        dest = File.Open(p, FileMode.Create);
                        Console.WriteLine("Created new file for output.");
                        dest.Close();
                        return;
                    }
                    path = p;
                    dest = File.Open(p, FileMode.Create);
                    Console.WriteLine("\nCreated new file for output.");
                    dest.Close();
                    return;
                }
                else
                {
                    return;
                }
            }
            dest = File.Open(p, FileMode.Open);
            dest.Close();
            return;
        }

        public static void Detokenize(byte[] buffer)
        {
            if (buffer == null)
            {
                Console.WriteLine("Error: Buffer Empty.");
                return;
            }

            for (int i = 0; i < (buffer.Length - 2); i++)
            {
                switch (buffer[i])
                {
                    case 0xEF:
                        switch (buffer[i++])
                        {
                            case (0x00): File.AppendAllText(path, "setDate(", Encoding.UTF8); break;
                            case (0x01): File.AppendAllText(path, "setTime(", Encoding.UTF8); break;
                            case (0x02): File.AppendAllText(path, "checkTmr(", Encoding.UTF8); break;
                            case (0x03): File.AppendAllText(path, "setDtFmt(", Encoding.UTF8); break;
                            case (0x04): File.AppendAllText(path, "setTmFmt(", Encoding.UTF8); break;
                            case (0x05): File.AppendAllText(path, "timeCnv(", Encoding.UTF8); break;
                            case (0x06): File.AppendAllText(path, "dayOfWk(", Encoding.UTF8); break;
                            case (0x07): File.AppendAllText(path, "getDtStr", Encoding.UTF8); break;
                            case (0x08): File.AppendAllText(path, "getTmStr(", Encoding.UTF8); break;
                            case (0x09): File.AppendAllText(path, "getDate", Encoding.UTF8); break;
                            case (0x0A): File.AppendAllText(path, "getTime", Encoding.UTF8); break;
                            case (0x0B): File.AppendAllText(path, "startTmr", Encoding.UTF8); break;
                            case (0x0C): File.AppendAllText(path, "getDtFmt", Encoding.UTF8); break;
                            case (0x0D): File.AppendAllText(path, "getTmFmt", Encoding.UTF8); break;
                            case (0x0E): File.AppendAllText(path, "isClockOn", Encoding.UTF8); break;
                            case (0x0F): File.AppendAllText(path, "ClockOff", Encoding.UTF8); break;
                            case (0x10): File.AppendAllText(path, "ClockOn", Encoding.UTF8); break;
                            case (0x11): File.AppendAllText(path, "OpenLib(", Encoding.UTF8); break;
                            case (0x12): File.AppendAllText(path, "ExecLib", Encoding.UTF8); break;
                            case (0x13): File.AppendAllText(path, "invT(", Encoding.UTF8); break;
                            case (0x14): File.AppendAllText(path, "χ²GOF-Test(", Encoding.UTF8); break;
                            case (0x15): File.AppendAllText(path, "LinRegTInt ", Encoding.UTF8); break;
                            case (0x16): File.AppendAllText(path, "Manual-Fit ", Encoding.UTF8); break;
                            case (0x17): File.AppendAllText(path, "ZQuadrant1", Encoding.UTF8); break;
                            case (0x18): File.AppendAllText(path, "ZFrac1/2", Encoding.UTF8); break;
                            case (0x19): File.AppendAllText(path, "ZFrac1/3", Encoding.UTF8); break;
                            case (0x1A): File.AppendAllText(path, "ZFrac1/4", Encoding.UTF8); break;
                            case (0x1B): File.AppendAllText(path, "ZFrac1/5", Encoding.UTF8); break;
                            case (0x1C): File.AppendAllText(path, "ZFrac1/8", Encoding.UTF8); break;
                            case (0x1D): File.AppendAllText(path, "ZFrac1/10", Encoding.UTF8); break;
                            case (0x1E): File.AppendAllText(path, "mathprintbox", Encoding.UTF8); break;
                            case (0x2E): File.AppendAllText(path, "⁄", Encoding.UTF8); break;
                            case (0x2F): File.AppendAllText(path, "ᵤ", Encoding.UTF8); break;
                            case (0x30): File.AppendAllText(path, "►n⁄d◄►Un⁄d", Encoding.UTF8); break;
                            case (0x31): File.AppendAllText(path, "►F◄►D", Encoding.UTF8); break;
                            case (0x32): File.AppendAllText(path, "remainder(", Encoding.UTF8); break;
                            case (0x33): File.AppendAllText(path, "Σ(", Encoding.UTF8); break;
                            case (0x34): File.AppendAllText(path, "logBASE(", Encoding.UTF8); break;
                            case (0x35): File.AppendAllText(path, "randIntNoRep(", Encoding.UTF8); break;
                            case (0x37): File.AppendAllText(path, "[MATHPRINT]", Encoding.UTF8); break;
                            case (0x38): File.AppendAllText(path, "[CLASSIC]", Encoding.UTF8); break;
                            case (0x39): File.AppendAllText(path, "n⁄d", Encoding.UTF8); break;
                            case (0x3A): File.AppendAllText(path, "Un⁄d", Encoding.UTF8); break;
                            case (0x3B): File.AppendAllText(path, "[AUTO]", Encoding.UTF8); break;
                            case (0x3C): File.AppendAllText(path, "[DEC]", Encoding.UTF8); break;
                            case (0x3D): File.AppendAllText(path, "[FRAC]", Encoding.UTF8); break;
                            case (0x41): File.AppendAllText(path, "BLUE", Encoding.UTF8); break;
                            case (0x42): File.AppendAllText(path, "RED", Encoding.UTF8); break;
                            case (0x43): File.AppendAllText(path, "BLACK", Encoding.UTF8); break;
                            case (0x44): File.AppendAllText(path, "MAGENTA", Encoding.UTF8); break;
                            case (0x45): File.AppendAllText(path, "GREEN", Encoding.UTF8); break;
                            case (0x46): File.AppendAllText(path, "ORANGE", Encoding.UTF8); break;
                            case (0x47): File.AppendAllText(path, "BROWN", Encoding.UTF8); break;
                            case (0x48): File.AppendAllText(path, "NAVY", Encoding.UTF8); break;
                            case (0x49): File.AppendAllText(path, "LTBLUE", Encoding.UTF8); break;
                            case (0x4A): File.AppendAllText(path, "YELLOW", Encoding.UTF8); break;
                            case (0x4B): File.AppendAllText(path, "WHITE", Encoding.UTF8); break;
                            case (0x4C): File.AppendAllText(path, "LTGREY", Encoding.UTF8); break;
                            case (0x4D): File.AppendAllText(path, "MEDGREY", Encoding.UTF8); break;
                            case (0x4E): File.AppendAllText(path, "GREY", Encoding.UTF8); break;
                            case (0x4F): File.AppendAllText(path, "DARKGREY", Encoding.UTF8); break;
                            case (0x5A): File.AppendAllText(path, "GridLine ", Encoding.UTF8); break;
                            case (0x5B): File.AppendAllText(path, "BackgroundOn ", Encoding.UTF8); break;
                            case (0x6A): File.AppendAllText(path, "DetectAsymOn", Encoding.UTF8); break;
                            case (0x6B): File.AppendAllText(path, "DetectAsymOff", Encoding.UTF8); break;
                            case (0x64): File.AppendAllText(path, "BackgroundOff", Encoding.UTF8); break;
                            case (0x65): File.AppendAllText(path, "GraphColor(", Encoding.UTF8); break;
                            case (0x67): File.AppendAllText(path, "TextColor(", Encoding.UTF8); break;
                            case (0x68): File.AppendAllText(path, "Asm84CPrgm", Encoding.UTF8); break;
                            case (0x6C): File.AppendAllText(path, "BorderColor ", Encoding.UTF8); break;
                            case (0x73): File.AppendAllText(path, "tinydotplot", Encoding.UTF8); break;
                            case (0x74): File.AppendAllText(path, "Thin", Encoding.UTF8); break;
                            case (0x75): File.AppendAllText(path, "Dot-Thin", Encoding.UTF8); break;
                            default:
                                Console.WriteLine("Error Decompiling: Invalid token: 0x" + buffer[i++].ToString("X2")); break;
                        }
                        break;

                    case 0xAA:
                        switch (buffer[i++])
                        {
                            case (0x00): File.AppendAllText(path, "Str1", Encoding.UTF8); break;
                            case (0x01): File.AppendAllText(path, "Str2", Encoding.UTF8); break;
                            case (0x02): File.AppendAllText(path, "Str3", Encoding.UTF8); break;
                            case (0x03): File.AppendAllText(path, "Str4", Encoding.UTF8); break;
                            case (0x04): File.AppendAllText(path, "Str5", Encoding.UTF8); break;
                            case (0x05): File.AppendAllText(path, "Str6", Encoding.UTF8); break;
                            case (0x06): File.AppendAllText(path, "Str7", Encoding.UTF8); break;
                            case (0x07): File.AppendAllText(path, "Str8", Encoding.UTF8); break;
                            case (0x08): File.AppendAllText(path, "Str9", Encoding.UTF8); break;
                            case (0x09): File.AppendAllText(path, "Str0", Encoding.UTF8); break;
                            default:
                                Console.WriteLine("Error Decompiling: Invalid token: 0x" + buffer[i++].ToString("X2")); break;
                        }
                        break;
                    case 0xBB:
                        switch (buffer[i++])
                        {
                            case (0x00): File.AppendAllText(path, "npv(", Encoding.UTF8); break;
                            case (0x01): File.AppendAllText(path, "irr(", Encoding.UTF8); break;
                            case (0x02): File.AppendAllText(path, "bal(", Encoding.UTF8); break;
                            case (0x03): File.AppendAllText(path, "ΣPrn(", Encoding.UTF8); break;
                            case (0x04): File.AppendAllText(path, "ΣInt(", Encoding.UTF8); break;
                            case (0x05): File.AppendAllText(path, "►Nom(", Encoding.UTF8); break;
                            case (0x06): File.AppendAllText(path, "►Eff(", Encoding.UTF8); break;
                            case (0x07): File.AppendAllText(path, "dbd(", Encoding.UTF8); break;
                            case (0x08): File.AppendAllText(path, "lcm(", Encoding.UTF8); break;
                            case (0x09): File.AppendAllText(path, "gcd(", Encoding.UTF8); break;
                            case (0x0A): File.AppendAllText(path, "randInt(", Encoding.UTF8); break;
                            case (0x0B): File.AppendAllText(path, "randBin(", Encoding.UTF8); break;
                            case (0x0C): File.AppendAllText(path, "sub(", Encoding.UTF8); break;
                            case (0x0D): File.AppendAllText(path, "stdDev(", Encoding.UTF8); break;
                            case (0x0E): File.AppendAllText(path, "variance(", Encoding.UTF8); break;
                            case (0x0F): File.AppendAllText(path, "inString(", Encoding.UTF8); break;
                            case (0x10): File.AppendAllText(path, "normalcdf(", Encoding.UTF8); break;
                            case (0x11): File.AppendAllText(path, "invNorm(", Encoding.UTF8); break;
                            case (0x12): File.AppendAllText(path, "tcdf(", Encoding.UTF8); break;
                            case (0x13): File.AppendAllText(path, "χ²cdf(", Encoding.UTF8); break;
                            case (0x14): File.AppendAllText(path, "Fcdf(", Encoding.UTF8); break;
                            case (0x15): File.AppendAllText(path, "binompdf(", Encoding.UTF8); break;
                            case (0x16): File.AppendAllText(path, "binomcdf(", Encoding.UTF8); break;
                            case (0x17): File.AppendAllText(path, "poissonpdf(", Encoding.UTF8); break;
                            case (0x18): File.AppendAllText(path, "poissoncdf(", Encoding.UTF8); break;
                            case (0x19): File.AppendAllText(path, "geometpdf(", Encoding.UTF8); break;
                            case (0x1A): File.AppendAllText(path, "geometcdf(", Encoding.UTF8); break;
                            case (0x1B): File.AppendAllText(path, "normalpdf(", Encoding.UTF8); break;
                            case (0x1C): File.AppendAllText(path, "tpdf(", Encoding.UTF8); break;
                            case (0x1D): File.AppendAllText(path, "χ²pdf(", Encoding.UTF8); break;
                            case (0x1E): File.AppendAllText(path, "Fpdf(", Encoding.UTF8); break;
                            case (0x1F): File.AppendAllText(path, "randNorm(", Encoding.UTF8); break;
                            case (0x20): File.AppendAllText(path, "tvm_Pmt", Encoding.UTF8); break;
                            case (0x21): File.AppendAllText(path, "tvm_I%", Encoding.UTF8); break;
                            case (0x22): File.AppendAllText(path, "tvm_PV", Encoding.UTF8); break;
                            case (0x23): File.AppendAllText(path, "tvm_N", Encoding.UTF8); break;
                            case (0x24): File.AppendAllText(path, "tvm_FV", Encoding.UTF8); break;
                            case (0x25): File.AppendAllText(path, "conj(", Encoding.UTF8); break;
                            case (0x26): File.AppendAllText(path, "real(", Encoding.UTF8); break;
                            case (0x27): File.AppendAllText(path, "imag(", Encoding.UTF8); break;
                            case (0x28): File.AppendAllText(path, "angle(", Encoding.UTF8); break;
                            case (0x29): File.AppendAllText(path, "cumSum(", Encoding.UTF8); break;
                            case (0x2A): File.AppendAllText(path, "expr(", Encoding.UTF8); break;
                            case (0x2B): File.AppendAllText(path, "length(", Encoding.UTF8); break;
                            case (0x2C): File.AppendAllText(path, "DeltaList(", Encoding.UTF8); break;
                            case (0x2D): File.AppendAllText(path, "ref(", Encoding.UTF8); break;
                            case (0x2E): File.AppendAllText(path, "rref(", Encoding.UTF8); break;
                            case (0x2F): File.AppendAllText(path, "►Rect", Encoding.UTF8); break;
                            case (0x30): File.AppendAllText(path, "►Polar", Encoding.UTF8); break;
                            case (0x31): File.AppendAllText(path, "[e]", Encoding.UTF8); break;
                            case (0x32): File.AppendAllText(path, "SinReg ", Encoding.UTF8); break;
                            case (0x33): File.AppendAllText(path, "Logistic ", Encoding.UTF8); break;
                            case (0x34): File.AppendAllText(path, "LinRegTTest ", Encoding.UTF8); break;
                            case (0x35): File.AppendAllText(path, "ShadeNorm(", Encoding.UTF8); break;
                            case (0x36): File.AppendAllText(path, "Shade_t(", Encoding.UTF8); break;
                            case (0x37): File.AppendAllText(path, "Shadeχ²(", Encoding.UTF8); break;
                            case (0x38): File.AppendAllText(path, "ShadeF(", Encoding.UTF8); break;
                            case (0x39): File.AppendAllText(path, "Matr►list(", Encoding.UTF8); break;
                            case (0x3A): File.AppendAllText(path, "List►matr(", Encoding.UTF8); break;
                            case (0x3B): File.AppendAllText(path, "Z-Test(", Encoding.UTF8); break;
                            case (0x3C): File.AppendAllText(path, "T-Test ", Encoding.UTF8); break;
                            case (0x3D): File.AppendAllText(path, "2-SampZTest(", Encoding.UTF8); break;
                            case (0x3E): File.AppendAllText(path, "1-PropZTest(", Encoding.UTF8); break;
                            case (0x3F): File.AppendAllText(path, "2-PropZTest(", Encoding.UTF8); break;
                            case (0x40): File.AppendAllText(path, "χ²-Test(", Encoding.UTF8); break;
                            case (0x41): File.AppendAllText(path, "ZInterval", Encoding.UTF8); break;
                            case (0x42): File.AppendAllText(path, "2-SampZInt(", Encoding.UTF8); break;
                            case (0x43): File.AppendAllText(path, "1-PropZInt(", Encoding.UTF8); break;
                            case (0x44): File.AppendAllText(path, "2-PropZInt(", Encoding.UTF8); break;
                            case (0x45): File.AppendAllText(path, "GraphStyle(", Encoding.UTF8); break;
                            case (0x46): File.AppendAllText(path, "2-SampTTest ", Encoding.UTF8); break;
                            case (0x47): File.AppendAllText(path, "2-SampFTest ", Encoding.UTF8); break;
                            case (0x48): File.AppendAllText(path, "TInterval ", Encoding.UTF8); break;
                            case (0x49): File.AppendAllText(path, "2-SampTInt ", Encoding.UTF8); break;
                            case (0x4A): File.AppendAllText(path, "SetUpEditor ", Encoding.UTF8); break;
                            case (0x4B): File.AppendAllText(path, "Pmt_End", Encoding.UTF8); break;
                            case (0x4C): File.AppendAllText(path, "Pmt_Bgn", Encoding.UTF8); break;
                            case (0x4D): File.AppendAllText(path, "Real", Encoding.UTF8); break;
                            case (0x4E): File.AppendAllText(path, "re^θi", Encoding.UTF8); break;
                            case (0x4F): File.AppendAllText(path, "a+bi", Encoding.UTF8); break;
                            case (0x50): File.AppendAllText(path, "ExprOn", Encoding.UTF8); break;
                            case (0x51): File.AppendAllText(path, "ExprOff", Encoding.UTF8); break;
                            case (0x52): File.AppendAllText(path, "ClrAllLists", Encoding.UTF8); break;
                            case (0x53): File.AppendAllText(path, "GetCalc(", Encoding.UTF8); break;
                            case (0x54): File.AppendAllText(path, "DelVar ", Encoding.UTF8); break;
                            case (0x55): File.AppendAllText(path, "Equ►String(", Encoding.UTF8); break;
                            case (0x56): File.AppendAllText(path, "String►Equ(", Encoding.UTF8); break;
                            case (0x57): File.AppendAllText(path, "Clear Entries", Encoding.UTF8); break;
                            case (0x58): File.AppendAllText(path, "Select(", Encoding.UTF8); break;
                            case (0x59): File.AppendAllText(path, "ANOVA(", Encoding.UTF8); break;
                            case (0x5A): File.AppendAllText(path, "ModBoxPlot", Encoding.UTF8); break;
                            case (0x5B): File.AppendAllText(path, "NormProbPlot", Encoding.UTF8); break;
                            case (0x64): File.AppendAllText(path, "G-T", Encoding.UTF8); break;
                            case (0x65): File.AppendAllText(path, "ZoomFit", Encoding.UTF8); break;
                            case (0x66): File.AppendAllText(path, "DiagnosticOn", Encoding.UTF8); break;
                            case (0x67): File.AppendAllText(path, "DiagnosticOff", Encoding.UTF8); break;
                            case (0x68): File.AppendAllText(path, "Archive ", Encoding.UTF8); break;
                            case (0x69): File.AppendAllText(path, "UnArchive ", Encoding.UTF8); break;
                            case (0x6A): File.AppendAllText(path, "Asm(", Encoding.UTF8); break;
                            case (0x6B): File.AppendAllText(path, "AsmComp(", Encoding.UTF8); break;
                            case (0x6C): File.AppendAllText(path, "AsmPrgm", Encoding.UTF8); break;
                            case (0x6E): File.AppendAllText(path, "Á", Encoding.UTF8); break;
                            case (0x6F): File.AppendAllText(path, "À", Encoding.UTF8); break;
                            case (0x70): File.AppendAllText(path, "Â", Encoding.UTF8); break;
                            case (0x71): File.AppendAllText(path, "Ä", Encoding.UTF8); break;
                            case (0x72): File.AppendAllText(path, "á", Encoding.UTF8); break;
                            case (0x73): File.AppendAllText(path, "à", Encoding.UTF8); break;
                            case (0x74): File.AppendAllText(path, "â", Encoding.UTF8); break;
                            case (0x75): File.AppendAllText(path, "ä", Encoding.UTF8); break;
                            case (0x76): File.AppendAllText(path, "É", Encoding.UTF8); break;
                            case (0x77): File.AppendAllText(path, "È", Encoding.UTF8); break;
                            case (0x78): File.AppendAllText(path, "Ê", Encoding.UTF8); break;
                            case (0x79): File.AppendAllText(path, "Ë", Encoding.UTF8); break;
                            case (0x7A): File.AppendAllText(path, "é", Encoding.UTF8); break;
                            case (0x7B): File.AppendAllText(path, "è", Encoding.UTF8); break;
                            case (0x7C): File.AppendAllText(path, "ê", Encoding.UTF8); break;
                            case (0x7D): File.AppendAllText(path, "ë", Encoding.UTF8); break;
                            case (0x7F): File.AppendAllText(path, "Ì", Encoding.UTF8); break;
                            case (0x80): File.AppendAllText(path, "Î", Encoding.UTF8); break;
                            case (0x81): File.AppendAllText(path, "Ï", Encoding.UTF8); break;
                            case (0x82): File.AppendAllText(path, "í", Encoding.UTF8); break;
                            case (0x83): File.AppendAllText(path, "ì", Encoding.UTF8); break;
                            case (0x84): File.AppendAllText(path, "î", Encoding.UTF8); break;
                            case (0x85): File.AppendAllText(path, "ï", Encoding.UTF8); break;
                            case (0x86): File.AppendAllText(path, "Ó", Encoding.UTF8); break;
                            case (0x87): File.AppendAllText(path, "Ò", Encoding.UTF8); break;
                            case (0x88): File.AppendAllText(path, "Ô", Encoding.UTF8); break;
                            case (0x89): File.AppendAllText(path, "Ö", Encoding.UTF8); break;
                            case (0x8A): File.AppendAllText(path, "ó", Encoding.UTF8); break;
                            case (0x8B): File.AppendAllText(path, "ò", Encoding.UTF8); break;
                            case (0x8C): File.AppendAllText(path, "ô", Encoding.UTF8); break;
                            case (0x8D): File.AppendAllText(path, "ö", Encoding.UTF8); break;
                            case (0x8E): File.AppendAllText(path, "Ú", Encoding.UTF8); break;
                            case (0x8F): File.AppendAllText(path, "Ù", Encoding.UTF8); break;
                            case (0x90): File.AppendAllText(path, "Û", Encoding.UTF8); break;
                            case (0x91): File.AppendAllText(path, "Ü", Encoding.UTF8); break;
                            case (0x92): File.AppendAllText(path, "ú", Encoding.UTF8); break;
                            case (0x93): File.AppendAllText(path, "ù", Encoding.UTF8); break;
                            case (0x94): File.AppendAllText(path, "û", Encoding.UTF8); break;
                            case (0x95): File.AppendAllText(path, "ü", Encoding.UTF8); break;
                            case (0x96): File.AppendAllText(path, "Ç", Encoding.UTF8); break;
                            case (0x97): File.AppendAllText(path, "ç", Encoding.UTF8); break;
                            case (0x98): File.AppendAllText(path, "Ñ", Encoding.UTF8); break;
                            case (0x99): File.AppendAllText(path, "ñ", Encoding.UTF8); break;
                            case (0x9A): File.AppendAllText(path, "´", Encoding.UTF8); break;
                            case (0x9B): File.AppendAllText(path, "|`", Encoding.UTF8); break;
                            case (0x9C): File.AppendAllText(path, "¨", Encoding.UTF8); break;
                            case (0x9D): File.AppendAllText(path, "¿", Encoding.UTF8); break;
                            case (0x9E): File.AppendAllText(path, "¡", Encoding.UTF8); break;
                            case (0x9F): File.AppendAllText(path, "α", Encoding.UTF8); break;
                            case (0xA0): File.AppendAllText(path, "β", Encoding.UTF8); break;
                            case (0xA1): File.AppendAllText(path, "γ", Encoding.UTF8); break;
                            case (0xA2): File.AppendAllText(path, "Δ", Encoding.UTF8); break;
                            case (0xA3): File.AppendAllText(path, "δ", Encoding.UTF8); break;
                            case (0xA4): File.AppendAllText(path, "ε", Encoding.UTF8); break;
                            case (0xA5): File.AppendAllText(path, "λ", Encoding.UTF8); break;
                            case (0xA6): File.AppendAllText(path, "μ", Encoding.UTF8); break;
                            case (0xA7): File.AppendAllText(path, "|π", Encoding.UTF8); break;
                            case (0xA8): File.AppendAllText(path, "ρ", Encoding.UTF8); break;
                            case (0xA9): File.AppendAllText(path, "Σ", Encoding.UTF8); break;
                            case (0xAB): File.AppendAllText(path, "Φ", Encoding.UTF8); break;
                            case (0xAC): File.AppendAllText(path, "Ω", Encoding.UTF8); break;
                            case (0xAD): File.AppendAllText(path, "ṗ", Encoding.UTF8); break;
                            case (0xAE): File.AppendAllText(path, "χ", Encoding.UTF8); break;
                            case (0xAF): File.AppendAllText(path, "|F", Encoding.UTF8); break;
                            case (0xB0): File.AppendAllText(path, "a", Encoding.UTF8); break;
                            case (0xB1): File.AppendAllText(path, "b", Encoding.UTF8); break;
                            case (0xB2): File.AppendAllText(path, "c", Encoding.UTF8); break;
                            case (0xB3): File.AppendAllText(path, "d", Encoding.UTF8); break;
                            case (0xB4): File.AppendAllText(path, "e", Encoding.UTF8); break;
                            case (0xB5): File.AppendAllText(path, "f", Encoding.UTF8); break;
                            case (0xB6): File.AppendAllText(path, "g", Encoding.UTF8); break;
                            case (0xB7): File.AppendAllText(path, "h", Encoding.UTF8); break;
                            case (0xB8): File.AppendAllText(path, "i", Encoding.UTF8); break;
                            case (0xB9): File.AppendAllText(path, "j", Encoding.UTF8); break;
                            case (0xBA): File.AppendAllText(path, "k", Encoding.UTF8); break;
                            case (0xBC): File.AppendAllText(path, "l", Encoding.UTF8); break;
                            case (0xBD): File.AppendAllText(path, "m", Encoding.UTF8); break;
                            case (0xBE): File.AppendAllText(path, "n", Encoding.UTF8); break;
                            case (0xBF): File.AppendAllText(path, "o", Encoding.UTF8); break;
                            case (0xC0): File.AppendAllText(path, "p", Encoding.UTF8); break;
                            case (0xC1): File.AppendAllText(path, "q", Encoding.UTF8); break;
                            case (0xC2): File.AppendAllText(path, "r", Encoding.UTF8); break;
                            case (0xC3): File.AppendAllText(path, "s", Encoding.UTF8); break;
                            case (0xC4): File.AppendAllText(path, "t", Encoding.UTF8); break;
                            case (0xC5): File.AppendAllText(path, "u", Encoding.UTF8); break;
                            case (0xC6): File.AppendAllText(path, "v", Encoding.UTF8); break;
                            case (0xC7): File.AppendAllText(path, "w", Encoding.UTF8); break;
                            case (0xC8): File.AppendAllText(path, "x", Encoding.UTF8); break;
                            case (0xC9): File.AppendAllText(path, "y", Encoding.UTF8); break;
                            case (0xCA): File.AppendAllText(path, "z", Encoding.UTF8); break;
                            case (0xCB): File.AppendAllText(path, "σ", Encoding.UTF8); break;
                            case (0xCC): File.AppendAllText(path, "τ", Encoding.UTF8); break;
                            case (0xCD): File.AppendAllText(path, "Í", Encoding.UTF8); break;
                            case (0xCE): File.AppendAllText(path, "GarbageCollect", Encoding.UTF8); break;
                            case (0xCF): File.AppendAllText(path, "|~", Encoding.UTF8); break;
                            case (0xD1): File.AppendAllText(path, "@", Encoding.UTF8); break;
                            case (0xD2): File.AppendAllText(path, "#", Encoding.UTF8); break;
                            case (0xD3): File.AppendAllText(path, "$", Encoding.UTF8); break;
                            case (0xD4): File.AppendAllText(path, "&", Encoding.UTF8); break;
                            case (0xD5): File.AppendAllText(path, "`", Encoding.UTF8); break;
                            case (0xD6): File.AppendAllText(path, ";", Encoding.UTF8); break;
                            case (0xD7): File.AppendAllText(path, "\\", Encoding.UTF8); break;
                            case (0xD8): File.AppendAllText(path, "|", Encoding.UTF8); break;
                            case (0xD9): File.AppendAllText(path, "_", Encoding.UTF8); break;
                            case (0xDA): File.AppendAllText(path, "%", Encoding.UTF8); break;
                            case (0xDB): File.AppendAllText(path, "…", Encoding.UTF8); break;
                            case (0xDC): File.AppendAllText(path, "∠", Encoding.UTF8); break;
                            case (0xDD): File.AppendAllText(path, "ß", Encoding.UTF8); break;
                            case (0xDE): File.AppendAllText(path, "ˣ", Encoding.UTF8); break;
                            case (0xDF): File.AppendAllText(path, "ᴛ", Encoding.UTF8); break;
                            case (0xE0): File.AppendAllText(path, "₀", Encoding.UTF8); break;
                            case (0xE1): File.AppendAllText(path, "₁", Encoding.UTF8); break;
                            case (0xE2): File.AppendAllText(path, "₂", Encoding.UTF8); break;
                            case (0xE3): File.AppendAllText(path, "₃", Encoding.UTF8); break;
                            case (0xE4): File.AppendAllText(path, "₄", Encoding.UTF8); break;
                            case (0xE5): File.AppendAllText(path, "₅", Encoding.UTF8); break;
                            case (0xE6): File.AppendAllText(path, "₆", Encoding.UTF8); break;
                            case (0xE7): File.AppendAllText(path, "₇", Encoding.UTF8); break;
                            case (0xE8): File.AppendAllText(path, "₈", Encoding.UTF8); break;
                            case (0xE9): File.AppendAllText(path, "₉", Encoding.UTF8); break;
                            case (0xEA): File.AppendAllText(path, "₁₀", Encoding.UTF8); break;
                            case (0xEB): File.AppendAllText(path, "◄", Encoding.UTF8); break;
                            case (0xEC): File.AppendAllText(path, "►", Encoding.UTF8); break;
                            case (0xED): File.AppendAllText(path, "↑", Encoding.UTF8); break;
                            case (0xEE): File.AppendAllText(path, "↓", Encoding.UTF8); break;
                            case (0xF0): File.AppendAllText(path, "×", Encoding.UTF8); break;
                            case (0xF1): File.AppendAllText(path, "∫", Encoding.UTF8); break;
                            case (0xF2): File.AppendAllText(path, "bolduparrow", Encoding.UTF8); break;
                            case (0xF3): File.AppendAllText(path, "bolddownarrow", Encoding.UTF8); break;
                            case (0xF4): File.AppendAllText(path, "√", Encoding.UTF8); break;
                            case (0xF5): File.AppendAllText(path, "invertedequal", Encoding.UTF8); break;
                            default:
                                Console.WriteLine("Error Decompiling: Invalid token: 0x" + buffer[i++].ToString("X2")); break;
                        }
                        break;
                    case 0xB3:
                        switch (buffer[i++])
                        {
                            case (0x32): File.AppendAllText(path, "InsertLine(", Encoding.UTF8); break;
                            case (0x33): File.AppendAllText(path, "SpecialChars(", Encoding.UTF8); break;
                            case (0x34): File.AppendAllText(path, "CreateVar(", Encoding.UTF8); break;
                            case (0x35): File.AppendAllText(path, "ArcUnarcVar(", Encoding.UTF8); break;
                            case (0x36): File.AppendAllText(path, "DeleteVar(", Encoding.UTF8); break;
                            case (0x37): File.AppendAllText(path, "DeleteLine(", Encoding.UTF8); break;
                            case (0x38): File.AppendAllText(path, "VarStatus(", Encoding.UTF8); break;
                            case (0x31): File.AppendAllText(path, "ReplaceLine(", Encoding.UTF8); break;
                            case (0x30): File.AppendAllText(path, "ReadLine(", Encoding.UTF8); break;
                            default:
                                File.AppendAllText(path, "det ", Encoding.UTF8); break;
                        }
                        break;
                    case 0x7E:
                        switch (buffer[i++])
                        {
                            case (0x00): File.AppendAllText(path, "Sequential", Encoding.UTF8); break;
                            case (0x01): File.AppendAllText(path, "Simul", Encoding.UTF8); break;
                            case (0x02): File.AppendAllText(path, "PolarGC", Encoding.UTF8); break;
                            case (0x03): File.AppendAllText(path, "RectGC", Encoding.UTF8); break;
                            case (0x04): File.AppendAllText(path, "CoordOn", Encoding.UTF8); break;
                            case (0x05): File.AppendAllText(path, "CoordOff", Encoding.UTF8); break;
                            case (0x06): File.AppendAllText(path, "Thick", Encoding.UTF8); break;
                            case (0x07): File.AppendAllText(path, "Dot-Thick", Encoding.UTF8); break;
                            case (0x08): File.AppendAllText(path, "AxesOn", Encoding.UTF8); break;
                            case (0x09): File.AppendAllText(path, "AxesOff", Encoding.UTF8); break;
                            case (0x0A): File.AppendAllText(path, "GridDot ", Encoding.UTF8); break;
                            case (0x0B): File.AppendAllText(path, "GridOff", Encoding.UTF8); break;
                            case (0x0C): File.AppendAllText(path, "LabelOn", Encoding.UTF8); break;
                            case (0x0D): File.AppendAllText(path, "LabelOff", Encoding.UTF8); break;
                            case (0x0E): File.AppendAllText(path, "Web", Encoding.UTF8); break;
                            case (0x0F): File.AppendAllText(path, "Time", Encoding.UTF8); break;
                            case (0x10): File.AppendAllText(path, "uvAxes", Encoding.UTF8); break;
                            case (0x11): File.AppendAllText(path, "vwAxes", Encoding.UTF8); break;
                            case (0x12): File.AppendAllText(path, "uwAxes", Encoding.UTF8); break;
                            default:
                                Console.WriteLine("Error Decompiling: Invalid token: 0x" + buffer[i++].ToString("X2")); break;
                        }
                        break;
                    case 0x60:
                        switch (buffer[i++])
                        {
                            case (0x00): File.AppendAllText(path, "Pic1", Encoding.UTF8); break;
                            case (0x01): File.AppendAllText(path, "Pic2", Encoding.UTF8); break;
                            case (0x02): File.AppendAllText(path, "Pic3", Encoding.UTF8); break;
                            case (0x03): File.AppendAllText(path, "Pic4", Encoding.UTF8); break;
                            case (0x04): File.AppendAllText(path, "Pic5", Encoding.UTF8); break;
                            case (0x05): File.AppendAllText(path, "Pic6", Encoding.UTF8); break;
                            case (0x06): File.AppendAllText(path, "Pic7", Encoding.UTF8); break;
                            case (0x07): File.AppendAllText(path, "Pic8", Encoding.UTF8); break;
                            case (0x08): File.AppendAllText(path, "Pic9", Encoding.UTF8); break;
                            case (0x09): File.AppendAllText(path, "Pic0", Encoding.UTF8); break;
                            default:
                                Console.WriteLine("Error Decompiling: Invalid token: 0x" + buffer[i++].ToString("X2")); break;
                        }
                        break;
                    case 0x61:
                        switch (buffer[i++])
                        {
                            case (0x00): File.AppendAllText(path, "GDB1", Encoding.UTF8); break;
                            case (0x01): File.AppendAllText(path, "GDB2", Encoding.UTF8); break;
                            case (0x02): File.AppendAllText(path, "GDB3", Encoding.UTF8); break;
                            case (0x03): File.AppendAllText(path, "GDB4", Encoding.UTF8); break;
                            case (0x04): File.AppendAllText(path, "GDB5", Encoding.UTF8); break;
                            case (0x05): File.AppendAllText(path, "GDB6", Encoding.UTF8); break;
                            case (0x06): File.AppendAllText(path, "GDB7", Encoding.UTF8); break;
                            case (0x07): File.AppendAllText(path, "GDB8", Encoding.UTF8); break;
                            case (0x08): File.AppendAllText(path, "GDB9", Encoding.UTF8); break;
                            case (0x09): File.AppendAllText(path, "GDB0", Encoding.UTF8); break;
                            default:
                                Console.WriteLine("Error Decompiling: Invalid token: 0x" + buffer[i++].ToString("X2")); break;
                        }
                        break;
                    case 0x62:
                        switch (buffer[i++])
                        {
                            case (0x01): File.AppendAllText(path, "[RegEQ]", Encoding.UTF8); break;
                            case (0x02): File.AppendAllText(path, "[n]", Encoding.UTF8); break;
                            case (0x03): File.AppendAllText(path, "ẋ", Encoding.UTF8); break;
                            case (0x04): File.AppendAllText(path, "Σx", Encoding.UTF8); break;
                            case (0x05): File.AppendAllText(path, "Σx²", Encoding.UTF8); break;
                            case (0x06): File.AppendAllText(path, "[Sx]", Encoding.UTF8); break;
                            case (0x07): File.AppendAllText(path, "σx", Encoding.UTF8); break;
                            case (0x08): File.AppendAllText(path, "[minX]", Encoding.UTF8); break;
                            case (0x09): File.AppendAllText(path, "[maxX]", Encoding.UTF8); break;
                            case (0x0A): File.AppendAllText(path, "[minY]", Encoding.UTF8); break;
                            case (0x0B): File.AppendAllText(path, "[maxY]", Encoding.UTF8); break;
                            case (0x0C): File.AppendAllText(path, "ȳ", Encoding.UTF8); break;
                            case (0x0D): File.AppendAllText(path, "Σy", Encoding.UTF8); break;
                            case (0x0E): File.AppendAllText(path, "Σy²", Encoding.UTF8); break;
                            case (0x0F): File.AppendAllText(path, "[Sy]", Encoding.UTF8); break;
                            case (0x10): File.AppendAllText(path, "σy", Encoding.UTF8); break;
                            case (0x11): File.AppendAllText(path, "Σxy", Encoding.UTF8); break;
                            case (0x12): File.AppendAllText(path, "[r]", Encoding.UTF8); break;
                            case (0x13): File.AppendAllText(path, "[Med]", Encoding.UTF8); break;
                            case (0x14): File.AppendAllText(path, "[Q1]", Encoding.UTF8); break;
                            case (0x15): File.AppendAllText(path, "[Q3]", Encoding.UTF8); break;
                            case (0x16): File.AppendAllText(path, "[|a]", Encoding.UTF8); break;
                            case (0x17): File.AppendAllText(path, "[|b]", Encoding.UTF8); break;
                            case (0x18): File.AppendAllText(path, "[|c]", Encoding.UTF8); break;
                            case (0x19): File.AppendAllText(path, "[|d]", Encoding.UTF8); break;
                            case (0x1A): File.AppendAllText(path, "[|e]", Encoding.UTF8); break;
                            case (0x1B): File.AppendAllText(path, "x₁", Encoding.UTF8); break;
                            case (0x1C): File.AppendAllText(path, "x₂", Encoding.UTF8); break;
                            case (0x1D): File.AppendAllText(path, "x₃", Encoding.UTF8); break;
                            case (0x1E): File.AppendAllText(path, "y₁", Encoding.UTF8); break;
                            case (0x1F): File.AppendAllText(path, "y₂", Encoding.UTF8); break;
                            case (0x20): File.AppendAllText(path, "y₃", Encoding.UTF8); break;
                            case (0x21): File.AppendAllText(path, "[recursiven]", Encoding.UTF8); break;
                            case (0x22): File.AppendAllText(path, "[p]", Encoding.UTF8); break;
                            case (0x23): File.AppendAllText(path, "[z]", Encoding.UTF8); break;
                            case (0x24): File.AppendAllText(path, "[t]", Encoding.UTF8); break;
                            case (0x25): File.AppendAllText(path, "χ²", Encoding.UTF8); break;
                            case (0x26): File.AppendAllText(path, "[|F]", Encoding.UTF8); break;
                            case (0x27): File.AppendAllText(path, "[df]", Encoding.UTF8); break;
                            case (0x28): File.AppendAllText(path, "[ṗ]", Encoding.UTF8); break;
                            case (0x29): File.AppendAllText(path, "ṗ₁", Encoding.UTF8); break;
                            case (0x2A): File.AppendAllText(path, "ṗ₂", Encoding.UTF8); break;
                            case (0x2B): File.AppendAllText(path, "ẋ₁", Encoding.UTF8); break;
                            case (0x2C): File.AppendAllText(path, "Sx₁", Encoding.UTF8); break;
                            case (0x2D): File.AppendAllText(path, "n₁", Encoding.UTF8); break;
                            case (0x2E): File.AppendAllText(path, "ẋ₂", Encoding.UTF8); break;
                            case (0x2F): File.AppendAllText(path, "Sx₂", Encoding.UTF8); break;
                            case (0x30): File.AppendAllText(path, "n₂", Encoding.UTF8); break;
                            case (0x31): File.AppendAllText(path, "[Sxp]", Encoding.UTF8); break;
                            case (0x32): File.AppendAllText(path, "[lower]", Encoding.UTF8); break;
                            case (0x33): File.AppendAllText(path, "[upper]", Encoding.UTF8); break;
                            case (0x34): File.AppendAllText(path, "[s]", Encoding.UTF8); break;
                            case (0x35): File.AppendAllText(path, "r²", Encoding.UTF8); break;
                            case (0x36): File.AppendAllText(path, "R²", Encoding.UTF8); break;
                            case (0x37): File.AppendAllText(path, "[factordf]", Encoding.UTF8); break;
                            case (0x38): File.AppendAllText(path, "[factorSS]", Encoding.UTF8); break;
                            case (0x39): File.AppendAllText(path, "[factorMS]", Encoding.UTF8); break;
                            case (0x3A): File.AppendAllText(path, "[errordf]", Encoding.UTF8); break;
                            case (0x3B): File.AppendAllText(path, "[errorSS]", Encoding.UTF8); break;
                            case (0x3C): File.AppendAllText(path, "[errorMS]", Encoding.UTF8); break;
                            default:
                                Console.WriteLine("Error Decompiling: Invalid token: 0x" + buffer[i++].ToString("X2")); break;
                        }
                        break;
                    case 0x63:
                        switch (buffer[i++])
                        {
                            case (0x00): File.AppendAllText(path, "ZXscl", Encoding.UTF8); break;
                            case (0x01): File.AppendAllText(path, "ZYscl", Encoding.UTF8); break;
                            case (0x02): File.AppendAllText(path, "Xscl", Encoding.UTF8); break;
                            case (0x03): File.AppendAllText(path, "Yscl", Encoding.UTF8); break;
                            case (0x04): File.AppendAllText(path, "u(nMin)", Encoding.UTF8); break;
                            case (0x05): File.AppendAllText(path, "v(nMin)", Encoding.UTF8); break;
                            case (0x06): File.AppendAllText(path, "Un-₁", Encoding.UTF8); break;
                            case (0x07): File.AppendAllText(path, "Vn-₁", Encoding.UTF8); break;
                            case (0x08): File.AppendAllText(path, "Zu(nmin)", Encoding.UTF8); break;
                            case (0x09): File.AppendAllText(path, "Zv(nmin)", Encoding.UTF8); break;
                            case (0x0A): File.AppendAllText(path, "Xmin", Encoding.UTF8); break;
                            case (0x0B): File.AppendAllText(path, "Xmax", Encoding.UTF8); break;
                            case (0x0C): File.AppendAllText(path, "Ymin", Encoding.UTF8); break;
                            case (0x0D): File.AppendAllText(path, "Ymax", Encoding.UTF8); break;
                            case (0x0E): File.AppendAllText(path, "Tmin", Encoding.UTF8); break;
                            case (0x0F): File.AppendAllText(path, "Tmax", Encoding.UTF8); break;
                            case (0x10): File.AppendAllText(path, "θMin", Encoding.UTF8); break;
                            case (0x11): File.AppendAllText(path, "θMax", Encoding.UTF8); break;
                            case (0x12): File.AppendAllText(path, "ZXmin", Encoding.UTF8); break;
                            case (0x13): File.AppendAllText(path, "ZXmax", Encoding.UTF8); break;
                            case (0x14): File.AppendAllText(path, "ZYmin", Encoding.UTF8); break;
                            case (0x15): File.AppendAllText(path, "ZYmax", Encoding.UTF8); break;
                            case (0x16): File.AppendAllText(path, "Zθmin", Encoding.UTF8); break;
                            case (0x17): File.AppendAllText(path, "Zθmax", Encoding.UTF8); break;
                            case (0x18): File.AppendAllText(path, "ZTmin", Encoding.UTF8); break;
                            case (0x19): File.AppendAllText(path, "ZTmax", Encoding.UTF8); break;
                            case (0x1A): File.AppendAllText(path, "TblStart", Encoding.UTF8); break;
                            case (0x1B): File.AppendAllText(path, "PlotStart", Encoding.UTF8); break;
                            case (0x1C): File.AppendAllText(path, "ZPlotStart", Encoding.UTF8); break;
                            case (0x1D): File.AppendAllText(path, "nMax", Encoding.UTF8); break;
                            case (0x1E): File.AppendAllText(path, "ZnMax", Encoding.UTF8); break;
                            case (0x1F): File.AppendAllText(path, "nMin", Encoding.UTF8); break;
                            case (0x20): File.AppendAllText(path, "ZnMin", Encoding.UTF8); break;
                            case (0x21): File.AppendAllText(path, "∆Tbl", Encoding.UTF8); break;
                            case (0x22): File.AppendAllText(path, "Tstep", Encoding.UTF8); break;
                            case (0x23): File.AppendAllText(path, "θstep", Encoding.UTF8); break;
                            case (0x24): File.AppendAllText(path, "ZTstep", Encoding.UTF8); break;
                            case (0x25): File.AppendAllText(path, "Zθstep", Encoding.UTF8); break;
                            case (0x26): File.AppendAllText(path, "∆X", Encoding.UTF8); break;
                            case (0x27): File.AppendAllText(path, "∆Y", Encoding.UTF8); break;
                            case (0x28): File.AppendAllText(path, "XFact", Encoding.UTF8); break;
                            case (0x29): File.AppendAllText(path, "YFact", Encoding.UTF8); break;
                            case (0x2A): File.AppendAllText(path, "TblInput", Encoding.UTF8); break;
                            case (0x2B): File.AppendAllText(path, "|N", Encoding.UTF8); break;
                            case (0x2C): File.AppendAllText(path, "I%", Encoding.UTF8); break;
                            case (0x2D): File.AppendAllText(path, "PV", Encoding.UTF8); break;
                            case (0x2E): File.AppendAllText(path, "PMT", Encoding.UTF8); break;
                            case (0x2F): File.AppendAllText(path, "FV", Encoding.UTF8); break;
                            case (0x30): File.AppendAllText(path, "|P/Y", Encoding.UTF8); break;
                            case (0x31): File.AppendAllText(path, "|C/Y", Encoding.UTF8); break;
                            case (0x32): File.AppendAllText(path, "w(nMin)", Encoding.UTF8); break;
                            case (0x33): File.AppendAllText(path, "Zw(nMin)", Encoding.UTF8); break;
                            case (0x34): File.AppendAllText(path, "PlotStep", Encoding.UTF8); break;
                            case (0x35): File.AppendAllText(path, "ZPlotStep", Encoding.UTF8); break;
                            case (0x36): File.AppendAllText(path, "Xres", Encoding.UTF8); break;
                            case (0x37): File.AppendAllText(path, "ZXres", Encoding.UTF8); break;
                            default:
                                Console.WriteLine("Error Decompiling: Invalid token: 0x" + buffer[i++].ToString("X2")); break;
                        }
                        break;
                    case 0x5E:
                        switch (buffer[i++])
                        {
                            case (0x10): File.AppendAllText(path, "Y₁", Encoding.UTF8); break;
                            case (0x11): File.AppendAllText(path, "Y₂", Encoding.UTF8); break;
                            case (0x12): File.AppendAllText(path, "Y₃", Encoding.UTF8); break;
                            case (0x13): File.AppendAllText(path, "Y₄", Encoding.UTF8); break;
                            case (0x14): File.AppendAllText(path, "Y₅", Encoding.UTF8); break;
                            case (0x15): File.AppendAllText(path, "Y₆", Encoding.UTF8); break;
                            case (0x16): File.AppendAllText(path, "Y₇", Encoding.UTF8); break;
                            case (0x17): File.AppendAllText(path, "Y₈", Encoding.UTF8); break;
                            case (0x18): File.AppendAllText(path, "Y₉", Encoding.UTF8); break;
                            case (0x19): File.AppendAllText(path, "Y₀", Encoding.UTF8); break;
                            case (0x20): File.AppendAllText(path, "X₁ᴛ", Encoding.UTF8); break;
                            case (0x21): File.AppendAllText(path, "Y₁ᴛ", Encoding.UTF8); break;
                            case (0x22): File.AppendAllText(path, "X₂ᴛ", Encoding.UTF8); break;
                            case (0x23): File.AppendAllText(path, "Y₂ᴛ", Encoding.UTF8); break;
                            case (0x24): File.AppendAllText(path, "X₃ᴛ", Encoding.UTF8); break;
                            case (0x25): File.AppendAllText(path, "Y₃ᴛ", Encoding.UTF8); break;
                            case (0x26): File.AppendAllText(path, "X₄ᴛ", Encoding.UTF8); break;
                            case (0x27): File.AppendAllText(path, "Y₄ᴛ", Encoding.UTF8); break;
                            case (0x28): File.AppendAllText(path, "X₅ᴛ", Encoding.UTF8); break;
                            case (0x29): File.AppendAllText(path, "Y₅ᴛ", Encoding.UTF8); break;
                            case (0x2A): File.AppendAllText(path, "X₆ᴛ", Encoding.UTF8); break;
                            case (0x2B): File.AppendAllText(path, "Y₆ᴛ", Encoding.UTF8); break;
                            case (0x40): File.AppendAllText(path, "r₁", Encoding.UTF8); break;
                            case (0x41): File.AppendAllText(path, "r₂", Encoding.UTF8); break;
                            case (0x42): File.AppendAllText(path, "r₃", Encoding.UTF8); break;
                            case (0x43): File.AppendAllText(path, "r₄", Encoding.UTF8); break;
                            case (0x44): File.AppendAllText(path, "r₅", Encoding.UTF8); break;
                            case (0x45): File.AppendAllText(path, "r₆", Encoding.UTF8); break;
                            case (0x80): File.AppendAllText(path, "|u", Encoding.UTF8); break;
                            case (0x81): File.AppendAllText(path, "|v", Encoding.UTF8); break;
                            case (0x82): File.AppendAllText(path, "|w", Encoding.UTF8); break;
                            default:
                                Console.WriteLine("Error Decompiling: Invalid token: 0x" + buffer[i++].ToString("X2")); break;
                        }
                        break;
                    case 0x5D:
                        switch (buffer[i++])
                        {
                            case (0x00): File.AppendAllText(path, "L₁", Encoding.UTF8); break;
                            case (0x01): File.AppendAllText(path, "L₂", Encoding.UTF8); break;
                            case (0x02): File.AppendAllText(path, "L₃", Encoding.UTF8); break;
                            case (0x03): File.AppendAllText(path, "L₄", Encoding.UTF8); break;
                            case (0x04): File.AppendAllText(path, "L₅", Encoding.UTF8); break;
                            case (0x05): File.AppendAllText(path, "L₆", Encoding.UTF8); break;
                            default:
                                Console.WriteLine("Error Decompiling: Invalid token: 0x" + buffer[i++].ToString("X2")); break;
                        }
                        break;
                    case 0x5C:
                        switch (buffer[i++])
                        {
                            case (0x00): File.AppendAllText(path, "[A]", Encoding.UTF8); break;
                            case (0x01): File.AppendAllText(path, "[B]", Encoding.UTF8); break;
                            case (0x02): File.AppendAllText(path, "[C]", Encoding.UTF8); break;
                            case (0x03): File.AppendAllText(path, "[D]", Encoding.UTF8); break;
                            case (0x04): File.AppendAllText(path, "[E]", Encoding.UTF8); break;
                            case (0x05): File.AppendAllText(path, "[F]", Encoding.UTF8); break;
                            case (0x06): File.AppendAllText(path, "[G]", Encoding.UTF8); break;
                            case (0x07): File.AppendAllText(path, "[H]", Encoding.UTF8); break;
                            case (0x08): File.AppendAllText(path, "[I]", Encoding.UTF8); break;
                            case (0x09): File.AppendAllText(path, "[J]", Encoding.UTF8); break;
                            default:
                                Console.WriteLine("Error Decompiling: Invalid token: 0x" + buffer[i++].ToString("X2")); break;
                        }
                        break;
                    default:
                        File.AppendAllText(path, Tokens[buffer[i]], Encoding.UTF8);
                        break;
                }
            }
            Console.WriteLine("Wrote " + size + " bytes to destination.");
        }

        private static string BytesArrayToString(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", " ");
        }
    }
}
