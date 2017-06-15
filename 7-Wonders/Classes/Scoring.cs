using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SevenWonders.Models;

namespace SevenWonders.Classes
{
    public class Scoring
    {
        
        public static int ScorePlayer(Player player)
        {
            int pointAccumulator = 0;
           
            //Add conflict tokens
            pointAccumulator += player.victoryTokens;
            pointAccumulator -= player.defeatTokens;

            //add coins
            pointAccumulator += player.coins;

            //calculate value of wonder stages
            var wonderClass = WonderFactory.CreateWonder(player.wonderName);
            pointAccumulator += wonderClass.Calculate(player.wonderStages, player.isWonderSideA);

            //Score structures
            pointAccumulator += player.civilianPoints;
            pointAccumulator += player.commercialPoints;

            //Score science structures
            //2 scores available, matching and sets
            int gears = player.gearCards;
            int protractors = player.protractorCards;
            int tablets = player.tabletCards;

            if(player.hasScientists)
            {
                switch (player.scienceSelected)
                {
                    case "Gear":
                        gears++;
                        break;
                    case "Protractor":
                        protractors++;
                        break;
                    case "Tablet":
                        tablets++;
                        break;
                }
            }
            pointAccumulator += gears * gears;
            pointAccumulator += protractors * protractors;
            pointAccumulator += tablets * tablets;

            int[] calcArray = { gears, protractors, tablets };
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

        public static int CalculateSideA(int stages, Boolean sideA)
        {
            int pointAccumulator = 0;
            if (sideA)
            { 
                if (stages >= 1 && stages <= 2) pointAccumulator += 3;
                if (stages == 3) pointAccumulator += 7;
            }
            return pointAccumulator;
        }
    }


    static class WonderFactory
    {
        public static WonderAction CreateWonder(string wonderType)
        {
            switch (wonderType)
            {
                case "The Colossus of Rhodes":
                    return new Colossus();
                case "The Lighthouse of Alexandria":
                    return new Lighthouse();
                case "The Temple of Artemis in Ephesus":
                    return new Temple();
                case "The Hanging Gardens of Babylon":
                    return new Garden();
                case "The Statue of Zeus in Olympia":
                    return new Statue();
                case "The Mausoleum of Halicarnassus":
                    return new Mausoleum();
                case "The Pyramids of Giza":
                    return new Pyramid();
                default:
                    return null;
            }
        }

    }

    abstract class WonderAction
    {
        public abstract int Calculate(int stages, Boolean sideA);
    }

    class Colossus : WonderAction
    {
        public override int Calculate(int stages, Boolean sideA)
        {
            int returnValue = 0;

            returnValue = Scoring.CalculateSideA(stages, sideA);

            if (!sideA)
            { 
                switch (stages)
                {
                    case 1:
                        returnValue += 3;
                        break;
                    case 2:
                        returnValue += 7;
                        break;
                }
            }
            return returnValue;
       }
    }

    class Lighthouse:WonderAction
    {
        public override int Calculate(int stages, bool sideA)
        {
            int returnValue = 0;

            returnValue = Scoring.CalculateSideA(stages, sideA);

            if (!sideA)
            {
                if (stages == 3) returnValue += 7;
            }

            return returnValue;
        }
    }

    class Temple : WonderAction
    {
        public override int Calculate(int stages, bool sideA)
        {
            int returnValue = 0;

            returnValue = Scoring.CalculateSideA(stages, sideA);

            if (!sideA)
            {
                switch (stages)
                {
                    case 1:
                        returnValue += 2;
                        break;
                    case 2:
                        returnValue += 5;
                        break;
                    case 3:
                        returnValue += 10;
                        break;
                }
            }
            return returnValue;
        }
    }

    class Garden : WonderAction
    {
        public override int Calculate(int stages, bool sideA)
        {
            int returnValue = 0;

            returnValue = Scoring.CalculateSideA(stages, sideA);

            if (!sideA)
            {
                if (stages >= 1) returnValue += 3;
            }

            return returnValue;
        }
    }

    class Statue : WonderAction
    {
        public override int Calculate(int stages, bool sideA)
        {
            int returnValue = 0;

            returnValue = Scoring.CalculateSideA(stages, sideA);

            if (!sideA)
            {
                if (stages >= 2) returnValue += 5;
            }

            return returnValue;
        }
    }

    class Mausoleum : WonderAction
    {
        public override int Calculate(int stages, bool sideA)
        {
            int returnValue = 0;

            returnValue = Scoring.CalculateSideA(stages, sideA);

            if (!sideA)
            {
                switch (stages)
                {
                    case 1:
                        returnValue += 2;
                        break;
                    case 2:
                        returnValue += 3;
                        break;
                }
            }

            return returnValue;
        }
    }

    class Pyramid : WonderAction
    {
        public override int Calculate(int stages, bool sideA)
        {
            int returnValue = 0;

            returnValue = Scoring.CalculateSideA(stages, sideA);
            if (sideA)
            {
                if (stages >= 2) returnValue += 5;
            }
            else
            {
                switch (stages)
                {
                    case 1:
                        returnValue += 3;
                        break;
                    case 2:
                        returnValue += 8;
                        break;
                    case 3:
                        returnValue += 13;
                        break;
                    case 4:
                        returnValue += 20;
                        break;
                }
            }

            return returnValue;
        }
    }
}
