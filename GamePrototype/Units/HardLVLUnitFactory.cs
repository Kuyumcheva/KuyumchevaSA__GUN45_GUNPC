using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePrototype.Units
{
    public class HardLVLUnitFactory : UnitFactory
    {
        public override Unit CreatePlayer(string name)
        {
            var player = new Player(name, 25, 25, 4);

            player.AddItemToInventory(new Weapon(8, 10, "Basic Sword"));
            player.AddItemToInventory(new Armour(5, 10, "Basic Armour"));
            player.AddItemToInventory(new HealthPotion("Potion"));

            return player;
        }

        public override Unit CreateGoblinEnemy()
        {
            return new Goblin("Strong Goblin", 25, 25, 4);
        }
    }
}
