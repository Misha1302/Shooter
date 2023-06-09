namespace Player.Jump
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    ///     Do not define the OnTriggerEnter and OnTriggerExit methods in the child class
    /// </summary>
    public abstract class JumpControllerBase : MonoBehaviour
    {
        [SerializeField] protected int groundLayerMask = 3;
        protected readonly List<Collider> Collisions = new();

        private void OnTriggerEnter(Collider other)
        {
            if ((other.gameObject.layer & groundLayerMask) != groundLayerMask)
                return;

            OnTriggerEnterInternal(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if ((other.gameObject.layer & groundLayerMask) == groundLayerMask)
                Collisions.Remove(other);
        }

        protected abstract void OnTriggerEnterInternal(Collider other);
    }
}