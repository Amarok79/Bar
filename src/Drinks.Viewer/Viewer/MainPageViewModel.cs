using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media.Imaging;

namespace Drinks.Viewer
{
	public class MainPageViewModel
	{
		public ObservableCollection<Cocktail> Cocktails { get; set; } = new ObservableCollection<Cocktail>();


		public MainPageViewModel()
		{
			var image = new BitmapImage(new Uri("https://amarok.blob.core.windows.net/drinks/41E7903D-086C-4E4B-A4BA-869248A2DB25.jpg"));
			image.ImageOpened += this.Image_ImageOpened;
			image.ImageFailed += this.Image_ImageFailed;

			this.Cocktails.Add(new Cocktail {
				Name = "Mai Tai",
				Description = "Rum, Rum, Cointreau, Mandel, Muskatnuss",
				Image = image
			});

			this.Cocktails.Add(new Cocktail {
				Name = "Mai Tai",
				Description = "Rum, Rum, Cointreau, Mandel, Muskatnuss",
				Image = image
			});
			this.Cocktails.Add(new Cocktail {
				Name = "Mai Tai",
				Description = "Rum, Rum, Cointreau, Mandel, Muskatnuss",
				Image = image
			});
			this.Cocktails.Add(new Cocktail {
				Name = "Mai Tai",
				Description = "Rum, Rum, Cointreau, Mandel, Muskatnuss",
				Image = image
			});
			this.Cocktails.Add(new Cocktail {
				Name = "Mai Tai",
				Description = "Rum, Rum, Cointreau, Mandel, Muskatnuss",
				Image = image
			});
			this.Cocktails.Add(new Cocktail {
				Name = "Mai Tai",
				Description = "Rum, Rum, Cointreau, Mandel, Muskatnuss",
				Image = image
			});
			this.Cocktails.Add(new Cocktail {
				Name = "Mai Tai",
				Description = "Rum, Rum, Cointreau, Mandel, Muskatnuss",
				Image = image
			});
			this.Cocktails.Add(new Cocktail {
				Name = "Mai Tai",
				Description = "Rum, Rum, Cointreau, Mandel, Muskatnuss",
				Image = image
			});
			this.Cocktails.Add(new Cocktail {
				Name = "Mai Tai",
				Description = "Rum, Rum, Cointreau, Mandel, Muskatnuss",
				Image = image
			});
			this.Cocktails.Add(new Cocktail {
				Name = "Mai Tai",
				Description = "Rum, Rum, Cointreau, Mandel, Muskatnuss",
				Image = image
			});
			this.Cocktails.Add(new Cocktail {
				Name = "Mai Tai",
				Description = "Rum, Rum, Cointreau, Mandel, Muskatnuss",
				Image = image
			});
			this.Cocktails.Add(new Cocktail {
				Name = "Mai Tai",
				Description = "Rum, Rum, Cointreau, Mandel, Muskatnuss",
				Image = image
			});
			this.Cocktails.Add(new Cocktail {
				Name = "Mai Tai",
				Description = "Rum, Rum, Cointreau, Mandel, Muskatnuss",
				Image = image
			});
		}

		private void Image_ImageFailed(Object sender, Windows.UI.Xaml.ExceptionRoutedEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void Image_ImageOpened(Object sender, Windows.UI.Xaml.RoutedEventArgs e)
		{
			Console.WriteLine("OPEN");
		}
	}
}
