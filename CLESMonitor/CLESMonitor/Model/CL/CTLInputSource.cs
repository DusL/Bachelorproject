using System;

namespace CLESMonitor.Model.CL
{
    public class InputElement
    {
        public enum Type
        {
            /// <summary>Default value</summary>
            Unknown,
            Event,
            Task,
        }

        public enum Action
        {
            /// <summary>Default value</summary>
            Unknown,
            Started,
            Stopped,
        }

        public string identifier { get; private set; }
        public string name { get; private set; }
        public Type type { get; private set; }
        public Action action { get; private set; }
        public string secondaryIndentifier { get; set; }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="_identifier">the identifier</param>
        /// <param name="_name">the name</param>
        /// <param name="_type">the type</param>
        /// <param name="_action">the action</param>
        public InputElement(string _identifier, string _name, Type _type, Action _action)
        {
            identifier = _identifier;
            name = _name;
            type = _type;
            action = _action;        
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(Object obj)
        {
            bool equality = false;

            if (obj != null && obj.GetType().Equals(typeof(InputElement)))
            {
                InputElement otherInputElement = (InputElement)obj;
                if (this.identifier.Equals(otherInputElement.identifier)
                    && this.name.Equals(otherInputElement.name)
                    && this.type == otherInputElement.type
                    && this.action == otherInputElement.action)
                {
                    equality = true;
                }

                if (this.secondaryIndentifier != null
                    && !this.secondaryIndentifier.Equals(otherInputElement.secondaryIndentifier))
                {
                    equality = false;
                }
                else if (this.secondaryIndentifier == null
                    && otherInputElement.secondaryIndentifier != null)
                {
                    equality = false;
                }
            }

            return equality;
        }

        public override string ToString()
        {
            string returnString = "InputElement: identifier=" + identifier
                + " name=" + name + " type=" + type + " action=" + action 
                + " secondaryIndentifier=" + secondaryIndentifier;

            return returnString;
        }
    }

    /// <summary>
    /// The interface for a delegate of CTLInputSource.
    /// </summary>
    public interface CTLInputSourceDelegate
    {
        void eventHasStarted(InputElement eventElement);
        void eventHasStopped(InputElement eventElement);
        void taskHasStarted(InputElement taskElement);
        void taskHasStopped(InputElement taskElement);
    }

    /// <summary>
    /// This abstract class is used as the input source by the CTLModel object.
    /// </summary>
    public abstract class CTLInputSource
    {
        /// <summary>
        /// The delegate object.
        /// </summary>
        public CTLInputSourceDelegate delegateObject;

        public abstract void startReceivingInput();
        public abstract void stopReceivingInput();

        public abstract void reset();
        
    }
}
