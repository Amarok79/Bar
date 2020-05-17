﻿/* MIT License
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
using Drinks.Model;
using Windows.UI.Xaml.Media.Imaging;


namespace Drinks.Viewer.DrinkDetail
{
	public class UiDrinkDetailPage : BindableBase
	{
		// state
		private UiIngredient[] mIngredients;
		private UiInstruction[] mInstructions;
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
					.Select(x => new UiIngredient { Ingredient = x })
					.ToArray();

				Int32 no = 1;
				mInstructions = mDrink.Recipe.Instructions
					.Select(x => new UiInstruction { No = no++, Text = x })
					.ToArray();

				OnPropertyChanged(nameof(Name));
				OnPropertyChanged(nameof(Teaser));
				OnPropertyChanged(nameof(Description));
				OnPropertyChanged(nameof(Ingredients));
				OnPropertyChanged(nameof(Instructions));
			}
		}

		public String Name => this.Drink.Name;

		public String Teaser => this.Drink.Teaser;


		public Boolean HasDescription => !String.IsNullOrEmpty(this.Drink.Description);

		public String DescriptionHeader => "Beschreibung";

		public String Description => this.Drink.Description;


		public String IngredientsHeader => "Rezeptur";

		public UiIngredient[] Ingredients => mIngredients;


		public Boolean HasInstructions => this.Drink.Recipe.Instructions.Any();

		public String InstructionsHeader => "Arbeitsschritte";

		public UiInstruction[] Instructions => mInstructions;


		public Boolean HasGlass => !String.IsNullOrEmpty(this.Drink.Glass);

		public String GlassHeader => "Glas";

		public String Glass => this.Drink.Glass;


		public Boolean HasIce => !String.IsNullOrEmpty(this.Drink.Ice);

		public String IceHeader => "Eis";

		public String Ice => this.Drink.Ice;
	}
}
