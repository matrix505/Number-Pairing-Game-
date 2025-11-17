using GameLogic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Number_Pairing_Game.Game.UI.Controls
{
    internal class ShopScreen : ScreenBase
    {
        public event EventHandler BackBtnClicked;
        private Button backBtn;
        private PlayerController gameController;

        private FlowLayoutPanel table;
        private FlowLayoutPanel subShopPanel;
        private Button playerCoins;
        public ShopScreen(PlayerController gmc)
        {
            gameController = gmc;

            ShowShop();
            UIComponentsMember.SetBackground(this);

            backBtn.Click += (s, e) => BackBtnClicked?.Invoke(this, EventArgs.Empty);
        }
        public void RefReshShop()
        {
            SuspendLayout();
            InitializePlayerCoins();
            table.Controls.Clear();
            InitializeShopItems();
            ResumeLayout();
        }
        private void InitializePlayerCoins()
        {
            playerCoins.Text = gameController.GetPlayerCoins().ToString();
        }
        private void InitializeShopItems()
        {
            table.SuspendLayout();

            int itemOwnedCount = 0;
            foreach (var g in gameController.GetShopItems())
            {
                subShopPanel = new FlowLayoutPanel()
                {
                    FlowDirection = FlowDirection.TopDown,
                    WrapContents = false,
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink
                ,
                    Padding = new Padding(14),
                    BackColor = Color.Beige,
                    Anchor = AnchorStyles.None,
                    BorderStyle = BorderStyle.FixedSingle
                };
                List<GameItem> playerItem = gameController.GetPlayerOwnedItems();


                itemOwnedCount = playerItem.Where(i => i.itemId == g.itemId).Select(item => item.stock).FirstOrDefault();
                subShopPanel.Controls.Add(new Label() { Text = $"Owned: {itemOwnedCount.ToString()}", Font = new Font("Poppins", 12, FontStyle.Regular) });

                subShopPanel.Controls.Add(new PictureBox() { Image = Image.FromFile(g.imgPath), SizeMode = PictureBoxSizeMode.Zoom, Anchor = AnchorStyles.None, Size = new Size(36, 36), Padding = new Padding(0, 0, 0, 0) });
                Button b = new Button()
                {
                    Text = g.itemPrice.ToString(),
                    AutoSize = true,
                    Width = subShopPanel.Width - (4 * subShopPanel.Padding.All),
                    Font = new Font("Poppins", 14, FontStyle.Regular)
                    ,
                    Image = new Bitmap(Image.FromFile("icons8-coins-100.png"), new Size(19, 19)),
                    ImageAlign = ContentAlignment.MiddleRight,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Tag = g.itemId
                };
                b.TextImageRelation = TextImageRelation.ImageBeforeText;

                b.Click += (s, e) =>
                {
                    if (gameController.BuyItem(s, e))
                    {
                        RefReshShop();
                    }

                };

                subShopPanel.Controls.Add(b);
                subShopPanel.Controls.Add(new Label() { Text = g.itemName, Font = new Font("Poppins", 12, FontStyle.Regular) });

                table.ResumeLayout();
                table.Controls.Add(subShopPanel);
            }
        }
        public void ShowShop()
        {
            FlowLayoutPanel shopPanel = new FlowLayoutPanel()
            {
                Size = new Size(560, 570),
                Anchor = AnchorStyles.None,
                Padding = new Padding(15),
                FlowDirection = FlowDirection.TopDown,
                BackColor = ColorTranslator.FromHtml(UIPropertyMember.parentMenuPanelBgColor)
                ,
                WrapContents = false
            };

            shopPanel.SuspendLayout();
            Label shopLabel = new Label()
            {
                Text = "Shop",
                Font = new Font(UIPropertyMember.mainFont, 24, FontStyle.Regular),
                AutoSize = false,
                Width = shopPanel.ClientSize.Width - (shopPanel.Padding.All * 4),
                Height = 40,
                TextAlign = ContentAlignment.MiddleCenter,
                Anchor = AnchorStyles.Top,
                Margin = new Padding(0, 10, 0, 10)
            };

            backBtn = UIComponentsMember.MenuBackButton();

            shopPanel.Controls.Add(backBtn);
            shopPanel.Controls.Add(shopLabel);
            
            playerCoins = UIComponentsMember.ImageButtonAsALabel(
                    text: gameController.GetPlayerCoins().ToString(),
                image: new Bitmap(Image.FromFile("icons8-coins-100.png"), new Size(26, 26)), backColor: shopPanel.BackColor,
                font: new Font("Poppins", 14, FontStyle.Regular));
            shopPanel.Controls.Add(
                playerCoins
                );

            table = new FlowLayoutPanel();
            table.Size = new Size(
                shopPanel.Width - ((shopPanel.Padding.All * 2) + 15 / 2), (shopPanel.Height - shopPanel.Padding.All * 4) - 120
                );
            table.Padding = new Padding(5);
            table.Controls.Clear();
            table.BackColor = ColorTranslator.FromHtml(UIPropertyMember.parentShopMenuBgColor);
            table.BorderStyle = BorderStyle.Fixed3D;



            //InitializeShopItems();



            shopPanel.Controls.Add(table);
            shopPanel.Location = new Point(
       (ClientSize.Width - shopPanel.Width) / 2,
       (ClientSize.Height - shopPanel.Height) / 2
   );
            shopPanel.ResumeLayout();
            this.Controls.Add(shopPanel);

        }
    }
}
