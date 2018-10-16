using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using MobileUI.Models;
using MobileUI.Views;

namespace MobileUI.ViewModels
{
    class ModulesViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ModulesViewModel()
        {
            Title = "Modules";
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
                Items.Add(new Item { Id = "0", Text = "Mod 1", Description = "" });
                Items.Add(new Item { Id = "1", Text = "Mod 2", Description = "" });
                Items.Add(new Item { Id = "2", Text = "Mod 3", Description = "" });
                Items.Add(new Item { Id = "3", Text = "Mod 4", Description = "" });
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

