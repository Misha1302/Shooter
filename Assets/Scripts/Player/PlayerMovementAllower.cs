using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerMovementAllower
    {
        private const float Tenth = 0.1f;
        private readonly PlayerManager _manager;
        private bool _autoJump;

        public PlayerMovementAllower(PlayerManager manager)
        {
            _manager = manager;
            _manager.AutoJumpController.ColliderEnterEvent += OnAutoJumpColliderEnter;
        }

        public bool CanMoveForward() => _manager.PlayerController.Ver > Tenth;

        public bool CanMoveBackward() => _manager.PlayerController.Ver < -Tenth;

        public bool CanMoveSideward() => Math.Abs(_manager.PlayerController.Hor) > Tenth;

        public bool CanJump() => CanJumpInternal();

        public bool CanStop() =>
            Math.Abs(_manager.PlayerController.Hor) < Tenth && Math.Abs(_manager.PlayerController.Ver) < Tenth;


        public static double GetHorizontalMagnitude(Vector3 rbVelocity) =>
            Math.Sqrt(rbVelocity.x * rbVelocity.x + rbVelocity.z * rbVelocity.z);


        private bool CanJumpInternal()
        {
            var value = _manager.JumpController.IsGrounded && (_manager.PlayerController.Jump || _autoJump);
            _autoJump = false;
            return value;
        }

        private void OnAutoJumpColliderEnter(List<Collider> colliders)
        {
            if (colliders.Count > 1)
                _autoJump = true;
        }
    }
}