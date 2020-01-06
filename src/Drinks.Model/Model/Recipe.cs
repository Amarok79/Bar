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
using System.Collections.Generic;
using System.Text;
using Amarok.Contracts;


namespace Drinks.Model
{
	/// <summary>
	/// This type represents a Recipe for a Drink.
	/// 
	/// A Recipe consists of a list of Ingredients and a list of Instructions.
	/// </summary>
	public sealed class Recipe
	{
		/// <summary>
		/// A list of Ingredients needed for this Recipe.
		/// </summary>
		public IReadOnlyList<Ingredient> Ingredients { get; }


		/// <summary>
		/// Initializes a new instance.
		/// </summary>
		public Recipe(IReadOnlyList<Ingredient> ingredients)
		{
			Verify.NotNull(ingredients, nameof(ingredients));
			this.Ingredients = ingredients;
		}


		/// <summary>
		/// Returns a string that represents the current instance.
		/// </summary>
		///
		/// <returns>
		/// A string that represents the current instance.</returns>
		public override String ToString()
		{
			var sb = new StringBuilder();
			foreach (var ingredient in this.Ingredients)
			{
				if (sb.Length > 0)
					sb.Append(", ");
				sb.Append(ingredient.ToString());
			}
			return sb.ToString();
		}
	}
}
