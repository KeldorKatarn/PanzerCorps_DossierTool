// <copyright file="CoreViewModel.cs" company="VacuumBreather">
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
    using Decorators;
    using Helpers;
    using Model;
    using Model.Helpers;
    using Services;

    #endregion

    /// <summary>
    ///     IDossierScreen implementation for the core statistics page.
    /// </summary>
    [Export(typeof(IStatisticsScreen))]
    public sealed class CoreViewModel : StatisticsScreenBase
    {
        #region Constants

        private const string ScreenName = "Core";

        #endregion

        #region Readonly & Static Fields

        private readonly IEquipmentProvider _equipmentProvider;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="StatisticsScreenBase" /> class.
        /// </summary>
        /// <param name="equipmentProvider">The equipment provider.</param>
        [ImportingConstructor]
        public CoreViewModel(IEquipmentProvider equipmentProvider)
            : base(0)
        {
            this._equipmentProvider = equipmentProvider;
            DisplayName = ScreenName;
        }

        #endregion

        #region Instance Properties

        /// <summary>
        ///     Gets the core composition based on unit type.
        /// </summary>
        /// <value>
        ///     The core composition based on unit type.
        /// </value>
        public IEnumerable<KeyValuePair<string, int>> CoreCompositionByUnitType
        {
            get
            {
                return
                    CoreUnits.GroupBy(unit => unit.Type.Value)
                             .OrderBy(grouping => grouping.Key)
                             .Select(
                                 grouping =>
                                 new KeyValuePair<string, int>(grouping.Key.ToDisplayName(), grouping.Count()));
            }
        }

        /// <summary>
        ///     Gets the core motorisation.
        /// </summary>
        /// <value>
        ///     The core motorisation.
        /// </value>
        public IEnumerable<KeyValuePair<string, int>> CoreMotorization
        {
            get
            {
                return
                    CoreUnits.Where(unit => unit.Type.Value.IsGroundBased())
                             .GroupBy(unit => GetMotorization(unit, this._equipmentProvider))
                             .OrderBy(grouping => grouping.Key)
                             .Select(
                                 grouping =>
                                 new KeyValuePair<string, int>(grouping.Key.ToDisplayName(), grouping.Count()));
            }
        }

        /// <summary>
        ///     Gets the number of units in the core progression per scenario.
        /// </summary>
        /// <value>
        ///     The number of units in the core progression per scenario.
        /// </value>
        public IEnumerable<KeyValuePair<string, int>> NumUnitsInCoreProgression
        {
            get
            {
                return
                    ScenarioReports.Select(
                        (report, reportIndex) =>
                        new KeyValuePair<string, int>(report.ScenarioName,
                                                      UnitReportIndices.Count(
                                                          reports => reports.Any(pair => pair.Key <= reportIndex))));
            }
        }

        /// <summary>
        ///     Gets the total core prestige value progression per scenario.
        /// </summary>
        /// <value>
        ///     The total core prestige value progression per scenario.
        /// </value>
        public IEnumerable<KeyValuePair<string, int>> TotalCoreValueProgression
        {
            get
            {
                return
                    ScenarioReports.Select(
                        (scenarioReport, index) =>
                        new KeyValuePair<string, int>(scenarioReport.ScenarioName,
                                                      UnitReportIndices.Select(
                                                          reports =>
                                                          reports.LastOrDefault(pair => pair.Key <= index).Value)
                                                                       .Where(report => report != null)
                                                                       .Sum(
                                                                           report =>
                                                                           report.Equipment.Cost +
                                                                           report.LandTransport.Cost)));
            }
        }

        private IEnumerable<IEnumerable<KeyValuePair<int, ReportDecorator>>> UnitReportIndices
        {
            get
            {
                return HierarchyHelper.GetUnitReportIndices(CoreUnits, ScenarioReports);
            }
        }

        #endregion

        #region Class Methods

        private static Motorization GetMotorization(UnitDecorator unit, IEquipmentProvider equipmentProvider)
        {
            var motorization = Motorization.NotMotorized;
            var lastReport = (ReportDecorator)((Unit)unit).Reports.LastOrDefault();

            if (lastReport != null)
            {
                if (Equipment.GetAvailableLandTransports(lastReport, unit, equipmentProvider).Count() <= 1)
                {
                    motorization = Motorization.SelfPropelled;
                }
                else if (lastReport.LandTransport != Equipment.None)
                {
                    motorization = Motorization.Motorized;
                }
            }

            return motorization;
        }

        #endregion
    }
}