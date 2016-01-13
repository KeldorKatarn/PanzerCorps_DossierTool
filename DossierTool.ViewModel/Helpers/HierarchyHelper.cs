// <copyright file="HierarchyHelper.cs" company="VacuumBreather">
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

    using System.Collections.Generic;
    using System.Linq;
    using Decorators;
    using Model;

    #endregion

    /// <summary>
    ///     A helper class to traverse a unit hierarchy.
    /// </summary>
    public static class HierarchyHelper
    {
        #region Class Methods

        /// <summary>
        ///     Gets the unit report indices, a helpful collection for statistics.
        /// </summary>
        /// <param name="units">The units.</param>
        /// <param name="scenarioReports">The scenario reports.</param>
        /// <returns>
        ///     The unit report indices collection.
        /// </returns>
        public static IEnumerable<IEnumerable<KeyValuePair<int, ReportDecorator>>> GetUnitReportIndices(
            IEnumerable<UnitDecorator> units,
            IList<ScenarioReportDecorator> scenarioReports)
        {
            return
                units.Select(
                    unit =>
                    unit.Reports.SourceCollection.Cast<ReportDecorator>()
                        .Select(
                            report =>
                            new KeyValuePair<int, ReportDecorator>(
                                scenarioReports.IndexOf(
                                    scenarioReports.Where(sr => sr.ScenarioName == report.ScenarioName).FirstOrDefault()),
                                report)));
        }

        /// <summary>
        ///     Gets all <see cref="Unit">Units</see> along the hierarchy or the current unit if it is a leaf in the hierarchy.
        /// </summary>
        /// <param name="unit">The <see cref="UnitBase" /> to start with.</param>
        /// <returns>All <see cref="Unit">Units</see> that were found along the hierarchy.</returns>
        public static IEnumerable<IUnitDecorator> GetUnitsAlongHierarchy(IUnitDecorator unit)
        {
            IEnumerable<IUnitDecorator> result;

            if (unit is UnitDecorator ||
                (unit is MultiSelectionUnitDecorator && !((MultiSelectionUnitDecorator)unit).DecoratesHigherUnit))
            {
                result = LinqExtensions.FromSingle(unit);
            }
            else if (unit is HigherUnitDecorator)
            {
                result =
                    ((HigherUnitDecorator)unit).Subordinates.Cast<IUnitDecorator>().SelectMany(GetUnitsAlongHierarchy);
            }
            else if (unit is MultiSelectionUnitDecorator)
            {
                result = ((MultiSelectionUnitDecorator)unit).Subordinates.SelectMany(GetUnitsAlongHierarchy);
            }
            else
            {
                result = Enumerable.Empty<UnitDecorator>();
            }

            return result;
        }

        #endregion
    }
}