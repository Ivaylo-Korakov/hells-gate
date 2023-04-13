using System.Collections.Generic;
using UnityEngine;
using HellsGate.Manager;
using System.Collections;
using TMPro;
using HellsGate.Inventory;

namespace HellsGate.PlayerCharacter
{
    public class PlayerCharacterStats : MonoBehaviour
    {
        // =========== Constants ===========
        #region Constants
        [SerializeField] private int BASE_HEALTH = 100;
        [SerializeField] private int BASE_MANA = 100;
        [SerializeField] private int BASE_STAMINA = 100;
        [SerializeField] private int BASE_VITALITY = 1;
        [SerializeField] private int BASE_DEXTERITY = 1;
        [SerializeField] private int BASE_MIND = 1;
        [SerializeField] private int BASE_INTELLIGENCE = 1;
        [SerializeField] private int BASE_DEFENSE = 1;
        [SerializeField, Range(1, 100)] private float LevelToGoldPriceRatio = 2;
        #endregion

        // =========== UI ===========
        #region UI
        [SerializeField] private TMP_Text healthText;
        [SerializeField] private TMP_Text manaText;
        [SerializeField] private TMP_Text staminaText;
        [SerializeField] private TMP_Text vitalityText;
        [SerializeField] private TMP_Text dexterityText;
        [SerializeField] private TMP_Text mindText;
        [SerializeField] private TMP_Text intelligenceText;
        [SerializeField] private TMP_Text defenseText;
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private TMP_Text goldText;
        [SerializeField] private TMP_Text nextLevelText;
        [SerializeField] private GameObject editStatsPanel;
        [SerializeField] private GameObject nextLevelButton;
        [SerializeField] private GameObject healthBar;
        [SerializeField] private GameObject manaBar;
        [SerializeField] private GameObject staminaBar;
        #endregion

        // =========== Stats ===========
        #region Stats
        private int _health;
        public int Health { get { return _health; } private set { _health = value; this.RefreshUI(); } }
        private int _maxHealth;
        public int MaxHealth { get { return _maxHealth; } private set { _maxHealth = value; this.RefreshUI(); } }
        private int _mana;
        public int Mana { get { return _mana; } private set { _mana = value; this.RefreshUI(); } }
        private int _maxMana;
        public int MaxMana { get { return _maxMana; } private set { _maxMana = value; this.RefreshUI(); } }
        private int _stamina;
        public int Stamina { get { return _stamina; } private set { _stamina = value; this.RefreshUI(); } }
        private int _maxStamina;
        public int MaxStamina { get { return _maxStamina; } private set { _maxStamina = value; this.RefreshUI(); } }

        private int _vitality;
        private int _baseVitality;
        public int Vitality { get { return _vitality + _baseVitality; } private set { _vitality = value; this.RefreshUI(); } } // MaxHealth
        private int _dexterity;
        private int _baseDexterity;
        public int Dexterity { get { return _dexterity + _baseDexterity; } private set { _dexterity = value; this.RefreshUI(); } } // MaxStamina
        private int _mind;
        private int _baseMind;
        public int Mind { get { return _mind + _baseMind; } private set { _mind = value; this.RefreshUI(); } } // MaxMana
        private int _intelligence;
        private int _baseIntelligence;
        public int Intelligence { get { return _intelligence + _baseIntelligence; } private set { _intelligence = value; this.RefreshUI(); } } // All Damage
        private int _defense;
        private int _baseDefense;
        public int Defense { get { return _defense + _baseDefense; } private set { _defense = value; this.RefreshUI(); } } // All Damage Reduction

        private int _level;
        public int Level { get { return _level; } private set { _level = value; this.RefreshUI(); } }
        private int _gold;
        public int Gold { get { return _gold; } private set { _gold = value; this.RefreshUI(); } }
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

            this.RefreshUI();
        }

        private void Start()
        {
            StartCoroutine(this.RegenHealth());
            StartCoroutine(this.RegenMana());
            StartCoroutine(this.RegenStamina());
        }
        #endregion

