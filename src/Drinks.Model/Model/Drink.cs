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
	/// This type represents a Drink.
	/// 
	/// A Drink belongs to a single Bar. A Drink contains information like a simple Name, e.g. "Mai Tai",
	/// a so called Teaser text representing the main ingredients, e.g. "Lime, Orgeat, Rum", a photo, and
	/// a longer Description text providing interesting information about the drink.
	/// </summary>
	public sealed class Drink : Entity<DrinkId>
	{
		/// <summary>
		/// Gets the Id of the Bar this Drink belongs to.
		/// </summary>
		public BarId BarId { get; }

		/// <summary>
		/// Gets the Name of the Drink, e.g. "Mai Tai".
		/// </summary>
		public String Name { get; private set; }

		/// <summary>
		/// Gets the Teaser of the Drink, e.g. "Lime, Orgeat, Rum".
		/// </summary>
		public String Teaser { get; private set; }

		/// <summary>
		/// Gets the Description of the Drink, e.g. "The Mai Tai is a cocktail based on rum...".
		/// </summary>
		public String Description { get; private set; }

		/// <summary>
		/// Gets the Id of the Image of the Drink.
		/// </summary>
		public ImageId ImageId { get; private set; }

		/// <summary>
		/// Gets the Recipe of the Drink.
		/// </summary>
		public Recipe Recipe { get; private set; }


		/// <summary>
		/// Initializes a new instance.
		/// </summary>
		public Drink(DrinkId drinkId, BarId barId) :
			base(drinkId)
		{
			this.BarId = barId;
		}


		/// <summary>
		/// Sets the Name of the Drink.
		/// </summary>
		/// 
		/// <param name="name">
		/// The name of the drink. Neither null nor empty strings are allowed.</param>
		public Drink SetName(String name)
		{
			Verify.NotEmpty(name, nameof(name));
			this.Name = name;
			return this;
		}

		/// <summary>
		/// Sets the Teaser of the Drink.
		/// </summary>
		/// 
		/// <param name="teaser">
		/// The teaser of the drink. Null is not allowed.</param>
		public Drink SetTeaser(String teaser)
		{
			Verify.NotNull(teaser, nameof(teaser));
			this.Teaser = teaser;
			return this;
		}

		/// <summary>
		/// Sets the Description of the Drink.
		/// </summary>
		/// 
		/// <param name="description">
		/// The description of the drink. Null is not allowed.</param>
		public Drink SetDescription(String description)
		{
			Verify.NotNull(description, nameof(description));
			this.Description = description;
			return this;
		}

		/// <summary>
		/// Sets the Id of the Image of the Drink.
		/// </summary>
		/// 
		/// <param name="image">
		/// The image of the drink.</param>
		public Drink SetImage(ImageId image)
		{
			this.ImageId = image;
			return this;
		}

		/// <summary>
		/// Sets the Recipe of the Drink.
		/// </summary>
		/// 
		/// <param name="recipe">
		/// The Recipe of the Drink. Null is not allowed.</param>
		public Drink SetRecipe(Recipe recipe)
		{
			Verify.NotNull(recipe, nameof(recipe));
			this.Recipe = recipe;
			return this;
		}
	}
}
