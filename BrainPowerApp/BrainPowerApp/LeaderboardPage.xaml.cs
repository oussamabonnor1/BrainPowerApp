using BrainPowerApp.Model;
using BrainPowerApp.ToolBox;
using BrainPowerApp.ViewModel;
using Newtonsoft.Json;
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
                Image image = new Image { Source = "profile.png" };
                playerCells.Add(new PlayerCell
                {
                    name = player.name,
                    score = player.score,
                    rank = player.id + "",
                    image = image
                });
            }
        }

        //async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        //{
        //    if (e.Item == null)
        //    {
        //        return;
        //    }

        //    await DisplayAlert("Item Tapped", "the item " + e.Item.ToString() + " was tapped.", "OK");

        //    //Deselect Item
        //    ((ListView)sender).SelectedItem = null;
        //}
    }
}
