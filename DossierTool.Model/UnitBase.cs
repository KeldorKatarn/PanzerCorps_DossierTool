// <copyright file="UnitBase.cs" company="VacuumBreather">
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
    using System.Diagnostics.Contracts;
    using System.Runtime.Serialization;
    using Helpers;

    #endregion

    /// <summary>
    ///     Base class for regular units of Panzer Corps and hierarchy units used only for organizing units.
    /// </summary>
    [DataContract(IsReference = true)]
    [KnownType(typeof(HigherUnit))]
    public abstract class UnitBase
    {
        #region Constants

        /// <summary>
        ///     The default name of a newly created unit.
        /// </summary>
        public const string DefaultName = "New Unit";

        #endregion

        #region Fields

        [DataMember(Name = "Name", Order = 0)]
        private string _name;

        [DataMember(Name = "Superior", Order = 1)]
        private HigherUnit _superior;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="UnitBase" /> class.
        /// </summary>
        protected UnitBase()
        {
            Contract.Assume(StringValidator.IsValidString(DefaultName));
            this._name = DefaultName;
        }

        #endregion

        #region Object Invariant

        [ContractInvariantMethod]
        private void Invariant()
        {
            Contract.Invariant(StringValidator.IsValidString(this._name), "Name must be a valid string.");
        }

        #endregion

        #region Instance Properties

        /// <summary>
        ///     Gets or sets the name of the unit.
        /// </summary>
        /// <value>The name of the unit.</value>
        /// <exception cref="ArgumentNullException">When value is null.</exception>
        /// <exception cref="ArgumentException">When value is an invalid string.</exception>
        public virtual string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                Contract.Requires<ArgumentException>(StringValidator.IsValidString(value));

                this._name = value;
            }
        }

        /// <summary>
        ///     Gets or sets the superior of this unit in the hierarchy.
        /// </summary>
        /// <value>The superior of this unit in the hierarchy.</value>
        public virtual HigherUnit Superior
        {
            get
            {
                return this._superior;
            }
            set
            {
                this._superior = value;
            }
        }

        #endregion

        #region Instance Methods

        /// <summary>
        ///     Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        ///     A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            Contract.Assume(Name != null);

            return Name;
        }

        #endregion
    }
}