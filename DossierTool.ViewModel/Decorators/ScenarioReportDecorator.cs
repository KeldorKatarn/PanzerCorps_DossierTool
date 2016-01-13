// <copyright file="ScenarioReportDecorator.cs" company="VacuumBreather">
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

namespace DossierTool.ViewModel.Decorators
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics.Contracts;
    using System.Linq.Expressions;
    using System.Runtime.Serialization;
    using Caliburn.Micro;
    using Model;
    using Model.Helpers;

    #endregion

    /// <summary>
    ///     The <see cref="IDecorator" /> for a <see cref="ScenarioReport" />.
    /// </summary>
    [DataContract]
    public sealed class ScenarioReportDecorator : ScenarioReport, IDecorator
    {
        #region Fields

        private KeyValuePair<string, ScenarioOutcome> _outcome;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ScenarioReportDecorator" /> class.
        /// </summary>
        /// <param name="report">The <see cref="ScenarioReport" /> this view model represents.</param>
        /// <exception cref="ArgumentNullException"><paramref name="report" /> is a null reference.</exception>
        public ScenarioReportDecorator(ScenarioReport report)
        {
            Contract.Requires<ArgumentNullException>(report != null);

            ScenarioName = report.ScenarioName;
            Prestige = report.Prestige;
            Outcome = new KeyValuePair<string, ScenarioOutcome>(report.Outcome.ToDisplayName(), report.Outcome);
        }

        #endregion

        #region Object Invariant

        [ContractInvariantMethod]
        private void Invariant()
        {
        }

        #endregion

        #region Instance Properties

        /// <summary>
        ///     Gets or sets the prestige after a scenario.
        /// </summary>
        /// <value>The prestige after a scenario.</value>
        public override int Prestige
        {
            get
            {
                return base.Prestige;
            }
            set
            {
                if (value.Equals(Prestige))
                {
                    return;
                }

                base.Prestige = value;
                NotifyOfPropertyChange(() => Prestige);
            }
        }

        /// <summary>
        ///     Gets or sets the name of the scenario.
        /// </summary>
        /// <value>The name of the scenario.</value>
        public override string ScenarioName
        {
            get
            {
                return base.ScenarioName;
            }
            set
            {
                if (value == base.ScenarioName)
                {
                    return;
                }

                base.ScenarioName = value;
                NotifyOfPropertyChange(() => ScenarioName);
            }
        }

        /// <summary>
        ///     Gets or sets the outcome of the scenario.
        /// </summary>
        /// <value>The outcome of the scenario.</value>
        public new KeyValuePair<string, ScenarioOutcome> Outcome
        {
            get
            {
                return this._outcome;
            }
            set
            {
                if (value.Equals(Outcome))
                {
                    return;
                }

                this._outcome = value;
                base.Outcome = value.Value;
                NotifyOfPropertyChange(() => Outcome);
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

        private void NotifyOfPropertyChange(string propertyName)
        {
            Execute.OnUIThread(() => OnPropertyChanged(new PropertyChangedEventArgs(propertyName)));
        }

        private void NotifyOfPropertyChange<TProperty>(Expression<Func<TProperty>> property)
        {
            NotifyOfPropertyChange((property).GetMemberInfo().Name);
        }

        private void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler eventHandler = PropertyChanged;

            if (eventHandler == null)
            {
                return;
            }

            eventHandler(this, e);
        }

        #endregion

        #region IDecorator Members

        /// <summary>
        ///     Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}