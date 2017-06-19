using System;
using System.Configuration;
using System.IO;
using Samples.Configuration.CollectionsSample;
using Samples.Configuration.InheritanceSample;

namespace Samples.UI.ConfigurationHelpers
{
    public class SharedElementsTempSampleConfigFileCreator : ITempSampleConfigFileCreator
    {
        /// <summary>
        /// If you want multiple sample scenarios, you can use this property in a 
        /// case statement in the CreateConfigFile method.
        /// </summary>
        public string SampleName { get; set; }


        public SharedElementsTempSampleConfigFileCreator()
            : this("")
        {

        }

        public SharedElementsTempSampleConfigFileCreator(string sampleName)
        {
            SampleName = sampleName;
        }

        public void CreateConfigFile(SampleRunnerOptions options, out string newConfigFilePath)
        {
            newConfigFilePath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".config");
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap { ExeConfigFilename = newConfigFilePath };
            var config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None, options.EnablePreloadConfigFileCheckBox);


            throw new NotImplementedException();



            config.Save();


        }


    }

    public class CollectionsTempSampleConfigFileCreator : ITempSampleConfigFileCreator
    {
        /// <summary>
        /// If you want multiple sample scenarios, you can use this property in a 
        /// case statement in the CreateConfigFile method.
        /// </summary>
        public string SampleName { get; set; }


        public CollectionsTempSampleConfigFileCreator()
            : this("")
        {

        }

        public CollectionsTempSampleConfigFileCreator(string sampleName)
        {
            SampleName = sampleName;
        }

        public void CreateConfigFile(SampleRunnerOptions options, out string newConfigFilePath)
        {
            newConfigFilePath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".config");
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap { ExeConfigFilename = newConfigFilePath };
            var config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None, options.EnablePreloadConfigFileCheckBox);


            // Create configuration
            var defaultCollDemoSection = new DefaultCollDemoSection();
            // NOTE: Can really be any name, but the same name must be used when reading.
            config.Sections.Add("defaultCollDemoSection", defaultCollDemoSection);

            // Create configuration
            var keylessCollectionSection = new KeylessCollectionSection();
            // NOTE: Can really be any name, but the same name must be used when reading.
            config.Sections.Add("keylessCollectionSection", keylessCollectionSection);


           

            config.Save();


        }


    }
}