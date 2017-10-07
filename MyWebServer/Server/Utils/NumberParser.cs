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

        public static decimal TryParseDecimalOrReturnZero(string number)
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
                    return 0;
                }
            }

            return result;
        }

        public static int ParseInt(string number)
        {
            int result;

            try
            {
                result = int.Parse(number);
            }
            catch (Exception)
            {
                throw new ArgumentException("Invalid string");
            }

            return result;
        }
    }
}
