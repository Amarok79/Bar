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
using System.Linq;
using Drinks.Model;
using Windows.UI.Xaml.Media.Imaging;


namespace Drinks.Viewer.DrinkDetail
{
	public class DrinkDetailPageViewModel : BindableBase
	{
		// state
		private IngredientViewModel[] mIngredients;
		private Drink mDrink;
		private BitmapImage mImage;


		public BitmapImage Image
		{
			get => mImage;
			set => Set(ref mImage, value);
		}

		public Drink Drink
		{
			get
			{
				return mDrink;
			}
			set
			{
				mDrink = value;

				mIngredients = mDrink.Recipe.Ingredients
					.Select(x => new IngredientViewModel() { Ingredient = x })
					.ToArray();

				OnPropertyChanged(nameof(Name));
				OnPropertyChanged(nameof(Teaser));
				OnPropertyChanged(nameof(Description));
				OnPropertyChanged(nameof(Ingredients));
			}
		}

		public String Name => this.Drink.Name;

		public String Teaser => this.Drink.Teaser;

		public String Description => this.Drink.Description;

		public IngredientViewModel[] Ingredients => mIngredients;
	}
}
