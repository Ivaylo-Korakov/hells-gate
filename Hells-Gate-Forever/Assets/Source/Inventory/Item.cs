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

    [System.Serializable]
    [CreateAssetMenu(fileName = "New Generic Stat", menuName = "Inventory/Generic Stat")]
    public class GenericStat : ScriptableObject
    {
        public string StatName;
        public int StatValue;
    }

    [System.Serializable]
    [CreateAssetMenu(fileName = "New Generic Effect", menuName = "Inventory/Generic Effect")]
    public class GenericEffect : ScriptableObject
    {
        public string EffectName;
        public string EffectDescription;
        public int EffectValue;
    }

    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
    [System.Serializable]
    public class Item : ScriptableObject
    {
        // =========== Item Properties ===========
        #region Item Properties
        [Header("General")]
        public int Id;
        public string Title;
        public string Description;
        public ItemQuality Quality;
        public ItemType Type;
        [Header("Graphics")]
        public Sprite Icon;
        [Header("Stats & Effects")]
        [SerializeField][SerializeReference] public List<GenericStat> Stats = new List<GenericStat>();
        [SerializeField][SerializeReference] public List<GenericEffect> Effects = new List<GenericEffect>();
        [Header("Stacking")]
        public bool IsStackable;
        public int MaxStackSize;
        [Header("Trading")]
        public bool IsSellable;
        public int SellPrice;
        public int BuyPrice;
        #endregion

        // =========== Constructors ===========
        #region Constructors
        public Item(
            int id,
            string title,
            string description,
            ItemQuality quality,
            ItemType type,
            int maxStackSize,
            bool isStackable,
            bool isSellable,
            int sellPrice,
            int buyPrice,
            List<GenericStat> stats = null,
            List<GenericEffect> effects = null
        )
        {
            this.Id = id;
            this.Title = title;
            this.Description = description;
            this.Quality = quality;
            this.Type = type;
            this.MaxStackSize = maxStackSize;
            this.IsStackable = isStackable;
            this.IsSellable = isSellable;
            this.SellPrice = sellPrice;
            this.BuyPrice = buyPrice;
            // this.Icon = Resources.Load<Sprite>("Items/Sprites/" + title);
            this.Stats = stats ?? new List<GenericStat>();
            this.Effects = effects ?? new List<GenericEffect>();
        }

        public Item(Item item)
        {
            this.Id = item.Id;
            this.Title = item.Title;
            this.Description = item.Description;
            this.Quality = item.Quality;
            this.Type = item.Type;
            this.MaxStackSize = item.MaxStackSize;
            this.IsStackable = item.IsStackable;
            this.IsSellable = item.IsSellable;
            this.SellPrice = item.SellPrice;
            this.BuyPrice = item.BuyPrice;
            // this.Icon = Resources.Load<Sprite>("Items/Sprites/" + item.Title);
            this.Stats = item.Stats;
            this.Effects = item.Effects;
        }
        #endregion

        // =========== Methods ===========
        #region Methods
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