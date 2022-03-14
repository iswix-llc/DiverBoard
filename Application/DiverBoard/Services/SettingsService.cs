using DiverBoard.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiverBoard.Services
{
    public class SettingsService
    {
        public static string TripsDirectory { get { return @"C:\DiveBoard\trips"; } }
        public static string ConfigDirectory { get { return @"C:\DiveBoard\config"; } }
        public static string LastDocument 
        {
            get
            {
                string lastDocument = string.Empty;

                object value = Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\ISWIX\DiverBoard", "LastDocument", null);
                if(value != null)
                {
                    lastDocument = value.ToString();
                }
                return lastDocument;
            }
            set
            {
                Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\ISWIX\DiverBoard", "Lastdocument", value, RegistryValueKind.String);
            }

        }

        public static List<string> BunkNumbers
        { 
            get 
            {
                string configFile = Path.Combine(ConfigDirectory, "bunks.txt");
                if (!Directory.Exists(ConfigDirectory))
                {
                    Directory.CreateDirectory(ConfigDirectory);
                }
                if(!File.Exists(configFile))
                {
                    List<string> bunks = new List<string>();
                    bunks.Add("1A");
                    bunks.Add("1B");
                    bunks.Add("2A");
                    bunks.Add("2B");
                    bunks.Add("3");
                    bunks.Add("4");
                    bunks.Add("5");
                    bunks.Add("6");
                    bunks.Add("7");
                    bunks.Add("8");
                    bunks.Add("9");
                    bunks.Add("10");
                    bunks.Add("11");
                    bunks.Add("12");
                    bunks.Add("13");
                    bunks.Add("14");
                    bunks.Add("15");
                    bunks.Add("16");
                    bunks.Add("17");
                    bunks.Add("18");
                    bunks.Add("19");
                    bunks.Add("20");
                    bunks.Add("21");
                    bunks.Add("22");
                    bunks.Add("24");
                    bunks.Add("25");
                    bunks.Add("26");
                    bunks.Add("27");
                    bunks.Add("28");
                    bunks.Add("29");
                    bunks.Add("30");
                    File.WriteAllLines(configFile, bunks);
                }
                return File.ReadAllLines(configFile).ToList<string>();
            }
        }
    }
}
