using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Xamarin_Hangman
{
    [Activity(Label = "PointTable")]
    public class PointTable : Activity
    {
        List<Resources.HangmanScore> myList;
        private DBConnection myConnectionClass;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.layout_point);

            myConnectionClass = new DBConnection();
            myList = myConnectionClass.ViewAll();
            Button btnHighPointBack = FindViewById<Button>
                (Resource.Id.btnHighestPoint);

            btnHighPointBack.Click += btnHighPointBack_Click;

            // Display the player names and high scores 
            ListView HighScoresListView = FindViewById<ListView>(Resource.Id.HighScoresListView);
            HighScoresListView.Adapter = new Resources.DataAdapter(this, myList);
        }

        private void btnHighPointBack_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
            Finish();
        }
    }
}