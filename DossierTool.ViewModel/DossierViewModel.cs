﻿// <copyright file="DossierViewModel.cs" company="VacuumBreather">
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
    using System.Linq;
    using Caliburn.Micro;
    using Decorators;
    using DossierScreens;
    using Model;
    using Services;

    #endregion

    /// <summary>
    ///     The view model class for the dossier.
    /// </summary>
    [Export]
    public sealed class DossierViewModel : Conductor<IDossierScreen>.Collection.OneActive, IReportModelChanges
    {
        #region Fields

        private DossierDecorator _dossier;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DossierViewModel" /> class.
        /// </summary>
        /// <param name="dossierScreens">The dossier screens.</param>
        /// <param name="decoratorService">The decorator service.</param>
        [ImportingConstructor]
        public DossierViewModel([ImportMany] IEnumerable<IDossierScreen> dossierScreens,
                                IDecoratorService decoratorService)
        {
            this._dossier = decoratorService.Decorate(new Dossier());

            Items.AddRange(dossierScreens.OrderBy(screen => screen.Order));
            ActivateItem(Items.FirstOrDefault());

            foreach (var dossierScreen in Items)
            {
                var reportingScreen = dossierScreen as IReportModelChanges;

                if (reportingScreen != null)
                {
                    reportingScreen.ModelChanged += OnReportingScreenModelChanged;
                }
            }
        }

        #endregion

        #region Instance Properties

        /// <summary>
        ///     Gets the underlying dossier.
        /// </summary>
        /// <value>The underlying dossier.</value>
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

                foreach (var dossierScreen in Items)
                {
                    dossierScreen.Dossier = this._dossier;
                }

                Refresh();
            }
        }

        #endregion

        #region Instance Methods

        private void OnModelChanged()
        {
            EventHandler handler = ModelChanged;

            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private void OnReportingScreenModelChanged(object sender, EventArgs e)
        {
            OnModelChanged();

            foreach (var screen in Items)
            {
                screen.RequestRefresh();
            }
        }

        #endregion

        #region IReportModelChanges Members

        /// <summary>
        ///     Occurs when any value on the model was changed.
        /// </summary>
        public event EventHandler ModelChanged;

        #endregion
    }
}