
namespace Mini_Adventure
{
    internal class Program
    {

        static void Main(string[] args)
        {
            //A list of weapons in the game and their stats
            Weapon[] listOfWeapons = new Weapon[]
            {
                new Weapon {Name = "Dagger", MinDamage = 3, MaxDamage = 5, AttackSpeed = 10},
                new Weapon {Name = "Short Sword", MinDamage = 5, MaxDamage = 7, AttackSpeed = 7},
                new Weapon {Name = "Long Sword", MinDamage = 9, MaxDamage = 18, AttackSpeed = 4},
                new Weapon {Name = "Mace", MinDamage = 13, MaxDamage =23, AttackSpeed = 2},
                new Weapon {Name = "Bow", MinDamage = 6, MaxDamage = 12, AttackSpeed = 6},
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
                new Enemy {Name = "Goblin", Hp = 14, MinDamage = 3, MaxDamage = 5, AttackSpeed = 9},
                new Enemy {Name = "Skeleton", Hp = 21, MinDamage = 4, MaxDamage = 6, AttackSpeed = 6},
                new Enemy {Name = "Zombie", Hp = 27, MinDamage = 4, MaxDamage = 6, AttackSpeed = 2},
                new Enemy {Name = "Giant Rat", Hp = 32, MinDamage = 4, MaxDamage = 7, AttackSpeed = 5},
                new Enemy {Name = "Dire Wolf", Hp = 47, MinDamage = 5, MaxDamage = 8, AttackSpeed = 7},
                new Enemy {Name = "Cloaker", Hp = 59, MinDamage = 7, MaxDamage = 13, AttackSpeed = 7},
                new Enemy {Name = "Lich", Hp = 63, MinDamage = 8, MaxDamage = 15, AttackSpeed = 8},
                new Enemy {Name = "Oni", Hp = 77, MinDamage = 9, MaxDamage = 15, AttackSpeed = 8},
                new Enemy {Name = "The Dreadnaught Horror", Hp = 200, MinDamage= 10, MaxDamage = 25, AttackSpeed = 10}
            };
               
            

            List<Player> players = new List<Player>();
            Player currentPlayer = null;
            int currentLevel = 0;

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

                string input = Console.ReadLine();
                if (int.TryParse(input, out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            if (players.Count == 0)
                            {
                                currentPlayer = AdventureHelper.AddPlayer(players, listOfWeapons);
                            }
                            else
                            {
                                AdventureHelper.AddPlayer(players, listOfWeapons);
                            }
                            break;

                        case 2:
                            if (currentPlayer != null)
                            {
                                AdventureHelper.ShowPlayerStats(players[0]);
                            }
                            else
                            {
                                Console.WriteLine("==========");
                                Console.WriteLine("No players in the list");
                                Console.WriteLine("Press any key to continue");
                                Console.WriteLine("==========");
                                Console.ReadKey();
                            }
                            break;

                        case 3:
                            if (currentPlayer != null)
                            {
                                currentLevel = AdventureHelper.GoToDungeon(currentPlayer, listOfEnemies, currentLevel);

                            }
                            else
                            {
                                Console.WriteLine("==========");
                                Console.WriteLine("You need to create a player to be able to go to the dungeon!");
                                Console.WriteLine("Press any key to continue");
                                Console.WriteLine("==========");
                                Console.ReadKey();
                            }
                            break;

                        case 4:
                            if (currentPlayer != null)
                            {
                                AdventureHelper.Rest(currentPlayer);
                            }
                            else
                            {
                                Console.WriteLine("==========");
                                Console.WriteLine("You need to create a player first");
                                Console.WriteLine("Press any key to continue");
                                Console.WriteLine("==========");
                                Console.ReadKey();
                            }
                            break;

                        case 5:
                            Console.WriteLine("Exit");
                            gameRunning = false;
                            break;
                            
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
            }
        }
    }
}
