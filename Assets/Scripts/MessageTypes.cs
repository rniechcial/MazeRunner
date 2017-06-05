namespace TGK.Communication
{
    using UnityEngine;

    namespace Messages
    {
        #region Actions
        public class Action
        {
            public Component Sender;
        }

        public class KeyAction
        {
            public Key key;
        }
        #endregion Actions

        #region Health
        public class Damage
        {
            public float Value;
        }

        public class HealthDepleated { }
        #endregion Health
    }
}