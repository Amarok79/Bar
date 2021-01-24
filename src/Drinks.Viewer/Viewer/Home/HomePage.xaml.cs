/* MIT License
 * 
 * Copyright (c) 2019, Olaf Kober
 * https://github.com/Amarok79/Bar
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

#pragma warning disable S1075   // URIs should not be hardcoded
#pragma warning disable CRR0033 // The void async method should be in a try/catch block

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Drinks.Model;
using Drinks.Services;
using Drinks.Viewer.DrinkDetail;
using Unity;


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

        public UiHomePage ViewModel => (UiHomePage) this.DataContext;


        public HomePage()
        {
            App.Current.Container.BuildUp(typeof(HomePage), this);

            var viewModel = new UiHomePage();
            _InitializeStyles(viewModel);
            this.DataContext = viewModel;

            InitializeComponent();
        }

        private void _InitializeStyles(UiHomePage viewModel)
        {
            viewModel.Styles.Add(new UiDrinkStyle { Id = "ALL", Name    = "Alle" });
            viewModel.Styles.Add(new UiDrinkStyle { Id = "SOUR", Name   = "Sauer" });
            viewModel.Styles.Add(new UiDrinkStyle { Id = "SWEET", Name  = "Süß" });
            viewModel.Styles.Add(new UiDrinkStyle { Id = "BITTER", Name = "Bitter" });
            viewModel.Styles.Add(new UiDrinkStyle { Id = "FRUITY", Name = "Fruchtig" });
            viewModel.Styles.Add(new UiDrinkStyle { Id = "DRY", Name    = "Trocken" });
            viewModel.Styles.Add(new UiDrinkStyle { Id = "FRESH", Name  = "Erfrischend" });
            viewModel.Styles.Add(new UiDrinkStyle { Id = "CREAMY", Name = "Cremig" });
            viewModel.Styles.Add(new UiDrinkStyle { Id = "SPICY", Name  = "Würzig" });
            viewModel.Styles.Add(new UiDrinkStyle { Id = "1STAR", Name  = "1+ Sterne Bewertung" });
            viewModel.Styles.Add(new UiDrinkStyle { Id = "2STAR", Name  = "2+ Sterne Bewertung" });
            viewModel.Styles.Add(new UiDrinkStyle { Id = "3STAR", Name  = "3 Sterne Bewertung" });
            viewModel.SelectedStyle = viewModel.Styles.FirstOrDefault();
        }


        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode != NavigationMode.New)
                return;

            await _Loading().ConfigureAwait(false);
        }


        private async Task _Loading()
        {
            try
            {
                ViewModel.IsDrinksLoading = true;

                var barId = new BarId(new Guid("A8A4E6C2-2B7D-41EE-8B3D-2053D74FAD67"));

                IEnumerable<Drink> drinks = await DrinkRepository.GetAll(barId).ConfigureAwait(true);

                var uiDrinks = new List<UiDrink>();

                foreach (var drink in drinks.OrderBy(x => x.Name))
                {
                    var imageUri = await ImageRepository.GetById(drink.ImageId).ConfigureAwait(true);

                    var image = new BitmapImage();
                    image.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                    image.UriSource     = imageUri;

                    var item = new UiDrink { Drink = drink, Image = image, IsImageLoading = true };

                    uiDrinks.Add(item);

                    image.ImageFailed += (_sender, _e) => {
                        image.UriSource     = new Uri("ms-appx:///Assets/{00000000-0000-0000-0000-000000000000}.jpg");
                        item.IsImageLoading = false;
                    };

                    image.ImageOpened += (_sender, _e) => { item.IsImageLoading = false; };
                }

                ViewModel.DrinksView.Source = uiDrinks.ToList();
            }
            finally
            {
                ViewModel.IsDrinksLoading = false;
            }
        }


        private void _HandleDrinkItemClick(Object sender, ItemClickEventArgs e)
        {
            var drinkViewModel = (UiDrink) e.ClickedItem;
            var drink          = drinkViewModel.Drink;

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
            var vm = ViewModel;

            var validTags = getValidTags(vm.SelectedStyle.Id);

            if (validTags == null)
                vm.DrinksView.Filter = x => true;
            else
            {
                vm.DrinksView.Filter = x => {
                    var drink = (UiDrink) x;

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

                    case "SWEET":
                        return new[] { "sweet" };

                    case "BITTER":
                        return new[] { "bitter", "herbal" };

                    case "DRY":
                        return new[] { "dry", "floral" };

                    case "FRESH":
                        return new[] { "fresh" };

                    case "FRUITY":
                        return new[] { "fruity" };

                    case "CREAMY":
                        return new[] { "creamy" };

                    case "SPICY":
                        return new[] { "spicy" };

                    case "1STAR":
                        return new[] { "1-star", "2-star", "3-star" };

                    case "2STAR":
                        return new[] { "2-star", "3-star" };

                    case "3STAR":
                        return new[] { "3-star" };

                    case "ALL":
                    default:
                        return null;
                }
            }
        }
    }
}
