using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePrototype.Dungeon
{
    public class EasyLVLDungeonBuilder : DungeonBuilderBase
    {
        public override DungeonRoom BuildDungeon()
        {
            var enter = new DungeonRoom("Enter");
            var monsterRoom = new DungeonRoom("Monster", CreateWeakGoblin());
            var emptyRoom = new DungeonRoom("Empty");
            var lootRoom = new DungeonRoom("Loot1", new Gold());
            var lootStoneRoom = new DungeonRoom("Loot2", new Grindstone("Stone"));
            var rangeWeaponRoom = new DungeonRoom("Loot3", new RangeWeapon(8, 12, "Bow"));
            var helmetRoom = new DungeonRoom("Loot4", new Helmet(5, 10, "Helmet"));
            var finalRoom = new DungeonRoom("Final", new Grindstone("Stone1"));

            enter.TrySetDirection(Direction.Forward, monsterRoom);
            enter.TrySetDirection(Direction.Right, emptyRoom);

            monsterRoom.TrySetDirection(Direction.Forward, lootRoom);
            emptyRoom.TrySetDirection(Direction.Left, helmetRoom);

            lootRoom.TrySetDirection(Direction.Forward, finalRoom);
            helmetRoom.TrySetDirection(Direction.Right, finalRoom);

            return enter;
        }

        private Unit CreateWeakGoblin() => new Goblin("Weak Goblin", 15, 15, 1);
    }
}
