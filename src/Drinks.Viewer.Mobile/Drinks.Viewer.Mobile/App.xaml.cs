using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Drinks.Viewer.Mobile.Services;
using Drinks.Viewer.Mobile.Views;

namespace Drinks.Viewer.Mobile
{
	public partial class App : Application
	{

		public App()
		{
			InitializeComponent();

			DependencyService.Register<MockDataStore>();
			MainPage = new AppShell();
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
