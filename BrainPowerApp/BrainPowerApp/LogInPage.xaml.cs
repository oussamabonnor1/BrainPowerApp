
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BrainPowerApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LogInPage : ContentPage
	{
        public string[] iconPaths = { "profile.png", "blueplayer.png", "redplayer.png" };
        int iconIndex;
        public LogInPage ()
		{
			InitializeComponent ();
            iconIndex = 0;
            avatar.Source = iconPaths[iconIndex];
        }

        private void RegisterPlayer(object sender, System.EventArgs e)
        {

        }

        private void ChangeAvatar(object sender, System.EventArgs e)
        {
            ImageButton button = (ImageButton)sender;
            iconIndex = button.Id == leftArraow.Id ? iconIndex - 1 : iconIndex + 1;
            if (iconIndex < 0) iconIndex = iconPaths.Length - 1;
            iconIndex = iconIndex % iconPaths.Length;
            avatar.Source = iconPaths[iconIndex];
        }
    }
}