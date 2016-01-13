// <copyright file="LinqExtensions.cs" company="VacuumBreather">
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

namespace DossierTool.ViewModel.Helpers
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    ///     Contains new LINQ operators.
    /// </summary>
    public static class LinqExtensions
    {
        #region Class Methods

        /// <summary>
        ///     Turns a single value into a strongly typed enumberable containing this value.
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>A strongly typed enumberable containing the value.</returns>
        public static IEnumerable<T> FromSingle<T>(T value)
        {
            yield return value;
        }

        /// <summary>
        ///     Select values by using the current and the previous value in an enumeration.
        /// </summary>
        /// <typeparam name="TSource">The type of the source sequence.</typeparam>
        /// <typeparam name="TResult">The type of the result sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="initialSelector">The initial selector for the first value in the sequence.</param>
        /// <param name="selector">The selector function for all following values in the sequence and their predecessor.</param>
        /// <returns></returns>
        public static IEnumerable<TResult> SelectByPrevious<TSource, TResult>(this IEnumerable<TSource> source,
                                                                              Func<TSource, TResult> initialSelector,
                                                                              Func<TSource, TSource, TResult> selector)
        {
            IEnumerator<TSource> enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                yield break;
            }

            yield return initialSelector(enumerator.Current);

            TSource previous = enumerator.Current;

            while (enumerator.MoveNext())
            {
                yield return selector(previous, enumerator.Current);

                previous = enumerator.Current;
            }
        }

        #endregion
    }
}