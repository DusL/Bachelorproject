using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLESMonitor.Model
{
    /// <summary>
    /// A parsed event, the PRLDomain can turn this into a CTLEvent
    /// </summary>
    public class ParsedEvent
    {
        public string identifier { get; private set; }
        public string type { get; private set; }

        public ParsedEvent(string _identifier, string _type)
        {
            identifier = _identifier;
            type = _type;
        }
    }

    /// <summary>
    /// A parsed task, the PRLDomain can turn this into a CTLTask
    /// </summary>
    public class ParsedTask
    {
        public string identifier { get; private set; }
        public string type { get; private set; }

        public ParsedTask(string _identifier, string _type)
        {
            identifier = _identifier;
            type = _type;
        }
    }

    public class InputElement
    {
        public enum Type
        {
            /// <summary>
            /// Default value
            /// </summary>
            Unknown,
            Event,
            Task,
        }

        public enum Action
        {
            /// <summary>
            /// Default value
            /// </summary>
            Unknown,
            Started,
            Stopped,
        }

        public string identifier { get; private set; }
        public string name { get; private set; }
        public Type type { get; private set; }
        public Action action { get; private set; }

        public InputElement(string _identifier, string _name, Type _type, Action _action)
        {
            identifier = _identifier;
            name = _name;
            action = _action;
        }
    }

    public interface CTLInputSourceDelegate
    {
        void eventHasStarted(InputElement eventElement);
        void eventHasStopped(InputElement eventElement);
        void taskHasStarted(InputElement taskElement);
        void taskHasStopped(InputElement taskElement);
    }

    public abstract class CTLInputSource
    {
        public CTLInputSourceDelegate delegateObject;

        public abstract void startReceivingInput();
        public abstract void stopReceivingInput();
    }
}
