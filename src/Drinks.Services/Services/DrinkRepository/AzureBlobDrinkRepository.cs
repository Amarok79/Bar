﻿/* MIT License
 * 
 * Copyright (c) 2019, Olaf Kober
 * https://github.com/Amarok79/Bar
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
using System.Text;
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
				var desc = drinkNode.Element("description")?.Value;
				var tags = drinkNode.Element("tags")?.Value;

				var drink = new Drink(new DrinkId(Guid.NewGuid()), new BarId())
					.SetName(name)
					.SetTeaser(teaser)
					.SetImage(new ImageId(image))
					.SetDescription(_TrimDescription(desc))
					.SetTags(_SplitAndTrimTags(tags));

				var recipeNode = drinkNode.Element("recipe");

				if (recipeNode != null)
				{
					var ingredients = recipeNode.Elements("ingredient")
						.Select(x =>
						{
							var amount = x.Attribute("amount")?.Value;
							var unit = x.Attribute("unit")?.Value;
							var substance = x.Attribute("substance").Value;

							if (amount == null && unit == null)
								return new Ingredient(substance);
							else
								return new Ingredient(
									Double.Parse(amount, CultureInfo.InvariantCulture),
									unit,
									substance);
						})
						.ToList();

					drink.SetRecipe(new Recipe(ingredients, Array.Empty<String>()));	// TODO
				}

				drinks.Add(drink);
			}

			return drinks;
		}

		private static String _TrimDescription(String text)
		{
			if (text == null)
				return String.Empty;

			var lines = text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

			var sb = new StringBuilder();
			foreach (var line in lines)
			{
				if (sb.Length > 0)
					sb.AppendLine();

				sb.AppendLine(line.Trim());
			}

			return sb.ToString();
		}

		private static String[] _SplitAndTrimTags(String text)
		{
			if (text == null)
				return Array.Empty<String>();

			var tags = text
				.Split(new[] { '|', ';', ',' }, StringSplitOptions.RemoveEmptyEntries)
				.Select(x => x.Trim())
				.ToArray();

			return tags;
		}
	}
}
