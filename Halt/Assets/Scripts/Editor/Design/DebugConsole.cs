using Mole.Halt.ApplicationLayer;
using Mole.Halt.GameLogicLayer;
using Mole.Halt.PresentationLayer;
using Mole.Halt.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Mole.Halt.Design
{
    [LevelDesign]
    public class DebugConsole : NodeClass, Initializable
    {
        [Injected] private readonly IQueueManager queue;
        [Injected] private readonly IUserInputService input;
        [Injected] private readonly EventParser parser;
        [SerializeField] private TMP_InputField entered;
        [SerializeField] private TMP_Text output;
        [SerializeField] private Button button;

        public void Init()
        {
            Deinit();

            button.onClick.AddListener(Clear);

            input.OnSubmit += SendMessage;
            input.OnToggleConsole += HandleToggle;
            queue.OnEventDequeued += AddMessage;

            output.text = string.Empty;
        }

        private void HandleToggle(bool toggle)
        {
            ToggleVisuals(toggle);

            if (toggle)
            {
                Focus();
            }
        }

        public void Deinit()
        {
            button.onClick.RemoveAllListeners();

            input.OnSubmit -= SendMessage;
            input.OnToggleConsole -= ToggleVisuals;
            queue.OnEventDequeued -= AddMessage;
        }

        private void SendMessage()
        {
            Parse(entered.text);
            entered.text = string.Empty;
        }

        private void AddMessage(GameEvent @event)
        {
            output.text += $"\n{@event}";
        }

        private void Clear()
        {
            output.text = string.Empty;
        }

        private void SendMessage(GameEvent @event)
        {
            queue.ReportEvent(@event);
        }

        private void Parse(string message)
        {
            bool parsed = parser.Try(message, SendMessage);

            if (!parsed)
            {
                new Warning($"invalid message, will be ignored: [{message}]");
            }

            Focus();
        }

        private void Focus()
        {
            entered.Select();
            entered.ActivateInputField();
        }
    }
}