using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using MobileUI.Models;
using MobileUI.Views;

namespace MobileUI.ViewModels
{
    class TrailersViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public TrailersViewModel()
        {
            Title = "Trailers";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                Items.Add(new Item { Id = "0", Text = "Front Trailers", Description = "[List trailer numbers here]" });
                Items.Add(new Item { Id = "1", Text = "Back Trailers", Description = "[List trailer numbers here]" });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
