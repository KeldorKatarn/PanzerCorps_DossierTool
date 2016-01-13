// <copyright file="IHeroProvider.cs" company="VacuumBreather">
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
    using System.Runtime.Serialization;
    using Model;

    #endregion

    /// <summary>
    ///     Interface for a service providing access to all heroes.
    /// </summary>
    public interface IHeroProvider
    {
        #region Instance Properties

        /// <summary>
        ///     Gets the heroes.
        /// </summary>
        /// <value>
        ///     The heroes.
        /// </value>
        [DataMember(Name = "Heroes", IsRequired = true)]
        List<Hero> Heroes { get; set; }

        #endregion

        #region Instance Methods

        /// <summary>
        ///     Finds the <see cref="Hero" /> with the specified ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>
        ///     The <see cref="Hero" /> with the specified ID or the default if no such hero could be found.
        /// </returns>
        Hero Find(string id);

        #endregion
    }
}