        #region Regen
        private IEnumerator RegenHealth()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                this.AddHealth(1);
                this.RefreshUI();
            }
        }

        private IEnumerator RegenMana()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                this.AddMana(1);
            }
        }

        private IEnumerator RegenStamina()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                this.AddStamina(1);
            }
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

        public const float StatMultiplier = 1.5f;

        #region Vitality
        public void AddVitality(int amount)
        {
            if (!CanUpgrade()) return;
            Vitality += amount;
            MaxHealth = BASE_HEALTH + (int)(Vitality * StatMultiplier);
        }

        public void RemoveVitality(int amount)
        {
            Vitality -= amount;
            MaxHealth = BASE_HEALTH + (int)(Vitality * StatMultiplier);
        }
        #endregion

        #region Dexterity
        public void AddDexterity(int amount)
        {
            if (!CanUpgrade()) return;
            Dexterity += amount;
            MaxStamina = BASE_STAMINA + (int)(Dexterity * StatMultiplier);
        }

        public void RemoveDexterity(int amount)
        {
            Dexterity -= amount;
            MaxStamina = BASE_STAMINA + (int)(Dexterity * StatMultiplier);
        }
        #endregion

        #region Mind
        public void AddMind(int amount)
        {
            if (!CanUpgrade()) return;
            Mind += amount;
            MaxMana = BASE_MANA + (int)(Mind * StatMultiplier);
        }

        public void RemoveMind(int amount)
        {
            Mind -= amount;
            MaxMana = BASE_MANA + (int)(Mind * StatMultiplier);
        }
        #endregion

        #region Intelligence
        public void AddIntelligence(int amount)
        {
            if (!CanUpgrade()) return;
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
            if (!CanUpgrade()) return;
            Defense += amount;
        }

        public void RemoveDefense(int amount)
        {
            Defense -= amount;
        }
        #endregion

        #region Level
        public void BuyLevel(int amount)
        {
            if (RemoveGold((int)(LevelToGoldPriceRatio * (amount + Level))))
            {
                Level += amount;
                // return true;
            }
            // return false;
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

        public void RefreshUI()
        {
            this.healthText.text = "Health:  " + Health + "/" + MaxHealth;
            this.healthBar.GetComponentInChildren<ProgressBar>().SetProgress((float)Health / MaxHealth);
            this.manaText.text = "Mana:  " + Mana + "/" + MaxMana;
            this.manaBar.GetComponentInChildren<ProgressBar>().SetProgress((float)Mana / MaxMana);
            this.staminaText.text = "Stamina:  " + Stamina + "/" + MaxStamina;
            this.staminaBar.GetComponentInChildren<ProgressBar>().SetProgress((float)Stamina / MaxStamina);

            this.vitalityText.text = "Vitality:  " + Vitality;
            this.dexterityText.text = "Dexterity:  " + Dexterity;
            this.mindText.text = "Mind:  " + Mind;
            this.intelligenceText.text = "Intelligence:  " + Intelligence;
            this.defenseText.text = "Defense:  " + Defense;

            this.levelText.text = Level.ToString();
            this.goldText.text = Gold.ToString();

            this.editStatsPanel.SetActive(CanUpgrade());

            int goldForNextLevel = (int)(LevelToGoldPriceRatio * (1 + Level));
            bool canBuyLevel = this.Gold >= goldForNextLevel;
            this.nextLevelButton.SetActive(canBuyLevel);
            this.nextLevelText.text = goldForNextLevel.ToString();
        }

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

        public void UpdateMaxStats()
        {
            MaxHealth = BASE_HEALTH + (int)(Vitality * StatMultiplier);
            MaxMana = BASE_MANA + (int)(Mind * StatMultiplier);
            MaxStamina = BASE_STAMINA + (int)(Dexterity * StatMultiplier);
        }

        private bool CanUpgrade()
        {
            var statSum = Vitality + Dexterity + Mind + Intelligence + Defense;
            statSum -= _baseVitality + _baseDexterity + _baseMind + _baseIntelligence + _baseDefense;

            if (statSum < this._level + 5)
            {
                return true;
            }
            return false;
        }

        public void ApplyItemsStats(List<Item> items)
        {
            int baseVitality = 0;
            int baseDexterity = 0;
            int baseMind = 0;
            int baseIntelligence = 0;
            int baseDefense = 0;

            foreach (var item in items)
            {
                foreach (var stat in item.Stats)
                {
                    switch (stat.StatName)
                    {
                        case StatName.Vitality:
                            baseVitality += stat.StatValue;
                            break;
                        case StatName.Dexterity:
                            baseDexterity += stat.StatValue;
                            break;
                        case StatName.Mind:
                            baseMind += stat.StatValue;
                            break;
                        case StatName.Intelligence:
                            baseIntelligence += stat.StatValue;
                            break;
                        case StatName.Defense:
                            baseDefense += stat.StatValue;
                            break;
                        default:
                            break;
                    }
                }
            }

            this._baseVitality = baseVitality;
            this._baseDexterity = baseDexterity;
            this._baseMind = baseMind;
            this._baseIntelligence = baseIntelligence;
            this._baseDefense = baseDefense;
            this.UpdateMaxStats();
        }

    }

}