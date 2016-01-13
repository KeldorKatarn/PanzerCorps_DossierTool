// <copyright file="BindableSelectedItemBehavior.cs" company="VacuumBreather">
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

namespace DossierTool.View.Helpers
{
    #region Using Directives

    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Interactivity;
    using ViewModel.Decorators;

    #endregion

    /// <summary>
    ///     Helper class to make the SelectedItem property on a TreeView bindable.
    /// </summary>
    public class BindableSelectedItemBehavior : Behavior<TreeView>
    {
        #region Readonly & Static Fields

        /// <summary>
        ///     The SelectedItem property.
        /// </summary>
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem",
                                                                                                     typeof(object),
                                                                                                     typeof(
                                                                                                         BindableSelectedItemBehavior
                                                                                                         ),
                                                                                                     new UIPropertyMetadata
                                                                                                         (null,
                                                                                                          OnSelectedItemChanged));

        #endregion

        #region Instance Properties

        /// <summary>
        ///     Gets or sets the selected item.
        /// </summary>
        /// <value>The selected item.</value>
        public object SelectedItem
        {
            get
            {
                return GetValue(SelectedItemProperty);
            }
            set
            {
                SetValue(SelectedItemProperty, value);
            }
        }

        #endregion

        #region Class Methods

        private static void OnSelectedItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var oldValue = e.OldValue as IUnitDecorator;
            var newValue = e.NewValue as IUnitDecorator;

            if (oldValue != null)
            {
                oldValue.IsSelected = false;
            }

            if (newValue != null)
            {
                newValue.IsSelected = true;
            }
        }

        #endregion

        #region Instance Methods

        /// <summary>
        ///     Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>
        ///     Override this to hook up functionality to the AssociatedObject.
        /// </remarks>
        protected override void OnAttached()
        {
            base.OnAttached();

            if (AssociatedObject != null)
            {
                AssociatedObject.SelectedItemChanged += OnTreeViewSelectedItemChanged;
            }
        }

        /// <summary>
        ///     Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>
        ///     Override this to unhook functionality from the AssociatedObject.
        /// </remarks>
        protected override void OnDetaching()
        {
            base.OnDetaching();

            if (AssociatedObject != null)
            {
                AssociatedObject.SelectedItemChanged -= OnTreeViewSelectedItemChanged;
            }
        }

        /// <summary>
        ///     Called when [tree view selected item changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="object" /> instance containing the event data.</param>
        private void OnTreeViewSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            SelectedItem = e.NewValue;
        }

        #endregion
    }
}