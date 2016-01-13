// <copyright file="ConcatenationConverter.cs" company="VacuumBreather">
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
    using System.Linq;
    using System.Windows.Data;
    using ViewModel.Decorators;

    #endregion

    /// <summary>
    ///     A string concatenation converter.
    /// </summary>
    public class ConcatenationConverter : IMultiValueConverter
    {
        #region IMultiValueConverter Members

        /// <summary>
        ///     Converts source values to a value for the binding target. The data binding engine calls this method when it
        ///     propagates the values from source bindings to the binding target.
        /// </summary>
        /// <param name="values">
        ///     The array of values that the source bindings in the
        ///     <see
        ///         cref="T:System.Windows.Data.MultiBinding" />
        ///     produces. The value
        ///     <see
        ///         cref="F:System.Windows.DependencyProperty.UnsetValue" />
        ///     indicates that the source binding has no value to provide for conversion.
        /// </param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        ///     A converted value.If the method returns null, the valid null value is used.A return value of
        ///     <see
        ///         cref="T:System.Windows.DependencyProperty" />
        ///     .<see cref="F:System.Windows.DependencyProperty.UnsetValue" /> indicates that the converter did not produce a
        ///     value, and that the binding will use the
        ///     <see
        ///         cref="P:System.Windows.Data.BindingBase.FallbackValue" />
        ///     if it is available, or else will use the default value.A return value of
        ///     <see
        ///         cref="T:System.Windows.Data.Binding" />
        ///     .<see cref="F:System.Windows.Data.Binding.DoNothing" /> indicates that the binding does not transfer the value or
        ///     use the
        ///     <see
        ///         cref="P:System.Windows.Data.BindingBase.FallbackValue" />
        ///     or the default value.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.Cast<string>()
                         .Where(s => s != Equipment.None.ShortName)
                         .Aggregate(string.Empty, (a, b) => string.IsNullOrEmpty(a) ? b : string.Concat(a, "\n", b));
        }

        /// <summary>
        ///     Converts a binding target value to the source binding values.
        /// </summary>
        /// <param name="value">The value that the binding target produces.</param>
        /// <param name="targetTypes">
        ///     The array of types to convert to. The array length indicates the number and types of values
        ///     that are suggested for the method to return.
        /// </param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        ///     An array of values that have been converted from the target value back to the source values.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}