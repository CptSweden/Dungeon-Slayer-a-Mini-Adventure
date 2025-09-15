
namespace Mini_Adventure
{
     public class AdventureHelper
    {
        //Method for adding a player to the game
        public static void AddPlayer(List<Player> players, Weapon[] listOfWeapons)
        {
            if (players.Count > 0)
            {
                Console.WriteLine("You have already created a player. You can only have one player.");
                Console.WriteLine("Press any key to return to the meny");
                Console.ReadKey();
                return;
            }
            //Making the user to choose a name for the player
            Console.Write("Enter your player name: ");
            string name = Console.ReadLine();

            //Tells the user what classes are available 
            Console.WriteLine("---Classes---");
            Console.WriteLine("[1] Warrior");
            Console.WriteLine("[2] Ranger");
            Console.WriteLine("[3] Rogue");

            PlayerClass playerclass;
            string playerClassName;

            //A loop to ensure a valid class is choosen
            while (true)
            { 
            Console.Write("Enter what class you want to play: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        playerClassName = "Warrior";
                        break;
                    case "2":
                        playerClassName = "Ranger";
                        break;
                    case "3":
                        playerClassName = "Rogue";
                        break;
                    default:
                        Console.WriteLine("Invalid inpout");
                        continue; //Restarts the loop
                }

                playerclass = new PlayerClass { Name = playerClassName };
                Console.WriteLine($"Choosen Class: {playerClassName}");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                break;

            }
            //Tells the user what class specific weapons are available
            Console.WriteLine("---Weapons---");
            Console.WriteLine("Avalible weapons:");          
            foreach (Weapon weapon in listOfWeapons)
            {
                if (Weapon.CanUseWeapon(weapon, playerclass))
                {
                    Console.WriteLine(weapon.Name);
                }
            }

            //Making the user to choose a starting weapon based on what class the user has choosen
            Weapon selectedWeapon = null;
            while (selectedWeapon == null)
            {
                Console.WriteLine("Select you starting weapon:");
                string weaponName = Console.ReadLine();

                foreach (Weapon weapon in listOfWeapons)
                {
                    if (weapon.Name == weaponName && Weapon.CanUseWeapon(weapon, playerclass)) 
                    {
                        selectedWeapon = weapon;
                        break;
                    }

                }

                if (selectedWeapon == null) 
                {
                    Console.WriteLine("Invalid weapon choice. Try again!");
                }
            }

            Player player = new Player
            {
                Name = name,
                Class = playerClassName,
                Weapon = selectedWeapon,
            };

            players.Add(player);
        }
        
        //show the stats of the player
        public static void ShowPlayerStats(Player player)
        {
            Console.Clear();
            Console.WriteLine("---Player Stats---");
            Console.WriteLine($"Name: {player.Name}");
            Console.WriteLine($"Class: {player.Class}");
            Console.WriteLine($"Current HP: {player.Class.Hp}");
            Console.WriteLine($"Current Weapon: {player.Weapon.Name}");
            Console.WriteLine($"Weapon Power: {player.Weapon.AttackPower}");
            Console.WriteLine($"Weapon Speed: {player.Weapon.AttackSpeed}");
            Console.WriteLine("Press any key to return to the meny");
            Console.ReadKey();
        }

        public static void GoToDungeon(Player player, Enemy[] listOfEnemies)
        {
            Console.Clear();

            if (player == null || player.Weapon == null)
            {
                Console.WriteLine("You need to create a player to be able to go to the dungeon!");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                return;
            }

            Enemy[] shuffledEnemies = new Enemy[listOfEnemies.Length];
            Array.Copy(listOfEnemies, shuffledEnemies, listOfEnemies.Length);

            Random random = new Random();
            int n = shuffledEnemies.Length;
            for (int i = n - 1;i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                Enemy temp = shuffledEnemies[i];
                shuffledEnemies[i] = shuffledEnemies[j];
                shuffledEnemies[j] = temp;
            }

            for (int i = 0; i < shuffledEnemies.Length; i++)
            {
                Console.WriteLine($"---Level {i + 1}---");
                Console.WriteLine($"A {shuffledEnemies[i].Name} appears!");

                bool playerAlive = Fight(player, shuffledEnemies[i]);

                if (!playerAlive) 
                {
                    Console.WriteLine("You have been defeated!");
                    Console.WriteLine("Press any key to return to the meny");
                    Console.ReadKey();
                    return;
                }

                else
                {
                    Console.WriteLine($"You have defetead {shuffledEnemies[i].Name}");

                    if (i < shuffledEnemies.Length - 1)
                    {

                        Console.WriteLine("Do you wish to continue into the dungeon?");
                        string choice = Console.ReadLine().ToLower();
                        if (choice != "Yes")
                        {
                            Console.WriteLine("You have retretead from the dungeon.");
                            Console.WriteLine("Press any key to return to the meny");
                            Console.ReadKey();
                            return;
                        }
                    }
                }
            }

            Console.WriteLine("---BOSS FIGHT---");
            Console.WriteLine("You have reached the end of the dungeon.");
            Console.WriteLine("Prepere for the boss fight");
            Console.WriteLine($"Final Boss: {shuffledEnemies[shuffledEnemies.Length - 1].Name}");
            bool defeatedBoss = Fight(player, shuffledEnemies[shuffledEnemies.Length - 1]);

            if (defeatedBoss)
            {
                Console.WriteLine("CONGRATULATION");
                Console.WriteLine("You have cleared the dungeon!");
            }
            else
            {
                Console.WriteLine("You have been defeated by the Boss!");
            }

            Console.WriteLine("Press any key to return to the meny");
        }

       public static bool Fight(Player player, Enemy enemy)
        {
            int playerHp = player.Class.Hp;
            int enemyHp = enemy.Hp;

            Console.WriteLine($"Your HP: {playerHp}");
            Console.WriteLine($"{enemy.Name}'s HP: {enemyHp}");
            Console.WriteLine("Press any key to start the fight");
            Console.ReadKey();

            bool playerStarts = player.Weapon.AttackSpeed >= enemy.AttackSpeed;

            while (playerHp > 0 && enemyHp > 0) 
            {
                if (playerStarts)
                {
                    int playerDamage = player.Weapon.AttackPower;
                    enemyHp -= playerDamage;
                    Console.WriteLine($"You attacked the {enemy.Name} and did {playerDamage} damage.");

                    if (enemyHp >= 0)
                    {
                        break;
                    }

                    int enemyDamage = enemy.AttackPower;
                    playerHp -= enemyDamage;
                    Console.WriteLine($"The {enemy.Name} did {enemyDamage} damage on you");
                }
                else
                {
                    int enemyDamage = enemy.AttackPower;
                    playerHp -= enemyDamage;
                    Console.WriteLine($"The {enemy.Name} did {enemyDamage} damage on you");

                    if (playerHp >= 0)
                    {
                        break;
                    }

                    int playerDamage = player.Weapon.AttackPower;
                    enemyHp -= playerDamage;
                    Console.WriteLine($"You attacked the {enemy.Name} and did {playerDamage} damage.");

                }

                Console.WriteLine($"Your current HP is: {playerHp}");
                Console.WriteLine($"{enemy.Name}'s HP is {enemyHp}");
                Console.WriteLine("Press any key to continue the fight");
                Console.ReadKey();

            }

            Console.WriteLine("The fight is over");
            Console.WriteLine($"Your current HP: {playerHp}");

            return playerHp > 0;
        }

        
    }
}
