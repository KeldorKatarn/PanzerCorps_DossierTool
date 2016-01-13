// <copyright file="StatisticsScreenBase.cs" company="VacuumBreather">
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

namespace DossierTool.ViewModel.StatisticsScreens
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Linq;
    using Caliburn.Micro;
    using Decorators;
    using Helpers;

    #endregion

    /// <summary>
    ///     Base class for an IStatisticsScreen.
    /// </summary>
    public abstract class StatisticsScreenBase : Screen, IStatisticsScreen
    {
        #region Readonly & Static Fields

        private readonly int _order;

        #endregion

        #region Fields

        private DossierDecorator _dossier;
        private bool _needsRefresh = true;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="StatisticsScreenBase" /> class.
        /// </summary>
        /// <param name="order">The order of the screen.</param>
        protected StatisticsScreenBase(int order)
        {
            this._order = order;
        }

        #endregion

        #region Instance Properties

        /// <summary>
        ///     Gets the core units.
        /// </summary>
        /// <value>
        ///     The core units.
        /// </value>
        protected IEnumerable<UnitDecorator> CoreUnits
        {
            get
            {
                return
                    HierarchyHelper.GetUnitsAlongHierarchy(Dossier.RootUnit)
                                   .Cast<UnitDecorator>()
                                   .Where(unit => !unit.IsReserve);
            }
        }

        /// <summary>
        ///     Gets the scenario reports.
        /// </summary>
        /// <value>
        ///     The scenario reports.
        /// </value>
        protected IList<ScenarioReportDecorator> ScenarioReports
        {
            get
            {
                return Dossier.ScenarioReports.SourceCollection.Cast<ScenarioReportDecorator>().ToList();
            }
        }

        #endregion

        #region Instance Methods

        /// <summary>
        ///     Called when the dossier was changed.
        /// </summary>
        protected virtual void OnDossierChanged()
        {
        }

        /// <summary>
        ///     Called when the screen is activated.
        /// </summary>
        protected override void OnActivate()
        {
            base.OnActivate();

            if (this._needsRefresh)
            {
                Refresh();
                this._needsRefresh = false;
            }
        }

        #endregion

        #region IStatisticsScreen Members

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
                OnDossierChanged();
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
            if (!IsActive)
            {
                this._needsRefresh = true;
            }
        }

        #endregion
    }
}