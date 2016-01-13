// <copyright file="DossierDecorator.cs" company="VacuumBreather">
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
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.Serialization;
    using System.Windows.Data;
    using Caliburn.Micro;
    using Helpers;
    using Model;
    using Services;

    #endregion

    /// <summary>
    ///     The <see cref="IDecorator" /> for a <see cref="Dossier" />.
    /// </summary>
    [DataContract]
    public sealed class DossierDecorator : Dossier, IDecorator
    {
        #region Readonly & Static Fields

        private readonly IDecoratorService _decoratorService;
        private readonly ListCollectionView _scenarioReportsView;

        #endregion

        #region Fields

        private UnitBase _selectedUnit;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DossierDecorator" /> class.
        /// </summary>
        /// <param name="dossier">The <see cref="Dossier" /> to decorate.</param>
        /// <param name="decoratorService">The <see cref="IDecoratorService" />.</param>
        public DossierDecorator(Dossier dossier, IDecoratorService decoratorService)
        {
            this._decoratorService = decoratorService;

            Name = dossier.Name;
            base.RootUnit = this._decoratorService.Decorate(dossier.RootUnit);

            this._scenarioReportsView = CollectionViewSource.GetDefaultView(base.ScenarioReports) as ListCollectionView;

            Contract.Assert(this._scenarioReportsView != null);

            ((INotifyCollectionChanged)this._scenarioReportsView).CollectionChanged +=
                OnScenarioReportsCollectionChanged;

            foreach (var report in dossier.ScenarioReports)
            {
                AddNewReport(this._decoratorService.Decorate(report));
            }
            this._scenarioReportsView.Refresh();

            foreach (var unit in UnitsInHierarchy)
            {
                unit.PropertyChanged += OnUnitDecoratorPropertyChanged;
            }
        }

        #endregion

        #region Object Invariant

        [ContractInvariantMethod]
        private void Invariant()
        {
            Contract.Invariant(this._decoratorService != null);
            Contract.Invariant(this._scenarioReportsView != null);
        }

        #endregion

        #region Instance Properties

        /// <summary>
        ///     Gets the root of the unit hierarchy of the dossier.
        /// </summary>
        /// <value>The root of the unit hierarchy of the dossier.</value>
        public new HigherUnitDecorator RootUnit
        {
            get
            {
                return (HigherUnitDecorator)base.RootUnit;
            }
            set
            {
                base.RootUnit = value;
            }
        }

        /// <summary>
        ///     Gets the scenario reports.
        /// </summary>
        /// <value>The scenario reports.</value>
        public new ICollectionView ScenarioReports
        {
            get
            {
                return this._scenarioReportsView;
            }
        }

        /// <summary>
        ///     Gets or sets the selected unit.
        /// </summary>
        /// <value>
        ///     The selected unit.
        /// </value>
        public UnitBase SelectedUnit
        {
            get
            {
                return this._selectedUnit;
            }
            set
            {
                if (value == this._selectedUnit)
                {
                    this._selectedUnit = value;
                }

                NotifyOfPropertyChange(() => SelectedUnit);
            }
        }

        /// <summary>
        ///     Gets all <see cref="Unit">Units</see> in the hierarchy.
        /// </summary>
        /// <value>All <see cref="Unit">Units</see> in the hierarchy.</value>
        public IEnumerable<UnitDecorator> UnitsInHierarchy
        {
            get
            {
                return HierarchyHelper.GetUnitsAlongHierarchy(RootUnit).Cast<UnitDecorator>();
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

        /// <summary>
        ///     Adds a new <see cref="HigherUnit" /> to the root unit's list of subordinates.
        /// </summary>
        public void AddNewHigherUnit()
        {
            HigherUnitDecorator higherUnit = this._decoratorService.Decorate(new HigherUnit());
            higherUnit.PropertyChanged += OnHigherUnitDecoratorPropertyChanged;

            RootUnit.AddSubordinate(higherUnit);
            SelectedUnit = higherUnit;
        }

        /// <summary>
        ///     Adds a new report.
        /// </summary>
        public void AddNewScenarioReport()
        {
            ScenarioReportDecorator scenarioReport = this._decoratorService.Decorate(new ScenarioReport());
            scenarioReport.PropertyChanged += OnScenarioReportDecoratorPropertyChanged;

            this._scenarioReportsView.AddNewItem(scenarioReport);
            this._scenarioReportsView.CommitNew();
            this._scenarioReportsView.MoveCurrentTo(scenarioReport);
        }

        /// <summary>
        ///     Adds a new <see cref="Unit" /> to the root unit's list of subordinates.
        /// </summary>
        public void AddNewUnit()
        {
            UnitDecorator unit = this._decoratorService.Decorate(new Unit());
            unit.PropertyChanged += OnUnitDecoratorPropertyChanged;

            RootUnit.AddSubordinate(unit);
            SelectedUnit = unit;
        }

        /// <summary>
        ///     Deletes the selected report.
        /// </summary>
        public void DeleteSelectedScenarioReport()
        {
            var report = (ScenarioReportDecorator)this._scenarioReportsView.CurrentItem;

            report.PropertyChanged -= OnScenarioReportDecoratorPropertyChanged;

            this._scenarioReportsView.Remove(report);
        }

        /// <summary>
        ///     Deletes the selected unit.
        /// </summary>
        public void DeleteSelectedUnit()
        {
            Contract.Assume(SelectedUnit.Superior != null);

            // This temporary reference is necessary because setting 
            // IsSelected to true false sets SelectedUnit to null.
            UnitBase currentlySelectedUnit = SelectedUnit;

            if (currentlySelectedUnit is IUnitDecorator)
            {
                var selectedUnit = (IUnitDecorator)currentlySelectedUnit;

                selectedUnit.PropertyChanged -= OnUnitDecoratorPropertyChanged;
                selectedUnit.IsSelected = false;
                selectedUnit.Superior.RemoveSubordinate(currentlySelectedUnit);
            }

            SelectedUnit = null;
        }

        /// <summary>
        ///     Moves the selected report one step down in the list.
        /// </summary>
        public void MoveSelectedReportDown()
        {
            object movedReport = this._scenarioReportsView.CurrentItem;
            int index = this._scenarioReportsView.CurrentPosition;

            Contract.Assume(index >= 0 && index < this._scenarioReportsView.Count - 1);

            MoveReportDownAt(index);

            this._scenarioReportsView.Refresh();
            this._scenarioReportsView.MoveCurrentTo(movedReport);
        }

        /// <summary>
        ///     Moves the selected report one step up in the list.
        /// </summary>
        public void MoveSelectedReportUp()
        {
            object movedReport = this._scenarioReportsView.CurrentItem;
            int index = this._scenarioReportsView.CurrentPosition;

            Contract.Assume(index >= 0 && index < this._scenarioReportsView.Count - 1);

            MoveReportUpAt(index);

            this._scenarioReportsView.Refresh();
            this._scenarioReportsView.MoveCurrentTo(movedReport);
        }

        private void NotifyOfPropertyChange(string propertyName)
        {
            Execute.OnUIThread(() => OnPropertyChanged(new PropertyChangedEventArgs(propertyName)));
        }

        private void NotifyOfPropertyChange<TProperty>(Expression<Func<TProperty>> property)
        {
            NotifyOfPropertyChange((property).GetMemberInfo().Name);
        }

        private void OnHigherUnitDecoratorPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Refresh();
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

        private void OnScenarioReportDecoratorPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Refresh();
        }

        private void OnScenarioReportsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Refresh();
        }

        private void OnUnitDecoratorPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            NotifyOfPropertyChange(string.Empty);
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