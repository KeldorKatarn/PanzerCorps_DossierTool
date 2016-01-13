// <copyright file="IDialogService.cs" company="VacuumBreather">
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

namespace DossierTool.ViewModel.Services
{
    #region Using Directives

    using System;
    using Dialogs;

    #endregion

    /// <summary>
    ///     Interface for a service allowing to show several types of dialogs.
    /// </summary>
    public interface IDialogService
    {
        #region Instance Methods

        /// <summary>
        ///     Shows a dialog.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="dialogButtons">The buttons to display.</param>
        /// <returns>
        ///     the dialog result.
        /// </returns>
        DialogResult ShowDialog(string message, DialogButtons dialogButtons);

        /// <summary>
        ///     Shows a dialog.
        /// </summary>
        /// <param name="viewModel">The dialog view model.</param>
        /// <returns>
        ///     the dialog result.
        /// </returns>
        DialogResult ShowDialog(IDialog viewModel);

        /// <summary>
        ///     Shows an error dialog.
        /// </summary>
        /// <param name="exception">The exception that reported the error.</param>
        /// <param name="message">The error message.</param>
        void ShowError(Exception exception, string message = null);

        /// <summary>
        ///     Shows a message box with an OK button.
        /// </summary>
        /// <param name="message">The message.</param>
        void ShowMessageBox(string message);

        #endregion
    }
}