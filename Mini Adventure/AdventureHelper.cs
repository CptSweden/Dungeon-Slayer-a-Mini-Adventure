
namespace Mini_Adventure
{
     public class AdventureHelper
    {
        //Method for adding a player to the game
        public static Player AddPlayer(List<Player> players, Weapon[] listOfWeapons)
        {
            Console.Clear();

            if (players.Count > 0)
            {
                Console.WriteLine("==========");
                Console.WriteLine("You have already created a player. You can only have one player.");
                Console.WriteLine("Press any key to return to the meny");
                Console.WriteLine("==========");
                Console.ReadKey();
                return null;
            }
            //Making the user to choose a name for the player
            Console.WriteLine("==========");
            Console.Write("Enter your player name: ");
            Console.WriteLine();
            Console.WriteLine("==========");
            string name = Console.ReadLine();
            Console.Clear();

            //Tells the user what classes are available 
            Console.WriteLine("==========");
            Console.WriteLine("---Classes---");
            Console.WriteLine("[1] Warrior");
            Console.WriteLine("[2] Ranger");
            Console.WriteLine("[3] Rogue");

            PlayerClass playerclass;

            //A loop to ensure a valid class is choosen
            while (true)
            {
                Console.WriteLine("==========");
                Console.Write("Enter what class you want to play: ");
                Console.WriteLine();
                Console.WriteLine("==========");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        playerclass = new PlayerClass { Name = "Warrior", Hp = 100 }; 
                        break;
                    case 2:
                        playerclass = new PlayerClass { Name = "Ranger", Hp = 60 };
                        break;
                    case 3:
                        playerclass = new PlayerClass { Name = "Rogue", Hp = 40 };
                        break;
                    default:
                        Console.WriteLine("==========");
                        Console.WriteLine("Invalid input");
                        Console.WriteLine("==========");
                        continue; //Restarts the loop
                }

                Console.Clear();
                Console.WriteLine("==========");
                Console.WriteLine($"Choosen Class: {playerclass.Name}");
                Console.WriteLine("Press any key to continue");
                Console.WriteLine("==========");
                Console.ReadKey();
                break;

            }
            //Tells the user what class specific weapons are available
            Console.Clear();
            Console.WriteLine("==========");
            Console.WriteLine("---Weapons---");
            Console.WriteLine("==========");

            List<Weapon> usableWeapons = new List<Weapon>();

            Console.WriteLine("Avalible weapons:");            
            for(int i = 0; i < listOfWeapons.Length; i++)
            {
                if (Weapon.CanUseWeapon(listOfWeapons[i], playerclass))
                {
                    usableWeapons.Add(listOfWeapons[i]);
                    Console.WriteLine($"[{usableWeapons.Count}] {listOfWeapons[i].Name}");
                }
            }

            //Making the user to choose a starting weapon based on what class the user has choosen
            Weapon selectedWeapon = null;
            while (selectedWeapon == null)
            {
                Console.WriteLine("==========");
                Console.WriteLine("Select you starting weapon:");
                Console.WriteLine("==========");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice))                
                {
                    if (choice > 0 && choice <= usableWeapons.Count)
                    {
                        selectedWeapon = usableWeapons[choice - 1];
                    }
                    else
                    {
                        Console.WriteLine("==========");
                        Console.WriteLine("Invalid input");
                        Console.WriteLine("==========");
                    }

                }

