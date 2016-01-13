// <copyright file="DialogButtons.cs" company="VacuumBreather">
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

    using System;

    #endregion

    /// <summary>
    ///     Represents a set of buttons that may be displayed in a dialog.
    /// </summary>
    [Flags]
    public enum DialogButtons
    {
        /// <summary>
        ///     The message box contains an OK button.
        /// </summary>
        OK,

        /// <summary>
        ///     The message box contains OK and Cancel buttons.
        /// </summary>
        OKCancel,

        /// <summary>
        ///     The message box contains Yes and No buttons.
        /// </summary>
        YesNo,

        /// <summary>
        ///     The message box contains Yes, No, and Cancel buttons.
        /// </summary>
        YesNoCancel
    }
}