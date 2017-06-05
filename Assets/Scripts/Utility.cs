namespace TGK.Communication
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public static class Utility
    {
        /// <summary>
        /// Get all the colliding gameObjects
        /// </summary>
        public static IEnumerable<GameObject> OverlapSphere(Vector3 position, float radius,
            bool attachedRigidbody = true)
        {
            return Physics.OverlapSphere(position, radius).Select(x =>
            {
                // If the collider is a part of a rigidbody
                // and the user specified that the attached rigidbody gameobject
                // should be returned
                if (attachedRigidbody && x.attachedRigidbody != null)
                {
                    return x.attachedRigidbody.gameObject;
                }
                return x.gameObject;
            }
            );
        }
    }
}