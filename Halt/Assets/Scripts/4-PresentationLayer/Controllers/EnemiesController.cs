using Mole.Halt.ApplicationLayer;
using Mole.Halt.ApplicationLayer.Interfaces;
using UnityEngine;
using Zenject;

namespace Mole.Halt.PresentationLayer.Controllers
{
    /// <summary>
    /// Controller for centralization of all enemies control
    /// PROTOTYPE
    /// 
    /// TODO
    /// - move here the relevant code from view and model (i.e. AI state machine)
    /// </summary>
    public class EnemiesController : MonoBehaviour, Resettable
    {
        [Inject] Resetter _resetter;

        /// <summary>
        /// Resettable implementation
        /// </summary>
        public void ResetData()
        {
            //_enemies.Clear();
        }


        public void AttachToReset()
        {
            throw new System.NotImplementedException();
        }
    }
}