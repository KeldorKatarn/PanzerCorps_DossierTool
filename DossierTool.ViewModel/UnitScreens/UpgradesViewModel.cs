// <copyright file="UpgradesViewModel.cs" company="VacuumBreather">
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

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using Decorators;
    using Services;

    #endregion

    /// <summary>
    ///     IUnitScreen implementation for the unit upgrade path page.
    /// </summary>
    [Export(typeof(IUnitScreen))]
    public sealed class UpgradesViewModel : UnitScreenBase
    {
        #region Constants

        private const string ScreenName = "Upgrades";

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="UpgradesViewModel" /> class.
        /// </summary>
        /// <param name="dialogService">The dialog service.</param>
        [ImportingConstructor]
        public UpgradesViewModel(IDialogService dialogService)
            : base(3, dialogService)
        {
            DisplayName = ScreenName;
        }

        #endregion

        #region Instance Properties

        /// <summary>
        ///     Gets the equipment upgrade path.
        /// </summary>
        /// <value>The equipment upgrade path.</value>
        public IEnumerable<KeyValuePair<string, Equipment>> UpgradePath
        {
            get
            {
                return
                    Unit.Reports.SourceCollection.Cast<ReportDecorator>()
                        .GroupBy(r => r.Equipment)
                        .Select(g => new KeyValuePair<string, Equipment>(g.First().ScenarioName, g.Key));
            }
        }

        #endregion
    }
}