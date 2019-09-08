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
				var drinks = repository.GetAll();

				foreach(var drink in drinks)
				{
					var imagePath = Path.Combine(assetsDirectory.Path, drink.Image);
					var image = new BitmapImage(new Uri(imagePath));
					var viewModel = (MainPageViewModel)this.DataContext;

					viewModel.Cocktails.Add(new Cocktail {
						Name = drink.Name,
						Description = drink.Teaser,
						Image = image,
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
