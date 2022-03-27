﻿using System.Globalization;

namespace Atata
{
    public class LowerMergedTermFormatter : ITermFormatter
    {
        public string Format(string[] words)
        {
            return string.Concat(words).ToLower(CultureInfo.CurrentCulture);
        }
    }
}
