﻿/* MIT License
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

#pragma warning disable S1075 // URIs should not be hardcoded
#pragma warning disable CRR0033 // The void async method should be in a try/catch block

using System;
using System.Linq;
using Drinks.Model;
using Drinks.Services;
using Drinks.Viewer.DrinkDetail;
using Unity;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;


namespace Drinks.Viewer.Home
{
	public sealed partial class HomePage : Page
	{
		[Dependency]
		public INavigationService NavigationService { get; set; }
		[Dependency]
		public IDrinkRepository DrinkRepository { get; set; }
		[Dependency]
		public IImageRepository ImageRepository { get; set; }

		public UiHomePage ViewModel => (UiHomePage)this.DataContext;


		public HomePage()
		{
			App.Current.Container.BuildUp(typeof(HomePage), this);

			var viewModel = new UiHomePage();
			_InitializeStyles(viewModel);
			this.DataContext = viewModel;

			this.InitializeComponent();
		}

		private void _InitializeStyles(UiHomePage viewModel)
		{
			viewModel.Styles.Add(new UiDrinkStyle { Id = "ALL", Name = "Alle" });
			viewModel.Styles.Add(new UiDrinkStyle { Id = "SOUR", Name = "Sauer-Süß" });
			viewModel.Styles.Add(new UiDrinkStyle { Id = "BITTER", Name = "Bitter-Süß" });
			viewModel.Styles.Add(new UiDrinkStyle { Id = "CREAMY", Name = "Cremig-Süß" });
			viewModel.Styles.Add(new UiDrinkStyle { Id = "EXOTIC", Name = "Exotisch-Tropisch" });
			viewModel.Styles.Add(new UiDrinkStyle { Id = "STRONG", Name = "Stark" });
			viewModel.SelectedStyle = viewModel.Styles.FirstOrDefault();
		}


		protected override async void OnNavigatedTo(NavigationEventArgs e)
		{
			if (e.NavigationMode != NavigationMode.New)
				return;

			var drinks = await this.DrinkRepository.GetAll()
				.ConfigureAwait(true);

			foreach (var drink in drinks.OrderBy(x => x.Name))
			{
				var imageUri = await this.ImageRepository.GetById(drink.ImageId)
					.ConfigureAwait(true);

				var image = new BitmapImage();
				image.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
				image.UriSource = imageUri;

				var item = new UiDrink {
					Drink = drink,
					Image = image,
					IsImageLoading = true,
				};

				((UiHomePage)DataContext).Drinks.Add(item);

				image.ImageFailed += (_sender, _e) =>
				{
					image.UriSource = new Uri("ms-appx:///Assets/{00000000-0000-0000-0000-000000000000}.jpg");
					item.IsImageLoading = false;
				};

				image.ImageOpened += (_sender, _e) =>
				{
					item.IsImageLoading = false;
				};
			}

			((UiHomePage)DataContext).DrinksView.Source = ((UiHomePage)DataContext).Drinks.ToList();
		}

		private void _HandleDrinkItemClick(Object sender, ItemClickEventArgs e)
		{
			var drinkViewModel = (UiDrink)e.ClickedItem;
			var drink = drinkViewModel.Drink;

			if (drink.Recipe == null)
				return;

			NavigationService.Navigate(
				typeof(DrinkDetailPage),
				new DrinkDetailPageArgs(drink, drinkViewModel.Image),
				new DrillInNavigationTransitionInfo()
			);
		}

		private void _HandleDrinkStyleSelectionChanged(Object sender, SelectionChangedEventArgs e)
		{
			var vm = this.ViewModel;

			var validTags = getValidTags(vm.SelectedStyle.Id);

			if (validTags == null)
			{
				vm.DrinksView.Filter = x => true;
			}
			else
			{
				vm.DrinksView.Filter = x =>
				{
					var drink = (UiDrink)x;

					foreach (var validTag in validTags)
					{
						if (drink.Drink.Tags.Contains(validTag, StringComparer.OrdinalIgnoreCase))
							return true;
					}

					return false;
				};
			}

			vm.DrinksView.RefreshFilter();


			String[] getValidTags(String id)
			{
				switch (id)
				{
					case "SOUR":
						return new[] { "sour" };
					case "BITTER":
						return new[] { "bitter", "herbal" };
					case "CREAMY":
						return new[] { "creamy" };
					case "EXOTIC":
						return new[] { "exotic", "tropical" };
					case "STRONG":
						return new[] { "strong" };
					case "ALL":
					default:
						return null;
				}
			}
		}
	}
}
