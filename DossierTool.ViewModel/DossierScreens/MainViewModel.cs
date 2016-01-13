// <copyright file="MainViewModel.cs" company="VacuumBreather">
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
    using System.ComponentModel.Composition;
    using Model.Helpers;
    using Services;

    #endregion

    /// <summary>
    ///     IDossierScreen implementation for the main page of the dossier.
    /// </summary>
    [Export(typeof(IDossierScreen))]
    public sealed class MainViewModel : DossierScreenBase, IReportModelChanges
    {
        #region Constants

        private const string Name = "Main";

        #endregion

        #region Fields

        private bool _isEditingName;
        private string _dossierName;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MainViewModel" /> class.
        /// </summary>
        /// <param name="dialogService">The dialog service.</param>
        [ImportingConstructor]
        public MainViewModel(IDialogService dialogService)
            : base(0, dialogService)
        {
            DisplayName = Name;
        }

        #endregion

        #region Instance Properties

        /// <summary>
        ///     Gets a value indicating whether this ViewModel can accept the new name of the dossier.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this ViewModel can accept the new name of the dossier; otherwise, <c>false</c>.
        /// </value>
        public bool CanAcceptRenaming
        {
            get
            {
                return IsEditingName && !HasPropertyValidationError(() => DossierName);
            }
        }

        /// <summary>
        ///     Gets a value indicating whether this ViewModel can cancel the dossier renaming.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this ViewModel can cancel the dossier renaming; otherwise, <c>false</c>.
        /// </value>
        public bool CanCancelRenaming
        {
            get
            {
                return IsEditingName;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether this ViewModel can start the dossier renaming.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this ViewModel can start the dossier renaming; otherwise, <c>false</c>.
        /// </value>
        public bool CanStartRenaming
        {
            get
            {
                return !IsEditingName;
            }
        }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string DossierName
        {
            get
            {
                return this._dossierName;
            }
            set
            {
                if (value == this._dossierName)
                {
                    return;
                }

                if (string.IsNullOrEmpty(value))
                {
                    SetPropertyValidationError(() => DossierName, "The name must not be empty.");
                }
                else if (!StringValidator.IsValidString(value))
                {
                    SetPropertyValidationError(() => DossierName, "The name contains invalid characters.");
                }
                else
                {
                    ResetPropertyValidationError(() => DossierName);
                }

                this._dossierName = value;
                NotifyOfPropertyChange(() => DossierName);
                Refresh();
            }
        }

        /// <summary>
        ///     Gets a value indicating whether the dossier name can currently be edited.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the dossier name can currently be edited; otherwise, <c>false</c>.
        /// </value>
        public bool IsEditingName
        {
            get
            {
                return this._isEditingName;
            }
            private set
            {
                if (value == IsEditingName)
                {
                    return;
                }

                this._isEditingName = value;
                NotifyOfPropertyChange(() => IsEditingName);
            }
        }

        #endregion

        #region Instance Methods

        /// <summary>
        ///     Toggles the renaming.
        /// </summary>
        public void AcceptRenaming()
        {
            if (Dossier.Name != DossierName)
            {
                Dossier.Name = DossierName;
                OnModelChanged();
            }

            IsEditingName = false;
            Refresh();
        }

        /// <summary>
        ///     Toggles the renaming.
        /// </summary>
        public void CancelRenaming()
        {
            DossierName = Dossier.Name;
            IsEditingName = false;
            Refresh();
        }

        /// <summary>
        ///     Toggles the renaming.
        /// </summary>
        public void StartRenaming()
        {
            IsEditingName = true;
            Refresh();
        }

        /// <summary>
        ///     Called when the screen is deactivated.
        /// </summary>
        /// <param name="close">if set to <c>true</c> the screen will close.</param>
        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);

            if (CanCancelRenaming)
            {
                CancelRenaming();
            }
        }

        /// <summary>
        ///     Called when the dossier was changed.
        /// </summary>
        protected override void OnDossierChanged()
        {
            if (CanCancelRenaming)
            {
                CancelRenaming();
            }

            DossierName = Dossier.Name;
        }

        private void OnModelChanged()
        {
            EventHandler handler = ModelChanged;

            if (handler != null)
            {
                handler(this, EventArgs.Empty);
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