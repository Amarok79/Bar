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
using System.IO;
using System.Linq;
using Drinks.Services.DrinkRepository;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Drinks.Viewer
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page
	{
		public MainPage()
		{
			this.InitializeComponent();

			this.Loading += this.MainPage_Loading;
		}

		private async void MainPage_Loading(FrameworkElement sender, Object args)
		{
			try
			{
				var assetsDirectory = await Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
				var repository = new LocalDrinkRepository(assetsDirectory.Path);
				var drinks = await repository.GetAll();

				foreach(var drink in drinks)
				{
					//var imagePath = Path.Combine(assetsDirectory.Path, drink.Image);
					//var image = new BitmapImage(new Uri(imagePath));
					var viewModel = (MainPageViewModel)this.DataContext;

					viewModel.Cocktails.Add(new Drink {
						Name = drink.Name,
						Teaser = drink.Teaser,
						//Image = image,
					});
				}
			}
			catch (Exception exception)
			{
				// TODO
			}
		}
	}
}
