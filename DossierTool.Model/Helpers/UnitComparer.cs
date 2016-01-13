// <copyright file="UnitComparer.cs" company="VacuumBreather">
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

    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    ///     Comparer for <see cref="UnitBase" /> objects.
    /// </summary>
    public class UnitComparer : IComparer<UnitBase>, IComparer
    {
        #region IComparer Members

        /// <summary>
        ///     Compares the two specified objects.
        /// </summary>
        /// <param name="lhs">The first <see cref="object" />.</param>
        /// <param name="rhs">The second <see cref="object" />.</param>
        /// <returns>
        ///     A negative value if lhs is less than rhs, a positive value if lhs is greater than rhs and 0 if both are equal
        ///     accordig to the comparison.
        /// </returns>
        public int Compare(object lhs, object rhs)
        {
            var lhsUnit = (UnitBase)lhs;
            var rhsUnit = (UnitBase)rhs;

            return Compare(lhsUnit, rhsUnit);
        }

        #endregion

        #region IComparer<UnitBase> Members

        /// <summary>
        ///     Compares the two specified units.
        /// </summary>
        /// <param name="lhs">The first <see cref="UnitBase" />.</param>
        /// <param name="rhs">The second <see cref="UnitBase" />.</param>
        /// <returns>
        ///     A negative value if lhs is less than rhs, a positive value if lhs is greater than rhs and 0 if both are equal
        ///     accordig to the comparison.
        /// </returns>
        public int Compare(UnitBase lhs, UnitBase rhs)
        {
            if (lhs == null && rhs == null)
            {
                return 0;
            }

            if (lhs == null)
            {
                return 1;
            }

            if (rhs == null)
            {
                return -1;
            }

            int comparison;

            if (lhs is HigherUnit && rhs is HigherUnit)
            {
                // Both are a HigherUnit.
                UnitBase lhsFirst = ((HigherUnit)lhs).Subordinates.OrderBy(unit => unit, this).FirstOrDefault();
                UnitBase rhsFirst = ((HigherUnit)rhs).Subordinates.OrderBy(unit => unit, this).FirstOrDefault();

                comparison = Compare(lhsFirst, rhsFirst);

                if (comparison == 0)
                {
                    comparison = string.Compare(lhs.Name, rhs.Name);
                }
            }
            else if ((lhs is Unit) ^ (rhs is Unit))
            {
                // One of them is a Unit.
                comparison = (lhs is Unit) ? 1 : -1;
            }
            else
            {
                // Both are a Unit.
                UnitType lhsType = ((Unit)lhs).Type;
                UnitType rhsType = ((Unit)rhs).Type;

                if (lhsType == rhsType)
                {
                    comparison = string.Compare(lhs.Name, rhs.Name);
                }
                else
                {
                    comparison = (lhsType == rhsType) ? 0 : ((lhsType < rhsType) ? -1 : 1);
                }
            }

            return comparison;
        }

        #endregion
    }
}