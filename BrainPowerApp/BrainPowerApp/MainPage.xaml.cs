using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BrainPowerApp
{
    public partial class MainPage : ContentPage
    {
        string originalButtonColor = "#8D81AA";
        string[] colors = { "#59DAB4", "#E74C70", "#F188E8", "#5AB1E6", "#F79174","#F0B93A", "#92D1EC" };
        List<int> patternIndexes;
        List<Color> patternColors;
        bool patternShowing;
        int currentPatternIndex;

        public MainPage()
        {
            InitializeComponent();
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

        public void ButtonClicked(Object sender, EventArgs e)
        {
            var name = ((Button)sender).Id;
            Console.WriteLine(name);
            if (name == one.Id)
            {
                CheckingPattern(0);
            }
            else if (name == two.Id)
            {
                CheckingPattern(1);
            }
            else if (name == three.Id)
            {
                CheckingPattern(2);
            }
            else if (name == four.Id)
            {
                CheckingPattern(3);
            }
            else if (name == five.Id)
            {
                CheckingPattern(4);
            }
            else if (name == six.Id)
            {
                CheckingPattern(5);
            }
            else if (name == seven.Id)
            {
                CheckingPattern(6);
            }
            else if (name == eight.Id)
            {
                CheckingPattern(7);
            }
            else if (name == nine.Id)
            {
                CheckingPattern(8);
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
                    //waiting for a bit here
                    await Task.Delay(150);
                    SelectingButton(patternIndexes[currentPatternIndex], ConvertHexaToColor(originalButtonColor));
                    await Task.Delay(150);
                    //going to the next iteration or waiting..
                    currentPatternIndex++;
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
                    progressBar.Progress = 1;
                    progressBar.ProgressColor = ConvertHexaToColor("#E74C70"); //making progress color in red
                    label.Text = "Wrong! Press start to try again...";
                    InitializingGameParameters();
                }
            }
        }

        private async void StartGameButtonClicked(object sender, EventArgs e)
        {
            if (!patternShowing)
            {
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
        }
    }
}
