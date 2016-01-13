// <copyright file="KillRatioViewModel.cs" company="VacuumBreather">
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
    using System.ComponentModel.Composition;
    using Helpers;

    #endregion

    /// <summary>
    ///     IDossierScreen implementation for the kill ratio statistics page.
    /// </summary>
    [Export(typeof(IStatisticsScreen))]
    public sealed class KillRatioViewModel : StatisticsScreenBase
    {
        #region Constants

        private const string ScreenName = "Kill Ratio";

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="StatisticsScreenBase" /> class.
        /// </summary>
        public KillRatioViewModel()
            : base(4)
        {
            DisplayName = ScreenName;
        }

        #endregion

        #region Instance Properties

        /// <summary>
        ///     Gets the average kill ratio values per unit type.
        /// </summary>
        /// <value>
        ///     The average kill ratio values per unit type.
        /// </value>
        public IEnumerable<KeyValuePair<string, double>> AverageKillRatioPerUnitType
        {
            get
            {
                return StatisticsHelper.GetAveragePerUnitType(CoreUnits, Statistic.KillRatio);
            }
        }

        /// <summary>
        ///     Gets the total kill ratio values per scenario.
        /// </summary>
        /// <value>
        ///     The total kill ratio values per scenario.
        /// </value>
        public IEnumerable<KeyValuePair<string, double>> TotalKillRatioPerScenario
        {
            get
            {
                return StatisticsHelper.GetTotalPerScenario(CoreUnits, ScenarioReports, Statistic.KillRatio);
            }
        }

        /// <summary>
        ///     Gets the total kill ratio values per unit type.
        /// </summary>
        /// <value>
        ///     The total kill ratio values per unit type.
        /// </value>
        public IEnumerable<KeyValuePair<string, double>> TotalKillRatioPerUnitType
        {
            get
            {
                return StatisticsHelper.GetTotalPerUnitType(CoreUnits, Statistic.KillRatio);
            }
        }

        /// <summary>
        ///     Gets the total kill ratio value progression per scenario.
        /// </summary>
        /// <value>
        ///     The total kill ratio value progression per scenario.
        /// </value>
        public IEnumerable<KeyValuePair<string, double>> TotalKillRatioProgression
        {
            get
            {
                return StatisticsHelper.GetTotalProgression(CoreUnits, ScenarioReports, Statistic.KillRatio);
            }
        }

        #endregion
    }
}