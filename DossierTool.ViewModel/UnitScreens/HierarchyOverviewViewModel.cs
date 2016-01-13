// <copyright file="HierarchyOverviewViewModel.cs" company="VacuumBreather">
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
    using System.ComponentModel.Composition;
    using Decorators;
    using DossierScreens;
    using Model.Helpers;
    using Services;

    #endregion

    /// <summary>
    ///     IUnitScreen implementation for the unit hierarchy overview page.
    /// </summary>
    [Export(typeof(IHigherUnitScreen))]
    public sealed class HierarchyOverviewViewModel : HigherUnitScreenBase, IReportModelChanges
    {
        #region Constants

        private const string ScreenName = "Overview";

        #endregion

        #region Fields

        private string _unitName = string.Empty;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="HierarchyOverviewViewModel" /> class.
        /// </summary>
        /// <param name="dialogService">The dialog service.</param>
        [ImportingConstructor]
        public HierarchyOverviewViewModel(IDialogService dialogService)
            : base(0, dialogService)
        {
            DisplayName = ScreenName;
        }

        #endregion

        #region Instance Properties

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

        #endregion

        #region Instance Methods

        /// <summary>
        ///     Called when the unit was changed.
        /// </summary>
        /// <param name="oldUnit">The old unit.</param>
        /// <param name="newUnit">The new unit.</param>
        protected override void OnUnitChanged(HigherUnitDecorator oldUnit, HigherUnitDecorator newUnit)
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