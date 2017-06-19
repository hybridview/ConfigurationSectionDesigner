using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using Samples.Configuration.CollectionsSample;
using Samples.Configuration.ExternalTypeSample;
using Samples.Configuration.ExternalTypes;
using Samples.Configuration.InheritanceSample;
using Samples.Configuration.SharedElementsSample;
using Samples.Configuration.SimpleSample;
using Samples.Configuration.ValidationSample;
using Samples.UI.ApplicationConfig;
using Samples.UI.ConfigurationHelpers;
using Bar = Samples.Configuration.ExternalTypeSample.Bar;

namespace Samples.UI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            CurrentSampleGroup = null;
            CurrentSample = null;

            RefreshSampleGroupsConfig("");

            //TryInternalTest();
        }


        protected SampleGroup CurrentSampleGroup { get; set; }

        protected Sample CurrentSample { get; set; }

        /// <summary>
        /// This demonstrates interesting behavior. When configuration is loaded via application config, 
        /// ALL properties are validated and errors are added to a collection. When exception is thrown, 
        /// we can see all property errors.
        /// 
        /// I can't see HOW this occurs, because for some reason, cannot step into System.Configuration source... Ugh..
        /// 
        /// When we load config file manually via EXE-MAP, properties are only validated on first access to that 
        /// property. It's not automatic. Also, we only get 1 error for file with multiple errors.
        /// </summary>
        private void TryInternalTest()
        {

            //ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap { ExeConfigFilename = "mypath" };
           // var config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None, enablePreloadConfigFileCheckBox.Checked);

            //var c = ValidationSampleSection.Instance.zipCode
            WriteResult("TryInternalTest: Testing validation section from app.config...");
            try
            {
                // CONFIRMED: Config is not touched until we access instance.
                WriteResult("\tValidationSampleSection.Instance.zipCode=", ValidationSampleSection.Instance.ZipCode);
            }
            catch (Exception ex)
            {
                WriteErrorResult(ex);
            }


        }




        #region "Config demos"
        private void RunConfigDemo_Collections(ConfigurationFile configFile)
        {
            try
            {
                var defaultCollDemoSection = configFile.CurrentConfiguration.GetSection("defaultCollDemoSection") as DefaultCollDemoSection;
                var keylessCollectionSection = configFile.CurrentConfiguration.GetSection("keylessCollectionSection") as KeylessCollectionSection;

                WriteResult("[defaultCollDemoSection]\r\n");
                WriteResult("*** [Demo1Items]\r\n");
                foreach (Demo1Element item in defaultCollDemoSection.Demo1Items)
                {
                    WriteResult("\tItem: Attribute1={0}; Attribute2={1}\r\n", item.Attribute1, item.Attribute2);
                }
                WriteResult("*** [Demo2Items]\r\n");
                foreach (Demo2Element item in defaultCollDemoSection.Demo2Items)
                {
                    WriteResult("\tItem: Attribute21={0}\r\n", item.Attribute21);
                }

                WriteResult("\r\n\r\n[keylessCollectionSection]\r\n");
                foreach (KeylessElement item in keylessCollectionSection.Items)
                {
                    WriteResult("\tItem: Attr1={0}; Attr2={1}\r\n", item.Attr1, item.Attr2);
                }


               // WriteResult("\tsection.Samples: {0}\r\n", keylessCollectionSection.Items);


               // WriteResult("\tsection.Samples: {0}\r\n", defaultCollDemoSection..);
               
               
            }
            catch (Exception ex)
            {
                WriteErrorResult(ex);
            }
            finally
            {
                WriteEndResult();
            }

        }

        private void RunConfigDemo_ExternalType(ConfigurationFile configFile)
        {
            try
            {
  
                // NOTE: Name is case sensitive and is used for [sectionGroup name=] in the config file. If not, the sectionGroup below will be null.
                ConfigurationSectionGroup sectionGroup = configFile.CurrentConfiguration.GetSectionGroup("externalTypesFooDemoGroup");
                WriteResult("\tSectionGroups[0]={0}.\r\n", sectionGroup.Name);

                //SampleConfigurationSection section = config.Sections["sample"] as SampleConfigurationSection;
                var section = configFile.CurrentConfiguration.GetSection("externalTypesFooDemoSection") as ExternalTypesFooDemoSection;
               // var section = sectionGroup.Sections["externalTypesFooDemoSection"] as ExternalTypesFooDemoSection; // Returns null. Investigate.


                WriteResult("\tsection.Samples: {0}\r\n", section.Samples);
                WriteResult("\tsection.Bars[0].Crackle: {0}\r\n", section.Bars[0].Crackle);

                WriteResult("\tFoo.Baz.Height: {0}\r\n", section.Foo.Baz.Height);
                WriteResult("\tFoo.Baz.Width: {0}\r\n", section.Foo.Baz.Width);
                WriteResult("\tFoo.Baz.Depth: {0}\r\n", section.Foo.Baz.Depth);
            }
            catch (Exception ex)
            {
                WriteErrorResult(ex);
            }
            finally
            {
                WriteEndResult();
            }

        }

        private void RunConfigDemo_SchoolRegistry(ConfigurationFile configFile)
        {
            try
            {
                //SchoolRegistrySection section = config.Sections["school"] as SchoolRegistrySection;
                //var section = config.Sections["schoolRegistrySection"] as SchoolRegistrySection;
                var section = configFile.CurrentConfiguration.GetSection("schoolRegistrySection") as SchoolRegistrySection;

                WriteResult("\tSchool name: {0}\r\n", section.SchoolName);
                WriteResult("\tNumber of professors: {0}\r\n", section.Professors.Count);
                WriteResult("\tNumber of students: {0}\r\n", section.Students.Count);
                WriteResult("\tName of student '0': {0}\r\n", section.Students[0].Name);
            }
            catch (Exception ex)
            {
                WriteErrorResult(ex);
            }
            finally
            {
                WriteEndResult();
            }
        }

        private void RunConfigDemo_SharedElements(ConfigurationFile configFile)
        {
            try
            {

                var section = configFile.CurrentConfiguration.GetSection("sharedConsumerSection") as SharedConsumerSection;
                //<section name="sharedConsumerSection" type="Samples.Configuration.SharedElementsSample.SharedConsumerSection, Samples.Configuration, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"/>

                WriteResult("\tFoo: {0}\r\n", section.MyElement.Foo);
                WriteResult("\tBar: {0}\r\n", section.MyElement.Bar);

            }
            catch (Exception ex)
            {
                WriteErrorResult(ex);
            }
            finally
            {
                WriteEndResult();
            }
        }

        private void RunConfigDemo_Simple(ConfigurationFile configFile)
        {
            try
            {
                //SchoolRegistrySection section = config.Sections["school"] as SchoolRegistrySection;
                //var section = config.Sections["schoolRegistrySection"] as SchoolRegistrySection;
                var section = configFile.CurrentConfiguration.GetSection("simpleSection") as SimpleSection;

                WriteResult("\tFoo: {0}\r\n", section.Simple.Foo);
                WriteResult("\tBar: {0}\r\n", section.Simple.Bar);
         
            }
            catch (Exception ex)
            {
                WriteErrorResult(ex);
            }
            finally
            {
                WriteEndResult();
            }
        }

        private void RunConfigDemo_Validation(ConfigurationFile configFile)
        {
            try
            {
                //var group = config.GetSectionGroup("SampleConfigurationGroup") as ConfigurationSectionGroup;
                //WriteResult("\tgroup: {0}\r\n", group.Sections[0]);

                // With groups, we MUST find this way!!!
                //var section = group.Sections[0] as ValidationSampleSection; // config.Sections["validationSampleSection"] as ValidationSampleSection;
                //var section = config.Sections["validationSampleSection"] as ValidationSampleSection;

                /** OBSERVATIONS:
                 * + Validation exceptions are thrown in order of property ACCESS. They are not thrown when loading section...
                 *   When not loading section through default config, property validation isn't pre-executed.
                 * */

                // Not working... Manager not used when manually load file. Causes validators to not call on GetSection????
                //WriteResult("\tValidationSampleSection.Instance.anythingButDots: {0}\r\n", ValidationSampleSection.Instance.anythingButDots);
                //ConfigurationManager.OpenMappedExeConfiguration()
                //ValidationSampleSection section = ConfigurationManager.GetSection("validationSampleSection") as ValidationSampleSection;
                ValidationSampleSection section = null;

                /**
                 * Below, there is some custom logic not present in other samples. This logic was used as "an experiment" and 
                 * will be better organized into a seperate component.
                 * */
                if (checkBox1.Checked)
                {
                    WriteResult("*** About to use CustomAppConfig.Change to load section as though it were default app.config!");
                    WriteResult("\r\n");

                    // Not working as I was expecting....
                    using (AppConfigRedirector.Change(configFile.ConfigFilePath))
                    {
                        // the app.config in tempFileName is used
                        section = ConfigurationManager.GetSection("validationSampleSection") as ValidationSampleSection;



                        //section = ValidationSampleSection.Instance;

                        WriteResult("*** Now attempting to read properties. If properties were validated during section load, this message would never be shown!");
                        WriteResult("\r\n");

                        WriteResult("\tsection.anythingButDots: {0}\r\n", section.AnythingButDots);
                        WriteResult("\tsection.Foo.Baz: {0}\r\n", section.Foo.Baz);

                        //WriteResult("\tsection.Bars[0].noDots: {0}\r\n", section.Bars[0].noDots);

                    }
                }
                else
                {

                    // IMPORTANT: For this test and all others, validation error handling is NOT the same as what you 
                    // would expect, because here, we load config via EXEMAP. Special error handling must be added when loading 
                    // config via exemap! In default app.config, all validation errors are collected in a list and are included in 
                    // a thrown exception at the end. For manual file load, NO validation exception results until property is accessed (I will 
                    // provide detailed document as to why later). However, you still have access to list of errors. You 
                    // will need to iterate through the errors and throw your own ConfigurationErrorsException for this 
                    // to behave as expected.

                    // Below, I demonstrate how to access this list of errors.
                    WriteResult("\r\n[[ NOTE ]] This sample includes Special validation logic below. See MainForm.cs source for more info.\r\n");

                    //if ((configFile.CurrentConfiguration == null) && configFile.IsTempFile)
                    if (configFile==null)
                    {
                       
                        WriteResult("\r\n[ NOTE ] Configuration could not be loaded! (possible reason) If we were trying to create a temp config file with a bad attribute, it would never be written to file and this NULL would result.\r\n");

                    }
                    section = configFile.CurrentConfiguration.GetSection("validationSampleSection") as ValidationSampleSection;
                    

                    var errs = section.ElementInformation.Errors;

                    if (errs.Count > 0)
                    {
                        WriteResult("\r\n[[ ERROR(s) REPORTED! ]] Config loader returned a list of 1 or more validation errors:\r\n");

                        foreach (ConfigurationErrorsException err in errs)
                        {
                            WriteResult("\r\n* ERROR: {0}\r\n", err.ToString());
                            if (err.Errors.Count > 1)
                            {
                                foreach (ConfigurationErrorsException interr in err.Errors)
                                {
                                    WriteResult("\r\n*** INNER {0}\r\n", interr.ToString());
                                }
                            }
                        }
                    }
                    WriteResult("\r\n*** Now attempting to read properties. If properties were validated during section load, this message would never be shown!");
                    WriteResult("\r\n");

                    WriteResult("\tsection.anythingButDots: {0}\r\n", section.AnythingButDots);
                    WriteResult("\tsection.Foo.Baz: {0}\r\n", section.Foo.Baz);
                    WriteResult("\tsection.Bars[0].noDots: {0}\r\n", section.Bars[0].NoDots);
                }
                // section = config.GetSection("validationSampleSection") as ValidationSampleSection;
                // section.CurrentConfiguration.EvaluationContext.GetSection()
            }
            catch (Exception ex)
            {
                WriteErrorResult(ex);
            }
            finally
            {
                WriteEndResult();
            }
        }

        #endregion





        #region "Config demo helpers (NEW)"


        private ConfigurationFile InitConfigDemoFromTemp(string groupName, ITempSampleConfigFileCreator sampleCreator)
        {
            WriteStartResult(groupName + " DEMO");
            ConfigurationFile configFile = null;
            try
            {
                // Create temp config file
                configFile = ConfigurationFile.CreateTempConfigurationFile(GetCurrentSampleRunnerOptions(), sampleCreator);
                string newConfigFilePath = configFile.ConfigFilePath;
                // WriteResult("Created new config file! Path={0}\r\n", newConfigFilePath);
                OnTempConfigFileCreated(newConfigFilePath);
                configFile = ConfigurationFile.GetTempConfigurationFile(newConfigFilePath);

                ShowXml(configFile.ConfigFileText);

                OnConfigFileLoaded(configFile.ConfigFilePath);
            }
            catch (Exception ex)
            {
                WriteErrorResult(ex);
            }
            return configFile;
        }

        private ConfigurationFile InitConfigDemoFromSample(string groupName, string configFilePath)
        {
            WriteStartResult(groupName + " DEMO");
            ConfigurationFile configFile = null;
            try
            {
                configFile = ConfigurationFile.GetSampleConfigurationFile(groupName, configFilePath);

                ShowXml(configFile.ConfigFileText);

                OnConfigFileLoaded(configFile.ConfigFilePath);
            }
            catch (Exception ex)
            {
                WriteErrorResult(ex);
            }
            return configFile;
        }

        private SampleRunnerOptions GetCurrentSampleRunnerOptions()
        {
            SampleRunnerOptions options = new SampleRunnerOptions(enablePreloadConfigFileCheckBox.Checked);
            return options;
        }

        #endregion



        #region "Helper methods"

        //WriteResult("\r\n>> Configuration file '{0}' found!", fileMap.ExeConfigFilename);

        private void OnConfigFileLoaded(string filePath)
        {
            WriteResult("\r\n>> Loaded config file {0}! Reading some values...\r\n", filePath);
        }
        private void OnTempConfigFileCreated(string filePath)
        {
            WriteResult("\r\n>> Created new temporary config file! Path={0}\r\n", filePath);
        }
        private void OnTempConfigFileDeleted(string filePath)
        {
            WriteResult("\r\n>> Deleted temporary config file. Path={0}\r\n", filePath);
        }

        private void ShowXml(string xml)
        {
            XmlViewTextBox.Text = xml;
        }

 
        private void WriteStartResult(string resultName)
        {
            if (ConsoleTextBox.Text.Length > 0)
            {
                // Add space if console has existing text.
                ConsoleTextBox.AppendText(string.Format("\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n"));
            }
            ConsoleTextBox.AppendText(string.Format("\r\n========================"));
            ConsoleTextBox.AppendText(string.Format("\r\n== {0}", resultName));
            ConsoleTextBox.AppendText(string.Format("\r\n========================\r\n"));
        }

        private void WriteResult(string format, params object[] vals)
        {
            ConsoleTextBox.AppendText(string.Format(format, vals));
        }
        private void WriteErrorResult(Exception ex)
        {
            
            ConsoleTextBox.AppendText(string.Format("\r\n****** {0} ******\r\n", ex.GetType().Name));
            if (enableVerboseOutputCheckBox.Checked)
            {
                ConsoleTextBox.AppendText(string.Format("[ Message ]\r\n\t{0}\r\n", ex));
            }
            else
            {
                ConsoleTextBox.AppendText(string.Format("[ Message ]\r\n\t{0}\r\n", ex.Message));
            }
           
            if (ex is ConfigurationErrorsException)
            {
                var cex = ex as ConfigurationErrorsException;
                ConsoleTextBox.AppendText(string.Format("[ XML Line # ] {0}\r\n", cex.Line));
                ConsoleTextBox.AppendText(string.Format("[ {0} Errors ]\r\n", cex.Errors.Count));
                // If error count == 1, it's the error we already printed.
                if (cex.Errors.Count > 1)
                {
                    foreach (ConfigurationErrorsException err in cex.Errors)
                    {
                        ConsoleTextBox.AppendText(string.Format("\t+ {0}\r\n", err.Message));
                    }
                }
                //cex.Errors
                if (false && ex.InnerException != null)
                {
                    ConsoleTextBox.AppendText(string.Format("\r\n------ {0} ------\r\n", cex.GetBaseException().GetType().Name));
                    ConsoleTextBox.AppendText(string.Format("[ Message ]\r\n\t{0}\r\n", cex.GetBaseException().Message));
                }
            }
            //ConsoleTextBox.AppendText(string.Format("[ StackTr ]\r\n\t{0}\r\n", ex.StackTrace));
            ConsoleTextBox.AppendText("\r\n*****************\r\n\r\n");
        }
        private void WriteEndResult()
        {

            ConsoleTextBox.AppendText(string.Format("\r\n========================"));
            ConsoleTextBox.AppendText(string.Format("\r\n== {0}", "Demo Complete!"));
            ConsoleTextBox.AppendText(string.Format("\r\n========================\r\n"));

            //ConsoleTextBox.AppendText("\r\n*** Demo Complete! **\r\n\r\n");
        }

        #endregion


        private void RefreshSampleGroupsConfig(string selectedGroupName)
        {
            //ApplicationConfig.SamplesSection.Instance.
            sampleGroupsConfigBindingSource.Clear();
            CurrentSampleGroup = null;

            foreach (SampleGroup sampleGroup in SamplesSection.Instance.SampleGroups)
            {
                sampleGroupsConfigBindingSource.Add(sampleGroup);
            }

            if (!string.IsNullOrEmpty(selectedGroupName) && SamplesSection.Instance.SampleGroups[selectedGroupName] != null)
            {
                sampleGroupsListBox.SelectedItem = SamplesSection.Instance.SampleGroups[selectedGroupName];
            }
            else
            {
                sampleGroupsListBox.SelectedIndex = 0;
                selectedGroupName = ((SampleGroup)sampleGroupsListBox.SelectedItem).Name;
            }

            CurrentSampleGroup = ((SampleGroup)sampleGroupsListBox.SelectedItem);

            RefreshSamplesConfig("");
        }

        private void RefreshSamplesConfig(string selectedSampleName)
        {
            //ApplicationConfig.SamplesSection.Instance.
            samplesConfigBindingSource.Clear();
            CurrentSample = null;

            if (CurrentSampleGroup != null)
            {
                foreach (Sample sample in SamplesSection.Instance.SampleGroups[CurrentSampleGroup.Name].Samples)
                {
                    samplesConfigBindingSource.Add(sample);
                }
            }

            if (!string.IsNullOrEmpty(selectedSampleName)
                && CurrentSampleGroup != null
                && SamplesSection.Instance.SampleGroups[CurrentSampleGroup.Name].Samples[selectedSampleName] != null)
            {
                samplesListBox.SelectedItem = SamplesSection.Instance.SampleGroups[CurrentSampleGroup.Name].Samples[selectedSampleName];
            }
            else
            {
                //if ()
                //samplesListBox.SetSelected(0, true);
                //samplesListBox.SelectedIndex = 0;
                //selectedGroupName = ((SampleGroup)sampleGroupsListBox.SelectedItem).name;
            }

            if (samplesConfigBindingSource.Count > 0)
            {
                //CurrentSample = ((Sample)samplesListBox.SelectedItem);
                SetCurrentSample();
            }
            else
            {

            }
        }

        private void RunDemoButton_Click(object sender, EventArgs e)
        {
            RunCurrentSample();
        }



        private void sampleGroupsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentSampleGroup = ((SampleGroup)sampleGroupsListBox.SelectedItem);

            //string selectedGroupName = ((SampleGroup)sampleGroupsListBox.SelectedItem).name;
            RefreshSamplesConfig("");
        }

        private void samplesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetCurrentSample();

        }

        private void SetCurrentSample()
        {
            CurrentSample = ((Sample)samplesListBox.SelectedItem);

            StringBuilder infoText = new StringBuilder();

            //infoText.AppendFormat("Sample: {0}\r\n", CurrentSample.label);
            selectedSampleInfoGroupBox.Text = "SAMPLE: " + CurrentSample.Label;
            infoText.AppendFormat("Description: {0}\r\n", CurrentSample.Description);
            infoText.AppendFormat("Error Demo: {0}\r\n", (CurrentSample.IsValid ? "Yes" : "No"));

            if (!string.IsNullOrEmpty(CurrentSample.FileName))
            {
                infoText.AppendFormat("File: {0}", CurrentSample.FileName);
            }
            else
            {
                infoText.AppendFormat("File: Temporary file");
            }


            //infoText.AppendFormat("Sample: {0}", "");

            selectedSampleInfoTextBox.Text = infoText.ToString();
        }



        protected void OnBeforeRunCurrentSample()
        {
            if (enablePreloadConfigFileCheckBox.Checked)
            {
                WriteResult("[ OPTION 'Enable preload' is enabled: All sections and groups will be preloaded when config file is first opened using ConfigurationManager.OpenMappedExeConfiguration. ]\r\n");
            }
        }


        /// <summary>
        /// TODO: This method has poor design. We should be able to handle all fo this without a case statement. Will fix later...
        /// </summary>
        private void RunCurrentSample()
        {
            OnBeforeRunCurrentSample();
            try
            {
                //string selectedSampleName = ((Sample)samplesListBox.SelectedItem).name;
                //string selectedSampleIdentifier = string.Concat(CurrentSampleGroup.Name, "/", CurrentSample.Name);
                //string selectedSampleFileName = CurrentSample.FileName;
                // FUTURE USE.
                //string selectedSampleFilePath = string.Concat(CurrentSampleGroup.Name, "\\", CurrentSample.FileName);
                switch (CurrentSampleGroup.Name)
                {
                    case "Collections":
                        RunConfigDemo_Collections(!string.IsNullOrEmpty(CurrentSample.FileName)
                   ? InitConfigDemoFromSample(CurrentSampleGroup.Name, CurrentSample.FileName)
                   : InitConfigDemoFromTemp(CurrentSampleGroup.Name,
                       new CollectionsTempSampleConfigFileCreator(CurrentSample.Name)));

                        break;

                    case "ExternalType":
                        RunConfigDemo_ExternalType(!string.IsNullOrEmpty(CurrentSample.FileName)
                   ? InitConfigDemoFromSample(CurrentSampleGroup.Name, CurrentSample.FileName)
                   : InitConfigDemoFromTemp(CurrentSampleGroup.Name,
                       new ExternalTypeTempSampleConfigFileCreator(CurrentSample.Name)));

                        break;
                  
                    case "Inheritance":

                        
                        RunConfigDemo_SchoolRegistry(!string.IsNullOrEmpty(CurrentSample.FileName)
                   ? InitConfigDemoFromSample(CurrentSampleGroup.Name, CurrentSample.FileName)
                   : InitConfigDemoFromTemp(CurrentSampleGroup.Name,
                       new InheritanceTempSampleConfigFileCreator(CurrentSample.Name)));

                        break;

                    case "SharedElements":
                        RunConfigDemo_SharedElements(!string.IsNullOrEmpty(CurrentSample.FileName)
                  ? InitConfigDemoFromSample(CurrentSampleGroup.Name, CurrentSample.FileName)
                  : InitConfigDemoFromTemp(CurrentSampleGroup.Name,
                      new SharedElementsTempSampleConfigFileCreator(CurrentSample.Name)));

                        break;

                    case "Simple":
                        

                        RunConfigDemo_Simple(!string.IsNullOrEmpty(CurrentSample.FileName)
                  ? InitConfigDemoFromSample(CurrentSampleGroup.Name, CurrentSample.FileName)
                  : InitConfigDemoFromTemp(CurrentSampleGroup.Name,
                      new SimpleTempSampleConfigFileCreator(CurrentSample.Name)));

                        break;

                    case "Validation":
                        // TODO: With a setup like this, we dont really need case statements. Remove it 
                        // once all demos work like this.
                        
                        RunConfigDemo_Validation(!string.IsNullOrEmpty(CurrentSample.FileName)
                  ? InitConfigDemoFromSample(CurrentSampleGroup.Name, CurrentSample.FileName)
                  : InitConfigDemoFromTemp(CurrentSampleGroup.Name,
                      new ValidationTempSampleConfigFileCreator(CurrentSample.Name)));
                        
                        break;

                 

                    case "/":
                        throw new NotImplementedException();
                        break;


                    default:
                        throw new Exception("Invalid demo group. Identifier=\r\n" + CurrentSampleGroup.Name);
                        break;
                }

                /*
                switch (selectedSampleIdentifier)
                {

                    case "ExternalType/CreateAndRead":
                        //RunConfigDemo_ExternalTypes("");
                        RunConfigDemo_ExternalType(InitConfigDemoFromTemp(CurrentSampleGroup.Name, new ExternalTypeTempSampleConfigFileCreator()));
                        

                        break;
                    case "ExternalType/ReadFileValid":
                        //RunConfigDemo_ExternalTypes(selectedSampleFileName);
                        RunConfigDemo_ExternalType(InitConfigDemoFromSample(CurrentSampleGroup.Name, selectedSampleFileName));
                        

                        break;
                    case "ExternalType/ReadFileInvalid":
                        //RunConfigDemo_ExternalTypes(selectedSampleFileName);
                        RunConfigDemo_ExternalType(InitConfigDemoFromSample(CurrentSampleGroup.Name, selectedSampleFileName));
                        
                        break;

                    case "Inheritance/CreateAndReadSchool":

                        //RunConfigDemo_SchoolRegistry("");
                        RunConfigDemo_SchoolRegistry(InitConfigDemoFromTemp(CurrentSampleGroup.Name, new SchoolRegistryTempSampleConfigFileCreator()));
                        

                        break;
                    case "Inheritance/ReadSchool":
                        //throw new Exception("Not yet implemented.");
                        RunConfigDemo_SchoolRegistry(InitConfigDemoFromSample(CurrentSampleGroup.Name, selectedSampleFileName));
                        
                        break;


                    case "Validation/CreateAndReadInvalidProperty":
                        // TODO: With a setup like this, we dont really need case statements. Remove it 
                        // once all demos work like this.
                        //RunConfigDemo_Validation("");
                        RunConfigDemo_Validation(InitConfigDemoFromTemp(CurrentSampleGroup.Name, new ValidationTempSampleConfigFileCreator()));
                        break;

                    case "Validation/ReadFileInvalidBazProperty":
                        RunConfigDemo_Validation(InitConfigDemoFromSample(CurrentSampleGroup.Name, selectedSampleFileName));
                        break;

                    case "Validation/ReadFileValid":
                        RunConfigDemo_Validation(InitConfigDemoFromSample(CurrentSampleGroup.Name, selectedSampleFileName));
                        break;

                    case "/":
                        throw new NotImplementedException();
                        break;


                    default:
                        throw new Exception("Invalid demo selection. Identifier=\r\n" + selectedSampleIdentifier);
                        break;
                }*/
            }
            catch (Exception ex)
            {
                WriteErrorResult(ex);
                //ConsoleTextBox.AppendText("\r\nException Occured: " + ex + "\r\n");
            }
        }

        private void clearConsoleLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ConsoleTextBox.Clear();
        }
    }
}
