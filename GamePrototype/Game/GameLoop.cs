using GamePrototype.Combat;
using GamePrototype.Dungeon;
using GamePrototype.Units;
using GamePrototype.Utils;

namespace GamePrototype.Game
{
    public sealed class GameLoop
    {
        private Unit _player;
        private DungeonRoom _dungeon;
        private readonly CombatManager _combatManager = new CombatManager();
        private UnitFactory _unitFactory;
        private DungeonBuilderBase _dungeonBuilder;
        private DifficultyLevel _difficulty;

        public void StartGame()
        {
            Console.WriteLine("Choose difficulty level:");
            Console.WriteLine("1 - Easy");
            Console.WriteLine("2 - Hard");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    _difficulty = DifficultyLevel.Easy;
                    _unitFactory = new EasyLVLUnitFactory();
                    _dungeonBuilder = new EasyLVLDungeonBuilder();
                    break;
                case "2":
                    _difficulty = DifficultyLevel.Hard;
                    _unitFactory = new HardLVLUnitFactory();
                    _dungeonBuilder = new HardLVLDungeonBuilder();
                    break;
                default:
                    Console.WriteLine("Incorrect choice; the default difficulty (Easy) is set.");
                    _difficulty = DifficultyLevel.Easy;
                    _unitFactory = new EasyLVLUnitFactory();
                    _dungeonBuilder = new EasyLVLDungeonBuilder();
                    break;
            }

            Initialize();
            Console.WriteLine($"The chosen level of difficulty is {_difficulty}");
            Console.WriteLine("Entering the dungeon");
            StartGameLoop();
        }

        #region Game Loop

        private void Initialize()
        {
            Console.WriteLine("Welcome, player!");
            _dungeon = _dungeonBuilder.BuildDungeon();
            Console.WriteLine("Enter your name");
            _player = _unitFactory.CreatePlayer(Console.ReadLine());
            Console.WriteLine($"Hello {_player.Name}");
        }

        private void StartGameLoop()
        {
            var currentRoom = _dungeon;
            
            while (currentRoom.IsFinal == false) 
            {
                StartRoomEncounter(currentRoom, out var success);
                if (!success) 
                {
                    Console.WriteLine("Game over!");
                    return;
                }
                DisplayRouteOptions(currentRoom);
                while (true) 
                {
                    if (Enum.TryParse<Direction>(Console.ReadLine(), out var direction) ) 
                    {
                        currentRoom = currentRoom.Rooms[direction];
                        break;
                    }
                    else 
                    {
                        Console.WriteLine("Wrong direction!");
                    }
                }
            }
            Console.WriteLine($"Congratulations, {_player.Name}");
            Console.WriteLine("Result: ");
            Console.WriteLine(_player.ToString());
        }

        private void StartRoomEncounter(DungeonRoom currentRoom, out bool success)
        {
            success = true;
            if (currentRoom.Loot != null) 
            {
                _player.AddItemToInventory(currentRoom.Loot);
            }
            if (currentRoom.Enemy != null) 
            {
                if (_combatManager.StartCombat(_player, currentRoom.Enemy) == _player)
                {
                    _player.HandleCombatComplete();
                    LootEnemy(currentRoom.Enemy);
                }
                else 
                {
                    success = false;
                }
            }

            void LootEnemy(Unit enemy)
            {
                _player.AddItemsFromUnitToInventory(enemy);
            }
        }

        private void DisplayRouteOptions(DungeonRoom currentRoom)
        {
            Console.WriteLine("Where to go?");
            foreach (var room in currentRoom.Rooms)
            {
                Console.Write($"{room.Key} - {(int) room.Key}\t");
            }
        }

        
        #endregion
    }
}
