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
    [Activity(Label = "DataBase")]
    public class DataBase : Activity
    {
        List<Resources.HangmanScore> myList;
        private Button btnDelEntry;
        private Spinner spinnerChangeDB;
        private Button btnEditDBMenu;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.db_layout);
            DBConnection myConnectionClass = new DBConnection();
            myList = myConnectionClass.ViewAll();

            btnEditDBMenu = FindViewById<Button>(Resource.Id.btnEditDBMainMenu);
            btnEditDBMenu.Click += btnEditDBMainMenu_Click;
            spinnerChangeDB = FindViewById<Spinner>(Resource.Id.spinnereditDB);
            Resources.DataAdapter da = new Resources.DataAdapter(this, myList);

            spinnerChangeDB.Adapter = da;

            spinnerChangeDB.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinnerChangeDB_ItemSelected);

            btnDelEntry = FindViewById<Button>(Resource.Id.btnDeleteEntry);
            btnDelEntry.Click += btnDeleteEntry_Click;
            btnDelEntry.Enabled = false;
        }

        private void btnDeleteEntry_Click(object sender, EventArgs e)
        {
            var cc = new DBConnection();
            cc.DeletePlayer(Home.Id);
            myList = cc.ViewAll();
            var da = new Resources.DataAdapter(this, myList);

            spinnerChangeDB.Adapter = da;
        }

        private void spinnerChangeDB_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            Home.Id = this.myList.ElementAt(e.Position).Id;
            btnDelEntry.Enabled = true;
        }

        private void btnEditDBMainMenu_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
            Finish();
        }
    }
}