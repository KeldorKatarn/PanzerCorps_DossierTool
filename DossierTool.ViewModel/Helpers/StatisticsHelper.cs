// <copyright file="StatisticsHelper.cs" company="VacuumBreather">
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

namespace DossierTool.ViewModel.Helpers
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Decorators;
    using Model;

    #endregion

    /// <summary>
    ///     Helper class used to get several statistics.
    /// </summary>
    public static class StatisticsHelper
    {
        #region Readonly & Static Fields

        private static readonly Dictionary<Statistic, Func<Report, double>> ReportFunctions =
            new Dictionary<Statistic, Func<Report, double>>
            {
                { Statistic.Experience, report => report.Experience },
                { Statistic.Kills, report => report.Kills },
                { Statistic.Losses, report => report.Losses },
            };

        private static readonly Dictionary<Statistic, Func<UnitDecorator, double>> UnitFunctions =
            new Dictionary<Statistic, Func<UnitDecorator, double>>
            {
                {
                    Statistic.Experience,
                    unit => unit.CurrentExperience
                },
                { Statistic.Kills, unit => unit.CurrentKills },
                { Statistic.Losses, unit => unit.CurrentLosses },
            };

        #endregion

        #region Class Methods

        /// <summary>
        ///     Gets the average statistic per unit type.
        /// </summary>
        /// <param name="units">The units.</param>
        /// <param name="statistic">The statistic.</param>
        /// <returns></returns>
        /// <value>
        ///     The average statistic per unit type.
        /// </value>
        public static IEnumerable<KeyValuePair<string, double>> GetAveragePerUnitType(IEnumerable<UnitDecorator> units,
                                                                                      Statistic statistic)
        {
            if (statistic == Statistic.KillRatio)
            {
                List<UnitDecorator> unitsList = units.ToList();

                return GetKillRatio(GetAveragePerUnitType(unitsList, Statistic.Kills),
                                    GetAveragePerUnitType(unitsList, Statistic.Losses));
            }

            return
                units.OrderBy(unit => unit.Type.Value)
                     .GroupBy(unit => unit.Type.Value)
                     .Select(
                         grouping =>
                         new KeyValuePair<string, double>(grouping.First().Type.Key,
                                                          Math.Round(
                                                              grouping.Average(unit => UnitFunctions[statistic](unit)))))
                     .Reverse();
        }

        /// <summary>
        ///     Gets the kill ratio.
        /// </summary>
        /// <param name="kills">The kills.</param>
        /// <param name="losses">The losses.</param>
        /// <returns>The kill ratio.</returns>
        public static double GetKillRatio(double kills, double losses)
        {
            return Math.Round(kills / Math.Max(losses, 0.5), 1);
        }

        /// <summary>
        ///     Gets the statistic per scenario for the specified unit.
        /// </summary>
        /// <param name="unit">A unit.</param>
        /// <param name="statistic">The statistic to use.</param>
        /// <returns>
        ///     The statistic per scenario for the specified unit.
        /// </returns>
        public static IEnumerable<KeyValuePair<string, double>> GetPerScenario(Unit unit, Statistic statistic)
        {
            if (statistic == Statistic.KillRatio)
            {
                return GetKillRatio(GetPerScenario(unit, Statistic.Kills), GetPerScenario(unit, Statistic.Losses));
            }

            Func<Report, double> function = ReportFunctions[statistic];

            return
                unit.Reports.SelectByPrevious(
                    first => new KeyValuePair<string, double>(first.ScenarioName, function(first)),
                    (previous, current) =>
                    new KeyValuePair<string, double>(current.ScenarioName, (function(current) - function(previous))));
        }

        /// <summary>
        ///     Gets the statistic progression for the specified unit.
        /// </summary>
        /// <param name="unit">A unit.</param>
        /// <param name="statistic">The statistic to use.</param>
        /// <returns>
        ///     The statistic progression for the specified unit.
        /// </returns>
        public static IEnumerable<KeyValuePair<string, double>> GetProgression(Unit unit, Statistic statistic)
        {
            if (statistic == Statistic.KillRatio)
            {
                return GetKillRatio(GetProgression(unit, Statistic.Kills), GetProgression(unit, Statistic.Losses));
            }

            return
                unit.Reports.Select(
                    report => new KeyValuePair<string, double>(report.ScenarioName, ReportFunctions[statistic](report)));
        }

        /// <summary>
        ///     Gets the total statistic per scenario.
        /// </summary>
        /// <param name="units">The units.</param>
        /// <param name="scenarioReports">The scenario reports.</param>
        /// <param name="statistic">The statistic.</param>
        /// <returns>The total statistic per scenario.</returns>
        public static IEnumerable<KeyValuePair<string, double>> GetTotalPerScenario(IEnumerable<UnitDecorator> units,
                                                                                    IList<ScenarioReportDecorator>
                                                                                        scenarioReports,
                                                                                    Statistic statistic)
        {
            if (statistic == Statistic.KillRatio)
            {
                List<UnitDecorator> unitsList = units.ToList();

                return GetKillRatio(GetTotalPerScenario(unitsList, scenarioReports, Statistic.Kills),
                                    GetTotalPerScenario(unitsList, scenarioReports, Statistic.Losses));
            }

            return
                units.SelectMany(unit => GetPerScenario(unit, statistic))
                     .GroupBy(pair => pair.Key)
                     .OrderBy(
                         grouping =>
                         scenarioReports.IndexOf(
                             scenarioReports.FirstOrDefault(report => report.ScenarioName == grouping.First().Key)))
                     .Select(
                         grouping =>
                         new KeyValuePair<string, double>(grouping.First().Key, grouping.Sum(pair => pair.Value)));
        }

        /// <summary>
        ///     Gets the total statistic per unit type.
        /// </summary>
        /// <param name="units">The units.</param>
        /// <param name="statistic">The statistic.</param>
        /// <returns></returns>
        /// <value>
        ///     The total statistic per unit type.
        /// </value>
        public static IEnumerable<KeyValuePair<string, double>> GetTotalPerUnitType(IEnumerable<UnitDecorator> units,
                                                                                    Statistic statistic)
        {
            if (statistic == Statistic.KillRatio)
            {
                List<UnitDecorator> unitsList = units.ToList();

                return GetKillRatio(GetTotalPerUnitType(unitsList, Statistic.Kills),
                                    GetTotalPerUnitType(unitsList, Statistic.Losses));
            }

            return
                units.OrderBy(unit => unit.Type.Value)
                     .GroupBy(unit => unit.Type.Value)
                     .Select(
                         grouping =>
                         new KeyValuePair<string, double>(grouping.First().Type.Key,
                                                          grouping.Sum(unit => UnitFunctions[statistic](unit))))
                     .Reverse();
        }

        /// <summary>
        ///     Gets the total statistic progression per scenario.
        /// </summary>
        /// <param name="units">The units.</param>
        /// <param name="scenarioReports">The scenario reports.</param>
        /// <param name="statistic">The statistic.</param>
        /// <returns></returns>
        /// <value>
        ///     The total statistic progression per scenario.
        /// </value>
        public static IEnumerable<KeyValuePair<string, double>> GetTotalProgression(IEnumerable<UnitDecorator> units,
                                                                                    IList<ScenarioReportDecorator>
                                                                                        scenarioReports,
                                                                                    Statistic statistic)
        {
            if (statistic == Statistic.KillRatio)
            {
                List<UnitDecorator> unitsList = units.ToList();

                return GetKillRatio(GetTotalProgression(unitsList, scenarioReports, Statistic.Kills),
                                    GetTotalProgression(unitsList, scenarioReports, Statistic.Losses));
            }

            return
                scenarioReports.Select(
                    (scenarioReport, index) =>
                    new KeyValuePair<string, double>(scenarioReport.ScenarioName,
                                                     HierarchyHelper.GetUnitReportIndices(units, scenarioReports)
                                                                    .Select(
                                                                        reports =>
                                                                        reports.LastOrDefault(pair => pair.Key <= index)
                                                                               .Value)
                                                                    .Where(report => report != null)
                                                                    .Sum(report => ReportFunctions[statistic](report))));
        }

        private static IEnumerable<KeyValuePair<string, double>> GetKillRatio(
            IEnumerable<KeyValuePair<string, double>> kills,
            IEnumerable<KeyValuePair<string, double>> losses)
        {
            return kills.Zip(losses, (k, l) => new KeyValuePair<string, double>(k.Key, GetKillRatio(k.Value, l.Value)));
        }

        #endregion
    }
}