using GameLogic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Number_Pairing_Game.Game.UI.Controls;


namespace Number_Pairing_Game.Game.UI.Controls
{
     class GameController : PlayerController
    {
        private GameManager gm;
        private GameItemManager gim;
        public bool gameRunning;
        private CancellationTokenSource cts1;
        private CancellationTokenSource cts2;
        private CancellationTokenSource cts3;
        private int remainingSeconds;
        //

        private List<Panel> cards;
        private Panel firstCard = null;
        private Panel secondCard = null;
        private Label scoreLbl;
        private Label cdLbl;
        private FlowLayoutPanel healthBar;
        private Button gameCurrentHighscoreLabel;
        private FlowLayoutPanel gameOverPanel;
        private Button GameCurrentCoins;

        public bool freeze = false;


        public GameController() : base()
        {

        }
       
        private void SaveHighscore()
        {
            if (GetPlayerHighScore() < gm.CurrentScore)
            {
                playerData.SetNewHighScore(gm.CurrentScore);
            }
        }
        private async void RegisterCards()
        {
            int i = 0;
            List<int> cardValues = gm.GeneratedNumberForCards();
            foreach (var card in cards)
            {
                card.Controls[0].Text = cardValues[i].ToString();
                card.Tag = "card_" + cardValues[i]
; card.Click -= card_click;
                card.Click += card_click;
                i++;
            }
            RevealCardTemp(gm.CardRevealCd * 1000);
            await GameCountdown(gm.GetGameCd(), cdLbl, true);
        }
        private void UpdateHealthBar()
        {
            healthBar.Controls.Clear();
            for (int i = 0; i < 6; i++)
            {
                PictureBox healthImg = new PictureBox()
                {
                    Image = Image.FromFile((i < gm.HeartBar) ? "heart_bar.png" : "heart_bar_empty.png"),
                    SizeMode = PictureBoxSizeMode.Zoom
                }; healthImg.Size = new Size(20, 20);

                healthBar.Controls.Add(healthImg);

            }
        }
        private void FinishedGame()
        {
            freeze = true;
            gm.GameRunning = false;
            gameOverPanel.Visible = true;
            
            cts1?.Cancel();
        }
        public async void StartGame(
            Label gameCd, 
            List<Panel> cards1, Label score1, 
            Button gameHighscoreLbl, 
            FlowLayoutPanel heartbarContainer,
            FlowLayoutPanel gameOverOverlay,
            Button gameCurrentCoins)
        
        {

            gm = new GameManager()
            {
                GameRunning = true,
            };

            freeze = false;
            gim = new GameItemManager(gm);
            healthBar = heartbarContainer;
            gameCurrentHighscoreLabel = gameHighscoreLbl;
            gameOverPanel = gameOverOverlay;
            GameCurrentCoins = gameCurrentCoins;

            UpdateHealthBar();
            scoreLbl = score1;
           
            cdLbl = gameCd;
            cards = cards1;
            
         
            RegisterCards();
        }

