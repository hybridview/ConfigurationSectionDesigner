using System.Configuration;
using System.IO;
using Samples.Configuration.ExternalTypes;
using Samples.Configuration.ExternalTypeSample;

namespace Samples.UI.ConfigurationHelpers
{
    public class ExternalTypeTempSampleConfigFileCreator : ITempSampleConfigFileCreator
    {
        /// <summary>
        /// If you want multiple sample scenarios, you can use this property in a 
        /// case statement in the CreateConfigFile method.
        /// </summary>
        public string SampleName { get; set; }


        public ExternalTypeTempSampleConfigFileCreator()
            : this("")
        {

        }

        public ExternalTypeTempSampleConfigFileCreator(string sampleName)
        {
            SampleName = sampleName;
        }

        public void CreateConfigFile(SampleRunnerOptions options, out string newConfigFilePath)
        {
            newConfigFilePath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".config");
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap { ExeConfigFilename = newConfigFilePath };
            var config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None, options.EnablePreloadConfigFileCheckBox);

            // Create configuration
            //ExternalTypesFooDemoGroup grp = new ExternalTypesFooDemoGroup();
            ConfigurationSectionGroup sectionGroup = new ConfigurationSectionGroup();

            ExternalTypesFooDemoSection section = new ExternalTypesFooDemoSection();

            config.SectionGroups.Add("externalTypesFooDemoGroup", sectionGroup);
            
            config.Sections.Add("externalTypesFooDemoSection", section);

            section.Foo.Baz = new CustomBlock { Height = 42, Width = 314, Depth = 312 };

            section.Bars.Add(new Bar { Crackle = 3.14F, Snap = true });
            section.Bars.Add(new Bar { Crackle = 2.71828F, Snap = false });

            config.Save();


        }


    }
}