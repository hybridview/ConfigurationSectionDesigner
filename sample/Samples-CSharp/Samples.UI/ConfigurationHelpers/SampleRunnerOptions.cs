namespace Samples.UI.ConfigurationHelpers
{
    public class SampleRunnerOptions
    {
        public bool EnablePreloadConfigFileCheckBox { get; set; }


        public SampleRunnerOptions(bool enablePreloadConfigFileCheckBox) {
            EnablePreloadConfigFileCheckBox = enablePreloadConfigFileCheckBox;
        }
    }
}