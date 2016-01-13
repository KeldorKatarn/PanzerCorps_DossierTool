// <copyright file="EnumEx.cs" company="VacuumBreather">
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

namespace DossierTool.Model.Helpers
{
    #region Using Directives

    using System;
    using System.Diagnostics.Contracts;

    #endregion

    /// <summary>
    ///     Contains extention methods for the <see cref="UnitType" /> enumeration used for validation.
    /// </summary>
    public static class EnumEx
    {
        #region Class Methods

        /// <summary>
        ///     Determines whether this <see cref="UnitType" /> is available for the player's core units.
        /// </summary>
        /// <param name="unitType">A <see cref="UnitType" /> value.</param>
        /// <returns>
        ///     <c>true</c> if this <see cref="UnitType" /> is available for the player's core units; otherwise, <c>false</c>.
        /// </returns>
        [Pure]
        public static bool IsCore(this UnitType unitType)
        {
            switch (unitType)
            {
                case UnitType.Infantry:
                case UnitType.Tank:
                case UnitType.Recon:
                case UnitType.AntiTank:
                case UnitType.Artillery:
                case UnitType.AntiAircraft:
                case UnitType.Fighter:
                case UnitType.TacticalBomber:
                case UnitType.StrategicBomber:
                    return true;

                default:
                    return false;
            }
        }

        /// <summary>
        ///     Determines whether this <see cref="UnitType" /> is ground based.
        /// </summary>
        /// <param name="unitType">A <see cref="UnitType" /> value.</param>
        /// <returns>
        ///     <c>true</c> if this <see cref="UnitType" /> is ground based; otherwise, <c>false</c>.
        /// </returns>
        [Pure]
        public static bool IsGroundBased(this UnitType unitType)
        {
            switch (unitType)
            {
                case UnitType.Infantry:
                case UnitType.Tank:
                case UnitType.Recon:
                case UnitType.AntiTank:
                case UnitType.Artillery:
                case UnitType.AntiAircraft:
                    return true;

                default:
                    return false;
            }
        }

        /// <summary>
        ///     Determines whether this value is within the valid range of the <see cref="UnitType" /> enumeration.
        /// </summary>
        /// <param name="unitType">A <see cref="UnitType" /> value.</param>
        /// <returns>
        ///     <c>true</c> if this value is within the valid range of the <see cref="UnitType" /> enumeration; otherwise,
        ///     <c>false</c>.
        /// </returns>
        [Pure]
        public static bool IsValid(this UnitType unitType)
        {
            switch (unitType)
            {
                case UnitType.Infantry:
                case UnitType.Tank:
                case UnitType.Recon:
                case UnitType.AntiTank:
                case UnitType.Artillery:
                case UnitType.AntiAircraft:
                case UnitType.Structure:
                case UnitType.Fighter:
                case UnitType.TacticalBomber:
                case UnitType.StrategicBomber:
                case UnitType.Submarine:
                case UnitType.Destroyer:
                case UnitType.CapitalShip:
                case UnitType.Carrier:
                case UnitType.LandTransport:
                case UnitType.AirTransport:
                case UnitType.SeaTransport:
                case UnitType.Train:
                case UnitType.ArmoredTrain:
                case UnitType.RiverBoat:
                    return true;

                default:
                    return false;
            }
        }

        /// <summary>
        ///     Determines whether this value is within the valid range of the <see cref="Nationality" /> enumeration.
        /// </summary>
        /// <param name="nationality">A <see cref="Nationality" /> value.</param>
        /// <returns>
        ///     <c>true</c> if this value is within the valid range of the <see cref="Nationality" /> enumeration; otherwise,
        ///     <c>false</c>.
        /// </returns>
        [Pure]
        public static bool IsValid(this Nationality nationality)
        {
            switch (nationality)
            {
                case Nationality.Germany:
                case Nationality.GreatBritain:
                case Nationality.USA:
                case Nationality.USSR:
                case Nationality.France:
                case Nationality.Italy:
                case Nationality.Poland:
                case Nationality.Belgium:
                case Nationality.Netherlands:
                case Nationality.NationalisticSpain:
                case Nationality.RepublicanSpain:
                case Nationality.Slovakia:
                case Nationality.Luxemburg:
                case Nationality.Hungary:
                case Nationality.Romania:
                case Nationality.Bulgaria:
                case Nationality.Yugoslavia:
                case Nationality.Greece:
                case Nationality.Canada:
                case Nationality.NewZealand:
                case Nationality.Australia:
                case Nationality.BlueTeam:
                case Nationality.RedTeam:
                case Nationality.Norway:
                case Nationality.Sweden:
                case Nationality.FreePoland:
                case Nationality.FreeFrance:
                case Nationality.Switzerland:
                case Nationality.Turkey:
                case Nationality.SouthAfrica:
                case Nationality.BritishIndia:
                case Nationality.VichyFrance:
                    return true;

                default:
                    return false;
            }
        }

