using Mole.Halt.GameLogicLayer;
using Mole.Halt.Utils;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Mole.Halt.Design
{
    public enum StartType
    {
        PreprocessHistory,
        Wait,
        SkipToStep,
        Wipe,
    }

    [LevelDesign]
    public class LogicDebug : NodeClass
    {
        [Injected] private readonly IQueueManager queue;
        [SerializeField] private GameMessageWrapper asset;
        [SerializeField] private History history;
        [SerializeField] private Button send;
        [SerializeField] private Button prune;
        [SerializeField] private Button push;
        [SerializeField] private Button next;
        [SerializeField] private Button wipe;
        [SerializeField] private StartType startType;
        [SerializeField] private uint step;

        private void Awake()
        {
            send.onClick.AddListener(SendMessage);
            prune.onClick.AddListener(PruneHistory);
            push.onClick.AddListener(PushHistory);
            next.onClick.AddListener(StepToNext);
            wipe.onClick.AddListener(WipeHistory);

            switch (startType)
            {
                case StartType.PreprocessHistory:
                    history
                        .PastEvents
                        .ForEach(w => SendMessage(w));
                    step = (uint)history.PastEvents.Count();
                    break;
                case StartType.Wait:
                    break;
                case StartType.Wipe:
                    history.Clear();
                    break;
                case StartType.SkipToStep:
                    int target = (int)step;
                    step = 0;
                    for (int i = 0; i < target; ++i)
                    {
                        StepToNext();
                    }
                    break;
                default:
                    new Error($"start type {startType} not implemented yet");
                    break;
            }
        }

        private void SendMessage()
        {
            SendMessage(asset);
            history.Add(asset);
        }

        private void PruneHistory() {
            history.Prune();
        }

        private void WipeHistory() {
            history.Clear();
        }

        private void PushHistory() {
            history.Push();
        }

        private void StepToNext() {
            GameMessageWrapper @event = history.GetStep((int)step);
            queue.ReportEvent(@event.GameEvent);
            ++step;
        }

        private void SendMessage(GameMessageWrapper wrapper) { 
            queue.ReportEvent(wrapper.GameEvent);
        }
    }
}