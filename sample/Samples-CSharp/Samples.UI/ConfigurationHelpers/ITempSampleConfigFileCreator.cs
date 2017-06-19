using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samples.UI.ConfigurationHelpers
{
    public interface ITempSampleConfigFileCreator
    {
        string SampleName { get; set; }

        void CreateConfigFile(SampleRunnerOptions options, out string newConfigFilePath);


    }
}
