// <copyright file="UnitStatisticsScreenBase.cs" company="VacuumBreather">
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

namespace DossierTool.ViewModel.UnitStatisticsScreens
{
    #region Using Directives

    using Caliburn.Micro;
    using Decorators;

    #endregion

    /// <summary>
    ///     Base class for all unit statistics screens.
    /// </summary>
    public abstract class UnitStatisticsScreenBase : Screen, IUnitStatisticsScreen
    {
        #region Readonly & Static Fields

        private readonly int _order;

        #endregion

        #region Fields

        private UnitDecorator _unit;
        private bool _needsRefresh = true;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="UnitStatisticsScreenBase" /> class.
        /// </summary>
        /// <param name="order">The order of the screen.</param>
        protected UnitStatisticsScreenBase(int order)
        {
            this._order = order;
        }

        #endregion

        #region Instance Methods

        /// <summary>
        ///     Called when the unit was changed.
        /// </summary>
        /// <param name="oldUnit">The old unit.</param>
        /// <param name="newUnit">The new unit.</param>
        protected virtual void OnUnitChanged(UnitDecorator oldUnit, UnitDecorator newUnit)
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

        #region IUnitStatisticsScreen Members

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
        ///     Gets or sets the underlying dossier.
        /// </summary>
        /// <value>
        ///     The underlying dossier.
        /// </value>
        public UnitDecorator Unit
        {
            get
            {
                return this._unit;
            }
            set
            {
                if (value == this._unit)
                {
                    return;
                }

                UnitDecorator oldUnit = this._unit;

                this._unit = value;
                OnUnitChanged(oldUnit, this._unit);
                Refresh();
            }
        }

        #endregion
    }
}