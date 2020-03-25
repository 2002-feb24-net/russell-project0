using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Data
{
    public class InputGetter : IInputGetter
    {
        // This method gets a non-empty input string from the user.
        public string GetInputString(string msg)
        {
            string input = null;
            do
            {
                Console.Write(msg);
                input = Console.ReadLine();
            }
            while (string.IsNullOrEmpty(input));
            return input;
        }

        // This method gets an integer from the user.
        public int GetInputInt(string msg)
        {
            bool inputValid = false;
            int inputInt = 0;
            while (!inputValid)
            {
                string strInput = GetInputString(msg);
                try
                {
                    inputInt = int.Parse(strInput);
                    inputValid = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error! Please input a number.");
                }
            }
            return inputInt;
        }
    }
}
