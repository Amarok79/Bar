/* MIT License
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

using System;
using System.Linq;
using System.Threading.Tasks;
using Drinks.Model;
using NFluent;
using NUnit.Framework;


namespace Drinks.Services.DrinkRepository
{
	[TestFixture]
	public class Test_AzureBlobDrinkRepository
	{
		[Test]
		public async Task GetAll()
		{
			var barId = new BarId(new Guid("A8A4E6C2-2B7D-41EE-8B3D-2053D74FAD67"));

			var repo = new AzureBlobDrinkRepository();

			var drinks = (await repo.GetAll(barId))
				.ToList();

			Check.That(drinks.Count)
				.IsStrictlyGreaterThan(10);

			foreach (var drink in drinks)
			{
				Check.That(drink.Id)
					.IsNotEqualTo(Guid.Empty);
				Check.That(drink.BarId.Guid)
					.IsEqualTo(new Guid("A8A4E6C2-2B7D-41EE-8B3D-2053D74FAD67"));
				Check.That(drink.Name)
					.Not.IsEmpty();
				Check.That(drink.Teaser)
					.Not.IsEmpty();
				Check.That(drink.Description)
					.IsNotNull();
			}
		}

		[Test]
		public async Task GetAll_Alexander()
		{
			var barId = new BarId(new Guid("A8A4E6C2-2B7D-41EE-8B3D-2053D74FAD67"));

			var repo = new AzureBlobDrinkRepository();

			var drink = (await repo.GetAll(barId))
				.SingleOrDefault(x => x.Name.Equals("Alexander"));

			Check.That(drink)
				.IsNotNull();

			Check.That(drink.Id)
				.IsNotEqualTo(Guid.Empty);
			Check.That(drink.BarId.Guid)
				.IsEqualTo(new Guid("A8A4E6C2-2B7D-41EE-8B3D-2053D74FAD67"));
			Check.That(drink.Name)
				.IsEqualTo("Alexander");
			Check.That(drink.Teaser)
				.IsEqualTo("Gin, Crème de Cacao, Sahne, Muskatnuss");
			Check.That(drink.ImageId.Guid)
				.IsEqualTo(new Guid("{8AFFEC5A-A659-460F-8BE4-438C1F17F638}"));
			Check.That(drink.Tags)
				.ContainsExactly("sweet", "creamy");
			Check.That(drink.Description)
				.Contains(
					"Der Alexander ist ein alkoholischer Sahne-Cocktail",
					"Der Cocktail entstand Anfang des 20. Jahrhunderts, eine",
					"(Wikipedia)"
				);
			Check.That(drink.Glass)
				.IsEqualTo("Martini");
			Check.That(drink.Ice)
				.IsEqualTo("None");
			Check.That(drink.Garnish)
				.IsEqualTo("Muskatnuss");

			Check.That(drink.Recipe.Ingredients)
				.HasSize(4);

			Check.That(drink.Recipe.Ingredients[0].Amount.Value)
				.IsEqualTo(3);
			Check.That(drink.Recipe.Ingredients[0].Unit)
				.IsEqualTo("cl");
			Check.That(drink.Recipe.Ingredients[0].Substance)
				.IsEqualTo("Beefeater 24 London Dry Gin");

			Check.That(drink.Recipe.Ingredients[1].Amount.Value)
				.IsEqualTo(3);
			Check.That(drink.Recipe.Ingredients[1].Unit)
				.IsEqualTo("cl");
			Check.That(drink.Recipe.Ingredients[1].Substance)
				.IsEqualTo("Crème de Cacao Blanc");

			Check.That(drink.Recipe.Ingredients[2].Amount.Value)
				.IsEqualTo(3);
			Check.That(drink.Recipe.Ingredients[2].Unit)
				.IsEqualTo("cl");
			Check.That(drink.Recipe.Ingredients[2].Substance)
				.IsEqualTo("Sahne");

			Check.That(drink.Recipe.Ingredients[3].Amount)
				.IsNull();
			Check.That(drink.Recipe.Ingredients[3].Unit)
				.IsNull();
			Check.That(drink.Recipe.Ingredients[3].Substance)
				.IsEqualTo("Muskatnuss");

			Check.That(drink.Recipe.Instructions)
				.HasSize(3);
			Check.That(drink.Recipe.Instructions[0])
				.IsEqualTo("SHAKE alle Zutaten");
			Check.That(drink.Recipe.Instructions[1])
				.IsEqualTo("FINE STRAIN ins Gästeglas");
			Check.That(drink.Recipe.Instructions[2])
				.IsEqualTo("DUST mit Muskatnuss");
		}
	}
}
