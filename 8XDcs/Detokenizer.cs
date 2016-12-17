using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8XDcs
{
    class Detokenizer
    {
        //public static FileStream dest;

        private static string path = "";
        public static Dictionary<UInt32, string> Tokens = new Dictionary<UInt32, string>()
        {
            {0x00, " "},
            {0x01, "►DMS"},
            {0x02, "►Dec"},
            {0x03, "►Frac"},
            {0x04, "→"},
            {0x05, "Boxplot"},
            {0x06, "["},
            {0x07, "]"},
            {0x08, "{"},
            {0x09, "}"},
            {0x0A, "ʳ"},
            {0x0B, "°"},
            {0x0C, "ˉ¹"},
            {0x0D, "²"},
            {0x0E, "ᵀ"},
            {0x0F, "³"},
            {0x10, "("},
            {0x11, ")"},
            {0x12, "round("},
            {0x13, "pxl-Test("},
            {0x14, "augment("},
            {0x15, "rowSwap("},
            {0x16, "row+("},
            {0x17, "*row("},
            {0x18, "*row+("},
            {0x19, "max("},
            {0x1A, "min("},
            {0x1B, "R►Pr("},
            {0x1C, "R►Pθ("},
            {0x1D, "P►Rx("},
            {0x1E, "P►Ry("},
            {0x1F, "median("},
            {0x20, "randM("},
            {0x21, "mean("},
            {0x22, "solve("},
            {0x23, "seq("},
            {0x24, "fnInt("},
            {0x25, "nDeriv("},
            {0x27, "fMin("},
            {0x28, "fMax("},
            {0x29, " "},
            {0x2A, "\""},
            {0x2B, ","},
            {0x2C, "[i]"},
            {0x2D, "!"},
            {0x2E, "CubicReg "},
            {0x2F, "QuartReg "},
            {0x30, "0"},
            {0x31, "1"},
            {0x32, "2"},
            {0x33, "3"},
            {0x34, "4"},
            {0x35, "5"},
            {0x36, "6"},
            {0x37, "7"},
            {0x38, "8"},
            {0x39, "9"},
            {0x3A, "."},
            {0x3B, "ᴇ"},
            {0x3C, " or "},
            {0x3D, " xor "},
            {0x3E, ":"},
            {0x3F, "\n"},
            {0x40, " and "},
            {0x41, "A"},
            {0x42, "B"},
            {0x43, "C"},
            {0x44, "D"},
            {0x45, "E"},
            {0x46, "F"},
            {0x47, "G"},
            {0x48, "H"},
            {0x49, "I"},
            {0x4A, "J"},
            {0x4B, "K"},
            {0x4C, "L"},
            {0x4D, "M"},
            {0x4E, "N"},
            {0x4F, "O"},
            {0x50, "P"},
            {0x51, "Q"},
            {0x52, "R"},
            {0x53, "S"},
            {0x54, "T"},
            {0x55, "U"},
            {0x56, "V"},
            {0x57, "W"},
            {0x58, "X"},
            {0x59, "Y"},
            {0x5A, "Z"},
            {0x5B, "θ"},
            {0x7F, "plotsquare"},
            {0x80, "﹢"},
            {0x81, "·"},
            {0x82, "*"},
            {0x83, "/"},
            {0x84, "Trace"},
            {0x85, "ClrDraw"},
            {0x86, "ZStandard"},
            {0x87, "ZTrig"},
            {0x88, "ZBox"},
            {0x89, "Zoom In"},
            {0x8A, "Zoom Out"},
            {0x8B, "ZSquare"},
            {0x8C, "ZInteger"},
            {0x8D, "ZPrevious"},
            {0x8E, "ZDecimal"},
            {0x8F, "ZoomStat"},
            {0x90, "ZoomRcl"},
            {0x91, "PrintScreen"},
            {0x92, "ZoomSto"},
            {0x93, "Text("},
            {0x94, " nPr "},
            {0x95, " nCr "},
            {0x96, "FnOn "},
            {0x97, "FnOff "},
            {0x98, "StorePic "},
            {0x99, "RecallPic "},
            {0x9A, "StoreGDB "},
            {0x9B, "RecallGDB "},
            {0x9C, "Line("},
            {0x9D, "Vertical "},
            {0x9E, "Pt-On("},
            {0x9F, "Pt-Off("},
            {0xA0, "Pt-Change("},
            {0xA1, "Pxl-On("},
            {0xA2, "Pxl-Off("},
            {0xA3, "Pxl-Change("},
            {0xA4, "Shade("},
            {0xA5, "Circle("},
            {0xA6, "Horizontal "},
            {0xA7, "Tangent("},
            {0xA8, "DrawInv "},
            {0xA9, "DrawF "},
            {0xAB, "rand"},
            {0xAC, "π"},
            {0xAD, "getKey"},
            {0xAE, "'"},
            {0xAF, "?"},
            {0xB0, "⁻"},
            {0xB1, "int("},
            {0xB2, "abs("},
            // Yeah. Bullshit. {0xB3, "det("},
            {0xB4, "identity("},
            {0xB5, "dim("},
            {0xB6, "sum("},
            {0xB7, "prod("},
            {0xB8, "not("},
            {0xB9, "iPart("},
            {0xBA, "fPart("},
            {0xBC, "√("},
            {0xBD, "³√("},
            {0xBE, "ln("},
            {0xBF, "e^("},
            {0xC0, "log("},
            {0xC1, "₁₀^("},
            {0xC2, "sin("},
            {0xC3, "sin⁻¹("},
            {0xC4, "cos("},
            {0xC5, "cos⁻¹("},
            {0xC6, "tan("},
            {0xC7, "tan⁻¹("},
            {0xC8, "sinh("},
            {0xC9, "sinh⁻¹("},
            {0xCA, "cosh("},
            {0xCB, "soch⁻¹("},
            {0xCC, "tanh("},
            {0xCD, "tanh⁻¹("},
            {0xCE, "If "},
            {0xCF, "Then"},
            {0xD0, "Else"},
            {0xD1, "While "},
            {0xD2, "Repeat "},
            {0xD3, "For("},
            {0xD4, "End"},
            {0xD5, "Return"},
            {0xD6, "Lbl "},
            {0xD7, "Goto "},
            {0xD8, "Pause "},
            {0xD9, "Stop"},
            {0xDA, "IS>("},
            {0xDB, "DS<("},
            {0xDC, "Input "},
            {0xDD, "Prompt "},
            {0xDE, "Disp "},
            {0xDF, "DispGraph"},
            {0xE0, "Output("},
            {0xE1, "ClrHome"},
            {0xE2, "Fill("},
            {0xE3, "SortA("},
            {0xE4, "SortD("},
            {0xE5, "DispTable"},
            {0xE6, "Menu("},
            {0xE7, "Send("},
            {0xE8, "Get("},
            {0xE9, "PlotsOn "},
            {0xEA, "PlotsOff "},
            {0xEB, "ʟ"},
            {0xEC, "Plot1("},
            {0xED, "Plot2("},
            {0xEE, "Plot3("},
            {0xF0, "^"},
            {0xF1, "ˣ√"},
            {0xF2, "1-Var Stats "},
            {0xF3, "2-Var Stats"},
            {0xF4, "LinReg(a+bx) "},
            {0xF5, "ExpReg "},
            {0xF6, "LnReg "},
            {0xF7, "PwrReg "},
            {0xF8, "Med-Med "},
            {0xF9, "QuadReg "},
            {0xFA, "ClrList "},
            {0xFB, "ClrTable"},
            {0xFC, "Histogram"},
            {0xFD, "xyLine"},
            {0xFE, "Scatter"},
            {0xFF, "LinReg(ax+b)"}
        };

        public static IEnumerable<byte> Open(string path)
        {
            IEnumerable<byte> filebuf = File.ReadAllBytes(path);

            if (filebuf == null)
            {
                Console.WriteLine("That file does not exist or was spelled incorrectly.");
                return null;
            }

            // Check the header
            IEnumerable<byte> header = filebuf.Take(8);

            return filebuf;
        }

        public static string GetDataSection(byte[] fb)
        {
            IEnumerable<byte> dataSection = fb.Take(75); // Data section begins on 75th byte of the file
            return BytesArrayToString(dataSection.ToArray());
        }

        public static void OpenDest()
        {
            path = RandomString(5) + ".txt";
            FileStream dest = File.Open(path, FileMode.Create);
            Console.WriteLine("Created new file for output.");
            dest.Close();
        }

        private static void AppendToSource(string t)
        {
            using (StreamWriter pen = File.AppendText(path))
            {
                pen.Write(t);
                pen.Close();
            }
        }

        private static Random random = new Random();
        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static void Detokenize(byte[] tokens)
        {
            if (tokens == null)
            {
                Console.WriteLine("Error: Buffer Empty");
                return;
            }

            for (int i = 0; i < tokens.Length; i++)
            {
                switch (tokens[i])
                {
                    case 0xEF:
                        switch (tokens[i++])
                        {
                            case (0x00): AppendToSource("setDate("); break;
                            case (0x01): AppendToSource("setTime("); break;
                            case (0x02): AppendToSource("checkTmr("); break;
                            case (0x03): AppendToSource("setDtFmt("); break;
                            case (0x04): AppendToSource("setTmFmt("); break;
                            case (0x05): AppendToSource("timeCnv("); break;
                            case (0x06): AppendToSource("dayOfWk("); break;
                            case (0x07): AppendToSource("getDtStr"); break;
                            case (0x08): AppendToSource("getTmStr("); break;
                            case (0x09): AppendToSource("getDate"); break;
                            case (0x0A): AppendToSource("getTime"); break;
                            case (0x0B): AppendToSource("startTmr"); break;
                            case (0x0C): AppendToSource("getDtFmt"); break;
                            case (0x0D): AppendToSource("getTmFmt"); break;
                            case (0x0E): AppendToSource("isClockOn"); break;
                            case (0x0F): AppendToSource("ClockOff"); break;
                            case (0x10): AppendToSource("ClockOn"); break;
                            case (0x11): AppendToSource("OpenLib("); break;
                            case (0x12): AppendToSource("ExecLib"); break;
                            case (0x13): AppendToSource("invT("); break;
                            case (0x14): AppendToSource("χ²GOF-Test("); break;
                            case (0x15): AppendToSource("LinRegTInt "); break;
                            case (0x16): AppendToSource("Manual-Fit "); break;
                            case (0x17): AppendToSource("ZQuadrant1"); break;
                            case (0x18): AppendToSource("ZFrac1/2"); break;
                            case (0x19): AppendToSource("ZFrac1/3"); break;
                            case (0x1A): AppendToSource("ZFrac1/4"); break;
                            case (0x1B): AppendToSource("ZFrac1/5"); break;
                            case (0x1C): AppendToSource("ZFrac1/8"); break;
                            case (0x1D): AppendToSource("ZFrac1/10"); break;
                            case (0x1E): AppendToSource("mathprintbox"); break;
                            case (0x2E): AppendToSource("⁄"); break;
                            case (0x2F): AppendToSource("ᵤ"); break;
                            case (0x30): AppendToSource("►n⁄d◄►Un⁄d"); break;
                            case (0x31): AppendToSource("►F◄►D"); break;
                            case (0x32): AppendToSource("remainder("); break;
                            case (0x33): AppendToSource("Σ("); break;
                            case (0x34): AppendToSource("logBASE("); break;
                            case (0x35): AppendToSource("randIntNoRep("); break;
                            case (0x37): AppendToSource("[MATHPRINT]"); break;
                            case (0x38): AppendToSource("[CLASSIC]"); break;
                            case (0x39): AppendToSource("n⁄d"); break;
                            case (0x3A): AppendToSource("Un⁄d"); break;
                            case (0x3B): AppendToSource("[AUTO]"); break;
                            case (0x3C): AppendToSource("[DEC]"); break;
                            case (0x3D): AppendToSource("[FRAC]"); break;
                            case (0x41): AppendToSource("BLUE"); break;
                            case (0x42): AppendToSource("RED"); break;
                            case (0x43): AppendToSource("BLACK"); break;
                            case (0x44): AppendToSource("MAGENTA"); break;
                            case (0x45): AppendToSource("GREEN"); break;
                            case (0x46): AppendToSource("ORANGE"); break;
                            case (0x47): AppendToSource("BROWN"); break;
                            case (0x48): AppendToSource("NAVY"); break;
                            case (0x49): AppendToSource("LTBLUE"); break;
                            case (0x4A): AppendToSource("YELLOW"); break;
                            case (0x4B): AppendToSource("WHITE"); break;
                            case (0x4C): AppendToSource("LTGREY"); break;
                            case (0x4D): AppendToSource("MEDGREY"); break;
                            case (0x4E): AppendToSource("GREY"); break;
                            case (0x4F): AppendToSource("DARKGREY"); break;
                            case (0x5A): AppendToSource("GridLine "); break;
                            case (0x5B): AppendToSource("BackgroundOn "); break;
                            case (0x6A): AppendToSource("DetectAsymOn"); break;
                            case (0x6B): AppendToSource("DetectAsymOff"); break;
                            case (0x64): AppendToSource("BackgroundOff"); break;
                            case (0x65): AppendToSource("GraphColor("); break;
                            case (0x67): AppendToSource("TextColor("); break;
                            case (0x68): AppendToSource("Asm84CPrgm"); break;
                            case (0x6C): AppendToSource("BorderColor "); break;
                            case (0x73): AppendToSource("tinydotplot"); break;
                            case (0x74): AppendToSource("Thin"); break;
                            case (0x75): AppendToSource("Dot-Thin"); break;
                            default:
                                Console.WriteLine("Error Decompiling: Invalid token."); break;
                        }
                        break;

                    case 0xAA:
                        switch (tokens[i++])
                        {
                            case (0x00): AppendToSource("Str1"); break;
                            case (0x01): AppendToSource("Str2"); break;
                            case (0x02): AppendToSource("Str3"); break;
                            case (0x03): AppendToSource("Str4"); break;
                            case (0x04): AppendToSource("Str5"); break;
                            case (0x05): AppendToSource("Str6"); break;
                            case (0x06): AppendToSource("Str7"); break;
                            case (0x07): AppendToSource("Str8"); break;
                            case (0x08): AppendToSource("Str9"); break;
                            case (0x09): AppendToSource("Str0"); break;
                            default:
                                Console.WriteLine("Error Decompiling: Invalid token."); break;
                        }
                        break;
                    case 0xBB:
                        switch (tokens[i++])
                        {
                            case (0x00): AppendToSource("npv("); break;
                            case (0x01): AppendToSource("irr("); break;
                            case (0x02): AppendToSource("bal("); break;
                            case (0x03): AppendToSource("ΣPrn("); break;
                            case (0x04): AppendToSource("ΣInt("); break;
                            case (0x05): AppendToSource("►Nom("); break;
                            case (0x06): AppendToSource("►Eff("); break;
                            case (0x07): AppendToSource("dbd("); break;
                            case (0x08): AppendToSource("lcm("); break;
                            case (0x09): AppendToSource("gcd("); break;
                            case (0x0A): AppendToSource("randInt("); break;
                            case (0x0B): AppendToSource("randBin("); break;
                            case (0x0C): AppendToSource("sub("); break;
                            case (0x0D): AppendToSource("stdDev("); break;
                            case (0x0E): AppendToSource("variance("); break;
                            case (0x0F): AppendToSource("inString("); break;
                            case (0x10): AppendToSource("normalcdf("); break;
                            case (0x11): AppendToSource("invNorm("); break;
                            case (0x12): AppendToSource("tcdf("); break;
                            case (0x13): AppendToSource("χ²cdf("); break;
                            case (0x14): AppendToSource("Fcdf("); break;
                            case (0x15): AppendToSource("binompdf("); break;
                            case (0x16): AppendToSource("binomcdf("); break;
                            case (0x17): AppendToSource("poissonpdf("); break;
                            case (0x18): AppendToSource("poissoncdf("); break;
                            case (0x19): AppendToSource("geometpdf("); break;
                            case (0x1A): AppendToSource("geometcdf("); break;
                            case (0x1B): AppendToSource("normalpdf("); break;
                            case (0x1C): AppendToSource("tpdf("); break;
                            case (0x1D): AppendToSource("χ²pdf("); break;
                            case (0x1E): AppendToSource("Fpdf("); break;
                            case (0x1F): AppendToSource("randNorm("); break;
                            case (0x20): AppendToSource("tvm_Pmt"); break;
                            case (0x21): AppendToSource("tvm_I%"); break;
                            case (0x22): AppendToSource("tvm_PV"); break;
                            case (0x23): AppendToSource("tvm_N"); break;
                            case (0x24): AppendToSource("tvm_FV"); break;
                            case (0x25): AppendToSource("conj("); break;
                            case (0x26): AppendToSource("real("); break;
                            case (0x27): AppendToSource("imag("); break;
                            case (0x28): AppendToSource("angle("); break;
                            case (0x29): AppendToSource("cumSum("); break;
                            case (0x2A): AppendToSource("expr("); break;
                            case (0x2B): AppendToSource("length("); break;
                            case (0x2C): AppendToSource("DeltaList("); break;
                            case (0x2D): AppendToSource("ref("); break;
                            case (0x2E): AppendToSource("rref("); break;
                            case (0x2F): AppendToSource("►Rect"); break;
                            case (0x30): AppendToSource("►Polar"); break;
                            case (0x31): AppendToSource("[e]"); break;
                            case (0x32): AppendToSource("SinReg "); break;
                            case (0x33): AppendToSource("Logistic "); break;
                            case (0x34): AppendToSource("LinRegTTest "); break;
                            case (0x35): AppendToSource("ShadeNorm("); break;
                            case (0x36): AppendToSource("Shade_t("); break;
                            case (0x37): AppendToSource("Shadeχ²("); break;
                            case (0x38): AppendToSource("ShadeF("); break;
                            case (0x39): AppendToSource("Matr►list("); break;
                            case (0x3A): AppendToSource("List►matr("); break;
                            case (0x3B): AppendToSource("Z-Test("); break;
                            case (0x3C): AppendToSource("T-Test "); break;
                            case (0x3D): AppendToSource("2-SampZTest("); break;
                            case (0x3E): AppendToSource("1-PropZTest("); break;
                            case (0x3F): AppendToSource("2-PropZTest("); break;
                            case (0x40): AppendToSource("χ²-Test("); break;
                            case (0x41): AppendToSource("ZInterval"); break;
                            case (0x42): AppendToSource("2-SampZInt("); break;
                            case (0x43): AppendToSource("1-PropZInt("); break;
                            case (0x44): AppendToSource("2-PropZInt("); break;
                            case (0x45): AppendToSource("GraphStyle("); break;
                            case (0x46): AppendToSource("2-SampTTest "); break;
                            case (0x47): AppendToSource("2-SampFTest "); break;
                            case (0x48): AppendToSource("TInterval "); break;
                            case (0x49): AppendToSource("2-SampTInt "); break;
                            case (0x4A): AppendToSource("SetUpEditor "); break;
                            case (0x4B): AppendToSource("Pmt_End"); break;
                            case (0x4C): AppendToSource("Pmt_Bgn"); break;
                            case (0x4D): AppendToSource("Real"); break;
                            case (0x4E): AppendToSource("re^θi"); break;
                            case (0x4F): AppendToSource("a+bi"); break;
                            case (0x50): AppendToSource("ExprOn"); break;
                            case (0x51): AppendToSource("ExprOff"); break;
                            case (0x52): AppendToSource("ClrAllLists"); break;
                            case (0x53): AppendToSource("GetCalc("); break;
                            case (0x54): AppendToSource("DelVar "); break;
                            case (0x55): AppendToSource("Equ►String("); break;
                            case (0x56): AppendToSource("String►Equ("); break;
                            case (0x57): AppendToSource("Clear Entries"); break;
                            case (0x58): AppendToSource("Select("); break;
                            case (0x59): AppendToSource("ANOVA("); break;
                            case (0x5A): AppendToSource("ModBoxPlot"); break;
                            case (0x5B): AppendToSource("NormProbPlot"); break;
                            case (0x64): AppendToSource("G-T"); break;
                            case (0x65): AppendToSource("ZoomFit"); break;
                            case (0x66): AppendToSource("DiagnosticOn"); break;
                            case (0x67): AppendToSource("DiagnosticOff"); break;
                            case (0x68): AppendToSource("Archive "); break;
                            case (0x69): AppendToSource("UnArchive "); break;
                            case (0x6A): AppendToSource("Asm("); break;
                            case (0x6B): AppendToSource("AsmComp("); break;
                            case (0x6C): AppendToSource("AsmPrgm"); break;
                            case (0x6E): AppendToSource("Á"); break;
                            case (0x6F): AppendToSource("À"); break;
                            case (0x70): AppendToSource("Â"); break;
                            case (0x71): AppendToSource("Ä"); break;
                            case (0x72): AppendToSource("á"); break;
                            case (0x73): AppendToSource("à"); break;
                            case (0x74): AppendToSource("â"); break;
                            case (0x75): AppendToSource("ä"); break;
                            case (0x76): AppendToSource("É"); break;
                            case (0x77): AppendToSource("È"); break;
                            case (0x78): AppendToSource("Ê"); break;
                            case (0x79): AppendToSource("Ë"); break;
                            case (0x7A): AppendToSource("é"); break;
                            case (0x7B): AppendToSource("è"); break;
                            case (0x7C): AppendToSource("ê"); break;
                            case (0x7D): AppendToSource("ë"); break;
                            case (0x7F): AppendToSource("Ì"); break;
                            case (0x80): AppendToSource("Î"); break;
                            case (0x81): AppendToSource("Ï"); break;
                            case (0x82): AppendToSource("í"); break;
                            case (0x83): AppendToSource("ì"); break;
                            case (0x84): AppendToSource("î"); break;
                            case (0x85): AppendToSource("ï"); break;
                            case (0x86): AppendToSource("Ó"); break;
                            case (0x87): AppendToSource("Ò"); break;
                            case (0x88): AppendToSource("Ô"); break;
                            case (0x89): AppendToSource("Ö"); break;
                            case (0x8A): AppendToSource("ó"); break;
                            case (0x8B): AppendToSource("ò"); break;
                            case (0x8C): AppendToSource("ô"); break;
                            case (0x8D): AppendToSource("ö"); break;
                            case (0x8E): AppendToSource("Ú"); break;
                            case (0x8F): AppendToSource("Ù"); break;
                            case (0x90): AppendToSource("Û"); break;
                            case (0x91): AppendToSource("Ü"); break;
                            case (0x92): AppendToSource("ú"); break;
                            case (0x93): AppendToSource("ù"); break;
                            case (0x94): AppendToSource("û"); break;
                            case (0x95): AppendToSource("ü"); break;
                            case (0x96): AppendToSource("Ç"); break;
                            case (0x97): AppendToSource("ç"); break;
                            case (0x98): AppendToSource("Ñ"); break;
                            case (0x99): AppendToSource("ñ"); break;
                            case (0x9A): AppendToSource("´"); break;
                            case (0x9B): AppendToSource("|`"); break;
                            case (0x9C): AppendToSource("¨"); break;
                            case (0x9D): AppendToSource("¿"); break;
                            case (0x9E): AppendToSource("¡"); break;
                            case (0x9F): AppendToSource("α"); break;
                            case (0xA0): AppendToSource("β"); break;
                            case (0xA1): AppendToSource("γ"); break;
                            case (0xA2): AppendToSource("Δ"); break;
                            case (0xA3): AppendToSource("δ"); break;
                            case (0xA4): AppendToSource("ε"); break;
                            case (0xA5): AppendToSource("λ"); break;
                            case (0xA6): AppendToSource("μ"); break;
                            case (0xA7): AppendToSource("|π"); break;
                            case (0xA8): AppendToSource("ρ"); break;
                            case (0xA9): AppendToSource("Σ"); break;
                            case (0xAB): AppendToSource("Φ"); break;
                            case (0xAC): AppendToSource("Ω"); break;
                            case (0xAD): AppendToSource("ṗ"); break;
                            case (0xAE): AppendToSource("χ"); break;
                            case (0xAF): AppendToSource("|F"); break;
                            case (0xB0): AppendToSource("a"); break;
                            case (0xB1): AppendToSource("b"); break;
                            case (0xB2): AppendToSource("c"); break;
                            case (0xB3): AppendToSource("d"); break;
                            case (0xB4): AppendToSource("e"); break;
                            case (0xB5): AppendToSource("f"); break;
                            case (0xB6): AppendToSource("g"); break;
                            case (0xB7): AppendToSource("h"); break;
                            case (0xB8): AppendToSource("i"); break;
                            case (0xB9): AppendToSource("j"); break;
                            case (0xBA): AppendToSource("k"); break;
                            case (0xBC): AppendToSource("l"); break;
                            case (0xBD): AppendToSource("m"); break;
                            case (0xBE): AppendToSource("n"); break;
                            case (0xBF): AppendToSource("o"); break;
                            case (0xC0): AppendToSource("p"); break;
                            case (0xC1): AppendToSource("q"); break;
                            case (0xC2): AppendToSource("r"); break;
                            case (0xC3): AppendToSource("s"); break;
                            case (0xC4): AppendToSource("t"); break;
                            case (0xC5): AppendToSource("u"); break;
                            case (0xC6): AppendToSource("v"); break;
                            case (0xC7): AppendToSource("w"); break;
                            case (0xC8): AppendToSource("x"); break;
                            case (0xC9): AppendToSource("y"); break;
                            case (0xCA): AppendToSource("z"); break;
                            case (0xCB): AppendToSource("σ"); break;
                            case (0xCC): AppendToSource("τ"); break;
                            case (0xCD): AppendToSource("Í"); break;
                            case (0xCE): AppendToSource("GarbageCollect"); break;
                            case (0xCF): AppendToSource("|~"); break;
                            case (0xD1): AppendToSource("@"); break;
                            case (0xD2): AppendToSource("#"); break;
                            case (0xD3): AppendToSource("$"); break;
                            case (0xD4): AppendToSource("&"); break;
                            case (0xD5): AppendToSource("`"); break;
                            case (0xD6): AppendToSource(";"); break;
                            case (0xD7): AppendToSource("\\"); break;
                            case (0xD8): AppendToSource("|"); break;
                            case (0xD9): AppendToSource("_"); break;
                            case (0xDA): AppendToSource("%"); break;
                            case (0xDB): AppendToSource("…"); break;
                            case (0xDC): AppendToSource("∠"); break;
                            case (0xDD): AppendToSource("ß"); break;
                            case (0xDE): AppendToSource("ˣ"); break;
                            case (0xDF): AppendToSource("ᴛ"); break;
                            case (0xE0): AppendToSource("₀"); break;
                            case (0xE1): AppendToSource("₁"); break;
                            case (0xE2): AppendToSource("₂"); break;
                            case (0xE3): AppendToSource("₃"); break;
                            case (0xE4): AppendToSource("₄"); break;
                            case (0xE5): AppendToSource("₅"); break;
                            case (0xE6): AppendToSource("₆"); break;
                            case (0xE7): AppendToSource("₇"); break;
                            case (0xE8): AppendToSource("₈"); break;
                            case (0xE9): AppendToSource("₉"); break;
                            case (0xEA): AppendToSource("₁₀"); break;
                            case (0xEB): AppendToSource("◄"); break;
                            case (0xEC): AppendToSource("►"); break;
                            case (0xED): AppendToSource("↑"); break;
                            case (0xEE): AppendToSource("↓"); break;
                            case (0xF0): AppendToSource("×"); break;
                            case (0xF1): AppendToSource("∫"); break;
                            case (0xF2): AppendToSource("bolduparrow"); break;
                            case (0xF3): AppendToSource("bolddownarrow"); break;
                            case (0xF4): AppendToSource("√"); break;
                            case (0xF5): AppendToSource("invertedequal"); break;
                            default:
                                Console.WriteLine("Error Decompiling: Invalid token."); break;
                        }
                        break;
                    case 0xB3:
                        switch (tokens[i++])
                        {
                            case (0x32): AppendToSource("InsertLine("); break;
                            case (0x33): AppendToSource("SpecialChars("); break;
                            case (0x34): AppendToSource("CreateVar("); break;
                            case (0x35): AppendToSource("ArcUnarcVar("); break;
                            case (0x36): AppendToSource("DeleteVar("); break;
                            case (0x37): AppendToSource("DeleteLine("); break;
                            case (0x38): AppendToSource("VarStatus("); break;
                            case (0x31): AppendToSource("ReplaceLine("); break;
                            case (0x30): AppendToSource("ReadLine("); break;
                            default:
                                // Fuckingbullshit
                                AppendToSource("det("); break;
                        }
                        break;
                    case 0x7E:
                        switch (tokens[i++])
                        {
                            case (0x00): AppendToSource("Sequential"); break;
                            case (0x01): AppendToSource("Simul"); break;
                            case (0x02): AppendToSource("PolarGC"); break;
                            case (0x03): AppendToSource("RectGC"); break;
                            case (0x04): AppendToSource("CoordOn"); break;
                            case (0x05): AppendToSource("CoordOff"); break;
                            case (0x06): AppendToSource("Thick"); break;
                            case (0x07): AppendToSource("Dot-Thick"); break;
                            case (0x08): AppendToSource("AxesOn"); break;
                            case (0x09): AppendToSource("AxesOff"); break;
                            case (0x0A): AppendToSource("GridDot "); break;
                            case (0x0B): AppendToSource("GridOff"); break;
                            case (0x0C): AppendToSource("LabelOn"); break;
                            case (0x0D): AppendToSource("LabelOff"); break;
                            case (0x0E): AppendToSource("Web"); break;
                            case (0x0F): AppendToSource("Time"); break;
                            case (0x10): AppendToSource("uvAxes"); break;
                            case (0x11): AppendToSource("vwAxes"); break;
                            case (0x12): AppendToSource("uwAxes"); break;
                            default:
                                Console.WriteLine("Error Decompiling: Invalid token."); break;
                        }
                        break;
                    case 0x60:
                        switch (tokens[i++])
                        {
                            case (0x00): AppendToSource("Pic1"); break;
                            case (0x01): AppendToSource("Pic2"); break;
                            case (0x02): AppendToSource("Pic3"); break;
                            case (0x03): AppendToSource("Pic4"); break;
                            case (0x04): AppendToSource("Pic5"); break;
                            case (0x05): AppendToSource("Pic6"); break;
                            case (0x06): AppendToSource("Pic7"); break;
                            case (0x07): AppendToSource("Pic8"); break;
                            case (0x08): AppendToSource("Pic9"); break;
                            case (0x09): AppendToSource("Pic0"); break;
                            default:
                                Console.WriteLine("Error Decompiling: Invalid token."); break;
                        }
                        break;
                    case 0x61:
                        switch (tokens[i++])
                        {
                            case (0x00): AppendToSource("GDB1"); break;
                            case (0x01): AppendToSource("GDB2"); break;
                            case (0x02): AppendToSource("GDB3"); break;
                            case (0x03): AppendToSource("GDB4"); break;
                            case (0x04): AppendToSource("GDB5"); break;
                            case (0x05): AppendToSource("GDB6"); break;
                            case (0x06): AppendToSource("GDB7"); break;
                            case (0x07): AppendToSource("GDB8"); break;
                            case (0x08): AppendToSource("GDB9"); break;
                            case (0x09): AppendToSource("GDB0"); break;
                            default:
                                Console.WriteLine("Error Decompiling: Invalid token."); break;
                        }
                        break;
                    case 0x62:
                        switch (tokens[i++])
                        {
                            case (0x01): AppendToSource("[RegEQ]"); break;
                            case (0x02): AppendToSource("[n]"); break;
                            case (0x03): AppendToSource("ẋ"); break;
                            case (0x04): AppendToSource("Σx"); break;
                            case (0x05): AppendToSource("Σx²"); break;
                            case (0x06): AppendToSource("[Sx]"); break;
                            case (0x07): AppendToSource("σx"); break;
                            case (0x08): AppendToSource("[minX]"); break;
                            case (0x09): AppendToSource("[maxX]"); break;
                            case (0x0A): AppendToSource("[minY]"); break;
                            case (0x0B): AppendToSource("[maxY]"); break;
                            case (0x0C): AppendToSource("ȳ"); break;
                            case (0x0D): AppendToSource("Σy"); break;
                            case (0x0E): AppendToSource("Σy²"); break;
                            case (0x0F): AppendToSource("[Sy]"); break;
                            case (0x10): AppendToSource("σy"); break;
                            case (0x11): AppendToSource("Σxy"); break;
                            case (0x12): AppendToSource("[r]"); break;
                            case (0x13): AppendToSource("[Med]"); break;
                            case (0x14): AppendToSource("[Q1]"); break;
                            case (0x15): AppendToSource("[Q3]"); break;
                            case (0x16): AppendToSource("[|a]"); break;
                            case (0x17): AppendToSource("[|b]"); break;
                            case (0x18): AppendToSource("[|c]"); break;
                            case (0x19): AppendToSource("[|d]"); break;
                            case (0x1A): AppendToSource("[|e]"); break;
                            case (0x1B): AppendToSource("x₁"); break;
                            case (0x1C): AppendToSource("x₂"); break;
                            case (0x1D): AppendToSource("x₃"); break;
                            case (0x1E): AppendToSource("y₁"); break;
                            case (0x1F): AppendToSource("y₂"); break;
                            case (0x20): AppendToSource("y₃"); break;
                            case (0x21): AppendToSource("[recursiven]"); break;
                            case (0x22): AppendToSource("[p]"); break;
                            case (0x23): AppendToSource("[z]"); break;
                            case (0x24): AppendToSource("[t]"); break;
                            case (0x25): AppendToSource("χ²"); break;
                            case (0x26): AppendToSource("[|F]"); break;
                            case (0x27): AppendToSource("[df]"); break;
                            case (0x28): AppendToSource("[ṗ]"); break;
                            case (0x29): AppendToSource("ṗ₁"); break;
                            case (0x2A): AppendToSource("ṗ₂"); break;
                            case (0x2B): AppendToSource("ẋ₁"); break;
                            case (0x2C): AppendToSource("Sx₁"); break;
                            case (0x2D): AppendToSource("n₁"); break;
                            case (0x2E): AppendToSource("ẋ₂"); break;
                            case (0x2F): AppendToSource("Sx₂"); break;
                            case (0x30): AppendToSource("n₂"); break;
                            case (0x31): AppendToSource("[Sxp]"); break;
                            case (0x32): AppendToSource("[lower]"); break;
                            case (0x33): AppendToSource("[upper]"); break;
                            case (0x34): AppendToSource("[s]"); break;
                            case (0x35): AppendToSource("r²"); break;
                            case (0x36): AppendToSource("R²"); break;
                            case (0x37): AppendToSource("[factordf]"); break;
                            case (0x38): AppendToSource("[factorSS]"); break;
                            case (0x39): AppendToSource("[factorMS]"); break;
                            case (0x3A): AppendToSource("[errordf]"); break;
                            case (0x3B): AppendToSource("[errorSS]"); break;
                            case (0x3C): AppendToSource("[errorMS]"); break;
                            default:
                                Console.WriteLine("Error Decompiling: Invalid token."); break;
                        }
                        break;
                    case 0x63:
                        switch (tokens[i++])
                        {
                            case (0x00): AppendToSource("ZXscl"); break;
                            case (0x01): AppendToSource("ZYscl"); break;
                            case (0x02): AppendToSource("Xscl"); break;
                            case (0x03): AppendToSource("Yscl"); break;
                            case (0x04): AppendToSource("u(nMin)"); break;
                            case (0x05): AppendToSource("v(nMin)"); break;
                            case (0x06): AppendToSource("Un-₁"); break;
                            case (0x07): AppendToSource("Vn-₁"); break;
                            case (0x08): AppendToSource("Zu(nmin)"); break;
                            case (0x09): AppendToSource("Zv(nmin)"); break;
                            case (0x0A): AppendToSource("Xmin"); break;
                            case (0x0B): AppendToSource("Xmax"); break;
                            case (0x0C): AppendToSource("Ymin"); break;
                            case (0x0D): AppendToSource("Ymax"); break;
                            case (0x0E): AppendToSource("Tmin"); break;
                            case (0x0F): AppendToSource("Tmax"); break;
                            case (0x10): AppendToSource("θMin"); break;
                            case (0x11): AppendToSource("θMax"); break;
                            case (0x12): AppendToSource("ZXmin"); break;
                            case (0x13): AppendToSource("ZXmax"); break;
                            case (0x14): AppendToSource("ZYmin"); break;
                            case (0x15): AppendToSource("ZYmax"); break;
                            case (0x16): AppendToSource("Zθmin"); break;
                            case (0x17): AppendToSource("Zθmax"); break;
                            case (0x18): AppendToSource("ZTmin"); break;
                            case (0x19): AppendToSource("ZTmax"); break;
                            case (0x1A): AppendToSource("TblStart"); break;
                            case (0x1B): AppendToSource("PlotStart"); break;
                            case (0x1C): AppendToSource("ZPlotStart"); break;
                            case (0x1D): AppendToSource("nMax"); break;
                            case (0x1E): AppendToSource("ZnMax"); break;
                            case (0x1F): AppendToSource("nMin"); break;
                            case (0x20): AppendToSource("ZnMin"); break;
                            case (0x21): AppendToSource("∆Tbl"); break;
                            case (0x22): AppendToSource("Tstep"); break;
                            case (0x23): AppendToSource("θstep"); break;
                            case (0x24): AppendToSource("ZTstep"); break;
                            case (0x25): AppendToSource("Zθstep"); break;
                            case (0x26): AppendToSource("∆X"); break;
                            case (0x27): AppendToSource("∆Y"); break;
                            case (0x28): AppendToSource("XFact"); break;
                            case (0x29): AppendToSource("YFact"); break;
                            case (0x2A): AppendToSource("TblInput"); break;
                            case (0x2B): AppendToSource("|N"); break;
                            case (0x2C): AppendToSource("I%"); break;
                            case (0x2D): AppendToSource("PV"); break;
                            case (0x2E): AppendToSource("PMT"); break;
                            case (0x2F): AppendToSource("FV"); break;
                            case (0x30): AppendToSource("|P/Y"); break;
                            case (0x31): AppendToSource("|C/Y"); break;
                            case (0x32): AppendToSource("w(nMin)"); break;
                            case (0x33): AppendToSource("Zw(nMin)"); break;
                            case (0x34): AppendToSource("PlotStep"); break;
                            case (0x35): AppendToSource("ZPlotStep"); break;
                            case (0x36): AppendToSource("Xres"); break;
                            case (0x37): AppendToSource("ZXres"); break;
                            default:
                                Console.WriteLine("Error Decompiling: Invalid token."); break;
                        }
                        break;
                    case 0x5E:
                        switch (tokens[i++])
                        {
                            case (0x10): AppendToSource("Y₁"); break;
                            case (0x11): AppendToSource("Y₂"); break;
                            case (0x12): AppendToSource("Y₃"); break;
                            case (0x13): AppendToSource("Y₄"); break;
                            case (0x14): AppendToSource("Y₅"); break;
                            case (0x15): AppendToSource("Y₆"); break;
                            case (0x16): AppendToSource("Y₇"); break;
                            case (0x17): AppendToSource("Y₈"); break;
                            case (0x18): AppendToSource("Y₉"); break;
                            case (0x19): AppendToSource("Y₀"); break;
                            case (0x20): AppendToSource("X₁ᴛ"); break;
                            case (0x21): AppendToSource("Y₁ᴛ"); break;
                            case (0x22): AppendToSource("X₂ᴛ"); break;
                            case (0x23): AppendToSource("Y₂ᴛ"); break;
                            case (0x24): AppendToSource("X₃ᴛ"); break;
                            case (0x25): AppendToSource("Y₃ᴛ"); break;
                            case (0x26): AppendToSource("X₄ᴛ"); break;
                            case (0x27): AppendToSource("Y₄ᴛ"); break;
                            case (0x28): AppendToSource("X₅ᴛ"); break;
                            case (0x29): AppendToSource("Y₅ᴛ"); break;
                            case (0x2A): AppendToSource("X₆ᴛ"); break;
                            case (0x2B): AppendToSource("Y₆ᴛ"); break;
                            case (0x40): AppendToSource("r₁"); break;
                            case (0x41): AppendToSource("r₂"); break;
                            case (0x42): AppendToSource("r₃"); break;
                            case (0x43): AppendToSource("r₄"); break;
                            case (0x44): AppendToSource("r₅"); break;
                            case (0x45): AppendToSource("r₆"); break;
                            case (0x80): AppendToSource("|u"); break;
                            case (0x81): AppendToSource("|v"); break;
                            case (0x82): AppendToSource("|w"); break;
                            default:
                                Console.WriteLine("Error Decompiling: Invalid token."); break;
                        }
                        break;
                    case 0x5D:
                        switch (tokens[i++])
                        {
                            case (0x00): AppendToSource("L₁"); break;
                            case (0x01): AppendToSource("L₂"); break;
                            case (0x02): AppendToSource("L₃"); break;
                            case (0x03): AppendToSource("L₄"); break;
                            case (0x04): AppendToSource("L₅"); break;
                            case (0x05): AppendToSource("L₆"); break;
                            default:
                                Console.WriteLine("Error Decompiling: Invalid token."); break;
                        }
                        break;
                    case 0x5C:
                        switch (tokens[i++])
                        {
                            case (0x00): AppendToSource("[A]"); break;
                            case (0x01): AppendToSource("[B]"); break;
                            case (0x02): AppendToSource("[C]"); break;
                            case (0x03): AppendToSource("[D]"); break;
                            case (0x04): AppendToSource("[E]"); break;
                            case (0x05): AppendToSource("[F]"); break;
                            case (0x06): AppendToSource("[G]"); break;
                            case (0x07): AppendToSource("[H]"); break;
                            case (0x08): AppendToSource("[I]"); break;
                            case (0x09): AppendToSource("[J]"); break;
                            default:
                                Console.WriteLine("Error Decompiling: Invalid token."); break;
                        }
                        break;
                    default:
                        AppendToSource((Tokens[tokens[i]]));
                        break;
                }
            }
        }

        private static string BytesArrayToString(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", " ");
        }
    }
}
