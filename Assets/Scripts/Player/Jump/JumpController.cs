namespace Player.Jump
{
    using UnityEngine;

    public class JumpController : JumpControllerBase
    {
        public bool IsGrounded => Collisions.Count != 0;

        protected override void OnTriggerEnterInternal(Collider other)
        {
            Collisions.Add(other);
        }
    }
}