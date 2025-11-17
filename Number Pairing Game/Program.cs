using Number_Pairing_Game.Game.UI;
using System;
using System.Windows.Forms;

namespace Number_Pairing_Game
{
    internal class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new MainScreen());


        }
    }
}
