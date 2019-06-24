﻿using BrainPowerApp.Model;
using BrainPowerApp.ToolBox;
using BrainPowerApp.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BrainPowerApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LeaderboardPage : ContentPage
    {
        public ObservableCollection<PlayerCell> playerCells { get; set; }
        ApiClient client;
        string[] iconNames = { "profile.png", "blueplayer.png", "redplayer.png" };

        public LeaderboardPage()
        {
            InitializeComponent();
            playerCells = new ObservableCollection<PlayerCell>
            {
                new PlayerCell{},
            };

            MyListView.ItemsSource = playerCells;
            //MyListView.ItemTemplate = new DataTemplate(typeof(PlayerCell));
        }

        protected override async void OnAppearing()
        {
            client = new ApiClient();
            string result = await client.GetRequest(MainPage.url + "/api/leaderboard");
            playerCells.Clear();
            Player[] players = JsonConvert.DeserializeObject<Player[]>(result);
            foreach (Player player in players)
            {
                int random = new Random().Next(3);

                Image image = new Image { Source = iconNames[random] };
                playerCells.Add(new PlayerCell
                {
                    name = player.name,
                    score = player.score,
                    rank = player.id + "",
                    image = image
                });
            }
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }

            await DisplayAlert("Item Tapped", "the item " + e.Item.ToString() + " was tapped.", "OK");

            playerCells[playerCells.IndexOf((PlayerCell)((ListView)sender).SelectedItem)].name = "hello";

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
