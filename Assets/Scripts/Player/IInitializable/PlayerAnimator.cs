namespace Player.IInitializable
{
    using UnityEngine;

    public class PlayerAnimator : MonoBehaviour, IManagerInitializable
    {
        [Header("Animator")]
        [SerializeField] private Animator animator;
        
        [Header("Parameters")]
        [SerializeField] private string steppingParameter = "Stepping";
        [SerializeField] private string idleParameter = "Idle";
        [SerializeField] private string steppingSpeedParameter = "SteppingSpeed";
        
        [Header("Speed")]
        [SerializeField] private float speedMultiplier = 1;
        
        private int _idleId;
        private PlayerManager _manager;
        private PlayerAnimatorState _state;
        private int _steppingId;
        private int _steppingSpeedId;

        private void Update()
        {
            var vel = _manager.PlayerMovement.Rigidbody.velocity;
            var speed = Mathf.Sqrt(vel.x * vel.x + vel.z * vel.z);

            TryChangeAnimation(speed);
        }

        public void Init(PlayerManager manager)
        {
            _manager = manager;

            _steppingId = Animator.StringToHash(steppingParameter);
            _idleId = Animator.StringToHash(idleParameter);
            _steppingSpeedId = Animator.StringToHash(steppingSpeedParameter);

            SetStartAnimation();
        }

        private void SetStartAnimation()
        {
            _state = PlayerAnimatorState.Idle;
            animator.SetBool(_idleId, true);
        }

        private void TryChangeAnimation(float speed)
        {
            if (!_manager.JumpController.IsGrounded)
            {
                if (_state == PlayerAnimatorState.Jumping) return;
                
                _state = PlayerAnimatorState.Jumping;
                animator.Rebind();
            }
            else if (speed > 1)
            {
                animator.SetFloat(_steppingSpeedId, speed * speedMultiplier);

                if (_state == PlayerAnimatorState.Stepping) return;

                _state = PlayerAnimatorState.Stepping;
                animator.Rebind();
                animator.SetBool(_steppingId, true);
            }
            else
            {
                if (_state == PlayerAnimatorState.Idle) return;

                _state = PlayerAnimatorState.Idle;
                animator.Rebind();
                animator.SetBool(_idleId, true);
            }
        }

        private enum PlayerAnimatorState
        {
            Stepping,
            Jumping,
            Idle
        }
    }
}