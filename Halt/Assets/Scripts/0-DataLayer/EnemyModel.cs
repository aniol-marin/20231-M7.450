using UnityEngine;

namespace Mole.Halt.PresentationLayer.Models
{
    /// <summary>
    /// Data model for enemies
    /// 
    /// Implements damage logic.
    /// NOTE: damage is NOT readonly.
    /// 
    /// TODO
    /// - Make asset data readonly
    /// - implement data instantiation and reset
    /// </summary>
    [CreateAssetMenu(fileName = "Generic Enemy Model", menuName = "Mole/Joking Sam/Enemy")]
    public class EnemyModel : ScriptableObject
    {
        [SerializeField] private string _name = "Generic Enemy";
        [SerializeField] private float _auditionThreshold = 10f;
        [SerializeField] private float _visionThreshold = 10f;
        [SerializeField] private float _health = 100f;
        [SerializeField] private WeaponModel _weapon = null;

        public string Name => _name;
        public float AuditionThreshold => _auditionThreshold;
        public float VisionThreshold => _visionThreshold;
        public float Health => _health;
        public WeaponModel Weapon => _weapon;
        public bool CriticalHealth => _health < 20f;
        public bool Dead => _health <= 0f;

        public void TakeDamage(float damage)
        {
            _health -= damage;
        }
    }
}
