using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.WebUI.Helpers
{
    class Utilities
    {
        public bool IsNullOrEmpty(string text)
        {
            if (text == null | text == "") return true;
            return false;
        }
        public void Check(Func<string, bool> condition, string text, Action IfTrue, Action IfFalse)
        {
            if (!condition.Invoke(text))
            {
                IfTrue.Invoke();
            }
            else
            {
                IfFalse.Invoke();
            }
        }
        public void Check(Func<string, bool> condition, string text, Action IfTrue)
        {
            if (!condition.Invoke(text))
            {
                IfTrue.Invoke();
            }
        }
    }
}
