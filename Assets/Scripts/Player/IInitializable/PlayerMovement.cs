namespace Player.IInitializable
{
    using System;
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour, IManagerInitializable
    {
        [Header("Speeds")]
        [SerializeField] private float forwardSpeed;
        [SerializeField] private float backwardSpeed;
        [SerializeField] private float sidewardSpeed;
        [SerializeField] private float stopSpeed;

        [Header("Other settings")] 
        [SerializeField] private float runMultiplier;

        private PlayerManager _manager;
        private float _multiplier;
        
        public Rigidbody Rigidbody { get; private set; }
        [NonSerialized] public PlayerMovementAllower MovementAllower;


        private void FixedUpdate()
        {
            _multiplier = _manager.PlayerController.Run ? runMultiplier : 1;
            MoveVertical();
            MoveHorizontal();
            Stop();


            void MoveVertical()
            {
                if (MovementAllower.CanMoveForward())
                    Rigidbody.AddForce(Rigidbody.transform.forward * (forwardSpeed * _multiplier * _manager.PlayerController.Ver));
                else if (MovementAllower.CanMoveBackward())
                    Rigidbody.AddForce(Rigidbody.transform.forward * (backwardSpeed * _multiplier * _manager.PlayerController.Ver));
            }

            void MoveHorizontal()
            {
                if (MovementAllower.CanMoveSideward())
                    Rigidbody.AddForce(Rigidbody.transform.right * (sidewardSpeed * _multiplier * _manager.PlayerController.Hor));
            }

            void Stop()
            {
                if (!MovementAllower.CanStop())
                    return;

                if (PlayerMovementAllower.GetHorizontalMagnitude(Rigidbody.velocity) < 1)
                {
                    var rbVelocity = Vector3.zero;
                    rbVelocity.y = Rigidbody.velocity.y;
                    Rigidbody.velocity = rbVelocity;
                    return;
                }

                var velocity = Rigidbody.velocity;

                var dividedVel = velocity / stopSpeed;
                dividedVel.y = velocity.y;

                Rigidbody.velocity = dividedVel;
            }
        }

        public void Init(PlayerManager manager)
        {
            _manager = manager;
            Rigidbody = GetComponent<Rigidbody>();
            Rigidbody.drag = 1;
        
            MovementAllower = new PlayerMovementAllower(_manager);
        }
    }
}