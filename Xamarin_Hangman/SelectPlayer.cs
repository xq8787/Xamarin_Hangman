   f  using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin_Hangman.Resources;

namespace Xamarin_Hangman
{
    [Activity(Label = "SelectPlayer")]
    public class SelectPlayer : Activity
    {
        List<Resources.HangmanScore> myList;

        public TextView txtEnterPlayerName;
        private Spinner PlayerNameSpinner;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.player_layout);

            DBConnection myConnectionClass = new DBConnection();
            myList = myConnectionClass.ViewAll();

            PlayerNameSpinner = FindViewById<Spinner>(Resource.Id.selectPlayerSpinner);
            DataAdapter da = new Resources.DataAdapter(this, myList);

            PlayerNameSpinner.Adapter = da;

            PlayerNameSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);

            txtEnterPlayerName = FindViewById<TextView>(Resource.Id.txtEnterName);

            Button btnStartGame = FindViewById<Button>(Resource.Id.btnStartGame);
            btnStartGame.Click += (object sender, EventArgs e) =>
            {
                btnStartGame_Click(sender, e);
            };


            Button btnAddPlayer = FindViewById<Button>(Resource.Id.btnAddProfile);
            btnAddPlayer.Click += (object sender, EventArgs e) =>
            {
                btnAddPlayer_Click(sender, e);
            };

            Button btnselectplayerMainMenu = FindViewById<Button>(Resource.Id.btnselectplayerMainMenu);

            btnselectplayerMainMenu.Click += (object sender, EventArgs e) =>
            {
                btnselectplayerMainMenu_Click(sender, e);
            };
        }


        private void btnselectplayerMainMenu_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
            Finish();
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(Home));
            Finish();
        }

        private void btnAddPlayer_Click(object sender, EventArgs e)
        {
            // If the length of the text is more then 0.. do this..
            if (txtEnterPlayerName.Text.Length > 0)
            {
                // Set the new PlayerName to the text in the textfield
                Home.PlayerName = txtEnterPlayerName.Text.ToString();
                // Give them a score of 0 to begin with
                Home.score = 0;
                var cc = new DBConnection();
                // Insert the Players name and score into the database
                String res = cc.InsertNewPlayer(Home.PlayerName, Home.score);
                myList = cc.ViewAll();

                var da = new Resources.DataAdapter(this, myList);
                // And display the updated list on the spinner
                PlayerNameSpinner.Adapter = da;

            }
            // Display a message if there is an empty textfield
            else
            {
                Toast.MakeText(this, "Please enter at least 1 character for your name", ToastLength.Short).Show();
            }
        }
        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {

            Spinner spinner = (Spinner)sender;
            // The Player Name and their score is collected from here 
            Home.Id = this.myList.ElementAt(e.Position).Id;
            Home.PlayerName = this.myList.ElementAt(e.Position).Name;
            Home.score = this.myList.ElementAt(e.Position).Score;
        }

    }
}