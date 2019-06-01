using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BrainPowerApp
{
    public partial class MainPage : ContentPage
    {
        string originalButtonColor = "#8D81AA";
        string[] colors = { "#59DAB4", "#E74C70", "#F188E8", "#5AB1E6" };
        List<int> patternIndexes;
        int times = 0;

        public MainPage()
        {
            InitializeComponent();
            patternIndexes = new List<int>();
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
            patternIndexes.Add(new Random().Next(9));
            for (int i = 0; i < patternIndexes.Count; i++)
            {
                SelectingButton(patternIndexes[i], ConvertHexaToColor(colors[new Random().Next(colors.Length)]));
                //waiting for a bit here
                await Task.Delay(1000);
                SelectingButton(patternIndexes[i], ConvertHexaToColor(originalButtonColor));
            }
        }

        Color ConvertHexaToColor(string hexaColor)
        {
            //i never expected to write so much nonsense on my first time on Xamarin
            ColorTypeConverter converter = new ColorTypeConverter();
            return (Color)converter.ConvertFromInvariantString(hexaColor);
        }

        public void ButtonClicked(Object sender, EventArgs e)
        {

        }

        private void StartGameButtonClicked(object sender, EventArgs e)
        {
            MakingPattern();
        }
    }
}
