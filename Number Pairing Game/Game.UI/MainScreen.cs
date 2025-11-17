using Number_Pairing_Game.Game.UI.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Number_Pairing_Game.Game.UI
{

    public partial class MainScreen : Form
    {
        private Panel MainContainer;
        private GameController gmc;
        private ScreenController controller;
        
        public MainScreen()
        {
            InitializeComponent();

            MainContainer = new Panel { Dock = DockStyle.Fill };

            Controls.Add(MainContainer);

            controller = new ScreenController(MainContainer);

            gmc = new GameController();

            GameScreen game = new GameScreen(gmc);
            MenuScreen menu = new MenuScreen(gmc);
            GuideScreen guide = new GuideScreen();
            ShopScreen shop = new ShopScreen(gmc);
            


            menu.OpenGuideClicked += (s, e) =>
            {
                controller.RenderScreen(guide);
            };
            menu.OpenShopClicked += (s, e) =>
            {
                shop.RefReshShop();
                controller.RenderScreen(shop);
            };

            guide.BackBtnClicked += (s, e) =>
            {
                controller.RenderScreen(menu);
            };

            shop.BackBtnClicked += (s, e) =>
            {
                menu.RefreshMenu();
                controller.RenderScreen(menu);
            };
            menu.StartGameClicked += (s, e) =>
            {
             
                game.RefreshGameUI();
                game.pausedGameOverlay.Visible = false;
                gmc.StartGame(game.gameTime, game.cards, game.scoreSgp, game.currrentHighscoreLabel, game.healthBar,game.gameOverPanel,game.PlayerCoins);
                controller.RenderScreen(game);
            };

            game.GameBackToMenu += (s, e) =>
            {
                gmc.SavePlayerData();
                menu.RefreshMenu();
                controller.RenderScreen(menu);
            };
            game.PauseGameClicked += (s, e) =>
            {
                gmc.GamePauseResume_Click(s, e);
                game.pausedGameOverlay.Visible = true;
            
            };
            game.ResumeGameClicked += (s, e) =>
            {
                gmc.GamePauseResume_Click(s, e);
                game.pausedGameOverlay.Visible = false;
            };
            game.PlayAgainClicked += (s, e) =>
            {
                game.RefreshGameUI();

                gmc.SavePlayerData();
                
                game.gameOverPanel.Visible = false;
                gmc.StartGame(game.gameTime, game.cards, game.scoreSgp, game.currrentHighscoreLabel, game.healthBar, game.gameOverPanel,game.PlayerCoins);
            };

            controller.AddScreen(menu);
            controller.AddScreen(guide);
            controller.AddScreen(shop);
            controller.AddScreen(game);


            controller.RenderScreen(menu);


        }
      
        private void InitializeComponent(string screenName = "MainScreen", string screenText = "Number Pairing Game")
        {
            this.SuspendLayout();
            // 
            //  fix layout of the game (bawal paltan masisira ang design layour)
            // 
            this.ClientSize = new Size(1200, 700);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = screenName;
            this.Text = screenText;


            this.ResumeLayout(false);

        }

    }
}
