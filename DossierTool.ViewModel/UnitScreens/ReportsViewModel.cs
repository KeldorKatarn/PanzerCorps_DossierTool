// <copyright file="ReportsViewModel.cs" company="VacuumBreather">
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

namespace DossierTool.ViewModel.UnitScreens
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using Decorators;
    using DossierScreens;
    using Model;
    using Model.Helpers;
    using Services;

    #endregion

    /// <summary>
    ///     IUnitScreen implementation for the unit reports page.
    /// </summary>
    [Export(typeof(IUnitScreen))]
    public sealed class ReportsViewModel : UnitScreenBase, IReportModelChanges
    {
        #region Constants

        private const string ScreenName = "Reports";

        #endregion

        #region Readonly & Static Fields

        private readonly IEquipmentProvider _equipmentProvider;
        private readonly IHeroProvider _heroProvider;
        private readonly IAwardProvider _awardProvider;

        private readonly KeyValuePair<string, Award> _noAward = new KeyValuePair<string, Award>(Award.None.DisplayName,
                                                                                                Award.None);

        private readonly KeyValuePair<string, Hero> _noHero = new KeyValuePair<string, Hero>(Hero.None.DisplayName,
                                                                                             Hero.None);

        #endregion

        #region Fields

        private string _scenarioName = string.Empty;
        private int _experience;
        private int _kills;
        private int _losses;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReportsViewModel" /> class.
        /// </summary>
        /// <param name="dialogService">The dialog service.</param>
        /// <param name="equipmentProvider">The equipment provider.</param>
        /// <param name="awardProvider"></param>
        /// <param name="heroProvider">The hero provider.</param>
        [ImportingConstructor]
        public ReportsViewModel(IDialogService dialogService,
                                IEquipmentProvider equipmentProvider,
                                IAwardProvider awardProvider,
                                IHeroProvider heroProvider)
            : base(2, dialogService)
        {
            this._equipmentProvider = equipmentProvider;
            this._awardProvider = awardProvider;
            this._heroProvider = heroProvider;
            DisplayName = ScreenName;
        }

        #endregion

        #region Instance Properties

        /// <summary>
        ///     Gets the available awards.
        /// </summary>
        /// <value>The available awards.</value>
        public IEnumerable<KeyValuePair<string, Award>> AvailableAwards
        {
            get
            {
                return
                    this._awardProvider.Awards.Where(
                        award => award.Nationality == Unit.Nationality.Value || award == Award.None)
                        .Select(award => new KeyValuePair<string, Award>(award.DisplayName, award));
            }
        }

        /// <summary>
        ///     Gets the available equipment.
        /// </summary>
        /// <value>The available equipment.</value>
        public IEnumerable<Equipment> AvailableEquipment
        {
            get
            {
                List<Equipment> equipmentList =
                    this._equipmentProvider.Equipments.Where(
                        equipment => equipment.Nationality == Unit.Nationality.Value)
                        .Where(equipment => equipment.Type == Unit.Type.Value)
                        .Where(
                            equipment => equipment.ShortName.StartsWith(Equipment.SpecialUnitPrefix) == Unit.IsSpecial)
                        .OrderBy(equipment => equipment.AvailableFrom)
                        .ToList();

                equipmentList.Insert(0, Equipment.None);

                return equipmentList;
            }
        }

        /// <summary>
        ///     Gets the available heroes.
        /// </summary>
        /// <value>The available heroes.</value>
        public IEnumerable<KeyValuePair<string, Hero>> AvailableHeroes
        {
            get
            {
                return this._heroProvider.Heroes.Select(hero => new KeyValuePair<string, Hero>(hero.DisplayName, hero));
            }
        }

        /// <summary>
        ///     Gets the available land transports.
        /// </summary>
        /// <value>The available land transports.</value>
        public IEnumerable<Equipment> AvailableLandTransports
        {
            get
            {
                return Equipment.GetAvailableLandTransports(SelectedReport, Unit, this._equipmentProvider);
            }
        }

        /// <summary>
        ///     Deletes the selected scenario report.
        /// </summary>
        public bool CanDeleteReport
        {
            get
            {
                return Unit.Reports.CurrentItem != null;
            }
        }

        /// <summary>
        ///     Moves the selected scenario report down.
        /// </summary>
        public bool CanMoveReportDown
        {
            get
            {
                return Unit.Reports.CurrentPosition < (Unit.Reports.Cast<ReportDecorator>().Count() - 1);
            }
        }

        /// <summary>
        ///     Moves the selected scenario report up.
        /// </summary>
        public bool CanMoveReportUp
        {
            get
            {
                return Unit.Reports.CurrentPosition > 0;
            }
        }

        /// <summary>
        ///     Gets or sets the name of the unit equiment at the end of a scenario.
        /// </summary>
        /// <value>The name of the unit equiment at the end of a scenario.</value>
        public Equipment Equipment
        {
            get
            {
                return (SelectedReport != null) ? SelectedReport.Equipment : Equipment.None;
            }
            set
            {
                if (SelectedReport == null)
                {
                    return;
                }

                if (value.Equals(Equipment))
                {
                    return;
                }

                SelectedReport.Equipment = value;

                Contract.Assume(AvailableLandTransports != null);

                if (!AvailableLandTransports.Contains(LandTransport))
                {
                    LandTransport = Equipment.None;
                }

                NotifyOfPropertyChange(() => Equipment);
                NotifyOfPropertyChange(() => AvailableLandTransports);
                OnModelChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the experience of the unit after a scenario.
        /// </summary>
        /// <value>The experience of the unit after a scenario.</value>
        public int Experience
        {
            get
            {
                return this._experience;
            }
            set
            {
                if (SelectedReport == null)
                {
                    return;
                }

                if (value >= 0)
                {
                    ResetPropertyValidationError(() => Experience);

                    if (!value.Equals(SelectedReport.Experience))
                    {
                        SelectedReport.Experience = value;
                        OnModelChanged();
                    }
                }
                else
                {
                    SetPropertyValidationError(() => Experience, "Experience must be a non-negative number.");
                }

                this._experience = value;
                NotifyOfPropertyChange(() => Experience);
            }
        }

        /// <summary>
        ///     Gets or sets the first hero.
        /// </summary>
        /// <value>The first hero.</value>
        public KeyValuePair<string, Hero> FirstHero
        {
            get
            {
                return (SelectedReport != null) ? SelectedReport.FirstHero : this._noHero;
            }
            set
            {
                if (SelectedReport == null)
                {
                    return;
                }

                if (value.Equals(SelectedReport.FirstHero))
                {
                    return;
                }

                SelectedReport.FirstHero = value;
                NotifyOfPropertyChange(() => FirstHero);
                OnModelChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the highest award.
        /// </summary>
        /// <value>The highest award.</value>
        public KeyValuePair<string, Award> HighestAward
        {
            get
            {
                return (SelectedReport != null) ? SelectedReport.HighestAward : this._noAward;
            }
            set
            {
                if (SelectedReport == null)
                {
                    return;
                }

                if (value.Equals(SelectedReport.HighestAward))
                {
                    return;
                }

                SelectedReport.HighestAward = value;
                NotifyOfPropertyChange(() => HighestAward);
                OnModelChanged();
            }
        }

        /// <summary>
        ///     Gets a value indicating whether a report is selected.
        /// </summary>
        /// <value>
        ///     <c>true</c> if a report is selected; otherwise, <c>false</c>.
        /// </value>
        public bool IsReportSelected
        {
            get
            {
                return Unit.Reports.CurrentItem != null;
            }
        }

        /// <summary>
        ///     Gets or sets the kills of the unit after a scenario.
        /// </summary>
        /// <value>The kills of the unit after a scenario.</value>
        public int Kills
        {
            get
            {
                return this._kills;
            }
            set
            {
                if (SelectedReport == null)
                {
                    return;
                }

                if (value >= 0)
                {
                    ResetPropertyValidationError(() => Kills);

                    if (!value.Equals(SelectedReport.Kills))
                    {
                        SelectedReport.Kills = value;
                        OnModelChanged();
                    }
                }
                else
                {
                    SetPropertyValidationError(() => Kills, "Kills must be a non-negative number.");
                }

                this._kills = value;
                NotifyOfPropertyChange(() => Kills);
            }
        }

        /// <summary>
        ///     Gets or sets the name of the unit land transport at the end of a scenario.
        /// </summary>
        /// <value>The name of the unit land transport at the end of a scenario.</value>
        public Equipment LandTransport
        {
            get
            {
                return (SelectedReport != null) ? SelectedReport.LandTransport : Equipment.None;
            }
            set
            {
                if (SelectedReport == null)
                {
                    return;
                }

                if (value.Equals(LandTransport))
                {
                    return;
                }

                SelectedReport.LandTransport = value;
                NotifyOfPropertyChange(() => LandTransport);
                OnModelChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the losses of the unit after a scenario.
        /// </summary>
        /// <value>The losses of the unit after a scenario.</value>
        public int Losses
        {
            get
            {
                return this._losses;
            }
            set
            {
                if (SelectedReport == null)
                {
                    return;
                }

                if (value >= 0)
                {
                    ResetPropertyValidationError(() => Losses);

                    if (!value.Equals(SelectedReport.Losses))
                    {
                        SelectedReport.Losses = value;
                        OnModelChanged();
                    }
                }
                else
                {
                    SetPropertyValidationError(() => Losses, "Losses must be a non-negative number.");
                }

                this._losses = value;
                NotifyOfPropertyChange(() => Losses);
            }
        }

        /// <summary>
        ///     Gets or sets the name of the scenario.
        /// </summary>
        /// <value>The name of the scenario.</value>
        public string ScenarioName
        {
            get
            {
                return this._scenarioName;
            }
            set
            {
                if (SelectedReport == null)
                {
                    return;
                }

                if (string.IsNullOrEmpty(value))
                {
                    SetPropertyValidationError(() => ScenarioName, "The scenario name must not be empty.");
                }
                else if (!StringValidator.IsValidString(value))
                {
                    SetPropertyValidationError(() => ScenarioName, "The scenario name contains invalid characters.");
                }
                else
                {
                    ResetPropertyValidationError(() => ScenarioName);

                    if (!value.Equals(SelectedReport.ScenarioName))
                    {
                        SelectedReport.ScenarioName = value;
                        OnModelChanged();
                    }
                }

                this._scenarioName = value;
                NotifyOfPropertyChange(() => ScenarioName);
            }
        }

        /// <summary>
        ///     Gets or sets the second hero.
        /// </summary>
        /// <value>The second hero.</value>
        public KeyValuePair<string, Hero> SecondHero
        {
            get
            {
                return (SelectedReport != null) ? SelectedReport.SecondHero : this._noHero;
            }
            set
            {
                if (SelectedReport == null)
                {
                    return;
                }

                if (value.Equals(SelectedReport.SecondHero))
                {
                    return;
                }

                SelectedReport.SecondHero = value;
                NotifyOfPropertyChange(() => SecondHero);
                OnModelChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the third hero.
        /// </summary>
        /// <value>The third hero.</value>
        public KeyValuePair<string, Hero> ThirdHero
        {
            get
            {
                return (SelectedReport != null) ? SelectedReport.ThirdHero : this._noHero;
            }
            set
            {
                if (SelectedReport == null)
                {
                    return;
                }

                if (value.Equals(SelectedReport.ThirdHero))
                {
                    return;
                }

                SelectedReport.ThirdHero = value;
                NotifyOfPropertyChange(() => ThirdHero);
                OnModelChanged();
            }
        }

        private ReportDecorator SelectedReport
        {
            get
            {
                return (ReportDecorator)Unit.Reports.CurrentItem;
            }
        }

        #endregion

        #region Instance Methods

        /// <summary>
        ///     Adds a new scenario report.
        /// </summary>
        public void AddReport()
        {
            Unit.AddNewReport();
            Refresh();
            OnModelChanged();
        }

        /// <summary>
        ///     Deletes the selected scenario report.
        /// </summary>
        public void DeleteReport()
        {
            Unit.DeleteSelectedReport();
            Refresh();
            OnModelChanged();
        }

        /// <summary>
        ///     Moves the selected scenario report down.
        /// </summary>
        public void MoveReportDown()
        {
            Unit.MoveSelectedReportDown();
            Refresh();
            OnModelChanged();
        }

        /// <summary>
        ///     Moves the selected scenario report up.
        /// </summary>
        public void MoveReportUp()
        {
            Unit.MoveSelectedReportUp();
            Refresh();
            OnModelChanged();
        }

        /// <summary>
        ///     Called when the unit was changed.
        /// </summary>
        /// <param name="oldUnit">The old unit.</param>
        /// <param name="newUnit">The new unit.</param>
        protected override void OnUnitChanged(UnitDecorator oldUnit, UnitDecorator newUnit)
        {
            if (oldUnit != null)
            {
                oldUnit.Reports.CurrentChanged -= OnUnitReportsCurrentChanged;
            }

            if (newUnit != null)
            {
                newUnit.Reports.CurrentChanged += OnUnitReportsCurrentChanged;

                if (newUnit.Reports.CurrentItem == null)
                {
                    newUnit.Reports.MoveCurrentToLast();
                }
            }

            ResetAllValidationErrors();
            UpdateProperties();
        }

        private void OnModelChanged()
        {
            EventHandler handler = ModelChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private void OnUnitReportsCurrentChanged(object sender, EventArgs e)
        {
            ResetAllValidationErrors();
            UpdateProperties();
            Refresh();
        }

        private void UpdateProperties()
        {
            ScenarioName = (SelectedReport != null) ? SelectedReport.ScenarioName : string.Empty;
            Experience = (SelectedReport != null) ? SelectedReport.Experience : 0;
            Kills = (SelectedReport != null) ? SelectedReport.Kills : 0;
            Losses = (SelectedReport != null) ? SelectedReport.Losses : 0;
        }

        #endregion

        #region IReportModelChanges Members

        /// <summary>
        ///     Occurs when any value on the model was changed.
        /// </summary>
        public event EventHandler ModelChanged;

        #endregion
    }
}