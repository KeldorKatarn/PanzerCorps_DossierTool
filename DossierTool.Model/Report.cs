// <copyright file="Report.cs" company="VacuumBreather">
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
    ///     Represents a unit's scenario after action report.
    /// </summary>
    [DataContract]
    public class Report
    {
        #region Constants

        /// <summary>
        ///     The default scenario name for a new report.
        /// </summary>
        public const string NewScenario = "New Scenario";

        /// <summary>
        ///     The default string for 'no equipment'.
        /// </summary>
        public const string None = "None";

        #endregion

        #region Fields

        [DataMember(Name = "ScenarioName", Order = 0, IsRequired = true)]
        private string _scenarioName;

        [DataMember(Name = "Equipment", Order = 1)]
        private string _equipment;

        [DataMember(Name = "EquipmentID", Order = 2)]
        private int _equipmentId = -1;

        [DataMember(Name = "LandTransport", Order = 3)]
        private string _landTransport;

        [DataMember(Name = "LandTransportID", Order = 4)]
        private int _landTransportId = -1;

        [DataMember(Name = "Experience", Order = 5)]
        private int _experience;

        [DataMember(Name = "Kills", Order = 6)]
        private int _kills;

        [DataMember(Name = "Losses", Order = 7)]
        private int _losses;

        [DataMember(Name = "HighestAward", Order = 8)]
        private string _highestAward;

        [DataMember(Name = "FirstHero", Order = 9)]
        private string _firstHero;

        [DataMember(Name = "SecondHero", Order = 10)]
        private string _secondHero;

        [DataMember(Name = "ThirdHero", Order = 11)]
        private string _thirdHero;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Report" /> class.
        /// </summary>
        public Report()
        {
            Contract.Assume(StringValidator.IsValidString(NewScenario));
            Contract.Assume(StringValidator.IsValidString(None));

            this._scenarioName = NewScenario;
            this._equipment = None;
            this._landTransport = None;
            this._highestAward = Award.None.ID;
            this._firstHero = Hero.None.ID;
            this._secondHero = Hero.None.ID;
            this._thirdHero = Hero.None.ID;
        }

        #endregion

        #region Object Invariant

        [ContractInvariantMethod]
        private void Invariant()
        {
            Contract.Invariant(StringValidator.IsValidString(this._scenarioName),
                               "ScenarioName must be a valid name string.");
            Contract.Invariant(this._experience >= 0, "Experience must be a positive integer.");
            Contract.Invariant(this._kills >= 0, "Experience must be a positive integer.");
            Contract.Invariant(this._losses >= 0, "Experience must be a positive integer.");
            Contract.Invariant(StringValidator.IsValidString(this._equipment), "Equipment must be a valid name string.");
            Contract.Invariant(StringValidator.IsValidString(this._landTransport),
                               "LandTransport must be a valid name string.");
        }

        #endregion

        #region Instance Properties

        /// <summary>
        ///     Gets or sets the name of the unit equiment at the end of a scenario.
        /// </summary>
        /// <value>The name of the unit equiment at the end of a scenario.</value>
        /// <exception cref="ArgumentNullException">When the <paramref name="value" /> is null.</exception>
        /// <exception cref="ArgumentException">When <paramref name="value" /> is an invalid string.</exception>
        public virtual string Equipment
        {
            get
            {
                return this._equipment;
            }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                Contract.Requires<ArgumentException>(StringValidator.IsValidString(value));

                this._equipment = value;
            }
        }

        /// <summary>
        ///     Gets or sets the ID of the unit equiment at the end of a scenario.
        /// </summary>
        /// <value>The ID of the unit equiment at the end of a scenario.</value>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="value" /> is negative.</exception>
        public virtual int EquipmentId
        {
            get
            {
                return this._equipmentId;
            }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(value >= 0);

                this._equipmentId = value;
            }
        }

        /// <summary>
        ///     Gets or sets the experience of the unit after a scenario.
        /// </summary>
        /// <value>The experience of the unit after a scenario.</value>
        /// <exception cref="ArgumentException">When <paramref name="value" /> is a negative value.</exception>
        public virtual int Experience
        {
            get
            {
                return this._experience;
            }
            set
            {
                Contract.Requires<ArgumentException>(value >= 0);

                this._experience = value;
            }
        }

        /// <summary>
        ///     Gets or sets the first hero of the unit at the end of a scenario.
        /// </summary>
        /// <value>The first hero of the unit at the end of a scenario.</value>
        public virtual string FirstHero
        {
            get
            {
                return this._firstHero;
            }
            set
            {
                this._firstHero = value;
            }
        }

        /// <summary>
        ///     Gets or sets the highest award of the unit at the end of a scenario.
        /// </summary>
        /// <value>The highest award of the unit at the end of a scenario.</value>
        public virtual string HighestAward
        {
            get
            {
                return this._highestAward;
            }
            set
            {
                this._highestAward = value;
            }
        }

        /// <summary>
        ///     Gets or sets the kills of the unit after a scenario.
        /// </summary>
        /// <value>The kills of the unit after a scenario.</value>
        /// <exception cref="ArgumentException">When <paramref name="value" /> is a negative value.</exception>
        public virtual int Kills
        {
            get
            {
                return this._kills;
            }
            set
            {
                Contract.Requires<ArgumentException>(value >= 0);

                this._kills = value;
            }
        }

        /// <summary>
        ///     Gets or sets the name of the unit land transport equiment at the end of a scenario.
        /// </summary>
        /// <value>The name of the unit land transport equiment at the end of a scenario.</value>
        /// <exception cref="ArgumentNullException">When the land transport equipment is set to null.</exception>
        /// <exception cref="ArgumentException">When the land transport equipment is set to an invalid string.</exception>
        public virtual string LandTransport
        {
            get
            {
                return this._landTransport;
            }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                Contract.Requires<ArgumentException>(StringValidator.IsValidString(value));

                this._landTransport = value;
            }
        }

        /// <summary>
        ///     Gets or sets the ID of the land transport equiment at the end of a scenario.
        /// </summary>
        /// <value>The ID of the land transport equiment at the end of a scenario.</value>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="value" /> is negative.</exception>
        public virtual int LandTransportId
        {
            get
            {
                return this._landTransportId;
            }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(value >= 0);

                this._landTransportId = value;
            }
        }

        /// <summary>
        ///     Gets or sets the losses of the unit after a scenario.
        /// </summary>
        /// <value>The losses of the unit after a scenario.</value>
        /// <exception cref="ArgumentException">When <paramref name="value" /> is a negative value.</exception>
        public virtual int Losses
        {
            get
            {
                return this._losses;
            }
            set
            {
                Contract.Requires<ArgumentException>(value >= 0);

                this._losses = value;
            }
        }

        /// <summary>
        ///     Gets or sets the name of the scenario.
        /// </summary>
        /// <value>The name of the scenario.</value>
        /// <exception cref="ArgumentNullException">When <paramref name="value" /> is null.</exception>
        /// <exception cref="ArgumentException">When <paramref name="value" /> is an invalid string.</exception>
        public virtual string ScenarioName
        {
            get
            {
                return this._scenarioName;
            }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                Contract.Requires<ArgumentException>(StringValidator.IsValidString(value));

                this._scenarioName = value;
            }
        }

        /// <summary>
        ///     Gets or sets the second hero of the unit at the end of a scenario.
        /// </summary>
        /// <value>The second hero of the unit at the end of a scenario.</value>
        public virtual string SecondHero
        {
            get
            {
                return this._secondHero;
            }
            set
            {
                this._secondHero = value;
            }
        }

        /// <summary>
        ///     Gets or sets the third hero of the unit at the end of a scenario.
        /// </summary>
        /// <value>The third hero of the unit at the end of a scenario.</value>
        public virtual string ThirdHero
        {
            get
            {
                return this._thirdHero;
            }
            set
            {
                this._thirdHero = value;
            }
        }

        #endregion

        #region Class Methods

        /// <summary>
        ///     Creates a copy of the specified report.
        /// </summary>
        /// <param name="report">The report to copy from.</param>
        /// <returns>The copied report.</returns>
        /// <remarks>
        ///     If <paramref name="report" /> is a null reference, a standard new instance is returned.
        /// </remarks>
        public static Report CreateCopyOf(Report report)
        {
            return (report == null)
                       ? new Report()
                       : new Report
                         {
                             Equipment = report.Equipment,
                             EquipmentId = report.EquipmentId,
                             LandTransport = report.LandTransport,
                             LandTransportId = report.LandTransportId,
                             Experience = report.Experience,
                             Kills = report.Kills,
                             Losses = report.Losses,
                             HighestAward = report.HighestAward,
                             FirstHero = report.FirstHero,
                             SecondHero = report.SecondHero,
                             ThirdHero = report.ThirdHero
                         };
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
            Contract.Assume(ScenarioName != null);

            return ScenarioName;
        }

        [OnDeserializing]
        private void SetDefaultEquipmentIDs(StreamingContext c)
        {
            this._equipmentId = -1;
            this._landTransportId = -1;
        }

        #endregion
    }
}