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
using Unity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;


namespace Drinks.Viewer
{
	public sealed partial class MainPage : Page
	{
		public MainPageViewModel ViewModel { get; } = new MainPageViewModel();

		[Dependency]
		public IDrinkRepository DrinkRepository { get; set; }
		[Dependency]
		public IImageRepository ImageRepository { get; set; }


		public MainPage()
		{
			this.InitializeComponent();
			this.Loading += _HandleOnLoading;
		}

		private async void _HandleOnLoading(FrameworkElement sender, Object args)
		{
			try
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

					var item = new DrinkItemViewModel {
						Name = drink.Name,
						Teaser = drink.Teaser,
						Image = image,
						IsImageLoading = Visibility.Visible,
					};

					ViewModel.Drinks.Add(item);

					image.ImageFailed += (_sender, _e) =>
					{
						image.UriSource = new Uri("ms-appx:///Assets/{00000000-0000-0000-0000-000000000000}.jpg");
					};

					image.ImageOpened += (_sender, _e) =>
					{
						item.IsImageLoading = Visibility.Collapsed;
					};
				}
			}
			catch (Exception exception)
			{
				// TODO
			}
		}
	}
}
