using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace opal.runtime
{
    internal class Program
    {
        static void WriteColoredText(string text, string alias)
        {
            // man i sure hope i didnt need 15 minutes to fix this!!!
            string keyword = $"[{alias}]";

            int keywordIndex = text.IndexOf(keyword);

            if (keywordIndex != -1)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(text.Substring(0, keywordIndex));

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(keyword);

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(text.Substring(keywordIndex + keyword.Length));
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(text);
            }

            Console.ResetColor();
        }

        static void Main(string[] args)
        {
            Console.Title = "opal - roblox launcher";
            WriteGradient(@"
                                                                       /$$
                                                                      | $$
                                          /$$$$$$   /$$$$$$   /$$$$$$ | $$
                                         /$$__  $$ /$$__  $$ |____  $$| $$
                                        | $$  \ $$| $$  \ $$  /$$$$$$$| $$
                                        | $$  | $$| $$  | $$ /$$__  $$| $$
                                        |  $$$$$$/| $$$$$$$/|  $$$$$$$| $$
                                         \______/ | $$____/  \_______/|__/
                                                  | $$                    
                                                  | $$                    
                                                  |__/                    
");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            WriteColoredText("  [opal] initiating version fetcher", "opal");
            var lad = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Roblox\\Versions";
            WriteColoredText("  [researcher] localappdata was found", "researcher");
            var isok = false;
            var latest = "";
            foreach (var flds in Directory.GetDirectories(lad))
            {
                // most likely wont happen, but just to make sure. 
                var i = new DirectoryInfo(flds);
                

                if (i.GetFiles().Length != 0)
                {
                    foreach (var fe in Directory.GetFiles(i.FullName))
                    {
                        var fileInfo1 = new FileInfo(fe);
                        if (Directory.Exists(i.FullName + "BuiltInPlugins"))
                        {
                            Console.WriteLine("  [researcher] found old version: " + i.FullName);
                        }
                    }
                }

                if (File.Exists(i.FullName + "\\RobloxPlayerBeta.exe"))
                {
                    WriteColoredText("  [researcher] found latest - " + i.FullName, "researcher");
                    latest = i.FullName;
                }

                if (i.GetFiles().Length == 0 || i.GetDirectories().Length == 0)
                {
                    if (!isok)
                    {
                        isok = true;
                        WriteColoredText("  [researcher] found and deleted old versions", "researcher");
                        

                    }
                    Directory.Delete(i.FullName);
                }
            }

        

            Thread.Sleep(1000);

            WriteColoredText("  [opal] applying fflags", "opal");
            if (!Directory.Exists(latest + "\\ClientSettings\\"))
            {
                WriteColoredText("  [fflags] clientsettings not found, creating", "fflags");
                Directory.CreateDirectory(latest + "\\ClientSettings\\");
                File.WriteAllText(latest + "\\ClientSettings\\ClientAppSettings.json", File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Roblox\\fflags.json"));
                WriteColoredText("  [fflags] flags inserted, continue", "fflags");
            }
            else
            {
                File.WriteAllText(latest + "\\ClientSettings\\ClientAppSettings.json", File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Roblox\\fflags.json"));
                WriteColoredText("  [fflags] flags inserted, continue", "fflags");
            }

            WriteColoredText("  [opal] initiating roblox", "opal");
            Thread.Sleep(300);
            WriteColoredText("  [opal] completed. closing.", "opal");
            Environment.Exit(0);


        }

        static void WriteGradient(string text)
        {
            int textLength = text.Length;

            for (int i = 0; i < textLength; i++)
            {
                if (i < textLength / 2)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                }

                Console.Write(text[i]);
            }

            Console.ResetColor();
            Console.WriteLine(); 
        }

    }
}
