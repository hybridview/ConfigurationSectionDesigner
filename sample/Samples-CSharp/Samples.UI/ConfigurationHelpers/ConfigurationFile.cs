using System;
using System.Configuration;
using System.IO;

namespace Samples.UI.ConfigurationHelpers
{
    /// <summary>
    /// NOT COMPLETE AND NOT IN USE YET. 
    /// 
    /// This will be part of a better sample loader component.
    /// </summary>
    public class ConfigurationFile 
    {

        public string ConfigFilePath { get; set; }

        public string ConfigFileText { get; private set; }

        public System.Configuration.Configuration CurrentConfiguration { get; set; }

        public bool IsTempFile { get; set; }

        private ConfigurationFile()
        {


        }

        public static ConfigurationFile GetSampleConfigurationFile(string groupName, string configFileName)
        {
            ConfigurationFile configFile = new ConfigurationFile { IsTempFile = false };

            configFile.LoadSampleConfigFile(groupName, configFileName);

            return configFile;
        }

        public static ConfigurationFile GetTempConfigurationFile(string configFilePath)
        {
            return GetTempConfigurationFile(configFilePath, true);
        }

        public static ConfigurationFile GetTempConfigurationFile(string configFilePath, bool deleteFileAfterLoad)
        {
            ConfigurationFile configFile = new ConfigurationFile { IsTempFile = true };
            try
            {
                configFile.LoadTempConfigFile(configFilePath);
                //configFile = ConfigurationFile.GetFromTempConfigurationFile(configFilePath);
                if (deleteFileAfterLoad)
                {
                    // Delete config file after load if temp.
                    configFile.DeleteTempFile();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return configFile;
        }

        public static ConfigurationFile CreateTempConfigurationFile(SampleRunnerOptions options, ITempSampleConfigFileCreator sampleCreator)
        {
            ConfigurationFile configFile = new ConfigurationFile { IsTempFile = true };
            //WriteStartResult(groupName + " DEMO");
            try
            {
                // Create temp config file
                    string newConfigFilePath = "";
                   sampleCreator.CreateConfigFile(options, out newConfigFilePath);
                    //configFile.OnTempConfigFileCreated(configFilePath);
                   configFile.ConfigFilePath = newConfigFilePath;
            }
            catch (Exception ex)
            {
                throw;
            }
            return configFile;
        }
        /*
        private static ConfigurationFile _GetConfigurationFile(string configFilePath)
        {
            ConfigurationFile configFile = null;

            bool isTempConfigFile = string.IsNullOrEmpty(configFilePath);

            WriteStartResult(groupName + " DEMO");

            //ConfigurationFile configFile = null;
            try
            {
                // Create temp config file if no config specified.
                if (isTempConfigFile)
                {
                    string newConfigFilePath = "";
                    CreateConfigFile_Validation(out newConfigFilePath);
                    // WriteResult("Created new config file! Path={0}\r\n", newConfigFilePath);
                    //configFilePath = newConfigFilePath;
                    OnTempConfigFileCreated(configFilePath);
                    //config = LoadTempConfigFile(configFilePath);
                    configFile = ConfigurationFile.GetFromTempConfigurationFile(newConfigFilePath);

                    // Delete config file after load if temp.
                    configFile.DeleteTempFile();
                }
                else
                {
                    configFile = ConfigurationFile.GetFromSampleConfigurationFile(groupName, configFilePath);
                    //config = LoadSampleConfigFile(groupName, configFilePath);
                }


            }
            catch (Exception ex)
            {

            }
            return configFile;
        }
        */
        /*
        public static ConfigurationFile GetFromTempConfigurationFile(string configFilePath)
        {
            ConfigurationFile cf = new ConfigurationFile {IsTempFile = true};
            cf.LoadTempConfigFile(configFilePath);
            return cf;
        }

        public static ConfigurationFile GetFromSampleConfigurationFile(string groupName, string configFileName)
        {
            ConfigurationFile cf = new ConfigurationFile {IsTempFile = false};
            cf.LoadSampleConfigFile(groupName, configFileName);
            return cf;
        }
        */

        protected void  LoadTempConfigFile(string configFilePath)
        {
            _LoadConfigFile(configFilePath, true);
        }


        private void LoadSampleConfigFile(string groupName, string configFileName)
        {
            _LoadConfigFile(FileUtils.ResolveSampleConfigFilePath(groupName,configFileName), false);
        }


        private void _LoadConfigFile(string configFilePath, bool isTempConfigFile)
        {
            //if (System.IO.Path.IsPathRooted(configFilePath))

            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap { ExeConfigFilename = configFilePath };

            //fileMap.ExeConfigFilename = ConfigFilePath;

            if (!File.Exists(fileMap.ExeConfigFilename))
            {
                throw new Exception("Configuration file could not be found :" + fileMap.ExeConfigFilename);

            }
            else
            {
                // WriteResult("\r\n>> Configuration file '{0}' found!", fileMap.ExeConfigFilename);
                // ShowXml(File.ReadAllText(fileMap.ExeConfigFilename));
                ConfigFileText = File.ReadAllText(fileMap.ExeConfigFilename);
            }

            // Load configuration
            CurrentConfiguration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            
        }

        public void DeleteTempFile()
        {
            if (IsTempFile && File.Exists(this.ConfigFilePath))
            {
                File.Delete(this.ConfigFilePath);
                //OnTempConfigFileDeleted(tempConfigFile);
            }
        }
    }
}