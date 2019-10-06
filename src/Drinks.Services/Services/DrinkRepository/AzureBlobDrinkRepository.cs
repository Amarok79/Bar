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

#pragma warning disable S1075 // URIs should not be hardcoded

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using Drinks.Model;


namespace Drinks.Services.DrinkRepository
{
	/// <summary>
	/// An implementation that loads drinks from a file downloaded from Azure.
	/// </summary>
	public sealed class AzureBlobDrinkRepository :
		IDrinkRepository
	{
		/// <summary>
		/// Gets all drinks.
		/// </summary>
		public async Task<IEnumerable<Drink>> GetAll()
		{
			String manifestPath = null;
			try
			{
				manifestPath = Path.GetTempFileName();

				await _DownloadManifest(manifestPath)
					.ConfigureAwait(false);

				return _LoadFromManifest(manifestPath);
			}
			finally
			{
				if (manifestPath != null)
					File.Delete(manifestPath);
			}
		}


		private async Task _DownloadManifest(String manifestPath)
		{
			const String __url = "https://amarok.blob.core.windows.net/drinks/drinks.xml";

			using (var client = new WebClient())
			{
				await client.DownloadFileTaskAsync(__url, manifestPath)
					.ConfigureAwait(false);
			}
		}

		private List<Drink> _LoadFromManifest(String manifestPath)
		{
			var doc = XDocument.Load(manifestPath);

			var drinks = new List<Drink>();
			foreach (var drinkNode in doc.Element("drinks").Elements("drink"))
			{
				var name = drinkNode.Element("name").Value;
				var teaser = drinkNode.Element("teaser").Value;
				var image = Guid.Parse(drinkNode.Element("image").Value);
				var desc = drinkNode.Element("description")?.Value?.Trim();

				var drink = new Drink(new DrinkId(Guid.NewGuid()), new BarId())
					.SetName(name)
					.SetTeaser(teaser)
					.SetImage(new ImageId(image))
					.SetDescription(desc ?? String.Empty);

				var recipeNode = drinkNode.Element("recipe");

				if (recipeNode != null)
				{
					var ingredients = recipeNode.Elements("ingredient")
						.Select(x => new Ingredient(
							Double.Parse(x.Attribute("amount").Value, CultureInfo.InvariantCulture),
							x.Attribute("unit").Value,
							x.Attribute("substance").Value
						))
						.ToList();

					drink.SetRecipe(new Recipe(ingredients));
				}

				drinks.Add(drink);
			}

			return drinks;
		}
	}
}
