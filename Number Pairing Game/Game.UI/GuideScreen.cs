using System;
using System.Drawing;
using System.Windows.Forms;

namespace Number_Pairing_Game.Game.UI.Controls
{
    internal class GuideScreen : ScreenBase
    {
        public event EventHandler BackBtnClicked;
        private Button backBtn;

        public GuideScreen()
        {
            InitializeComponents();
            UIComponentsMember.SetBackground(this);

            backBtn.Click += (s, e) =>
            {
                BackBtnClicked?.Invoke(this, EventArgs.Empty);
            };
        }

        private void InitializeComponents()
        {

            int panelWidth = 600;
            int panelHeight = 550;
            FlowLayoutPanel guidePanel = new FlowLayoutPanel()
            {
                Size = new Size(panelWidth, panelHeight),
                Anchor = AnchorStyles.None,
                FlowDirection = FlowDirection.TopDown,
                //AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                BackColor = ColorTranslator.FromHtml(UIPropertyMember.parentMenuPanelBgColor)
            ,
                Padding = new Padding(15),


            };

            guidePanel.Location = new Point(
       (ClientSize.Width - guidePanel.Width) / 2,
       (ClientSize.Height - guidePanel.Height) / 2
   );
            Label label1 = new Label()
            {
                Text = "Instructions",
                Font = new Font(UIPropertyMember.mainFont, 24, FontStyle.Regular),
                AutoSize = false,
                Width = guidePanel.ClientSize.Width - (guidePanel.Padding.All * 4),
                Height = 40,
                TextAlign = ContentAlignment.MiddleCenter,
                Anchor = AnchorStyles.Top,
                Margin = new Padding(0, 10, 0, 10)
            };

            Label label2 = new Label()
            {
                Text = "At the start of each phase, all numbers on the grid are revealed for a limited time At the start of each phase, all numbers " +
                "on the grid are revealed for a limited time At the start of each phase, all numbers on the grid are revealed for a limited time" +
                "At the start of each phase, all numbers on the grid are revealed for a limited time At the start of each phase, all numbers on the grid are revealed for a limited time At the start of each phase, all numbers on the grid are revealed for a limited time" +
                "At the start of each phase, all numbers on the grid are revealed for a limited time At the start of each phase, all numbers on the grid are revealed for a limited time At the start of each phase, all numbers on the grid are revealed for a limited time" +
                "At the start of each phase, all numbers on the grid are revealed for a limited time At the start of each phase, all numbers on the grid are revealed for a limited time At the start of each phase, all numbers on the grid are revealed for a limited time",
                Font = new Font(UIPropertyMember.mainFont, 12, FontStyle.Regular),
                AutoSize = true,
                Margin = new Padding(20),


            };

            backBtn = new Button();
            backBtn.Image = new Bitmap(UIPropertyMember.MenuBackButtonImg, new Size(32, 32));
            backBtn.AutoSize = true;
            backBtn.FlatStyle = FlatStyle.Flat;
            backBtn.BackColor = Color.Empty;
            backBtn.ForeColor = ColorTranslator.FromHtml(UIPropertyMember.parentMenuForeColor);
            backBtn.Font = new Font(UIPropertyMember.secondaryFont, 10, FontStyle.Regular);
            backBtn.FlatAppearance.BorderSize = 0;
            guidePanel.Controls.Add(backBtn);

            guidePanel.Controls.Add(label1);
            guidePanel.Controls.Add(label2);

            this.Controls.Add(guidePanel);



        }
    }
}
