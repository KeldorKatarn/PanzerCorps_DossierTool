// <copyright file="HigherUnit.cs" company="VacuumBreather">
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

namespace DossierTool.Model
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Runtime.Serialization;

    #endregion

    /// <summary>
    ///     Represents an organizational unit containing Panzer Corps core units;
    ///     like a division, regiment or the entire corps.
    /// </summary>
    [DataContract]
    [KnownType(typeof(Unit))]
    public class HigherUnit : UnitBase
    {
        #region Readonly & Static Fields

        [DataMember(Name = "Subordinates")]
        private readonly List<UnitBase> _subordinates = new List<UnitBase>();

        #endregion

        #region Object Invariant

        [ContractInvariantMethod]
        private void Invariant()
        {
            Contract.Invariant(this._subordinates != null, "Subordinates must never be null.");
            Contract.Invariant(Contract.ForAll(this._subordinates, s => s != null),
                               "Subordinates must not contain any null values.");
        }

        #endregion

        #region Instance Properties

        /// <summary>
        ///     Gets or sets the <see cref="Unit">Units</see> or <see cref="HigherUnit">HigherUnits</see>
        ///     that are subordinates of this <see cref="HigherUnit" />.
        /// </summary>
        /// <value>
        ///     The <see cref="Unit">Units</see> or <see cref="HigherUnit">HigherUnits</see>
        ///     that are subordinates of this <see cref="HigherUnit" />
        /// </value>
        public virtual IEnumerable<UnitBase> Subordinates
        {
            get
            {
                return this._subordinates;
            }
        }

        #endregion

        #region Instance Methods

        /// <summary>
        ///     Adds a subordinate to the list of subordinates.
        /// </summary>
        /// <param name="subordinate">The subordinate to add.</param>
        /// <exception cref="ArgumentNullException">When <paramref name="subordinate" /> is null.</exception>
        /// <exception cref="ArgumentException">When <paramref name="subordinate" /> is already on the list.</exception>
        [SuppressMessage("Microsoft.Contracts", "RequiresAtCall-!this.Subordinates.Contains(subordinate)")]
        public virtual void AddSubordinate(UnitBase subordinate)
        {
            Contract.Requires<ArgumentNullException>(subordinate != null);
            Contract.Requires<ArgumentException>(!Subordinates.Contains(subordinate));

            this._subordinates.Add(subordinate);
            subordinate.Superior = this;
        }

        /// <summary>
        ///     Removes a subordinate from the list of subordinates.
        /// </summary>
        /// <param name="subordinate">The subordinate to remove.</param>
        /// <exception cref="ArgumentNullException">When <paramref name="subordinate" /> is null.</exception>
        /// <exception cref="ArgumentException">When <paramref name="subordinate" /> is not on the list.</exception>
        public virtual void RemoveSubordinate(UnitBase subordinate)
        {
            Contract.Requires<ArgumentNullException>(subordinate != null);
            Contract.Requires<ArgumentException>(Subordinates.Contains(subordinate));

            this._subordinates.Remove(subordinate);
            subordinate.Superior = null;
        }

        #endregion
    }
}