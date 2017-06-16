using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using Debugging.IssueTests;

namespace ConfigurationTester
{
    public class Util
    {

        public static void DeleteConfigFile(string tempConfigFile)
        {
            if (File.Exists(tempConfigFile))
                File.Delete(tempConfigFile);
            WriteResult("Deleted temp config file. Path={0}\r\n", tempConfigFile);
        }

        public static void WriteResult(string format, params object[] vals)
        {
           // ConsoleTextBox.AppendText(string.Format(format, vals));
            Console.Write(format, vals);
        }

        public static Configuration LoadConfigFile(string configFileName)
        {
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap { ExeConfigFilename = configFileName };
            // Load configuration
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            return config;
        }

        public static void CreateWorkItem6053ConfigFile(out string newConfigFilePath)
        {
            newConfigFilePath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".config");
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap { ExeConfigFilename = newConfigFilePath };
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);

            // Create configuration

            //OuterSectionGroup outer = new OuterSectionGroup();
            InnerConfigurationSection section = new InnerConfigurationSection();
            config.Sections.Add("test1", section);
            section.TestAttribute1 = "mytest";



            ConfigurationSection1 section1 = new ConfigurationSection1();
            config.Sections.Add("test2", section1);
            section1.TestAttribute2 = "mytest2";


            /*
            Professor flimflop = new Professor { Name = "Dr. Flimflop", YearOfBirth = 1968 };
            section.Professors.Add(flimflop);
            Professor mania = new Professor { Name = "Dr. Maniä", YearOfBirth = 1972 };
            section.Professors.Add(mania);

            Student johnson = new Student { Name = "Johnson", YearOfBirth = 1989 };
            section.Students.Add(johnson);
            mania.Students.Add(johnson);
            */
            config.Save();

        }
    }
}
