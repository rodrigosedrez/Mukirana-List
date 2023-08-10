using Android.App;
using Android.Content;
using Android.Content.PM;

namespace Muquirana;
[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
[IntentFilter(new[] { Intent.ActionView, Intent.ActionEdit }, Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable }, DataScheme = "file", DataHost = "*")]

public class MainActivity : MauiAppCompatActivity
{
}
