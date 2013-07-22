using System;

namespace CLESMonitor.Model.CL
{
    public class InputElement
    {
        /// <summary>Specifies the type of the InputElement; either a task or an event</summary>
        public enum Type
        {
            /// <summary>Default value</summary>
            Unknown,
            Event,
            Task,
        }

        /// <summary> Specifies weather the InputElement represents an element that has stopped or started this second</summary>
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

        /// <summary>
        /// Creates a string representation of an InputElement.
        /// </summary>
        /// <returns>A string</returns>
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
        /// <summary> When an event has started, this method will be called </summary>
        /// <param name="eventElement">An input element representing an event</param>
        void eventHasStarted(InputElement eventElement);
        /// <summary>  When an event has stoped, this method will be called </summary>
        /// <param name="eventElement">An input element representing an event</param>
        void eventHasStopped(InputElement eventElement);
        /// <summary> When an event has started, this method will be called </summary>
        /// <param name="taskElement">An input element representing a task</param>
        void taskHasStarted(InputElement taskElement);
        /// <summary> When an event has started, this method will be called </summary>
        /// <param name="taskElement">An input element representing a task</param>
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

        /// <summary>This method should implement a way to start receiving input</summary>
        public abstract void startReceivingInput();
        /// <summary>This method should implement a way to stop receiving input</summary>
        public abstract void stopReceivingInput();
        /// <summary>This method should implement a way to reset the CTLInputSource </summary>
        public abstract void reset();
    }
}
