// <copyright file="AsyncResult.cs" company="VacuumBreather">
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
    using System.ComponentModel;
    using Caliburn.Micro;
    using Action = System.Action;

    #endregion

    /// <summary>
    ///     An IResult implementation of an asynchronous operation.
    /// </summary>
    public class AsyncResult : IResult
    {
        #region Readonly & Static Fields

        private readonly Action _work;
        private readonly Action _onSuccess;
        private readonly Action<Exception> _onFail;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AsyncResult" /> class.
        /// </summary>
        /// <param name="work">The work.</param>
        /// <param name="onSuccess">The success callback.</param>
        /// <param name="onFail">The error handler.</param>
        public AsyncResult(Action work, Action onSuccess = null, Action<Exception> onFail = null)
        {
            this._work = work;
            this._onSuccess = onSuccess;
            this._onFail = onFail;
        }

        #endregion

        #region IResult Members

        /// <summary>
        ///     Executes the work on the specified specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public void Execute(ActionExecutionContext context)
        {
            Exception error = null;
            var worker = new BackgroundWorker();

            worker.DoWork += (s, e) =>
                             {
                                 try
                                 {
                                     this._work();
                                 }
                                 catch (Exception ex)
                                 {
                                     error = ex;
                                 }
                             };

            worker.RunWorkerCompleted += (s, e) =>
                                         {
                                             if (error == null && this._onSuccess != null)
                                             {
                                                 this._onSuccess.OnUIThread();
                                             }

                                             if (error != null && this._onFail != null)
                                             {
                                                 Caliburn.Micro.Execute.OnUIThread(() => this._onFail(error));
                                             }

                                             Completed(this, new ResultCompletionEventArgs { Error = error });
                                         };
            worker.RunWorkerAsync();
        }

        /// <summary>
        ///     Occurs when thw work is completed.
        /// </summary>
        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };

        #endregion
    }

    /// <summary>
    ///     An IResult implementation of an asynchronous operation with a return value.
    /// </summary>
    public class AsyncResult<T> : IResult<T>
    {
        #region Readonly & Static Fields

        private readonly Func<T> _work;
        private readonly Action _onSuccess;
        private readonly Action<Exception> _onFail;

        #endregion

        #region Fields

        private T _result;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AsyncResult{T}" /> class.
        /// </summary>
        /// <param name="work">The work.</param>
        /// <param name="onSuccess">The success callback.</param>
        /// <param name="onFail">The error handler.</param>
        public AsyncResult(Func<T> work, Action onSuccess = null, Action<Exception> onFail = null)
        {
            this._work = work;
            this._onSuccess = onSuccess;
            this._onFail = onFail;
        }

        #endregion

        #region IResult<T> Members

        /// <summary>
        ///     Executes the work on the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public void Execute(ActionExecutionContext context)
        {
            Exception error = null;
            var worker = new BackgroundWorker();

            worker.DoWork += (s, e) =>
                             {
                                 try
                                 {
                                     e.Result = this._work();
                                 }
                                 catch (Exception ex)
                                 {
                                     error = ex;
                                 }
                             };

            worker.RunWorkerCompleted += (s, e) =>
                                         {
                                             if (error == null && this._onSuccess != null)
                                             {
                                                 this._onSuccess.OnUIThread();
                                             }

                                             if (error != null && this._onFail != null)
                                             {
                                                 Caliburn.Micro.Execute.OnUIThread(() => this._onFail(error));
                                             }

                                             this._result = (T)e.Result;

                                             Completed(this, new ResultCompletionEventArgs { Error = error });
                                         };
            worker.RunWorkerAsync();
        }

        /// <summary>
        ///     Occurs when the work is completed.
        /// </summary>
        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };

        /// <summary>
        ///     Gets the result of the operation.
        /// </summary>
        /// <value>
        ///     The result of the operation.
        /// </value>
        public T Result
        {
            get
            {
                return this._result;
            }
        }

        #endregion
    }
}