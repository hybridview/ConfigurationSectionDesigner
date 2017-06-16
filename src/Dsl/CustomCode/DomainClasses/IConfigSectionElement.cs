using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConfigurationSectionDesigner
{
    /// <summary>
    /// Used by the code generator to be able to pick out ConfigurationSections
    /// and ConfigurationSectionGroups more easily
    /// </summary>
    public interface IConfigSectionElement
    {
    }

    public partial class ConfigurationSectionGroup : IConfigSectionElement
    {
    }

    public partial class ConfigurationSection : IConfigSectionElement
    {
    }
}
