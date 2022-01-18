using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ConfigurationSectionDesigner
{
    // These implementations are requested by the DSL tool for an unknown reason,
    // as custom dash patterns are not used by CSD. Included here to avoid compilation
    // errors.

    public partial class ConfigurationElementCollectionShape
    {
        private static ArrayList customOutlineDashPattern;
        protected static ArrayList CustomOutlineDashPattern
        {
            get
            {
                if( customOutlineDashPattern == null )
                    customOutlineDashPattern = new ArrayList( new float[] { 4.0F, 2.0F, 1.0F, 3.0F } );
                return customOutlineDashPattern;
            }
        }
    }

    public partial class ConfigurationSectionGroupShapeBase
    {
        internal static readonly string ConfigurationSectionCompartmentName = "ConfigurationSectionProperties";
        internal static readonly string ConfigurationSectionGroupCompartmentName = "ConfigurationSectionGroupProperties";

        private static ArrayList customOutlineDashPattern;
        protected static ArrayList CustomOutlineDashPattern
        {
            get
            {
                if( customOutlineDashPattern == null )
                    customOutlineDashPattern = new ArrayList( new float[] { 4.0F, 2.0F, 1.0F, 3.0F } );
                return customOutlineDashPattern;
            }
        }
    }
}
