// <copyright file="Equipment.cs" company="VacuumBreather">
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

namespace DossierTool.ViewModel.Decorators
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using Model;
    using Model.Helpers;
    using Services;

    #endregion

    /// <summary>
    ///     A Panzer Corps equipment file entry.
    /// </summary>
    public struct Equipment : IEquatable<Equipment>
    {
        #region Constants

        /// <summary>
        ///     Entry for usable heavy pull transports.
        /// </summary>
        public const string HeavyPullTransport = "heavypull";

        /// <summary>
        ///     Entry for usable land transports.
        /// </summary>
        public const string LandTransport = "land";

        /// <summary>
        ///     Short name of reserved entries.
        /// </summary>
        public const string Reserved = "Reserved";

        /// <summary>
        ///     Short name prefix of SE units.
        /// </summary>
        public const string SpecialUnitPrefix = "SE ";

        #endregion

        #region Readonly & Static Fields

        /// <summary>
        ///     When no equipment is used.
        /// </summary>
        public static Equipment None = new Equipment(0, Report.None);

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Equipment" /> struct.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="shortName">The short name.</param>
        /// <param name="type">The <see cref="UnitType" />.</param>
        /// <param name="cost">The cost.</param>
        /// <param name="maxAmmo">The max ammo.</param>
        /// <param name="maxFuel">The max fuel.</param>
        /// <param name="movement">The movement.</param>
        /// <param name="spotting">The spotting.</param>
        /// <param name="range">The range.</param>
        /// <param name="initiative">The initiative.</param>
        /// <param name="softAttack">The soft attack.</param>
        /// <param name="hardAttack">The hard attack.</param>
        /// <param name="airAttack">The air attack.</param>
        /// <param name="navalAttack">The naval attack.</param>
        /// <param name="groundDefense">The ground defense.</param>
        /// <param name="airDefense">The air defense.</param>
        /// <param name="closeDefense">The close defense.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="nationality">The <see cref="Model.Nationality" />.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="availableFrom">The available from.</param>
        /// <param name="availableTill">The icon.</param>
        /// <param name="typeOfMovement">The available from.</param>
        /// <param name="rateOfFire">The available till.</param>
        /// <param name="maxStrength">The type of movement.</param>
        /// <param name="fullName">The rate of fire.</param>
        /// <param name="addTraits">The max strength.</param>
        /// <param name="removeTraits">The full name.</param>
        /// <param name="series">The add traits.</param>
        /// <param name="multipurpose">The remove traits.</param>
        /// <param name="theatre">The series.</param>
        /// <param name="usableTransports">The multipurpose.</param>
        /// <param name="transportCategory">The theatre.</param>
        public Equipment(int id = 0,
                         string shortName = "",
                         UnitType type = UnitType.Infantry,
                         int cost = 0,
                         int maxAmmo = 0,
                         int maxFuel = 0,
                         int movement = 0,
                         int spotting = 0,
                         int range = 0,
                         int initiative = 0,
                         int softAttack = 0,
                         int hardAttack = 0,
                         int airAttack = 0,
                         int navalAttack = 0,
                         int groundDefense = 0,
                         int airDefense = 0,
                         int closeDefense = 0,
                         int targetType = 0,
                         Nationality nationality = Nationality.Germany,
                         string icon = "",
                         DateTime availableFrom = default(DateTime),
                         DateTime availableTill = default(DateTime),
                         int typeOfMovement = 0,
                         int rateOfFire = 0,
                         int maxStrength = 10,
                         string fullName = "",
                         string addTraits = "",
                         string removeTraits = "",
                         string series = "",
                         int multipurpose = -1,
                         int theatre = -1,
                         string usableTransports = "",
                         string transportCategory = "")
            : this()
        {
            Contract.Requires<ArgumentNullException>(shortName != null);
            Contract.Requires<ArgumentNullException>(type.IsValid());
            Contract.Requires<ArgumentNullException>(nationality.IsValid());
            Contract.Requires<ArgumentNullException>(icon != null);
            Contract.Requires<ArgumentNullException>(fullName != null);
            Contract.Requires<ArgumentNullException>(addTraits != null);
            Contract.Requires<ArgumentNullException>(removeTraits != null);
            Contract.Requires<ArgumentNullException>(series != null);
            Contract.Requires<ArgumentNullException>(usableTransports != null);
            Contract.Requires<ArgumentNullException>(transportCategory != null);

            ID = id;
            ShortName = shortName.Trim();
            Type = type;
            Cost = cost;
            MaxAmmo = maxAmmo;
            MaxFuel = maxFuel;
            Movement = movement;
            Spotting = spotting;
            Range = range;
            Initiative = initiative;
            SoftAttack = softAttack;
            HardAttack = hardAttack;
            AirAttack = airAttack;
            NavalAttack = navalAttack;
            GroundDefense = groundDefense;
            AirDefense = airDefense;
            CloseDefense = closeDefense;
            TargetType = targetType;
            Nationality = nationality;
            Icon = icon;
            AvailableFrom = availableFrom;
            AvailableTill = availableTill;
            TypeOfMovement = (TypeOfMovement)typeOfMovement;
            RateOfFire = rateOfFire;
            MaxStrength = maxStrength;
            FullName = fullName;
            AddTraits = addTraits;
            RemoveTraits = removeTraits;
            Series = series;
            Multipurpose = multipurpose;
            Theatre = theatre;
            UsableTransports = usableTransports;
            TransportCategory = transportCategory;
        }

        #endregion

        #region Object Invariant

        [ContractInvariantMethod]
        private void Invariant()
        {
            Contract.Invariant(ShortName != null);
            Contract.Invariant(Type.IsValid());
            Contract.Invariant(Nationality.IsValid());
            Contract.Invariant(Icon != null);
            Contract.Invariant(FullName != null);
            Contract.Invariant(AddTraits != null);
            Contract.Invariant(RemoveTraits != null);
            Contract.Invariant(Series != null);
            Contract.Invariant(UsableTransports != null);
            Contract.Invariant(TransportCategory != null);
        }

        #endregion

        #region Instance Properties

        /// <summary>
        ///     Gets or sets the added traits.
        /// </summary>
        /// <value>The added traits.</value>
        public string AddTraits { get; private set; }

        /// <summary>
        ///     Gets or sets the air attack.
        /// </summary>
        /// <value>The air attack.</value>
        public int AirAttack { get; private set; }

        /// <summary>
        ///     Gets or sets the air defense.
        /// </summary>
        /// <value>The air defense.</value>
        public int AirDefense { get; private set; }

        /// <summary>
        ///     Gets or sets the available from date.
        /// </summary>
        /// <value>The available from date.</value>
        public DateTime AvailableFrom { get; private set; }

        /// <summary>
        ///     Gets or sets the available till date.
        /// </summary>
        /// <value>The available till date.</value>
        public DateTime AvailableTill { get; private set; }

        /// <summary>
        ///     Gets or sets the close defense.
        /// </summary>
        /// <value>The close defense.</value>
        public int CloseDefense { get; private set; }

        /// <summary>
        ///     Gets or sets the cost.
        /// </summary>
        /// <value>The cost.</value>
        public int Cost { get; private set; }

        /// <summary>
        ///     Gets the display name.
        /// </summary>
        /// <value>
        ///     The display name.
        /// </value>
        public string DisplayName
        {
            get
            {
                return String.IsNullOrEmpty(FullName) ? ShortName : FullName;
            }
        }

        /// <summary>
        ///     Gets or sets the full name.
        /// </summary>
        /// <value>The full name.</value>
        public string FullName { get; private set; }

        /// <summary>
        ///     Gets or sets the ground defense.
        /// </summary>
        /// <value>The ground defense.</value>
        public int GroundDefense { get; private set; }

        /// <summary>
        ///     Gets or sets the hard attack.
        /// </summary>
        /// <value>The hard attack.</value>
        public int HardAttack { get; private set; }

        /// <summary>
        ///     Gets or sets the ID.
        /// </summary>
        /// <value>The ID.</value>
        public int ID { get; private set; }

        /// <summary>
        ///     Gets or sets the icon.
        /// </summary>
        /// <value>The icon.</value>
        public string Icon { get; private set; }

        /// <summary>
        ///     Gets or sets the initiative.
        /// </summary>
        /// <value>The initiative.</value>
        public int Initiative { get; private set; }

        /// <summary>
        ///     Gets or sets the max ammo.
        /// </summary>
        /// <value>The max ammo.</value>
        public int MaxAmmo { get; private set; }

        /// <summary>
        ///     Gets or sets the max fuel.
        /// </summary>
        /// <value>The max fuel.</value>
        public int MaxFuel { get; private set; }

        /// <summary>
        ///     Gets or sets the max strength.
        /// </summary>
        /// <value>The max strength.</value>
        public int MaxStrength { get; private set; }

        /// <summary>
        ///     Gets or sets the movement points.
        /// </summary>
        /// <value>The movement points.</value>
        public int Movement { get; private set; }

        /// <summary>
        ///     Gets or sets the multipurpose equipment ID.
        /// </summary>
        /// <value>The multipurpose equipment ID.</value>
        public int Multipurpose { get; private set; }

        /// <summary>
        ///     Gets or sets the nationality.
        /// </summary>
        /// <value>The nationality.</value>
        public Nationality Nationality { get; private set; }

        /// <summary>
        ///     Gets or sets the naval attack.
        /// </summary>
        /// <value>The naval attack.</value>
        public int NavalAttack { get; private set; }

        /// <summary>
        ///     Gets or sets the ballistic range.
        /// </summary>
        /// <value>The ballistic range.</value>
        public int Range { get; private set; }

        /// <summary>
        ///     Gets or sets the rate of fire.
        /// </summary>
        /// <value>The rate of fire.</value>
        public int RateOfFire { get; private set; }

        /// <summary>
        ///     Gets or sets the removed traits.
        /// </summary>
        /// <value>The removed traits.</value>
        public string RemoveTraits { get; private set; }

        /// <summary>
        ///     Gets or sets the upgrade relevant series.
        /// </summary>
        /// <value>The upgrade relevant series.</value>
        public string Series { get; private set; }

        /// <summary>
        ///     Gets or sets the short name.
        /// </summary>
        /// <value>The short name.</value>
        public string ShortName { get; private set; }

        /// <summary>
        ///     Gets or sets the soft attack.
        /// </summary>
        /// <value>The soft attack.</value>
        public int SoftAttack { get; private set; }

        /// <summary>
        ///     Gets or sets the spotting range.
        /// </summary>
        /// <value>The spotting range.</value>
        public int Spotting { get; private set; }

        /// <summary>
        ///     Gets or sets the type of the target.
        /// </summary>
        /// <value>The type of the target.</value>
        public int TargetType { get; private set; }

        /// <summary>
        ///     Gets or sets the theatre.
        /// </summary>
        /// <value>The theatre.</value>
        public int Theatre { get; private set; }

        /// <summary>
        ///     Gets or sets the transport category.
        /// </summary>
        /// <value>The transport category.</value>
        public string TransportCategory { get; private set; }

        /// <summary>
        ///     Gets or sets the unit type.
        /// </summary>
        /// <value>The unit type.</value>
        public UnitType Type { get; private set; }

        /// <summary>
        ///     Gets or sets the type of movement.
        /// </summary>
        /// <value>The type of movement.</value>
        public TypeOfMovement TypeOfMovement { get; private set; }

        /// <summary>
        ///     Gets or sets the usable transports.
        /// </summary>
        /// <value>The usable transports.</value>
        public string UsableTransports { get; private set; }

        #endregion

        #region Operators

        /// <summary>
        ///     Implements the operator ==.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(Equipment left, Equipment right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Implements the operator !=.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(Equipment left, Equipment right)
        {
            return !left.Equals(right);
        }

        #endregion

        #region Class Methods

        /// <summary>
        ///     Gets the available land transports.
        /// </summary>
        /// <param name="report">The report.</param>
        /// <param name="unit">The unit the report is for.</param>
        /// <param name="equipmentProvider">The equipment provider.</param>
        /// <returns>
        ///     The available land transports.
        /// </returns>
        public static IEnumerable<Equipment> GetAvailableLandTransports(ReportDecorator report,
                                                                        UnitDecorator unit,
                                                                        IEquipmentProvider equipmentProvider)
        {
            var landTransports = new List<Equipment>();

            if (report != null)
            {
                var unorderedTransports = new List<Equipment>();

                if (report.Equipment.UsableTransports.Contains(LandTransport))
                {
                    // Usable transports has the "land" entry
                    unorderedTransports.AddRange(
                        equipmentProvider.Equipments.Where(equipment => equipment.Nationality == unit.Nationality.Value)
                                         .Where(equipment => equipment.Type == UnitType.LandTransport)
                                         .Where(
                                             equipment =>
                                             equipment.ShortName.StartsWith(SpecialUnitPrefix) == unit.IsSpecial)
                                         .Where(equipment => String.IsNullOrEmpty(equipment.TransportCategory)));
                }

                if (String.IsNullOrEmpty(report.Equipment.UsableTransports))
                {
                    // Handle the default case without specified data in the usable transports column.
                    if (report.Equipment != None && SatisfiesDefaultLandTransportRules(report.Equipment))
                    {
                        // We do have valid equipment but no entry
                        unorderedTransports.AddRange(
                            equipmentProvider.Equipments.Where(
                                equipment => equipment.Nationality == unit.Nationality.Value)
                                             .Where(equipment => equipment.Type == UnitType.LandTransport)
                                             .Where(
                                                 equipment =>
                                                 equipment.ShortName.StartsWith(SpecialUnitPrefix) == unit.IsSpecial));
                    }
                }
                else
                {
                    // Usable transports has no "land" entry but may have "heavypull" or similar entries
                    unorderedTransports.AddRange(
                        equipmentProvider.Equipments.Where(equipment => equipment.Nationality == unit.Nationality.Value)
                                         .Where(equipment => equipment.Type == UnitType.LandTransport)
                                         .Where(
                                             equipment =>
                                             equipment.ShortName.StartsWith(SpecialUnitPrefix) == unit.IsSpecial)
                                         .Where(equipment => !String.IsNullOrEmpty(equipment.TransportCategory))
                                         .Where(
                                             equipment =>
                                             report.Equipment.UsableTransports.Contains(equipment.TransportCategory)));
                }

                landTransports.AddRange(unorderedTransports.OrderBy(equipment => equipment.AvailableFrom));
            }

            landTransports.Insert(0, None);

            return landTransports;
        }

        /// <summary>
        ///     Verifies if the specified equipment satisfieses the default land transport rules.
        ///     Any units with movement type leg, towed, alpine and bridge can use default land transports.
        /// </summary>
        /// <param name="equipment">The equipment.</param>
        /// <returns>
        ///     <c>true</c> if the specified equipment satisfieses the default land transport rules; <c>false</c> otherwise.
        /// </returns>
        public static bool SatisfiesDefaultLandTransportRules(Equipment equipment)
        {
            return equipment.TypeOfMovement == TypeOfMovement.Leg || equipment.TypeOfMovement == TypeOfMovement.Towed ||
                   equipment.TypeOfMovement == TypeOfMovement.Alpine ||
                   equipment.TypeOfMovement == TypeOfMovement.Bridge;
        }

        #endregion

        #region Instance Methods

        /// <summary>
        ///     Determines whether the specified object is equal to the current instance.
        /// </summary>
        /// <returns>
        ///     true if the specified object is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (obj.GetType() != typeof(Equipment))
            {
                return false;
            }
            return Equals((Equipment)obj);
        }

        /// <summary>
        ///     Serves as a hash function for this type.
        /// </summary>
        /// <returns>
        ///     A hash code for the current instance.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            unchecked
            {
                int result = ID.GetHashCode();
                result = (result * 397) ^ ShortName.GetHashCode();

                return result;
            }
        }

        /// <summary>
        ///     Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        ///     A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return ShortName;
        }

        #endregion

        #region IEquatable<Equipment> Members

        /// <summary>
        ///     Determines whether the current object is equal to a specified object of the same type.
        /// </summary>
        /// <returns>
        ///     true if the specified object is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="other">
        ///     The object to compare with the current object.
        /// </param>
        public bool Equals(Equipment other)
        {
            return other.ID == ID && Equals(other.ShortName, ShortName);
        }

        #endregion
    }
}