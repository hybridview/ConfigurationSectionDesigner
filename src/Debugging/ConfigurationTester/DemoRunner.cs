using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Debugging.IssueTests;

namespace ConfigurationTester
{
    public class DemoRunner
    {

        

        public void RunWorkItem6053Demo()
        {
            Util.WriteResult("===== WorkItem6053 DEMO =====\r\n");

            try
            {
                string newConfigFilePath = "";
                Util.CreateWorkItem6053ConfigFile(out newConfigFilePath);
                Util.WriteResult("Created new config file! Path={0}\r\n", newConfigFilePath);

                Configuration config = Util.LoadConfigFile(newConfigFilePath);

                Util.WriteResult("Loaded config file.\r\n");

                InnerConfigurationSection section = config.Sections["test1"] as InnerConfigurationSection;

                Util.WriteResult("\r\n### Configuration Read Results for InnerConfigurationSection:\r\n");

                Util.WriteResult("\tTestAttribute1 = {0}\r\n", section.TestAttribute1);


                Util.WriteResult("\r\n### Configuration Read Results for ConfigurationSection1:\r\n");

                ConfigurationSection1 section2 = config.Sections["test2"] as ConfigurationSection1;

                Util.WriteResult("\tTestAttribute2 = {0}\r\n", section2.TestAttribute2);

                /*
                WriteResult("\tNumber of professors: {0}\r\n", section.Professors.Count);
                WriteResult("\tNumber of students: {0}\r\n", section.Students.Count);
                WriteResult("\tName of student '0': {0}\r\n", section.Students[0].Name);
                */
                //Util.DeleteConfigFile(newConfigFilePath);
            }
            catch (Exception ex)
            {
                Util.WriteResult("Exception Occured! {0}\r\n\r\n", ex);
            }

            Util.WriteResult("Demo Complete!\r\n\r\n");
        }

        public void RunWorkItem6053Demo1()
        {
            Util.WriteResult("===== WorkItem6053 DEMO =====\r\n");

            try
            {
                string newConfigFilePath = "MainDemo.config";
                //ExeConfigurationFileMap mappedConfig = new ExeConfigurationFileMap();

                //ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap()

                //Util.CreateWorkItem6053ConfigFile(out newConfigFilePath);
                //Util.WriteResult("Created new config file! Path={0}\r\n", newConfigFilePath);

                Configuration config = Util.LoadConfigFile(newConfigFilePath);

                //Util.WriteResult("Loaded config file.\r\n");
                

                InnerConfigurationSection configSection = InnerConfigurationSection.Instance;



                //InnerConfigurationSection section = config.Sections["test1"] as InnerConfigurationSection;

                Util.WriteResult("\r\n### Configuration Read Results for InnerConfigurationSection:\r\n");

                Util.WriteResult("\tInnerConfigurationSection.TestAttribute1 = {0}\r\n", configSection.TestAttribute1);


                //Util.WriteResult("\r\n### Configuration Read Results for ConfigurationSection1:\r\n");

                //ConfigurationSection1 section2 = config.Sections["test2"] as ConfigurationSection1;

                //Util.WriteResult("\tTestAttribute2 = {0}\r\n", section2.TestAttribute2);

                /*
                WriteResult("\tNumber of professors: {0}\r\n", section.Professors.Count);
                WriteResult("\tNumber of students: {0}\r\n", section.Students.Count);
                WriteResult("\tName of student '0': {0}\r\n", section.Students[0].Name);
                */
                //Util.DeleteConfigFile(newConfigFilePath);
            }
            catch (Exception ex)
            {
                Util.WriteResult("Exception Occured! {0}\r\n\r\n", ex);
            }

            Util.WriteResult("Demo Complete!\r\n\r\n");
        }

        public void RunWorkItem6053Demo2()
        {
            Util.WriteResult("===== WorkItem6053 DEMO =====\r\n");

            try
            {
                //string newConfigFilePath = "WorkItem6053_Test1.config";
                //ExeConfigurationFileMap mappedConfig = new ExeConfigurationFileMap();
                
                //ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap()

                //Util.CreateWorkItem6053ConfigFile(out newConfigFilePath);
                //Util.WriteResult("Created new config file! Path={0}\r\n", newConfigFilePath);

                //Configuration config = Util.LoadConfigFile(newConfigFilePath);

                //Util.WriteResult("Loaded config file.\r\n");

                //EmailConfigSection emailConfigSection = config.Sections["emailConfigSection"] as EmailConfigSection;
                EmailConfigSection emailConfigSection = EmailConfigSection.Instance;

                //InnerConfigurationSection section = config.Sections["test1"] as InnerConfigurationSection;

                Util.WriteResult("\r\n### Configuration Read Results for emailConfigSection:\r\n");

                Util.WriteResult("\tEmail[0].enabled = {0}\r\n", emailConfigSection.Emails[0].enabled);


                //Util.WriteResult("\r\n### Configuration Read Results for ConfigurationSection1:\r\n");

                //ConfigurationSection1 section2 = config.Sections["test2"] as ConfigurationSection1;

                //Util.WriteResult("\tTestAttribute2 = {0}\r\n", section2.TestAttribute2);

                /*
                WriteResult("\tNumber of professors: {0}\r\n", section.Professors.Count);
                WriteResult("\tNumber of students: {0}\r\n", section.Students.Count);
                WriteResult("\tName of student '0': {0}\r\n", section.Students[0].Name);
                */
                //Util.DeleteConfigFile(newConfigFilePath);
            }
            catch (Exception ex)
            {
                Util.WriteResult("Exception Occured! {0}\r\n\r\n", ex);
            }

            Util.WriteResult("Demo Complete!\r\n\r\n");
        }
    }
}
