// <copyright file="Nationality.cs" company="VacuumBreather">
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

    /// <summary>
    ///     Represent the possible nationalities of Panzer Corps core units.
    /// </summary>
    [DataContract]
    public enum Nationality
    {
        /// <summary>
        ///     German units.
        /// </summary>
        [EnumMember]
        Germany = 0,

        /// <summary>
        ///     British units.
        /// </summary>
        [EnumMember]
        GreatBritain,

        /// <summary>
        ///     US American units.
        /// </summary>
        [EnumMember]
        USA,

        /// <summary>
        ///     Soviet units.
        /// </summary>
        [EnumMember]
        USSR,

        /// <summary>
        ///     French units.
        /// </summary>
        [EnumMember]
        France,

        /// <summary>
        ///     Italian units.
        /// </summary>
        [EnumMember]
        Italy,

        /// <summary>
        ///     Polish units.
        /// </summary>
        [EnumMember]
        Poland,

        /// <summary>
        ///     Belgiam units.
        /// </summary>
        [EnumMember]
        Belgium,

        /// <summary>
        ///     Dutch units.
        /// </summary>
        [EnumMember]
        Netherlands,

        /// <summary>
        ///     Nationalistic Spanish units.
        /// </summary>
        [EnumMember]
        NationalisticSpain,

        /// <summary>
        ///     Republican Spanish units.
        /// </summary>
        [EnumMember]
        RepublicanSpain,

        /// <summary>
        ///     Slovakian units.
        /// </summary>
        [EnumMember]
        Slovakia,

        /// <summary>
        ///     Luxemburgish units.
        /// </summary>
        [EnumMember]
        Luxemburg,

        /// <summary>
        ///     Hungarian units.
        /// </summary>
        [EnumMember]
        Hungary,

        /// <summary>
        ///     Romanian units.
        /// </summary>
        [EnumMember]
        Romania,

        /// <summary>
        ///     Bulgarian units.
        /// </summary>
        [EnumMember]
        Bulgaria,

        /// <summary>
        ///     Yugoslavian units.
        /// </summary>
        [EnumMember]
        Yugoslavia,

        /// <summary>
        ///     Greek units.
        /// </summary>
        [EnumMember]
        Greece,

        /// <summary>
        ///     Canadian units.
        /// </summary>
        [EnumMember]
        Canada,

        /// <summary>
        ///     New Zealand units.
        /// </summary>
        [EnumMember]
        NewZealand,

        /// <summary>
        ///     Australian units.
        /// </summary>
        [EnumMember]
        Australia,

        /// <summary>
        ///     Blue Team units.
        /// </summary>
        [EnumMember]
        BlueTeam,

        /// <summary>
        ///     RedTeam units.
        /// </summary>
        [EnumMember]
        RedTeam,

        /// <summary>
        ///     Norwegian units.
        /// </summary>
        [EnumMember]
        Norway,

        /// <summary>
        ///     Swedish units.
        /// </summary>
        [EnumMember]
        Sweden,

        /// <summary>
        ///     Free Polish units.
        /// </summary>
        [EnumMember]
        FreePoland,

        /// <summary>
        ///     Free French units.
        /// </summary>
        [EnumMember]
        FreeFrance,

        /// <summary>
        ///     Swiss units.
        /// </summary>
        [EnumMember]
        Switzerland,

        /// <summary>
        ///     Turkish units.
        /// </summary>
        [EnumMember]
        Turkey,

        /// <summary>
        ///     South African units.
        /// </summary>
        [EnumMember]
        SouthAfrica,

        /// <summary>
        ///     British Indian units.
        /// </summary>
        [EnumMember]
        BritishIndia,

        /// <summary>
        ///     Vichy French units.
        /// </summary>
        [EnumMember]
        VichyFrance,
    }
}