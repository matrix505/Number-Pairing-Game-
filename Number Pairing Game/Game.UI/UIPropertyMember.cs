using System.Drawing;

namespace Number_Pairing_Game.Game.UI
{
    public static class UIPropertyMember
    {
        //fonts 
        public static string mainFont = "Popppins";
        public static string secondaryFont = "Puritan";

        //for parent menu panel properties
        public static string parentMenuPanelBgColor = "#858F83";
        public static string parentMenuButtonBgColor = "#313233";
        public static string parentMenuForeColor = "#FFFFFF";
        public static string parentShopMenuBgColor = "#3F3F3F";


        public static Color BgDarkColorPrimary { get; } = ColorTranslator.FromHtml("#262626");

        public static Color bgDarkColorSecondary { get; } = ColorTranslator.FromHtml("#505259");
        public static Color BgDarkGreenSecondary { get; } = ColorTranslator.FromHtml("#56615A");

        public static Image MenuBackgroundImage = Image.FromFile("MenuBackground.png");
        public static Image MenuBackButtonImg = Image.FromFile("back.png");
    }
}
