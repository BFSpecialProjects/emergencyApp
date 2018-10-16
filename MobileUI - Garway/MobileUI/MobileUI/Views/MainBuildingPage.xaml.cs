using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MobileUI.Models;
using MobileUI.Views;
using MobileUI.ViewModels;

namespace MobileUI.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainBuildingPage : ContentPage
	{
        MainBuildingViewModel viewModel;

        public MainBuildingPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new MainBuildingViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Item;
            if (item == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}