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

using NFluent;
using NUnit.Framework;


namespace Drinks.Model
{
	[TestFixture]
	public class Test_Recipe
	{
		[Test]
		public void Construction()
		{
			var ing1 = new Ingredient(5, "cl", "Light Rum");
			var ing2 = new Ingredient(2, "tea spoon", "Sugar");

			var rec = new Recipe(
				new[] { ing1, ing2 },
				new[] { "Shake all", "Fine Grain" }
			);

			Check.That(rec.Ingredients[0].Amount)
				.IsEqualTo(5);
			Check.That(rec.Ingredients[0].Unit)
				.IsEqualTo("cl");
			Check.That(rec.Ingredients[0].Substance)
				.IsEqualTo("Light Rum");

			Check.That(rec.Ingredients[1].Amount)
				.IsEqualTo(2);
			Check.That(rec.Ingredients[1].Unit)
				.IsEqualTo("tea spoon");
			Check.That(rec.Ingredients[1].Substance)
				.IsEqualTo("Sugar");

			Check.That(rec.Instructions)
				.ContainsExactly(
					"Shake all",
					"Fine Grain"
				);

			Check.That(rec.ToString())
				.Contains(
					"5 cl Light Rum",
					"2 tea spoon Sugar",
					"Shake all",
					"Fine Grain"
				);
		}
	}
}
