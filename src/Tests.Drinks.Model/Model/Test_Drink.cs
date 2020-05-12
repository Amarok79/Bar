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
using NFluent;
using NUnit.Framework;


namespace Drinks.Model
{
	[TestFixture]
	public class Test_Drink
	{
		[Test]
		public void Construction()
		{
			// arrange
			var drinkId = new DrinkId(Guid.NewGuid());
			var barId = new BarId(Guid.NewGuid());
			var drink = new Drink(drinkId, barId);
			var recipe = new Recipe(Array.Empty<Ingredient>(), Array.Empty<String>());

			// assert
			Check.That(drink.Id)
				.IsEqualTo(drinkId);
			Check.That(drink.BarId)
				.IsEqualTo(barId);
			Check.That(drink.Name)
				.IsNull();
			Check.That(drink.Teaser)
				.IsNull();
			Check.That(drink.Description)
				.IsNull();
			Check.That(drink.ImageId)
				.IsEqualTo(new ImageId());
			Check.That(drink.Recipe)
				.IsNull();
			Check.That(drink.Tags)
				.IsEmpty();

			// act
			drink
				.SetName("Mai Tai")
				.SetTeaser("Lime, Orgeat, Rum")
				.SetDescription("The Mai Tai is a cocktail based on rum...")
				.SetRecipe(recipe)
				.SetTags(new[] { "sweet", "exotic" });

			// assert
			Check.That(drink.Id)
				.IsEqualTo(drinkId);
			Check.That(drink.BarId)
				.IsEqualTo(barId);
			Check.That(drink.Name)
				.IsEqualTo("Mai Tai");
			Check.That(drink.Teaser)
				.IsEqualTo("Lime, Orgeat, Rum");
			Check.That(drink.Description)
				.IsEqualTo("The Mai Tai is a cocktail based on rum...");
			Check.That(drink.ImageId)
				.IsEqualTo(new ImageId());
			Check.That(drink.Recipe)
				.IsSameReferenceAs(recipe);
			Check.That(drink.Tags)
				.ContainsExactly("sweet", "exotic");
		}
	}
}