        /// <summary>
        ///     Determines whether this value is within the valid range of the <see cref="ScenarioOutcome" /> enumeration.
        /// </summary>
        /// <param name="scenarioOutcome">A <see cref="ScenarioOutcome" /> value.</param>
        /// <returns>
        ///     <c>true</c> if this value is within the valid range of the <see cref="ScenarioOutcome" /> enumeration; otherwise,
        ///     <c>false</c>.
        /// </returns>
        [Pure]
        public static bool IsValid(this ScenarioOutcome scenarioOutcome)
        {
            switch (scenarioOutcome)
            {
                case ScenarioOutcome.Pending:
                case ScenarioOutcome.MajorVictory:
                case ScenarioOutcome.MinorVictory:
                case ScenarioOutcome.Loss:
                    return true;

                default:
                    return false;
            }
        }

        /// <summary>
        ///     Returns the display name for the given <see cref="UnitType" /> enumeration value.
        /// </summary>
        /// <param name="unitType">A <see cref="UnitType" /> value.</param>
        /// <returns>
        ///     The display name for the given <see cref="UnitType" /> enumeration value.
        /// </returns>
        [Pure]
        public static string ToDisplayName(this UnitType unitType)
        {
            switch (unitType)
            {
                case UnitType.Infantry:
                    return "Infantry";

                case UnitType.Tank:
                    return "Tank";

                case UnitType.Recon:
                    return "Recon";

                case UnitType.AntiTank:
                    return "Anti Tank";

                case UnitType.Artillery:
                    return "Artillery";

                case UnitType.AntiAircraft:
                    return "Anti Aircraft";

                case UnitType.Structure:
                    return "Structure";

                case UnitType.Fighter:
                    return "Fighter";

                case UnitType.TacticalBomber:
                    return "Tactical Bomber";

                case UnitType.StrategicBomber:
                    return "Strategic Bomber";

                case UnitType.Submarine:
                    return "Submarine";

                case UnitType.Destroyer:
                    return "Destroyer";

                case UnitType.CapitalShip:
                    return "Capital Ship";

                case UnitType.Carrier:
                    return "Carrier";

                case UnitType.LandTransport:
                    return "Land Transport";

                case UnitType.AirTransport:
                    return "Air Transport";

                case UnitType.SeaTransport:
                    return "Sea Transport";

                case UnitType.Train:
                    return "Train";

                case UnitType.ArmoredTrain:
                    return "Armored Train";

                case UnitType.RiverBoat:
                    return "River Boat";

                default:
                    throw new ArgumentOutOfRangeException("unitType");
            }
        }

        /// <summary>
        ///     Returns the display name for the given <see cref="Nationality" /> enumeration value.
        /// </summary>
        /// <param name="nationality">A <see cref="Nationality" /> value.</param>
        /// <returns>
        ///     The display name for the given <see cref="Nationality" /> enumeration value.
        /// </returns>
        [Pure]
        public static string ToDisplayName(this Nationality nationality)
        {
            switch (nationality)
            {
                case Nationality.Germany:
                    return "Germany";

                case Nationality.GreatBritain:
                    return "Great Britain";

                case Nationality.USA:
                    return "USA";

                case Nationality.USSR:
                    return "USSR";

                case Nationality.France:
                    return "France";

                case Nationality.Italy:
                    return "Italy";

                case Nationality.Poland:
                    return "Poland";

                case Nationality.Belgium:
                    return "Belgium";

                case Nationality.Netherlands:
                    return "Netherlands";

                case Nationality.NationalisticSpain:
                    return "Nationalistic Spain";

                case Nationality.RepublicanSpain:
                    return "Republican Spain";

                case Nationality.Slovakia:
                    return "Slovakia";

                case Nationality.Luxemburg:
                    return "Luxemburg";

                case Nationality.Hungary:
                    return "Hungary";

                case Nationality.Romania:
                    return "Romania";

                case Nationality.Bulgaria:
                    return "Bulgaria";

                case Nationality.Yugoslavia:
                    return "Yugoslavia";

                case Nationality.Greece:
                    return "Greece";

                case Nationality.Canada:
                    return "Canada";

                case Nationality.NewZealand:
                    return "New Zealand";

                case Nationality.Australia:
                    return "Australia";

                case Nationality.BlueTeam:
                    return "Blue Team";

                case Nationality.RedTeam:
                    return "Red Team";

                case Nationality.Norway:
                    return "Norway";

                case Nationality.Sweden:
                    return "Sweden";

                case Nationality.FreePoland:
                    return "Free Poland";

                case Nationality.FreeFrance:
                    return "Free France";

                case Nationality.Switzerland:
                    return "Switzerland";

                case Nationality.Turkey:
                    return "Turkey";

                case Nationality.SouthAfrica:
                    return "South Africa";

                case Nationality.BritishIndia:
                    return "British India";

                case Nationality.VichyFrance:
                    return "Vichy France";

                default:
                    throw new ArgumentOutOfRangeException("nationality");
            }
        }

