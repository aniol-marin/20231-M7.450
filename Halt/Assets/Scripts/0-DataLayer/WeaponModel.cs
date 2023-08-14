using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mole.Halt.PresentationLayer.Models
{
    /// <summary>
    /// Data model for fire weapons. 
    /// 
    /// All the serialized and public properties are readonly. 
    /// Handles basic logic errors on ammunition.
    /// 
    /// TODO:
    /// - Handle magazine capacity
    /// - Extend to weapon modifications
    /// </summary>
    [CreateAssetMenu(fileName = "Generic Weapon Model", menuName = "Mole/Joking Sam/Fire Weapon")]
    public class WeaponModel : ScriptableObject
    {
        [SerializeField] private string _name = "Generic Fire Weapon";
        [SerializeField] private bool _isAutomatic = false;
        [SerializeField] private int _ammunitionReadonly = 10;
        [SerializeField] private float _range = 20f;
        [SerializeField] private float _period = 0.2f;
        [SerializeField] private float _strength = 60f;
        [SerializeField] private float _loudness = 10f;
        [SerializeField, Range(0, 0.1f)] private double _imprecision = 0.0d;
        private int _ammunition;

        public void ResetAmmunition()
        {
            _ammunition = _ammunitionReadonly;
        }

        /// <summary>
        /// Public action called upon ammunition update. Includes the new updated amount.
        /// </summary>
        public Action<int> OnAmmunitionUpdate = null;

        public string Name => _name;
        public bool IsAutomatic => _isAutomatic;
        public int Ammunition { get => _ammunition; private set => UpdateAmmunition(value); }
        public float Range => _range;
        public float Period => _period;
        public float Strength => _strength;
        public float Loudness => _loudness;
        public double Imprecision => _imprecision;
        /// <summary>
        /// Handles ammunition logic. Throws exception 
        /// </summary>
        /// <param name="amount">the amount to be added. WARNING: use negative value for subtraction</param>
        public void UpdateAmmunition(int amount)
        {
            _ammunition += amount;
            OnAmmunitionUpdate?.Invoke(_ammunition);

            if (_ammunition < 0)
            {
                throw new Exception("tried to use a weapon beyond its ammunition limit");
            }
        }

    }
}