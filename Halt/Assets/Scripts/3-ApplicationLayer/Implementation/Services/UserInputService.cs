using Mole.Halt.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Mole.Halt.ApplicationLayer
{
    public class UserInputService : Initializable, IUserInputService
    {
        [Injected] private readonly InputActionsInstaller input;
        [Injected] private readonly CameraProvider cameras;
        private bool consoleToggled = false;

        public event Callback<Position> OnPositionRequested = delegate { };
        public event Callback OnSubmit = delegate { };
        public event Callback<bool> OnToggleConsole = delegate { };

        public void Init()
        {
            input.Enable();
            input.UI.Primarypointer.performed += MockInput;
            input.UI.Submit.performed += HandleSubmit;
            input.UI.ToggleConsole.performed += HandleToggleConsole;
        }

        public void Deinit()
        {
        }

        private void MockInput(InputAction.CallbackContext _)
        {
            if (!consoleToggled)
            {
                Ray screenToPoint = cameras.ActiveCamera.ScreenPointToRay(Input.mousePosition);
                Position destination = Physics.Raycast(screenToPoint, out RaycastHit info) ? info.point.ToPosition() : Vector3.zero.ToPosition();

                OnPositionRequested(destination);
            }
        }

        private void HandleSubmit(InputAction.CallbackContext _)
        {
            OnSubmit();
        }

        private void HandleToggleConsole(InputAction.CallbackContext _)
        {
            consoleToggled = !consoleToggled;

            OnToggleConsole(consoleToggled);
        }
    }
}
