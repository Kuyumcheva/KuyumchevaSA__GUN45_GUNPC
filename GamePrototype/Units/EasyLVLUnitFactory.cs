using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePrototype.Units
{
    public class EasyLVLUnitFactory : UnitFactory
    {
        public override Unit CreatePlayer(string name)
        {
            var player = new Player(name, 40, 40, 8);

            player.AddItemToInventory(new Weapon(12, 20, "Good Sword"));
            player.AddItemToInventory(new Armour(15, 20, "Good Armour"));
            player.AddItemToInventory(new HealthPotion("Potion"));

            return player;
        }

        public override Unit CreateGoblinEnemy()
        {
            return new Goblin("Weak Goblin", 15, 15, 1);
        }
    }
}
