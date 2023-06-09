namespace Player.Jump
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Events;

    public class AutoJumpController : JumpControllerBase
    {
        [SerializeField] private float maxJumpHeight;
        public UnityAction<List<Collider>> ColliderEnterEvent;
    
    
        protected override void OnTriggerEnterInternal(Collider other)
        {
            var topPosition = other.GetComponent<MeshFilter>().sharedMesh.vertices.OrderByDescending(x => x.y).First() + other.transform.position;

            if (topPosition.y > transform.position.y + maxJumpHeight)
                return;

            Collisions.Add(other);
            ColliderEnterEvent?.Invoke(Collisions);
        }
    }
}