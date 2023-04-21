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

    [CreateAssetMenu(fileName = "New Spell", menuName = "Inventory/Spell")]
    public class Spell : Item
    {
        // =========== Spell Properties ===========
        #region Spell Properties
        [Header("Spell Properties")]
        public SpellType SpellType;
        #endregion

        // =========== Constructors ===========
        #region Constructors
        public Spell(
            string title,
            string description,
            ItemQuality quality,
            ItemType type,
            int maxStackSize,
            bool isStackable,
            bool isSellable,
            int sellPrice,
            int buyPrice,
            GenericStat[] stats,
            GenericEffect[] effects,
            SpellType spellType
        ) : base(
            title,
            description,
            quality,
            type,
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
        #endregion

        // =========== Unity Methods ===========
        #region Unity Methods
        private void Awake()
        {
            this.SetDefaultSpellProperties();
        }
        #endregion

    }

}