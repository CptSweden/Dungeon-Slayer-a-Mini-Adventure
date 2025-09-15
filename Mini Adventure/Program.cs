
namespace Mini_Adventure
{
    internal class Program
    {

        static void Main(string[] args)
        {
            //A list of weapons in the game and their stats
            Weapon[] listOfWeapons = new Weapon[]
            {
                new Weapon {Name = "Dagger", AttackPower = 3, AttackSpeed = 7},
                new Weapon {Name = "Short Sword", AttackPower = 5, AttackSpeed = 5},
                new Weapon {Name = "Long Sword", AttackPower = 8, AttackSpeed = 4},
                new Weapon {Name = "Mace", AttackPower = 9, AttackSpeed = 2},
                new Weapon {Name = "Bow", AttackPower = 6, AttackSpeed = 6},
            };

            //A list of classes the player can choose to play 
            PlayerClass[] listOfClasses = new PlayerClass[]
            {
                new PlayerClass {Name = "Warrior", Hp = 100},
                new PlayerClass {Name = "Ranger", Hp = 60},
                new PlayerClass {Name = "Rogue", Hp = 40},
            };

            //A list of what type of enemies are in the game
            Enemy[] listOfEnemies = new Enemy[]
            {
                new Enemy {Name = "Goblin", Hp = 5, AttackPower = 3, AttackSpeed = 9},
                new Enemy {Name = "Skeleton", Hp = 8, AttackPower = 4, AttackSpeed = 6},
                new Enemy {Name = "Zombie", Hp = 6, AttackPower = 6, AttackSpeed = 2},
                new Enemy {Name = "Giant Rat", Hp = 5, AttackPower = 7, AttackSpeed = 5},
                new Enemy {Name = "Dire Wolf", Hp = 8, AttackPower = 8, AttackSpeed = 7},
                new Enemy {Name = "Cloaker", Hp = 8, AttackPower = 8, AttackSpeed = 7},
                new Enemy {Name = "Lich", Hp = 8 , AttackPower = 9, AttackSpeed = 8},
                new Enemy {Name = "Oni", Hp = 8, AttackPower = 9, AttackSpeed = 8},
                new Enemy {Name = "The Dreadnaught Horror", Hp = 10, AttackPower = 10, AttackSpeed  = 10}
            };

            List<Player> players = new List<Player>();

            bool gameRunning = true;
            while (gameRunning)
            {

                Console.Clear();

                Console.WriteLine("===DUNGEON SLAYER===");
                Console.WriteLine("[1] Add player");
                Console.WriteLine("[2] Show player stats/inventory");
                Console.WriteLine("[3] Go to dungeon");
                Console.WriteLine("[4] Rest");
                Console.WriteLine("[5] Exit game");
                Console.Write("Option: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        AdventureHelper.AddPlayer(players, listOfWeapons);
                        break;

                    case 2:
                        if (players.Count > 0)
                        {
                            AdventureHelper.ShowPlayerStats(players[0]);
                        }
                        else
                        {
                            Console.WriteLine("No players in the list");
                        }
                        break;

                    case 5:
                        Console.WriteLine("Exit");
                        gameRunning = false;
                        break;
                    
                }


            }



        }
    }
}
