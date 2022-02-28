using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFTBuilder
{
    //Calling constructor on this class with a list of champions will output the top compositions
    internal class SearchTree
    {
        public List<List<Champion>> TopCompositions;
        public List<Champion> AllChamps;
        int traitCountAllowable;

        public SearchTree(List<Champion> champList)
        {
            TopCompositions = new List<List<Champion>>();
            AllChamps = champList;
            traitCountAllowable = 0;
            BuildList();
        }

        public void BuildList()
        {
            foreach (Champion champion in AllChamps)
            {
                if (champion.TraitThree != Traits.Blank)
                {
                    LayerIterator(new List<Champion> { champion }, 1);
                }
            }
        }
        private void LayerIterator(List<Champion> currentFieldedChampList, int champCount)
        {
            if (champCount < 9)
            {
                int throwAwayOne = StateAnalysis(currentFieldedChampList, out int traitTierCount, out int throwAwayTwo);

                if (champCount > 7)
                {
                    if (traitTierCount >= champCount + 2)
                    {
                        List<List<Champion>> nextLists = GenerateNextChampionLists(currentFieldedChampList);
                        foreach (List<Champion> champList in nextLists)
                        {
                            LayerIterator(champList, champCount + 1);
                        }
                    }

                }
                else if(champCount > 4)
                {
                    if (traitTierCount >= champCount + 1)
                    {
                        List<List<Champion>> nextLists = GenerateNextChampionLists(currentFieldedChampList);
                        foreach (List<Champion> champList in nextLists)
                        {
                            LayerIterator(champList, champCount + 1);
                        }
                    }
                }
                else
                {
                    if (traitTierCount >= champCount)
                    {
                        List<List<Champion>> nextLists = GenerateNextChampionLists(currentFieldedChampList);
                        foreach (List<Champion> champList in nextLists)
                        {
                            LayerIterator(champList, champCount + 1);
                        }
                    }
                }
                
                
                             
            }
            else
            {
                CompareToCurrentTopCompositions(currentFieldedChampList);
            }
        }

        public List<List<Champion>> GenerateNextChampionLists(List<Champion> champList)
        {
            List<List<Champion>> nextLists = new List<List<Champion>>();
            List<Champion> champsToAdd = new List<Champion>();

            bool isClosedLoop = DetermineIfClosedLoop(champList);

            foreach (Champion champion in AllChamps)
            {
                bool inListAlready = false;
                bool jinxOrViOverlap = false;
                bool sharesTrait = false;

                foreach (Champion champOnBoard in champList)
                {
                    if (champion.Name == champOnBoard.Name)
                    {
                        inListAlready = true;
                        break;
                    }
                    else if ((champion.Name == "Jinx" && champion.Name == "Vi") || (champion.Name == "Vi" && champion.Name == "Jinx"))
                    {
                        jinxOrViOverlap = true;
                        break;
                    }
                    else if (CheckIfSharesTrait(champion, champOnBoard))
                    {
                        sharesTrait = true;
                    }
                }
                if (!inListAlready && !jinxOrViOverlap && (sharesTrait || isClosedLoop))
                {
                    champsToAdd.Add(champion);
                }
            }
            //PUT STUFF HERE
            foreach (Champion champion in champsToAdd)
            {
                List<Champion> tempList = new List<Champion>();
                tempList.AddRange(champList);
                tempList.Add(champion);
                nextLists.Add(tempList);
            }
            return nextLists;
        }

        public bool CheckIfSharesTrait(Champion champOne, Champion champTwo)
        {
            if (champOne.TraitThree == Traits.Blank)
            {
                if (champTwo.TraitThree == Traits.Blank)
                {
                    if (champOne.TraitOne == champTwo.TraitOne || champOne.TraitOne == champTwo.TraitTwo ||
                        champOne.TraitTwo == champTwo.TraitOne || champOne.TraitTwo == champTwo.TraitTwo)
                    {
                        return true;
                    }
                    else return false;
                }
                else
                {
                    if (champOne.TraitOne == champTwo.TraitOne || champOne.TraitOne == champTwo.TraitTwo || champOne.TraitOne == champTwo.TraitThree ||
                        champOne.TraitTwo == champTwo.TraitOne || champOne.TraitTwo == champTwo.TraitTwo || champOne.TraitTwo == champTwo.TraitThree)
                    {
                        return true;
                    }
                    else return false;
                }
            }
            else
            {
                if (champTwo.TraitThree == Traits.Blank)
                {
                    if (champOne.TraitOne == champTwo.TraitOne || champOne.TraitOne == champTwo.TraitTwo ||
                        champOne.TraitTwo == champTwo.TraitOne || champOne.TraitTwo == champTwo.TraitTwo ||
                        champOne.TraitThree == champTwo.TraitOne || champOne.TraitThree == champTwo.TraitTwo)
                    {
                        return true;
                    }
                    else return false;
                }
                else
                {
                    if (champOne.TraitOne == champTwo.TraitOne || champOne.TraitOne == champTwo.TraitTwo || champOne.TraitOne == champTwo.TraitThree ||
                        champOne.TraitTwo == champTwo.TraitOne || champOne.TraitTwo == champTwo.TraitTwo || champOne.TraitTwo == champTwo.TraitThree ||
                        champOne.TraitThree == champTwo.TraitOne || champOne.TraitThree == champTwo.TraitTwo || champOne.TraitThree == champTwo.TraitThree)
                    {
                        return true;
                    }
                    else return false;
                }
            }
        }
        public bool DetermineIfClosedLoop(List<Champion> champList)
        {
            int throwAway = StateAnalysis(champList, out int throwAwayTwo, out int wastedTraits);
            if (wastedTraits != 0)
            {
                return false;
            }
            else return true;
        }


        public void CompareToCurrentTopCompositions(List<Champion> currentFieldedChampList)
        {
            int traitCount = StateAnalysis(currentFieldedChampList, out int numOfTraitTiers, out int wastedTraits);
            if (traitCount == traitCountAllowable)
            {
                if (!IsAlreadyInList(currentFieldedChampList))
                {
                    TopCompositions.Add(currentFieldedChampList);
                }
            }
            else if (traitCount > traitCountAllowable)
            {
                TopCompositions.Clear();
                traitCountAllowable = traitCount;
                TopCompositions.Add(currentFieldedChampList);
            }
        }
        public bool IsAlreadyInList(List<Champion> listToCheck)
        {
            bool duplicateFound = false;
            foreach (List<Champion> topComp in TopCompositions)
            {
                duplicateFound = IsEqual(listToCheck, topComp);
                if (duplicateFound)
                    return duplicateFound;
            }
            return duplicateFound;
        }
        public bool IsEqual(List<Champion> listOne, List<Champion> listTwo)
        {
            int equalChampCount = 0;
            foreach (Champion championOne in listOne)
            {
                foreach (Champion championTwo in listTwo)
                {
                    if (championOne.Name == championTwo.Name)
                    {
                        equalChampCount++;
                    }
                }
            }
            if (equalChampCount == 9)
                return true;
            else
                return false;
        }

        //Returns total number of active traits, number of trait tiers passed, and number of unused traits
        public int StateAnalysis(List<Champion> champList, out int numOfTraitTiers, out int wastedTraits)
        {
            int numOfTraits = 0;
            numOfTraitTiers = 0;
            wastedTraits = 0;

            int chemtechCount = 0;
            int clockworkCount = 0;
            int debonairCount = 0;
            int enforcerCount = 0;
            int gluttonCount = 0;
            int hextechCount = 0;
            int mastermindCount = 0;
            int mercenaryCount = 0;
            int mutantCount = 0;
            int rivalCount = 0;
            int scrapCount = 0;
            int socialiteCount = 0;
            int syndicateCount = 0;
            int yordleCount = 0;
            int arcanistCount = 0;
            int assassinCount = 0;
            int bodyguardCount = 0;
            int bruiserCount = 0;
            int challengerCount = 0;
            int enchanterCount = 0;
            int innovatorCount = 0;
            int scholarCount = 0;
            int sniperCount = 0;
            int strikerCount = 0;
            int transformerCount = 0;
            int twinshotCount = 0;

            foreach (Champion champ in champList)
            {
                List<Traits> traitList = new List<Traits>();
                traitList.Add(champ.TraitOne);
                traitList.Add(champ.TraitTwo);

                if (champ.TraitThree != Traits.Blank)
                {
                    traitList.Add(champ.TraitThree);
                }

                foreach (Traits trait in traitList)
                {
                    switch (trait)
                    {
                        case Traits.Chemtech:
                            {
                                chemtechCount++;
                                break;
                            }
                        case Traits.Clockwork:
                            {
                                clockworkCount++;
                                break;
                            }
                        case Traits.Debonair:
                            {
                                debonairCount++;
                                break;
                            }
                        case Traits.Enforcer:
                            {
                                enforcerCount++;
                                break;
                            }
                        case Traits.Glutton:
                            {
                                gluttonCount++;
                                break;
                            }
                        case Traits.Hextech:
                            {
                                hextechCount++;
                                break;
                            }
                        case Traits.Mastermind:
                            {
                                mastermindCount++;
                                break;
                            }
                        case Traits.Mercenary:
                            {
                                mercenaryCount++;
                                break;
                            }
                        case Traits.Mutant:
                            {
                                mutantCount++;
                                break;
                            }
                        case Traits.Rival:
                            {
                                rivalCount++;
                                break;
                            }
                        case Traits.Scrap:
                            {
                                scrapCount++;
                                break;
                            }
                        case Traits.Socialite:
                            {
                                socialiteCount++;
                                break;
                            }
                        case Traits.Syndicate:
                            {
                                syndicateCount++;
                                break;
                            }
                        case Traits.Yordle:
                            {
                                yordleCount++;
                                break;
                            }
                        case Traits.Arcanist:
                            {
                                arcanistCount++;
                                break;
                            }
                        case Traits.Assassin:
                            {
                                assassinCount++;
                                break;
                            }
                        case Traits.Bodyguard:
                            {
                                bodyguardCount++;
                                break;
                            }
                        case Traits.Bruiser:
                            {
                                bruiserCount++;
                                break;
                            }
                        case Traits.Challenger:
                            {
                                challengerCount++;
                                break;
                            }
                        case Traits.Enchanter:
                            {
                                enchanterCount++;
                                break;
                            }
                        case Traits.Innovator:
                            {
                                innovatorCount++;
                                break;
                            }
                        case Traits.Scholar:
                            {
                                scholarCount++;
                                break;
                            }
                        case Traits.Sniper:
                            {
                                sniperCount++;
                                break;
                            }
                        case Traits.Striker:
                            {
                                strikerCount++;
                                break;
                            }
                        case Traits.Transformer:
                            {
                                transformerCount++;
                                break;
                            }
                        case Traits.Twinshot:
                            {
                                twinshotCount++;
                                break;
                            }
                    }
                }
            }

            //Traits with even numbered tier bonuses
            int[] evenArray = new int[] { clockworkCount, enforcerCount, hextechCount, scrapCount, arcanistCount, assassinCount,
                bodyguardCount, bruiserCount, challengerCount, scholarCount, sniperCount, strikerCount, twinshotCount };
            for (int i = 0; i < evenArray.Length; i++)
            {
                numOfTraits += EvenTrait(evenArray[i], out int evenTier, out int evenWastedTraits);
                numOfTraitTiers += evenTier;
                wastedTraits += evenWastedTraits;
            }
            //Traits with odd numbered tier bonuses
            int[] oddArray = new int[] { chemtechCount, debonairCount, mercenaryCount, mutantCount, syndicateCount, innovatorCount };
            for (int i = 0; i < oddArray.Length; i++)
            {
                numOfTraits += OddTrait(oddArray[i], out int oddTier, out int oddWastedTraits);
                numOfTraitTiers += oddTier;
                wastedTraits += oddWastedTraits;
            }
            //Traits with single points only
            int[] singleArray = new int[] { gluttonCount, mastermindCount, rivalCount, transformerCount };
            for (int i = 0; i < singleArray.Length; i++)
            {
                if (singleArray[i] == 1)
                {
                    numOfTraits++;
                    numOfTraitTiers++;
                }
            }
            //Enchanter, Socialite, Yordle
            numOfTraits += Socialite(socialiteCount, out int socialiteTier, out int socWastedTraits);
            numOfTraitTiers += socialiteTier;
            wastedTraits += socWastedTraits;
            numOfTraits += Enchanter(enchanterCount, out int enchanterTier, out int enchWastedTraits);
            numOfTraitTiers += enchanterTier;
            wastedTraits += enchWastedTraits;
            numOfTraits += Yordle(yordleCount, out int yordleTier, out int yordWastedTraits);
            numOfTraitTiers += yordleTier;
            wastedTraits += yordWastedTraits;

            return numOfTraits;

        }

        //Methods for determining incrementation on number of traits and number of trait tiers
        public int OddTrait(int inputTraitCount, out int inputTraitTierCount, out int wastedTraits)
        {
            int outputTraitCount = 0;
            if (inputTraitCount == 9)
            {
                outputTraitCount = 1;
                inputTraitTierCount = 4;
            }
            else if (inputTraitCount >= 7)
            {
                outputTraitCount = 1;
                inputTraitTierCount = 3;
            }
            else if (inputTraitCount >= 5)
            {
                outputTraitCount = 1;
                inputTraitTierCount = 2;
            }
            else if (inputTraitCount >= 3)
            {
                outputTraitCount = 1;
                inputTraitTierCount = 1;
            }
            else
            {
                inputTraitTierCount = 0;
            }
            if (0 <= inputTraitCount && inputTraitCount <= 2)
            {
                wastedTraits = inputTraitCount;
            }
            else
            {
                wastedTraits = inputTraitCount % 2 == 1 ? 0 : 1;
            }
            return outputTraitCount;
        }
        public int EvenTrait(int inputTraitCount, out int inputTraitTierCount, out int wastedTraits)
        {
            int outputTraitCount = 0;
            if (inputTraitCount == 8)
            {
                outputTraitCount = 1;
                inputTraitTierCount = 4;
            }
            else if (inputTraitCount >= 6)
            {
                outputTraitCount = 1;
                inputTraitTierCount = 3;
            }
            else if (inputTraitCount >= 4)
            {
                outputTraitCount = 1;
                inputTraitTierCount = 2;
            }
            else if (inputTraitCount >= 2)
            {
                outputTraitCount = 1;
                inputTraitTierCount = 1;
            }
            else
            {
                inputTraitTierCount = 0;
            }
            wastedTraits = inputTraitCount % 2 == 0 ? 0 : 1;
            return outputTraitCount;
        }
        public int Socialite(int inputTraitCount, out int inputTraitTierCount, out int wastedTraits)
        {
            int outputTraitCount = 0;
            wastedTraits = 0;
            if (inputTraitCount == 5)
            {
                outputTraitCount = 1;
                inputTraitTierCount = 4;
            }
            else if (inputTraitCount >= 3)
            {
                if (inputTraitCount == 4)
                {
                    wastedTraits = 1;
                }
                outputTraitCount = 1;
                inputTraitTierCount = 3;
            }
            else if (inputTraitCount >= 2)
            {
                outputTraitCount = 1;
                inputTraitTierCount = 2;
            }
            else if (inputTraitCount >= 1)
            {
                outputTraitCount = 1;
                inputTraitTierCount = 1;
            }
            else
            {
                inputTraitTierCount = 0;
            }
            return outputTraitCount;
        }
        public int Enchanter(int inputTraitCount, out int inputTraitTierCount, out int wastedTraits)
        {
            int outputTraitCount = 0;
            wastedTraits = 0;
            if (inputTraitCount == 5)
            {
                outputTraitCount = 1;
                inputTraitTierCount = 4;
            }
            else if (inputTraitCount >= 4)
            {
                outputTraitCount = 1;
                inputTraitTierCount = 3;
            }
            else if (inputTraitCount >= 3)
            {
                outputTraitCount = 1;
                inputTraitTierCount = 2;
            }
            else if (inputTraitCount >= 2)
            {
                outputTraitCount = 1;
                inputTraitTierCount = 1;
            }
            else
            {
                if (inputTraitCount == 1)
                {
                    wastedTraits = 1;
                }
                inputTraitTierCount = 0;
            }
            return outputTraitCount;
        }
        public int Yordle(int inputTraitCount, out int inputTraitTierCount, out int wastedTraits)
        {
            int outputTraitCount = 0;
            if (inputTraitCount >= 6)
            {
                outputTraitCount = 1;
                inputTraitTierCount = 2;
            }
            else if (inputTraitCount >= 3)
            {
                outputTraitCount = 1;
                inputTraitTierCount = 1;
            }
            else
            {
                inputTraitTierCount = 0;
            }
            wastedTraits = inputTraitCount % 3 == 0 ? 0 : inputTraitCount % 3;
            return outputTraitCount;
        }
    }
}
