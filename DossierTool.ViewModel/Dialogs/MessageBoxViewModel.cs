// <copyright file="MessageBoxViewModel.cs" company="VacuumBreather">
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

namespace DossierTool.ViewModel.Dialogs
{
    #region Using Directives

    using Caliburn.Micro;

    #endregion

    /// <summary>
    ///     ViewModel for a simple message box.
    /// </summary>
    public class MessageBoxViewModel : Screen, IDialog
    {
        #region Fields

        private DialogResult _dialogResult = DialogResult.Cancel;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initialisiert eine neue Instanz der <see cref="T:System.Object" />-Klasse.
        /// </summary>
        /// <param name="subject">The subject.</param>
        /// <param name="message">The message.</param>
        /// <param name="dialogButtons">The message box buttons.</param>
        public MessageBoxViewModel(string subject, string message, DialogButtons dialogButtons = DialogButtons.OK)
        {
            Subject = subject;
            Message = message;
            DialogButtons = dialogButtons;
        }

        #endregion

        #region Instance Properties

        /// <summary>
        ///     Gets the message box buttons.
        /// </summary>
        /// <value>
        ///     The message box buttons.
        /// </value>
        public DialogButtons DialogButtons { get; private set; }

        /// <summary>
        ///     Gets the message.
        /// </summary>
        /// <value>
        ///     The message.
        /// </value>
        public string Message { get; private set; }

        /// <summary>
        ///     Gets the subject.
        /// </summary>
        /// <value>
        ///     The subject.
        /// </value>
        public string Subject { get; private set; }

        #endregion

        #region Instance Methods

        /// <summary>
        ///     Called when the user selects 'Cancel'.
        /// </summary>
        public void OnCancel()
        {
            DialogResult = DialogResult.Cancel;

            TryClose(false);
        }

        /// <summary>
        ///     Called when the user selects 'No'.
        /// </summary>
        public void OnNo()
        {
            DialogResult = DialogResult.No;

            TryClose(false);
        }

        /// <summary>
        ///     Called when the user selects 'Ok'.
        /// </summary>
        public void OnOk()
        {
            DialogResult = DialogResult.Ok;

            TryClose(true);
        }

        /// <summary>
        ///     Called when the user selects 'Yes'.
        /// </summary>
        public void OnYes()
        {
            DialogResult = DialogResult.Yes;

            TryClose(true);
        }

        #endregion

        #region IDialog Members

        /// <summary>
        ///     Gets the dialog result.
        /// </summary>
        /// <value>
        ///     The dialog result.
        /// </value>
        public DialogResult DialogResult
        {
            get
            {
                return this._dialogResult;
            }
            private set
            {
                this._dialogResult = value;
            }
        }

        #endregion
    }
}