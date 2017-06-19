using System.Configuration;
using System.IO;
using Samples.Configuration.SimpleSample;

namespace Samples.UI.ConfigurationHelpers
{
    public class SimpleTempSampleConfigFileCreator : ITempSampleConfigFileCreator
    {
        /// <summary>
        /// If you want multiple sample scenarios, you can use this property in a 
        /// case statement in the CreateConfigFile method.
        /// </summary>
        public string SampleName { get; set; }


        public SimpleTempSampleConfigFileCreator()
            : this("")
        {

        }

        public SimpleTempSampleConfigFileCreator(string sampleName)
        {
            SampleName = sampleName;
        }

        public void CreateConfigFile(SampleRunnerOptions options, out string newConfigFilePath)
        {
            newConfigFilePath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".config");
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap { ExeConfigFilename = newConfigFilePath };
            var config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None, options.EnablePreloadConfigFileCheckBox);

            // Create configuration
            var section = new SimpleSection();
            // NOTE: Can really be any name, but the same name must be used when reading.
            config.Sections.Add("simpleSection", section);

            var simple = new Samples.Configuration.SimpleSample.SimpleElement();
            simple.Foo = "fooval";
            simple.Bar = "barval";

            config.Save();


        }


    }
}