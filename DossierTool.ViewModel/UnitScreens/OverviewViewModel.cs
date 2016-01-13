// <copyright file="OverviewViewModel.cs" company="VacuumBreather">
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
    using System.Linq;
    using Decorators;
    using DossierScreens;
    using Model;
    using Model.Helpers;
    using Services;

    #endregion

    /// <summary>
    ///     IUnitScreen implementation for the unit overview page.
    /// </summary>
    [Export(typeof(IUnitScreen))]
    public sealed class OverviewViewModel : UnitScreenBase, IReportModelChanges
    {
        #region Constants

        private const string ScreenName = "Overview";

        private const string ToolTip =
            "Reserve units are not counted for statistics. Use this to mark captured units and other units that should not be counted as 'regular' core units.";

        #endregion

        #region Fields

        private string _unitName = string.Empty;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DossierScreenBase" /> class.
        /// </summary>
        /// <param name="dialogService">The dialog service.</param>
        [ImportingConstructor]
        public OverviewViewModel(IDialogService dialogService)
            : base(0, dialogService)
        {
            DisplayName = ScreenName;
        }

        #endregion

        #region Instance Properties

        /// <summary>
        ///     Gets the available nationalities.
        /// </summary>
        /// <value>The available nationalities.</value>
        public IEnumerable<KeyValuePair<string, Nationality>> AvailableNationalities
        {
            get
            {
                return
                    Enum.GetValues(typeof(Nationality))
                        .Cast<Nationality>()
                        .Select(
                            nationality =>
                            new KeyValuePair<string, Nationality>(nationality.ToDisplayName(), nationality));
            }
        }

        /// <summary>
        ///     Gets the available unit types.
        /// </summary>
        /// <value>The available unit types.</value>
        public IEnumerable<KeyValuePair<string, UnitType>> AvailableTypes
        {
            get
            {
                return
                    Enum.GetValues(typeof(UnitType))
                        .Cast<UnitType>()
                        .Where(type => type.IsCore())
                        .Select(type => new KeyValuePair<string, UnitType>(type.ToDisplayName(), type));
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the selected unit is marked as a reserve unit.
        /// </summary>
        /// <value>A value indicating whether the selected unit is marked as a reserve unit.</value>
        public bool IsUnitReserve
        {
            get
            {
                return Unit.IsReserve;
            }
            set
            {
                if (value.Equals(Unit.IsReserve))
                {
                    return;
                }

                Unit.IsReserve = value;
                NotifyOfPropertyChange(() => IsUnitReserve);
                OnModelChanged();
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the selected unit is a SE unit.
        /// </summary>
        /// <value>A value indicating whether the selected unit is a SE unit.</value>
        public bool IsUnitSpecial
        {
            get
            {
                return Unit.IsSpecial;
            }
            set
            {
                if (value.Equals(Unit.IsSpecial))
                {
                    return;
                }

                Unit.IsSpecial = value;
                NotifyOfPropertyChange(() => IsUnitSpecial);
                OnModelChanged();
            }
        }

        /// <summary>
        ///     Gets the tool tip explaining the reserve status.
        /// </summary>
        /// <value>
        ///     The tool tip explaining the reserve status.
        /// </value>
        public string ReserveToolTip
        {
            get
            {
                return ToolTip;
            }
        }

        /// <summary>
        ///     Gets or sets the unit name.
        /// </summary>
        /// <value>The unit name.</value>
        public string UnitName
        {
            get
            {
                return this._unitName;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    SetPropertyValidationError(() => UnitName, "The name must not be empty.");
                }
                else if (!StringValidator.IsValidString(value))
                {
                    SetPropertyValidationError(() => UnitName, "The name contains invalid characters.");
                }
                else
                {
                    ResetPropertyValidationError(() => UnitName);

                    if (!value.Equals(Unit.Name))
                    {
                        Unit.Name = value;
                        OnModelChanged();
                    }
                }

                this._unitName = value;
                NotifyOfPropertyChange(() => UnitName);
            }
        }

        /// <summary>
        ///     Gets or sets the nationality of the unit.
        /// </summary>
        /// <value>The nationality of the unit.</value>
        public KeyValuePair<string, Nationality> UnitNationality
        {
            get
            {
                return Unit.Nationality;
            }
            set
            {
                if (value.Equals(Unit.Nationality))
                {
                    return;
                }

                Unit.Nationality = value;

                NotifyOfPropertyChange(() => UnitNationality);
                OnModelChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the type of the unit.
        /// </summary>
        /// <value>The type of the unit.</value>
        public KeyValuePair<string, UnitType> UnitType
        {
            get
            {
                return Unit.Type;
            }
            set
            {
                if (value.Equals(Unit.Type))
                {
                    return;
                }

                Unit.Type = value;

                NotifyOfPropertyChange(() => UnitType);
                OnModelChanged();
            }
        }

        #endregion

        #region Instance Methods

        /// <summary>
        ///     Called when the unit was changed.
        /// </summary>
        /// <param name="oldUnit">The old unit.</param>
        /// <param name="newUnit">The new unit.</param>
        protected override void OnUnitChanged(UnitDecorator oldUnit, UnitDecorator newUnit)
        {
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

        private void UpdateProperties()
        {
            UnitName = (Unit != null) ? Unit.Name : string.Empty;
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