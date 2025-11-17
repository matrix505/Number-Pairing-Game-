using Number_Pairing_Game.Game.UI.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace Number_Pairing_Game.Game.UI
{
    partial class MenuScreen : ScreenBase
    {
        // button handlers
        public event EventHandler StartGameClicked;
        public event EventHandler OpenShopClicked;
        public event EventHandler OpenGuideClicked;

        private Button playBtn;
        private Button shopBtn;
        private Button guideBtn;

        private PlayerController gameController;
        private Label score;
        private Button playerMoney;
        public MenuScreen(PlayerController gameController)
        {
            this.gameController = gameController;
            InitializeMenuComponents();

            UIComponentsMember.SetBackground(this);
            registerButtons();

        }
        public void RefreshMenu()
        {
            UpdatePlayerStats();
        }
        private void UpdatePlayerStats()
        {
            score.Text = gameController.GetPlayerHighScore().ToString();
            playerMoney.Text = gameController.GetPlayerCoins().ToString();
        }
        private void InitializeMenuComponents()
        {
            Size imgBtnIconSize = new Size(28, 28);
            Image playBtnImg = new Bitmap(Image.FromFile("icons8-play-100.png"), imgBtnIconSize);
            Image shopBtnImg = new Bitmap(Image.FromFile("icons8-shop-100.png"), imgBtnIconSize);
            Image guideBtnImg = new Bitmap(Image.FromFile("icons8-manual-100.png"), imgBtnIconSize);
            Image hsBtnImg = new Bitmap(Image.FromFile("icons8-crown-100.png"), imgBtnIconSize);
            Image moneyBtnImg = new Bitmap(Image.FromFile("icons8-coins-100.png"), imgBtnIconSize);

            FlowLayoutPanel mainPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                Size = new Size(300, 400),
                BackColor = ColorTranslator.FromHtml(UIPropertyMember.parentMenuPanelBgColor),
                Anchor = AnchorStyles.None,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
            };
            int left = 30;
            mainPanel.Padding = new Padding(left, 20, left, 100);

            mainPanel.SuspendLayout();

            playerMoney = UIComponentsMember.ImageButtonAsALabel(
                text: gameController.GetPlayerCoins().ToString(), image: moneyBtnImg,
                font: new Font(UIPropertyMember.mainFont, 14, FontStyle.Regular),
                backColor: mainPanel.BackColor,
                foreColor: ColorTranslator.FromHtml(UIPropertyMember.parentMenuForeColor)
                );


            mainPanel.Controls.Add(playerMoney);

            FlowLayoutPanel subPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,

                BackColor = mainPanel.BackColor,
                Anchor = AnchorStyles.None,
                AutoSize = true,
                Margin = new Padding(0, 20, 0, 20)
                ,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                WrapContents = false
            };
            mainPanel.Controls.Add(subPanel);

            score = UIComponentsMember.GameScoreLabel(

                text: gameController.GetPlayerHighScore().ToString(),
                foreColor: ColorTranslator.FromHtml(UIPropertyMember.parentMenuForeColor),
                anchor: AnchorStyles.None,
                backColor: Color.Empty,
                autoSize: true

            );
            Button highscoreLabel = UIComponentsMember.ImageButtonAsALabel(

                text: "Highscore",
                font: new Font(UIPropertyMember.mainFont, 16, FontStyle.Regular),
                foreColor: ColorTranslator.FromHtml(UIPropertyMember.parentMenuForeColor),
                backColor: mainPanel.BackColor,
                image: hsBtnImg
            );


            subPanel.Controls.Add(highscoreLabel);
            subPanel.Controls.Add(score);

            Size buttonSize = new Size(200, 40);
            Padding pad = new Padding(20, 5, 20, 5);

            this.playBtn = UIComponentsMember.DarkButton("Play Game", buttonSize);
            playBtn.Image = playBtnImg;
            playBtn.ImageAlign = ContentAlignment.MiddleLeft;
            playBtn.TextImageRelation = TextImageRelation.ImageBeforeText;

            this.shopBtn = UIComponentsMember.DarkButton("Shop", buttonSize);
            shopBtn.Image = shopBtnImg;
            shopBtn.ImageAlign = ContentAlignment.MiddleLeft;
            shopBtn.TextImageRelation = TextImageRelation.ImageBeforeText;

            this.guideBtn = UIComponentsMember.DarkButton("Guide", buttonSize);

            guideBtn.Image = guideBtnImg;
            guideBtn.ImageAlign = ContentAlignment.MiddleLeft;
            guideBtn.TextImageRelation = TextImageRelation.ImageBeforeText;

            playBtn.Margin = shopBtn.Margin = guideBtn.Margin = pad;
            playBtn.Padding = shopBtn.Padding = guideBtn.Padding = pad;

            mainPanel.Controls.Add(playBtn);
            mainPanel.Controls.Add(shopBtn);
            mainPanel.Controls.Add(guideBtn);

            mainPanel.ResumeLayout();


            this.Controls.Add(mainPanel);


            mainPanel.Location = new Point(
                (this.ClientSize.Width - mainPanel.Width) / 2,
                (this.ClientSize.Height - mainPanel.Height) / 2
            );

        }
        private void registerButtons()
        {
            playBtn.Click += (e, s) => StartGameClicked?.Invoke(this, EventArgs.Empty);
            shopBtn.Click += (e, s) => OpenShopClicked?.Invoke(this, EventArgs.Empty);
            guideBtn.Click += (e, s) => OpenGuideClicked?.Invoke(this, EventArgs.Empty);
        }

    }
}
