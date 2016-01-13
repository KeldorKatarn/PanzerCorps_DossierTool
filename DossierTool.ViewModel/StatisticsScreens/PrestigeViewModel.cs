// <copyright file="PrestigeViewModel.cs" company="VacuumBreather">
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
    using System.Linq;
    using Helpers;

    #endregion

    /// <summary>
    ///     IDossierScreen implementation for the prestige statistics page.
    /// </summary>
    [Export(typeof(IStatisticsScreen))]
    public sealed class PrestigeViewModel : StatisticsScreenBase
    {
        #region Constants

        private const string ScreenName = "Prestige";

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="StatisticsScreenBase" /> class.
        /// </summary>
        public PrestigeViewModel()
            : base(5)
        {
            DisplayName = ScreenName;
        }

        #endregion

        #region Instance Properties

        /// <summary>
        ///     Gets the prestige value per scenario.
        /// </summary>
        /// <value>
        ///     The prestige value per scenario.
        /// </value>
        public IEnumerable<KeyValuePair<string, int>> PrestigePerScenario
        {
            get
            {
                return
                    ScenarioReports.SelectByPrevious(
                        first => new KeyValuePair<string, int>(first.ScenarioName, first.Prestige),
                        (previous, current) =>
                        new KeyValuePair<string, int>(current.ScenarioName, (current.Prestige - previous.Prestige)));
            }
        }

        /// <summary>
        ///     Gets the prestige progression.
        /// </summary>
        /// <value>
        ///     The prestige progression.
        /// </value>
        public IEnumerable<KeyValuePair<string, int>> PrestigeProgression
        {
            get
            {
                return
                    ScenarioReports.Select(report => new KeyValuePair<string, int>(report.ScenarioName, report.Prestige));
            }
        }

        #endregion
    }
}