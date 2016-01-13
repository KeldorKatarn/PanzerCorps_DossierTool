// <copyright file="AboutViewModel.cs" company="VacuumBreather">
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

    using Helpers;

    #endregion

    /// <summary>
    ///     ViewModel for the about box.
    /// </summary>
    public class AboutViewModel : IDialog
    {
        #region Instance Properties

        /// <summary>
        ///     Gets the product's company name.
        /// </summary>
        public string Company
        {
            get
            {
                return ApplicationInfo.Company;
            }
        }

        /// <summary>
        ///     Gets the copyright information for the product.
        /// </summary>
        public string Copyright
        {
            get
            {
                return ApplicationInfo.Copyright;
            }
        }

        /// <summary>
        ///     Gets the description about the application.
        /// </summary>
        public string Description
        {
            get
            {
                return ApplicationInfo.Description;
            }
        }

        /// <summary>
        ///     Gets the product's full name.
        /// </summary>
        public string Product
        {
            get
            {
                return ApplicationInfo.ProductName;
            }
        }

        /// <summary>
        ///     Gets the title property, which is display in the About dialogs window title.
        /// </summary>
        public string ProductTitle
        {
            get
            {
                return ApplicationInfo.ProductTitle;
            }
        }

        /// <summary>
        ///     Gets the application's version information to show.
        /// </summary>
        public string Version
        {
            get
            {
                return ApplicationInfo.Version;
            }
        }

        #endregion

        #region IDialog Members

        /// <summary>
        ///     Gets or sets the dialog result.
        /// </summary>
        /// <value>
        ///     The dialog result.
        /// </value>
        public DialogResult DialogResult
        {
            get
            {
                return DialogResult.Ok;
            }
        }

        #endregion
    }
}