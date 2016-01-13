// <copyright file="BoolToVisibilityConverter.cs" company="VacuumBreather">
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
    using System.Windows;
    using System.Windows.Data;

    #endregion

    /// <summary>
    ///     A customizable boolean to visibility converter.
    /// </summary>
    public class BoolToVisibilityConverter : IValueConverter
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="BoolToVisibilityConverter" /> class.
        /// </summary>
        public BoolToVisibilityConverter()
        {
            OnTrue = Visibility.Visible;
            OnFalse = Visibility.Collapsed;
        }

        #endregion

        #region Instance Properties

        /// <summary>
        ///     Gets or sets the visibility to map to false.
        /// </summary>
        /// <value>
        ///     The visibility to map to false.
        /// </value>
        public Visibility OnFalse { get; set; }

        /// <summary>
        ///     Gets or sets the visibility to map to true.
        /// </summary>
        /// <value>
        ///     The visibility to map to true.
        /// </value>
        public Visibility OnTrue { get; set; }

        #endregion

        #region IValueConverter Members

        /// <summary>
        ///     Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        ///     A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return true.Equals(value) ? OnTrue : OnFalse;
        }

        /// <summary>
        ///     Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        ///     A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return OnTrue.Equals(value);
        }

        #endregion
    }
}