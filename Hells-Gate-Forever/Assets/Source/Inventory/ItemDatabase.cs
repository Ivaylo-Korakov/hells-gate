using System.Collections.Generic;
using HellsGate.Spells;
using UnityEngine;

namespace HellsGate.Inventory
{
    public class ItemDatabase : MonoBehaviour
    {
        public static ItemDatabase Instance { get; private set; }
        public List<Item> Items { get; private set; } = new List<Item>();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this;
            BuildDatabase();
        }

        public Item GetItem(int id)
        {
            return Items.Find(item => item.Id == id);
        }

        public Item GetItem(string title)
        {
            return Items.Find(item => item.Title == title);
        }

        void BuildDatabase()
        {
            this.Items = new List<Item>();
            this.Items.AddRange(BuildPotions());
            this.Items.AddRange(BuildMagicWeapons());
            this.Items.AddRange(BuildMagicArmor());
            this.Items.AddRange(BuildMagicSpells());
        }

        private List<Item> BuildPotions()
        {
            List<Item> potions = new List<Item>()
            {
                // == Common Potions ==
                // new Item(id: 0, title: "Small Red Pot", description: "A potion that heals 10 HP",
                // quality: ItemQuality.Common, type: ItemType.Potion, maxStackSize: 3, isStackable: false,
                // isSellable: false, sellPrice: 2, buyPrice: 4,
                // stats: new List<(string, int)>() { ( "Health", 10 ) }, effects: new List<ItemEffect>()
                // ),
                // new Item(id: 1, title: "Small Blue Pot", description: "A potion that restores 10 Mana",
                // quality: ItemQuality.Common, type: ItemType.Potion, maxStackSize: 3, isStackable: false,
                // isSellable: false, sellPrice: 2, buyPrice: 4,
                // stats: new List<(string, int)>() { ( "Mana", 10 ) }, effects: new List<ItemEffect>()
                // ),
                // new Item(id: 2, title: "Small Green Pot", description: "A potion that restores 10 Stamina",
                // quality: ItemQuality.Common, type: ItemType.Potion, maxStackSize: 3, isStackable: false,
                // isSellable: false, sellPrice: 2, buyPrice: 4,
                // stats: new List<(string, int)>() { ( "Stamina", 10 ) }, effects: new List<ItemEffect>()
                // ),

                // == Rare Potions ==
                // new Item(id: 3, title: "Medium Red Pot", description: "A potion that heals 25 HP",
                // quality: ItemQuality.Rare, type: ItemType.Potion, maxStackSize: 5, isStackable: false,
                // isSellable: false, sellPrice: 5, buyPrice: 10,
                // stats: new List<(string, int)>() { ( "Health", 25 ) }, effects: new List<ItemEffect>()
                // ),
                // new Item(id: 4, title: "Medium Blue Pot", description: "A potion that restores 25 Mana",
                // quality: ItemQuality.Rare, type: ItemType.Potion, maxStackSize: 5, isStackable: false,
                // isSellable: false, sellPrice: 5, buyPrice: 10,
                // stats: new List<(string, int)>() { ( "Mana", 25 ) }, effects: new List<ItemEffect>()
                // ),
                // new Item(id: 5, title: "Medium Green Pot", description: "A potion that restores 25 Stamina",
                // quality: ItemQuality.Rare, type: ItemType.Potion, maxStackSize: 5, isStackable: false,
                // isSellable: false, sellPrice: 5, buyPrice: 10,
                // stats: new List<(string, int)>() { ( "Stamina", 25 ) }, effects: new List<ItemEffect>()
                // ),

                // == Legendary Potions ==
                // new Item(id: 6, title: "Large Red Pot", description: "A potion that heals 50 HP",
                // quality: ItemQuality.Legendary, type: ItemType.Potion, maxStackSize: 10, isStackable: false,
                // isSellable: false, sellPrice: 10, buyPrice: 20,
                // stats: new List<(string, int)>() { ( "Health", 50 ) }, effects: new List<ItemEffect>()
                // ),
                // new Item(id: 7, title: "Large Blue Pot", description: "A potion that restores 50 Mana",
                // quality: ItemQuality.Legendary, type: ItemType.Potion, maxStackSize: 10, isStackable: false,
                // isSellable: false, sellPrice: 10, buyPrice: 20,
                // stats: new List<(string, int)>() { ( "Mana", 50 ) }, effects: new List<ItemEffect>()
                // ),
                // new Item(id: 8, title: "Large Green Pot", description: "A potion that restores 50 Stamina",
                // quality: ItemQuality.Legendary, type: ItemType.Potion, maxStackSize: 10, isStackable: false,
                // isSellable: false, sellPrice: 10, buyPrice: 20,
                // stats: new List<(string, int)>() { ( "Stamina", 50 ) }, effects: new List<ItemEffect>()
                // ),
            };

            return potions;
        }

