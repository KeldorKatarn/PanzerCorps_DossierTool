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

namespace DossierTool.ViewModel.DossierScreens
{
    #region Using Directives

    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using Decorators;
    using Helpers;
    using Model.Helpers;
    using Services;

    #endregion

    /// <summary>
    ///     IDossierScreen implementation for the core overview page of the dossier.
    /// </summary>
    [Export(typeof(IDossierScreen))]
    public sealed class CoreViewModel : DossierScreenBase
    {
        #region Constants

        private const string Name = "Core";

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DossierScreenBase" /> class.
        /// </summary>
        /// <param name="dialogService">The dialog service.</param>
        [ImportingConstructor]
        public CoreViewModel(IDialogService dialogService)
            : base(2, dialogService)
        {
            DisplayName = Name;
        }

        #endregion

        #region Instance Properties

        /// <summary>
        ///     Gets a list of all core (non-reserve) units in the hierarchy.
        /// </summary>
        /// <value>A list of all (non-reserve) units in the hierarchy.</value>
        public IEnumerable<UnitDecorator> CoreUnits
        {
            get
            {
                return
                    HierarchyHelper.GetUnitsAlongHierarchy(Dossier.RootUnit)
                                   .Cast<UnitDecorator>()
                                   .Where(unit => !unit.IsReserve);
            }
        }

        /// <summary>
        ///     Gets all units gouped by UnitType.
        /// </summary>
        /// <value>
        ///     All units gouped by UnitType.
        /// </value>
        public IEnumerable<KeyValuePair<string, IEnumerable<UnitDecorator>>> UnitsByType
        {
            get
            {
                IEnumerable<KeyValuePair<string, IEnumerable<UnitDecorator>>> unitsByType =
                    CoreUnits.Where(unit => unit.CurrentEquipment != Equipment.None)
                             .GroupBy(unit => unit.Type.Value)
                             .OrderBy(grouping => grouping.Key)
                             .Select(
                                 grouping =>
                                 new KeyValuePair<string, IEnumerable<UnitDecorator>>(grouping.Key.ToDisplayName(),
                                                                                      grouping));

                return unitsByType;
            }
        }

        #endregion
    }
}