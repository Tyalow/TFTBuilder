using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TFTBuilder
{
    internal static class Program
    {
        
        static void Main()
        {

            List<Champion> champList = new List<Champion>();
            AddChampions(champList);

            SearchTree searchTree = new SearchTree(champList);
            foreach (List<Champion> topChampList in searchTree.TopCompositions)
            {
                List<String> nameList = new List<String>();
                foreach (Champion champion in topChampList)
                {
                    nameList.Add(champion.Name);
                }
                Console.WriteLine(String.Join(", ", nameList));
            }

        }

        static public void AddChampions(List<Champion> champList)
        {
            champList.Add(new Champion("Ahri", Traits.Syndicate, Traits.Arcanist));
            champList.Add(new Champion("Ashe", Traits.Syndicate, Traits.Sniper));
            champList.Add(new Champion("Blitzcrank", Traits.Scrap, Traits.Bodyguard));
            champList.Add(new Champion("Brand", Traits.Debonair, Traits.Arcanist));
            champList.Add(new Champion("Braum", Traits.Syndicate, Traits.Bodyguard));
            champList.Add(new Champion("Caitlyn", Traits.Enforcer, Traits.Sniper));
            champList.Add(new Champion("Camille", Traits.Clockwork, Traits.Challenger));
            champList.Add(new Champion("Corki", Traits.Yordle, Traits.Twinshot));
            champList.Add(new Champion("Caitlyn", Traits.Enforcer, Traits.Sniper));
            champList.Add(new Champion("Darius", Traits.Syndicate, Traits.Bodyguard));
            champList.Add(new Champion("Draven", Traits.Debonair, Traits.Challenger));
            champList.Add(new Champion("Ekko", Traits.Scrap, Traits.Assassin, Traits.Innovator));
            champList.Add(new Champion("Ezreal", Traits.Scrap, Traits.Innovator));
            champList.Add(new Champion("Gangplank", Traits.Mercenary, Traits.Twinshot));
            champList.Add(new Champion("Gnar", Traits.Socialite, Traits.Striker, Traits.Yordle));
            champList.Add(new Champion("Illaoi", Traits.Mercenary, Traits.Bruiser));
            champList.Add(new Champion("Irelia", Traits.Scrap, Traits.Striker));
            champList.Add(new Champion("Jarvan", Traits.Hextech, Traits.Striker));
            champList.Add(new Champion("Jayce", Traits.Transformer, Traits.Innovator, Traits.Enforcer));
            champList.Add(new Champion("Jhin", Traits.Clockwork, Traits.Sniper));
            champList.Add(new Champion("Jinx", Traits.Scrap, Traits.Twinshot, Traits.Rival));
            champList.Add(new Champion("Kaisa", Traits.Mutant, Traits.Challenger));
            champList.Add(new Champion("Kassadin", Traits.Mutant, Traits.Scholar));
            champList.Add(new Champion("Khazix", Traits.Mutant, Traits.Assassin));
            champList.Add(new Champion("Leona", Traits.Debonair, Traits.Bodyguard));
            champList.Add(new Champion("Lucian", Traits.Hextech, Traits.Twinshot));
            champList.Add(new Champion("Lulu", Traits.Yordle, Traits.Enchanter));
            champList.Add(new Champion("Malzahar", Traits.Mutant, Traits.Arcanist));
            champList.Add(new Champion("Miss Fortune", Traits.Mercenary, Traits.Sniper));
            champList.Add(new Champion("Morgana", Traits.Syndicate, Traits.Enchanter));
            champList.Add(new Champion("Nocturne", Traits.Hextech, Traits.Assassin));
            champList.Add(new Champion("Orianna", Traits.Clockwork, Traits.Enchanter));
            champList.Add(new Champion("Poppy", Traits.Yordle, Traits.Bodyguard));
            champList.Add(new Champion("Quinn", Traits.Mercenary, Traits.Challenger));
            champList.Add(new Champion("Reksai", Traits.Mutant, Traits.Striker, Traits.Bruiser));
            champList.Add(new Champion("Renata", Traits.Chemtech, Traits.Scholar));
            champList.Add(new Champion("Sejuani", Traits.Hextech, Traits.Enforcer, Traits.Bruiser));
            champList.Add(new Champion("Senna", Traits.Socialite, Traits.Enchanter));
            champList.Add(new Champion("Seraphine", Traits.Innovator, Traits.Socialite));
            champList.Add(new Champion("Silco", Traits.Mastermind, Traits.Scholar));
            champList.Add(new Champion("Singed", Traits.Chemtech, Traits.Innovator));
            champList.Add(new Champion("Sivir", Traits.Hextech, Traits.Striker));
            champList.Add(new Champion("Swain", Traits.Hextech, Traits.Arcanist));
            champList.Add(new Champion("Syndra", Traits.Debonair, Traits.Scholar));
            champList.Add(new Champion("Tahm Kench", Traits.Glutton, Traits.Mercenary, Traits.Bruiser));
            champList.Add(new Champion("Talon", Traits.Debonair, Traits.Assassin));
            champList.Add(new Champion("Tryndamere", Traits.Chemtech, Traits.Challenger));
            champList.Add(new Champion("Twitch", Traits.Chemtech, Traits.Assassin));
            champList.Add(new Champion("Vex", Traits.Yordle, Traits.Arcanist));
            champList.Add(new Champion("Vi", Traits.Enforcer, Traits.Rival, Traits.Bruiser));
            champList.Add(new Champion("Viktor", Traits.Chemtech, Traits.Arcanist));
            champList.Add(new Champion("Warwick", Traits.Scrap, Traits.Challenger));
            champList.Add(new Champion("Zac", Traits.Chemtech, Traits.Bruiser));
            champList.Add(new Champion("Zeri", Traits.Debonair, Traits.Sniper));
            champList.Add(new Champion("Ziggs", Traits.Scrap, Traits.Yordle, Traits.Arcanist));
            champList.Add(new Champion("Zilean", Traits.Clockwork, Traits.Innovator));
            champList.Add(new Champion("Zyra", Traits.Syndicate, Traits.Scholar));
        }
    }
}
