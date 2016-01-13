// <copyright file="DataViewModel.cs" company="VacuumBreather">
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

namespace DossierTool.ViewModel.UnitScreens
{
    #region Using Directives

    using System.ComponentModel.Composition;
    using Decorators;
    using Helpers;
    using Services;

    #endregion

    /// <summary>
    ///     IUnitScreen implementation for the unit data page.
    /// </summary>
    [Export(typeof(IUnitScreen))]
    public sealed class DataViewModel : UnitScreenBase
    {
        #region Constants

        private const string Name = "Data";

        #endregion

        #region Readonly & Static Fields

        private readonly IBonusProvider _bonusProvider;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DataViewModel" /> class.
        /// </summary>
        /// <param name="dialogService">The dialog service.</param>
        /// <param name="bonusProvider">The bonus provider.</param>
        [ImportingConstructor]
        public DataViewModel(IDialogService dialogService, IBonusProvider bonusProvider)
            : base(1, dialogService)
        {
            this._bonusProvider = bonusProvider;
            DisplayName = Name;
        }

        #endregion

        #region Instance Properties

        /// <summary>
        ///     Gets a value indicating whether the unit has a land transport.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the unit has a land transport; otherwise, <c>false</c>.
        /// </value>
        public bool HasTransport
        {
            get
            {
                return Unit.CurrentLandTransport != Equipment.None;
            }
        }

        /// <summary>
        ///     Gets the land transport data.
        /// </summary>
        /// <value>
        ///     The land transport data.
        /// </value>
        public UnitData TransportData
        {
            get
            {
                return new UnitData(Unit, this._bonusProvider, true);
            }
        }

        /// <summary>
        ///     Gets the unit data.
        /// </summary>
        /// <value>
        ///     The unit data.
        /// </value>
        public UnitData UnitData
        {
            get
            {
                return new UnitData(Unit, this._bonusProvider);
            }
        }

        #endregion
    }
}