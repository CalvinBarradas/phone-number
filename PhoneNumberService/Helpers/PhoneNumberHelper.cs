using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PhoneNumberService.Helpers
{
    public static class PhoneNumberHelper
    {
        private static readonly string _extensionReplacement = "0";

        private static readonly string _patternUKExtension = "^\\+44";

        // order matters here last regex pattern match wins
        private static readonly List<(string, string)> _regexNumberFormatArray = new List<(string, string)>()
        {
            ("^01\\d{8}$", "{0:0#### #####}"),
            ("^01\\d{9}$", "{0:0#### ######}"),
            ("^011\\d{8}$", "{0:0### ### ####}"),
            ("^01\\d1\\d{7}$", "{0:0### ### ####}"),
            ("^013397\\d{5}$", "{0:0##### #####}"),
            ("^013398\\d{5}$", "{0:0##### #####}"),
            ("^013873\\d{5}$", "{0:0##### #####}"),
            ("^015242\\d{5}$", "{0:0##### #####}"),
            ("^015394\\d{5}$", "{0:0##### #####}"),
            ("^015395\\d{5}$", "{0:0##### #####}"),
            ("^015396\\d{5}$", "{0:0##### #####}"),
            ("^016973\\d{5}$", "{0:0##### #####}"),
            ("^016974\\d{5}$", "{0:0##### #####}"),
            ("^016977\\d{4}$", "{0:0##### ####}"),
            ("^016977\\d{5}$", "{0:0##### #####}"),
            ("^017683\\d{5}$", "{0:0##### #####}"),
            ("^017684\\d{5}$", "{0:0##### #####}"),
            ("^017687\\d{5}$", "{0:0##### #####}"),
            ("^019467\\d{5}$", "{0:0##### #####}"),
            ("^019755\\d{5}$", "{0:0##### #####}"),
            ("^019756\\d{5}$", "{0:0##### #####}"),
            ("^02\\d{9}$", "{0:0## #### ####}"),
            ("^03\\d{9}$", "{0:0### ### ####}"),
            ("^05\\d{9}$", "{0:0#### ######}"),
            ("^07\\d{9}$", "{0:0#### ######}"),
            ("^0800\\d{6}$", "{0:0### ######}"),
            ("^08\\d{9}$", "{0:0### ### ####}"),
            ("^09\\d{9}$", "{0:0### ### ####}")
        };

        public static bool IsUKExtension(string phoneNumber)
        {
            return Regex.IsMatch(phoneNumber, _patternUKExtension);
        }

        public static string RemoveUKExtension(string phoneNumber)
        {
            return Regex.Replace(phoneNumber, _patternUKExtension, _extensionReplacement);
        }

        public static string FormatNumber(string phoneNumber)
        {
            double phoneNumberDouble;

            string formatPattern = null;

            for (int i = _regexNumberFormatArray.Count - 1; i >= 0; i--)
            {
                if (Regex.IsMatch(phoneNumber, _regexNumberFormatArray[i].Item1))
                {
                    formatPattern = _regexNumberFormatArray[i].Item2;
                    break;
                }
            }

            _regexNumberFormatArray.ForEach(item =>
            {
                if (Regex.IsMatch(phoneNumber, item.Item1))
                {
                    formatPattern = item.Item2;
                }
            });

            return formatPattern != null && Double.TryParse(phoneNumber, out phoneNumberDouble)
                ? String.Format(formatPattern, phoneNumberDouble)
                : phoneNumber;
        }
    }
}