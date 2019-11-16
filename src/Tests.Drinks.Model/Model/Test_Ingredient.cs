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

using NFluent;
using NUnit.Framework;


namespace Drinks.Model
{
	[TestFixture]
	public class Test_Ingredient
	{
		[Test]
		public void Construction()
		{
			var ing = new Ingredient(5, "cl", "Light Rum");

			Check.That(ing.Amount)
				.IsEqualTo(5.0);
			Check.That(ing.Unit)
				.IsEqualTo("cl");
			Check.That(ing.Substance)
				.IsEqualTo("Light Rum");

			Check.That(ing.ToString())
				.IsEqualTo("5 cl Light Rum");
		}

		[Test]
		public void Construction_WithoutAmountAndUnit()
		{
			var ing = new Ingredient("Muskatnuss");

			Check.That(ing.Amount)
				.IsEqualTo(null);
			Check.That(ing.Unit)
				.IsEqualTo(null);
			Check.That(ing.Substance)
				.IsEqualTo("Muskatnuss");

			Check.That(ing.ToString())
				.IsEqualTo("Muskatnuss");
		}
	}
}
