using System.Configuration;
using System.IO;
using Samples.Configuration.InheritanceSample;

namespace Samples.UI.ConfigurationHelpers
{
    public class InheritanceTempSampleConfigFileCreator : ITempSampleConfigFileCreator
    {
        /// <summary>
        /// If you want multiple sample scenarios, you can use this property in a 
        /// case statement in the CreateConfigFile method.
        /// </summary>
        public string SampleName { get; set; }


        public InheritanceTempSampleConfigFileCreator()
            : this("")
        {

        }

        public InheritanceTempSampleConfigFileCreator(string sampleName)
        {
            SampleName = sampleName;
        }

        public void CreateConfigFile(SampleRunnerOptions options, out string newConfigFilePath)
        {
            newConfigFilePath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".config");
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap { ExeConfigFilename = newConfigFilePath };
            var config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None, options.EnablePreloadConfigFileCheckBox);

            // Create configuration
            SchoolRegistrySection section = new SchoolRegistrySection();
            // NOTE: Can really be any name, but the same name must be used when reading.
            config.Sections.Add("schoolRegistrySection", section);

            Professor flimflop = new Professor { Name = "Dr. Flimflop", YearOfBirth = 1968 };
            section.Professors.Add(flimflop);
            Professor mania = new Professor { Name = "Dr. Maniä", YearOfBirth = 1972 };
            section.Professors.Add(mania);

            Student johnson = new Student { Name = "Johnson", YearOfBirth = 1989 };
            section.Students.Add(johnson);
            mania.Students.Add(johnson);

            config.Save();


        }


    }
}