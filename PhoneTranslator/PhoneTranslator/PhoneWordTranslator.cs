using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneTranslator
{
    public static class PhoneWordTranslator
    {
        public static string ToNumber(string raw)
        {
            if(String.IsNullOrWhiteSpace(raw))
            {
                return null;
            }

            raw = raw.ToUpperInvariant();
            var newNumber = new StringBuilder();

            foreach(var c in raw)
            {
                if(" -0123456789".Contains(c))
                {
                    newNumber.Append(c);
                } else
                {
                    var result = TranslateToNumber(c);
                    if(result != null)
                    {
                        newNumber.Append(result);
                    } else
                    {
                        return null;
                    }
                }
            }

            return newNumber.ToString();
        }

        static bool Contains(this string keyString, char c)
        {
            return keyString.IndexOf(c) >= 0;
        }

        static readonly string[] Digits =
        {
            "ABC", "DEF", "GHI", "JKL", "MNO", "PQRS", "TUV", "WXYZ"
        };

        static int? TranslateToNumber(char c)
        {
            for(int i = 0; i < Digits.Length; i++)
            {
                if(Digits[i].Contains(c))
                {
                    return 2 + i;
                }
            }
            
            return null;
        }
    }
}
