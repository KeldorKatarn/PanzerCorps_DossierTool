// <copyright file="Bootstrapper.cs" company="VacuumBreather">
//      Copyright © 2015 VacuumBreather. All rights reserved.
// </copyright>
// <license type="X11/MIT">
//      Permission is hereby granted, free of charge, to any person obtaining a copy
//      of this software and associated documentation files (the "Software"), to deal
//      in the Software without restriction, including without limitation the rights
//      to use, copy, modify, merge, publish, distribute, sub-license, and/or sell
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

namespace DossierTool
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Windows;
    using System.Windows.Threading;
    using Caliburn.Micro;
    using ViewModel;
    using ViewModel.Helpers;
    using ViewModel.Services;

    #endregion

    /// <summary>
    ///     The bootstrapper of the application based on MEF.
    /// </summary>
    public class Bootstrapper : Bootstrapper<ShellViewModel>
    {
        #region Readonly & Static Fields

        private static readonly List<Exception> FatalExceptions = new List<Exception>();

        #endregion

        #region Fields

        private CompositionContainer _container;

        #endregion

        #region Class Methods

        private static void AddCustomNamingConventions()
        {
            const string groupName = "subname";
            string captureGroup = RegExHelper.GetNamespaceCaptureGroup("subname");

            string viewPattern = captureGroup + @"View";
            const string viewReplacement = @"${" + groupName + "}ViewModel";

            ViewModelLocator.NameTransformer.AddRule(viewPattern, viewReplacement);

            string viewModelPattern = captureGroup + @"ViewModel";
            const string viewModelReplacement = @"${" + groupName + "}View";

            ViewLocator.NameTransformer.AddRule(viewModelPattern, viewModelReplacement);
        }

        #endregion

        #region Instance Methods

        /// <summary>
        ///     Builds up the composed hierarchy.
        /// </summary>
        /// <param name="instance">The root instance.</param>
        protected override void BuildUp(object instance)
        {
            this._container.SatisfyImportsOnce(instance);
        }

        /// <summary>
        ///     Configures the bootstrapper.
        /// </summary>
        protected override void Configure()
        {
            AddCustomNamingConventions();

            this._container =
                new CompositionContainer(
                    new AggregateCatalog(AssemblySource.Instance.Select(assembly => new AssemblyCatalog(assembly))));

            var batch = new CompositionBatch();

            batch.AddExportedValue<IWindowManager>(new WindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());

            using (FileStream equipmentStream = File.OpenRead("Content/Data/equipment.pzeqp"))
            {
                var equipmentProvider = new EquipmentProvider(equipmentStream);
                batch.AddExportedValue<IEquipmentProvider>(equipmentProvider);
            }

            using (FileStream heroStream = File.OpenRead("Content/Data/heroes.xml"))
            {
                HeroProvider heroProvider = HeroProvider.LoadFrom(heroStream);
                batch.AddExportedValue<IHeroProvider>(heroProvider);
            }

            using (FileStream bonusStream = File.OpenRead("Content/Data/exp.pzdat"))
            {
                var bonusProvider = new BonusProvider(bonusStream);
                batch.AddExportedValue<IBonusProvider>(bonusProvider);
            }

            using (FileStream stringStream = File.OpenRead("Content/Data/strings.pzdat"))
            {
                var stringProvider = new StringProvider(stringStream);
                batch.AddExportedValue<IStringProvider>(stringProvider);

                using (FileStream awardStream = File.OpenRead("Content/Data/awards.pzdat"))
                {
                    var awardProvider = new AwardProvider(awardStream, stringProvider);
                    batch.AddExportedValue<IAwardProvider>(awardProvider);
                }
            }

            batch.AddExportedValue(this._container);

            this._container.Compose(batch);
        }

        /// <summary>
        ///     Gets all instances of the specified service type.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns>All instances of the specified service type.</returns>
        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return this._container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
        }

        /// <summary>
        ///     Gets the instance of the specified service type.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="key">The key.</param>
        /// <returns>The found instance.</returns>
        /// <exception cref="System.Exception">if not such instance could be found.</exception>
        protected override object GetInstance(Type serviceType, string key)
        {
            string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;

            IEnumerable<object> exports = this._container.GetExportedValues<object>(contract);

            object result = exports.FirstOrDefault();

            if (result != null)
            {
                return result;
            }

            throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
        }

        /// <summary>
        ///     Called when an unhandled exception occurs.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">
        ///     The <see cref="System.Windows.Threading.DispatcherUnhandledExceptionEventArgs" /> instance containing
        ///     the event data.
        /// </param>
        protected override void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            base.OnUnhandledException(sender, e);

            HandleFatalException(e.Exception);
        }

        /// <summary>
        ///     Prepares the application.
        /// </summary>
        protected override void PrepareApplication()
        {
            base.PrepareApplication();

            AppDomain.CurrentDomain.UnhandledException += OnAppDomainUnhandledException;
            Application.DispatcherUnhandledException += OnApplicationDispatcherUnhandledException;
        }

        /// <summary>
        ///     Selects the assemblies to search in.
        /// </summary>
        /// <returns>The assemblies to search in.</returns>
        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            var assemblies = new List<Assembly>();
            assemblies.AddRange(base.SelectAssemblies());

            string[] fileEntries = Directory.GetFiles(Directory.GetCurrentDirectory());

            assemblies.AddRange(
                fileEntries.Where(fileName => fileName.Contains("DossierTool."))
                           .Where(fileName => fileName.EndsWith(".dll"))
                           .Select(Assembly.LoadFile));

            return assemblies;
        }

        private void HandleFatalException(Exception exception)
        {
            if (exception == null)
            {
                return;
            }

            const string errorMessage =
                "An unexpected error occurred. Sorry for the inconvenience.\nDetailed information about the errors below has been copied to the clipboard. Please let us know about this problem.\n\n";

            FatalExceptions.Add(exception);

            var exceptionSummaryInDetail = new StringBuilder();
            var exceptionSummary = new StringBuilder();

            foreach (var fatalException in FatalExceptions)
            {
                exceptionSummaryInDetail.AppendLine(fatalException.ToString());
                exceptionSummary.AppendLine(fatalException.Message);

                Exception innerException = fatalException.InnerException;

                while (innerException != null)
                {
                    exceptionSummary.AppendLine(innerException.Message);
                    innerException = innerException.InnerException;
                }
            }

            Clipboard.SetText(exceptionSummaryInDetail.ToString());

            try
            {
                MessageBox.Show(errorMessage + exceptionSummary,
                                ApplicationInfo.ProductName,
                                MessageBoxButton.OK,
                                MessageBoxImage.Error,
                                MessageBoxResult.None,
                                (CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft)
                                    ? MessageBoxOptions.RtlReading
                                    : MessageBoxOptions.None);
            }
            finally
            {
                Environment.Exit(0);
            }
        }

        private void OnAppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleFatalException(e.ExceptionObject as Exception);
        }

        private void OnApplicationDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            HandleFatalException(e.Exception);
        }

        #endregion
    }
}