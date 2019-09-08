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
		}
	}
}
