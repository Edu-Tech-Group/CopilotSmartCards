using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace PromptRunner
{
    internal class Program
    {
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hwnd);

        static Dictionary<string, string> Prompts = new Dictionary<string, string>{
            {"PromptCard1", "Maak geheugenkaartjes om te leren voor mijn examen Natuurwetenschappen: Biodiversiteit. Gebruik deze pagina als bron: Biodiversiteit | Bioleren"},
            {"PromptCard2", "Kun je me helpen de tekst op onderstaande pagina te herschrijven zodat deze duidelijker en beknopter wordt? Breng deze op niveau van een 13-jarige. https://zowerkthetlichaam.nl/39/longen-en-ademhaling/"},
        };

        static void Main(string[] args)
        {
            string prompt = Prompts[args[2]];
            Console.WriteLine($"card {args[2]} - {prompt}");

            try
            {
                var browserProcess = Process.Start("C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe", "--hide-crash-restore-bubble https://m365.cloud.microsoft/chat");
                //var browserProcess = Process.Start("C:\\Program Files (x86)\\Microsoft\\Edge\\Application\\msedge.exe", "https://m365.cloud.microsoft/chat");

                if (browserProcess == null)
                {
                    Console.WriteLine("Cannot start process");
                    return;
                }

                Thread.Sleep(5000);

                SetForegroundWindow(browserProcess.MainWindowHandle);
                SendKeys.SendWait($"{prompt}\n");

                Thread.Sleep(10000);

                browserProcess.Kill();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception - {ex.Message}");
            }
        }
    }
}
