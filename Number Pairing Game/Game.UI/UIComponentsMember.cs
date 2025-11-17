using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Number_Pairing_Game.Game.UI
{
    internal class UIComponentsMember
    {
        public static void SetBackground(UserControl parent)
        {
            try
            {

                if (File.Exists("MenuBackground.png"))
                {
                    Image bgImage = Image.FromFile("MenuBackground.png");
                    parent.BackgroundImage = null;

                    parent.BackgroundImage = bgImage;
                    parent.BackgroundImageLayout = ImageLayout.Stretch;

                }
                else
                {
                    parent.BackColor = Color.Red;
                }


            }
            catch (Exception e)
            {
                MessageBox.Show("Error loading background image: " + e.Message);
            }
        }

        public static Label GameScoreLabel(string text, Font font = default, bool autoSize = default, AnchorStyles anchor = default, Color foreColor = default, Color backColor = default)
        {
            var lbl = new Label()
            {
                Text = text,
                Font = font == default ? new Font(UIPropertyMember.mainFont, 32, FontStyle.Bold) : font,
                AutoSize = autoSize,
                Anchor = anchor,
                ForeColor = foreColor == default ? Color.White : foreColor,
                BackColor = (backColor == default) ? Color.Empty : backColor
            };
            return lbl;
        }
        public static Button DarkButton(string TextButton, Size size = new Size())
        {
            Button darkButton = new Button();
            darkButton.BackColor = ColorTranslator.FromHtml(UIPropertyMember.parentMenuButtonBgColor);
            darkButton.ForeColor = ColorTranslator.FromHtml(UIPropertyMember.parentMenuForeColor);
            darkButton.FlatStyle = FlatStyle.Flat;

            darkButton.FlatAppearance.BorderSize = 0;
            darkButton.Size = size;
            darkButton.Font = new Font(UIPropertyMember.mainFont, 13, FontStyle.Regular);
            darkButton.AutoSize = true;
            darkButton.Text = TextButton;
            return darkButton;
        }
        public static Button MenuBackButton()
        {
            Button backButton = new Button();
            backButton.Image = new Bitmap(System.Drawing.Image.FromFile("back.png"), new Size(32, 32));
            backButton.FlatStyle = FlatStyle.Flat;
            backButton.FlatAppearance.BorderSize = 0;

            backButton.AutoSize = true;
            return backButton;
        }
        public static Button ImageButtonAsALabel(
                string text = "",
                Image image = null,
                Font font = default,
                Size size = new Size(),
                Color backColor = default,
                Color foreColor = default,
                Padding pad = default,
                bool enabled = true)
        {
            var btn = new Button
            {
                Text = text,
                Size = size,
                BackColor = backColor == default ? Color.Empty : backColor,
                ForeColor = foreColor == default ? Color.White : foreColor,
                Enabled = enabled,
                FlatStyle = FlatStyle.Flat,
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft,
                ImageAlign = ContentAlignment.MiddleLeft,
                TextImageRelation = TextImageRelation.ImageBeforeText,
                Padding = new Padding(10, 0, 10, 0),
                Font = font,
            };

            if (image != null)
                btn.Image = image;

            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = btn.BackColor;
            btn.FlatAppearance.MouseDownBackColor = btn.BackColor;
            btn.FlatAppearance.CheckedBackColor = btn.BackColor;
            btn.FlatAppearance.BorderColor = btn.BackColor;

            return btn;
        }
    }
}
