using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using MobileUI.Models;
using MobileUI.Views;

namespace MobileUI.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Main Building";
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
                Items.Add(new Item { Id = "0", Text = "200", Description = ""});
                Items.Add(new Item { Id = "1", Text = "300", Description = "" });
                Items.Add(new Item { Id = "2", Text = "2100", Description = "" });
                Items.Add(new Item { Id = "3", Text = "2200", Description = "" });
                Items.Add(new Item { Id = "4", Text = "2300", Description = "" });
                Items.Add(new Item { Id = "5", Text = "3200", Description = "" });
                Items.Add(new Item { Id = "6", Text = "3300", Description = "" });
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