        private async Task GameCountdown(int mins, Label uilabel, bool startFromBeginning = false)
        {
            if (startFromBeginning)
            {
                remainingSeconds = mins * 60;
            }
            if (!startFromBeginning && remainingSeconds == 0)
            {
                remainingSeconds = mins;
            }


            // cts for cancelling previous countdown if any
            cts1?.Cancel();
            cts1 = new CancellationTokenSource();
            var token = cts1.Token;
            while (remainingSeconds > 0)
            {
                if (token.IsCancellationRequested) { return; }

                int currMins = remainingSeconds / 60;
                int currSecs = remainingSeconds % 60;

                string temp = $"{currMins:D2}:{currSecs:D2} \n{gm.GameDiffulty}";

                // to prevent cards being clicked when revealing card for next level
                int secToBeStop = 7;
                if (remainingSeconds < secToBeStop && gm.GameRunning)
                {
                    gm.GameRunning = false;
                }
                if (!gm.GameRunning && secToBeStop < currSecs)
                { gm.GameRunning = true; }
                if (uilabel.Text != temp)
                    uilabel.Text = temp;
                try
                {
                    await Task.Delay(1000, token);

                    remainingSeconds--;
                }
                catch (TaskCanceledException)
                {
                    return;
                }

            }

            uilabel.Text = "00:00";
            remainingSeconds = 0;
            gm.SwitchNextLevel();
            gameRunning = true;
            RegisterCards();

        }
        private async Task RevealCardTemp(int ms)
        {
            firstCard = null;
            cts2?.Cancel();
            cts2 = new CancellationTokenSource();
            var token = cts2.Token;


            foreach (var card in cards)
            {
                card.Controls[0].Visible = true;
                card.BackColor = Color.Yellow;
            }

            try
            {
                await Task.Delay(ms, token);
            }
            catch (TaskCanceledException)
            {
                return;
            }

            foreach (var card in cards)
            {
                card.Controls[0].Visible = false;
                card.BackColor = Color.White;
            }
        }
        public async void card_click(object sender, EventArgs e)
        {
            if (!gm.GameRunning) { return; }
            Panel clickedCard = sender as Panel;

            if (clickedCard.Controls[0].Visible) return;

            clickedCard.Controls[0].Visible = true;

            if (firstCard == null)
            {
                firstCard = clickedCard;
                return;
            }

            secondCard = clickedCard;
            //   GOES HERE IF PLAYER CORRECTLY MATCHED CARDS
            if (firstCard.Tag.ToString() == secondCard.Tag.ToString())
            {
                //step1
                gm.IncreaseScore();
                playerData.AddCoins(gm.AddedCurrentCoins());
                
                //step2
                GameCurrentCoins.Text = gm.CurrentGameCoins.ToString();
                scoreLbl.Text = gm.CurrentScore.ToString();
                gameCurrentHighscoreLabel.Text = GetPlayerHighScore().ToString();

                //step3
                SaveHighscore();
                firstCard = null;
                secondCard = null;
                return;
            }
            else
            {
                gm.Attempt += 1;
                if (gm.Attempt == 1)
                
                    gm.HeartBar -= 1;

                    UpdateHealthBar();
                    if (gm.HeartBar == 0)
                    {

                        gm.GameRunning = false;
                        FinishedGame();
                        return;
                    }
                    gm.Attempt = 0;
                }
                cts3?.Cancel();
                cts3 = new CancellationTokenSource();
                var token = cts3.Token;

                if (token.IsCancellationRequested)
                {
                    return;
                }

                try
                {
                    clickedCard.Parent.Enabled = false;
                    await Task.Delay(1000, token);
                }
                catch (TaskCanceledException)
                {

                    return;
                }
                clickedCard.Parent.Enabled = true;
                firstCard.Controls[0].Visible = false;
                secondCard.Controls[0].Visible = false;

                firstCard = null;
                secondCard = null;

            }
        
        public void GamePauseResume_Click(object sender, EventArgs e)
        {
         
            if (gm.GameRunning )
            {

                cts1?.Cancel();
                gm.GameRunning = false;
            }
            else
            {
                // only for with instance button sender can trigger this
                if (sender != null)
                {
                    var btn = sender as Button;
                    if(btn.Tag.ToString() != "resumeBtn") { return; }
                        GameCountdown(remainingSeconds, cdLbl);
                        gm.GameRunning = true;
                    
                }
                       



            }
        }
        public override void ConsumeItem_Click(object s, EventArgs e)
        {
           
            Label b = s as Label;

            
            if (b.Tag == null || !gm.GameRunning) { return; }

            {

                int result = gim.EffectItemInGame(b.Tag.ToString());
                if (result == 1)
                {
                    playerData.UseItem(b.Tag.ToString());
                    UpdateHealthBar();
                }

            }

        }
    }

}
