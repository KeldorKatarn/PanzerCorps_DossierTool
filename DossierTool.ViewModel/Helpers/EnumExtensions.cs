// <copyright file="EnumExtensions.cs" company="VacuumBreather">
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
    using System.Diagnostics.Contracts;
    using Decorators;
    using Model;

    #endregion

    /// <summary>
    ///     Contains extention methods for the <see cref="UnitType" /> enumeration used for validation.
    /// </summary>
    public static class EnumExtensions
    {
        #region Class Methods

        /// <summary>
        ///     Returns the display name for the given <see cref="Motorization" /> enumeration value.
        /// </summary>
        /// <param name="motorization">The motorization.</param>
        /// <returns>
        ///     The display name for the given <see cref="Motorization" /> enumeration value.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException"><paramref name="motorization" /> is out of range.</exception>
        [Pure]
        public static string ToDisplayName(this Motorization motorization)
        {
            switch (motorization)
            {
                case Motorization.NotMotorized:
                    return "Not Motorized";

                case Motorization.Motorized:
                    return "Motorized";

                case Motorization.SelfPropelled:
                    return "Self Propelled";

                default:
                    throw new ArgumentOutOfRangeException("motorization");
            }
        }

        #endregion
    }
}