// <copyright file="ApplicationInfo.cs" company="VacuumBreather">
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
    using System.IO;
    using System.Reflection;

    #endregion

    /// <summary>
    ///     This class provides information about the running application.
    /// </summary>
    public static class ApplicationInfo
    {
        #region Readonly & Static Fields

        private static string _productName;
        private static string _description;
        private static string _version;
        private static string _company;
        private static string _copyright;
        private static string _applicationPath;
        private static string _productTitle;

        #endregion

        #region Class Properties

        /// <summary>
        ///     Gets the path for the executable file that started the application, not including the executable name.
        /// </summary>
        public static string ApplicationPath
        {
            get
            {
                if (_applicationPath == null)
                {
                    Assembly entryAssembly = Assembly.GetEntryAssembly();

                    _applicationPath = (entryAssembly != null)
                                           ? Path.GetDirectoryName(entryAssembly.Location)
                                           : string.Empty;
                }

                return _applicationPath;
            }
        }

        /// <summary>
        ///     Gets the company of the application.
        /// </summary>
        public static string Company
        {
            get
            {
                if (_company == null)
                {
                    Assembly entryAssembly = Assembly.GetEntryAssembly();

                    if (entryAssembly != null)
                    {
                        var attribute =
                            ((AssemblyCompanyAttribute)
                             Attribute.GetCustomAttribute(entryAssembly, typeof(AssemblyCompanyAttribute)));

                        _company = (attribute != null) ? attribute.Company : string.Empty;
                    }
                    else
                    {
                        _company = string.Empty;
                    }
                }

                return _company;
            }
        }

        /// <summary>
        ///     Gets the copyright information of the application.
        /// </summary>
        public static string Copyright
        {
            get
            {
                if (_copyright == null)
                {
                    Assembly entryAssembly = Assembly.GetEntryAssembly();

                    if (entryAssembly != null)
                    {
                        var attribute =
                            (AssemblyCopyrightAttribute)
                            Attribute.GetCustomAttribute(entryAssembly, typeof(AssemblyCopyrightAttribute));

                        _copyright = attribute != null ? attribute.Copyright : string.Empty;
                    }
                    else
                    {
                        _copyright = string.Empty;
                    }
                }

                return _copyright;
            }
        }

        /// <summary>
        ///     Gets the descripton of the application.
        /// </summary>
        public static string Description
        {
            get
            {
                if (_description == null)
                {
                    Assembly entryAssembly = Assembly.GetEntryAssembly();

                    if (entryAssembly != null)
                    {
                        var attribute =
                            ((AssemblyDescriptionAttribute)
                             Attribute.GetCustomAttribute(entryAssembly, typeof(AssemblyDescriptionAttribute)));

                        _description = (attribute != null) ? attribute.Description : string.Empty;
                    }
                    else
                    {
                        _description = string.Empty;
                    }
                }

                return _description;
            }
        }

        /// <summary>
        ///     Gets the product name of the application.
        /// </summary>
        public static string ProductName
        {
            get
            {
                if (_productName == null)
                {
                    Assembly entryAssembly = Assembly.GetEntryAssembly();

                    if (entryAssembly != null)
                    {
                        var attribute =
                            ((AssemblyProductAttribute)
                             Attribute.GetCustomAttribute(entryAssembly, typeof(AssemblyProductAttribute)));

                        _productName = (attribute != null) ? attribute.Product : string.Empty;
                    }
                    else
                    {
                        _productName = string.Empty;
                    }
                }

                return _productName;
            }
        }

        /// <summary>
        ///     Gets the product title of the application.
        /// </summary>
        public static string ProductTitle
        {
            get
            {
                if (_productTitle == null)
                {
                    Assembly entryAssembly = Assembly.GetEntryAssembly();

                    if (entryAssembly != null)
                    {
                        var attribute =
                            ((AssemblyTitleAttribute)
                             Attribute.GetCustomAttribute(entryAssembly, typeof(AssemblyTitleAttribute)));

                        _productTitle = (attribute != null) ? attribute.Title : string.Empty;
                    }
                    else
                    {
                        _productTitle = string.Empty;
                    }
                }

                return _productTitle;
            }
        }

        /// <summary>
        ///     Gets the version number of the application.
        /// </summary>
        public static string Version
        {
            get
            {
                if (_version == null)
                {
                    Assembly entryAssembly = Assembly.GetEntryAssembly();

                    _version = entryAssembly != null ? entryAssembly.GetName().Version.ToString() : string.Empty;
                }

                return _version;
            }
        }

        #endregion
    }
}