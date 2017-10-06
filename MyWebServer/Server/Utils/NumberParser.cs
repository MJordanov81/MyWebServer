namespace MyWebServer.Server.Utils
{
    using System;

    public static class NumberParser
    {
        public static decimal ParseDecimal(string number)
        {
            decimal result;

            try
            {
                result = decimal.Parse(number);
            }
            catch (Exception)
            {
                try
                {
                    if (number.Contains("."))
                    {
                        number = number.Replace(".", ",");
                        result = decimal.Parse(number);
                    }
                    else
                    {
                        number = number.Replace(",", ".");
                        result = decimal.Parse(number);
                    }
                }
                catch (Exception)
                {
                    throw new ArgumentException("Invalid string");
                }
            }

            return result;
        }
    }
}
