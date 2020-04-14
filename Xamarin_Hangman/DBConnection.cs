using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using SQLite;

namespace Xamarin_Hangman
{
    class DBConnection
    {
        private string dbpath { get; set; }
        private SQLiteConnection db { get; set; }


        public DBConnection()
        {
            try
            {
                string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "DataBase.sqlite");
                //string dbPath = Path.Combine("D:/ASP.NET/HangmanProject-master/Hangman/Assets/", "HangmanDB.sqlite");
                db = new SQLiteConnection(dbPath);
                db.CreateTable<Resources.HangmanScore>();
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
            }
        }

        public List<Resources.HangmanScore> ViewAll()
        {
            try
            {
                ;
                return db.Query<Resources.HangmanScore>("select *  from HangmanScore  ORDER BY Score DESC");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:" + e.Message);
                return null;
            }
        }

        public string UpdateScore(int id, string name, int score)
        {
            try
            {
                string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "DataBase.sqlite");
                var db = new SQLiteConnection(dbPath);
                var item = new Resources.HangmanScore();
                item.Id = id;
                item.Name = name;
                item.Score = score;
                db.Update(item);
                return "Record Updated...";
            }
            catch (Exception ex)
            {
                return "Error : " + ex.Message;
            }
        }

        public string InsertNewPlayer(string name, int score)
        {
            try
            {
                string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "DataBase.sqlite");
                var db = new SQLiteConnection(dbPath);
                var item = new Resources.HangmanScore();
                item.Name = name;
                item.Score = score;
                db.Insert(item);
                return "You have been added to the database";
            }
            catch (Exception ex)
            {
                return "Error : " + ex.Message;
            }
        }

        public string DeletePlayer(int id)
        {
            try
            {
                string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "DataBase.sqlite");
                var db = new SQLiteConnection(dbPath);
                var item = new Resources.HangmanScore();
                item.Id = id;
                db.Delete(item);
                return "You have been added to the database";
            }
            catch (Exception ex)
            {
                return "Error : " + ex.Message;
            }
        }

    }
}