using System.Windows.Forms;

namespace Number_Pairing_Game.Game.UI
{
    partial class ScreenController
    {
        private readonly Panel container;

        public ScreenController(Panel mainContainer)
        {
            this.container = mainContainer;
        }
        public void RenderScreen(Control Screen)
        {

            foreach (Control ctrl in container.Controls)
            {
                ctrl.Visible = false;
            }
            Screen.BringToFront();
            Screen.Visible = true;

        }
        public void AddScreen(UserControl screen)
        {
            container.SuspendLayout();

            screen.Dock = DockStyle.Fill;

            screen.Visible = false;
            container.Controls.Add(screen);



            container.ResumeLayout(false);

        }


    }
}
