using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SevenWonders.Models
{
    public class Player
    {
        public int id;
        public string name;
        public int victoryTokens;
        public int defeatTokens;
        public int coins;
        public int wonderId;
        public string wonderName;
        public Boolean isWonderSideA;
        public int wonderStages;
        public int civilianPoints;
        public int gearCards;
        public int protractorCards;
        public int tabletCards;
        public int commercialPoints;
        public Boolean hasSpies;
        public Boolean hasMagistrates;
        public Boolean hasWorkers;
        public Boolean hasCraftsmans;
        public Boolean hasTraders;
        public Boolean hasPhilosophers;
        public Boolean hasBuilders;
        public Boolean hasShipOwners;
        public Boolean hasStrategists;
        public Boolean hasScientists;
        public int Spies;
        public int Magistrates;
        public int Workers;
        public int Craftsmans;
        public int Traders;
        public int Philosophers;
        public int Builders;
        public int ShipOwners;
        public int Strategists;
        public string scienceSelected;

    }

    public static class Persistent
    {
        public static List<Player> PlayerList = new List<Player>();
        public static List<Scores> PlayerScores = new List<Scores>();
        public static List<Wonder> Wonders = new List<Wonder>();
    }

}
