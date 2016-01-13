// <copyright file="EquipmentProvider.cs" company="VacuumBreather">
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
    using System.Diagnostics.Contracts;
    using System.IO;
    using System.Linq;
    using Decorators;
    using Model;
    using Model.Helpers;

    #endregion

    /// <summary>
    ///     Loads and provides access to all available unit equipments.
    /// </summary>
    public class EquipmentProvider : IEquipmentProvider
    {
        #region Readonly & Static Fields

        private readonly List<Equipment> _equipments = new List<Equipment>();

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="EquipmentProvider" /> class.
        /// </summary>
        /// <param name="stream">The stream containing the equipment data.</param>
        /// <exception cref="ArgumentException"><paramref name="stream" /> does not support reading.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="stream" /> is a null reference.</exception>
        /// <exception cref="IOException">An I/O error occurs.</exception>
        public EquipmentProvider(Stream stream)
        {
            using (var equipmentFile = new StreamReader(stream))
            {
                string line = equipmentFile.ReadLine();

                while (!String.IsNullOrEmpty(line))
                {
                    if (!line.StartsWith("#"))
                    {
                        this._equipments.Add(ParseLine(line));
                    }

                    line = equipmentFile.ReadLine();
                }
            }
        }

        #endregion

        #region Object Invariant

        [ContractInvariantMethod]
        private void Invariant()
        {
            Contract.Invariant(this._equipments != null);
        }

        #endregion

        #region Class Methods

        /// <summary>
        ///     Factory method to build an Equipment entry from a string definition.
        /// </summary>
        /// <param name="line">The line containing the string definition.</param>
        /// <returns>The equipment entry.</returns>
        public static Equipment ParseLine(string line)
        {
            Contract.Requires<ArgumentNullException>(line != null, "line must not be null.");

            string[] equipmentDetails = line.Split(new[]
                                                   {
                                                       '\t'
                                                   },
                                                   StringSplitOptions.None);

            var equipment = new Equipment(id: ParseInt(equipmentDetails, 0, 0),
                                          shortName: ParseString(equipmentDetails, 1, String.Empty),
                                          type: ParseUnitType(equipmentDetails, 2, 0),
                                          cost: ParseInt(equipmentDetails, 3, 0),
                                          maxAmmo: ParseInt(equipmentDetails, 4, 0),
                                          maxFuel: ParseInt(equipmentDetails, 5, 0),
                                          movement: ParseInt(equipmentDetails, 6, 0),
                                          spotting: ParseInt(equipmentDetails, 7, 0),
                                          range: ParseInt(equipmentDetails, 8, 0),
                                          initiative: ParseInt(equipmentDetails, 9, 0),
                                          softAttack: ParseInt(equipmentDetails, 10, 0),
                                          hardAttack: ParseInt(equipmentDetails, 11, 0),
                                          airAttack: ParseInt(equipmentDetails, 12, 0),
                                          navalAttack: ParseInt(equipmentDetails, 13, 0),
                                          groundDefense: ParseInt(equipmentDetails, 14, 0),
                                          airDefense: ParseInt(equipmentDetails, 15, 0),
                                          closeDefense: ParseInt(equipmentDetails, 16, 0),
                                          targetType: ParseInt(equipmentDetails, 17, 0),
                                          nationality: ParseNationality(equipmentDetails, 18, 0),
                                          icon: ParseString(equipmentDetails, 19, String.Empty),
                                          availableFrom: ParseDateTime(equipmentDetails, 20, default(DateTime)),
                                          availableTill: ParseDateTime(equipmentDetails, 21, default(DateTime)),
                                          typeOfMovement: ParseInt(equipmentDetails, 22, 0),
                                          rateOfFire: ParseInt(equipmentDetails, 23, 10),
                                          maxStrength: ParseInt(equipmentDetails, 24, 10),
                                          fullName: ParseString(equipmentDetails, 25, String.Empty),
                                          addTraits: ParseString(equipmentDetails, 26, String.Empty),
                                          removeTraits: ParseString(equipmentDetails, 27, String.Empty),
                                          series: ParseString(equipmentDetails, 28, String.Empty),
                                          multipurpose: ParseInt(equipmentDetails, 29, -1),
                                          theatre: ParseInt(equipmentDetails, 30, -1),
                                          usableTransports: ParseString(equipmentDetails, 31, String.Empty),
                                          transportCategory: ParseString(equipmentDetails, 32, String.Empty));

            return equipment;
        }

        private static DateTime ParseDateTime(string[] equipmentDetails, int index, DateTime defaultValue)
        {
            Contract.Requires<ArgumentOutOfRangeException>(index >= 0);
            Contract.Requires<ArgumentNullException>(equipmentDetails != null);

            DateTime parsedDateTime;

            if (index < equipmentDetails.Length)
            {
                if (!DateTime.TryParse(equipmentDetails[index], out parsedDateTime))
                {
                    parsedDateTime = defaultValue;
                }
            }
            else
            {
                parsedDateTime = defaultValue;
            }

            return parsedDateTime;
        }

        private static int ParseInt(string[] equipmentDetails, int index, int defaultValue)
        {
            Contract.Requires<ArgumentOutOfRangeException>(index >= 0);
            Contract.Requires<ArgumentNullException>(equipmentDetails != null);

            int parsedInt;

            if (index < equipmentDetails.Length)
            {
                if (!Int32.TryParse(equipmentDetails[index], out parsedInt))
                {
                    parsedInt = defaultValue;
                }
            }
            else
            {
                parsedInt = defaultValue;
            }

            return parsedInt;
        }

        private static Nationality ParseNationality(string[] equipmentDetails, int index, Nationality defaultValue)
        {
            Contract.Requires<ArgumentOutOfRangeException>(index >= 0);
            Contract.Requires<ArgumentNullException>(equipmentDetails != null);
            Contract.Requires<ArgumentOutOfRangeException>(defaultValue.IsValid());

            Nationality parsedNationality;

            if (index < equipmentDetails.Length)
            {
                int parsedInt;

                if (Int32.TryParse(equipmentDetails[index], out parsedInt))
                {
                    if (((Nationality)parsedInt).IsValid())
                    {
                        Contract.Assume(Enum.IsDefined(typeof(Nationality), (Nationality)parsedInt));
                        parsedNationality = (Nationality)parsedInt;
                    }
                    else
                    {
                        parsedNationality = defaultValue;
                    }
                }
                else
                {
                    parsedNationality = defaultValue;
                }
            }
            else
            {
                parsedNationality = defaultValue;
            }

            return parsedNationality;
        }

        private static string ParseString(string[] equipmentDetails, int index, string defaultValue)
        {
            Contract.Requires<ArgumentOutOfRangeException>(index >= 0);
            Contract.Requires<ArgumentNullException>(equipmentDetails != null);
            Contract.Requires<ArgumentNullException>(defaultValue != null);
            Contract.Ensures(Contract.Result<string>() != null);

            return (index < equipmentDetails.Length) ? (equipmentDetails[index] ?? defaultValue) : defaultValue;
        }

        private static UnitType ParseUnitType(string[] equipmentDetails, int index, UnitType defaultValue)
        {
            Contract.Requires<ArgumentOutOfRangeException>(index >= 0);
            Contract.Requires<ArgumentNullException>(equipmentDetails != null);
            Contract.Requires<ArgumentOutOfRangeException>(defaultValue.IsValid());

            UnitType parsedUnitType;

            if (index < equipmentDetails.Length)
            {
                int parsedInt;

                if (Int32.TryParse(equipmentDetails[index], out parsedInt))
                {
                    if (((UnitType)parsedInt).IsValid())
                    {
                        Contract.Assume(Enum.IsDefined(typeof(UnitType), (UnitType)parsedInt));
                        parsedUnitType = (UnitType)parsedInt;
                    }
                    else
                    {
                        parsedUnitType = defaultValue;
                    }
                }
                else
                {
                    parsedUnitType = defaultValue;
                }
            }
            else
            {
                parsedUnitType = defaultValue;
            }

            return parsedUnitType;
        }

        #endregion

        #region IEquipmentProvider Members

        /// <summary>
        ///     Gets all known equipments.
        /// </summary>
        /// <value>All known equipments.</value>
        public IEnumerable<Equipment> Equipments
        {
            get
            {
                return this._equipments.Where(e => e.ShortName != Equipment.Reserved);
            }
        }

        /// <summary>
        ///     Finds the <see cref="Equipment" /> with the specified short name.
        /// </summary>
        /// <param name="shortName">The short name of the <see cref="Equipment" />.</param>
        /// <param name="nationality">The nationality of the <see cref="Equipment" />.</param>
        /// <param name="type">The type.</param>
        /// <returns>
        ///     The <see cref="Equipment" /> with the specified short name and nationality or the default if no such equipment
        ///     could be found.
        /// </returns>
        public Equipment Find(string shortName, Nationality nationality, UnitType type)
        {
            List<Equipment> found =
                this._equipments.Where(equipment => equipment.ShortName == shortName)
                    .Where(equipment => equipment.Nationality == nationality)
                    .Where(equipment => equipment.Type == type)
                    .ToList();

            if (found.Count > 1)
            {
                found = found.Where(e => e.AddTraits.Contains("primary")).ToList();
            }

            return (found.Count == 1) ? found[0] : Equipment.None;
        }

        /// <summary>
        ///     Finds the <see cref="Equipment" /> with the specified ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>
        ///     The <see cref="Equipment" /> with the specified ID or the default if no such equipment could be found.
        /// </returns>
        public Equipment Find(int id)
        {
            List<Equipment> found = this._equipments.Where(equipment => equipment.ID == id).ToList();

            return (found.Count == 1) ? found[0] : Equipment.None;
        }

        #endregion
    }
}