// <copyright file="HeroProvider.cs" company="VacuumBreather">
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

namespace DossierTool.ViewModel.Services
{
    #region Using Directives

    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Xml;
    using Model;

    #endregion

    /// <summary>
    ///     Provides access to all possible heroes.
    /// </summary>
    [DataContract]
    public class HeroProvider : IHeroProvider
    {
        #region Class Methods

        /// <summary>
        ///     Loads a hero provider from the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>The loaded hero provider.</returns>
        public static HeroProvider LoadFrom(Stream stream)
        {
            var dataContractSerializer = new DataContractSerializer(typeof(HeroProvider));

            HeroProvider heroProvider;

            using (XmlReader reader = new XmlTextReader(stream))
            {
                heroProvider = (HeroProvider)dataContractSerializer.ReadObject(reader);
            }

            return heroProvider;
        }

        #endregion

        #region IHeroProvider Members

        /// <summary>
        ///     Gets the heroes.
        /// </summary>
        /// <value>
        ///     The heroes.
        /// </value>
        [DataMember(Name = "Heroes", IsRequired = true)]
        public List<Hero> Heroes { get; set; }

        /// <summary>
        ///     Finds the <see cref="Hero" /> with the specified ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>
        ///     The <see cref="Hero" /> with the specified ID or the default if no such hero could be found.
        /// </returns>
        public Hero Find(string id)
        {
            List<Hero> found = Heroes.Where(hero => hero.ID == id).ToList();

            return (found.Count == 1) ? found[0] : Hero.None;
        }

        #endregion
    }
}