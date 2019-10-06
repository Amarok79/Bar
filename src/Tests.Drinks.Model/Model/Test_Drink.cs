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
			var recipe = new Recipe(Array.Empty<Ingredient>());

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

			Check.That(drink.Validate().IsValid)
				.IsFalse();

			// act
			drink
				.SetName("Mai Tai")
				.SetTeaser("Lime, Orgeat, Rum")
				.SetDescription("The Mai Tai is a cocktail based on rum...")
				.SetRecipe(recipe);

			// assert
			Check.That(drink.Validate().IsValid)
				.IsTrue();
		}

		[Test]
		public void Name()
		{
			// arrange
			var drinkId = new DrinkId(Guid.NewGuid());
			var barId = new BarId(Guid.NewGuid());
			var drink = new Drink(drinkId, barId);

			drink
				.SetTeaser("Lime, Orgeat, Rum")
				.SetDescription("The Mai Tai is a cocktail based on rum...");

			// act / assert
			Check.ThatCode(() => drink.SetName(null))
				.ThrowsAny();
			Check.ThatCode(() => drink.SetName(""))
				.ThrowsAny();

			drink.SetName("Mai Tai");

			Check.That(drink.Validate().IsValid)
				.IsTrue();

			drink.SetName(new String('A', 256));

			Check.That(drink.Validate().IsValid)
				.IsTrue();

			drink.SetName(new String('A', 257));

			Check.That(drink.Validate().IsValid)
				.IsFalse();
		}

		[Test]
		public void Teaser()
		{
			// arrange
			var drinkId = new DrinkId(Guid.NewGuid());
			var barId = new BarId(Guid.NewGuid());
			var drink = new Drink(drinkId, barId);

			drink
				.SetName("Mai Tai")
				.SetDescription("The Mai Tai is a cocktail based on rum...");

			// act / assert
			Check.ThatCode(() => drink.SetTeaser(null))
				.ThrowsAny();

			drink.SetTeaser("");

			Check.That(drink.Validate().IsValid)
				.IsTrue();

			drink.SetTeaser("Lime, Orgeat, Rum");

			Check.That(drink.Validate().IsValid)
				.IsTrue();

			drink.SetTeaser(new String('A', 256));

			Check.That(drink.Validate().IsValid)
				.IsTrue();

			drink.SetTeaser(new String('A', 257));

			Check.That(drink.Validate().IsValid)
				.IsFalse();
		}

		[Test]
		public void Description()
		{
			// arrange
			var drinkId = new DrinkId(Guid.NewGuid());
			var barId = new BarId(Guid.NewGuid());
			var drink = new Drink(drinkId, barId);

			drink
				.SetName("Mai Tai")
				.SetTeaser("Lime, Orgeat, Rum");

			// act / assert
			Check.ThatCode(() => drink.SetDescription(null))
				.ThrowsAny();

			drink.SetDescription("");

			Check.That(drink.Validate().IsValid)
				.IsTrue();

			drink.SetDescription("The Mai Tai is a cocktail based on rum...");

			Check.That(drink.Validate().IsValid)
				.IsTrue();

			drink.SetDescription(new String('A', 5000));

			Check.That(drink.Validate().IsValid)
				.IsTrue();

			drink.SetDescription(new String('A', 5001));

			Check.That(drink.Validate().IsValid)
				.IsFalse();
		}

		[Test]
		public void ImageId()
		{
			// arrange
			var drinkId = new DrinkId(Guid.NewGuid());
			var barId = new BarId(Guid.NewGuid());
			var drink = new Drink(drinkId, barId);

			drink
				.SetName("Mai Tai")
				.SetTeaser("Lime, Orgeat, Rum")
				.SetDescription("The Mai Tai is a cocktail based on rum...");

			// act / assert
			drink.SetImage(default);

			Check.That(drink.Validate().IsValid)
				.IsTrue();

			drink.SetImage(new ImageId(Guid.NewGuid()));
		}
	}
}
