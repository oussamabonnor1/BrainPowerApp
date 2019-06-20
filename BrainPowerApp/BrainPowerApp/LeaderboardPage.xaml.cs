using BrainPowerApp.Model;
using BrainPowerApp.ToolBox;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BrainPowerApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LeaderboardPage : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }
        ApiClient client;

        public LeaderboardPage()
        {
            InitializeComponent();
            Items = new ObservableCollection<string>
            {
                "Loading list...",
            };

            MyListView.ItemsSource = Items;
        }

        protected override async void OnAppearing()
        {
            client = new ApiClient();
            string result = await client.GetRequest(MainPage.url + "/api/leaderboard");
            Player[] players = JsonConvert.DeserializeObject<Player[]>(result);
            foreach (Player player in players)
            {
                Items.Add(player.name + ": " + player.score + " on " + player.recordDate);
            }
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }

            await DisplayAlert("Item Tapped", "the item " + e.Item.ToString() + " was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
