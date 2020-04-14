using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;

namespace Xamarin_Hangman
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Button btnNGScreen;
        private Button btnHScores;
        private Button btnChangeDB;
        private Button btnExit;
        private TextView res;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            res = FindViewById<TextView>(Resource.Id.txtres);
            btnNGScreen = FindViewById<Button>(Resource.Id.btnNewGameScreen);
            btnNGScreen.Click += (object sender, EventArgs e) =>
            {
                btnNGScreen_Click(sender, e);
            };

            btnHScores = FindViewById<Button>(Resource.Id.btnHighScores);
            btnHScores.Click += (object sender, EventArgs e) =>
            {
                btnHScores_Click(sender, e);
            };

            btnChangeDB = FindViewById<Button>(Resource.Id.btnEditDB);
            btnChangeDB.Click += (object sender, EventArgs e) =>
            {
                btnChangeDB_Click(sender, e);
            };
            btnExit = FindViewById<Button>(Resource.Id.btnQuit);
            btnExit.Click += (object sender, EventArgs e) =>
            {
                btnExit_Click(sender, e);
            };

        }

        private void btnChangeDB_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(DataBase));
            Finish();
        }

        private void btnHScores_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(PointTable));
            Finish();
        }

        private void btnNGScreen_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(SelectPlayer));
            Finish(); 
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
    }

    }