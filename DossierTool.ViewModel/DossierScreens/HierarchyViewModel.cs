// <copyright file="HierarchyViewModel.cs" company="VacuumBreather">
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
    using Caliburn.Micro;
    using Decorators;
    using Dialogs;
    using Model;
    using Properties;
    using Services;
    using UnitScreens;

    #endregion

    /// <summary>
    ///     IDossierScreen implementation for the unit hierarchy page of the dossier.
    /// </summary>
    [Export(typeof(IDossierScreen))]
    public sealed class HierarchyViewModel : Conductor<IUnitDecoratorScreen>.Collection.OneActive,
                                             IDossierScreen,
                                             IReportModelChanges
    {
        #region Constants

        private const string Name = "Hierarchy";
        private const int ScreenOrder = 1;

        #endregion

        #region Readonly & Static Fields

        private readonly List<IUnitScreen> _unitScreens = new List<IUnitScreen>();
        private readonly List<IHigherUnitScreen> _higherUnitScreens = new List<IHigherUnitScreen>();

        private readonly int _order;
        private readonly IDialogService _dialogService;
        private readonly Dictionary<string, string> _validationErrors = new Dictionary<string, string>();

        #endregion

        #region Fields

        private DossierDecorator _dossier;
        private IUnitDecorator _selectedUnit;
        private bool _needsRefresh;
        private IUnitScreen _lastActiveUnitScreen;
        private IHigherUnitScreen _lastActiveHigherUnitScreen;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="HierarchyViewModel" /> class.
        /// </summary>
        /// <param name="unitScreens">The unit screens.</param>
        /// <param name="higherunitScreens">The higherunit screens.</param>
        /// <param name="dialogService">The dialog service.</param>
        [ImportingConstructor]
        public HierarchyViewModel([ImportMany] IEnumerable<IUnitScreen> unitScreens,
                                  [ImportMany] IEnumerable<IHigherUnitScreen> higherunitScreens,
                                  IDialogService dialogService)
        {
            this._dialogService = dialogService;
            this._order = ScreenOrder;
            DisplayName = Name;

            this._unitScreens.AddRange(unitScreens.OrderBy(screen => screen.Order));
            this._higherUnitScreens.AddRange(higherunitScreens.OrderBy(screen => screen.Order));

            foreach (var screen in this._unitScreens.Concat<IUnitDecoratorScreen>(this._higherUnitScreens))
            {
                var reportingScreen = screen as IReportModelChanges;

                if (reportingScreen != null)
                {
                    reportingScreen.ModelChanged += (sender, e) => OnModelChanged();
                }
            }
        }

        #endregion

        #region Instance Properties

        /// <summary>
        ///     Gets the root unit.
        /// </summary>
        /// <value>
        ///     The root unit.
        /// </value>
        public HigherUnitDecorator RootUnit
        {
            get
            {
                return Dossier.RootUnit;
            }
        }

        /// <summary>
        ///     Gets or sets the selected unit.
        /// </summary>
        /// <value>
        ///     The selected unit.
        /// </value>
        public IUnitDecorator SelectedUnit
        {
            get
            {
                return this._selectedUnit;
            }
            set
            {
                if (value == this._selectedUnit)
                {
                    return;
                }

                IUnitDecorator oldUnit = this._selectedUnit;

                this._selectedUnit = value;

                NotifyOfPropertyChange(() => SelectedUnit);

                if (HasUnitTypeChanged(oldUnit, this._selectedUnit))
                {
                    UpdateScreens();
                }

                foreach (var screen in Items)
                {
                    screen.UnitDecorator = this._selectedUnit;
                }
            }
        }

        #endregion

        #region Class Methods

        private static bool HasUnitTypeChanged(IUnitDecorator oldUnit, IUnitDecorator newUnit)
        {
            bool hasUnitTypeChanged = (oldUnit is UnitDecorator) ^ (newUnit is UnitDecorator);
            bool isOneUnitNull = (oldUnit == null) ^ (newUnit == null);

            return hasUnitTypeChanged || isOneUnitNull;
        }

        #endregion

        #region Instance Methods

        /// <summary>
        ///     Adds a hierarchy level.
        /// </summary>
        public void AddHierarchyLevel()
        {
            IUnitDecorator newHierarchyLevel = RootUnit.AddNewHigherUnit();
            SelectedUnit = newHierarchyLevel;
            ActivateItem(Items.FirstOrDefault());

            OnModelChanged();
        }

        /// <summary>
        ///     Adds a unit.
        /// </summary>
        public void AddUnit()
        {
            IUnitDecorator newUnit = RootUnit.AddNewUnit();
            SelectedUnit = newUnit;
            ActivateItem(Items.FirstOrDefault());

            OnModelChanged();
        }

        /// <summary>
        ///     Deletes the selected unit.
        /// </summary>
        public void DeleteUnit()
        {
            if (SelectedUnit == null)
            {
                return;
            }

            var hierarchyLevel = SelectedUnit as HigherUnitDecorator;
            var unit = SelectedUnit as UnitDecorator;

            if (hierarchyLevel != null && !hierarchyLevel.Subordinates.IsEmpty)
            {
                DialogResult result = ShowYesNoCancelDialog(Resources.HierarchyContainsUnits);

                if (result != DialogResult.Yes)
                {
                    return;
                }
            }
            else if (unit != null && !unit.Reports.IsEmpty)
            {
                DialogResult result = ShowYesNoCancelDialog(Resources.UnitContainsReports);

                if (result != DialogResult.Yes)
                {
                    return;
                }
            }

            SelectedUnit.Superior.RemoveSubordinate((UnitBase)SelectedUnit);

            OnModelChanged();
        }

        /// <summary>
        ///     Moves a unit to a new location within the hierarchy.
        /// </summary>
        /// <param name="unit">The moved unit.</param>
        /// <param name="target">The new suprtior of the unit.</param>
        public void MoveUnitInHierarchy(IUnitDecorator unit, HigherUnitDecorator target)
        {
            if (unit.Superior != null)
            {
                if (unit.Superior == target)
                {
                    return;
                }

                unit.Superior.RemoveSubordinate((UnitBase)unit);
            }

            target.AddSubordinate((UnitBase)unit);

            SelectedUnit = unit;

            RootUnit.Subordinates.Refresh();

            OnModelChanged();
        }

        /// <summary>
        ///     Called when the screen is activated.
        /// </summary>
        protected override void OnActivate()
        {
            base.OnActivate();

            if (this._needsRefresh)
            {
                Refresh();
                this._needsRefresh = false;
            }
        }

        private void OnDossierChanged()
        {
            SelectedUnit = Dossier.RootUnit.Subordinates.Cast<IUnitDecorator>().FirstOrDefault();
        }

        private void OnModelChanged()
        {
            EventHandler handler = ModelChanged;

            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private DialogResult ShowYesNoCancelDialog(string message)
        {
            return this._dialogService.ShowDialog(message, DialogButtons.YesNoCancel);
        }

        private void UpdateScreens()
        {
            var activeUnitScreen = ActiveItem as IUnitScreen;
            var activeHigherUnitScreen = ActiveItem as IHigherUnitScreen;

            if (activeUnitScreen != null)
            {
                this._lastActiveUnitScreen = activeUnitScreen;
            }

            if (activeHigherUnitScreen != null)
            {
                this._lastActiveHigherUnitScreen = activeHigherUnitScreen;
            }

            Items.Clear();

            if (SelectedUnit is HigherUnitDecorator)
            {
                Items.AddRange(this._higherUnitScreens);
                ActivateItem(this._lastActiveHigherUnitScreen ?? Items.FirstOrDefault());
            }
            else if (SelectedUnit is UnitDecorator)
            {
                Items.AddRange(this._unitScreens);
                ActivateItem(this._lastActiveUnitScreen ?? Items.FirstOrDefault());
            }
        }

        #endregion

        #region IDossierScreen Members

        /// <summary>
        ///     Gets or sets the underlying dossier.
        /// </summary>
        /// <value>
        ///     The underlying dossier.
        /// </value>
        public DossierDecorator Dossier
        {
            get
            {
                return this._dossier;
            }
            set
            {
                if (value == this._dossier)
                {
                    return;
                }

                this._dossier = value;
                OnDossierChanged();
                Refresh();
            }
        }

        /// <summary>
        ///     Gets the error message for the property with the given name.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>The error message.</returns>
        public string this[string columnName]
        {
            get
            {
                string errorMessage;

                this._validationErrors.TryGetValue(columnName, out errorMessage);

                return errorMessage ?? string.Empty;
            }
        }

        /// <summary>
        ///     Gets an error message indicating what is wrong with this object.
        /// </summary>
        /// <value>An error message indicating what is wrong with this object. The default is an empty string ("").</value>
        /// <returns>An error message indicating what is wrong with this object. The default is an empty string ("").</returns>
        public string Error
        {
            get
            {
                return string.Empty;
            }
        }

        /// <summary>
        ///     Gets the order of this screen.
        /// </summary>
        /// <value>
        ///     The order of this screen.
        /// </value>
        public int Order
        {
            get
            {
                return this._order;
            }
        }

        /// <summary>
        ///     Requests the refreshing of the screen.
        /// </summary>
        public void RequestRefresh()
        {
            if (!IsActive)
            {
                this._needsRefresh = true;
            }

            foreach (var screen in this._unitScreens.Concat<IUnitDecoratorScreen>(this._higherUnitScreens))
            {
                screen.RequestRefresh();
            }
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