// <copyright file="ReportDecorator.cs" company="VacuumBreather">
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
    using Services;

    #endregion

    /// <summary>
    ///     The <see cref="IDecorator" /> for a <see cref="Report" />.
    /// </summary>
    [DataContract]
    public sealed class ReportDecorator : Report, IDecorator
    {
        #region Fields

        private Equipment _equipment = Equipment.None;
        private Equipment _landTransport = Equipment.None;
        private KeyValuePair<string, Award> _highestAward;
        private KeyValuePair<string, Hero> _firstHero;
        private KeyValuePair<string, Hero> _secondHero;
        private KeyValuePair<string, Hero> _thirdHero;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReportDecorator" /> class.
        /// </summary>
        /// <param name="report">The <see cref="Report" /> this decorater wraps.</param>
        /// <param name="unit">The <see cref="Unit" /> the report is for.</param>
        /// <param name="equipmentProvider">The equipment provider.</param>
        /// <param name="awardProvider">The award provider.</param>
        /// <param name="heroProvider">The hero provider.</param>
        /// <exception cref="ArgumentNullException"><paramref name="report" /> or <paramref name="unit" /> is a null reference.</exception>
        public ReportDecorator(Report report,
                               Unit unit,
                               IEquipmentProvider equipmentProvider,
                               IAwardProvider awardProvider,
                               IHeroProvider heroProvider)
        {
            Contract.Requires<ArgumentNullException>(report != null);
            Contract.Requires<ArgumentException>(report.Equipment != null);
            Contract.Requires<ArgumentException>(report.LandTransport != null);
            Contract.Requires<ArgumentNullException>(unit != null);

            // Copy data from undecorated report.
            ScenarioName = report.ScenarioName;

            // Find the correct land transport.
            if (report.LandTransportId < 0 || (report.LandTransport.Equals(Equipment.None.ShortName)))
            {
                LandTransport = equipmentProvider.Find(report.LandTransport, unit.Nationality, UnitType.LandTransport);
            }
            else
            {
                LandTransport = equipmentProvider.Find(report.LandTransportId);
            }

            // Find the correct equipment.
            if (report.EquipmentId < 0 || (report.Equipment.Equals(Equipment.None.ShortName)))
            {
                Equipment = equipmentProvider.Find(report.Equipment, unit.Nationality, unit.Type);
            }
            else
            {
                Equipment = equipmentProvider.Find(report.EquipmentId);
            }

            Award highestAward = awardProvider.Find(report.HighestAward);

            HighestAward = new KeyValuePair<string, Award>(highestAward.DisplayName, highestAward);

            Experience = report.Experience;
            Kills = report.Kills;
            Losses = report.Losses;

            Hero firstHero = heroProvider.Find(report.FirstHero);
            Hero secondHero = heroProvider.Find(report.SecondHero);
            Hero thirdHero = heroProvider.Find(report.ThirdHero);

            FirstHero = new KeyValuePair<string, Hero>(firstHero.DisplayName, firstHero);
            SecondHero = new KeyValuePair<string, Hero>(secondHero.DisplayName, secondHero);
            ThirdHero = new KeyValuePair<string, Hero>(thirdHero.DisplayName, thirdHero);
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
        ///     Gets or sets the experience of the unit after a scenario.
        /// </summary>
        /// <value>The experience of the unit after a scenario.</value>
        public override int Experience
        {
            get
            {
                return base.Experience;
            }
            set
            {
                if (value.Equals(Experience))
                {
                    return;
                }

                base.Experience = value;
                NotifyOfPropertyChange(() => Experience);
            }
        }

        /// <summary>
        ///     Gets or sets the kills of the unit after a scenario.
        /// </summary>
        /// <value>The kills of the unit after a scenario.</value>
        public override int Kills
        {
            get
            {
                return base.Kills;
            }
            set
            {
                if (value.Equals(Kills))
                {
                    return;
                }

                base.Kills = value;
                NotifyOfPropertyChange(() => Kills);
            }
        }

        /// <summary>
        ///     Gets or sets the losses of the unit after a scenario.
        /// </summary>
        /// <value>The losses of the unit after a scenario.</value>
        public override int Losses
        {
            get
            {
                return base.Losses;
            }
            set
            {
                if (value.Equals(Losses))
                {
                    return;
                }

                base.Losses = value;
                NotifyOfPropertyChange(() => Losses);
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
                if (value.Equals(ScenarioName))
                {
                    return;
                }

                base.ScenarioName = value;
                NotifyOfPropertyChange(() => ScenarioName);
            }
        }

        /// <summary>
        ///     Gets or sets the name of the unit equiment at the end of a scenario.
        /// </summary>
        /// <value>The name of the unit equiment at the end of a scenario.</value>
        public new Equipment Equipment
        {
            get
            {
                return this._equipment;
            }
            set
            {
                this._equipment = value;
                base.Equipment = value.ShortName.Trim();
                EquipmentId = value.ID;

                NotifyOfPropertyChange(() => Equipment);
            }
        }

        /// <summary>
        ///     Gets or sets the first hero.
        /// </summary>
        /// <value>The first hero.</value>
        public new KeyValuePair<string, Hero> FirstHero
        {
            get
            {
                return this._firstHero;
            }
            set
            {
                if (value.Equals(FirstHero))
                {
                    return;
                }

                this._firstHero = value;
                base.FirstHero = value.Value.ID;

                NotifyOfPropertyChange(() => FirstHero);
            }
        }

        /// <summary>
        ///     Gets or sets the highest award.
        /// </summary>
        /// <value>The highest award.</value>
        public new KeyValuePair<string, Award> HighestAward
        {
            get
            {
                return this._highestAward;
            }
            set
            {
                if (value.Equals(HighestAward))
                {
                    return;
                }

                this._highestAward = value;
                base.HighestAward = value.Value.ID;

                NotifyOfPropertyChange(() => HighestAward);
            }
        }

        /// <summary>
        ///     Gets or sets the name of the unit land transport at the end of a scenario.
        /// </summary>
        /// <value>The name of the unit land transport at the end of a scenario.</value>
        public new Equipment LandTransport
        {
            get
            {
                return this._landTransport;
            }
            set
            {
                this._landTransport = value;
                base.LandTransport = value.ShortName;
                LandTransportId = value.ID;

                NotifyOfPropertyChange(() => LandTransport);
            }
        }

        /// <summary>
        ///     Gets or sets the second hero.
        /// </summary>
        /// <value>The second hero.</value>
        public new KeyValuePair<string, Hero> SecondHero
        {
            get
            {
                return this._secondHero;
            }
            set
            {
                if (value.Equals(SecondHero))
                {
                    return;
                }

                this._secondHero = value;
                base.SecondHero = value.Value.ID;

                NotifyOfPropertyChange(() => SecondHero);
            }
        }

        /// <summary>
        ///     Gets or sets the third hero.
        /// </summary>
        /// <value>The third hero.</value>
        public new KeyValuePair<string, Hero> ThirdHero
        {
            get
            {
                return this._thirdHero;
            }
            set
            {
                if (value.Equals(ThirdHero))
                {
                    return;
                }

                this._thirdHero = value;
                base.ThirdHero = value.Value.ID;

                NotifyOfPropertyChange(() => ThirdHero);
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