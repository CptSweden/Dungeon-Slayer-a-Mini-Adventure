
using System.Globalization;
using System.Security;

namespace Mini_Adventure
{
    public class Weapon
    {
        public string Name;
        public int AttackPower;
        public int AttackSpeed;
        public List<Weapon> Weapons;


        public static bool CanUseWeapon(Weapon weapon, PlayerClass playerClass)
        {
            switch (playerClass.Name)
            {
                case "Warrior":
                    string[] warriorWeapons = { "Short Sword", "Long Sword", "Mace"};
                    foreach (string warriorWeapon in warriorWeapons)
                    {
                        if (weapon.Name == warriorWeapon)
                        {
                            return true;
                        }
                    }
                    break;

                case "Ranger":
                    string[] rangerWeapons = { "Dagger", "Short Sword", "Bow"};
                    foreach(string rangerWeapon in rangerWeapons)
                    {
                        if (weapon.Name == rangerWeapon)
                        {
                            return true;
                        }
                    }
                    break;

                case "Rogue":
                    string[] rogueWeapons = { "Dagger", "Short Sword"};
                    foreach(string rogueWeapon in rogueWeapons)
                    {
                        if (weapon.Name == rogueWeapon)
                        {
                            return true;
                        }
                    }
                    break;
            }
            return false;

        }

    }


}
