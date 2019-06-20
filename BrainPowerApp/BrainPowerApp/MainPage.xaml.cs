using BrainPowerApp.Model;
using BrainPowerApp.ToolBox;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BrainPowerApp
{
    public partial class MainPage : ContentPage
    {
        string originalButtonColor = "#8D81AA";
        string[] colors = { "#59DAB4", "#E74C70", "#F188E8", "#5AB1E6", "#F79174", "#F0B93A", "#92D1EC" };
        List<int> patternIndexes;
        List<Color> patternColors;
        bool patternShowing;
        int currentPatternIndex;
        int bestScore;
        ApiClient client;
        Player currentPlayer;
        public static string url = "http://11c15710.ngrok.io";

        public MainPage()
        {
            InitializeComponent();
            gridView.SizeChanged += (object sender, EventArgs e) => { gridView.HeightRequest = gridView.Width; };
        }

        protected override async void OnAppearing()
        {
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = ConvertHexaToColor("#5C4887");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
            Title = "Loading...";
            client = new ApiClient();
            string result = await client.GetRequest(url + "/api/leaderboard/" + 4);
            if (currentPlayer == null)
            {
                currentPlayer = JsonConvert.DeserializeObject<Player>(result);
            }
            InitializingGameParameters();
        }

        public void SelectingButton(int buttonIndex, Color color)
        {
            switch (buttonIndex)
            {
                case 0:
                    one.BackgroundColor = color;
                    break;
                case 1:
                    two.BackgroundColor = color;
                    break;
                case 2:
                    three.BackgroundColor = color;
                    break;
                case 3:
                    four.BackgroundColor = color;
                    break;
                case 4:
                    five.BackgroundColor = color;
                    break;
                case 5:
                    six.BackgroundColor = color;
                    break;
                case 6:
                    seven.BackgroundColor = color;
                    break;
                case 7:
                    eight.BackgroundColor = color;
                    break;
                case 8:
                    nine.BackgroundColor = color;
                    break;
            }
        }

        async Task MakingPattern()
        {
            patternShowing = true;
            patternIndexes.Add(new Random().Next(9));
            patternColors.Add(ConvertHexaToColor(colors[new Random().Next(colors.Length)]));
            for (int i = 0; i < patternIndexes.Count; i++)
            {
                SelectingButton(patternIndexes[i], patternColors[i]);
                //waiting for a bit here
                await Task.Delay(1000);
                SelectingButton(patternIndexes[i], ConvertHexaToColor(originalButtonColor));
                await Task.Delay(250);
            }
            label.Text = "Try to replay the pattern!";
            patternShowing = false;
        }

        Color ConvertHexaToColor(string hexaColor)
        {
            //i never expected to write so much nonsense on my first time on Xamarin
            ColorTypeConverter converter = new ColorTypeConverter();
            return (Color)converter.ConvertFromInvariantString(hexaColor);
        }

        public async void ButtonPressed(Object sender, EventArgs e)
        {
            if (!startGameButton.IsVisible)
            {
                var name = ((Button)sender).Id;
                if (name == one.Id)
                {
                    await CheckingPattern(0);
                }
                else if (name == two.Id)
                {
                    await CheckingPattern(1);
                }
                else if (name == three.Id)
                {
                    await CheckingPattern(2);
                }
                else if (name == four.Id)
                {
                    await CheckingPattern(3);
                }
                else if (name == five.Id)
                {
                    await CheckingPattern(4);
                }
                else if (name == six.Id)
                {
                    await CheckingPattern(5);
                }
                else if (name == seven.Id)
                {
                    await CheckingPattern(6);
                }
                else if (name == eight.Id)
                {
                    await CheckingPattern(7);
                }
                else if (name == nine.Id)
                {
                    await CheckingPattern(8);
                }
            }
        }

        async Task CheckingPattern(int index)
        {
            if (!patternShowing)
            {
                if (index == patternIndexes[currentPatternIndex])
                {
                    label.Text = "Steps left: " + (patternIndexes.Count - currentPatternIndex - 1);
                    //if it s correct, we need to light up the button and set the progress bar
                    SelectingButton(patternIndexes[currentPatternIndex], patternColors[currentPatternIndex]);
                    progressBar.Progress = ((float)(currentPatternIndex + 1) / patternIndexes.Count);
                    //going to the next iteration or waiting..
                    currentPatternIndex++;
                    currentPlayer.score += (10 * currentPatternIndex);
                    scoreLabel.Text = "Score: " + currentPlayer.score;
                    if (currentPatternIndex >= patternIndexes.Count)
                    {
                        label.Text = "Great Job!";
                        await Task.Delay(1000); //waiting for player to realize this itteration is over
                        currentPatternIndex = 0;
                        progressBar.Progress = 0;
                        label.Text = "Remember the sequence!";
                        await MakingPattern();
                    }
                }
                else
                {
                    await GameOver();
                }
            }
        }

        async Task GameOver()
        {
            startGameButton.IsVisible = true;
            progressBar.Progress = 1;
            progressBar.ProgressColor = ConvertHexaToColor("#E74C70"); //making progress color in red
            label.Text = "Press start to try again";

            if (currentPlayer.score > bestScore)
            {
                bestScore = currentPlayer.score;
                bestScoreLabel.Text = "Best score: " + bestScore;
                if (currentPlayer != null)
                {
                    await client.PutRequest(url + "/api/leaderboard/" + currentPlayer.id, currentPlayer);
                }
            }
            InitializingGameParameters();
        }

        private async void StartGameButtonClicked(object sender, EventArgs e)
        {
            if (!patternShowing)
            {
                startGameButton.IsVisible = false;
                label.Text = "Remember the sequence!";
                progressBar.Progress = 0;
                progressBar.ProgressColor = ConvertHexaToColor("#80CBC4"); //making progress color is green
                await MakingPattern();
            }
        }

        void InitializingGameParameters()
        {
            patternIndexes = new List<int>();
            patternColors = new List<Color>();
            patternShowing = false;
            currentPatternIndex = 0;
            bestScore = currentPlayer.score;
            currentPlayer.score = 0;
            Title = currentPlayer.name;
            scoreLabel.Text = "Score: " + currentPlayer.score;
            bestScoreLabel.Text = "Best score: " + bestScore;
        }

        private void ButtonReleased(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundColor = ConvertHexaToColor(originalButtonColor);
        }

        private async void OpenLeaderboard(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NavigationPage(new LeaderboardPage()));
        }
    }
}
