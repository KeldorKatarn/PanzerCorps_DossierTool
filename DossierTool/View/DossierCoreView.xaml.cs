// <copyright file="DossierCoreView.xaml.cs" company="Andreas Schmitt">
//      Copyright © 2012 Andreas Schmitt. All rights reserved.
// </copyright>
// <license type="X11/MIT">
//      Permission is hereby granted, free of charge, to any person obtaining a copy
//      of this software and associated documentation files (the "Software"), to deal
//      in the Software without restriction, including without limitation the rights
//      to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//      copies of the Software, and to permit persons to whom the Software is
// 
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

namespace DossierTool.View
{
    #region Using Directives

    using System;
    using System.Diagnostics.Contracts;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using ViewModel;

    #endregion

    /// <summary>
    /// Interaction logic for DossierCoreView.xaml
    /// </summary>
    public partial class DossierCoreView : UserControl
    {
        #region Fields

        private Point _startPoint;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DossierCoreView" /> class.
        /// </summary>
        public DossierCoreView()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Instance Properties

        private DossierViewModel ViewModel
        {
            get
            {
                return this.DataContext as DossierViewModel;
            }
        }

        #endregion

        #region Class Methods

        private static T FindAnchestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }

                current = VisualTreeHelper.GetParent(current);
            } while (current != null);

            return null;
        }

        #endregion

        #region Instance Methods

        private void DropTreeDragEnter(object sender, DragEventArgs e)
        {
            Contract.Requires(e.Data != null);

            if (!e.Data.GetDataPresent(typeof(HigherUnitViewModel)))
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void DropTreeDrop(object sender, DragEventArgs e)
        {
            Contract.Requires(e.Data != null);

            UnitBaseViewModel droppedUnit = null;

            if (e.Data.GetDataPresent(typeof(UnitViewModel)))
            {
                droppedUnit = e.Data.GetData(typeof(UnitViewModel)) as UnitViewModel;
            }
            else if (e.Data.GetDataPresent(typeof(HigherUnitViewModel)))
            {
                droppedUnit = e.Data.GetData(typeof(HigherUnitViewModel)) as HigherUnitViewModel;
            }

            if (droppedUnit != null)
            {
                var treeViewItem = FindAnchestor<TreeViewItem>((DependencyObject)e.OriginalSource);

                HigherUnitViewModel dropTarget = this.ViewModel.RootUnit;

                if (treeViewItem != null)
                {
                    dropTarget = treeViewItem.Header as HigherUnitViewModel;

                    if (dropTarget == null)
                    {
                        return;
                    }
                }

                Contract.Assume(dropTarget != null);

                if (droppedUnit == dropTarget)
                {
                    return;
                }

                if (droppedUnit.Superior != null)
                {
                    droppedUnit.Superior.RemoveSubordinate(droppedUnit);
                }

                dropTarget.AddSubordinate(droppedUnit);
            }
        }

        private void TreeMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point mousePos = e.GetPosition(null);
                Vector diff = this._startPoint - mousePos;

                if (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    var treeView = sender as TreeView;
                    var treeViewItem = FindAnchestor<TreeViewItem>((DependencyObject)e.OriginalSource);

                    if (treeView == null || treeViewItem == null)
                    {
                        return;
                    }

                    var baseUnit = treeView.SelectedItem as UnitBaseViewModel;

                    if (baseUnit == null)
                    {
                        return;
                    }

                    var dragData = new DataObject(baseUnit);
                    DragDrop.DoDragDrop(treeViewItem, dragData, DragDropEffects.Move);
                }
            }
        }

        private void TreePreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this._startPoint = e.GetPosition(null);
        }

        private void TreeViewSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Contract.Requires(this.ViewModel != null);

            this.ViewModel.SelectedUnit = (UnitBaseViewModel)e.NewValue;
        }

        #endregion
    }
}