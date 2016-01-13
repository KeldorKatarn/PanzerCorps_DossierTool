// <copyright file="DialogResult.cs" company="VacuumBreather">
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
    /// <summary>
    ///     Respresents a dialog result.
    /// </summary>
    public enum DialogResult
    {
        /// <summary>
        ///     The dialog box return value is OK (usually sent from a button labeled OK).
        /// </summary>
        Ok,

        /// <summary>
        ///     The dialog box return value is Yes (usually sent from a button labeled Yes).
        /// </summary>
        Yes,

        /// <summary>
        ///     The dialog box return value is No (usually sent from a button labeled No).
        /// </summary>
        No,

        /// <summary>
        ///     The dialog box return value is Cancel (usually sent from a button labeled Cancel).
        /// </summary>
        Cancel
    }
}