// <copyright file="AwardProvider.cs" company="VacuumBreather">
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

    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using CsvHelper;
    using Model;

    #endregion

    /// <summary>
    ///     Provides access to all possible awards.
    /// </summary>
    public class AwardProvider : IAwardProvider
    {
        #region Constants

        private const string HeaderStart = "# Name";

        #endregion

        #region Readonly & Static Fields

        private readonly List<Award> _awards = new List<Award>();

        private readonly Dictionary<string, string> _legacySynonyms = new Dictionary<string, string>
                                                                      {
                                                                          {
                                                                              "IronCrossSecondClass",
                                                                              "AWARD_IRON_CROSS1"
                                                                          },
                                                                          {
                                                                              "IronCrossFirstClass",
                                                                              "AWARD_IRON_CROSS2"
                                                                          },
                                                                          {
                                                                              "KnightsCross",
                                                                              "AWARD_KNIGHTS_CROSS1"
                                                                          },
                                                                          {
                                                                              "OakLeaves",
                                                                              "AWARD_KNIGHTS_CROSS2"
                                                                          },
                                                                          {
                                                                              "Swords",
                                                                              "AWARD_KNIGHTS_CROSS3"
                                                                          },
                                                                          {
                                                                              "Diamonds",
                                                                              "AWARD_KNIGHTS_CROSS4"
                                                                          },
                                                                          {
                                                                              "GoldOakLeaves",
                                                                              "AWARD_KNIGHTS_CROSS5"
                                                                          },
                                                                          {
                                                                              "ValourMedalBronze",
                                                                              "ITALY_AWARD1"
                                                                          },
                                                                          {
                                                                              "ValourMedalSilver",
                                                                              "ITALY_AWARD2"
                                                                          },
                                                                          {
                                                                              "ValourMedalGold",
                                                                              "ITALY_AWARD3"
                                                                          },
                                                                          {
                                                                              "ValourCross",
                                                                              "ITALY_AWARD4"
                                                                          },
                                                                          {
                                                                              "EagleCross",
                                                                              "ITALY_AWARD5"
                                                                          },
                                                                      };

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AwardProvider" /> class.
        /// </summary>
        /// <param name="stream">The award stream.</param>
        /// <param name="stringProvider">The string provider.</param>
        public AwardProvider(Stream stream, IStringProvider stringProvider)
        {
            using (var csv = new CsvReader(SkipToFirstEntry(new StreamReader(stream))))
            {
                csv.Configuration.AutoMap<AwardData>();
                csv.Configuration.AllowComments = true;
                csv.Configuration.Delimiter = "\t";
                csv.Configuration.HasHeaderRecord = false;
                csv.Configuration.WillThrowOnMissingField = false;

                this._awards.Add(Award.None);
                this._awards.AddRange(
                    csv.GetRecords<AwardData>()
                       .Select(
                           awardData =>
                           new Award
                           {
                               DisplayName = stringProvider.Find(awardData.Name),
                               ID = awardData.Name.Substring(4),
                               ImageFile = awardData.Image,
                               Nationality = (Nationality)awardData.Nation
                           }));
            }
        }

        #endregion

        #region Class Methods

        private static StreamReader SkipToFirstEntry(StreamReader streamReader)
        {
            bool isLastHeadLine = false;

            while (!isLastHeadLine)
            {
                string line = streamReader.ReadLine();
                isLastHeadLine = ((line != null) && line.StartsWith(HeaderStart));
            }

            return streamReader;
        }

        #endregion

        #region Instance Methods

        private string GetSynonym(string s)
        {
            string result;

            if (!this._legacySynonyms.TryGetValue(s, out result))
            {
                result = string.Empty;
            }

            return result;
        }

        #endregion

        #region IAwardProvider Members

        /// <summary>
        ///     Gets the awards.
        /// </summary>
        /// <value>
        ///     The award.
        /// </value>
        public IEnumerable<Award> Awards
        {
            get
            {
                return this._awards;
            }
        }

        /// <summary>
        ///     Finds the <see cref="Award" /> with the specified ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>
        ///     The <see cref="Award" /> with the specified ID or the default if no such award could be found.
        /// </returns>
        public Award Find(string id)
        {
            List<Award> found = Awards.Where(award => award.ID == id || award.ID == GetSynonym(id)).ToList();

            return (found.Count == 1) ? found[0] : Award.None;
        }

        #endregion

        #region Nested type: AwardData

        internal struct AwardData
        {
            #region Instance Properties

            public string Name { get; set; }
            public string Image { get; set; }
            public int Nation { get; set; }
            public int UnitClass { get; set; }
            public int Kills { get; set; }
            public int Slot { get; set; }

            #endregion
        }

        #endregion
    }
}