using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduledTaskAgent1.TSP
{
        public class Cities
    {

            private int[] x = new int[70];
            private int[] y = new int[70];
            private string[] CityNames = {
            "Stockholm",
            "Goteborg",
            "Malmo",
            "Ullaberg",
            "Malmsko",
            "Tender",
            "Hilsinko",
            "Ankeborg", 
            "Arkham",
            "Metropolis",
            "Lund",
            "Kalmar", 
            "Helsingborg", 
            "Orebro",
            "Gammelköping",
            "Sangby", 
            "Inneby", 
            "Utby", 
            "Ledsenby", 
            "Nerby", 
            "Uppsala", 
            "Vara",
            "Bli", 
            "Koping", 
            "Norrkoping", 
            "Linkoping", 
            "Kumla", 
            "Berg", 
            "Kebinekajse", 
            "Katrineholm", 
            "Vasteros",
            "Uddevalla",
            "Rayman", 
            "Nuln", 
            "Dominiara", 
            "Seth", 
            "Kalmah", 
            "Bant", 
            "Grixis", 
            "Naya", 
            "Maelstrom", 
            "Tolaria", 
            "Krosa", 
            "Llanowar", 
            "Otaria",
            "Shiv",
            "Skybreen", 
            "Goldmeadow", 
            "Sokenzan", 
            "Minamo", 
            "Velis",
            "Glimmervoid",
            "Stronghold",
            "Agyrem", 
            "Naar", 
            "Murasa", 
            "Tazeem",
            "Akoum", 
            "Gavony", 
            "Hel",
            "Asgard", 
            "Sensia", 
            "Takenuma",
            "Zephyr",
            "Windriddle",
            "Ashen", 
            "Dreampods", 
            "Quilsilver",
            "Mirrodin", 
            "Ravnica", 
            "Alara" };

            public void initArrays()
            {
                

                Random random = new Random();

                for (int i = 0; i < 70; i++)
                {
                    x[i] = random.Next(1500);
                    y[i] = random.Next(1500);

                }
            }

            public void createCities()
            {
                City[] Cities = new City[70];

                for (int i = 0; i < 70; i++)
                {
                    Cities[i] = new City(CityNames[i], x[i], y[i]);
                }
            }
    }

}

