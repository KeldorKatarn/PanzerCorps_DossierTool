// <copyright file="ReportGenerationViewModel.cs" company="VacuumBreather">
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

namespace DossierTool.ViewModel.Dialogs
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Linq;
    using Caliburn.Micro;
    using Decorators;
    using Helpers;
    using Model;

    #endregion

    /// <summary>
    ///     View model for the "Generate Unit Reports" dialog.
    /// </summary>
    public sealed class ReportGenerationViewModel : Screen, IDialog
    {
        #region Constants

        private const string Title = "Generate Unit Reports";

        #endregion

        #region Readonly & Static Fields

        private readonly MultiSelectionUnitDecorator _root;
        private readonly List<UnitDecorator> _markedUnits = new List<UnitDecorator>();

        #endregion

        #region Fields

        private DialogResult _dialogResult = DialogResult.Cancel;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReportGenerationViewModel" /> class.
        /// </summary>
        /// <param name="root">The root unit.</param>
        public ReportGenerationViewModel(UnitBase root)
        {
            this._root = new MultiSelectionUnitDecorator(root);
            DisplayName = Title;
        }

        #endregion

        #region Instance Properties

        /// <summary>
        ///     Gets the marked units.
        /// </summary>
        /// <value>
        ///     The marked units.
        /// </value>
        public IEnumerable<UnitDecorator> MarkedUnits
        {
            get
            {
                return this._markedUnits;
            }
        }

        /// <summary>
        ///     Gets the root unit.
        /// </summary>
        /// <value>
        ///     The root unit.
        /// </value>
        public MultiSelectionUnitDecorator Root
        {
            get
            {
                return this._root;
            }
        }

        #endregion

        #region Instance Methods

        /// <summary>
        ///     Called when Cancel is selected.
        /// </summary>
        public void OnCancel()
        {
            this._dialogResult = DialogResult.Cancel;

            TryClose(false);
        }

        /// <summary>
        ///     Called when OK is selected.
        /// </summary>
        public void OnOk()
        {
            IEnumerable<UnitDecorator> units =
                HierarchyHelper.GetUnitsAlongHierarchy(this._root)
                               .Cast<MultiSelectionUnitDecorator>()
                               .Where(unit => unit.IsMarked)
                               .Select(unit => unit.Unit)
                               .Cast<UnitDecorator>();

            this._markedUnits.AddRange(units);
            this._dialogResult = DialogResult.Ok;

            TryClose(true);
        }

        #endregion

        #region IDialog Members

        /// <summary>
        ///     Gets the dialog result.
        /// </summary>
        /// <value>
        ///     The dialog result.
        /// </value>
        public DialogResult DialogResult
        {
            get
            {
                return this._dialogResult;
            }
        }

        #endregion
    }
}