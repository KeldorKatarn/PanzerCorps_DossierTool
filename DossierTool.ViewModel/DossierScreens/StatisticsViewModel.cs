﻿// <copyright file="StatisticsViewModel.cs" company="VacuumBreather">
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

namespace DossierTool.ViewModel.DossierScreens
{
    #region Using Directives

    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using Caliburn.Micro;
    using Decorators;
    using StatisticsScreens;

    #endregion

    /// <summary>
    ///     IDossierScreen implementation for the statistics page of the dossier.
    /// </summary>
    [Export(typeof(IDossierScreen))]
    public sealed class StatisticsViewModel : Conductor<IStatisticsScreen>.Collection.OneActive, IDossierScreen
    {
        #region Constants

        private const string ScreenName = "Statistics";

        #endregion

        #region Readonly & Static Fields

        private readonly int _order;

        #endregion

        #region Fields

        private DossierDecorator _dossier;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="StatisticsViewModel" /> class.
        /// </summary>
        /// <param name="statisticsScreens">The statistics screens.</param>
        [ImportingConstructor]
        public StatisticsViewModel([ImportMany] IEnumerable<IStatisticsScreen> statisticsScreens)
        {
            this._order = 5;
            DisplayName = ScreenName;

            Items.AddRange(statisticsScreens.OrderBy(screen => screen.Order));
            ActivateItem(Items.FirstOrDefault());
        }

        #endregion

        #region Instance Methods

        /// <summary>
        ///     Called when the screen is activated.
        /// </summary>
        protected override void OnActivate()
        {
            base.OnActivate();

            foreach (var screen in Items)
            {
                screen.RequestRefresh();
            }
        }

        #endregion

        #region IDossierScreen Members

        /// <summary>
        ///     Gets or sets the underlying dossier.
        /// </summary>
        /// <value>
        ///     The underlying dossier.
        /// </value>
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

        /// <summary>
        ///     Gets the order of this screen.
        /// </summary>
        /// <value>
        ///     The order of this screen.
        /// </value>
        public int Order
        {
            get
            {
                return this._order;
            }
        }

        /// <summary>
        ///     Requests the refreshing of the screen.
        /// </summary>
        public void RequestRefresh()
        {
            foreach (var screen in Items)
            {
                screen.RequestRefresh();
            }
        }

        /// <summary>
        ///     Gets the error message for the property with the given name.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns></returns>
        public string this[string columnName]
        {
            get
            {
                return string.Empty;
            }
        }

        /// <summary>
        ///     Gets an error message indicating what is wrong with this object.
        /// </summary>
        /// <returns>An error message indicating what is wrong with this object. The default is an empty string ("").</returns>
        public string Error
        {
            get
            {
                return string.Empty;
            }
        }

        #endregion
    }
}