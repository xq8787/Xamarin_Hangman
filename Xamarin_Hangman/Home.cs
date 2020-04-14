using System;
using System.Collections.Generic;
using System.IO;
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
    [Activity(Label = "Home")]
    public class Home : Activity
    {
        private ImageButton btnA;
        private ImageButton btnB;
        private ImageButton btnC;
        private ImageButton btnD;
        private ImageButton btnE;
        private ImageButton btnF;
        private ImageButton btnG;
        private ImageButton btnH;
        private ImageButton btnI;
        private ImageButton btnJ;
        private ImageButton btnK;
        private ImageButton btnL;
        private ImageButton btnM;
        private ImageButton btnN;
        private ImageButton btnO;
        private ImageButton btnP;
        private ImageButton btnQ;
        private ImageButton btnR;
        private ImageButton btnS;
        private ImageButton btnT;
        private ImageButton btnU;
        private ImageButton btnV;
        private ImageButton btnW;
        private ImageButton btnX;
        private ImageButton btnY;
        private ImageButton btnZ;



        private TextView txtWordToGuess;
        private TextView txthintqes;
        private TextView txtMsg;
        private ImageButton btngameMainMenu;
        private ImageButton btnNewGame;
        private ImageView imgHangman;
        private TextView txtCurrentScore;
        private TextView txtGuessesLeft;

        public static int Id;
        public static string PlayerName;
        public static int score;
        private string letter;
        private string rand;
        private string msg;

        private int GuessesLeft = 7;

        private char[] wordToGuess;
        private char[] HiddenWord;

        private bool GuessedCorrect;

        private List<string> wordList = new List<string>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.layout_home);
            btnNewGame = FindViewById<ImageButton>(Resource.Id.btnNewGame);
            txtWordToGuess = FindViewById<TextView>(Resource.Id.txtWordToGuess);
            txthintqes = FindViewById<TextView>(Resource.Id.txtQues);
            txtMsg = FindViewById<TextView>(Resource.Id.txtMesg);
            btngameMainMenu = FindViewById<ImageButton>(Resource.Id.gamebtnMainMenu);
            btngameMainMenu.Click += btngameMainMenu_Click;
            btnNewGame.Click += btnNewGame_Click;
            txtCurrentScore = FindViewById<TextView>(Resource.Id.txtCurrentScore);
            txtCurrentScore.Text = score.ToString();
            txtGuessesLeft = FindViewById<TextView>(Resource.Id.txtGuessesLeft);
            txtGuessesLeft.Text = GuessesLeft.ToString();


            btnA = FindViewById<ImageButton>(Resource.Id.btnA);
            btnB = FindViewById<ImageButton>(Resource.Id.btnB);
            btnC = FindViewById<ImageButton>(Resource.Id.btnC);
            btnD = FindViewById<ImageButton>(Resource.Id.btnD);
            btnE = FindViewById<ImageButton>(Resource.Id.btnE);
            btnF = FindViewById<ImageButton>(Resource.Id.btnF);
            btnG = FindViewById<ImageButton>(Resource.Id.btnG);
            btnH = FindViewById<ImageButton>(Resource.Id.btnH);
            btnI = FindViewById<ImageButton>(Resource.Id.btnI);
            btnJ = FindViewById<ImageButton>(Resource.Id.btnJ);
            btnK = FindViewById<ImageButton>(Resource.Id.btnK);
            btnL = FindViewById<ImageButton>(Resource.Id.btnL);
            btnM = FindViewById<ImageButton>(Resource.Id.btnM);
            btnN = FindViewById<ImageButton>(Resource.Id.btnN);
            btnO = FindViewById<ImageButton>(Resource.Id.btnO);
            btnP = FindViewById<ImageButton>(Resource.Id.btnP);
            btnQ = FindViewById<ImageButton>(Resource.Id.btnQ);
            btnR = FindViewById<ImageButton>(Resource.Id.btnR);
            btnS = FindViewById<ImageButton>(Resource.Id.btnS);
            btnT = FindViewById<ImageButton>(Resource.Id.btnT);
            btnU = FindViewById<ImageButton>(Resource.Id.btnU);
            btnV = FindViewById<ImageButton>(Resource.Id.btnV);
            btnW = FindViewById<ImageButton>(Resource.Id.btnW);
            btnX = FindViewById<ImageButton>(Resource.Id.btnX);
            btnY = FindViewById<ImageButton>(Resource.Id.btnY);
            btnZ = FindViewById<ImageButton>(Resource.Id.btnZ);
            LoadWords();
            //Disable all the buttons now so nobody can go on a click frenzy
            //DisableButtons();
            imgHangman = FindViewById<ImageView>(Resource.Id.imgHangman);
            DefaultImage();
            btnNewGame.Enabled = false;
            LoadRandomWord();

            // Tie all of the "Letter" button clicks to 1 event.
            btnA.Click += OnChar_Click;
            btnB.Click += OnChar_Click;
            btnC.Click += OnChar_Click;
            btnD.Click += OnChar_Click;
            btnE.Click += OnChar_Click;
            btnF.Click += OnChar_Click;
            btnG.Click += OnChar_Click;
            btnH.Click += OnChar_Click;
            btnI.Click += OnChar_Click;
            btnJ.Click += OnChar_Click;
            btnK.Click += OnChar_Click;
            btnL.Click += OnChar_Click;
            btnM.Click += OnChar_Click;
            btnN.Click += OnChar_Click;
            btnO.Click += OnChar_Click;
            btnP.Click += OnChar_Click;
            btnQ.Click += OnChar_Click;
            btnR.Click += OnChar_Click;
            btnS.Click += OnChar_Click;
            btnT.Click += OnChar_Click;
            btnU.Click += OnChar_Click;
            btnV.Click += OnChar_Click;
            btnW.Click += OnChar_Click;
            btnX.Click += OnChar_Click;
            btnY.Click += OnChar_Click;
            btnZ.Click += OnChar_Click;
        }

        private void btngameMainMenu_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
            Finish();
        }


        private void OnChar_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            // the variable clickbutton references the button  that was clicked
            var clickedbutton = (ImageButton)sender;
            // Disable the button that was clicked
            clickedbutton.Enabled = false;
            clickedbutton.Visibility = ViewStates.Invisible;
            // the variable "letter" contains the text of the button that was clicked
            letter = clickedbutton.Tag.ToString();
            //Change that letter to upper case
            letter = letter.ToUpper();
            // go through the array of the hidden word and see if we can find the letter
            for (int i = 0; i < HiddenWord.Length; i++)
            {
                // if the "letter" of the button clicked matches a letter of the word we are trying to guess
                if (letter == wordToGuess[i].ToString())
                {
                    // // The position of the letter(i) in the word that is hidden(with underscores) is set.
                    HiddenWord[i] = letter.ToCharArray()[0];
                    txtWordToGuess.Text = string.Join(" ", HiddenWord);


                    // Run the "LetterScore" method. Add to the score based upon the letter guessed
                    LetterScore();
                    ScoreUpdate();
                    // The condition "GuessedCorrect" is set to true
                    GuessedCorrect = true;

                }
            }
            // If the GuessedCorrect condition is false, reduce the "GuessesLeft" by 1
            if (GuessedCorrect == false)
            {
                GuessesLeft = GuessesLeft - 1;

                GuessFailed();
                GuessedWrongTextUpdate();
                ScoreUpdate();
            }
            else
            { // Set GuessedCorrect back to False for the next round
                GuessedCorrect = false;
            }

            // If the hidden word does not have underscores left(meaning it is a complete word), the game has been won.
            if (!HiddenWord.Contains('_'))

            {
                GameWon();
            }

        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {// Loads a new word, disable the NewGame button and set the default image

            txtMsg.Text = "";
            btnNewGame.Enabled = false;
            LoadRandomWord();
            btnNewGame.Enabled = false;
            DefaultImage();
        }


        private void LoadRandomWord()
        {// Enable the A-Z buttons, set the "guesses left" to 8 and choose a random word from the wordlist and set it to uppercase and then convert that word to an array
            ButtonEnable();
            GuessesLeft = 7;
            Random randomGen = new Random();
            rand = wordList[randomGen.Next(wordList.Count)];
            rand = rand.ToUpper();
            String[] data = rand.Split(':');
            rand = data[0];
            wordToGuess = data[0].ToCharArray();
            txthintqes.Text = data[1].ToString();


            //  The Hiddenword char array is set to the length of the Word to Guess 
            HiddenWord = new char[wordToGuess.Length];

            // For every letter of the word, set that letter to _ 
            for (int i = 0; i < HiddenWord.Length; i++)
            {
                HiddenWord[i] = '_';
                // And display it on the textWordToGuess, seperating the letters with a space
                txtWordToGuess.Text = string.Join(" ", HiddenWord);
            }

        }


        private void LoadWords()
        {
            Stream myStream = Assets.Open("HangmanDic.txt");
            using (StreamReader sr = new StreamReader(myStream))
            {

                string line;
                // while the line that is being read is not equal to null (meaning there is still text to be read)
                while ((line = sr.ReadLine()) != null)
                { // Add that line to the wordlist
                    //String[] data = line.Split(':');
                    wordList.Add(line);
                    //txthintqes.Text = data[1].ToString();
                }
            }
            myStream.Close();
        }



        // This switch statement is based upon how many guesses the player has left
        //From 7 through to 0. Each case statement displays a different picture and runs the "GuessedWrongText" method, which just displays  a text.
        private void GuessFailed()
        {
            switch (GuessesLeft)
            {
                //case 7:

                //    imgHangman.SetImageResource(Resource.Drawable.GuessFailed1);

                //    break;
                case 6:
                    imgHangman.SetImageResource(Resource.Drawable.GuessFailed1);

                    break;
                case 5:
                    imgHangman.SetImageResource(Resource.Drawable.GuessFailed2);
                    break;

                case 4:
                    imgHangman.SetImageResource(Resource.Drawable.GuessFailed3);

                    break;

                case 3:
                    imgHangman.SetImageResource(Resource.Drawable.GuessFailed4);

                    break;

                case 2:
                    imgHangman.SetImageResource(Resource.Drawable.GuessFailed5);

                    break;

                case 1:
                    imgHangman.SetImageResource(Resource.Drawable.GuessFailed6);

                    break;

                // Case 0(0 turns left), the player has lost the game.  
                case 0:
                    imgHangman.SetImageResource(Resource.Drawable.GuessFailed7);

                    // For losing the game, the player incurs a 12 point penalty to their score. If it puts their score below 0, it will be set to 0
                    score = score - 12;
                    if (score < 0)
                    {
                        score = 0;
                    }
                    System.Threading.Thread.Sleep(200);
                    msg = "You Lost!";
                    txtMsg.Text = msg;
                    txtMsg.SetTextColor(Android.Graphics.Color.Red);
                    txtWordToGuess.Text = rand.ToString();
                    //Toast.MakeText(this, "You have run out of guesses! You LOSE. Your Score was " + score, ToastLength.Short).Show();
                    var cc = new DBConnection();
                    DisableButtons();
                    btnNewGame.Enabled = true;
                    cc.UpdateScore(Id, PlayerName, score);
                    System.Threading.Thread.Sleep(500);
                    //btnNewGame.Enabled = false;
                    //LoadRandomWord();
                    //btnNewGame.Enabled = false;
                    //DefaultImage();
                    //StartActivity(typeof(MainActivity));
                    break;

            }
        }

        // The scoring System
        private void LetterScore()
        {


            // If any of these letters are correct, their score is increased by 4
            switch (letter)
            {
                case "A":
                case "E":
                case "I":
                case "O":
                case "U":
                case "L":
                case "N":
                case "R":
                case "S":
                case "T":
                    score = score + 4;
                    // These letters increase the score by 5.. and so on
                    break;
                case "D":
                case "G":
                case "B":
                case "C":
                case "M":
                case "P":
                    score = score + 5;

                    break;
                case "F":
                case "H":
                case "W":
                case "Y":
                case "V":
                    score = score + 6;

                    break;
                case "K":
                case "J":
                case "X":
                    score = score + 8;

                    break;
                case "Q":
                case "Z":
                    score = score + 10;

                    break;
            }



        }


        // This method enables all the buttons, it is used once a game has been won. 
        private void ButtonEnable()
        {

            btnA.Enabled = true;
            btnB.Enabled = true;
            btnC.Enabled = true;
            btnD.Enabled = true;
            btnE.Enabled = true;
            btnF.Enabled = true;
            btnG.Enabled = true;
            btnH.Enabled = true;
            btnI.Enabled = true;
            btnJ.Enabled = true;
            btnK.Enabled = true;
            btnL.Enabled = true;
            btnM.Enabled = true;
            btnN.Enabled = true;
            btnO.Enabled = true;
            btnP.Enabled = true;
            btnQ.Enabled = true;
            btnR.Enabled = true;
            btnS.Enabled = true;
            btnT.Enabled = true;
            btnU.Enabled = true;
            btnV.Enabled = true;
            btnW.Enabled = true;
            btnX.Enabled = true;
            btnY.Enabled = true;
            btnZ.Enabled = true;
            btnNewGame.Enabled = true;
            btnA.Visibility = ViewStates.Visible;
            btnB.Visibility = ViewStates.Visible;
            btnC.Visibility = ViewStates.Visible;
            btnD.Visibility = ViewStates.Visible;
            btnE.Visibility = ViewStates.Visible;
            btnF.Visibility = ViewStates.Visible;
            btnG.Visibility = ViewStates.Visible;
            btnH.Visibility = ViewStates.Visible;
            btnI.Visibility = ViewStates.Visible;
            btnJ.Visibility = ViewStates.Visible;
            btnK.Visibility = ViewStates.Visible;
            btnL.Visibility = ViewStates.Visible;
            btnM.Visibility = ViewStates.Visible;
            btnN.Visibility = ViewStates.Visible;
            btnO.Visibility = ViewStates.Visible;
            btnP.Visibility = ViewStates.Visible;
            btnQ.Visibility = ViewStates.Visible;
            btnR.Visibility = ViewStates.Visible;
            btnS.Visibility = ViewStates.Visible;
            btnT.Visibility = ViewStates.Visible;
            btnU.Visibility = ViewStates.Visible;
            btnV.Visibility = ViewStates.Visible;
            btnW.Visibility = ViewStates.Visible;
            btnX.Visibility = ViewStates.Visible;
            btnY.Visibility = ViewStates.Visible;
            btnZ.Visibility = ViewStates.Visible;
        }
        // This disables the "letter" buttons so you can no longer click them.
        private void DisableButtons()
        {
            btnA.Enabled = false;
            btnB.Enabled = false;
            btnC.Enabled = false;
            btnD.Enabled = false;
            btnE.Enabled = false;
            btnF.Enabled = false;
            btnG.Enabled = false;
            btnH.Enabled = false;
            btnI.Enabled = false;
            btnJ.Enabled = false;
            btnK.Enabled = false;
            btnL.Enabled = false;
            btnM.Enabled = false;
            btnN.Enabled = false;
            btnO.Enabled = false;
            btnP.Enabled = false;
            btnQ.Enabled = false;
            btnR.Enabled = false;
            btnS.Enabled = false;
            btnT.Enabled = false;
            btnU.Enabled = false;
            btnV.Enabled = false;
            btnW.Enabled = false;
            btnX.Enabled = false;
            btnY.Enabled = false;
            btnZ.Enabled = false;
            btnA.Visibility = ViewStates.Invisible;
            btnB.Visibility = ViewStates.Invisible;
            btnC.Visibility = ViewStates.Invisible;
            btnD.Visibility = ViewStates.Invisible;
            btnE.Visibility = ViewStates.Invisible;
            btnF.Visibility = ViewStates.Invisible;
            btnG.Visibility = ViewStates.Invisible;
            btnH.Visibility = ViewStates.Invisible;
            btnI.Visibility = ViewStates.Invisible;
            btnJ.Visibility = ViewStates.Invisible;
            btnK.Visibility = ViewStates.Invisible;
            btnL.Visibility = ViewStates.Invisible;
            btnM.Visibility = ViewStates.Invisible;
            btnN.Visibility = ViewStates.Invisible;
            btnO.Visibility = ViewStates.Invisible;
            btnP.Visibility = ViewStates.Invisible;
            btnQ.Visibility = ViewStates.Invisible;
            btnR.Visibility = ViewStates.Invisible;
            btnS.Visibility = ViewStates.Invisible;
            btnT.Visibility = ViewStates.Invisible;
            btnU.Visibility = ViewStates.Invisible;
            btnV.Visibility = ViewStates.Invisible;
            btnW.Visibility = ViewStates.Invisible;
            btnX.Visibility = ViewStates.Invisible;
            btnY.Visibility = ViewStates.Invisible;
            btnZ.Visibility = ViewStates.Invisible;

        }


        private void GameWon()
        {

            // Set the image to  default
            //DefaultImage();
            // Display the text
            msg = "You Win!";
            txtMsg.SetTextColor(Android.Graphics.Color.Green);
            txtMsg.Text = msg;
            //Toast.MakeText(this, "You guessed the word correctly", ToastLength.Short).     Show();
            var cc = new DBConnection();
            cc.UpdateScore(Id, PlayerName, score);
            // And load a new word
            // LoadRandomWord();
            btnNewGame.Enabled = true;

        }


        // This method is called when the player has guessed the wrong letter and it tells them how many guesses left they have
        private void GuessedWrongTextUpdate()
        {
            txtGuessesLeft.Text = GuessesLeft.ToString();
        }



        private void ScoreUpdate()
        {
            txtCurrentScore.Text = score.ToString();
        }


        private void DefaultImage()
        {

            imgHangman.SetImageResource(Resource.Drawable.GuessFailed1);
        }




    }
}