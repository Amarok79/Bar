/* MIT License
 * 
 * Copyright (c) 2019, Olaf Kober
 * https://bitbucket.org/Amarok/bar
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using Drinks.Model;


namespace Drinks.Services.DrinkRepository
{
	/// <summary>
	/// An implementation that loads drinks from a local manifest.
	/// </summary>
	public sealed class LocalDrinkRepository :
		IDrinkRepository
	{
		// data
		private readonly String mAssetsDirectory;


		/// <summary>
		/// Initializes a new instance.
		/// </summary>
		public LocalDrinkRepository(String assetsDirectory)
		{
			mAssetsDirectory = assetsDirectory;
		}


		/// <summary>
		/// Gets all drinks.
		/// </summary>
		public Task<IEnumerable<Drink>> GetAll()
		{
			var manifestPath = Path.Combine(mAssetsDirectory, "drinks.xml");
			var doc = XDocument.Load(manifestPath);

			var drinks = new List<Drink>();
			foreach (var drinkNode in doc.Element("drinks").Elements("drink"))
			{
				var name = drinkNode.Element("name").Value;
				var teaser = drinkNode.Element("teaser").Value;
				var image = Guid.Parse(drinkNode.Element("image").Value);

				var drink = new Drink(new DrinkId(Guid.NewGuid()), new BarId())
					.SetName(name)
					.SetTeaser(teaser)
					.SetImage(new ImageId(image));

				drinks.Add(drink);
			}

			return Task.FromResult<IEnumerable<Drink>>(drinks);
		}
	}
}
