namespace Player.IInitializable
{
    using UnityEngine;

    public class PlayerRotator : MonoBehaviour, IManagerInitializable
    {
        [Header("Limits")] 
        [SerializeField] private float maxYRotation;
        [SerializeField] private float minYRotation;

        [Header("Speed")]
        [SerializeField] private float xSpeed;
        [SerializeField] private float ySpeed;

        [Header("Camera")]
        [SerializeField] private Camera playerCamera;

        private PlayerManager _manager;


        private void FixedUpdate()
        {
            RotateX();
            RotateY();
        }

        public void Init(PlayerManager manager)
        {
            _manager = manager;
        }

        private void RotateY()
        {
            var vector3 = Vector3.right * (xSpeed * -_manager.PlayerController.MouseY);
            if (CheckBounds(vector3))
                playerCamera.transform.Rotate(vector3);
        }

        private void RotateX()
        {
            transform.Rotate(transform.up * (ySpeed * _manager.PlayerController.MouseX));
        }

        private bool CheckBounds(Vector3 vector3)
        {
            var rotation = (playerCamera.transform.rotation * Quaternion.Euler(vector3)).eulerAngles;

            return rotation.x switch
            {
                > 180 when rotation.x < maxYRotation => false,
                < 180 when rotation.x > minYRotation => false,
                _ => true
            };
        }
    }
}