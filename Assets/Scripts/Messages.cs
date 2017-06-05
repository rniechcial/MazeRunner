namespace TGK.Communication
{
    using UnityEngine;
    using System.Reflection;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// Simple message dispatcher implementation
    /// </summary>
    public static class MessageDispatcher
    {
        #region Public Methods
        /// <summary>
        /// Send the messages to all the GameObjects in array
        /// </summary>
        public static void Send<T>(T message, IEnumerable<GameObject> objects) where T : class
        {
            foreach (var obj in objects)
            {
                InformGameObject(message, obj);
            }
            Break = false;
        }

        /// <summary>
        /// Send the messages to all components in GameObject
        /// </summary>
        public static void Send<T>(T message, GameObject obj) where T : class
        {
            InformGameObject(message, obj);
            Break = false;
        }

        /// <summary>
        /// Send message to specified component
        /// </summary>
        public static void Send<T>(T message, Component component) where T : class
        {
            InformComponent(message, component);
            Break = false;
        }

        /// <summary>
        /// Used for breaking further message dispatching
        /// </summary>
        public static bool Break = false;
        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Iterates through all object components
        /// </summary>
        private static void InformGameObject<T>(T message, GameObject obj) where T : class
        {
            // Get all GameObject components
            var components = obj.GetComponents<Component>();
            foreach (var component in components)
            {
                InformComponent(message, component);
            }
        }

        /// <summary>
        /// Finds appropriate methods to call using reflection,
        /// MessageDispatcher will call all methods that
        /// have only one parameter with the same type as the message sent
        /// </summary>
        private static void InformComponent<T>(T message, Component component) where T : class
        {
            // Get the component type
            var type = component.GetType();

            // Get all methods for that type, this includes:
            var methods = type.GetMethods(
                BindingFlags.Instance |         // instance methods
                BindingFlags.NonPublic |        // private and protected methods
                BindingFlags.Public |           // public methods
                BindingFlags.FlattenHierarchy | // public/protected static methods up the hierarchy
                BindingFlags.Static);           // static methods

            // Foreach found method
            foreach (var method in methods)
            {
                // Get its parameters
                var parameters = method.GetParameters();

                // Check if there is only one
                if (parameters.Length != 1)
                {
                    continue;
                }

                var parameterType = parameters[0].ParameterType;

                // And the type is the same as message type
                if (parameterType == message.GetType())
                {
                    // Conditional break of dispatching
                    // based on client request
                    if (Break == true)
                    {
                        return;
                    }

                    // Invoke found method
                    method.Invoke(component, new object[] { message });
                }
            }
        }
    }
    #endregion Private Methods
}