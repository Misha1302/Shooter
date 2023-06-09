namespace Player
{
    using UnityEngine;

    public class PlayerController : MonoBehaviour
    {
        private bool _cursorLocked = true;
        public float Hor { get; private set; }
        public float Ver { get; private set; }
        public float MouseX { get; private set; }
        public float MouseY { get; private set; }

        public bool Run { get; private set; }
        public bool Jump { get; private set; }

        private void Start()
        {
            LockCursor();
        }

        private void Update()
        {
            Hor = Input.GetAxis("Horizontal");
            Ver = Input.GetAxis("Vertical");
            Run = Input.GetKey(KeyCode.LeftShift);
            MouseX = Input.GetAxis("Mouse X");
            MouseY = Input.GetAxis("Mouse Y");
            Jump = Input.GetKeyDown(KeyCode.Space);

            HandleCursor();
        }

        private void HandleCursor()
        {
            if (!Input.GetKeyDown(KeyCode.Escape)) return;
            _cursorLocked = !_cursorLocked;

            if (!_cursorLocked)
                UnlockCursor();
            else LockCursor();
        }

        private static void LockCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private static void UnlockCursor()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}