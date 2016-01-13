// <copyright file="ScenarioReport.cs" company="VacuumBreather">
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
    ///     Represents a campaign scenario after action report containing not unit specific information.
    /// </summary>
    [DataContract]
    public class ScenarioReport
    {
        #region Constants

        /// <summary>
        ///     The default scenario name for a new report.
        /// </summary>
        public const string NewScenario = "New Scenario";

        #endregion

        #region Fields

        [DataMember(Name = "ScenarioName")]
        private string _scenarioName;

        [DataMember(Name = "Outcome")]
        private ScenarioOutcome _outcome;

        [DataMember(Name = "Prestige")]
        private int _prestige;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ScenarioReport" /> class.
        /// </summary>
        public ScenarioReport()
        {
            Contract.Assume(StringValidator.IsValidString(NewScenario));

            this._scenarioName = NewScenario;
            this._outcome = ScenarioOutcome.Pending;
            this._prestige = 0;
        }

        #endregion

        #region Object Invariant

        [ContractInvariantMethod]
        private void Invariant()
        {
            Contract.Invariant(StringValidator.IsValidString(this._scenarioName),
                               "ScenarioName must be a valid name string.");
            Contract.Invariant(this._outcome.IsValid());
            Contract.Invariant(this._prestige >= 0, "Experience must be a positive integer.");
        }

        #endregion

        #region Instance Properties

        /// <summary>
        ///     Gets or sets the outcome of the scenario.
        /// </summary>
        /// <value>The outcome of the scenario.</value>
        /// <exception cref="ArgumentException">When <paramref name="value" /> is an undefined type.</exception>
        public virtual ScenarioOutcome Outcome
        {
            get
            {
                return this._outcome;
            }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(value.IsValid());

                this._outcome = value;
            }
        }

        /// <summary>
        ///     Gets or sets the prestige after a scenario.
        /// </summary>
        /// <value>The prestige after a scenario.</value>
        /// <exception cref="ArgumentException">When <paramref name="value" /> is a negative value.</exception>
        public virtual int Prestige
        {
            get
            {
                return this._prestige;
            }
            set
            {
                Contract.Requires<ArgumentException>(value >= 0);

                this._prestige = value;
            }
        }

        /// <summary>
        ///     Gets or sets the name of the scenario.
        /// </summary>
        /// <value>The name of the scenario.</value>
        /// <exception cref="ArgumentNullException">When the scenario name is set to null.</exception>
        /// <exception cref="ArgumentException">When the scenario name is set to an invalid string.</exception>
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

        #endregion
    }
}