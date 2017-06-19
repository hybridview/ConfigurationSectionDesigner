using System.Configuration;
using System.IO;
using Samples.Configuration.ValidationSample;

namespace Samples.UI.ConfigurationHelpers
{
    public class ValidationTempSampleConfigFileCreator : ITempSampleConfigFileCreator
    {
        /// <summary>
        /// If you want multiple sample scenarios, you can use this property in a 
        /// case statement in the CreateConfigFile method.
        /// </summary>
        public string SampleName { get; set; }


        public ValidationTempSampleConfigFileCreator():this("")
        {

        }

        public ValidationTempSampleConfigFileCreator(string sampleName)
        {
            SampleName = sampleName;
        }

        public void CreateConfigFile(SampleRunnerOptions options, out string newConfigFilePath)
        {
            newConfigFilePath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".config");
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap { ExeConfigFilename = newConfigFilePath };
            var config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None, options.EnablePreloadConfigFileCheckBox);

            // Create configuration
            var section = new ValidationSampleSection();
            // NOTE: Can really be any name, but the same name must be used when reading.
            config.Sections.Add("validationSampleSection", section);

            var foo = new Samples.Configuration.ValidationSample.Foo();
            foo.Baz = "I have dots!..."; // Exception will fire here!
            section.Foo = foo;

            config.Save();


        }


    }
}