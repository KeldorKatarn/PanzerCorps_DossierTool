// <copyright file="Award.cs" company="VacuumBreather">
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

    #endregion

    #region Using Directives

    #endregion

    /// <summary>
    ///     Represents the different awards that Panzer Corps units can gain.
    /// </summary>
    public class Award : IEquatable<Award>
    {
        #region Readonly & Static Fields

        /// <summary>
        ///     The none award.
        /// </summary>
        public static Award None = new Award { ID = "None", DisplayName = "No award", ImageFile = string.Empty };

        #endregion

        #region Instance Properties

        /// <summary>
        ///     Gets or sets the display name.
        /// </summary>
        /// <value>
        ///     The display name.
        /// </value>
        public string DisplayName { get; set; }

        /// <summary>
        ///     Gets or sets the ID.
        /// </summary>
        /// <value>
        ///     The ID.
        /// </value>
        public string ID { get; set; }

        /// <summary>
        ///     Gets or sets the image file.
        /// </summary>
        /// <value>
        ///     The image file.
        /// </value>
        public string ImageFile { get; set; }

        /// <summary>
        ///     Gets or sets the nationality.
        /// </summary>
        /// <value>
        ///     The nationality.
        /// </value>
        public Nationality Nationality { get; set; }

        #endregion

        #region Operators

        /// <summary>
        ///     Determines whether the specified two <see cref="Award">Awards</see> are equal.
        /// </summary>
        /// <param name="left">The first <see cref="Award" />.</param>
        /// <param name="right">The second <see cref="Award" />.</param>
        /// <returns>
        ///     <c>true</c> if the specified two <see cref="Award">Awards</see> are equal; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator ==(Award left, Award right)
        {
            return Equals(left, right);
        }

        /// <summary>
        ///     Determines whether the specified two <see cref="Award">Awards</see> are not equal.
        /// </summary>
        /// <param name="left">The first <see cref="Award" />.</param>
        /// <param name="right">The second <see cref="Award" />.</param>
        /// <returns>
        ///     <c>true</c> if the specified two <see cref="Award">Awards</see> are not equal; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator !=(Award left, Award right)
        {
            return !Equals(left, right);
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
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != typeof(Award))
            {
                return false;
            }
            return Equals((Award)obj);
        }

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        ///     A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            return (ID != null ? ID.GetHashCode() : 0);
        }

        #endregion

        #region IEquatable<Award> Members

        /// <summary>
        ///     Determines whether the specified <see cref="Award" /> is equal to this award.
        /// </summary>
        /// <param name="other">The <see cref="Award" /> to compare with this award.</param>
        /// <returns>
        ///     <c>true</c> if the specified <see cref="Award" /> is equal to this award; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(Award other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return Equals(other.ID, ID);
        }

        #endregion
    }
}