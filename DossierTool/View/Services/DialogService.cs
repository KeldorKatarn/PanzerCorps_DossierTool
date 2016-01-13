// <copyright file="DialogService.cs" company="VacuumBreather">
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

namespace DossierTool.View.Services
{
    #region Using Directives

    using System;
    using System.ComponentModel.Composition;
    using System.Windows;
    using Caliburn.Micro;
    using ViewModel.Dialogs;
    using ViewModel.Helpers;
    using ViewModel.Services;

    #endregion

    /// <summary>
    ///     Window manager based implementation of the dialog service.
    /// </summary>
    [Export(typeof(IDialogService))]
    public class DialogService : IDialogService
    {
        #region Readonly & Static Fields

        private readonly IWindowManager _windowManager;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initialisiert eine neue Instanz der <see cref="T:System.Object" />-Klasse.
        /// </summary>
        [ImportingConstructor]
        public DialogService(IWindowManager windowManager)
        {
            this._windowManager = windowManager;
        }

        #endregion

        #region IDialogService Members

        /// <summary>
        ///     Shows a dialog.
        /// </summary>
        /// <param name="viewModel">The dialog view model.</param>
        /// <returns>
        ///     the dialog result.
        /// </returns>
        public DialogResult ShowDialog(IDialog viewModel)
        {
            this._windowManager.ShowDialog(viewModel);

            return viewModel.DialogResult;
        }

        /// <summary>
        ///     Shows an error dialog.
        /// </summary>
        /// <param name="exception">The exception that reported the error.</param>
        /// <param name="message">The error message.</param>
        public void ShowError(Exception exception, string message = null)
        {
            string exceptionMessage = (exception.InnerException != null)
                                          ? exception.InnerException.Message
                                          : exception.Message;

            string errorMessage = string.IsNullOrEmpty(message)
                                      ? string.Format("{0}", exceptionMessage)
                                      : string.Format("{0}\n\n{1}", message, exceptionMessage);

            Clipboard.SetText(errorMessage);

            this._windowManager.ShowDialog(new MessageBoxViewModel(ApplicationInfo.ProductTitle, errorMessage));
        }

        /// <summary>
        ///     Shows a message box with an OK button.
        /// </summary>
        /// <param name="message">The message.</param>
        public void ShowMessageBox(string message)
        {
            this._windowManager.ShowDialog(new MessageBoxViewModel(ApplicationInfo.ProductTitle, message));
        }

        /// <summary>
        ///     Shows a dialog.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="dialogButtons">The buttons to display.</param>
        /// <returns>
        ///     the dialog result.
        /// </returns>
        public DialogResult ShowDialog(string message, DialogButtons dialogButtons)
        {
            var viewModel = new MessageBoxViewModel(ApplicationInfo.ProductTitle, message, dialogButtons);

            this._windowManager.ShowDialog(viewModel);

            return viewModel.DialogResult;
        }

        #endregion
    }
}