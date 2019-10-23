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

#pragma warning disable S1075 // URIs should not be hardcoded
#pragma warning disable CRR0033 // The void async method should be in a try/catch block

using System;
using System.Linq;
using Drinks.Model;
using Drinks.Viewer.DrinkInfo;
using Unity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;


namespace Drinks.Viewer.Home
{
	public sealed partial class HomeView : Page
	{
		public HomeViewModel ViewModel { get; } = new HomeViewModel();


		[Dependency]
		public IDrinkRepository DrinkRepository { get; set; }
		[Dependency]
		public IImageRepository ImageRepository { get; set; }


		public HomeView()
		{
			this.InitializeComponent();
			this.Loading += _HandleOnLoading;
			this.DrinksGridView.ItemClick += _HandleDrinkItemClick;
		}


		private async void _HandleOnLoading(FrameworkElement sender, Object args)
		{
			var drinks = await this.DrinkRepository.GetAll()
				.ConfigureAwait(true);

			foreach (var drink in drinks.OrderBy(x => x.Name))
			{
				var imageUri = await this.ImageRepository.GetById(drink.ImageId)
					.ConfigureAwait(true);

				var image = new BitmapImage();
				image.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
				image.UriSource = imageUri;

				var item = new DrinkViewModel() {
					Drink = drink,
					Image = image,
					IsImageLoading = true,
				};

				ViewModel.Drinks.Add(item);

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
		}

		private void _HandleDrinkItemClick(Object sender, ItemClickEventArgs e)
		{
			var drinkViewModel = (DrinkViewModel)e.ClickedItem;
			var drink = drinkViewModel.Drink;

			if (drink.Recipe == null)
				return;

			var infoView = new DrinkInfoView();
			infoView.ViewModel.Drink = drink;
			infoView.ViewModel.Image = drinkViewModel.Image;
			infoView.ViewModel.CloseButtonCommand = new DelegateCommand(_HandleDrinkInfoPopupClose);

			infoView.Width = DrinksArea.ActualWidth - 240;
			infoView.Height = DrinksArea.ActualHeight - 120;

			DrinkInfoViewHost.Content = infoView;

			DrinkInfoPopup.IsOpen = true;
		}

		private void _HandleDrinkInfoPopupClose()
		{
			DrinkInfoPopup.IsOpen = false;
			DrinkInfoViewHost.Content = null;
		}
	}
}
