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
using System.Windows.Input;
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
		private ICommand mCloseButtonCommand;


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

		public BitmapImage Image
		{
			get => mImage;
			set => Set(ref mImage, value);
		}

		public ICommand CloseButtonCommand
		{
			get => mCloseButtonCommand;
			set => Set(ref mCloseButtonCommand, value);
		}
	}

	public class SampleDrinkDetailPageViewModel : DrinkDetailPageViewModel
	{
		public SampleDrinkDetailPageViewModel()
		{
			var recipe = new Recipe(new[] {
					new Ingredient(3, "cl", "Campari"),
					new Ingredient(3, "cl", "Vermouth Rosso"),
					new Ingredient(3, "cl", "London Dry Gin"),
				});

			var drink = new Drink(default, default)
				.SetDescription(
@"Der Cosmopolitan (kurz „Cosmo“) ist ein herb-süßer, erfrischender Cocktail, dessen moderne Rezeptur aus aromatisiertem Wodka, Orangenlikör, Limetten- und Cranberrysaft besteht. Erstmals erwähnt wird ein Shortdrink dieses Namens in einem Barbuch von 1934, in seiner heutigen Form ist der Cosmopolitan aber erst seit den 1990er Jahren weltweit verbreitet. Er steht sinnbildlich für die moderne, design-orientierte Bar, die der Barboom der 1990er Jahre hervorbrachte.

Ende der 1990er Jahre erlangte der Cosmopolitan einen Bekanntheitsschub durch die Fernsehserie Sex and the City, in der er das Lieblingsgetränk der Hauptfiguren war.

(Wikipedia)")
				.SetTeaser("Wodka, Triple Sec, Cranberrysaft, Limettensaft")
				.SetName("Sample Drink")
				.SetRecipe(recipe);

			this.Drink = drink;
			this.Image = new BitmapImage(new Uri("ms-appx:///Assets/{00000000-0000-0000-0000-000000000000}.jpg"));
		}
	}
}
