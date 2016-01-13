// <copyright file="Bonus.cs" company="VacuumBreather">
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

    #endregion

    /// <summary>
    ///     Represents a collection of boni to a unit's stats.
    /// </summary>
    public struct Bonus : IEquatable<Bonus>
    {
        #region Instance Properties

        /// <summary>
        ///     Gets or sets the air attack.
        /// </summary>
        /// <value>
        ///     The air attack.
        /// </value>
        public double AirAttack { get; set; }

        /// <summary>
        ///     Gets or sets the air defense.
        /// </summary>
        /// <value>
        ///     The air defense.
        /// </value>
        public double AirDefense { get; set; }

        /// <summary>
        ///     Gets or sets the close defense.
        /// </summary>
        /// <value>
        ///     The close defense.
        /// </value>
        public double CloseDefense { get; set; }

        /// <summary>
        ///     Gets or sets the ground defense.
        /// </summary>
        /// <value>
        ///     The ground defense.
        /// </value>
        public double GroundDefense { get; set; }

        /// <summary>
        ///     Gets or sets the hard attack.
        /// </summary>
        /// <value>
        ///     The hard attack.
        /// </value>
        public double HardAttack { get; set; }

        /// <summary>
        ///     Gets or sets the initiative.
        /// </summary>
        /// <value>
        ///     The initiative.
        /// </value>
        public double Initiative { get; set; }

        /// <summary>
        ///     Gets or sets the movement.
        /// </summary>
        /// <value>
        ///     The movement.
        /// </value>
        public double Movement { get; set; }

        /// <summary>
        ///     Gets or sets the naval attack.
        /// </summary>
        /// <value>
        ///     The naval attack.
        /// </value>
        public double NavalAttack { get; set; }

        /// <summary>
        ///     Gets or sets the range.
        /// </summary>
        /// <value>
        ///     The range.
        /// </value>
        public double Range { get; set; }

        /// <summary>
        ///     Gets or sets the soft attack.
        /// </summary>
        /// <value>
        ///     The soft attack.
        /// </value>
        public double SoftAttack { get; set; }

        /// <summary>
        ///     Gets or sets the spotting.
        /// </summary>
        /// <value>
        ///     The spotting.
        /// </value>
        public double Spotting { get; set; }

        #endregion

        #region Operators

        /// <summary>
        ///     Adds the specified boni.
        /// </summary>
        /// <param name="left">The first Bonus.</param>
        /// <param name="right">The second Bonus.</param>
        /// <returns>The sum of the two boni.</returns>
        public static Bonus operator +(Bonus left, Bonus right)
        {
            return Add(left, right);
        }

        /// <summary>
        ///     Determines whether the first <see cref="Bonus" /> is equal to the second <see cref="Bonus" />.
        /// </summary>
        /// <param name="left">The first <see cref="Bonus" />.</param>
        /// <param name="right">The second <see cref="Bonus" />.</param>
        /// <returns>
        ///     <c>true</c> if the first <see cref="Bonus" /> is equal to the second <see cref="Bonus" />; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator ==(Bonus left, Bonus right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Determines whether the first <see cref="Bonus" /> is not equal to the second <see cref="Bonus" />.
        /// </summary>
        /// <param name="left">The first <see cref="Bonus" />.</param>
        /// <param name="right">The second <see cref="Bonus" />.</param>
        /// <returns>
        ///     <c>true</c> if the first <see cref="Bonus" /> is not equal to the second <see cref="Bonus" />; otherwise,
        ///     <c>false</c>.
        /// </returns>
        public static bool operator !=(Bonus left, Bonus right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        ///     Multiplies the specified bonus by the specified scalar value.
        /// </summary>
        /// <param name="bonus">A bonus.</param>
        /// <param name="scalar">A scalar.</param>
        /// <returns>
        ///     The product bonus.
        /// </returns>
        public static Bonus operator *(Bonus bonus, double scalar)
        {
            return Multiply(bonus, scalar);
        }

        /// <summary>
        ///     Subtracts the second Bonus from the fist Bonus.
        /// </summary>
        /// <param name="left">The first Bonus.</param>
        /// <param name="right">The second Bonus.</param>
        /// <returns>The difference of the two boni.</returns>
        public static Bonus operator -(Bonus left, Bonus right)
        {
            return Subtract(left, right);
        }

        #endregion

        #region Class Methods

        /// <summary>
        ///     Adds the specified boni.
        /// </summary>
        /// <param name="left">The first Bonus.</param>
        /// <param name="right">The second Bonus.</param>
        /// <returns>The sum of the two boni.</returns>
        public static Bonus Add(Bonus left, Bonus right)
        {
            return new Bonus
                   {
                       Initiative = left.Initiative + right.Initiative,
                       SoftAttack = left.SoftAttack + right.SoftAttack,
                       HardAttack = left.HardAttack + right.HardAttack,
                       AirAttack = left.AirAttack + right.AirAttack,
                       NavalAttack = left.NavalAttack + right.NavalAttack,
                       GroundDefense = left.GroundDefense + right.GroundDefense,
                       AirDefense = left.AirDefense + right.AirDefense,
                       CloseDefense = left.CloseDefense + right.CloseDefense,
                       Range = left.Range + right.Range,
                       Movement = left.Movement + right.Movement,
                       Spotting = left.Spotting + right.Spotting,
                   };
        }

        /// <summary>
        ///     Multiplies the specified bonus by the specified scalar value.
        /// </summary>
        /// <param name="bonus">A bonus.</param>
        /// <param name="scalar">A scalar.</param>
        /// <returns>
        ///     The product bonus.
        /// </returns>
        public static Bonus Multiply(Bonus bonus, double scalar)
        {
            return new Bonus
                   {
                       Initiative = bonus.Initiative * scalar,
                       SoftAttack = bonus.SoftAttack * scalar,
                       HardAttack = bonus.HardAttack * scalar,
                       AirAttack = bonus.AirAttack * scalar,
                       NavalAttack = bonus.NavalAttack * scalar,
                       GroundDefense = bonus.GroundDefense * scalar,
                       AirDefense = bonus.AirDefense * scalar,
                       CloseDefense = bonus.CloseDefense * scalar,
                       Range = bonus.Range * scalar,
                       Movement = bonus.Movement * scalar,
                       Spotting = bonus.Spotting * scalar,
                   };
        }

        /// <summary>
        ///     Subtracts the second Bonus from the fist Bonus.
        /// </summary>
        /// <param name="left">The first Bonus.</param>
        /// <param name="right">The second Bonus.</param>
        /// <returns>The difference of the two boni.</returns>
        public static Bonus Subtract(Bonus left, Bonus right)
        {
            return new Bonus
                   {
                       Initiative = left.Initiative - right.Initiative,
                       SoftAttack = left.SoftAttack - right.SoftAttack,
                       HardAttack = left.HardAttack - right.HardAttack,
                       AirAttack = left.AirAttack - right.AirAttack,
                       NavalAttack = left.NavalAttack - right.NavalAttack,
                       GroundDefense = left.GroundDefense - right.GroundDefense,
                       AirDefense = left.AirDefense - right.AirDefense,
                       CloseDefense = left.CloseDefense - right.CloseDefense,
                       Range = left.Range - right.Range,
                       Movement = left.Movement - right.Movement,
                       Spotting = left.Spotting - right.Spotting,
                   };
        }

        #endregion

        #region Instance Methods

        /// <summary>
        ///     Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///     <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (obj.GetType() != typeof(Bonus))
            {
                return false;
            }
            return Equals((Bonus)obj);
        }

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        ///     A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int result = Initiative.GetHashCode();
                result = (result * 397) ^ SoftAttack.GetHashCode();
                result = (result * 397) ^ HardAttack.GetHashCode();
                result = (result * 397) ^ AirAttack.GetHashCode();
                result = (result * 397) ^ NavalAttack.GetHashCode();
                result = (result * 397) ^ GroundDefense.GetHashCode();
                result = (result * 397) ^ AirDefense.GetHashCode();
                result = (result * 397) ^ CloseDefense.GetHashCode();
                result = (result * 397) ^ Range.GetHashCode();
                result = (result * 397) ^ Movement.GetHashCode();
                result = (result * 397) ^ Spotting.GetHashCode();
                return result;
            }
        }

        #endregion

        #region IEquatable<Bonus> Members

        /// <summary>
        ///     Determines whether the specified <see cref="Bonus" /> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="Bonus" /> to compare with this instance.</param>
        /// <returns>
        ///     <c>true</c> if the specified <see cref="Bonus" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(Bonus other)
        {
            return other.Initiative.Equals(Initiative) && other.SoftAttack.Equals(SoftAttack) &&
                   other.HardAttack.Equals(HardAttack) && other.AirAttack.Equals(AirAttack) &&
                   other.NavalAttack.Equals(NavalAttack) && other.GroundDefense.Equals(GroundDefense) &&
                   other.AirDefense.Equals(AirDefense) && other.CloseDefense.Equals(CloseDefense) &&
                   other.Range.Equals(Range) && other.Movement.Equals(Movement) && other.Spotting.Equals(Spotting);
        }

        #endregion
    }
}