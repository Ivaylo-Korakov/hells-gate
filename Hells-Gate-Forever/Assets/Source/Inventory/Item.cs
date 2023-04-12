using System.Collections.Generic;
using UnityEngine;

namespace HellsGate.Inventory
{
    public enum ItemQuality
    {
        Common,
        Rare,
        Legendary
    }

    public enum ItemType
    {
        Potion,
        Weapon,
        Armor,
        Spell,
        Quest,
        Misc
    }

    abstract public class ItemEffect
    {
        public string EffectName { get; set; }
        public string EffectDescription { get; set; }
        public int EffectValue { get; set; }

        public ItemEffect(string effectName, string effectDescription, int effectValue)
        {
            this.EffectName = effectName;
            this.EffectDescription = effectDescription;
            this.EffectValue = effectValue;
        }

        public ItemEffect(ItemEffect effect)
        {
            this.EffectName = effect.EffectName;
            this.EffectDescription = effect.EffectDescription;
            this.EffectValue = effect.EffectValue;
        }

        public abstract void ApplyEffect();
    }

    public class Item
    {
        // =========== Item Properties ===========
        #region Item Properties
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ItemQuality Quality { get; set; }
        public ItemType Type { get; set; }
        public Sprite Icon { get; set; }
        public Dictionary<string, int> Stats { get; set; } = new Dictionary<string, int>();
        public List<ItemEffect> Effects { get; set; } = new List<ItemEffect>();
        public int StackSize { get; set; }
        public int MaxStackSize { get; set; }
        public bool IsStackable { get; set; }
        public bool IsSellable { get; set; }
        public int SellPrice { get; set; }
        public int BuyPrice { get; set; }
        #endregion

        // =========== Constructors ===========
        #region Constructors
        public Item(
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
            Dictionary<string, int> stats = null,
            List<ItemEffect> effects = null
        )
        {
            this.Id = id;
            this.Title = title;
            this.Description = description;
            this.Quality = quality;
            this.Type = type;
            this.StackSize = stackSize;
            this.MaxStackSize = maxStackSize;
            this.IsStackable = isStackable;
            this.IsSellable = isSellable;
            this.SellPrice = sellPrice;
            this.BuyPrice = buyPrice;
            this.Icon = Resources.Load<Sprite>("Items/Sprites/" + title);
            this.Stats = stats ?? new Dictionary<string, int>();
            this.Effects = effects ?? new List<ItemEffect>();
        }

        public Item(Item item)
        {
            this.Id = item.Id;
            this.Title = item.Title;
            this.Description = item.Description;
            this.Quality = item.Quality;
            this.Type = item.Type;
            this.StackSize = item.StackSize;
            this.MaxStackSize = item.MaxStackSize;
            this.IsStackable = item.IsStackable;
            this.IsSellable = item.IsSellable;
            this.SellPrice = item.SellPrice;
            this.BuyPrice = item.BuyPrice;
            this.Icon = Resources.Load<Sprite>("Items/Sprites/" + item.Title);
            this.Stats = item.Stats;
            this.Effects = item.Effects;
        }
        #endregion

        // =========== Methods ===========
        #region Methods
        public void AddToStack(int amount)
        {
            this.StackSize += amount;
        }

        public void RemoveFromStack(int amount)
        {
            this.StackSize -= amount;
        }

        public void SetStackSize(int amount)
        {
            this.StackSize = amount;
        }

        public Item Clone()
        {
            return new Item(this);
        }
        #endregion

        // =========== Operators ===========
        #region Operators
        public static bool operator ==(Item item1, Item item2)
        {
            if (item1 is null && item2 is null)
            {
                return true;
            }

            if (item1 is null || item2 is null)
            {
                return false;
            }

            return item1.Id == item2.Id;
        }

        public static bool operator !=(Item item1, Item item2)
        {
            return !(item1 == item2);
        }

        public override bool Equals(object obj)
        {
            if (obj is Item)
            {
                return this == (Item)obj;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }
}