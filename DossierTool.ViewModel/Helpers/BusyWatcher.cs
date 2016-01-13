// <copyright file="BusyWatcher.cs" company="VacuumBreather">
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

namespace DossierTool.ViewModel.Helpers
{
    #region Using Directives

    using System;
    using System.ComponentModel.Composition;

    #endregion

    /// <summary>
    ///     A class used to handle the busy state of the UI.
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class BusyWatcher
    {
        #region Fields

        private bool _isBusy;
        private int _busyCounter;

        #endregion

        #region Instance Properties

        /// <summary>
        ///     Gets a value indicating whether this BusyWatcher indicates a busy state.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this BusyWatcher indicates a busy state; otherwise, <c>false</c>.
        /// </value>
        public bool IsBusy
        {
            get
            {
                return this._isBusy;
            }
            private set
            {
                if (this._isBusy == value)
                {
                    return;
                }

                this._isBusy = value;
                OnBusyChanged();
            }
        }

        private int BusyCounter
        {
            get
            {
                return this._busyCounter;
            }
            set
            {
                this._busyCounter = value;

                if (this._busyCounter <= 0)
                {
                    this._busyCounter = 0;
                    IsBusy = false;
                }
                else
                {
                    IsBusy = true;
                }
            }
        }

        #endregion

        #region Instance Methods

        /// <summary>
        ///     Gets a ticket.  The watcher is busy as long as at least one ticket is being held.
        ///     Dispose the ticket to return it.
        /// </summary>
        /// <returns>The BusyTicket.</returns>
        public BusyTicket GetTicket()
        {
            return new BusyTicket(this);
        }

        /// <summary>
        ///     Called when  the IsBusy property has changed.
        /// </summary>
        protected void OnBusyChanged()
        {
            EventHandler handler = BusyChanged;

            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        #endregion

        #region Event Declarations

        /// <summary>
        ///     Occurs when the IsBusy property has changed.
        /// </summary>
        public event EventHandler BusyChanged;

        #endregion

        #region Nested type: BusyTicket

        /// <summary>
        ///     Ticket for the busy watcher. The watcher is busy as long as at least one ticket is being held.
        /// </summary>
        public class BusyTicket : IDisposable
        {
            #region Readonly & Static Fields

            private readonly BusyWatcher _busyWatcher;

            #endregion

            #region Constructors

            /// <summary>
            ///     Initialisiert eine neue Instanz der <see cref="T:System.Object" />-Klasse.
            /// </summary>
            public BusyTicket(BusyWatcher busyWatcher)
            {
                this._busyWatcher = busyWatcher;
                ++this._busyWatcher.BusyCounter;
            }

            #endregion

            #region IDisposable Members

            /// <summary>
            ///     Performs application-defined tasks associated with freeing, releasing,
            ///     or resetting unmanaged resources.
            /// </summary>
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            /// <summary>
            ///     Gets or sets a value indicating whether this instance is disposed.
            /// </summary>
            /// <value>
            ///     <c>true</c> if this instance is disposed; otherwise, <c>false</c>.
            /// </value>
            public bool IsDisposed { get; private set; }

            /// <summary>
            ///     Releases unmanaged and - optionally - managed resources.
            /// </summary>
            /// <param name="disposing">
            ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
            /// </param>
            protected virtual void Dispose(bool disposing)
            {
                // Check to see if Dispose has already been called.
                if (IsDisposed)
                {
                    return;
                }

                --this._busyWatcher.BusyCounter;

                // Mark this instance as disposed.
                IsDisposed = true;
            }

            #endregion
        }

        #endregion
    }
}