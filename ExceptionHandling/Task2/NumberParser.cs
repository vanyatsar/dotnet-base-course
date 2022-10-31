using System;

namespace Task2
{
    public class NumberParser : INumberParser
    {
        private int _number;

        public int Parse(string stringValue)
        {
            if (stringValue == null)
            {
                throw new ArgumentNullException();
            }

            try
            {
                _number = Convert.ToInt32(stringValue);
            }
            catch (FormatException)
            {
                throw new FormatException();
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException();
            }
            catch (OverflowException)
            {
                throw new OverflowException();
            }
            return _number;
        }
    }
}