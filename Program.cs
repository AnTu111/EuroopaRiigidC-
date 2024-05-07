using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Dictionary<string, string> riik_pealinn = new Dictionary<string, string>();
        Dictionary<string, string> pealinn_riik = new Dictionary<string, string>();
        List<string> riigid = new List<string>();

        FailistToDict("riigid_pealinnad.txt", riik_pealinn, pealinn_riik, riigid);

        while (true)
        {
            Console.WriteLine("Valige allolevatest tehingutest:");
            Console.WriteLine("1. - Näita pealinn riigi järgi");
            Console.WriteLine("2. - Näita riiki pealinna järgi");
            Console.WriteLine("3. - Lisage uus sõna");
            Console.WriteLine("4. - Parandus kui viga on tuvastatud");
            Console.WriteLine("5. - Mäng (5 küsimust)");
            Console.WriteLine("6. - Lõpp");

            string valimine = Console.ReadLine();

            switch (valimine)
            {
                case "1":
                    Console.Write("Siseta riik: ");
                    string riik = Console.ReadLine().ToLower();
                    if (riik_pealinn.ContainsKey(riik))
                        Console.WriteLine(riik_pealinn[riik]);
                    else
                        Console.WriteLine("Sellist riiki ei leitud.");
                    break;
                case "2":
                    Console.Write("Siseta pealinn: ");
                    string pealinn = Console.ReadLine().ToLower();
                    if (pealinn_riik.ContainsKey(pealinn))
                        Console.WriteLine(pealinn_riik[pealinn]);
                    else
                        Console.WriteLine("Sellist pealinna ei leitud.");
                    break;
                case "3":
                    Console.Write("Sistage uus riik: ");
                    string newRiik = Console.ReadLine();
                    Console.Write("Sistage uus pealinn: ");
                    string newPealinn = Console.ReadLine();
                    ElLisamine(riik_pealinn, newRiik.ToLower(), newPealinn.ToLower(), "riigid_pealinnad.txt");
                    break;
                case "4":
                    Console.Write("Sisestage riigi nimi: ");
                    string uus = Console.ReadLine().ToLower();
                    if (riigid.Contains(uus))
                    {
                        Console.WriteLine($"Sisestatud riik {uus} ja tema pealinn on {riik_pealinn[uus]}. Sisestage uus pealinn: ");
                        string uusPealinn = Console.ReadLine();
                        riik_pealinn[uus] = uusPealinn;
                        Console.WriteLine($"Riigi {uus} pealinn on: {uusPealinn}.");
                        Lisamine(riik_pealinn, "riigid_pealinnad.txt");
                    }
                    else
                    {
                        Console.WriteLine("Sellist riiki ei leitud.");
                    }
                    break;
                case "5":
                    Mang(riik_pealinn, riigid);
                    break;
                case "6":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Vale valik.");
                    break;
            }
        }
    }

    static void FailistToDict(string f, Dictionary<string, string> riik_pealinn, Dictionary<string, string> pealinn_riik, List<string> riigid)
    //метод принадлежит классу ,а не конктретному объекту
    {
        using (StreamReader sr = new StreamReader(f)) //обеспечивает автоматическое закрытие ресурсов после их использования
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] parts = line.Split('-');
                string riik = parts[0].Trim().ToLower();
                string pealinn = parts[1].Trim().ToLower();
                riik_pealinn[riik] = pealinn;
                pealinn_riik[pealinn] = riik;
                riigid.Add(riik);
            }
        }
    }

    static void ElLisamine(Dictionary<string, string> riik_pealinn, string v_riik, string v_pealinn, string file)
    {
        if (riik_pealinn.ContainsKey(v_riik) == false) //если записи в словаре нету
        {
            riik_pealinn[v_riik] = v_pealinn;
            Console.WriteLine($"Riik {v_riik} pealinnaga {v_pealinn} lisatud.");
            Lisamine(riik_pealinn, file);
        }
        else
        {
            Console.WriteLine($"Riik {v_riik} on juba olemas.");
        }
    }

    static void Lisamine(Dictionary<string, string> riik_pealinn, string file)
    {
        using (StreamWriter sw = new StreamWriter(file))
        {
            foreach (var i in riik_pealinn)
            {
                sw.WriteLine($"{i.Key} - {i.Value}");
            }
        }
    }

    static void Mang(Dictionary<string, string> riik_pealinn, List<string> riigid)
    {
        int oige = 0;
        int vale = 0;
        int kusimused = 0;
        Random rnd = new Random();

        while (kusimused < 5)
        {
            string n = riigid[rnd.Next(riigid.Count)];
            Console.Write($"Pealinn riigis {n}: ");
            string vastus = Console.ReadLine().ToLower();
            string oigePealinn = riik_pealinn[n];

            if (vastus == oigePealinn)
            {
                oige=oige+1;
                kusimused = kusimused + 1;
                Console.WriteLine("Õige!");
            }
            else
            {
                kusimused = kusimused + 1;
                vale =vale+1;
                Console.WriteLine($"Vale! Õige vastus: {oigePealinn}");
            }
        }

        double prots = (double)oige / (oige + vale) * 100;
        Console.WriteLine($"Teie võit protsentide on {prots}");
    }
}
