// <copyright file="BonusInfo.cs" company="VacuumBreather">
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
    ///     Represents a collection of descriptions of boni to a unit's stats.
    /// </summary>
    public struct BonusInfo
    {
        #region Fields

        private Bonus _heroBonus;
        private Bonus _experienceBonus;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="BonusInfo" /> struct.
        /// </summary>
        /// <param name="heroBonus">The hero bonus.</param>
        /// <param name="experienceBonus">The experience bonus.</param>
        public BonusInfo(Bonus heroBonus, Bonus experienceBonus)
        {
            this._heroBonus = heroBonus;
            this._experienceBonus = experienceBonus;
        }

        #endregion

        #region Instance Properties

        /// <summary>
        ///     Gets or sets the air attack bonus info.
        /// </summary>
        /// <value>
        ///     The air attack bonus info.
        /// </value>
        public string AirAttack
        {
            get
            {
                return InfoString(this._heroBonus.AirAttack, this._experienceBonus.AirAttack);
            }
        }

        /// <summary>
        ///     Gets or sets the air defense bonus info.
        /// </summary>
        /// <value>
        ///     The air defense bonus info.
        /// </value>
        public string AirDefense
        {
            get
            {
                return InfoString(this._heroBonus.AirDefense, this._experienceBonus.AirDefense);
            }
        }

        /// <summary>
        ///     Gets or sets the close defense bonus info.
        /// </summary>
        /// <value>
        ///     The close defense bonus info.
        /// </value>
        public string CloseDefense
        {
            get
            {
                return InfoString(this._heroBonus.CloseDefense, this._experienceBonus.CloseDefense);
            }
        }

        /// <summary>
        ///     Gets or sets the ground defense bonus info.
        /// </summary>
        /// <value>
        ///     The ground defense bonus info.
        /// </value>
        public string GroundDefense
        {
            get
            {
                return InfoString(this._heroBonus.GroundDefense, this._experienceBonus.GroundDefense);
            }
        }

        /// <summary>
        ///     Gets or sets the hard attack bonus info.
        /// </summary>
        /// <value>
        ///     The hard attack bonus info.
        /// </value>
        public string HardAttack
        {
            get
            {
                return InfoString(this._heroBonus.HardAttack, this._experienceBonus.HardAttack);
            }
        }

        /// <summary>
        ///     Gets or sets the initiative bonus info.
        /// </summary>
        /// <value>
        ///     The initiative bonus info.
        /// </value>
        public string Initiative
        {
            get
            {
                return InfoString(this._heroBonus.Initiative, this._experienceBonus.Initiative);
            }
        }

        /// <summary>
        ///     Gets or sets the movement bonus info.
        /// </summary>
        /// <value>
        ///     The movement bonus info.
        /// </value>
        public string Movement
        {
            get
            {
                return InfoString(this._heroBonus.Movement, this._experienceBonus.Movement);
            }
        }

        /// <summary>
        ///     Gets or sets the naval attack bonus info.
        /// </summary>
        /// <value>
        ///     The naval attack bonus info.
        /// </value>
        public string NavalAttack
        {
            get
            {
                return InfoString(this._heroBonus.NavalAttack, this._experienceBonus.NavalAttack);
            }
        }

        /// <summary>
        ///     Gets or sets the range bonus info.
        /// </summary>
        /// <value>
        ///     The range bonus info.
        /// </value>
        public string Range
        {
            get
            {
                return InfoString(this._heroBonus.Range, this._experienceBonus.Range);
            }
        }

        /// <summary>
        ///     Gets or sets the soft attack bonus info.
        /// </summary>
        /// <value>
        ///     The soft attack bonus info.
        /// </value>
        public string SoftAttack
        {
            get
            {
                return InfoString(this._heroBonus.SoftAttack, this._experienceBonus.SoftAttack);
            }
        }

        /// <summary>
        ///     Gets or sets the spotting bonus info.
        /// </summary>
        /// <value>
        ///     The spotting bonus info.
        /// </value>
        public string Spotting
        {
            get
            {
                return InfoString(this._heroBonus.Spotting, this._experienceBonus.Spotting);
            }
        }

        #endregion

        #region Instance Methods

        private string InfoString(double heroBonus, double experienceBonus)
        {
            var heroBonusRounded = (int)Math.Round(heroBonus);
            var experienceBonusRounded = (int)Math.Round(experienceBonus);

            string heroBonusInfo = (heroBonusRounded != 0) ? heroBonusRounded.ToString() : string.Empty;
            string expBonusInfo = (experienceBonusRounded != 0) ? experienceBonusRounded.ToString() : string.Empty;

            string bonusInfo = string.Empty;

            if (!string.IsNullOrEmpty(heroBonusInfo))
            {
                string sign = (heroBonusRounded > 0 ? "+" : string.Empty);

                bonusInfo = string.Format("Hero: {0}{1}", sign, heroBonusInfo);
            }

            if (!string.IsNullOrEmpty(expBonusInfo))
            {
                string maybeNewLine = string.IsNullOrEmpty(bonusInfo) ? string.Empty : Environment.NewLine;
                string sign = (experienceBonusRounded > 0 ? "+" : string.Empty);

                bonusInfo += string.Format("{0}XP: {1}{2}", maybeNewLine, sign, expBonusInfo);
            }

            if (string.IsNullOrEmpty(bonusInfo))
            {
                bonusInfo = "No bonus";
            }

            return bonusInfo;
        }

        #endregion
    }
}