                if (selectedWeapon == null) 
                {
                    Console.WriteLine("==========");
                    Console.WriteLine("Invalid weapon choice. Try again!");
                    Console.WriteLine("==========");
                }
            }

            Player player = new Player
            {
                Name = name,
                Class = playerclass,
                Weapon = selectedWeapon,
                CurrentHP = playerclass.Hp,
            };

            players.Add(player);
            return player;
        }
        
        //show the stats of the player
        public static void ShowPlayerStats(Player player)
        {
            Console.Clear();
            Console.WriteLine("==========");
            Console.WriteLine("---Player Stats---");
            Console.WriteLine($"Name: {player.Name}");
            Console.WriteLine($"Class: {player.Class.Name}");
            Console.WriteLine($"Current HP: {player.CurrentHP}");
            Console.WriteLine($"Current Weapon: {player.Weapon.Name}");
            Console.WriteLine($"Weapon Damage: Min: [{player.Weapon.MinDamage}] | Max: [{player.Weapon.MaxDamage}]");
            Console.WriteLine($"Weapon Speed: {player.Weapon.AttackSpeed}");
            Console.WriteLine("==========");
            Console.WriteLine("Press any key to return to the meny");
            Console.WriteLine("==========");
            Console.ReadKey();
        }

        public static int GoToDungeon(Player player, Enemy[] listOfEnemies, int startLevel)
        {

            if (player == null || player.Weapon == null)
            {              
                return 0;
            }
            
            Console.Clear();

            Enemy[] regularEnemies = new Enemy[listOfEnemies.Length - 1];
            Array.Copy(listOfEnemies, regularEnemies, listOfEnemies.Length - 1);
            Enemy finalBoss = listOfEnemies[listOfEnemies.Length - 1];

            Enemy[] shuffledEnemies = new Enemy[regularEnemies.Length];
            Array.Copy(regularEnemies, shuffledEnemies, regularEnemies.Length);
            Random random = new Random();
            int n = shuffledEnemies.Length;

            for (int i = n - 1;i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                Enemy temp = shuffledEnemies[i];
                shuffledEnemies[i] = shuffledEnemies[j];
                shuffledEnemies[j] = temp;
            }

            for (int i = startLevel; i < shuffledEnemies.Length; i++)
            {
                Console.WriteLine("==========");
                Console.WriteLine($"---Level {i + 1}---");
                Console.WriteLine($"A {shuffledEnemies[i].Name} appears!");

                Fight(player, shuffledEnemies[i]);

                if (player.CurrentHP <= 0) 
                {
                    Console.WriteLine("You have been defeated!");
                    Console.WriteLine("==========");
                    Console.WriteLine("Press any key to return to the meny");
                    Console.WriteLine("==========");
                    Console.ReadKey();
                    return 0;
                }

                else
                {
                    Console.WriteLine("==========");
                    Console.WriteLine($"You have defetead {shuffledEnemies[i].Name}");
                    Console.WriteLine("==========");

                    if (i < shuffledEnemies.Length - 1)
                    {
                        while (true)
                        {
                            Console.WriteLine("---[Yes/No] Do you wish to continue into the dungeon?---");
                            Console.WriteLine("==========");
                            string choice = Console.ReadLine().ToLower();
                            if (choice == "yes")
                            {
                                break;
                            }
                            else if (choice == "no")
                            {
                                Console.WriteLine("==========");
                                Console.WriteLine("You have retretead from the dungeon.");
                                Console.WriteLine("Press any key to return to the meny");
                                Console.WriteLine("==========");
                                Console.ReadKey();
                                return i + 1;
                            }
                            else
                            {
                                Console.WriteLine("==========");
                                Console.WriteLine("Invalid input");
                                Console.WriteLine("==========");
                            }
                        }
                    }
                }
            }

            Console.WriteLine("==========");
            Console.WriteLine("---BOSS FIGHT---");
            Console.WriteLine("You have reached the end of the dungeon.");
            Console.WriteLine("Prepere for the boss fight");
            Console.WriteLine($"Final Boss: {finalBoss.Name}");
            Fight(player, finalBoss);

            if (player.CurrentHP > 0)
            {
                Console.WriteLine("==========");
                Console.WriteLine("---CONGRATULATION---");
                Console.WriteLine("You have cleared the dungeon!");
                Console.WriteLine("==========");
            }
            else
            {
                Console.WriteLine("==========");
                Console.WriteLine("You have been defeated by the Boss!");
                Console.WriteLine("==========");
            }

            Console.WriteLine("==========");
            Console.WriteLine("Press any key to return to the meny");
            Console.WriteLine("==========");
            Console.ReadKey();
            return 0;
        }

       public static void Fight(Player player, Enemy enemy)
        {
            
            int enemyHp = enemy.Hp;
            Random random = new Random();

            Console.WriteLine($"Your HP: {player.CurrentHP}");
            Console.WriteLine($"{enemy.Name}'s HP: {enemyHp}");
            Console.WriteLine("==========");
            Console.WriteLine("Press any key to start the fight");
            Console.WriteLine("==========");
            Console.ReadKey();
            Console.Clear();

            bool playerStarts = player.Weapon.AttackSpeed >= enemy.AttackSpeed;

            while (player.CurrentHP > 0 && enemyHp > 0) 
            {
                Console.WriteLine("==========");
                Console.WriteLine("---FIGHT START---");

                if (playerStarts)
                {
                    int playerDamage = random.Next(player.Weapon.MinDamage, player.Weapon.MaxDamage + 1);
                    enemyHp -= playerDamage;
                    if (enemyHp < 0)
                    {
                        enemyHp = 0;
                    }
                    Console.WriteLine($"You attacked the {enemy.Name} and did {playerDamage} damage.");

                    if (enemyHp <= 0)
                    {
                        break;    
                    }

                    int enemyDamage = random.Next(enemy.MinDamage, enemy.MaxDamage + 1);
                    player.CurrentHP -= enemyDamage;
                    if (player.CurrentHP <= 0)
                    {
                        player.CurrentHP = 0;
                    }
                    Console.WriteLine($"The {enemy.Name} did {enemyDamage} damage on you");
                }
                else
                {
                    int enemyDamage = random.Next(enemy.MinDamage, enemy.MaxDamage +1);
                    player.CurrentHP -= enemyDamage;
                    if (player.CurrentHP < 0)
                    {
                        player.CurrentHP = 0;
                    }
                    Console.WriteLine($"The {enemy.Name} did {enemyDamage} damage on you");

                    if (player.CurrentHP <= 0)
                    {
                        break;
                    }

                    int playerDamage = random.Next(player.Weapon.MinDamage, player.Weapon.MaxDamage + 1);
                    enemyHp -= playerDamage;
                    if (enemyHp < 0)
                    {
                        enemyHp = 0;
                    }
                    Console.WriteLine($"You attacked the {enemy.Name} and did {playerDamage} damage.");

                }

                Console.WriteLine($"Your current HP is: {player.CurrentHP}");
                Console.WriteLine($"{enemy.Name}'s HP is {enemyHp}");
                Console.WriteLine("==========");
                Console.WriteLine("Press any key to continue the fight");
                Console.WriteLine("==========");
                Console.ReadKey();
                Console.Clear();

            }

            Console.WriteLine("The fight is over");
            Console.WriteLine($"Your current HP: {player.CurrentHP}");

        }

        public static void Rest(Player player)
        {
            Console.Clear();

            if (player == null)
            {               
                return;
            }
            Console.WriteLine("==========");
            Console.WriteLine("AT THE CAMPFIRE");
            Console.WriteLine("==========");
            Console.WriteLine("You are now resting and starts to feel healed");
            player.CurrentHP = player.Class.Hp;
            Console.WriteLine($"Your HP has restored to {player.CurrentHP}");
            Console.WriteLine("==========");
            Console.WriteLine("Press any key to continue");
            Console.WriteLine("==========");
            Console.ReadKey();
        }

        
    }
}
