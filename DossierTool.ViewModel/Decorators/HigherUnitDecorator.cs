// <copyright file="HigherUnitDecorator.cs" company="VacuumBreather">
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
    using System.Diagnostics.Contracts;
    using System.Linq.Expressions;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Windows.Data;
    using Caliburn.Micro;
    using Model;
    using Model.Helpers;
    using Services;
    using Action = System.Action;

    #endregion

    /// <summary>
    ///     The <see cref="IDecorator" /> for a <see cref="HigherUnit" />.
    /// </summary>
    [DataContract]
    public class HigherUnitDecorator : HigherUnit, IUnitDecorator
    {
        #region Readonly & Static Fields

        private readonly ListCollectionView _subordinatesView;
        private readonly IDecoratorService _decoratorService;
        private readonly Dictionary<string, string> _validationErrors = new Dictionary<string, string>();

        #endregion

        #region Fields

        private bool _isSelected;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="HigherUnitDecorator" /> class.
        /// </summary>
        /// <param name="higherUnit">The unit this decorater wraps.</param>
        /// <param name="decoratorService">The <see cref="IDecoratorService" />.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="higherUnit" /> or <paramref name="decoratorService" />is a null
        ///     reference.
        /// </exception>
        public HigherUnitDecorator(HigherUnit higherUnit, IDecoratorService decoratorService)
        {
            Contract.Requires<ArgumentNullException>(higherUnit != null);
            Contract.Requires<ArgumentNullException>(decoratorService != null);

            this._decoratorService = decoratorService;

            Name = higherUnit.Name;
            base.Superior = higherUnit.Superior;

            foreach (var subordinate in higherUnit.Subordinates)
            {
                base.AddSubordinate(this._decoratorService.Decorate(subordinate));
            }

            this._subordinatesView = CollectionViewSource.GetDefaultView(base.Subordinates) as ListCollectionView;

            Contract.Assert(this._subordinatesView != null);

            this._subordinatesView.CustomSort = new UnitComparer();
        }

        #endregion

        #region Object Invariant

        [ContractInvariantMethod]
        private void Invariant()
        {
            Contract.Invariant(this._subordinatesView != null);
            Contract.Invariant(this._decoratorService != null);
        }

        #endregion

        #region Instance Indexers

        /// <summary>
        ///     Gets the error message for the property with the given name.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>The error message.</returns>
        public string this[string columnName]
        {
            get
            {
                string errorMessage;

                this._validationErrors.TryGetValue(columnName, out errorMessage);

                return errorMessage ?? string.Empty;
            }
        }

        #endregion

        #region Instance Properties

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public override sealed string Name
        {
            get
            {
                return base.Name;
            }
            set
            {
                if (value == base.Name)
                {
                    return;
                }

                if (string.IsNullOrEmpty(value))
                {
                    SetPropertyValidationError(() => Name, "The name must not be empty.");
                }
                else if (!StringValidator.IsValidString(value))
                {
                    SetPropertyValidationError(() => Name, "The name contains invalid characters.");
                }
                else
                {
                    base.Name = value;
                    ResetPropertyValidationError(() => Name);
                }

                NotifyOfPropertyChange(() => Name);
            }
        }

        /// <summary>
        ///     Gets an error message indicating what is wrong with this object.
        /// </summary>
        /// <value>An error message indicating what is wrong with this object. The default is an empty string ("").</value>
        /// <returns>An error message indicating what is wrong with this object. The default is an empty string ("").</returns>
        public string Error
        {
            get
            {
                var stringBuilder = new StringBuilder();

                foreach (var error in this._validationErrors)
                {
                    stringBuilder.AppendLine(error.Value);
                }

                return stringBuilder.ToString();
            }
        }

        /// <summary>
        ///     Gets the subordinates.
        /// </summary>
        /// <value>The subordinates.</value>
        public new ICollectionView Subordinates
        {
            get
            {
                return this._subordinatesView;
            }
        }

        #endregion

        #region Instance Methods

        /// <summary>
        ///     Adds a subordinate to the list of subordinates.
        /// </summary>
        /// <param name="subordinate">The subordinate to add.</param>
        public override void AddSubordinate(UnitBase subordinate)
        {
            this._subordinatesView.AddNewItem(subordinate);
            this._subordinatesView.CommitNew();
            subordinate.Superior = this;
        }

        /// <summary>
        ///     Removes a subordinate from the list of subordinates.
        /// </summary>
        /// <param name="subordinate">The subordinate to remove.</param>
        public override void RemoveSubordinate(UnitBase subordinate)
        {
            this._subordinatesView.Remove(subordinate);
            subordinate.Superior = null;
        }

        /// <summary>
        ///     Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        ///     A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            Contract.Assume(Name != null);

            return Name;
        }

        /// <summary>
        ///     Adds a new higher unit.
        /// </summary>
        /// <returns>The new <see cref="HigherUnitDecorator" />.</returns>
        public IUnitDecorator AddNewHigherUnit()
        {
            HigherUnitDecorator higherUnit = this._decoratorService.Decorate(new HigherUnit());
            AddSubordinate(higherUnit);

            return higherUnit;
        }

        /// <summary>
        ///     Adds a new unit.
        /// </summary>
        /// <returns>The new <see cref="UnitDecorator" />.</returns>
        public IUnitDecorator AddNewUnit()
        {
            UnitDecorator unit = this._decoratorService.Decorate(new Unit());
            AddSubordinate(unit);

            return unit;
        }

        /// <summary>
        ///     Determines whether the specified property has any validation errors.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="property">The property expression.</param>
        /// <returns>
        ///     <c>true</c> if the specified property has any validation errors; otherwise, <c>false</c>.
        /// </returns>
        public bool HasPropertyValidationError<TProperty>(Expression<Func<TProperty>> property)
        {
            string propertyName = property.GetMemberInfo().Name;

            return !string.IsNullOrEmpty(this._validationErrors[propertyName]);
        }

        private void NotifyOfPropertyChange(string propertyName)
        {
            ((Action)(() => OnPropertyChanged(new PropertyChangedEventArgs(propertyName)))).OnUIThread();
        }

        private void NotifyOfPropertyChange<TProperty>(Expression<Func<TProperty>> property)
        {
            NotifyOfPropertyChange((property).GetMemberInfo().Name);
        }

        private void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler eventHandler = PropertyChanged;

            if (eventHandler == null)
            {
                return;
            }

            eventHandler(this, e);
        }

        private void ResetPropertyValidationError<TProperty>(Expression<Func<TProperty>> property)
        {
            string propertyName = property.GetMemberInfo().Name;

            this._validationErrors[propertyName] = null;
        }

        private void SetPropertyValidationError<TProperty>(Expression<Func<TProperty>> property, string errorMessage)
        {
            string propertyName = property.GetMemberInfo().Name;

            this._validationErrors[propertyName] = errorMessage;
        }

        #endregion

        #region IUnitDecorator Members

        /// <summary>
        ///     Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is selected in a list or tree view.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is selected; otherwise, <c>false</c>.
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
        ///     Gets or sets the superior.
        /// </summary>
        /// <value>The superior.</value>
        public new HigherUnitDecorator Superior
        {
            get
            {
                return (HigherUnitDecorator)base.Superior;
            }
            set
            {
                base.Superior = value;
            }
        }

        #endregion
    }
}