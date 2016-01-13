// <copyright file="DossierSerializer.cs" company="VacuumBreather">
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
    using System.Diagnostics.Contracts;
    using System.IO;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Xml;
    using Caliburn.Micro;
    using Model;

    #endregion

    /// <summary>
    ///     Helper class to serialize and deserialize a dossier.
    /// </summary>
    public static class DossierSerializer
    {
        #region Class Methods

        /// <summary>
        ///     Loads a dossier asynchronously from a stream.
        /// </summary>
        /// <param name="stream">The stream to load from.</param>
        /// <param name="onFail">The error handler.</param>
        /// <returns>
        ///     The loaded dossier.
        /// </returns>
        /// <exception cref="ArgumentNullException">When <paramref name="stream" /> is null.</exception>
        public static IResult<Dossier> LoadFromAsync(Stream stream, Action<Exception> onFail)
        {
            Contract.Requires<ArgumentNullException>(stream != null);

            return new AsyncResult<Dossier>(() =>
                                            {
                                                var dataContractSerializer = new DataContractSerializer(typeof(Dossier));

                                                Dossier dossier;

                                                using (
                                                    XmlDictionaryReader reader =
                                                        XmlDictionaryReader.CreateDictionaryReader(
                                                            new XmlTextReader(stream)))
                                                {
                                                    dossier =
                                                        (Dossier)
                                                        dataContractSerializer.ReadObject(reader,
                                                                                          false,
                                                                                          new DecoratedTypeResolver());
                                                }

                                                return dossier;
                                            },
                                            null,
                                            onFail);
        }

        /// <summary>
        ///     Saves a dossier asynchronously to a stream.
        /// </summary>
        /// <param name="dossier">The dossier to save.</param>
        /// <param name="stream">The stream to save to.</param>
        /// <param name="onFail">The error handler.</param>
        /// <exception cref="ArgumentNullException">When <paramref name="dossier" /> or <paramref name="stream" />is null.</exception>
        public static IResult SaveToAsync(Dossier dossier, Stream stream, Action<Exception> onFail)
        {
            Contract.Requires<ArgumentNullException>(dossier != null);
            Contract.Requires<ArgumentNullException>(stream != null);

            var serializer = new DataContractSerializer(typeof(Dossier));
            var xmlWriterSettings = new XmlWriterSettings
                                    {
                                        CheckCharacters = true,
                                        Encoding = new UTF8Encoding(),
                                        Indent = true
                                    };

            return new AsyncResult(() =>
                                   {
                                       using (
                                           XmlDictionaryWriter writer =
                                               XmlDictionaryWriter.CreateDictionaryWriter(XmlWriter.Create(stream,
                                                                                                           xmlWriterSettings))
                                           )
                                       {
                                           serializer.WriteObject(writer, dossier, new DecoratedTypeResolver());
                                       }
                                   },
                                   null,
                                   onFail);
        }

        #endregion

        #region Nested type: DecoratedTypeResolver

        /// <summary>
        ///     Helper resolver class to make sure dekorated types are serialized as their base type.
        /// </summary>
        private class DecoratedTypeResolver : DataContractResolver
        {
            #region Constants

            private const string DecoratorSuffix = "Decorator";

            #endregion

            #region Readonly & Static Fields

            private readonly Assembly _assembly;

            #endregion

            #region Constructors

            /// <summary>
            ///     Initializes a new instance of the <see cref="DecoratedTypeResolver" /> class.
            /// </summary>
            public DecoratedTypeResolver()
            {
                this._assembly = Assembly.GetAssembly(typeof(Dossier));
            }

            #endregion

            #region Instance Methods

            /// <summary>
            ///     Override this method to map the specified xsi:type name and namespace to a data contract type during
            ///     deserialization.
            /// </summary>
            /// <param name="typeName">The xsi:type name to map.</param>
            /// <param name="typeNamespace">The xsi:type namespace to map.</param>
            /// <param name="declaredType">The type declared in the data contract.</param>
            /// <param name="knownTypeResolver">The known type resolver.</param>
            /// <returns>
            ///     The type the xsi:type name and namespace is mapped to.
            /// </returns>
            public override Type ResolveName(string typeName,
                                             string typeNamespace,
                                             Type declaredType,
                                             DataContractResolver knownTypeResolver)
            {
                return knownTypeResolver.ResolveName(typeName, typeNamespace, declaredType, null);
            }

            /// <summary>
            ///     Override this method to map a data contract type to an xsi:type name and namespace during serialization.
            /// </summary>
            /// <param name="type">The type to map.</param>
            /// <param name="declaredType">The type declared in the data contract.</param>
            /// <param name="knownTypeResolver">The known type resolver.</param>
            /// <param name="typeName">The xsi:type name.</param>
            /// <param name="typeNamespace">The xsi:type namespace.</param>
            /// <returns>
            ///     true if mapping succeeded; otherwise, false.
            /// </returns>
            public override bool TryResolveType(Type type,
                                                Type declaredType,
                                                DataContractResolver knownTypeResolver,
                                                out XmlDictionaryString typeName,
                                                out XmlDictionaryString typeNamespace)
            {
                bool isTypeResolved = knownTypeResolver.TryResolveType(type,
                                                                       declaredType,
                                                                       knownTypeResolver,
                                                                       out typeName,
                                                                       out typeNamespace);

                if (!isTypeResolved)
                {
                    Type ensuredType = EnsureModelType(type);

                    isTypeResolved = (ensuredType == declaredType) ||
                                     knownTypeResolver.TryResolveType(ensuredType,
                                                                      declaredType,
                                                                      knownTypeResolver,
                                                                      out typeName,
                                                                      out typeNamespace);
                }

                return isTypeResolved;
            }

            private Type EnsureModelType(Type type)
            {
                Type ensuredType = type;

                if (type.Name.EndsWith(DecoratorSuffix))
                {
                    ensuredType =
                        this._assembly.GetType(typeof(Dossier).Namespace + "." +
                                               type.Name.Replace(DecoratorSuffix, string.Empty));
                }

                return ensuredType;
            }

            #endregion
        }

        #endregion
    }
}