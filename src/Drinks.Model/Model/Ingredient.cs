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
using Amarok.Contracts;


namespace Drinks.Model
{
	/// <summary>
	/// This type represents a single ingredient in a recipe, e.g. "5 cl Light Rum".
	/// </summary>
	public sealed class Ingredient
	{
		/// <summary>
		/// Gets the amount of the ingredient, e.g. "5".
		/// </summary>
		public Double Amount { get; }

		/// <summary>
		/// Gets the unit used for the amount of the ingredient, e.g. "cl".
		/// </summary>
		public String Unit { get; }

		/// <summary>
		/// Gets the name of the ingredient, e.g. "Light Rum".
		/// </summary>
		public String Substance { get; }


		/// <summary>
		/// Initializes a new instance.
		/// </summary>
		public Ingredient(Double amount, String unit, String substance)
		{
			Verify.IsPositive(amount, nameof(amount));
			Verify.NotNull(unit, nameof(unit));
			Verify.NotEmpty(substance, nameof(substance));

			this.Amount = amount;
			this.Unit = unit;
			this.Substance = substance;
		}


		/// <summary>
		/// Returns a string that represents the current instance.
		/// </summary>
		///
		/// <returns>
		/// A string that represents the current instance.</returns>
		public override String ToString()
		{
			return $"{Amount:F1} {Unit} {Substance}";
		}
	}
}
