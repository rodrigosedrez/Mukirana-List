using CommunityToolkit.Maui.Views;

namespace Muquirana.Views.Popups;

public partial class ListasHelp : Popup
{
    public ListasHelp()
    {
        InitializeComponent();
    }

    private async void FistPage(object sender, EventArgs e)
    {
        await rolarLado.ScrollToAsync(0, 0, true);
    }
    private async void SecondPage(object sender, EventArgs e)
    {
        await rolarLado.ScrollToAsync(300, 0, true);
    }

    private async void ThirdPage(object sender, EventArgs e)
    {
        await rolarLado.ScrollToAsync(600, 0, true);
    }
    private async void InfoPage(object sender, EventArgs e)
    {
        await rolarLado.ScrollToAsync(900, 0, true);
    }
}