        public List<Item> BuildMagicWeapons()
        {
            List<Item> magicWeapons = new List<Item>()
            {
                // == Common Magic Weapons ==
                // new Item(id: 9, title: "Common Magic Sword", description: "A common magic sword",
                // quality: ItemQuality.Common, type: ItemType.Weapon, maxStackSize: 1, isStackable: false,
                // isSellable: false, sellPrice: 10, buyPrice: 20,
                // stats: new List<(string, int)>() { ( "Damage", 10 ) }, effects: new List<ItemEffect>()
                // ),
                // new Item(id: 10, title: "Common Magic Staff", description: "A common magic staff",
                // quality: ItemQuality.Common, type: ItemType.Weapon, maxStackSize: 1, isStackable: false,
                // isSellable: false, sellPrice: 10, buyPrice: 20,
                // stats: new List<(string, int)>() { ( "Damage", 10 ) }, effects: new List<ItemEffect>()
                // ),
                // new Item(id: 11, title: "Common Magic Book", description: "A common magic bow",
                // quality: ItemQuality.Common, type: ItemType.Weapon, maxStackSize: 1, isStackable: false,
                // isSellable: false, sellPrice: 10, buyPrice: 20,
                // stats: new List<(string, int)>() { ( "Damage", 10 ) }, effects: new List<ItemEffect>()
                // ),

                // == Rare Magic Weapons ==
                // new Item(id: 12, title: "Rare Magic Sword", description: "A rare magic sword",
                // quality: ItemQuality.Rare, type: ItemType.Weapon, maxStackSize: 1, isStackable: false,
                // isSellable: false, sellPrice: 20, buyPrice: 40,
                // stats: new List<(string, int)>() { ( "Damage", 20 ) }, effects: new List<ItemEffect>()
                // ),
                // new Item(id: 13, title: "Rare Magic Staff", description: "A rare magic staff",
                // quality: ItemQuality.Rare, type: ItemType.Weapon, maxStackSize: 1, isStackable: false,
                // isSellable: false, sellPrice: 20, buyPrice: 40,
                // stats: new List<(string, int)>() { ( "Damage", 20 ) }, effects: new List<ItemEffect>()
                // ),
                // new Item(id: 14, title: "Rare Magic Book", description: "A rare magic bow",
                // quality: ItemQuality.Rare, type: ItemType.Weapon, maxStackSize: 1, isStackable: false,
                // isSellable: false, sellPrice: 20, buyPrice: 40,
                // stats: new List<(string, int)>() { ( "Damage", 20 ) }, effects: new List<ItemEffect>()
                // ),

                // == Legendary Magic Weapons ==
                // new Item(id: 15, title: "Legendary Magic Sword", description: "A legendary magic sword",
                // quality: ItemQuality.Legendary, type: ItemType.Weapon, maxStackSize: 1, isStackable: false,
                // isSellable: false, sellPrice: 40, buyPrice: 80,
                // stats: new List<(string, int)>() { ( "Damage", 40 ) }, effects: new List<ItemEffect>()
                // ),
                // new Item(id: 16, title: "Legendary Magic Staff", description: "A legendary magic staff",
                // quality: ItemQuality.Legendary, type: ItemType.Weapon, maxStackSize: 1, isStackable: false,
                // isSellable: false, sellPrice: 40, buyPrice: 80,
                // stats: new List<(string, int)>() { ( "Damage", 40 ) }, effects: new List<ItemEffect>()
                // ),
                // new Item(id: 17, title: "Legendary Magic Book", description: "A legendary magic bow",
                // quality: ItemQuality.Legendary, type: ItemType.Weapon, maxStackSize: 1, isStackable: false,
                // isSellable: false, sellPrice: 40, buyPrice: 80,
                // stats: new List<(string, int)>() { ( "Damage", 40 ) }, effects: new List<ItemEffect>()
                // ),

            };

            return magicWeapons;
        }

