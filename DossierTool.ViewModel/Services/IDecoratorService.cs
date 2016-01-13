// <copyright file="IDecoratorService.cs" company="VacuumBreather">
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

namespace DossierTool.ViewModel.Services
{
    #region Using Directives

    using Decorators;
    using Model;

    #endregion

    public interface IDecoratorService
    {
        #region Instance Methods

        /// <summary>
        ///     Decorates the specified <see cref="Report" />.
        /// </summary>
        /// <param name="report">The <see cref="Report" />.</param>
        /// <param name="unit">The <see cref="Unit" /> the <see cref="Report" /> belongs to.</param>
        /// <returns>The decorated <see cref="Report" />.</returns>
        ReportDecorator Decorate(Report report, Unit unit);

        /// <summary>
        ///     Decorates the specified <see cref="ScenarioReport" />.
        /// </summary>
        /// <param name="report">The <see cref="ScenarioReport" />.</param>
        /// <returns>The decorated <see cref="ScenarioReport" />.</returns>
        ScenarioReportDecorator Decorate(ScenarioReport report);

        /// <summary>
        ///     Decorates the specified <see cref="UnitBase" />.
        /// </summary>
        /// <param name="unitBase">The <see cref="UnitBase" />.</param>
        /// <returns>The decorated <see cref="UnitBase" />.</returns>
        UnitBase Decorate(UnitBase unitBase);

        /// <summary>
        ///     Decorates the specified <see cref="HigherUnit" />.
        /// </summary>
        /// <param name="higherUnit">The <see cref="HigherUnit" />.</param>
        /// <returns>The decorated <see cref="HigherUnit" />.</returns>
        HigherUnitDecorator Decorate(HigherUnit higherUnit);

        /// <summary>
        ///     Decorates the specified <see cref="Unit" />.
        /// </summary>
        /// <param name="unit">The <see cref="Unit" />.</param>
        /// <returns>The decorated <see cref="Unit" />.</returns>
        UnitDecorator Decorate(Unit unit);

        /// <summary>
        ///     Decorates the specified <see cref="Dossier" />.
        /// </summary>
        /// <param name="dossier">The <see cref="Dossier" />.</param>
        /// <returns>The decorated <see cref="Dossier" />.</returns>
        DossierDecorator Decorate(Dossier dossier);

        #endregion
    }
}