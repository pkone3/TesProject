using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SevenWonders.Models;

namespace SevenWonders.Classes
{
    public class Tools
    {
        
        public static int ConvertToInt(string value)
        {
            int temp;

            if (int.TryParse(value, out temp))
            {
                return temp;
            }
            else
            {
                return 0;
            }
        }

        public static int ScorePlayer(Player player)
        {
            int pointAccumulator = 0;
           
            //Add conflict tokens
            pointAccumulator += player.victoryTokens;
            pointAccumulator -= player.defeatTokens;

            //add coins
            pointAccumulator += player.coins;

            //calculate value of wonder stages
            if (player.isWonderSideA)
            {
                if (player.wonderStages >= 1 && player.wonderStages <= 2) pointAccumulator += 3;
                if (player.wonderStages == 3) pointAccumulator += 7;
                if (player.wonderName == "The Pyramids of Giza") pointAccumulator += 5;
            }
            else
            {
                if (player.wonderName == "The Colossus of Rhodes")
                {
                    switch (player.wonderStages)
                    {
                        case 1:
                            pointAccumulator += 3;
                            break;
                        case 2:
                            pointAccumulator += 7;
                            break;
                   }
                }
                if (player.wonderName == "The Lighthouse of Alexandria")
                {
                    if(player.wonderStages == 3) pointAccumulator += 7;
                }
                if (player.wonderName == "The Temple of Artemis in Ephesus")
                {
                    switch (player.wonderStages)
                    {
                        case 1:
                            pointAccumulator += 2;
                            break;
                        case 2:
                            pointAccumulator += 5;
                            break;
                        case 3:
                            pointAccumulator += 10;
                            break;
                    }
                }
                if (player.wonderName == "The Hanging Gardens of Babylon")
                {
                    if (player.wonderStages >= 1) pointAccumulator += 3;
                }
                if (player.wonderName == "The Statue of Zeus in Olympia")
                {
                    if (player.wonderStages >= 2) pointAccumulator += 5;
                }
                if (player.wonderName == "The Mausoleum of Halicarnassus")
                {
                    switch (player.wonderStages)
                    {
                        case 1:
                            pointAccumulator += 2;
                            break;
                        case 2:
                            pointAccumulator += 3;
                            break;
                    }
                }
                if (player.wonderName == "The Pyramids of Giza")
                {
                    switch (player.wonderStages)
                    {
                        case 1:
                            pointAccumulator += 3;
                            break;
                        case 2:
                            pointAccumulator += 8;
                            break;
                        case 3:
                            pointAccumulator += 13;
                            break;
                        case 4:
                            pointAccumulator += 20;
                            break;
                    }
                }
            }

            //Score structures
            pointAccumulator += player.civilianPoints;
            pointAccumulator += player.commercialPoints;

            //Score science structures
            //2 scores available, matching and sets
            pointAccumulator += player.gearCards * player.gearCards;
            pointAccumulator += player.protractorCards * player.protractorCards;
            pointAccumulator += player.tabletCards * player.tabletCards;

            int[] calcArray = { player.gearCards, player.protractorCards, player.tabletCards };
            pointAccumulator += calcArray.Min() * 7;

            //Score guild values
            if (player.hasSpies) pointAccumulator += player.Spies;
            if (player.hasMagistrates) pointAccumulator += player.Magistrates;
            if (player.hasWorkers) pointAccumulator += player.Workers;
            if (player.hasCraftsmans) pointAccumulator += player.Craftsmans;
            if (player.hasTraders) pointAccumulator += player.Traders;
            if (player.hasPhilosophers) pointAccumulator += player.Philosophers;
            if (player.hasBuilders) pointAccumulator = pointAccumulator + player.Builders + player.wonderStages;
            if (player.hasShipOwners)
            {
                pointAccumulator = pointAccumulator + player.Workers + player.Craftsmans;
                pointAccumulator += (player.hasSpies) ? 1 : 0;
                pointAccumulator += (player.hasMagistrates) ? 1 : 0;
                pointAccumulator += (player.hasWorkers) ? 1 : 0;
                pointAccumulator += (player.hasCraftsmans) ? 1 : 0;
                pointAccumulator += (player.hasTraders) ? 1 : 0;
                pointAccumulator += (player.hasPhilosophers) ? 1 : 0;
                pointAccumulator += (player.hasShipOwners) ? 1 : 0;
                pointAccumulator += (player.hasStrategists) ? 1 : 0;
                pointAccumulator += (player.hasScientists) ? 1 : 0;
            }

            return pointAccumulator;
        }
    }
}
