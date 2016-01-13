// <copyright file="KillsViewModel.cs" company="VacuumBreather">
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

    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using Helpers;

    #endregion

    /// <summary>
    ///     Statistics screen for a unit's kills.
    /// </summary>
    [Export(typeof(IUnitStatisticsScreen))]
    public sealed class KillsViewModel : UnitStatisticsScreenBase
    {
        #region Constants

        private const string ScreenName = "Kills";

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="UnitStatisticsScreenBase" /> class.
        /// </summary>
        public KillsViewModel()
            : base(1)
        {
            DisplayName = ScreenName;
        }

        #endregion

        #region Instance Properties

        /// <summary>
        ///     Gets the kills per scenario.
        /// </summary>
        /// <value>
        ///     The kills per scenario.
        /// </value>
        public IEnumerable<KeyValuePair<string, double>> KillsPerScenario
        {
            get
            {
                return StatisticsHelper.GetPerScenario(Unit, Statistic.Kills);
            }
        }

        /// <summary>
        ///     Gets the kills progression.
        /// </summary>
        /// <value>
        ///     The kills progression.
        /// </value>
        public IEnumerable<KeyValuePair<string, double>> KillsProgression
        {
            get
            {
                return StatisticsHelper.GetProgression(Unit, Statistic.Kills);
            }
        }

        #endregion
    }
}