using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using Debugging.IssueTests;
using Sample;
using Debugging;
//using Inherit;

namespace ConfigurationTester
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            string tempConfigFile = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".config");
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap { ExeConfigFilename = tempConfigFile };
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            */
            // Create configuration
            /*SampleConfigurationSection section = new SampleConfigurationSection();
            config.Sections.Add("sample", section);
            section.Foo.Baz = new CustomType { Height = 42, Width = 314, Depth = 313 };

            section.Bars.Add(new Bar { Crackle = 3.14F, Snap = true });
            //section.Bars.Add(new Bar { Crackle = 2.71828F, Snap = false });
            */

            /*SchoolRegistrySection section = new SchoolRegistrySection();
            config.Sections.Add("school", section);

            Professor flimflop = new Professor { Name = "Dr. Flimflop", YearOfBirth = 1968 };
            section.Professors.Add(flimflop);
            Professor mania = new Professor { Name = "Dr. Maniä", YearOfBirth = 1972 };
            section.Professors.Add(mania);

            Student johnson = new Student { Name = "Johnson", YearOfBirth = 1989 };
            section.Students.Add(johnson);
            mania.Students.Add(johnson);

            config.Save();

            // Load configuration
            config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            section = config.Sections["school"] as SchoolRegistrySection;
            /*section = config.Sections["sample"] as SampleConfigurationSection;

            Console.WriteLine(section.Foo.Baz.Height);
            Console.WriteLine(section.Foo.Baz.Width);
            Console.WriteLine(section.Foo.Baz.Depth);*/

            /*
            if (File.Exists(tempConfigFile))
                File.Delete(tempConfigFile);
            */
            //RunWorkItem6053Demo(1);
            RunWorkItem6053Demo(2);
            Console.ReadLine();
        }


        public static void RunWorkItem6053Demo(int testNumber)
        {
            DemoRunner runner = new DemoRunner();

            switch (testNumber)
            {
                case 1:
                    runner.RunWorkItem6053Demo1();
                    break;
                case 2:
                    runner.RunWorkItem6053Demo2();
                    break;
                default:
                    throw new ArgumentException("Invalid testNumber!");

            }
         
        }

    }
}
