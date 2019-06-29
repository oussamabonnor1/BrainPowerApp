using BrainPowerApp.Model;
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
            int rank = 0;
            foreach (Player player in players)
            {
                int random = new Random().Next(3);
                rank++;

                playerCells.Add(new PlayerCell
                {
                    name = player.name,
                    score = player.score,
                    rank = rank + "",
                    image = iconNames[player.avatarId]
                });
            }
            //first place affection
            firstPlaceImage.Source = iconNames[players[0].avatarId];
            firstPlaceLabel.Text = players[0].name;
            //second place affection 
            secondPlaceImage.Source = iconNames[players[1].avatarId];
            secondPlaceLabel.Text = players[1].name;
            //third place affection
            thirdPlaceImage.Source = iconNames[players[2].avatarId];
            thirdPlaceLabel.Text = players[2].name;

            playerCells.RemoveAt(0);
            playerCells.RemoveAt(0);
            playerCells.RemoveAt(0);
        }

        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }

            playerCells[playerCells.IndexOf((PlayerCell)((ListView)sender).SelectedItem)].name = "hello";
            ((PlayerCell)((ListView)sender).SelectedItem).name = "brb";

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
