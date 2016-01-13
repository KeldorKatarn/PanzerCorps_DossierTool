// <copyright file="Hero.cs" company="VacuumBreather">
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

    using System;
    using System.Runtime.Serialization;

    #endregion

    /// <summary>
    ///     Represents the different heroes a unit can have.
    /// </summary>
    [DataContract]
    public struct Hero : IEquatable<Hero>
    {
        #region Readonly & Static Fields

        /// <summary>
        ///     The none hero.
        /// </summary>
        public static Hero None = new Hero { ID = "None", DisplayName = "None" };

        #endregion

        #region Instance Properties

        /// <summary>
        ///     Gets or sets the attack bonus.
        /// </summary>
        /// <value>
        ///     The attack bonus.
        /// </value>
        [DataMember(Name = "AttackBonus", Order = 2, IsRequired = true)]
        public int AttackBonus { get; set; }

        /// <summary>
        ///     Gets or sets the defense bonus.
        /// </summary>
        /// <value>
        ///     The defense bonus.
        /// </value>
        [DataMember(Name = "DefenseBonus", Order = 3, IsRequired = true)]
        public int DefenseBonus { get; set; }

        /// <summary>
        ///     Gets or sets the display name.
        /// </summary>
        /// <value>
        ///     The display name.
        /// </value>
        [DataMember(Name = "DisplayName", Order = 1, IsRequired = true)]
        public string DisplayName { get; set; }

        /// <summary>
        ///     Gets or sets the ID.
        /// </summary>
        /// <value>
        ///     The ID.
        /// </value>
        [DataMember(Name = "ID", Order = 0, IsRequired = true)]
        public string ID { get; set; }

        /// <summary>
        ///     Gets or sets the initiative bonus.
        /// </summary>
        /// <value>
        ///     The initiative bonus.
        /// </value>
        [DataMember(Name = "InitiativeBonus", Order = 4, IsRequired = true)]
        public int InitiativeBonus { get; set; }

        /// <summary>
        ///     Gets or sets the movement bonus.
        /// </summary>
        /// <value>
        ///     The movement bonus.
        /// </value>
        [DataMember(Name = "MovementBonus", Order = 6, IsRequired = true)]
        public int MovementBonus { get; set; }

        /// <summary>
        ///     Gets or sets the range bonus.
        /// </summary>
        /// <value>
        ///     The range bonus.
        /// </value>
        [DataMember(Name = "RangeBonus", Order = 5, IsRequired = true)]
        public int RangeBonus { get; set; }

        /// <summary>
        ///     Gets or sets the spotting bonus.
        /// </summary>
        /// <value>
        ///     The spotting bonus.
        /// </value>
        [DataMember(Name = "SpottingBonus", Order = 7, IsRequired = true)]
        public int SpottingBonus { get; set; }

        #endregion

        #region Operators

        /// <summary>
        ///     Equals operator.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result.</returns>
        public static bool operator ==(Hero left, Hero right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Not-Equals operator.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result.</returns>
        public static bool operator !=(Hero left, Hero right)
        {
            return !left.Equals(right);
        }

        #endregion

        #region Instance Methods

        /// <summary>
        ///     Gibt an, ob diese Instanz und ein angegebenes Objekt gleich sind.
        /// </summary>
        /// <returns>
        ///     true, wenn <paramref name="obj" /> und diese Instanz denselben Typ aufweisen und denselben Wert darstellen,
        ///     andernfalls false.
        /// </returns>
        /// <param name="obj">Ein weiteres Objekt für den Vergleich. </param>
        /// <filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (obj.GetType() != typeof(Hero))
            {
                return false;
            }
            return Equals((Hero)obj);
        }

        /// <summary>
        ///     Gibt den Hashcode für diese Instanz zurück.
        /// </summary>
        /// <returns>
        ///     Eine 32-Bit-Ganzzahl mit Vorzeichen. Diese ist der Hashcode für die Instanz.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return (ID != null ? ID.GetHashCode() : 0);
        }

        #endregion

        #region IEquatable<Hero> Members

        /// <summary>
        ///     Gibt an, ob das aktuelle Objekt einem anderen Objekt des gleichen Typs entspricht.
        /// </summary>
        /// <returns>
        ///     true, wenn das aktuelle Objekt gleich dem <paramref name="other" />-Parameter ist, andernfalls false.
        /// </returns>
        /// <param name="other">Ein Objekt, das mit diesem Objekt verglichen werden soll.</param>
        public bool Equals(Hero other)
        {
            return Equals(other.ID, ID);
        }

        #endregion
    }
}