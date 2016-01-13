// <copyright file="ButtonVisibilityConverter.cs" company="VacuumBreather">
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

namespace DossierTool.View.ValueConverters
{
    #region Using Directives

    using System;
    using System.Globalization;
    using ViewModel.Dialogs;

    #endregion

    /// <summary>
    ///     A converter that determines button visibility based on MessageBoxButtons enumeration values.
    /// </summary>
    public class ButtonVisibilityConverter : BoolToVisibilityConverter
    {
        #region Instance Properties

        /// <summary>
        ///     Gets or sets a value indicating whether the converter should check the CANCEL button.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the converter should check the CANCEL button; otherwise, <c>false</c>.
        /// </value>
        public bool CheckCancelButton { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the converter should check the NO button.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the converter should check the NO button; otherwise, <c>false</c>.
        /// </value>
        public bool CheckNoButton { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the converter should check the OK button.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the converter should check the OK button; otherwise, <c>false</c>.
        /// </value>
        public bool CheckOkButton { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the converter should check the YES button.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the converter should check the YES button; otherwise, <c>false</c>.
        /// </value>
        public bool CheckYesButton { get; set; }

        #endregion

        #region Instance Methods

        /// <summary>
        ///     Converts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var buttons = (DialogButtons)value;

            bool showButton = ((CheckOkButton && (buttons == DialogButtons.OK || buttons == DialogButtons.OKCancel)) ||
                               (CheckYesButton &&
                                (buttons == DialogButtons.YesNo || buttons == DialogButtons.YesNoCancel)) ||
                               (CheckNoButton &&
                                (buttons == DialogButtons.YesNo || buttons == DialogButtons.YesNoCancel)) ||
                               (CheckCancelButton &&
                                (buttons == DialogButtons.OKCancel || buttons == DialogButtons.YesNoCancel)));

            return base.Convert(showButton, targetType, parameter, culture);
        }

        /// <summary>
        ///     Converts the back.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}