        /// <summary>
        ///     Returns the display name for the given <see cref="ScenarioOutcome" /> enumeration value.
        /// </summary>
        /// <param name="scenarioOutcome">A <see cref="ScenarioOutcome" /> value.</param>
        /// <returns>
        ///     The display name for the given <see cref="ScenarioOutcome" /> enumeration value.
        /// </returns>
        [Pure]
        public static string ToDisplayName(this ScenarioOutcome scenarioOutcome)
        {
            switch (scenarioOutcome)
            {
                case ScenarioOutcome.Pending:
                    return "Pending";

                case ScenarioOutcome.MajorVictory:
                    return "Decisive Victory";

                case ScenarioOutcome.MinorVictory:
                    return "Marginal Victory";

                case ScenarioOutcome.Loss:
                    return "Loss";

                default:
                    throw new ArgumentOutOfRangeException("scenarioOutcome");
            }
        }

        /// <summary>
        ///     Returns the flag path for this <see cref="Nationality" />.
        /// </summary>
        /// <param name="nationality">Nationality of the unit.</param>
        /// <returns>The flag path for this <see cref="Nationality" />.</returns>
        [Pure]
        public static string ToFlagPath(this Nationality nationality)
        {
            switch (nationality)
            {
                case Nationality.Germany:
                case Nationality.GreatBritain:
                case Nationality.USA:
                case Nationality.USSR:
                case Nationality.France:
                case Nationality.Italy:
                case Nationality.Poland:
                case Nationality.Belgium:
                case Nationality.Netherlands:
                case Nationality.NationalisticSpain:
                case Nationality.RepublicanSpain:
                case Nationality.Slovakia:
                case Nationality.Luxemburg:
                case Nationality.Hungary:
                case Nationality.Romania:
                case Nationality.Bulgaria:
                case Nationality.Yugoslavia:
                case Nationality.Greece:
                case Nationality.Canada:
                case Nationality.NewZealand:
                case Nationality.Australia:
                case Nationality.BlueTeam:
                case Nationality.RedTeam:
                case Nationality.Norway:
                case Nationality.Sweden:
                case Nationality.FreePoland:
                case Nationality.FreeFrance:
                case Nationality.Switzerland:
                case Nationality.Turkey:
                case Nationality.SouthAfrica:
                case Nationality.BritishIndia:
                case Nationality.VichyFrance:
                    return string.Format("{0}.png", ((int)nationality + 1));

                default:
                    return string.Empty;
            }
        }

        /// <summary>
        ///     Returns the icon path for this <see cref="UnitType" />.
        /// </summary>
        /// <param name="unitType">Type of the unit.</param>
        /// <returns>The icon path for this <see cref="UnitType" />.</returns>
        [Pure]
        public static string ToIconPath(this UnitType unitType)
        {
            switch (unitType)
            {
                case UnitType.Infantry:
                    return "0.png";
                case UnitType.Tank:
                    return "1.png";
                case UnitType.Recon:
                    return "2.png";
                case UnitType.AntiTank:
                    return "3.png";
                case UnitType.Artillery:
                    return "4.png";
                case UnitType.AntiAircraft:
                    return "5.png";
                case UnitType.Fighter:
                    return "7.png";
                case UnitType.TacticalBomber:
                    return "8.png";
                case UnitType.StrategicBomber:
                    return "9.png";

                default:
                    return string.Empty;
            }
        }

        #endregion
    }
}