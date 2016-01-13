// <copyright file="InputBindingTrigger.cs" company="VacuumBreather">
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

    using System;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Interactivity;

    #endregion

    /// <summary>
    ///     Helper class to allow for global keyboard shortcuts as action triggers.
    /// </summary>
    public class InputBindingTrigger : TriggerBase<FrameworkElement>, ICommand
    {
        #region Readonly & Static Fields

        /// <summary>
        ///     The input binding property
        /// </summary>
        public static readonly DependencyProperty InputBindingProperty = DependencyProperty.Register("InputBinding",
                                                                                                     typeof(InputBinding
                                                                                                         ),
                                                                                                     typeof(
                                                                                                         InputBindingTrigger
                                                                                                         ),
                                                                                                     new UIPropertyMetadata
                                                                                                         (null));

        #endregion

        #region Instance Properties

        /// <summary>
        ///     Gets or sets the input binding.
        /// </summary>
        /// <value>
        ///     The input binding.
        /// </value>
        public InputBinding InputBinding
        {
            get
            {
                return (InputBinding)GetValue(InputBindingProperty);
            }
            set
            {
                SetValue(InputBindingProperty, value);
            }
        }

        #endregion

        #region Instance Methods

        /// <summary>
        ///     Called after the trigger is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached()
        {
            if (InputBinding != null)
            {
                InputBinding.Command = this;

                AssociatedObject.Loaded += delegate
                                           {
                                               Window window = GetWindow(AssociatedObject);
                                               window.InputBindings.Add(InputBinding);
                                           };
            }

            base.OnAttached();
        }

        /// <summary>
        ///     Gets the parent window of the specified framework element.
        /// </summary>
        /// <param name="frameworkElement">A framework element.</param>
        /// <returns>The parent window</returns>
        private Window GetWindow(FrameworkElement frameworkElement)
        {
            if (frameworkElement is Window)
            {
                return frameworkElement as Window;
            }

            var parent = frameworkElement.Parent as FrameworkElement;

            Debug.Assert(parent != null);

            return GetWindow(parent);
        }

        #endregion

        #region ICommand Members

        /// <summary>
        ///     Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged = delegate { };

        /// <summary>
        ///     Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">
        ///     Data used by the command.  If the command does not require data to be passed, this object can
        ///     be set to null.
        /// </param>
        /// <returns>
        ///     true if this command can be executed; otherwise, false.
        /// </returns>
        public bool CanExecute(object parameter)
        {
            // The action is blocked by Caliburn at the invoke level.
            return true;
        }

        /// <summary>
        ///     Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">
        ///     Data used by the command.
        ///     If the command does not require data to be passed, this object can be set to null.
        /// </param>
        public void Execute(object parameter)
        {
            InvokeActions(parameter);
        }

        #endregion
    }
}