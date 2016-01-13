// <copyright file="IFileService.cs" company="VacuumBreather">
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
    using System.IO;

    #endregion

    /// <summary>
    ///     An interface for a service providing access to the file system.
    /// </summary>
    public interface IFileService
    {
        #region Instance Properties

        /// <summary>
        ///     Gets the filename of the last opened or saved file.
        /// </summary>
        /// <value>
        ///     The filename of the last opened or saved file.
        /// </value>
        String LastFile { get; }

        #endregion

        #region Instance Methods

        /// <summary>
        ///     Opens a file selected by the user, with read-only permission.
        /// </summary>
        /// <returns>A Stream that specifies the read-only file selected by the user or null if the user canceled the operation.</returns>
        Stream Open();

        /// <summary>
        ///     Opens the last manipulated file, with write permission.
        /// </summary>
        Stream Save();

        /// <summary>
        ///     Opens a file selected by the user, with write permission.
        /// </summary>
        Stream SaveAs();

        #endregion
    }
}