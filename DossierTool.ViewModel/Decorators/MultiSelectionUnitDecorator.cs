// <copyright file="MultiSelectionUnitDecorator.cs" company="VacuumBreather">
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

namespace DossierTool.ViewModel.Decorators
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;
    using Caliburn.Micro;
    using Model;
    using Model.Helpers;

    #endregion

    /// <summary>
    ///     A decorator which wraps units to allow multiselection functionality.
    /// </summary>
    public class MultiSelectionUnitDecorator : IUnitDecorator
    {
        #region Readonly & Static Fields

        private readonly UnitBase _unit;
        private readonly List<IUnitDecorator> _subordinates;
        private static readonly UnitComparer UnitComparer = new UnitComparer();

        #endregion

        #region Fields

        private bool _isMarked = true;
        private bool _isSelected;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MultiSelectionUnitDecorator" /> class.
        /// </summary>
        /// <param name="unit">A <see cref="Unit" />.</param>
        public MultiSelectionUnitDecorator(UnitBase unit)
        {
            this._unit = unit;
            this._subordinates = new List<IUnitDecorator>();

            var higherUnit = Unit as HigherUnit;

            if (higherUnit != null)
            {
                foreach (var subordinate in higherUnit.Subordinates.OrderBy(subordinate => subordinate, UnitComparer))
                {
                    this._subordinates.Add(new MultiSelectionUnitDecorator(subordinate));
                }
            }
        }

        #endregion

        #region Instance Properties

        /// <summary>
        ///     Gets a value indicating whether this decorator decorates a <see cref="HigherUnit" />.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this decorator decorates a <see cref="HigherUnit" />; otherwise, <c>false</c>.
        /// </value>
        public bool DecoratesHigherUnit
        {
            get
            {
                return Unit is HigherUnit;
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is selected in a list or tree view.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is selected; otherwise, <c>false</c>.
        /// </value>
        public bool IsMarked
        {
            get
            {
                return this._isMarked;
            }
            set
            {
                if (value == this._isMarked)
                {
                    return;
                }

                this._isMarked = value;

                foreach (var subordinate in Subordinates)
                {
                    ((MultiSelectionUnitDecorator)subordinate).IsMarked = this._isMarked;
                }

                NotifyOfPropertyChange(() => IsMarked);
            }
        }

        /// <summary>
        ///     Gets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name
        {
            get
            {
                return this._unit.Name;
            }
        }

        /// <summary>
        ///     Gets the subordinates.
        /// </summary>
        /// <value>
        ///     The subordinates.
        /// </value>
        public IEnumerable<IUnitDecorator> Subordinates
        {
            get
            {
                return this._subordinates;
            }
        }

        /// <summary>
        ///     Gets the underlying unit.
        /// </summary>
        /// <value>
        ///     The the underlying unit.
        /// </value>
        public UnitBase Unit
        {
            get
            {
                return this._unit;
            }
        }

        #endregion

        #region Instance Methods

        /// <summary>
        ///     Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        ///     A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Unit.ToString();
        }

        private void NotifyOfPropertyChange(string propertyName)
        {
            Execute.OnUIThread(() => OnPropertyChanged(new PropertyChangedEventArgs(propertyName)));
        }

        private void NotifyOfPropertyChange<TProperty>(Expression<Func<TProperty>> property)
        {
            NotifyOfPropertyChange((property).GetMemberInfo().Name);
        }

        private void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion

        #region IUnitDecorator Members

        /// <summary>
        ///     Gets or sets a value indicating whether this unit is selected.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this unit is selected; otherwise, <c>false</c>.
        /// </value>
        public bool IsSelected
        {
            get
            {
                return this._isSelected;
            }
            set
            {
                if (value == this._isSelected)
                {
                    return;
                }

                this._isSelected = value;
                NotifyOfPropertyChange(() => IsSelected);
            }
        }

        /// <summary>
        ///     Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Gets or sets the superior.
        /// </summary>
        /// <value>The superior.</value>
        public HigherUnitDecorator Superior { get; set; }

        #endregion
    }
}