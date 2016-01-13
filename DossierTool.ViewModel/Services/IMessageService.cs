// <copyright file="IMessageService.cs" company="VacuumBreather">
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
    /// <summary>
    ///     This service shows messages to the user. It returns the answer when the message was a question.
    /// </summary>
    /// <remarks>
    ///     This interface is designed for simplicity. If you have to accomplish more advanced
    ///     scenarios then we recommend implementing your own specific message service.
    /// </remarks>
    public interface IMessageService
    {
        #region Instance Methods

        /// <summary>
        ///     Shows the message as error.
        /// </summary>
        /// <param name="message">The message.</param>
        void ShowError(string message);

        /// <summary>
        ///     Shows the message.
        /// </summary>
        /// <param name="message">The message.</param>
        void ShowMessage(string message);

        /// <summary>
        ///     Shows the specified question.
        /// </summary>
        /// <param name="message">The question.</param>
        /// <returns><c>true</c> for yes, <c>false</c> for no and <c>null</c> for cancel.</returns>
        bool? ShowQuestion(string message);

        /// <summary>
        ///     Shows the message as warning.
        /// </summary>
        /// <param name="message">The message.</param>
        void ShowWarning(string message);

        /// <summary>
        ///     Shows the specified yes/no question.
        /// </summary>
        /// <param name="message">The question.</param>
        /// <returns><c>true</c> for yes and <c>false</c> for no.</returns>
        bool ShowYesNoQuestion(string message);

        #endregion
    }
}