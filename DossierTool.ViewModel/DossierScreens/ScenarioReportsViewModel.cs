// <copyright file="ScenarioReportsViewModel.cs" company="VacuumBreather">
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

namespace DossierTool.ViewModel.DossierScreens
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Threading.Tasks;
    using Caliburn.Micro;
    using Decorators;
    using Dialogs;
    using Helpers;
    using Model;
    using Model.Helpers;
    using Properties;
    using Services;

    #endregion

    /// <summary>
    ///     IDossierScreen implementation for the scenario reports screen.
    /// </summary>
    [Export(typeof(IDossierScreen))]
    public sealed class ScenarioReportsViewModel : DossierScreenBase, IReportModelChanges
    {
        #region Constants

        private const string ScreenName = "Scenario Reports";

        #endregion

        #region Readonly & Static Fields

        private readonly BusyWatcher _busyWatcher;

        private readonly KeyValuePair<string, ScenarioOutcome> _pendingOutcome =
            new KeyValuePair<string, ScenarioOutcome>(ScenarioOutcome.Pending.ToDisplayName(), ScenarioOutcome.Pending);

        #endregion

        #region Fields

        private int _prestige;
        private string _scenarioName = string.Empty;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ScenarioReportsViewModel" /> class.
        /// </summary>
        /// <param name="dialogService">The dialog service.</param>
        /// <param name="busyWatcher">The busy watcher.</param>
        [ImportingConstructor]
        public ScenarioReportsViewModel(IDialogService dialogService, BusyWatcher busyWatcher)
            : base(4, dialogService)
        {
            this._busyWatcher = busyWatcher;
            DisplayName = ScreenName;
        }

        #endregion

        #region Instance Properties

        /// <summary>
        ///     Gets the available scenario outcomes.
        /// </summary>
        /// <value>The available scenario outcomes.</value>
        public IEnumerable<KeyValuePair<string, ScenarioOutcome>> AvailableOutcomes
        {
            get
            {
                return
                    Enum.GetValues(typeof(ScenarioOutcome))
                        .Cast<ScenarioOutcome>()
                        .Select(outcome => new KeyValuePair<string, ScenarioOutcome>(outcome.ToDisplayName(), outcome));
            }
        }

        /// <summary>
        ///     Deletes the selected scenario report.
        /// </summary>
        public bool CanDeleteReport
        {
            get
            {
                return Dossier.ScenarioReports.CurrentItem != null;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether unit reports can be generated for the currently selected report.
        /// </summary>
        /// <value>
        ///     <c>true</c> if unit reports can be generated for the currently selected report; otherwise, <c>false</c>.
        /// </value>
        public bool CanGenerateUnitReports
        {
            get
            {
                return (SelectedReport != null) && (Dossier.UnitsInHierarchy.Count() != 0);
            }
        }

        /// <summary>
        ///     Moves the selected scenario report down.
        /// </summary>
        public bool CanMoveReportDown
        {
            get
            {
                return Dossier.ScenarioReports.CurrentPosition <
                       (Dossier.ScenarioReports.Cast<ScenarioReportDecorator>().Count() - 1);
            }
        }

        /// <summary>
        ///     Moves the selected scenario report up.
        /// </summary>
        public bool CanMoveReportUp
        {
            get
            {
                return Dossier.ScenarioReports.CurrentPosition > 0;
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
                return Dossier.ScenarioReports.CurrentItem != null;
            }
        }

        /// <summary>
        ///     Gets or sets the outcome of the scenario.
        /// </summary>
        /// <value>The outcome of the scenario.</value>
        public KeyValuePair<string, ScenarioOutcome> Outcome
        {
            get
            {
                return (SelectedReport != null) ? SelectedReport.Outcome : this._pendingOutcome;
            }
            set
            {
                if (SelectedReport == null)
                {
                    return;
                }

                if (value.Equals(SelectedReport.Outcome))
                {
                    return;
                }

                SelectedReport.Outcome = value;
                NotifyOfPropertyChange(() => Outcome);
                OnModelChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the prestige after a scenario.
        /// </summary>
        /// <value>The prestige after a scenario.</value>
        public int Prestige
        {
            get
            {
                return this._prestige;
            }
            set
            {
                if (SelectedReport == null)
                {
                    return;
                }

                if (value >= 0)
                {
                    ResetPropertyValidationError(() => Prestige);

                    if (!value.Equals(SelectedReport.Prestige))
                    {
                        SelectedReport.Prestige = value;
                        OnModelChanged();
                    }
                }
                else
                {
                    SetPropertyValidationError(() => Prestige, "Prestige must be a non-negative number.");
                }

                this._prestige = value;
                NotifyOfPropertyChange(() => Prestige);
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

        private ScenarioReportDecorator SelectedReport
        {
            get
            {
                return (ScenarioReportDecorator)Dossier.ScenarioReports.CurrentItem;
            }
        }

        #endregion

        #region Instance Methods

        /// <summary>
        ///     Adds a new scenario report.
        /// </summary>
        public void AddReport()
        {
            Dossier.AddNewScenarioReport();
            Refresh();
            OnModelChanged();
        }

        /// <summary>
        ///     Deletes the selected scenario report.
        /// </summary>
        public void DeleteReport()
        {
            Dossier.DeleteSelectedScenarioReport();
            Refresh();
            OnModelChanged();
        }

        /// <summary>
        ///     Triggers the unit report creation.
        /// </summary>
        public IEnumerable<IResult> GenerateUnitReports()
        {
            var dialog = new ReportGenerationViewModel(Dossier.RootUnit);
            DialogResult result = ShowDialog(dialog);

            List<UnitDecorator> units = dialog.MarkedUnits.ToList();

            if (result != DialogResult.Ok || units.Count == 0)
            {
                yield break;
            }

            bool modelChanged = false;

            using (this._busyWatcher.GetTicket())
            {
                yield return
                    new AsyncResult(
                        () =>
                        Parallel.ForEach(
                            units.Where(
                                unit =>
                                !unit.Reports.SourceCollection.Cast<ReportDecorator>()
                                     .Any(report => report.ScenarioName == SelectedReport.ScenarioName)),
                            unit =>
                            {
                                Execute.OnUIThread(unit.AddNewReport);
                                ((ReportDecorator)unit.Reports.CurrentItem).ScenarioName = SelectedReport.ScenarioName;
                                modelChanged = true;
                            }),
                        null,
                        exception => ShowError(exception, Resources.UnableToGenerateReports));

                if (modelChanged)
                {
                    OnModelChanged();
                }
            }
        }

        /// <summary>
        ///     Moves the selected scenario report down.
        /// </summary>
        public void MoveReportDown()
        {
            Dossier.MoveSelectedReportDown();
            Refresh();
            OnModelChanged();
        }

        /// <summary>
        ///     Moves the selected scenario report up.
        /// </summary>
        public void MoveReportUp()
        {
            Dossier.MoveSelectedReportUp();
            Refresh();
            OnModelChanged();
        }

        /// <summary>
        ///     Called when the dossier was changed.
        /// </summary>
        protected override void OnDossierChanged()
        {
            base.OnDossierChanged();

            Dossier.ScenarioReports.CurrentChanged += OnDossierScenarioReportsCurrentChanged;
            Dossier.ScenarioReports.MoveCurrentToLast();

            ResetAllValidationErrors();
            UpdateProperties();
        }

        private void OnDossierScenarioReportsCurrentChanged(object sender, EventArgs e)
        {
            ResetAllValidationErrors();
            UpdateProperties();
            Refresh();
        }

        private void OnModelChanged()
        {
            EventHandler handler = ModelChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private void UpdateProperties()
        {
            ScenarioName = (SelectedReport != null) ? SelectedReport.ScenarioName : string.Empty;
            Prestige = (SelectedReport != null) ? SelectedReport.Prestige : 0;
            Outcome = (SelectedReport != null) ? SelectedReport.Outcome : this._pendingOutcome;
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