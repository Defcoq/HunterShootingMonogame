using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Microsoft.Xna.Framework;
using static Android.Content.Res.Resources;

namespace ShootingGameAndroidNew
{
    [Activity(
        Label = "Shooting.Hunter",
        MainLauncher = true, Icon = "@drawable/icon",
        AlwaysRetainTaskState = true,
        Theme = "@android:style/Theme.NoTitleBar",
        LaunchMode = LaunchMode.SingleInstance,
        ScreenOrientation = ScreenOrientation.FullUser,
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden)]
    public class Activity1 : AndroidGameActivity
    {
        private Game1 _game;
        private View _view;

   
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _game = new Game1();
            _view = _game.Services.GetService(typeof(View)) as View;
            //_view.SystemUiVisibility = (StatusBarVisibility)(SystemUiFlags.LayoutStable | SystemUiFlags.LayoutHideNavigation | SystemUiFlags.LayoutFullscreen | SystemUiFlags.HideNavigation | SystemUiFlags.Fullscreen | SystemUiFlags.ImmersiveSticky);

            SetContentView(_view);
            //SetFullScreen();

            // Activer le mode immersif
        

            _game.Run();
        }

        public override void OnWindowFocusChanged(bool hasFocus)
        {
            base.OnWindowFocusChanged(hasFocus);
            if (hasFocus)
            {
                Window.DecorView.SystemUiVisibility = (StatusBarVisibility)(
                    SystemUiFlags.LayoutStable
                    | SystemUiFlags.LayoutFullscreen
                    | SystemUiFlags.LayoutHideNavigation
                    | SystemUiFlags.HideNavigation
                    | SystemUiFlags.Fullscreen
                    | SystemUiFlags.ImmersiveSticky
                );
            }
        }


        protected override void OnResume()
        {
            base.OnResume();

            // When we resume (which also seems to happen on startup), hide the system UI to go to full screen mode.
            HideSystemUI();
        }

        [System.Obsolete]
        public void SetFullScreen()
        {
            var uiOptions = (int)Window.DecorView.SystemUiVisibility;
            uiOptions |= (int)SystemUiFlags.LowProfile;
            uiOptions |= (int)SystemUiFlags.Fullscreen;
            uiOptions |= (int)SystemUiFlags.HideNavigation;
            uiOptions |= (int)SystemUiFlags.ImmersiveSticky;

            Window.DecorView.SystemUiVisibility = (StatusBarVisibility)uiOptions;
            //Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);
            this.Immersive = true;
        }
        private void HideSystemUI()
        {
            // Apparently for Android OS Kitkat and higher, you can set a full screen mode. Why this isn't on by default, or some kind
            // of simple switch, is beyond me.
            // Got this from the following forum post: http://community.monogame.net/t/blocking-the-menu-bar-from-appearing/1021/2
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
            {
                View decorView = Window.DecorView;
                var uiOptions = (int)decorView.SystemUiVisibility;
                var newUiOptions = (int)uiOptions;

                newUiOptions |= (int)SystemUiFlags.LowProfile;
                newUiOptions |= (int)SystemUiFlags.Fullscreen;
                newUiOptions |= (int)SystemUiFlags.HideNavigation;
                newUiOptions |= (int)SystemUiFlags.ImmersiveSticky;

                decorView.SystemUiVisibility = (StatusBarVisibility)newUiOptions;

                this.Immersive = true;
            }
        }


        }
}
