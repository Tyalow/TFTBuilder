using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFTBuilder
{
    enum Traits
    {
        Chemtech,
        Clockwork,
        Debonair,
        Enforcer,
        Glutton,
        Hextech,
        Mastermind,
        Mercenary,
        Mutant,
        Rival,
        Scrap,
        Socialite,
        Syndicate,
        Yordle,
        Arcanist,
        Assassin,
        Bodyguard,
        Bruiser,
        Challenger,
        Enchanter,
        Innovator,
        Scholar,
        Sniper,
        Striker,
        Transformer,
        Twinshot,
        Blank
    }
    internal class Champion
    {
        public string Name { get; private set; }
        public Traits TraitOne { get; private set; }
        public Traits TraitTwo { get; private set; }
        public Traits TraitThree { get; private set; }

        public Champion(string name, Traits traitOne, Traits traitTwo, Traits traitThree)
        {
            Name = name;
            TraitOne = traitOne;
            TraitTwo = traitTwo;
            TraitThree = traitThree;
        }

        public Champion(string name, Traits traitOne, Traits traitTwo)
        {
            Name = name;
            TraitOne = traitOne;
            TraitTwo = traitTwo;
            TraitThree = Traits.Blank;
        }
    }
}
