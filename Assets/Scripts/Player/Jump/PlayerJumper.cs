namespace Player.Jump
{
    using Player.IInitializable;
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody))]
    public class PlayerJumper : MonoBehaviour, IManagerInitializable
    {
        [SerializeField] private float jumpForce;

        private PlayerManager _manager;
        private Rigidbody _rb;


        private void FixedUpdate()
        {
            if (!_manager.PlayerMovement.MovementAllower.CanJump()) return;

            Invoke(nameof(Jump), 0.1f);
        }

        public void Init(PlayerManager manager)
        {
            _rb = GetComponent<Rigidbody>();
            _manager = manager;
        }

        private void Jump()
        {
            var vel = _rb.velocity;
            _rb.velocity = new Vector3(vel.x, jumpForce, vel.z);
        }
    }
}