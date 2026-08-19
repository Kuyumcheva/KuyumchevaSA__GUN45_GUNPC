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
    public class HardLVLDungeonBuilder : DungeonBuilderBase
    {
        public override DungeonRoom BuildDungeon()
        {
            var enter = new DungeonRoom("Enter");
            var monsterRoom1 = new DungeonRoom("Monster1", CreateStrongGoblin());
            var monsterRoom2 = new DungeonRoom("Monster2", CreateStrongGoblin());
            var emptyRoom1 = new DungeonRoom("Empty");
            var emptyRoom2 = new DungeonRoom("Empty");
            var lootRoom = new DungeonRoom("Loot1", new Gold());
            var lootStoneRoom = new DungeonRoom("Loot2", new Grindstone("Stone"));
            var rangeWeaponRoom = new DungeonRoom("Loot3", new RangeWeapon(8, 12, "Bow"));
            var helmetRoom = new DungeonRoom("Loot4", new Helmet(5, 10, "Helmet"));
            var finalRoom = new DungeonRoom("Final", new Grindstone("Stone1"));


            enter.TrySetDirection(Direction.Right, monsterRoom1);
            enter.TrySetDirection(Direction.Left, monsterRoom2);

            monsterRoom1.TrySetDirection(Direction.Forward, lootRoom);
            monsterRoom1.TrySetDirection(Direction.Left, lootStoneRoom);
            lootStoneRoom.TrySetDirection(Direction.Forward, rangeWeaponRoom);
            rangeWeaponRoom.TrySetDirection(Direction.Forward, finalRoom);

            monsterRoom2.TrySetDirection(Direction.Forward, lootRoom);
            monsterRoom2.TrySetDirection(Direction.Right, emptyRoom1);
            emptyRoom1.TrySetDirection(Direction.Forward, helmetRoom);
            helmetRoom.TrySetDirection(Direction.Right, finalRoom);

            lootRoom.TrySetDirection(Direction.Forward, emptyRoom2);
            emptyRoom2.TrySetDirection(Direction.Right, finalRoom);


            return enter;
        }

        private Unit CreateStrongGoblin() => new Goblin("Strong Goblin", 25, 25, 4);
    }
}
