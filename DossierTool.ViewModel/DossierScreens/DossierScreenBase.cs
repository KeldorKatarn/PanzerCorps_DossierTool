// <copyright file="DossierScreenBase.cs" company="VacuumBreather">
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
    using System.Linq.Expressions;
    using Caliburn.Micro;
    using Decorators;
    using Dialogs;
    using Services;

    #endregion

    /// <summary>
    ///     Base class for a IDossierScreen.
    /// </summary>
    public abstract class DossierScreenBase : Screen, IDossierScreen
    {
        #region Readonly & Static Fields

        private readonly int _order;
        private readonly IDialogService _dialogService;
        private readonly Dictionary<string, string> _validationErrors = new Dictionary<string, string>();

        #endregion

        #region Fields

        private DossierDecorator _dossier;
        private bool _needsRefresh = true;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DossierScreenBase" /> class.
        /// </summary>
        /// <param name="order">The order of the screen.</param>
        /// <param name="dialogService">The dialog service.</param>
        protected DossierScreenBase(int order, IDialogService dialogService)
        {
            this._order = order;
            this._dialogService = dialogService;
        }

        #endregion

        #region Instance Methods

        /// <summary>
        ///     Called when the dossier was changed.
        /// </summary>
        protected virtual void OnDossierChanged()
        {
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

        /// <summary>
        ///     Determines whether the specified property has any validation errors.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="property">The property expression.</param>
        /// <returns>
        ///     <c>true</c> if the specified property has any validation errors; otherwise, <c>false</c>.
        /// </returns>
        protected bool HasPropertyValidationError<TProperty>(Expression<Func<TProperty>> property)
        {
            string propertyName = property.GetMemberInfo().Name;

            return !string.IsNullOrEmpty(this._validationErrors[propertyName]);
        }

        /// <summary>
        ///     Resets all validation errors.
        /// </summary>
        protected void ResetAllValidationErrors()
        {
            this._validationErrors.Clear();
        }

        /// <summary>
        ///     Resets any validation errors for the specified property.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="property">The property expression.</param>
        protected void ResetPropertyValidationError<TProperty>(Expression<Func<TProperty>> property)
        {
            string propertyName = property.GetMemberInfo().Name;

            this._validationErrors[propertyName] = null;
        }

        /// <summary>
        ///     Sets a validation error for the specified property.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="property">The property expression.</param>
        /// <param name="errorMessage">The error message.</param>
        protected void SetPropertyValidationError<TProperty>(Expression<Func<TProperty>> property, string errorMessage)
        {
            string propertyName = property.GetMemberInfo().Name;

            this._validationErrors[propertyName] = errorMessage;
        }

        /// <summary>
        ///     Shows a dialog.
        /// </summary>
        /// <param name="dialog">The dialog view model.</param>
        protected DialogResult ShowDialog(IDialog dialog)
        {
            return this._dialogService.ShowDialog(dialog);
        }

        /// <summary>
        ///     Shows an error dialog.
        /// </summary>
        /// <param name="exception">The exception that reported the error.</param>
        /// <param name="message">The error message.</param>
        protected void ShowError(Exception exception, string message = null)
        {
            this._dialogService.ShowError(exception, message);
        }

        /// <summary>
        ///     Shows a message box with an OK button.
        /// </summary>
        /// <param name="message">The message.</param>
        protected void ShowMessageBox(string message)
        {
            this._dialogService.ShowMessageBox(message);
        }

        /// <summary>
        ///     Shows a Ok/Cancel dialog.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>the dialog result.</returns>
        protected DialogResult ShowOkCancelDialog(string message)
        {
            return this._dialogService.ShowDialog(message, DialogButtons.OKCancel);
        }

        /// <summary>
        ///     Shows a Yes/No/Cancel dialog.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>the dialog result.</returns>
        protected DialogResult ShowYesNoCancelDialog(string message)
        {
            return this._dialogService.ShowDialog(message, DialogButtons.YesNoCancel);
        }

        /// <summary>
        ///     Shows a Yes/No dialog.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>the dialog result.</returns>
        protected DialogResult ShowYesNoDialog(string message)
        {
            return this._dialogService.ShowDialog(message, DialogButtons.YesNo);
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
        }

        #endregion
    }
}