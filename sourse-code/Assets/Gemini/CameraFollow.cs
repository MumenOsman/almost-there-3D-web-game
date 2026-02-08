using UnityEngine;
using UnityEngine.InputSystem;

namespace Gemini
{
    public class CameraFollow : MonoBehaviour
    {
        [Header("Target & Follow")]
        public Transform target;
        public Vector3 offset = new Vector3(0, 1.5f, -5);
        public float smoothSpeed = 10f;

        [Header("Orbit Settings")]
        public float sensitivity = 0.2f;
        public float minPitch = -20f;
        public float maxPitch = 60f;

        private Vector2 _rotation;
        private PlayerInput _playerInput;
        private InputAction _lookAction;

        private void Start()
        {
            // Lock cursor for a better 3rd person feel
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Try to find PlayerInput for the "Look" action (Mouse Delta)
            if (target != null)
            {
                _playerInput = target.GetComponent<PlayerInput>();
                if (_playerInput != null)
                {
                    _lookAction = _playerInput.actions.FindAction("Look");
                }
            }

            // Initialize rotation based on current camera orientation
            Vector3 euler = transform.eulerAngles;
            _rotation.x = euler.y;
            _rotation.y = euler.x;
        }

        private void LateUpdate()
        {
            if (target == null) return;

            // Handle Mouse/RightStick Rotation
            if (_lookAction != null)
            {
                Vector2 lookInput = _lookAction.ReadValue<Vector2>();
                _rotation.x += lookInput.x * sensitivity;
                _rotation.y -= lookInput.y * sensitivity;
                _rotation.y = Mathf.Clamp(_rotation.y, minPitch, maxPitch);
            }

            // Calculate Position
            Quaternion rotation = Quaternion.Euler(_rotation.y, _rotation.x, 0);
            Vector3 desiredPosition = target.position + rotation * offset;
            
            // Smooth movement
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

            // Always look at the target (relative to offset height)
            Vector3 lookAtPos = target.position + Vector3.up * offset.y;
            transform.LookAt(lookAtPos);
        }
    }
}
