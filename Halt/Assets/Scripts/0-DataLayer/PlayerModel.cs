using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Mole.Halt.PresentationLayer.Models
{
    /// <summary>
    /// Data model for player representation.
    /// 
    /// All the serialized fields are read-only. Handles basic weapon reset and update logic.
    /// 
    /// TODO
    /// - Implement transient save states through scriptable object instances and checkpoint interaction
    /// - Implement persistent save states through serialization
    /// </summary>
    [CreateAssetMenu(fileName = "Player Model", menuName = "Mole/Joking Sam/Player")]
    public class PlayerModel : ScriptableObject
    {
        private const float _MAX_HEALTH = 100f;
        private const float _MAX_SHIELD = 200f;
        private const float _SHIELD_DAMPENING_FACTOR = 0.9f;

        [SerializeField] private string _name = "Player";
        [SerializeField] private float _health = 100f;
        [SerializeField] private float _shield = 100f;
        [SerializeField] private List<WeaponModel> _weaponsReadonly = null;
        private List<WeaponModel> _weapons = null;

        public Action<float> OnHealthUpdated = null;
        public Action<float> OnShieldUpdated = null;
        public string Name => _name;
        public float Health => _health;
        public float Shield => _shield;
        public List<WeaponModel> Weapons
        {
            get { if (_weapons == null) ResetWeapons(); return _weapons; }
            private set => _weapons = value;
        }
        /// <summary>
        /// Resets the current weapon data to default
        /// </summary>
        public void ResetWeapons()
        {
            _weapons = new List<WeaponModel>(_weaponsReadonly);
            foreach (WeaponModel weapon in _weapons)
            {
                weapon.ResetAmmunition();
            }
        }
        /// <summary>
        /// Handles health increase logic
        /// 
        /// TODO
        /// - remove the warning upon poison logic implementation
        /// </summary>
        /// <param name="amount">the raw amount of health to be increased (may be modified upon reaching the limit)</param>
        public void IncreaseHealth(float amount)
        {
            if (amount < 0)
            {
                Debug.LogWarning("Tried to decrease player health bypassing the damage system.");
            }
            _health += amount;
            if (_health > _MAX_HEALTH)
            {
                _health = _MAX_HEALTH;
            }
            else if (_health < 0)
            {
                _health = 0;
            }
            OnHealthUpdated?.Invoke(_health);
        }
        /// <summary>
        /// Handles shield increase logic
        /// </summary>
        /// <param name="amount">the raw amount of shield to be increased (may be modified upon reaching the limit)param>
        public void IncreaseShield(float amount)
        {
            _shield += amount;
            if (_shield > _MAX_SHIELD)
            {
                _shield = _MAX_SHIELD;
            }
            else if (_shield < 0)
            {
                _shield = 0;
            }
            OnShieldUpdated?.Invoke(_shield);
        }
        /// <summary>
        /// Handles damage logic.
        /// 
        /// The damage is first split into dampening and residual. Player always takes the residue.
        /// Dampening may be fully absorbed by the shield if smaller than its value.
        /// In case of insufficient shield the player takes all the remnant damage.
        /// 
        /// </summary>
        /// <param name="damage"></param>
        public void TakeDamage(float damage)
        {
            // Local functions
            float residualDamage() => damage * (1 - _SHIELD_DAMPENING_FACTOR);
            float shieldDampening()
            {
                _shield -= damage * _SHIELD_DAMPENING_FACTOR;
                float undampened = (_shield < 0f) ? -_shield : 0f; // relocates the remnant value in case it was bigger than the shield
                _shield = (_shield > 0f) ? _shield : 0f; // avoids persistent negative shield values
                return undampened;
            };

            _health -= shieldDampening() + residualDamage();

            if (_health <= 0)
            {
                _health = 0; // avoids persistent negative shield values
            }

            OnHealthUpdated?.Invoke(_health);
            OnShieldUpdated?.Invoke(_shield);

        }
    }
}
