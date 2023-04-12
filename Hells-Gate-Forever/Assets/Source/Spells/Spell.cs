using UnityEngine;
using HellsGate.Inventory;
using System.Collections.Generic;

namespace HellsGate.Spells
{
    public enum SpellType
    {
        Fire,
        Ice,
        Lightning,
        Poison
    }

    public class Spell : Item
    {
        public SpellType SpellType { get; set; }

        public Spell(
            int id,
            string title,
            string description,
            ItemQuality quality,
            ItemType type,
            int stackSize,
            int maxStackSize,
            bool isStackable,
            bool isSellable,
            int sellPrice,
            int buyPrice,
            Dictionary<string, int> stats,
            List<ItemEffect> effects,
            SpellType spellType
        ) : base(
            id,
            title,
            description,
            quality,
            type,
            stackSize,
            maxStackSize,
            isStackable,
            isSellable,
            sellPrice,
            buyPrice,
            stats,
            effects
        )
        {
            this.SetDefaultSpellProperties();
            this.SpellType = spellType;
        }

        public Spell(Spell spell) : base(spell)
        {
            this.SetDefaultSpellProperties();
            this.SpellType = spell.SpellType;
        }

        private void SetDefaultSpellProperties()
        {
            this.Type = ItemType.Spell;
            this.MaxStackSize = 1;
            this.IsStackable = false;
            this.IsSellable = false;
            this.SellPrice = 0;
            this.BuyPrice = 0;
        }

    }

}