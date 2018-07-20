using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace CRUDTest.Utils
{
    public static class Helper
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentMethod(string Entity)
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);

            return string.Concat(sf.GetMethod().Name,"_",Entity);
        }
    }
}