        public List<Item> BuildMagicArmor()
        {
            List<Item> magicArmor = new List<Item>()
            {
                // == Common Magic Armor ==
                // new Item(id: 18, title: "Common Magic Helmet", description: "A common magic helmet",
                // quality: ItemQuality.Common, type: ItemType.Armor, maxStackSize: 1, isStackable: false,
                // isSellable: false, sellPrice: 10, buyPrice: 20,
                // stats: new List<(string, int)>() { ( "Armor", 10 ) }, effects: new List<ItemEffect>()
                // ),
                // new Item(id: 19, title: "Common Magic Chestplate", description: "A common magic chestplate",
                // quality: ItemQuality.Common, type: ItemType.Armor, maxStackSize: 1, isStackable: false,
                // isSellable: false, sellPrice: 10, buyPrice: 20,
                // stats: new List<(string, int)>() { ( "Armor", 10 ) }, effects: new List<ItemEffect>()
                // ),
                // new Item(id: 20, title: "Common Magic Leggings", description: "A common magic leggings",
                // quality: ItemQuality.Common, type: ItemType.Armor, maxStackSize: 1, isStackable: false,
                // isSellable: false, sellPrice: 10, buyPrice: 20,
                // stats: new List<(string, int)>() { ( "Armor", 10 ) }, effects: new List<ItemEffect>()
                // ),
                // new Item(id: 21, title: "Common Magic Boots", description: "A common magic boots",
                // quality: ItemQuality.Common, type: ItemType.Armor, maxStackSize: 1, isStackable: false,
                // isSellable: false, sellPrice: 10, buyPrice: 20,
                // stats: new List<(string, int)>() { ( "Armor", 10 ) }, effects: new List<ItemEffect>()
                // ),

                // == Rare Magic Armor ==
                // new Item(id: 22, title: "Rare Magic Helmet", description: "A rare magic helmet",
                // quality: ItemQuality.Rare, type: ItemType.Armor, maxStackSize: 1, isStackable: false,
                // isSellable: false, sellPrice: 20, buyPrice: 40,
                // stats: new List<(string, int)>() { ( "Armor", 20 ) }, effects: new List<ItemEffect>()
                // ),
                // new Item(id: 23, title: "Rare Magic Chestplate", description: "A rare magic chestplate",
                // quality: ItemQuality.Rare, type: ItemType.Armor, maxStackSize: 1, isStackable: false,
                // isSellable: false, sellPrice: 20, buyPrice: 40,
                // stats: new List<(string, int)>() { ( "Armor", 20 ) }, effects: new List<ItemEffect>()
                // ),
                // new Item(id: 24, title: "Rare Magic Leggings", description: "A rare magic leggings",
                // quality: ItemQuality.Rare, type: ItemType.Armor, maxStackSize: 1, isStackable: false,
                // isSellable: false, sellPrice: 20, buyPrice: 40,
                // stats: new List<(string, int)>() { ( "Armor", 20 ) }, effects: new List<ItemEffect>()
                // ),
                // new Item(id: 25, title: "Rare Magic Boots", description: "A rare magic boots",
                // quality: ItemQuality.Rare, type: ItemType.Armor, maxStackSize: 1, isStackable: false,
                // isSellable: false, sellPrice: 20, buyPrice: 40,
                // stats: new List<(string, int)>() { ( "Armor", 20 ) }, effects: new List<ItemEffect>()
                // ),

                // == Legendary Magic Armor ==
                // new Item(id: 26, title: "Legendary Magic Helmet", description: "A legendary magic helmet",
                // quality: ItemQuality.Legendary, type: ItemType.Armor,  maxStackSize: 1, isStackable: false,
                // isSellable: false, sellPrice: 40, buyPrice: 80,
                // stats: new List<(string, int)>() { ( "Armor", 40 ) }, effects: new List<ItemEffect>()
                // ),
                // new Item(id: 27, title: "Legendary Magic Chestplate", description: "A legendary magic chestplate",
                // quality: ItemQuality.Legendary, type: ItemType.Armor, maxStackSize: 1, isStackable: false,
                // isSellable: false, sellPrice: 40, buyPrice: 80,
                // stats: new List<(string, int)>() { ( "Armor", 40 ) }, effects: new List<ItemEffect>()
                // ),
                // new Item(id: 28, title: "Legendary Magic Leggings", description: "A legendary magic leggings",
                // quality: ItemQuality.Legendary, type: ItemType.Armor, maxStackSize: 1, isStackable: false,
                // isSellable: false, sellPrice: 40, buyPrice: 80,
                // stats: new List<(string, int)>() { ( "Armor", 40 ) }, effects: new List<ItemEffect>()
                // ),
                // new Item(id: 29, title: "Legendary Magic Boots", description: "A legendary magic boots",
                // quality: ItemQuality.Legendary, type: ItemType.Armor, maxStackSize: 1, isStackable: false,
                // isSellable: false, sellPrice: 40, buyPrice: 80,
                // stats: new List<(string, int)>() { ( "Armor", 40 ) }, effects: new List<ItemEffect>()
                // )
            };

            return magicArmor;
        }

