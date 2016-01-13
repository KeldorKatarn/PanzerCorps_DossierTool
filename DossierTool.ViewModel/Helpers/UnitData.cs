// <copyright file="UnitData.cs" company="VacuumBreather">
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
    using System.Collections.Generic;
    using Decorators;
    using Model;
    using Model.Helpers;
    using Services;

    #endregion

    /// <summary>
    ///     Helper class offering easy access to certain unit data.
    /// </summary>
    public class UnitData
    {
        #region Readonly & Static Fields

        private readonly UnitDecorator _unit;
        private readonly Bonus _bonus;
        private readonly BonusInfo _bonusInfo;
        private readonly bool _useLandTransportData;

        private readonly string[] _targetTypes =
        {
            "Soft",
            "Hard",
            "Air",
            "Naval"
        };

        private readonly KeyValuePair<string, UnitType> _landTransportType =
            new KeyValuePair<string, UnitType>(UnitType.LandTransport.ToDisplayName(), UnitType.LandTransport);

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="UnitData" /> struct.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="bonusProvider">The bonus provider.</param>
        /// <param name="useLandTransportData">if set to <c>true</c> land transport data is used instead of the main equipment.</param>
        public UnitData(UnitDecorator unit, IBonusProvider bonusProvider, bool useLandTransportData = false)
        {
            this._unit = unit;
            this._useLandTransportData = useLandTransportData;

            double numStars = Math.Floor(this._unit.CurrentExperience / 100.0);
            Bonus experienceBonux = bonusProvider.ExperienceBoni[this._unit.Type.Value] * numStars;
            Bonus firstHeroBonus = bonusProvider.GetHeroBonusFor(this._unit.CurrentFirstHero.Value);
            Bonus secondHeroBonus = bonusProvider.GetHeroBonusFor(this._unit.CurrentSecondHero.Value);
            Bonus thirdHeroBonus = bonusProvider.GetHeroBonusFor(this._unit.CurrentThirdHero.Value);
            Bonus totalHeroBonus = firstHeroBonus + secondHeroBonus + thirdHeroBonus;

            var experienceBonus = new Bonus
                                  {
                                      Initiative = (Equipment.Initiative != 0) ? experienceBonux.Initiative : 0,
                                      SoftAttack = (Equipment.SoftAttack != 0) ? experienceBonux.SoftAttack : 0,
                                      HardAttack = (Equipment.HardAttack != 0) ? experienceBonux.HardAttack : 0,
                                      AirAttack = (Equipment.AirAttack != 0) ? experienceBonux.AirAttack : 0,
                                      NavalAttack =
                                          (Equipment.NavalAttack != 0) ? experienceBonux.NavalAttack : 0,
                                      GroundDefense =
                                          (Equipment.GroundDefense != 0) ? experienceBonux.GroundDefense : 0,
                                      AirDefense = (Equipment.AirDefense != 0) ? experienceBonux.AirDefense : 0,
                                      CloseDefense =
                                          (Equipment.CloseDefense != 0) ? experienceBonux.CloseDefense : 0,
                                      Range = (Equipment.Range != 0) ? experienceBonux.Range : 0,
                                      Movement = (Equipment.Movement != 0) ? experienceBonux.Movement : 0,
                                      Spotting = (Equipment.Spotting != 0) ? experienceBonux.Spotting : 0,
                                  };

            var heroBonus = new Bonus
                            {
                                Initiative = (Equipment.Initiative != 0) ? totalHeroBonus.Initiative : 0,
                                SoftAttack = (Equipment.SoftAttack != 0) ? totalHeroBonus.SoftAttack : 0,
                                HardAttack = (Equipment.HardAttack != 0) ? totalHeroBonus.HardAttack : 0,
                                AirAttack = (Equipment.AirAttack != 0) ? totalHeroBonus.AirAttack : 0,
                                NavalAttack = (Equipment.NavalAttack != 0) ? totalHeroBonus.NavalAttack : 0,
                                GroundDefense =
                                    (Equipment.GroundDefense != 0) ? totalHeroBonus.GroundDefense : 0,
                                AirDefense = (Equipment.AirDefense != 0) ? totalHeroBonus.AirDefense : 0,
                                CloseDefense = (Equipment.CloseDefense != 0) ? totalHeroBonus.CloseDefense : 0,
                                Range = (Equipment.Range != 0) ? totalHeroBonus.Range : 0,
                                Movement = (Equipment.Movement != 0) ? totalHeroBonus.Movement : 0,
                                Spotting = (Equipment.Spotting != 0) ? totalHeroBonus.Spotting : 0,
                            };

            this._bonusInfo = new BonusInfo(heroBonus, experienceBonus);

            this._bonus = experienceBonus + heroBonus;
        }

        #endregion

        #region Instance Properties

        /// <summary>
        ///     Gets the absolute air attack.
        /// </summary>
        /// <value>
        ///     The absolute air attack.
        /// </value>
        public int AbsAirAttack
        {
            get
            {
                return Math.Abs(AirAttack);
            }
        }

        /// <summary>
        ///     Gets the absolute hard attack.
        /// </summary>
        /// <value>
        ///     The absolute hard attack.
        /// </value>
        public int AbsHardAttack
        {
            get
            {
                return Math.Abs(HardAttack);
            }
        }

        /// <summary>
        ///     Gets the absolute naval attack.
        /// </summary>
        /// <value>
        ///     The absolute naval attack.
        /// </value>
        public int AbsNavalAttack
        {
            get
            {
                return Math.Abs(NavalAttack);
            }
        }

        /// <summary>
        ///     Gets the absolute soft attack.
        /// </summary>
        /// <value>
        ///     The absolute soft attack.
        /// </value>
        public int AbsSoftAttack
        {
            get
            {
                return Math.Abs(SoftAttack);
            }
        }

        /// <summary>
        ///     Gets the air attack.
        /// </summary>
        /// <value>
        ///     The air attack.
        /// </value>
        public int AirAttack
        {
            get
            {
                int airAttack = Equipment.AirAttack;

                airAttack += (int)Math.Round(Bonus.AirAttack) * Math.Sign(Equipment.AirAttack);

                return airAttack;
            }
        }

        /// <summary>
        ///     Gets the air defense.
        /// </summary>
        /// <value>
        ///     The air defense.
        /// </value>
        public int AirDefense
        {
            get
            {
                int airDefense = Equipment.AirDefense;

                airDefense += (int)Math.Round(Bonus.AirDefense);

                return airDefense;
            }
        }

        /// <summary>
        ///     Gets the total bonus.
        /// </summary>
        /// <value>
        ///     The total bonus.
        /// </value>
        public Bonus Bonus
        {
            get
            {
                return this._bonus;
            }
        }

        /// <summary>
        ///     Gets the bonus info.
        /// </summary>
        /// <value>
        ///     The bonus info.
        /// </value>
        public BonusInfo BonusInfo
        {
            get
            {
                return this._bonusInfo;
            }
        }

        /// <summary>
        ///     Gets the close defense.
        /// </summary>
        /// <value>
        ///     The close defense.
        /// </value>
        public int CloseDefense
        {
            get
            {
                int closeDefense = Equipment.CloseDefense;

                closeDefense += (int)Math.Round(Bonus.CloseDefense);

                return closeDefense;
            }
        }

        /// <summary>
        ///     Gets the cost.
        /// </summary>
        /// <value>
        ///     The cost.
        /// </value>
        public int Cost
        {
            get
            {
                return Equipment.Cost;
            }
        }

        /// <summary>
        ///     Gets the name of the equipment.
        /// </summary>
        /// <value>
        ///     The name of the equipment.
        /// </value>
        public string EquipmentName
        {
            get
            {
                return Equipment.ShortName;
            }
        }

        /// <summary>
        ///     Gets the experience.
        /// </summary>
        /// <value>
        ///     The experience.
        /// </value>
        public int Experience
        {
            get
            {
                return this._unit.CurrentExperience;
            }
        }

        /// <summary>
        ///     Gets the ground defense.
        /// </summary>
        /// <value>
        ///     The ground defense.
        /// </value>
        public int GroundDefense
        {
            get
            {
                int groundDefense = Equipment.GroundDefense;

                groundDefense += (int)Math.Round(Bonus.GroundDefense);

                return groundDefense;
            }
        }

        /// <summary>
        ///     Gets the hard attack.
        /// </summary>
        /// <value>
        ///     The hard attack.
        /// </value>
        public int HardAttack
        {
            get
            {
                int hardAttack = Equipment.HardAttack;

                hardAttack += (int)Math.Round(Bonus.HardAttack) * Math.Sign(Equipment.HardAttack);

                return hardAttack;
            }
        }

        /// <summary>
        ///     Gets the icon.
        /// </summary>
        /// <value>
        ///     The icon.
        /// </value>
        public string Icon
        {
            get
            {
                return Equipment.Icon;
            }
        }

        /// <summary>
        ///     Gets the initiative.
        /// </summary>
        /// <value>
        ///     The initiative.
        /// </value>
        public int Initiative
        {
            get
            {
                int initiative = Equipment.Initiative;

                initiative += (int)Math.Round(Bonus.Initiative);

                return initiative;
            }
        }

        /// <summary>
        ///     Gets the max ammo.
        /// </summary>
        /// <value>
        ///     The max ammo.
        /// </value>
        public int MaxAmmo
        {
            get
            {
                return Equipment.MaxAmmo;
            }
        }

        /// <summary>
        ///     Gets the max fuel.
        /// </summary>
        /// <value>
        ///     The max fuel.
        /// </value>
        public int MaxFuel
        {
            get
            {
                return Equipment.MaxFuel;
            }
        }

        /// <summary>
        ///     Gets the max strength.
        /// </summary>
        /// <value>
        ///     The max strength.
        /// </value>
        public int MaxStrength
        {
            get
            {
                return Equipment.MaxStrength;
            }
        }

        /// <summary>
        ///     Gets the movement.
        /// </summary>
        /// <value>
        ///     The movement.
        /// </value>
        public int Movement
        {
            get
            {
                int movement = Equipment.Movement;

                movement += (int)Math.Round(Bonus.Movement);

                return movement;
            }
        }

        /// <summary>
        ///     Gets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name
        {
            get
            {
                return this._useLandTransportData ? string.Empty : this._unit.Name;
            }
        }

        /// <summary>
        ///     Gets the naval attack.
        /// </summary>
        /// <value>
        ///     The naval attack.
        /// </value>
        public int NavalAttack
        {
            get
            {
                int navalAttack = Equipment.NavalAttack;

                navalAttack += (int)(Math.Round(Bonus.NavalAttack)) * Math.Sign(Equipment.NavalAttack);

                return navalAttack;
            }
        }

        /// <summary>
        ///     Gets the range.
        /// </summary>
        /// <value>
        ///     The range.
        /// </value>
        public int Range
        {
            get
            {
                int range = Equipment.Range;

                range += (int)Math.Round(Bonus.Range);

                return range;
            }
        }

        /// <summary>
        ///     Gets the soft attack.
        /// </summary>
        /// <value>
        ///     The soft attack.
        /// </value>
        public int SoftAttack
        {
            get
            {
                int softAttack = Equipment.SoftAttack;

                softAttack += (int)Math.Round(Bonus.SoftAttack) * Math.Sign(Equipment.SoftAttack);

                return softAttack;
            }
        }

        /// <summary>
        ///     Gets the spotting.
        /// </summary>
        /// <value>
        ///     The spotting.
        /// </value>
        public int Spotting
        {
            get
            {
                int spotting = Equipment.Spotting;

                spotting += (int)Math.Round(Bonus.Spotting);

                return spotting;
            }
        }

        /// <summary>
        ///     Gets the type of the target.
        /// </summary>
        /// <value>
        ///     The type of the target.
        /// </value>
        public string TargetType
        {
            get
            {
                return this._targetTypes[Equipment.TargetType];
            }
        }

        /// <summary>
        ///     Gets the unit type.
        /// </summary>
        /// <value>
        ///     The unit type.
        /// </value>
        public KeyValuePair<string, UnitType> Type
        {
            get
            {
                return this._useLandTransportData ? this._landTransportType : this._unit.Type;
            }
        }

        private Equipment Equipment
        {
            get
            {
                return this._useLandTransportData ? this._unit.CurrentLandTransport : this._unit.CurrentEquipment;
            }
        }

        #endregion
    }
}