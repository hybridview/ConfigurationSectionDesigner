using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samples.UI
{
    public class ConfigDemoBase
    {
        public string GroupName { get; set; }

        public System.Configuration.Configuration LoadConfigurationFile(string configFilePath)
        {
            bool isTempConfigFile = string.IsNullOrEmpty(configFilePath);
            // WriteStartResult(groupName + " DEMO");

            try
            {
                string newConfigFilePath = "";
                System.Configuration.Configuration config = null;
                // Create temp config file if no config specified.
                if (isTempConfigFile)
                {

                    CreateConfigFile_Validation(out newConfigFilePath);

                    configFilePath = newConfigFilePath;

                    OnTempConfigFileCreated(configFilePath);

                    config = LoadTempConfigFile(configFilePath);
                }
                else
                {
                    config = LoadSampleConfigFile(GroupName, configFilePath);
                }
                OnConfigFileLoaded(configFilePath);

                var section = config.Sections["validationSampleSection"] as ValidationSampleSection;

                _WriteResult("\tsection.Foo.Baz: {0}\r\n", section.Foo.Baz);

                // Delete config file if temp.
                if (isTempConfigFile)
                {
                    _DeleteConfigFile(configFilePath);
                }
            }
            catch (Exception ex)
            {
                _WriteErrorResult(ex);
            }
            finally
            {
                _WriteEndResult();
            }
        }

        private void _WriteResult(string format, params object[] vals)
        {

        }

        private void _WriteEndResult()
        {

        }

        private void _WriteErrorResult(Exception ex)
        {

        }

        private void _DeleteConfigFile(string configFilePath)
        {

        }

        public System.Configuration.Configuration LoadTempConfigFile(string configFilePath)
        {
            return _LoadConfigFile(configFilePath, true);
        }

        public System.Configuration.Configuration LoadSampleConfigFile(string groupName, string configFileName)
        {
            return _LoadConfigFile(Path.Combine(groupName, configFileName), false);
        }

        private System.Configuration.Configuration _LoadConfigFile(string configFilePath, bool isTempConfigFile)
        {
            //if (System.IO.Path.IsPathRooted(configFilePath))

            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap { ExeConfigFilename = configFilePath };

            if (!isTempConfigFile)
            {
                fileMap.ExeConfigFilename = Environment.CurrentDirectory + @"\ConfigurationFiles\" + configFilePath;
            }

            if (!File.Exists(fileMap.ExeConfigFilename))
            {
                throw new Exception("Configuration file could not be found :" + fileMap.ExeConfigFilename);
                //send("Configuration file required :" + fileMap.ExeConfigFilename, LogLevel.Error);
                //return;
            }
            else
            {
                WriteResult("\r\n>> Configuration file '{0}' found!", fileMap.ExeConfigFilename);
                ShowXml(File.ReadAllText(fileMap.ExeConfigFilename));
            }

            // Load configuration
            var config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            return config;
        }

    }
}
