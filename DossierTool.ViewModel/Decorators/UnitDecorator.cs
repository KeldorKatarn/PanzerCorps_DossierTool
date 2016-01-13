// <copyright file="UnitDecorator.cs" company="VacuumBreather">
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
    using Model;
    using Model.Helpers;
    using Services;

    #endregion

    /// <summary>
    ///     The <see cref="IDecorator" /> for a <see cref="Unit" />.
    /// </summary>
    [DataContract]
    public sealed class UnitDecorator : Unit, IUnitDecorator
    {
        #region Constants

        private const string NationalityIconPath = "/Content/UI/purchase/flags/";
        private const string NationalityImagePath = "/Content/Images/Flags/";

        private const string TypeIconPath = "/Content/UI/purchase/classes/";

        #endregion

        #region Readonly & Static Fields

        private readonly IDecoratorService _decoratorService;
        private readonly ListCollectionView _reportsView;

        #endregion

        #region Fields

        private KeyValuePair<string, UnitType> _type;
        private KeyValuePair<string, Nationality> _nationality;
        private bool _isSelected;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="UnitDecorator" /> class.
        /// </summary>
        /// <param name="unit">The <see cref="Unit" /> this view model represents.</param>
        /// <param name="decoratorService">The <see cref="IDecoratorService" />.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="unit" /> or <paramref name="decoratorService" />is a null
        ///     reference.
        /// </exception>
        public UnitDecorator(Unit unit, IDecoratorService decoratorService)
        {
            Contract.Requires<ArgumentNullException>(unit != null);
            Contract.Requires<ArgumentNullException>(decoratorService != null);

            this._decoratorService = decoratorService;

            Name = unit.Name;
            IsSpecial = unit.IsSpecial;
            IsReserve = unit.IsReserve;

            Type = new KeyValuePair<string, UnitType>(unit.Type.ToDisplayName(), unit.Type);
            Nationality = new KeyValuePair<string, Nationality>(unit.Nationality.ToDisplayName(), unit.Nationality);

            this._reportsView = CollectionViewSource.GetDefaultView(base.Reports) as ListCollectionView;

            Contract.Assert(this._reportsView != null);

            ((INotifyCollectionChanged)this._reportsView).CollectionChanged += OnReportsCollectionChanged;

            foreach (var report in unit.Reports)
            {
                AddNewReport(this._decoratorService.Decorate(report, this));
            }

            this._reportsView.Refresh();
        }

        #endregion

        #region Object Invariant

        [ContractInvariantMethod]
        private void Invariant()
        {
            Contract.Invariant(this._decoratorService != null);
            Contract.Invariant(this._reportsView != null);
        }

        #endregion

        #region Instance Properties

        /// <summary>
        ///     Gets or sets a value indicating whether this unit is marked as a reserve unit.
        /// </summary>
        /// <value>A value indicating whether this unit is marked as a reserve unit.</value>
        public override bool IsReserve
        {
            get
            {
                return base.IsReserve;
            }
            set
            {
                if (value == base.IsReserve)
                {
                    return;
                }

                base.IsReserve = value;
                NotifyOfPropertyChange(() => IsReserve);
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this unit is a SE unit.
        /// </summary>
        /// <value>A value indicating whether this unit is a SE unit.</value>
        public override bool IsSpecial
        {
            get
            {
                return base.IsSpecial;
            }
            set
            {
                if (value.Equals(IsSpecial))
                {
                    return;
                }

                base.IsSpecial = value;
                NotifyOfPropertyChange(() => IsSpecial);
            }
        }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public override string Name
        {
            get
            {
                return base.Name;
            }
            set
            {
                if (value.Equals(Name))
                {
                    return;
                }

                base.Name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        /// <summary>
        ///     Gets the current equipment.
        /// </summary>
        /// <value>
        ///     The current equipment.
        /// </value>
        public Equipment CurrentEquipment
        {
            get
            {
                var lastReport = (ReportDecorator)base.Reports.LastOrDefault();

                return (lastReport != null) ? lastReport.Equipment : Equipment.None;
            }
        }

        /// <summary>
        ///     Gets the current experience.
        /// </summary>
        /// <value>
        ///     The current experience.
        /// </value>
        public int CurrentExperience
        {
            get
            {
                Report lastReport = base.Reports.LastOrDefault();

                return (lastReport != null) ? lastReport.Experience : 0;
            }
        }

        /// <summary>
        ///     Gets the current first hero.
        /// </summary>
        /// <value>
        ///     The current first hero.
        /// </value>
        public KeyValuePair<string, Hero> CurrentFirstHero
        {
            get
            {
                var lastReport = (ReportDecorator)base.Reports.LastOrDefault();

                return (lastReport != null)
                           ? lastReport.FirstHero
                           : new KeyValuePair<string, Hero>(Hero.None.DisplayName, Hero.None);
            }
        }

        /// <summary>
        ///     Gets the current highest award.
        /// </summary>
        /// <value>
        ///     The current highest award.
        /// </value>
        public KeyValuePair<string, Award> CurrentHighestAward
        {
            get
            {
                var lastReport = (ReportDecorator)base.Reports.LastOrDefault();

                return (lastReport != null)
                           ? lastReport.HighestAward
                           : new KeyValuePair<string, Award>(Award.None.DisplayName, Award.None);
            }
        }

        /// <summary>
        ///     Gets the current highest award image.
        /// </summary>
        /// <value>
        ///     The current highest award image.
        /// </value>
        public string CurrentHighestAwardImage
        {
            get
            {
                var lastReport = (ReportDecorator)base.Reports.LastOrDefault();

                return (lastReport != null) ? lastReport.HighestAward.Value.ImageFile : null;
            }
        }

        /// <summary>
        ///     Gets the current kills.
        /// </summary>
        /// <value>
        ///     The current kills.
        /// </value>
        public int CurrentKills
        {
            get
            {
                Report lastReport = base.Reports.LastOrDefault();

                return (lastReport != null) ? lastReport.Kills : 0;
            }
        }

        /// <summary>
        ///     Gets the current land transport.
        /// </summary>
        /// <value>
        ///     The current land transport.
        /// </value>
        public Equipment CurrentLandTransport
        {
            get
            {
                var lastReport = (ReportDecorator)base.Reports.LastOrDefault();

                return (lastReport != null) ? lastReport.LandTransport : Equipment.None;
            }
        }

        /// <summary>
        ///     Gets the current losses.
        /// </summary>
        /// <value>
        ///     The current losses.
        /// </value>
        public int CurrentLosses
        {
            get
            {
                Report lastReport = base.Reports.LastOrDefault();

                return (lastReport != null) ? lastReport.Losses : 0;
            }
        }

        /// <summary>
        ///     Gets the current second hero.
        /// </summary>
        /// <value>
        ///     The current second hero.
        /// </value>
        public KeyValuePair<string, Hero> CurrentSecondHero
        {
            get
            {
                var lastReport = (ReportDecorator)base.Reports.LastOrDefault();

                return (lastReport != null)
                           ? lastReport.SecondHero
                           : new KeyValuePair<string, Hero>(Hero.None.DisplayName, Hero.None);
            }
        }

        /// <summary>
        ///     Gets the current third hero.
        /// </summary>
        /// <value>
        ///     The current third hero.
        /// </value>
        public KeyValuePair<string, Hero> CurrentThirdHero
        {
            get
            {
                var lastReport = (ReportDecorator)base.Reports.LastOrDefault();

                return (lastReport != null)
                           ? lastReport.ThirdHero
                           : new KeyValuePair<string, Hero>(Hero.None.DisplayName, Hero.None);
            }
        }

        /// <summary>
        ///     Gets or sets the nationality of the unit.
        /// </summary>
        /// <value>The nationality of the unit.</value>
        public new KeyValuePair<string, Nationality> Nationality
        {
            get
            {
                return this._nationality;
            }
            set
            {
                if (value.Equals(Nationality))
                {
                    return;
                }

                this._nationality = value;
                base.Nationality = value.Value;

                NotifyOfPropertyChange(() => Nationality);
                NotifyOfPropertyChange(() => NationalityIcon);
                NotifyOfPropertyChange(() => NationalityImage);
            }
        }

        /// <summary>
        ///     Gets the nationality icon.
        /// </summary>
        /// <value>
        ///     The nationality icon.
        /// </value>
        public string NationalityIcon
        {
            get
            {
                return NationalityIconPath + base.Nationality.ToFlagPath();
            }
        }

        /// <summary>
        ///     Gets the nationality image.
        /// </summary>
        /// <value>
        ///     The nationality image.
        /// </value>
        public string NationalityImage
        {
            get
            {
                return NationalityImagePath + base.Nationality.ToFlagPath();
            }
        }

        /// <summary>
        ///     Gets the reports.
        /// </summary>
        /// <value>The reports.</value>
        public new ICollectionView Reports
        {
            get
            {
                return this._reportsView;
            }
        }

        /// <summary>
        ///     Gets or sets the type of the unit.
        /// </summary>
        /// <value>The type of the unit.</value>
        public new KeyValuePair<string, UnitType> Type
        {
            get
            {
                return this._type;
            }
            set
            {
                if (value.Equals(Type))
                {
                    return;
                }

                this._type = value;
                base.Type = value.Value;

                NotifyOfPropertyChange(() => Type);
                NotifyOfPropertyChange(() => TypeIcon);
            }
        }

        /// <summary>
        ///     Gets the unit type icon.
        /// </summary>
        /// <value>
        ///     The unit type icon.
        /// </value>
        public string TypeIcon
        {
            get
            {
                return TypeIconPath + base.Type.ToIconPath();
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
        ///     Adds a new report.
        /// </summary>
        public void AddNewReport()
        {
            ReportDecorator report = this._decoratorService.Decorate(base.Reports.LastOrDefault() ?? new Report(), this);
            report.ScenarioName = Report.NewScenario;

            report.PropertyChanged += OnReportDecoratorPropertyChanged;

            this._reportsView.AddNewItem(report);
            this._reportsView.CommitNew();
            this._reportsView.MoveCurrentTo(report);
        }

        /// <summary>
        ///     Deletes the selected report.
        /// </summary>
        public void DeleteSelectedReport()
        {
            var currentReport = (ReportDecorator)this._reportsView.CurrentItem;

            currentReport.PropertyChanged -= OnReportDecoratorPropertyChanged;

            this._reportsView.Remove(currentReport);
        }

        /// <summary>
        ///     Moves the selected report one step down in the list.
        /// </summary>
        public void MoveSelectedReportDown()
        {
            object movedReport = this._reportsView.CurrentItem;
            int index = this._reportsView.CurrentPosition;

            Contract.Assume(index >= 0 && index < this._reportsView.Count - 1);

            MoveReportDownAt(index);

            this._reportsView.Refresh();
            this._reportsView.MoveCurrentTo(movedReport);
        }

        /// <summary>
        ///     Moves the selected report one step up in the list.
        /// </summary>
        public void MoveSelectedReportUp()
        {
            object movedReport = this._reportsView.CurrentItem;
            int index = this._reportsView.CurrentPosition;

            Contract.Assume(index >= 0 && index < this._reportsView.Count - 1);

            MoveReportUpAt(index);

            this._reportsView.Refresh();
            this._reportsView.MoveCurrentTo(movedReport);
        }

        private void NotifyOfPropertyChange(string propertyName)
        {
            Execute.OnUIThread(((() => OnPropertyChanged(new PropertyChangedEventArgs(propertyName)))));
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

        private void OnReportDecoratorPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Refresh();
        }

        private void OnReportsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            NotifyOfPropertyChange(string.Empty);
        }

        #endregion

        #region IUnitDecorator Members

        /// <summary>
        ///     Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is selected in a list or tree view.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is selected; otherwise, <c>false</c>.
        /// </value>
        public bool IsSelected
        {
            get
            {
                return this._isSelected;
            }
            set
            {
                if (value == this._isSelected)
                {
                    return;
                }

                this._isSelected = value;

                NotifyOfPropertyChange(() => IsSelected);
            }
        }

        /// <summary>
        ///     Gets or sets the superior.
        /// </summary>
        /// <value>The superior.</value>
        public new HigherUnitDecorator Superior
        {
            get
            {
                return (HigherUnitDecorator)base.Superior;
            }
            set
            {
                base.Superior = value;
            }
        }

        #endregion
    }
}