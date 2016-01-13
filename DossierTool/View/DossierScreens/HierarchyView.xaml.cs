// <copyright file="HierarchyView.xaml.cs" company="VacuumBreather">
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

namespace DossierTool.View.DossierScreens
{
    #region Using Directives

    using System;
    using System.Diagnostics.Contracts;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using Model;
    using ViewModel.Decorators;
    using ViewModel.DossierScreens;

    #endregion

    /// <summary>
    ///     Interaction logic for HierarchyView.xaml
    /// </summary>
    public partial class HierarchyView : UserControl
    {
        #region Fields

        private Point _startPoint;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="HierarchyView" /> class.
        /// </summary>
        public HierarchyView()
        {
            InitializeComponent();
        }

        #endregion

        #region Instance Properties

        private HierarchyViewModel ViewModel
        {
            get
            {
                return DataContext as HierarchyViewModel;
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

            if (!e.Data.GetDataPresent(typeof(HigherUnitDecorator)))
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void DropTreeDrop(object sender, DragEventArgs e)
        {
            Contract.Requires(e.Data != null);

            IUnitDecorator droppedUnitDecorator = null;

            if (e.Data.GetDataPresent(typeof(UnitDecorator)))
            {
                droppedUnitDecorator = e.Data.GetData(typeof(UnitDecorator)) as UnitDecorator;
            }
            else if (e.Data.GetDataPresent(typeof(HigherUnitDecorator)))
            {
                droppedUnitDecorator = e.Data.GetData(typeof(HigherUnitDecorator)) as HigherUnitDecorator;
            }

            if (droppedUnitDecorator != null)
            {
                var treeViewItem = FindAnchestor<TreeViewItem>((DependencyObject)e.OriginalSource);

                HigherUnitDecorator dropTarget = ViewModel.RootUnit;

                if (treeViewItem != null)
                {
                    dropTarget = treeViewItem.Header as HigherUnitDecorator;

                    if (dropTarget == null)
                    {
                        return;
                    }
                }

                Contract.Assume(dropTarget != null);

                if (droppedUnitDecorator == dropTarget)
                {
                    return;
                }

                ViewModel.MoveUnitInHierarchy(droppedUnitDecorator, dropTarget);
            }
        }

        private void OnItemSelected(object sender, RoutedEventArgs e)
        {
            var item = e.OriginalSource as TreeViewItem;

            if (item != null)
            {
                item.BringIntoView();
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

                    var unitDecoratorBase = treeView.SelectedItem as UnitBase;

                    if (unitDecoratorBase == null)
                    {
                        return;
                    }

                    var dragData = new DataObject(unitDecoratorBase);
                    DragDrop.DoDragDrop(treeViewItem, dragData, DragDropEffects.Move);
                }
            }
        }

        private void TreePreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this._startPoint = e.GetPosition(null);
        }

        #endregion
    }
}