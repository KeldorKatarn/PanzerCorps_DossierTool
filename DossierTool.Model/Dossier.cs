// <copyright file="Dossier.cs" company="VacuumBreather">
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
    ///     Represents a player dossier, containing information about all the player's units and their performance.
    /// </summary>
    [DataContract]
    public class Dossier
    {
        #region Constants

        /// <summary>
        ///     The default name of a newly created dossier.
        /// </summary>
        public const string DefaultName = "New Dossier";

        /// <summary>
        ///     The name of the root unit.
        /// </summary>
        public const string RootName = "Dossier Root Unit";

        #endregion

        #region Readonly & Static Fields

        [DataMember(Name = "ScenarioReports")]
        private readonly List<ScenarioReport> _scenarioReports = new List<ScenarioReport>();

        #endregion

        #region Fields

        [DataMember(Name = "RootUnit")]
        private HigherUnit _rootUnit;

        [DataMember(Name = "Name")]
        private string _name;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Dossier" /> class.
        /// </summary>
        public Dossier()
        {
            Contract.Assume(StringValidator.IsValidString(RootName));
            Contract.Assume(StringValidator.IsValidString(DefaultName));

            this._rootUnit = new HigherUnit();
            this._rootUnit.Name = RootName;
            this._name = DefaultName;
        }

        #endregion

        #region Object Invariant

        [ContractInvariantMethod]
        private void Invariant()
        {
            Contract.Invariant(StringValidator.IsValidString(this._name), "Name must be a valid string.");
            Contract.Invariant(this._rootUnit != null, "RootUnit must never be null.");
            Contract.Invariant(this._scenarioReports != null, "ScenarioReports must never be null.");
            Contract.Invariant(Contract.ForAll(this._scenarioReports, sr => sr != null),
                               "ScenarioReports must not contain any null values.");
        }

        #endregion

        #region Instance Properties

        /// <summary>
        ///     Gets or sets the name of the player this dossier belongs to.
        /// </summary>
        /// <value>The name of the player this dossier belongs to.</value>
        /// <exception cref="ArgumentNullException">When null is passed.</exception>
        /// <exception cref="ArgumentException">When a name with an invalid format is assigned.</exception>
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
        ///     Gets the scenario reports.
        /// </summary>
        /// <value>The scenario reports.</value>
        public virtual IEnumerable<ScenarioReport> ScenarioReports
        {
            get
            {
                return this._scenarioReports;
            }
        }

        /// <summary>
        ///     Gets the root of the unit hierarchy of the dossier.
        /// </summary>
        /// <value>The root of the unit hierarchy of the dossier.</value>
        public HigherUnit RootUnit
        {
            get
            {
                return this._rootUnit;
            }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);

                this._rootUnit = value;
            }
        }

        #endregion

        #region Instance Methods

        /// <summary>
        ///     Adds a <see cref="ScenarioReport" /> to the end of the list of reports.
        /// </summary>
        /// <param name="report">The <see cref="ScenarioReport" /> to add.</param>
        /// <exception cref="ArgumentNullException">When <paramref name="report" /> is null.</exception>
        public virtual void AddNewReport(ScenarioReport report)
        {
            Contract.Requires<ArgumentNullException>(report != null);

            this._scenarioReports.Add(report);
        }

        /// <summary>
        ///     Moves a report at the specified index one step down.
        /// </summary>
        /// <param name="index">The index of the report.</param>
        public virtual void MoveReportDownAt(int index)
        {
            Contract.Requires<ArgumentOutOfRangeException>(index >= 0 && index < ScenarioReports.Count());

            Contract.Assume(index < this._scenarioReports.Count);

            ScenarioReport report = this._scenarioReports[index];

            this._scenarioReports.RemoveAt(index);
            this._scenarioReports.Insert(index + 1, report);
        }

        /// <summary>
        ///     Moves a report at the specified index one step up.
        /// </summary>
        /// <param name="index">The index of the report.</param>
        public virtual void MoveReportUpAt(int index)
        {
            Contract.Requires<ArgumentOutOfRangeException>(index >= 0 && index < ScenarioReports.Count());

            Contract.Assume(index < this._scenarioReports.Count);

            ScenarioReport report = this._scenarioReports[index];

            this._scenarioReports.RemoveAt(index);
            this._scenarioReports.Insert(index - 1, report);
        }

        /// <summary>
        ///     Removes the <see cref="ScenarioReport" /> at the specified index from the list.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> is not a valid index.</exception>
        [SuppressMessage("Microsoft.Contracts", "RequiresAtCall-index >= 0 && index < this.ScenarioReports.Count()")]
        public virtual void RemoveReportAt(int index)
        {
            Contract.Requires<ArgumentOutOfRangeException>(index >= 0 && index < ScenarioReports.Count());

            Contract.Assume(index < this._scenarioReports.Count);

            this._scenarioReports.RemoveAt(index);
        }

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