// <copyright file="StringValidator.cs" company="VacuumBreather">
//      Copyright © 2014 VacuumBreather. All rights reserved.
// </copyright>
// <license type="X11/MIT">
//      Permission is hereby granted, free of charge, to any person obtaining a copy
//      of this software and associated documentation files (the "Software"), to deal
//      in the Software without restriction, including without limitation the rights
//      to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//      copies of the Software, and to permit persons to whom the Software is
//      furnished to do so, subject to the following conditions:
//      The above copyright notice and this permission notice shall be included in
//      all copies or substantial portions of the Software.
//      THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//      IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//      FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//      AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//      LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//      OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//      THE SOFTWARE.
// </license>

namespace DossierTool.Model.Helpers
{
    #region Using Directives

    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Xml;

    #endregion

    /// <summary>
    ///     Helper class to validate strings.
    /// </summary>
    public static class StringValidator
    {
        #region Class Methods

        /// <summary>
        ///     Determines whether the specified string is a valid name.
        /// </summary>
        /// <param name="s">The string to check.</param>
        /// <returns>
        ///     <c>true</c> if the specified string is a valid name; otherwise, <c>false</c>.
        /// </returns>
        [Pure]
        public static bool IsValidString(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return false;
            }

            bool doesNotStartWithWhiteSpace = !s.StartsWith(" ");
            bool doesNotEndWithWhiteSpace = !s.EndsWith(" ");
            bool areAllCharsXmlChars = s.All(XmlConvert.IsXmlChar);
            bool areAllCharsValid =
                s.All(c => (char.IsLetterOrDigit(c) || char.IsPunctuation(c) || char.IsSymbol(c) || c == ' '));

            return doesNotStartWithWhiteSpace && doesNotEndWithWhiteSpace && areAllCharsXmlChars && areAllCharsValid;
        }

        #endregion
    }
}