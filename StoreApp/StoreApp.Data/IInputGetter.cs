using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Data
{
    interface IInputGetter
    {
        // This method should get a non-empty input string from the user.
        public string GetInputString(string message);
        // This method should get an integer from the user.
        public int GetInputInt(string message);
    }
}
