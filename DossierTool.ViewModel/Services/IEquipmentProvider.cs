// <copyright file="IEquipmentProvider.cs" company="VacuumBreather">
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

    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using Decorators;
    using Model;
    using Model.Helpers;

    #endregion

    /// <summary>
    ///     Interface for a EquipmentProvider.
    /// </summary>
    [ContractClass(typeof(EquipmentProviderContracts))]
    public interface IEquipmentProvider
    {
        #region Instance Properties

        /// <summary>
        ///     Gets all known equipments.
        /// </summary>
        /// <value>All known equipments.</value>
        IEnumerable<Equipment> Equipments { get; }

        #endregion

        #region Instance Methods

        /// <summary>
        ///     Finds the <see cref="Equipment" /> with the specified short name and nationality.
        /// </summary>
        /// <param name="shortName">The short name of the <see cref="Equipment" />.</param>
        /// <param name="nationality">The nationality of the <see cref="Equipment" />.</param>
        /// <param name="type">The type.</param>
        /// <returns>
        ///     The <see cref="Equipment" /> with the specified short name and nationality or the default if no such equipment
        ///     could be found.
        /// </returns>
        Equipment Find(string shortName, Nationality nationality, UnitType type);

        /// <summary>
        ///     Finds the <see cref="Equipment" /> with the specified ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>
        ///     The <see cref="Equipment" /> with the specified ID or the default if no such equipment could be found.
        /// </returns>
        Equipment Find(int id);

        #endregion
    }

    [ContractClassFor(typeof(IEquipmentProvider))]
    internal abstract class EquipmentProviderContracts : IEquipmentProvider
    {
        #region IEquipmentProvider Members

        /// <summary>
        ///     Gets all known equipments.
        /// </summary>
        /// <value>All known equipments.</value>
        public IEnumerable<Equipment> Equipments
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnumerable<Equipment>>() != null);

                throw new NotImplementedException();
            }
        }

        /// <summary>
        ///     Finds the <see cref="Equipment" /> with the specified short name.
        /// </summary>
        /// <param name="shortName">The short name of the <see cref="Equipment" />.</param>
        /// <param name="nationality">The nationality of the <see cref="Equipment" />.</param>
        /// <param name="type">The type.</param>
        /// <returns>
        ///     The <see cref="Equipment" /> with the specified short name and nationality or the default if no such equipment
        ///     could be found.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Equipment Find(string shortName, Nationality nationality, UnitType type)
        {
            Contract.Requires<ArgumentNullException>(shortName != null);
            Contract.Requires<ArgumentOutOfRangeException>(nationality.IsValid());
            Contract.Requires<ArgumentOutOfRangeException>(type.IsValid());

            throw new NotImplementedException();
        }

        /// <summary>
        ///     Finds the <see cref="Equipment" /> with the specified ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>
        ///     The <see cref="Equipment" /> with the specified ID or the default if no such equipment could be found.
        /// </returns>
        public Equipment Find(int id)
        {
            Contract.Requires<ArgumentOutOfRangeException>(id >= 0);

            throw new NotImplementedException();
        }

        #endregion
    }
}