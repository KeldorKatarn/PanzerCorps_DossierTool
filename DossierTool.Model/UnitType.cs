// <copyright file="UnitType.cs" company="VacuumBreather">
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

namespace DossierTool.Model
{
    #region Using Directives

    using System.Runtime.Serialization;

    #endregion

    #region Using Directives

    #endregion

    /// <summary>
    ///     Enum representing the differnt unit types in Panzer Corps.
    /// </summary>
    [DataContract]
    public enum UnitType
    {
        /// <summary>
        ///     Infantery.
        /// </summary>
        [EnumMember]
        Infantry = 0,

        /// <summary>
        ///     Tanks.
        /// </summary>
        [EnumMember]
        Tank,

        /// <summary>
        ///     Recon Vehicles.
        /// </summary>
        [EnumMember]
        Recon,

        /// <summary>
        ///     Anti-Tank guns and Tank Destroyers.
        /// </summary>
        [EnumMember]
        AntiTank,

        /// <summary>
        ///     Field Artillery and Self Propelled Artillery.
        /// </summary>
        [EnumMember]
        Artillery,

        /// <summary>
        ///     Flak and Flak tanks.
        /// </summary>
        [EnumMember]
        AntiAircraft,

        /// <summary>
        ///     Bunkers, radar towers, etc.
        /// </summary>
        [EnumMember]
        Structure,

        /// <summary>
        ///     Fighter craft.
        /// </summary>
        [EnumMember]
        Fighter,

        /// <summary>
        ///     Close support and dive bombers.
        /// </summary>
        [EnumMember]
        TacticalBomber,

        /// <summary>
        ///     Large long range bombers.
        /// </summary>
        [EnumMember]
        StrategicBomber,

        /// <summary>
        ///     Submarines and U-Boats.
        /// </summary>
        [EnumMember]
        Submarine,

        /// <summary>
        ///     Destroyers, speedboat and other light ships and boats.
        /// </summary>
        [EnumMember]
        Destroyer,

        /// <summary>
        ///     Cruisers and battleships.
        /// </summary>
        [EnumMember]
        CapitalShip,

        /// <summary>
        ///     Aircraft carriers.
        /// </summary>
        [EnumMember]
        Carrier,

        /// <summary>
        ///     Trucks and other land transport vehicles.
        /// </summary>
        [EnumMember]
        LandTransport,

        /// <summary>
        ///     Transport aircraft.
        /// </summary>
        [EnumMember]
        AirTransport,

        /// <summary>
        ///     Landing craft.
        /// </summary>
        [EnumMember]
        SeaTransport,

        /// <summary>
        ///     Rail transports.
        /// </summary>
        [EnumMember]
        Train,

        /// <summary>
        ///     Armored trains.
        /// </summary>
        [EnumMember]
        ArmoredTrain,

        /// <summary>
        ///     River boats.
        /// </summary>
        [EnumMember]
        RiverBoat
    }
}