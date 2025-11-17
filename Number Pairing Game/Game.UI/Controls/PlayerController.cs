
using GameLogic;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Number_Pairing_Game.Game.UI.Controls
{
    internal class PlayerController
    {
        private List<GameItem> shopItem;
        protected GameData playerData;
        protected FileGameData fileGameData;

        public PlayerController()
        {
            shopItem = Config.ShopItems();
            fileGameData = new FileGameData();
            playerData = fileGameData.LoadGameData();
        }
        public void SavePlayerData()
        {
            fileGameData.SaveGameData(playerData);
        }
        public List<GameItem> GetShopItems() => shopItem;
        public int GetPlayerCoins()
        {
            return playerData.coins;
        }
        public int GetPlayerHighScore()
        {
            return playerData.highScore;
        }
        public List<GameItem> GetPlayerOwnedItems() => this.playerData.ownedItems;
        public bool BuyItem(object sender, EventArgs e)
        {
            Button b = sender as Button;

            GameItem item = shopItem.Find(n => n.itemId == b.Tag.ToString());

            if (playerData.BuyItem(item))
            {
                fileGameData.SaveGameData(playerData);
                return true;
            }
            return false;
        }
        public virtual void ConsumeItem_Click(object s, EventArgs e) { }
        
    }
}
