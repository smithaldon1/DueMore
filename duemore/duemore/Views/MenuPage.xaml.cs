using DueMore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DueMore.Views
{
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Inbox, Title="Inbox", ImageSource="ic_inbox_custom_18dp.png" },
                new HomeMenuItem {Id = MenuItemType.Tasks, Title="Tasks", ImageSource="ic_create_custom_18dp.png" },
                new HomeMenuItem {Id = MenuItemType.Projects, Title="Projects", ImageSource="ic_folder_custom_18dp.png" },
                new HomeMenuItem {Id = MenuItemType.Calendar, Title="Calendar", ImageSource="ic_calendar_today_custom_18dp.png" },
                new HomeMenuItem {Id = MenuItemType.Filters, Title="Filters", ImageSource="ic_filter_list_custom_18dp.png" },
                new HomeMenuItem {Id = MenuItemType.Settings, Title="Settings", ImageSource="ic_settings_custom_18dp.png" },
                new HomeMenuItem {Id = MenuItemType.About, Title="About", ImageSource="ic_help_outline_custom_18dp.png" }
            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
            };
        }
    }
}