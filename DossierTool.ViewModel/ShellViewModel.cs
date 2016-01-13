// <copyright file="ShellViewModel.cs" company="VacuumBreather">
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

namespace DossierTool.ViewModel
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.IO;
    using Caliburn.Micro;
    using Decorators;
    using Dialogs;
    using Helpers;
    using Model;
    using Properties;
    using Services;

    #endregion

    /// <summary>
    ///     The viewModel of the main shell.
    /// </summary>
    [Export]
    public sealed class ShellViewModel : Conductor<IScreen>
    {
        #region Constants

        private const string DummyFileName = "Untitled";

        #endregion

        #region Readonly & Static Fields

        private readonly DossierViewModel _dossierViewModel;
        private readonly BusyWatcher _busyWatcher;
        private readonly IDecoratorService _decoratorService;
        private readonly IApplicationService _applicationService;
        private readonly IFileService _fileService;
        private readonly IDialogService _dialogService;
        private readonly IWindowManager _windowManager;

        #endregion

        #region Fields

        private DossierDecorator _currentDossier;
        private string _currentFileName;
        private bool _isBusy;
        private bool _isModelDirty;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ShellViewModel" /> class.
        /// </summary>
        [ImportingConstructor]
        public ShellViewModel(DossierViewModel dossierViewModel,
                              BusyWatcher busyWatcher,
                              IApplicationService applicationService,
                              IDecoratorService decoratorService,
                              IFileService fileService,
                              IDialogService dialogService,
                              IWindowManager windowManager)
        {
            this._dossierViewModel = dossierViewModel;
            this._busyWatcher = busyWatcher;
            this._applicationService = applicationService;
            this._decoratorService = decoratorService;
            this._fileService = fileService;
            this._dialogService = dialogService;
            this._windowManager = windowManager;

            this._busyWatcher.BusyChanged += (sender, e) => { IsBusy = this._busyWatcher.IsBusy; };

            this._currentDossier = this._decoratorService.Decorate(new Dossier());
            this._dossierViewModel.Dossier = this._currentDossier;

            ActivateItem(this._dossierViewModel);
            this._dossierViewModel.ModelChanged += (sender, e) => IsModelDirty = true;

            UpdateDisplayName();
        }

        #endregion

        #region Instance Properties

        /// <summary>
        ///     Gets a value indicating whether a new dossier can be created.
        /// </summary>
        /// <value>
        ///     <c>true</c> if a new dossier can be created; otherwise, <c>false</c>.
        /// </value>
        public bool CanCreateNewDossierAsync
        {
            get
            {
                return !IsBusy;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether a dossier can be opened.
        /// </summary>
        /// <value>
        ///     <c>true</c> if a dossier can be opened; otherwise, <c>false</c>.
        /// </value>
        public bool CanOpenDossierAsync
        {
            get
            {
                return !IsBusy;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether the current dossier can be saved.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the current dossier can be saved; otherwise, <c>false</c>.
        /// </value>
        public bool CanSaveDossierAsAsync
        {
            get
            {
                return !IsBusy;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether the current dossier can be saved under the last filename.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the current dossier can be saved under the last filename; otherwise, <c>false</c>.
        /// </value>
        public bool CanSaveDossierAsync
        {
            get
            {
                return !IsBusy && !String.IsNullOrEmpty(CurrentFileName) && IsModelDirty;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether the AboutBox can be shown.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the AboutBox can be shown; otherwise, <c>false</c>.
        /// </value>
        public bool CanShowAboutBox
        {
            get
            {
                return !IsBusy;
            }
        }

        /// <summary>
        ///     Gets the name of the current file.
        /// </summary>
        /// <value>
        ///     The name of the current file.
        /// </value>
        public string CurrentFileName
        {
            get
            {
                return this._currentFileName;
            }
            private set
            {
                this._currentFileName = value;

                UpdateDisplayName();
                Refresh();
            }
        }

        /// <summary>
        ///     Gets a value indicating whether the application is busy with an async operation.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the application is busy with an async operation; otherwise, <c>false</c>.
        /// </value>
        public bool IsBusy
        {
            get
            {
                return this._isBusy;
            }
            private set
            {
                this._isBusy = value;
                NotifyOfPropertyChange(() => IsBusy);
                Refresh();
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the model has any unsaved changes.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the model has any unsaved changes; otherwise, <c>false</c>.
        /// </value>
        public bool IsModelDirty
        {
            get
            {
                return this._isModelDirty;
            }
            set
            {
                this._isModelDirty = value;
                UpdateDisplayName();
                Refresh();
            }
        }

        #endregion

        #region Instance Methods

        public override void CanClose(Action<bool> callback)
        {
            CloseDossierAsync(callback).ExecuteAsync();
        }

        /// <summary>
        ///     Creates a new dossier.
        /// </summary>
        public IEnumerable<IResult> CreateNewDossierAsync()
        {
            bool isCurrentDossierClosable = false;

            yield return CloseDossierAsync(canClose => isCurrentDossierClosable = canClose).AsSequentialResult();

            if (!isCurrentDossierClosable)
            {
                yield break;
            }

            this._currentDossier = this._decoratorService.Decorate(new Dossier());
            this._dossierViewModel.Dossier = this._currentDossier;

            CurrentFileName = null;
            IsModelDirty = false;
        }

        /// <summary>
        ///     Shuts the application down.
        /// </summary>
        public void Exit()
        {
            CanClose(canClose =>
                     {
                         if (canClose)
                         {
                             this._applicationService.Shutdown();
                         }
                     });
        }

        /// <summary>
        ///     Opens a dossier.
        /// </summary>
        public IEnumerable<IResult> OpenDossierAsync()
        {
            bool isCurrentDossierClosable = false;

            yield return CloseDossierAsync(canClose => isCurrentDossierClosable = canClose).AsSequentialResult();

            if (!isCurrentDossierClosable)
            {
                yield break;
            }

            using (Stream stream = this._fileService.Open())
            {
                if (stream == null)
                {
                    yield break;
                }

                using (this._busyWatcher.GetTicket())
                {
                    IResult<Dossier> dossierOp = DossierSerializer.LoadFromAsync(stream,
                                                                                 exception =>
                                                                                 ShowErrorDialog(exception,
                                                                                                 Resources.UnableToOpen));

                    yield return dossierOp;

                    var decoratedDossierOp =
                        new AsyncResult<DossierDecorator>(() => this._decoratorService.Decorate(dossierOp.Result));

                    yield return decoratedDossierOp;

                    this._currentDossier = decoratedDossierOp.Result;
                    this._dossierViewModel.Dossier = this._currentDossier;

                    CurrentFileName = this._fileService.LastFile;
                    IsModelDirty = false;
                }
            }
        }

        /// <summary>
        ///     Saves the dossier in a user specified file.
        /// </summary>
        public IEnumerable<IResult> SaveDossierAsAsync()
        {
            using (Stream stream = this._fileService.SaveAs())
            {
                if (stream == null)
                {
                    yield break;
                }

                using (this._busyWatcher.GetTicket())
                {
                    yield return
                        DossierSerializer.SaveToAsync(this._currentDossier,
                                                      stream,
                                                      exception => ShowErrorDialog(exception, Resources.UnableToSave));

                    CurrentFileName = this._fileService.LastFile;
                    IsModelDirty = false;
                }
            }
        }

        /// <summary>
        ///     Saves the dossier in the last specified file.
        /// </summary>
        public IEnumerable<IResult> SaveDossierAsync()
        {
            using (Stream stream = this._fileService.Save())
            {
                if (stream == null)
                {
                    ShowErrorDialog(new IOException(), Resources.UnableToSave);
                    yield break;
                }

                using (this._busyWatcher.GetTicket())
                {
                    yield return
                        DossierSerializer.SaveToAsync(this._currentDossier,
                                                      stream,
                                                      exception => ShowErrorDialog(exception, Resources.UnableToSave));

                    IsModelDirty = false;
                }
            }
        }

        /// <summary>
        ///     Shows the about box.
        /// </summary>
        public void ShowAboutBox()
        {
            this._windowManager.ShowDialog(new AboutViewModel());
        }

        private IEnumerable<IResult> CloseDossierAsync(Action<bool> canCloseCallback)
        {
            if (!IsModelDirty)
            {
                canCloseCallback(true);
                yield break;
            }

            DialogResult result =
                this._dialogService.ShowDialog(
                    string.Format(Resources.SaveChangesTo,
                                  String.IsNullOrEmpty(CurrentFileName)
                                      ? DummyFileName
                                      : new FileInfo(CurrentFileName).FullName),
                    DialogButtons.YesNoCancel);

            if (result == DialogResult.Cancel)
            {
                canCloseCallback(false);
            }
            else if (result == DialogResult.No)
            {
                canCloseCallback(true);
            }
            else
            {
                if (CanSaveDossierAsync)
                {
                    yield return SaveDossierAsync().AsSequentialResult();
                }
                else
                {
                    yield return SaveDossierAsAsync().AsSequentialResult();
                }

                canCloseCallback(!IsModelDirty);
            }
        }

        private void ShowErrorDialog(Exception exception, string message)
        {
            this._dialogService.ShowError(exception, message);
        }

        private void UpdateDisplayName()
        {
            string fileName = String.IsNullOrEmpty(CurrentFileName) ? DummyFileName : new FileInfo(CurrentFileName).Name;
            string dirtyMarker = IsModelDirty ? "*" : string.Empty;

            DisplayName = String.Format("{0}{1} - {2}", fileName, dirtyMarker, ApplicationInfo.ProductTitle);
        }

        #endregion
    }
}