        public List<Item> BuildMagicSpells()
        {
            List<Item> magicSpells = new List<Item>()
            {
                // == Common Magic Spells ==
                // new Spell(id: 30, title: "Common Magic Spell", description: "A common magic spell",
                // quality: ItemQuality.Common, type: ItemType.Spell, maxStackSize: 1, isStackable: false,
                // isSellable: false, sellPrice: 10, buyPrice: 20,
                // stats: new List<(string, int)>() { ( "Damage", 10 ) }, effects: new List<ItemEffect>(), spellType: SpellType.Fire
                // ),
                // new Spell(id: 31, title: "Common Magic Spell", description: "A common magic spell",
                // quality: ItemQuality.Common, type: ItemType.Spell, maxStackSize: 1, isStackable: false,
                // isSellable: false, sellPrice: 10, buyPrice: 20,
                // stats: new List<(string, int)>() { ( "Damage", 10 ) }, effects: new List<ItemEffect>(), spellType: SpellType.Ice
                // ),
                // new Spell(id: 32, title: "Common Magic Spell", description: "A common magic spell",
                // quality: ItemQuality.Common, type: ItemType.Spell, maxStackSize: 1, isStackable: false,
                // isSellable: false, sellPrice: 10, buyPrice: 20,
                // stats: new List<(string, int)>() { ( "Damage", 10 ) }, effects: new List<ItemEffect>(), spellType: SpellType.Lightning
                // ),
                // new Spell(id: 33, title: "Common Magic Spell", description: "A common magic spell",
                // quality: ItemQuality.Common, type: ItemType.Spell, maxStackSize: 1, isStackable: false,
                // isSellable: false, sellPrice: 10, buyPrice: 20,
                // stats: new List<(string, int)>() { ( "Damage", 10 ) }, effects: new List<ItemEffect>(), spellType: SpellType.Poison
                // ),

                // == Rare Magic Spells ==

                // == Legendary Magic Spells ==

            };

            return magicSpells;
        }
    }
}