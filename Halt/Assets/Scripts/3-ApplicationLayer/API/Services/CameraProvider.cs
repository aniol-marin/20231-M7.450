using Mole.Halt.Utils;
using System;
using UnityEngine;

namespace Mole.Halt.ApplicationLayer
{
    public class CameraProvider : NodeClass
    {
        [SerializeField] private Camera main;

        public Camera ActiveCamera => main;

        public void SwitchCamera()
        {
            throw new NotImplementedException();
        }
    }
}