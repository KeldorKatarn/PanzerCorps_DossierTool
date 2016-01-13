// <copyright file="IBonusProvider.cs" company="VacuumBreather">
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

namespace DossierTool.ViewModel.Services
{
    #region Using Directives

    using System.Collections.Generic;
    using Helpers;
    using Model;

    #endregion

    public interface IBonusProvider
    {
        #region Instance Properties

        /// <summary>
        ///     Gets the experience based boni.
        /// </summary>
        /// <value>
        ///     The experience based boni.
        /// </value>
        IDictionary<UnitType, Bonus> ExperienceBoni { get; }

        #endregion

        #region Instance Methods

        /// <summary>
        ///     Gets the hero bonus for the specified hero.
        /// </summary>
        /// <param name="hero">The hero.</param>
        /// <returns>The bonus for the specified hero.</returns>
        Bonus GetHeroBonusFor(Hero hero);

        #endregion
    }
}