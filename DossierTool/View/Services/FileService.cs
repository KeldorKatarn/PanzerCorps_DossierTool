// <copyright file="FileService.cs" company="VacuumBreather">
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
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Windows.Threading;
    using Microsoft.Win32;
    using ViewModel.Services;

    #endregion

    /// <summary>
    ///     Standard file system implementation of the <see cref="IFileService" /> interface.
    /// </summary>
    [Export(typeof(IFileService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class FileService : IFileService
    {
        #region Readonly & Static Fields

        private readonly OpenFileDialog _openFileDialog = new OpenFileDialog();
        private readonly SaveFileDialog _saveFileDialog = new SaveFileDialog();
        private readonly Dispatcher _currentDispatcher;
        private readonly Func<bool?> _showOpenFileDialogFunc;
        private readonly Func<bool?> _showSaveFileDialogFunc;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="FileService" /> class.
        /// </summary>
        public FileService()
        {
            this._currentDispatcher = Dispatcher.CurrentDispatcher;
            this._showOpenFileDialogFunc = () => this._openFileDialog.ShowDialog();
            this._showSaveFileDialogFunc = () => this._saveFileDialog.ShowDialog();
        }

        #endregion

        #region IFileService Members

        /// <summary>
        ///     Opens a file selected by the user, with read-only permission.
        /// </summary>
        /// <returns>A Stream that specifies the read-only file selected by the user or null if the user canceled the operation.</returns>
        [SuppressMessage("Microsoft.Contracts", "Nonnull-11-0")]
        [SuppressMessage("Microsoft.Contracts", "Nonnull-66-0")]
        [SuppressMessage("Microsoft.Contracts", "Nonnull-76-0")]
        public Stream Open()
        {
            this._openFileDialog.FileName = "Dossier.pzcdossier";
            this._openFileDialog.DefaultExt = ".pzcdossier";
            this._openFileDialog.Filter = "Panzer Corps Dossiers (.pzcdossier)|*.pzcdossier";

            var result = this._currentDispatcher.Invoke(this._showOpenFileDialogFunc) as bool?;

            if (result != true)
            {
                return null;
            }

            LastFile = this._openFileDialog.FileName;

            return this._openFileDialog.OpenFile();
        }

        /// <summary>
        ///     Gets the filename of the last opened or saved file.
        /// </summary>
        /// <value>
        ///     The filename of the last opened or saved file.
        /// </value>
        public String LastFile { get; private set; }

        /// <summary>
        ///     Opens a file selected by the user, with write permission.
        /// </summary>
        [SuppressMessage("Microsoft.Contracts", "Nonnull-11-0")]
        [SuppressMessage("Microsoft.Contracts", "Nonnull-66-0")]
        [SuppressMessage("Microsoft.Contracts", "Nonnull-76-0")]
        public Stream SaveAs()
        {
            this._saveFileDialog.FileName = "Dossier";
            this._saveFileDialog.DefaultExt = ".pzcdossier";
            this._saveFileDialog.Filter = "Panzer Corps Dossiers (.pzcdossier)|*.pzcdossier";

            var result = this._currentDispatcher.Invoke(this._showSaveFileDialogFunc) as bool?;

            if (result != true)
            {
                return null;
            }

            LastFile = this._saveFileDialog.FileName;

            return this._saveFileDialog.OpenFile();
        }

        /// <summary>
        ///     Opens the last manipulated file, with write permission.
        /// </summary>
        public Stream Save()
        {
            Stream stream;

            try
            {
                stream = File.Create(LastFile);
            }
            catch (Exception)
            {
                stream = null;
            }

            return stream;
        }

        #endregion
    }
}