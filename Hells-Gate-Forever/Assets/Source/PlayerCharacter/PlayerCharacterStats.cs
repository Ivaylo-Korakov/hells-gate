using System.Collections.Generic;
using UnityEngine;
using HellsGate.Manager;
using System.Collections;

namespace HellsGate.PlayerCharacter
{
    public class PlayerCharacterStats : MonoBehaviour
    {
        // =========== Constants ===========
        #region Constants
        [SerializeField] private int BASE_HEALTH = 100;
        [SerializeField] private int BASE_MANA = 100;
        [SerializeField] private int BASE_STAMINA = 100;
        [SerializeField] private int BASE_VITALITY = 10;
        [SerializeField] private int BASE_DEXTERITY = 10;
        [SerializeField] private int BASE_MIND = 10;
        [SerializeField] private int BASE_INTELLIGENCE = 10;
        [SerializeField] private int BASE_DEFENSE = 10;
        [SerializeField, Range(1, 100)] private float LevelToGoldPriceRatio = 2;
        #endregion

        // =========== Stats ===========
        #region Stats
        public int Health { get; private set; }
        public int MaxHealth { get; private set; }
        public int Mana { get; private set; }
        public int MaxMana { get; private set; }
        public int Stamina { get; private set; }
        public int MaxStamina { get; private set; }

        public int Vitality { get; private set; } // MaxHealth
        public int Dexterity { get; private set; } // MaxStamina
        public int Mind { get; private set; } // MaxMana
        public int Intelligence { get; private set; } // Magic Damage
        public int Defense { get; private set; } // All Resistance

        public int Level { get; private set; }
        public int Gold { get; private set; }
        #endregion

        // =========== Unity Methods ===========
        #region Unity Methods
        private void Awake()
        {
            Health = BASE_HEALTH;
            MaxHealth = BASE_HEALTH;
            Mana = BASE_MANA;
            MaxMana = BASE_MANA;
            Stamina = BASE_STAMINA;
            MaxStamina = BASE_STAMINA;

            Vitality = BASE_VITALITY;
            Dexterity = BASE_DEXTERITY;
            Mind = BASE_MIND;
            Intelligence = BASE_INTELLIGENCE;
            Defense = BASE_DEFENSE;

            Level = 1;
            Gold = 0;
        }
        #endregion

        // =========== Stat Methods ===========
        #region Stat Methods
        #region Health
        public void AddHealth(int amount)
        {
            Health += amount;
            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }
        }

        public void RemoveHealth(int amount)
        {
            Health -= amount;
            if (Health < 0)
            {
                Health = 0;
            }
        }
        #endregion

        #region Mana
        public void AddMana(int amount)
        {
            Mana += amount;
            if (Mana > MaxMana)
            {
                Mana = MaxMana;
            }
        }

        public void RemoveMana(int amount)
        {
            Mana -= amount;
            if (Mana < 0)
            {
                Mana = 0;
            }
        }
        #endregion

        #region Stamina
        public void AddStamina(int amount)
        {
            Stamina += amount;
            if (Stamina > MaxStamina)
            {
                Stamina = MaxStamina;
            }
        }

        public void RemoveStamina(int amount)
        {
            Stamina -= amount;
            if (Stamina < 0)
            {
                Stamina = 0;
            }
        }
        #endregion

        #region Vitality
        public void AddVitality(int amount)
        {
            Vitality += amount;
            MaxHealth = BASE_HEALTH + (Vitality * 10);
        }

        public void RemoveVitality(int amount)
        {
            Vitality -= amount;
            MaxHealth = BASE_HEALTH + (Vitality * 10);
        }
        #endregion

        #region Dexterity
        public void AddDexterity(int amount)
        {
            Dexterity += amount;
            MaxStamina = BASE_STAMINA + (Dexterity * 10);
        }

        public void RemoveDexterity(int amount)
        {
            Dexterity -= amount;
            MaxStamina = BASE_STAMINA + (Dexterity * 10);
        }
        #endregion

        #region Mind
        public void AddMind(int amount)
        {
            Mind += amount;
            MaxMana = BASE_MANA + (Mind * 10);
        }

        public void RemoveMind(int amount)
        {
            Mind -= amount;
            MaxMana = BASE_MANA + (Mind * 10);
        }
        #endregion

        #region Intelligence
        public void AddIntelligence(int amount)
        {
            Intelligence += amount;
        }

        public void RemoveIntelligence(int amount)
        {
            Intelligence -= amount;
        }
        #endregion

        #region Defense
        public void AddDefense(int amount)
        {
            Defense += amount;
        }

        public void RemoveDefense(int amount)
        {
            Defense -= amount;
        }
        #endregion

        #region Level
        public bool BuyLevel(int amount)
        {
            if (RemoveGold((int)(LevelToGoldPriceRatio * amount)))
            {
                Level += amount;
                return true;
            }
            return false;
        }
        #endregion

        #region Gold
        public void AddGold(int amount)
        {
            Gold += amount;
        }

        public bool RemoveGold(int amount)
        {
            if (Gold >= amount)
            {
                Gold -= amount;
                return true;
            }
            return false;
        }
        #endregion

        #region Reset Stats
        public void ResetStats()
        {
            Health = BASE_HEALTH;
            MaxHealth = BASE_HEALTH;
            Mana = BASE_MANA;
            MaxMana = BASE_MANA;
            Stamina = BASE_STAMINA;
            MaxStamina = BASE_STAMINA;

            Vitality = BASE_VITALITY;
            Dexterity = BASE_DEXTERITY;
            Mind = BASE_MIND;
            Intelligence = BASE_INTELLIGENCE;
            Defense = BASE_DEFENSE;

            Level = 1;
            Gold = 0;
        }

        public void ResetHealth()
        {
            Health = BASE_HEALTH;
            MaxHealth = BASE_HEALTH;
        }

        public void ResetMana()
        {
            Mana = BASE_MANA;
            MaxMana = BASE_MANA;
        }

        public void ResetStamina()
        {
            Stamina = BASE_STAMINA;
            MaxStamina = BASE_STAMINA;
        }

        public void ResetVitality()
        {
            Vitality = BASE_VITALITY;
            ResetHealth();
        }

        public void ResetDexterity()
        {
            Dexterity = BASE_DEXTERITY;
            ResetStamina();
        }

        public void ResetMind()
        {
            Mind = BASE_MIND;
            ResetMana();
        }

        public void ResetIntelligence()
        {
            Intelligence = BASE_INTELLIGENCE;
        }

        public void ResetDefense()
        {
            Defense = BASE_DEFENSE;
        }

        public void ResetLevel()
        {
            Level = 1;
        }

        public void ResetGold()
        {
            Gold = 0;
        }

        public void ResetAll()
        {
            ResetStats();
        }

        #endregion

        #endregion

    }

}