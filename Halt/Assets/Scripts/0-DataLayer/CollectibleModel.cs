using UnityEngine;


namespace Mole.Halt.PresentationLayer.Models
{
    /// <summary>
    /// Data model for collectibles.
    /// </summary>
    [CreateAssetMenu(fileName = "asset-collectible-generic", menuName = "Mole/Joking Sam/Collectible")]
    public class CollectibleModel : ScriptableObject
    {
        [SerializeField] private string _name = "Generic Collectible";
        [SerializeField] private int _health = 100;
        [SerializeField] private int _shield = 100;
        [SerializeField] private bool _containsAmmunition = true;
        [SerializeField] private int _ammunition = 100;
        [SerializeField] private bool _containsWeapon = false;
        [SerializeField] private WeaponModel _weapon = null;
        [SerializeField] private bool _containsKey = false;

        public string Name => _name;
        public int Health => _health;
        public int Shield => _shield;
        public int Ammunition => _ammunition;
        public bool ContainsKey => _containsKey;
        public bool ContainsAmmunition => _containsAmmunition;
        public bool ContainsWeapon => _containsWeapon;
        public WeaponModel Weapon => _weapon;
    }
}