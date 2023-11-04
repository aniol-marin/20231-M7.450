using Mole.Halt.DataLayer;
using Mole.Halt.GameLogicLayer;
using Mole.Halt.GameLogicLayer.Internal;
using Mole.Halt.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mole.Halt.PresentationLayer
{
    public class EventParser // TODO extract interface / base class, split between production and debug parsers
    {
        [Injected] private readonly ICensusManager census;
        private IEnumerable<string> rest;

        private string ReconstructMessage()
        {
            string message = string.Join("-", rest);
            rest = Enumerable.Empty<string>();
            return message;
        }

        public bool Try(string message, Callback<GameEvent> callback)
        {
            rest = Enumerable.Empty<string>();
            IEnumerable<string> tokens = message
                .Split();

            GameEvent gameEvent = tokens.DequeueFirst(out rest) switch
            {
                "mock" => new MockEvent(ReconstructMessage()),
                "move" => TryParseOrder(rest),
                _ => null,
            };

            bool parsed = gameEvent != null;

            if (parsed)
            {
                callback(gameEvent);
            }

            if (!rest.Empty())
            {
                string unparsed = string.Empty;
                rest.ForEach(t => unparsed += $"[{t}]");

                new Warning($"Some tokens were ignored: {unparsed}");
            }

            return parsed;
        }

        private OrderEvent TryParseOrder(IEnumerable<string> arguments)
        {
            OrderEvent order;
            string token = arguments.DequeueFirst(out rest);
            try
            {
                EntityId id = ParseEntity(token);
                if (census.Active.None(c => c == id))
                {
                    new Warning($"targeting non-active entity: {id}");
                }

                token = rest.DequeueFirst(out rest);
                Position pos = ParsePosition(rest);

                order = new OrderEvent(OrderType.reach, id, pos, BehaviorType.mock);
            }
            catch
            {
                new Warning($"invalid order token: {token}");
                order = null;
            }

            return order;
        }

        private EntityId ParseEntity(string token)
        {
            return Enum
                .TryParse(token, out EntityId id)
                ? id
                : throw new Exception();
        }

        private Position ParsePosition(IEnumerable<string> tokens)
        {
            float result;

            float x = float.TryParse(tokens.DequeueFirst(out rest), out result) ? result : 0;
            float y = float.TryParse(rest.DequeueFirst(out rest), out result) ? result : 0;
            float z = float.TryParse(rest.DequeueFirst(out rest), out result) ? result : 0;

            return new Position(x, y, z);

        }
    }
}