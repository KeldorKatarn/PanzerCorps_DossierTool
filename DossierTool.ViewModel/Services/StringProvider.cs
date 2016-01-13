// <copyright file="StringProvider.cs" company="VacuumBreather">
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

namespace DossierTool.ViewModel.Services
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.IO;
    using CsvHelper;

    #endregion

    /// <summary>
    ///     Provides access to all Panzer Corps strings.
    /// </summary>
    public class StringProvider : IStringProvider
    {
        #region Readonly & Static Fields

        private readonly Dictionary<string, string> _strings = new Dictionary<string, string>();

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="StringProvider" /> class.
        /// </summary>
        /// <param name="stream">The string stream.</param>
        public StringProvider(Stream stream)
        {
            using (var csv = new CsvReader(new StreamReader(stream)))
            {
                csv.Configuration.AutoMap<StringData>();
                csv.Configuration.AllowComments = true;
                csv.Configuration.Delimiter = "\t";
                csv.Configuration.HasHeaderRecord = false;
                csv.Configuration.WillThrowOnMissingField = false;

                foreach (var stringData in csv.GetRecords<StringData>())
                {
                    this._strings.Add(stringData.ID, stringData.Text);
                }
            }
        }

        #endregion

        #region IStringProvider Members

        /// <summary>
        ///     Gets the swards.
        /// </summary>
        /// <value>
        ///     The awards.
        /// </value>
        public IDictionary<string, string> Strings
        {
            get
            {
                return this._strings;
            }
        }

        /// <summary>
        ///     Finds the string with the specified ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>
        ///     The string with the specified ID or an emtpy string if no such ID could be found.
        /// </returns>
        public string Find(string id)
        {
            string result;

            if (!this._strings.TryGetValue(id, out result))
            {
                result = String.Empty;
            }

            return result;
        }

        #endregion

        #region Nested type: StringData

        internal struct StringData
        {
            #region Instance Properties

            public string ID { get; set; }
            public string Text { get; set; }
            public string Comment { get; set; }

            #endregion
        }

        #endregion
    }
}