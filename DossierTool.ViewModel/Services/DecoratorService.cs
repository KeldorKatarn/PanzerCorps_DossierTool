// <copyright file="DecoratorService.cs" company="VacuumBreather">
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

    using System;
    using System.ComponentModel.Composition;
    using Decorators;
    using Model;
    using Properties;

    #endregion

    /// <summary>
    ///     Decorator helper able to transform the model entities into their decorated equivalent.
    /// </summary>
    [Export(typeof(IDecoratorService))]
    public class DecoratorService : IDecoratorService
    {
        #region Readonly & Static Fields

        private readonly IEquipmentProvider _equipmentProvider;
        private readonly IAwardProvider _awardProvider;
        private readonly IHeroProvider _heroProvider;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DecoratorService" /> class.
        /// </summary>
        /// <param name="equipmentProvider">The equipment provider.</param>
        /// <param name="awardProvider">The award provider.</param>
        /// <param name="heroProvider">The hero provider.</param>
        [ImportingConstructor]
        public DecoratorService(IEquipmentProvider equipmentProvider,
                                IAwardProvider awardProvider,
                                IHeroProvider heroProvider)
        {
            this._equipmentProvider = equipmentProvider;
            this._awardProvider = awardProvider;
            this._heroProvider = heroProvider;
        }

        #endregion

        #region IDecoratorService Members

        /// <summary>
        ///     Decorates the specified <see cref="Report" />.
        /// </summary>
        /// <param name="report">The <see cref="Report" />.</param>
        /// <param name="unit">The <see cref="Unit" /> the <see cref="Report" /> belongs to.</param>
        /// <returns>The decorated <see cref="Report" />.</returns>
        public ReportDecorator Decorate(Report report, Unit unit)
        {
            return new ReportDecorator(report, unit, this._equipmentProvider, this._awardProvider, this._heroProvider);
        }

        /// <summary>
        ///     Decorates the specified <see cref="ScenarioReport" />.
        /// </summary>
        /// <param name="report">The <see cref="ScenarioReport" />.</param>
        /// <returns>The decorated <see cref="ScenarioReport" />.</returns>
        public ScenarioReportDecorator Decorate(ScenarioReport report)
        {
            return new ScenarioReportDecorator(report);
        }

        /// <summary>
        ///     Decorates the specified <see cref="UnitBase" />.
        /// </summary>
        /// <param name="unitBase">The <see cref="UnitBase" />.</param>
        /// <returns>The decorated <see cref="UnitBase" />.</returns>
        public UnitBase Decorate(UnitBase unitBase)
        {
            if (!(unitBase is HigherUnit || unitBase is Unit))
            {
                throw new ArgumentException(Resources.InvalidArgumentType, "unitBase");
            }

            return unitBase is HigherUnit ? Decorate((HigherUnit)unitBase) : (UnitBase)Decorate((Unit)unitBase);
        }

        /// <summary>
        ///     Decorates the specified <see cref="HigherUnit" />.
        /// </summary>
        /// <param name="higherUnit">The <see cref="HigherUnit" />.</param>
        /// <returns>The decorated <see cref="HigherUnit" />.</returns>
        public HigherUnitDecorator Decorate(HigherUnit higherUnit)
        {
            return (higherUnit != null) ? new HigherUnitDecorator(higherUnit, this) : null;
        }

        /// <summary>
        ///     Decorates the specified <see cref="Unit" />.
        /// </summary>
        /// <param name="unit">The <see cref="Unit" />.</param>
        /// <returns>The decorated <see cref="Unit" />.</returns>
        public UnitDecorator Decorate(Unit unit)
        {
            return new UnitDecorator(unit, this);
        }

        /// <summary>
        ///     Decorates the specified <see cref="Dossier" />.
        /// </summary>
        /// <param name="dossier">The <see cref="Dossier" />.</param>
        /// <returns>The decorated <see cref="Dossier" />.</returns>
        public DossierDecorator Decorate(Dossier dossier)
        {
            return new DossierDecorator(dossier, this);
        }

        #endregion
    }
}