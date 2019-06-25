﻿
using BrainPowerApp.Model;
using BrainPowerApp.ToolBox;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BrainPowerApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogInPage : ContentPage
    {
        public string[] iconPaths = { "profile.png", "blueplayer.png", "redplayer.png" };
        int iconIndex;
        ApiClient client;
        public static int playerId;

        public LogInPage()
        {
            InitializeComponent();
            iconIndex = 0;
            avatar.Source = iconPaths[iconIndex];
            client = new ApiClient();
        }

        private async void RegisterPlayer(object sender, System.EventArgs e)
        {
            string input = playerNameInput.Text;
            
            if(input != "")
            {
                Player player = new Player { name = input, recordDate = "today", score = 0 };
                string result = await client.PostRequest(MainPage.url + "/api/leaderboard/", player);
                player = JsonConvert.DeserializeObject<Player>(result);
                playerId = player.id;
            }

            await Navigation.PushAsync(new NavigationPage(new MainPage()));
        }

        private void ChangeAvatar(object sender, System.EventArgs e)
        {
            ImageButton button = (ImageButton)sender;
            iconIndex = button.Id == leftArraow.Id ? iconIndex - 1 : iconIndex + 1;
            if (iconIndex < 0)
            {
                iconIndex = iconPaths.Length - 1;
            }

            iconIndex = iconIndex % iconPaths.Length;
            avatar.Source = iconPaths[iconIndex];
        }
    }
}