// <copyright file="Unit.cs" company="VacuumBreather">
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
    using Helpers;

    #endregion

    /// <summary>
    ///     Represents a Panzer Corps core unit.
    /// </summary>
    [DataContract]
    public class Unit : UnitBase
    {
        #region Readonly & Static Fields

        [DataMember(Name = "Reports", Order = 4)]
        private readonly List<Report> _reports = new List<Report>();

        #endregion

        #region Fields

        [DataMember(Name = "Type", Order = 0)]
        private UnitType _type = UnitType.Infantry;

        [DataMember(Name = "IsSE", Order = 2)]
        private bool _isSpecial;

        [DataMember(Name = "Nationality", Order = 1)]
        private Nationality _nationality = Nationality.Germany;

        [DataMember(Name = "IsReserve", Order = 3)]
        private bool _isReserve;

        #endregion

        #region Object Invariant

        [ContractInvariantMethod]
        private void Invariant()
        {
            Contract.Invariant(this._type.IsValid());
            Contract.Invariant(this._nationality.IsValid());
            Contract.Invariant(this._reports != null, "Reports must never be null.");
            Contract.Invariant(Contract.ForAll(this._reports, aar => aar != null),
                               "Reports must not contain any null values.");
        }

        #endregion

        #region Instance Properties

        /// <summary>
        ///     Gets or sets a value indicating whether this unit is marked as a reserve unit.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this unit is marked as a reserve unit; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsReserve
        {
            get
            {
                return this._isReserve;
            }
            set
            {
                this._isReserve = value;
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this unit is a SE unit.
        /// </summary>
        /// <value>A value indicating whether this unit is a SE unit.</value>
        public virtual bool IsSpecial
        {
            get
            {
                return this._isSpecial;
            }
            set
            {
                this._isSpecial = value;
            }
        }

        /// <summary>
        ///     Gets or sets the nationality of the unit.
        /// </summary>
        /// <value>The nationality of the unit.</value>
        /// <exception cref="ArgumentOutOfRangeException">When value is an undefined type.</exception>
        public virtual Nationality Nationality
        {
            get
            {
                return this._nationality;
            }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(value.IsValid());

                this._nationality = value;
            }
        }

        /// <summary>
        ///     Gets or sets the scenario After Action Reports of the unit.
        /// </summary>
        /// <value>The scenario After Action Reports of the unit.</value>
        public virtual IEnumerable<Report> Reports
        {
            get
            {
                return this._reports;
            }
        }

        /// <summary>
        ///     Gets or sets the type of the unit.
        /// </summary>
        /// <value>The type of the unit.</value>
        /// <exception cref="ArgumentOutOfRangeException">When value is an undefined type.</exception>
        public virtual UnitType Type
        {
            get
            {
                return this._type;
            }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(value.IsValid());

                this._type = value;
            }
        }

        #endregion

        #region Instance Methods

        /// <summary>
        ///     Adds an after action report to the end of the list of this unit's AARs.
        /// </summary>
        /// <param name="report">The after action report to add.</param>
        /// <exception cref="ArgumentNullException">When report is null.</exception>
        public virtual void AddNewReport(Report report)
        {
            Contract.Requires<ArgumentNullException>(report != null);

            this._reports.Add(report);
        }

        /// <summary>
        ///     Moves a report at the specified index one step down.
        /// </summary>
        /// <param name="index">The index of the report.</param>
        public virtual void MoveReportDownAt(int index)
        {
            Contract.Requires<ArgumentOutOfRangeException>(index >= 0 && index < Reports.Count());

            Contract.Assume(index < this._reports.Count);

            Report report = this._reports[index];

            this._reports.RemoveAt(index);
            this._reports.Insert(index + 1, report);
        }

        /// <summary>
        ///     Moves a report at the specified index one step up.
        /// </summary>
        /// <param name="index">The index of the report.</param>
        public virtual void MoveReportUpAt(int index)
        {
            Contract.Requires<ArgumentOutOfRangeException>(index >= 0 && index < Reports.Count());

            Contract.Assume(index < this._reports.Count);

            Report report = this._reports[index];

            this._reports.RemoveAt(index);
            this._reports.Insert(index - 1, report);
        }

        /// <summary>
        ///     Removes the <see cref="Report" /> at the specified index from the list.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> is not a valid index.</exception>
        [SuppressMessage("Microsoft.Contracts", "RequiresAtCall-index >= 0 && index < this.Reports.Count()")]
        public virtual void RemoveReportAt(int index)
        {
            Contract.Requires<ArgumentOutOfRangeException>(index >= 0 && index < Reports.Count());

            Contract.Assume(index < this._reports.Count);

            this._reports.RemoveAt(index);
        }

        #endregion
    }
}