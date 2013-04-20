using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudService.TSP
{
    public static class Cities
    {
        public const int NUM_CITIES = 70;

        private static int[] x = new int[NUM_CITIES];
        private static int[] y = new int[NUM_CITIES];

        #region Cities
            private static string[] cityNames = {
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
            #endregion

        public static String[] CityNames
        {
          get { return cityNames; }
        }

        public static void InitArrays()
        {
           Random random = new Random();

           for (int i = 0; i < NUM_CITIES; i++)
           {
              x[i] = random.Next(1500);
              y[i] = random.Next(1500);
           }
        }

        public static City[] GetCities()
        {
            City[] cities = new City[NUM_CITIES];

            for (int i = 0; i < NUM_CITIES; i++)
            {
                cities[i] = new City(cityNames[i], x[i], y[i]);
            }

            return cities;
        }

        public static City GetCityByName(string name)
        {   
            City[] cities = GetCities();

            for (int i = 0; i < NUM_CITIES; i++)
            {
                if (cities[i].Name.Equals(name))
                    return cities[i];
            }

            return new City("Error @ GetcityByName", -1, -1);
        }

    }

}

