
using Xamarin.Forms;

namespace BrainPowerApp.ViewModel
{
    class PlayerCell : ViewCell
    {
        public static readonly BindableProperty NameProperty =
            BindableProperty.Create("PlayerName", typeof(string), typeof(PlayerCell), "");
        public string PlayerName
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public static readonly BindableProperty ScoreProperty =
            BindableProperty.Create("PlayerScore", typeof(string), typeof(PlayerCell), "");
        public string PlayerScore
        {
            get { return (string)GetValue(ScoreProperty); }
            set { SetValue(ScoreProperty, value); }
        }

        public static readonly BindableProperty RankProperty =
            BindableProperty.Create("PlayerRank", typeof(string), typeof(PlayerCell), "");
        public string PlayerRank
        {
            get { return (string)GetValue(RankProperty); }
            set { SetValue(RankProperty, value); }
        }
    }
}
