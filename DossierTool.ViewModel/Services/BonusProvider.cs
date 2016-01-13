// <copyright file="BonusProvider.cs" company="VacuumBreather">
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
    using System.Linq;
    using CsvHelper;
    using Helpers;
    using Model;

    #endregion

    /// <summary>
    ///     Provides access to all possible boni, both experience and hero based.
    /// </summary>
    public class BonusProvider : IBonusProvider
    {
        #region Readonly & Static Fields

        private readonly IDictionary<UnitType, Bonus> _boni = new Dictionary<UnitType, Bonus>();

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="BonusProvider" /> class.
        /// </summary>
        /// <param name="stream">The experience bonus stream.</param>
        public BonusProvider(Stream stream)
        {
            using (var csv = new CsvReader(new StreamReader(stream)))
            {
                csv.Configuration.AutoMap<BonusData>();
                csv.Configuration.AllowComments = true;
                csv.Configuration.Delimiter = "\t";
                csv.Configuration.HasHeaderRecord = false;
                csv.Configuration.WillThrowOnMissingField = false;
                csv.Configuration.SkipEmptyRecords = true;

                var records =
                    csv.GetRecords<BonusData>()
                       .Zip(Enum.GetValues(typeof(UnitType)).Cast<UnitType>(),
                            (data, type) => new { BonusData = data, Type = type })
                       .ToArray();

                foreach (var record in records)
                {
                    var bonus = new Bonus
                                {
                                    Initiative = record.BonusData.Initiative / 100.0,
                                    SoftAttack = record.BonusData.SoftAttack / 100.0,
                                    HardAttack = record.BonusData.HardAttack / 100.0,
                                    AirAttack = record.BonusData.AirAttack / 100.0,
                                    NavalAttack = record.BonusData.NavalAttack / 100.0,
                                    GroundDefense = record.BonusData.GroundDefense / 100.0,
                                    AirDefense = record.BonusData.AirDefense / 100.0,
                                    CloseDefense = record.BonusData.CloseDefense / 100.0,
                                    Range = 0.0,
                                    Movement = 0.0,
                                    Spotting = 0.0,
                                };

                    this._boni.Add(record.Type, bonus);
                }
            }
        }

        #endregion

        #region IBonusProvider Members

        /// <summary>
        ///     Gets the experience based boni.
        /// </summary>
        /// <value>
        ///     The experience based boni.
        /// </value>
        public IDictionary<UnitType, Bonus> ExperienceBoni
        {
            get
            {
                return this._boni;
            }
        }

        /// <summary>
        ///     Gets the hero bonus for the specified hero.
        /// </summary>
        /// <param name="hero">The hero.</param>
        /// <returns>The bonus for the specified hero.</returns>
        public Bonus GetHeroBonusFor(Hero hero)
        {
            return new Bonus
                   {
                       Initiative = hero.InitiativeBonus,
                       SoftAttack = hero.AttackBonus,
                       HardAttack = hero.AttackBonus,
                       AirAttack = hero.AttackBonus,
                       NavalAttack = hero.AttackBonus,
                       GroundDefense = hero.DefenseBonus,
                       AirDefense = hero.DefenseBonus,
                       CloseDefense = hero.DefenseBonus,
                       Range = hero.RangeBonus,
                       Movement = hero.MovementBonus,
                       Spotting = hero.SpottingBonus,
                   };
        }

        #endregion

        #region Nested type: BonusData

        internal struct BonusData
        {
            #region Instance Properties

            public string UnitClass { get; set; }
            public int Initiative { get; set; }
            public int SoftAttack { get; set; }
            public int HardAttack { get; set; }
            public int AirAttack { get; set; }
            public int NavalAttack { get; set; }
            public int GroundDefense { get; set; }
            public int AirDefense { get; set; }
            public int CloseDefense { get; set; }

            #endregion
        }

        #endregion
    }
}