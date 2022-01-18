using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConfigurationSectionDesigner
{
    class VsUtils
    {
        internal sealed class HourglassHelper : IDisposable
        {
            private readonly Cursor _previousCursor;

            public HourglassHelper()
            {
                //Change the cursor to an hourglass
                _previousCursor = Cursor.Current;
                Cursor.Current = Cursors.WaitCursor;
            }

            public void Dispose()
            {
                //Clear the hourglass cursor, and restore
                Cursor.Current = _previousCursor;
            }
        }
    }
}
