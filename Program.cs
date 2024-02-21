

public delegate int AttackDelegate(int damage, DamageType type, double criticalChance, double dodgeChance);
public enum DamageType
{
    Physical,
    Magical
}

public class Hero
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int AttackPower { get; set; }
    public double ResistanceToPhysical { get; set; }
    public double ResistanceToMagical { get; set; }
    public double CriticalChance { get; set; }
    public double DodgeChance { get; set; }

    public AttackDelegate AttackHandler;

    public Hero(string name, int health, int attackPower, double resistanceToPhysical, double resistanceToMagical, double criticalChance, double dodgeChance)
    {
        Name = name;
        Health = health;
        AttackPower = attackPower;
        ResistanceToPhysical = resistanceToPhysical;
        ResistanceToMagical = resistanceToMagical;
        CriticalChance = criticalChance;
        DodgeChance = dodgeChance;
    }

    public int Attack(Hero target, DamageType damageType)
    {
        int damage = CalculateDamage(damageType);
        return target.TakeDamage(damage);
    }

    public int TakeDamage(int damage)
    {
        int finalDamage = (int)(damage * (1 - (ResistanceToPhysical + ResistanceToMagical)));
        Health -= finalDamage;
        return finalDamage;
    }

    private int CalculateDamage(DamageType damageType)
    {
       
        Random random = new Random();
        double randomValue = random.NextDouble();

        if (randomValue < DodgeChance)
        {
            Console.WriteLine($"{Name} ухилився від атаки!");
            return 0;
        }

        int baseDamage = AttackPower;
        if (randomValue < CriticalChance)
        {
            Console.WriteLine($" Critick attack {Name}!");
            baseDamage *= 2;
        }

        return (damageType == DamageType.Magical) ? (int)(baseDamage * ResistanceToMagical) : baseDamage;
    }
}

public class Warrior : Hero
{
    
    public int Armor { get; set; }

    public Warrior(string name, int health, int attackPower, double resistanceToPhysical, double resistanceToMagical, double criticalChance, double dodgeChance, int armor)
        : base(name, health, attackPower, resistanceToPhysical, resistanceToMagical, criticalChance, dodgeChance)
    {
        Armor = armor;
    }
}

public class Mage : Hero
{
   
    public double BonusDamageDuringStorm { get; set; }

    public Mage(string name, int health, int attackPower, double resistanceToPhysical, double resistanceToMagical, double criticalChance, double dodgeChance, double bonusDamageDuringStorm)
        : base(name, health, attackPower, resistanceToPhysical, resistanceToMagical, criticalChance, dodgeChance)
    {
        BonusDamageDuringStorm = bonusDamageDuringStorm;
    }
}

public class Archer : Hero
{
    public int BonusDamageOnTripleShot { get; set; }

    public Archer(string name, int health, int attackPower, double resistanceToPhysical, double resistanceToMagical, double criticalChance, double dodgeChance, int bonusDamageOnTripleShot)
        : base(name, health, attackPower, resistanceToPhysical, resistanceToMagical, criticalChance, dodgeChance)
    {
        BonusDamageOnTripleShot = bonusDamageOnTripleShot;
    }
}

