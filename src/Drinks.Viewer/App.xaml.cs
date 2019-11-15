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

using System;
using Drinks.Model;
using Drinks.Services;
using Drinks.Services.DrinkRepository;
using Drinks.Services.ImageRepository;
using Drinks.Viewer.Home;
using Unity;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;


namespace Drinks.Viewer
{
	public sealed partial class App : Application,
		INavigationService
	{
		public IUnityContainer Container { get; }

		public Frame Frame { get; private set; }

		public static new App Current => (App)Application.Current;


		public App()
		{
			this.Container = new UnityContainer();
			this.InitializeComponent();
		}

		protected override void OnLaunched(LaunchActivatedEventArgs args)
		{
			base.OnLaunched(args);

			_RegisterServices(this.Container, this);

			Frame = new Frame();
			Frame.Navigate(typeof(HomePage), null, new EntranceNavigationTransitionInfo());

			Window.Current.Content = Frame;
			Window.Current.Activate();
		}


		private static void _RegisterServices(IUnityContainer container, App me)
		{
			container.RegisterSingleton<IImageRepository, AzureImageRepository>();
			container.RegisterSingleton<IDrinkRepository, AzureBlobDrinkRepository>();
			container.RegisterInstance<INavigationService>(me);
		}

		public Boolean Navigate(Type pageType, Object args, NavigationTransitionInfo transitionInfo)
		{
			return this.Frame.Navigate(pageType, args, transitionInfo);
		}

		public Boolean GoBack()
		{
			if (this.Frame.CanGoBack)
			{
				this.Frame.GoBack();
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
