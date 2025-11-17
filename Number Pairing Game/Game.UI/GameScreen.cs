using Number_Pairing_Game.Game.UI.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Number_Pairing_Game.Game.UI
{
    internal class GameScreen : ScreenBase
    {
        // EVENT HANDLERS
        public Button PauseGameBtn;
        private Button ResumeGameBtn;
        private Button BackToMenuBtn;
        private Button PlayAgainBtn;

        private PlayerController gameController;
        public event EventHandler PauseGameClicked;
        public event EventHandler GameBackToMenu;
        public event EventHandler ResumeGameClicked;
        public event EventHandler PlayAgainClicked;


        // GAME UI ESSENTIALS
        public Label scoreSgp;
        public Label gameTime;
        public Button currrentHighscoreLabel; // label but in button style
        public FlowLayoutPanel healthBar;
        public FlowLayoutPanel itemsContainer;

        private FlowLayoutPanel gp;
        private FlowLayoutPanel sgp2;

        public FlowLayoutPanel pausedGameOverlay;
        public FlowLayoutPanel gameOverPanel;
        public List<Panel> cards = new List<Panel>(); // card holder
        public Button PlayerCoins;
        public GameScreen(GameController gmc)
        {
            gameController = gmc;
            ShowGame();
            

            PauseGamePanel();
            GameOverPanel();


           
            //register event 
            BackToMenuBtn.Click += (s, e) => GameBackToMenu?.Invoke(this, EventArgs.Empty);

            PauseGameBtn.Tag = "pauseBtn";
            ResumeGameBtn.Tag = "resumeBtn";
            PauseGameBtn.Click += (s, e) => PauseGameClicked?.Invoke(s, EventArgs.Empty);
            ResumeGameBtn.Click += (s, e) => ResumeGameClicked?.Invoke(s, EventArgs.Empty);

            PlayAgainBtn.Click += (s, e) => PlayAgainClicked?.Invoke(s, EventArgs.Empty);

            currrentHighscoreLabel.Text = gameController.GetPlayerHighScore().ToString();
            
        }
        private void GameOverPanel()
        {
            gameOverPanel = new FlowLayoutPanel()
            {

                FlowDirection = FlowDirection.TopDown,
                BackColor = UIPropertyMember.bgDarkColorSecondary,
                Padding = new Padding(15),
                Visible = false,
                AutoSize = true,
            };
            gameOverPanel.Location = new Point(
                (1200 - 250) / 2,
                (700 - 250) / 2
            );
            

            PlayAgainBtn = UIComponentsMember.DarkButton("Play Again");
           
            Button backMenu = UIComponentsMember.DarkButton("Back to Menu");
            backMenu.Width = PlayAgainBtn.Width = 200;
            backMenu.Click += (s, e) => GameBackToMenu(s,e);

            gameOverPanel.Controls.Add(new Label() { Text = "Game Over!", Font = new Font(UIPropertyMember.secondaryFont, 19, FontStyle.Regular), AutoSize = true, ForeColor = Color.Red });
            gameOverPanel.Controls.Add(PlayAgainBtn);
            gameOverPanel.Controls.Add(backMenu); 
            Controls.Add(gameOverPanel);
            gameOverPanel.BringToFront();


        } 
        private void PauseGamePanel()
        {
            pausedGameOverlay = new FlowLayoutPanel()
            {
                //Anchor = AnchorStyles.Top,
                FlowDirection = FlowDirection.TopDown,
                BackColor = UIPropertyMember.bgDarkColorSecondary,
                Padding = new Padding(15),
                Visible = false,
                AutoSize = true,
            };
            pausedGameOverlay.Location = new Point(
                (1200 - 250) / 2,
                (700 - 250) / 2
            );


            ResumeGameBtn = UIComponentsMember.DarkButton(TextButton: "Resume");
            ResumeGameBtn.Anchor = AnchorStyles.Top;
            BackToMenuBtn = UIComponentsMember.DarkButton("Back to Menu");
            BackToMenuBtn.Anchor = AnchorStyles.Top;

            ResumeGameBtn.Width = BackToMenuBtn.Width = 200;
            Label pLbl = new Label()
            {
                Text = "Game Paused",
                AutoSize = true,
                Font = new Font("Poppins", 22, FontStyle.Regular),
                ForeColor = Color.White,

                Margin = new Padding(0, 40, 0, 40)

            };
            pLbl.BackColor = pausedGameOverlay.BackColor;


            pausedGameOverlay.Controls.Add(pLbl);

            pausedGameOverlay.Controls.Add(ResumeGameBtn);
            pausedGameOverlay.Controls.Add(BackToMenuBtn);

            Controls.Add(pausedGameOverlay);
            pausedGameOverlay.BringToFront();

        }
        
        public void RefreshGameUI()
        {
            SetBackgroundUI();
            UpdateScoreUI();
            UpdateItemsUI();
            PlayerCoins.Text = "0";
            gameOverPanel.Visible = false;
            sgp2.Controls.Clear();
            cards.Clear();
            InitializeCardUI();


        }
        private void UpdateScoreUI()
        {
            scoreSgp.Text = "0";
        }

        public void UpdateItemsUI()
        {
            itemsContainer.Controls.Clear();
            foreach (var gameOwnedItem in gameController.GetPlayerOwnedItems())
            {

                Label sa = new Label() { Text = gameOwnedItem.stock.ToString(), ForeColor = Color.White , Size = new Size(32,32),Font = new Font("Poppins",16,FontStyle.Regular)};
                sa.Tag = gameOwnedItem.itemId;
                sa.Image = new Bitmap(Image.FromFile(gameController.GetShopItems().Find(n => n.itemId ==gameOwnedItem.itemId).imgPath), new Size(32,32));
                
                sa.Click += (s, e) => {

                    gameController.ConsumeItem_Click(s, e);
                    UpdateItemsUI();
            };
            itemsContainer.Controls.Add(sa);
        }
             
           
        
    }

        private void SetBackgroundUI()
        {
            gp.BackgroundImage = Image.FromFile("GameBackground-Easy.png");
            gp.BackgroundImageLayout = ImageLayout.Stretch;
            sgp2.BackgroundImage = Image.FromFile("board_image.png");
            sgp2.BackgroundImageLayout = ImageLayout.Stretch;
        }
        private void InitializeCardUI()
        {


            int cardRow = 4;
            int cardCol = 6;
            var parent = sgp2;
            int cardWidth = (parent.ClientSize.Width - (parent.Padding.All * 3)) / cardCol;
            int cardHeight = (parent.ClientSize.Height - (parent.Padding.All * 3)) / cardRow;
            for (int i = 0; i < cardRow; i++)
            {
                for (int j = 0; j < cardCol; j++)
                {
                    Panel panel = new Panel()
                    {
                        Size = new Size(cardWidth - 5, cardHeight - 5),

                    };
                    panel.BackgroundImage = Image.FromFile("hidden_card.png");
                    panel.BackgroundImageLayout = ImageLayout.Stretch;

                    Label lbl = new Label()
                    {

                        Font = new Font("Press Start 2P", 20, FontStyle.Bold),
                        ForeColor = UIPropertyMember.BgDarkColorPrimary
                        ,
                        AutoSize = false,
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Visible = false,
                        BackColor = ColorTranslator.FromHtml("#FFFDBA")

                    };

                    panel.Controls.Add(lbl);
                    parent.Controls.Add(panel);
                    cards.Add(panel);
                }
            }


        }
        private void ShowGame()
        {
            gp = new FlowLayoutPanel()
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(15),
                FlowDirection = FlowDirection.LeftToRight

                 ,
                WrapContents = false
            };

            Size gpSize = new Size(310, 650);

            // FIRST COLUMN PANEL FOR THE SCORE, HEALTH BAR, AND GAME ITEMS
            FlowLayoutPanel sgp1 = new FlowLayoutPanel()
            {
                Size = new Size(gpSize.Width, gpSize.Height - 100),
                WrapContents = true,
                FlowDirection = FlowDirection.RightToLeft
            ,
                Padding = new Padding(10),
                AutoSizeMode = AutoSizeMode,
                BackColor = UIPropertyMember.bgDarkColorSecondary
            };


            scoreSgp = UIComponentsMember.GameScoreLabel(text: "0");

            int forSubSgpWidth = gpSize.Width - (sgp1.Padding.All * 4);
            scoreSgp.Width = forSubSgpWidth;
            scoreSgp.Height = 50;
            scoreSgp.TextAlign = ContentAlignment.MiddleRight;

            // current highscore label 
            currrentHighscoreLabel = UIComponentsMember.ImageButtonAsALabel(
                image: new Bitmap(Image.FromFile("icons8-crown-100.png"), new Size(22, 22)), font: new Font(UIPropertyMember.mainFont, 19, FontStyle.Regular), backColor: sgp1.BackColor
                );



            sgp1.Controls.Add(currrentHighscoreLabel);
            sgp1.Controls.Add(scoreSgp);

            gameTime = UIComponentsMember.GameScoreLabel(text: "100",
                font: new Font("Puritan", 16, FontStyle.Regular));
            gameTime.Width = forSubSgpWidth;
            gameTime.TextAlign = ContentAlignment.MiddleRight;
            gameTime.Height = 50;
            sgp1.Controls.Add(gameTime);

            healthBar = new FlowLayoutPanel()
            {
                Size = new Size(forSubSgpWidth, 50),
                FlowDirection = FlowDirection.LeftToRight,
                BackColor = UIPropertyMember.bgDarkColorSecondary,
                WrapContents = true
            };
            healthBar.Padding = new Padding(healthBar.Height / 4);


            itemsContainer = new FlowLayoutPanel()
            {
                Size = new Size(forSubSgpWidth, 50 * 3),
                FlowDirection = FlowDirection.LeftToRight,
                BackColor = UIPropertyMember.BgDarkColorPrimary,
                WrapContents = true
            };
            itemsContainer.Padding = new Padding(healthBar.Height / 4);
            itemsContainer.SuspendLayout();

            UpdateItemsUI();
            itemsContainer.ResumeLayout();
            PauseGameBtn = UIComponentsMember.DarkButton("MENU");

            gp.Controls.Add(PauseGameBtn);
            sgp1.Controls.Add(healthBar);
            sgp1.Controls.Add(itemsContainer);


            sgp1.Height = scoreSgp.Height + healthBar.Height + itemsContainer.Height + gameTime.Height + (sgp1.Padding.All * 4) + (currrentHighscoreLabel.Height + 20);

            // SECOND PANEL FOR CARD
            sgp2 = new FlowLayoutPanel() { Size = new Size(650, gpSize.Height), FlowDirection = FlowDirection.TopDown, Padding = new Padding(15) };

            InitializeCardUI();

            PlayerCoins = UIComponentsMember.ImageButtonAsALabel(
                   text: "0",
               image: new Bitmap(Image.FromFile("icons8-coins-100.png"), new Size(26, 26)), backColor: UIPropertyMember.BgDarkColorPrimary,
               font: new Font("Poppins", 14, FontStyle.Regular));
            gp.Controls.Add(
                PlayerCoins
                );

            gp.Controls.Add(sgp1);
            gp.Controls.Add(sgp2);
            gp.Controls.Add(PlayerCoins);


            Controls.Add(gp);
        }

    }
}
