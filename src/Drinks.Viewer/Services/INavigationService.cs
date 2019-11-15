using System;
using Windows.UI.Xaml.Media.Animation;


namespace Drinks.Services
{
	public interface INavigationService
	{
		Boolean Navigate(Type pageType, Object args, NavigationTransitionInfo transitionInfo);

		Boolean GoBack();
	}
}
