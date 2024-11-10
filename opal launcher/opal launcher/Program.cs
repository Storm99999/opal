using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace opal_launcher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "opal launcher";
            Console.WriteLine("[opal] installing");
            File.Copy("opal.runtime.exe", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Roblox\\opal.runtime.exe");
            File.Copy("fflags.json", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Roblox\\fflags.json");
            File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\rbx-run.bat", @"
@echo off
setlocal enabledelayedexpansion

:: Get the path to the Roblox Versions folder
set ""robloxFolder=%LocalAppData%\Roblox\Versions""

:: Find the latest folder with RobloxPlayerBeta.exe in it
for /f ""delims="" %%A in ('dir ""%robloxFolder%"" /ad /b /o:-d') do (
    if exist ""%robloxFolder%\%%A\RobloxPlayerBeta.exe"" (
        set ""robloxPath=%robloxFolder%\%%A\RobloxPlayerBeta.exe""
        goto :found
    )
)

echo RobloxPlayerBeta.exe not found.
exit /b

:found
:: Launch Roblox and your app
start """" ""!robloxPath!"" %1
start """" ""C:\Users\%username%\AppData\Local\Roblox\opal.runtime.exe""
exit
");
            string batchFilePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\rbx-run.bat";

            string registryKey = @"HKEY_CLASSES_ROOT\roblox-player\shell\open\command";
            string commandValue = $"\"{batchFilePath}\" \"%1\"";
            try
            {
                // Modify the registry key to point to the batch file
                Registry.SetValue(registryKey, "", commandValue, RegistryValueKind.String);
                Console.WriteLine("Registry modified successfully. Roblox and opal are now paired.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error modifying registry: {ex.Message}");
            }

            Console.ReadLine();
        }
    }
}