public class Program
{
    public static void Main(string[] args)
    {

        Warrior warrior = new Warrior("Воїн", 1000, 140, 0.2, 0.1, 0.1, 0.2, 10);
        Mage mage = new Mage("Маг", 850, 170, 0.1, 0.2, 0.2, 0.1, 0.3);
        Archer archer = new Archer("Лучник", 990, 150, 0.15, 0.15, 0.3, 0.1, 15);


        Console.WriteLine($@" ____    __                    __        ____              __    __    ___ ", Console.ForegroundColor = ConsoleColor.Blue);
       Console.WriteLine($@"/\  _`\ /\ \__                /\ \__    /\  _`\           /\ \__/\ \__/\_ \ ");
        Console.WriteLine($@"\ \,\L\_\ \ ,_\    __     _ __\ \ ,_\   \ \ \L\ \     __  \ \ ,_\ \ ,_\//\ \      __");
        Console.WriteLine($@" \/_\__ \\ \ \/  /'__`\  /\`'__\ \ \/    \ \  _ <'  /'__`\ \ \ \/\ \ \/ \ \ \   /'__`\ ");
        Console.WriteLine($@"  /\ \L\ \ \ \_/\ \L\.\_\ \ \/ \ \ \_    \ \ \L\ \/\ \L\.\_\ \ \_\ \ \_ \_\ \_/\  __/ ", Console.ForegroundColor = ConsoleColor.Yellow);
        Console.WriteLine($@"  \ `\____\ \__\ \__/.\_\\ \_\  \ \__\    \ \____/\ \__/.\_\\ \__\\ \__\/\____\ \____\ ");
        Console.WriteLine($@"  \/_____/\/__/\/__/\/_/ \/_/   \/__/     \/___/  \/__/\/_/ \/__/ \/__/\/____/\/____/ ");




        Console.WriteLine("Warrior", Console.ForegroundColor = ConsoleColor.Red);
        warrior.AttackHandler = (damage, type, criticalChance, dodgeChance) =>
        {
            int modifiedDamage = (int)(damage * (1 - dodgeChance));
            Console.WriteLine("___________________________________________________________________");
            Console.WriteLine($"{warrior.Name} атакує з фізичним уроном {modifiedDamage} одиниць.");
            Console.WriteLine("___________________________________________________________________");
            return modifiedDamage;
        };

        Console.WriteLine("Mage:", Console.ForegroundColor = ConsoleColor.Red);
        mage.AttackHandler = (damage, type, criticalChance, dodgeChance) =>
        {
            int modifiedDamage = (int)(damage * (1 - dodgeChance + mage.BonusDamageDuringStorm));
            Console.WriteLine("___________________________________________________________________");
            Console.WriteLine($"{mage.Name} атакує з магічним уроном {modifiedDamage} одиниць.");
            Console.WriteLine("___________________________________________________________________");
            return modifiedDamage;
        };

        archer.AttackHandler = (damage, type, criticalChance, dodgeChance) =>
        {
            int modifiedDamage = (int)(damage * (1 - dodgeChance + archer.BonusDamageOnTripleShot));
            Console.WriteLine("___________________________________________________________________");
            Console.WriteLine($"{archer.Name} атакує з фізичним уроном {modifiedDamage} одиниць.");
            Console.WriteLine("___________________________________________________________________");
            return modifiedDamage;
        };

        // Розпочніть бій
        StartBattle(warrior, mage, archer);

        Console.ReadLine();
    }

    public static void StartBattle(params Hero[] heroes)
    {
        Random random = new Random();
        int round = 1;

        while (AnyHeroAlive(heroes))
        {
            Console.WriteLine($"\nRaund {round}:");

            foreach (Hero attacker in heroes)
            {
                if (!attacker.Health.Equals(0))
                {
                    Hero target = GetRandomAliveTarget(heroes, attacker);
                    DamageType damageType = (random.Next(2) == 0) ? DamageType.Physical : DamageType.Magical;
                    int damageDealt = attacker.Attack(target, damageType);

                    Console.WriteLine($"{attacker.Name} завдав {target.Name} {damageDealt} одиниць урону. Здоров'я {target.Name}: {target.Health}");

                    if (target.Health <= 0)
                    {
                        Console.WriteLine($"{target.Name} вбито!");
                    }
                }
            }

            round++;
        }

        Console.WriteLine("\nBattle fineshed!");
        PrintBattleStats(heroes);
    }

    public static bool AnyHeroAlive(Hero[] heroes)
    {
        foreach (Hero hero in heroes)
        {
            if (hero.Health > 0)
            {
                return true;
            }
        }
        return false;
    }

    public static Hero GetRandomAliveTarget(Hero[] heroes, Hero attacker)
    {
        Random random = new Random();
        Hero target;

        do
        {
            target = heroes[random.Next(heroes.Length)];
        } while (target.Equals(attacker) || target.Health.Equals(0));

        return target;
    }

    public static void PrintBattleStats(Hero[] heroes)
    {
        Console.WriteLine("\nStatistic batlle:");
        foreach (Hero hero in heroes)
        {
            Console.WriteLine($"{hero.Name}: Spent  {hero.Health} health");
        }
    }
}