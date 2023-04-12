using UnityEngine;
using HellsGate.Manager;
using System.Collections;

namespace HellsGate.PlayerCharacter
{
    public class PlayerCharacterStats : MonoBehaviour
    {
        // =========== Constants ===========
        #region Constants
        [SerializeField] private const int BASE_HEALTH = 100;
        [SerializeField] private const int BASE_MANA = 100;
        [SerializeField] private const int BASE_STAMINA = 100;
        [SerializeField] private const int BASE_VITALITY = 10;
        [SerializeField] private const int BASE_DEXTERITY = 10;
        [SerializeField] private const int BASE_MIND = 10;
        [SerializeField] private const int BASE_INTELLIGENCE = 10;
        [SerializeField] private const int BASE_DEFENSE = 10;
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

    }

}