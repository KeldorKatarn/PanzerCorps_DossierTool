// <copyright file="CoroutineExtensions.cs" company="VacuumBreather">
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

    using System.Collections.Generic;
    using Caliburn.Micro;

    #endregion

    /// <summary>
    ///     Coroutine helper extension methods.
    /// </summary>
    public static class CoroutineExtensions
    {
        #region Class Methods

        /// <summary>
        ///     Wraps the coroutine into a <see cref="SequentialResult" />.
        /// </summary>
        /// <param name="coroutine">The coroutine.</param>
        public static IResult AsSequentialResult(this IEnumerable<IResult> coroutine)
        {
            return new SequentialResult(coroutine.GetEnumerator());
        }

        /// <summary>
        ///     Executes the coroutine asynchronously.
        /// </summary>
        /// <param name="coroutine">The coroutine.</param>
        public static void ExecuteAsync(this IEnumerable<IResult> coroutine)
        {
            Coroutine.ExecuteAsync(coroutine.GetEnumerator());
        }

        #endregion
    }
}