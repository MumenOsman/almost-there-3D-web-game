using UnityEngine;
using UnityEngine.InputSystem;

namespace Gemini
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(PlayerInput))]
    public class RabbitController : MonoBehaviour
    {
        [Header("Movement Settings")]
        public float moveSpeed = 5f;
        public float rotationSpeed = 10f;
        public float gravity = -9.81f;
        public float jumpHeight = 1.0f;
        public float pushPower = 2.0f;

        [Header("References")]
        public Transform cameraTransform;

        private CharacterController _characterController;
        private Animator _animator;
        private PlayerInput _playerInput;
        private InputAction _moveAction;
        private InputAction _jumpAction;
        
        private Vector3 _velocity;
        private bool _isGrounded;

        // Animator Parameter Hashes
        private static readonly int SpeedHash = Animator.StringToHash("Speed");
        private static readonly int IsMovingHash = Animator.StringToHash("IsMoving");
        private static readonly int JumpHash = Animator.StringToHash("Jump");
        private static readonly int VerticalVelocityHash = Animator.StringToHash("VerticalVelocity");
        private static readonly int IsGroundedHash = Animator.StringToHash("IsGrounded");

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();
            _playerInput = GetComponent<PlayerInput>();

            // The default InputSystem_Actions asset uses "Move" for WASD/LeftStick and "Jump" for Space
            _moveAction = _playerInput.actions.FindAction("Move");
            _jumpAction = _playerInput.actions.FindAction("Jump");

            if (cameraTransform == null && Camera.main != null)
            {
                cameraTransform = Camera.main.transform;
            }
        }

        private void Update()
        {
            // Ground Check
            _isGrounded = _characterController.isGrounded;
            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }

            // Input using New Input System
            Vector2 inputVector = _moveAction != null ? _moveAction.ReadValue<Vector2>() : Vector2.zero;
            Vector3 direction = new Vector3(inputVector.x, 0f, inputVector.y).normalized;

            // Movement
            if (direction.magnitude >= 0.1f && _characterController.enabled)
            {
                // Calculate rotation angle based on camera view
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
                float angle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime);
                
                // Rotate character
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                // Move in direction of rotation
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                _characterController.Move(moveDir.normalized * moveSpeed * Time.deltaTime);

                // Update Animator
                _animator.SetFloat(SpeedHash, direction.magnitude * moveSpeed);
                _animator.SetBool(IsMovingHash, true);
            }
            else
            {
                _animator.SetFloat(SpeedHash, 0f);
                _animator.SetBool(IsMovingHash, false);
            }

            // Jump logic
            if (_isGrounded && _jumpAction != null && _jumpAction.triggered)
            {
                _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                _animator.SetTrigger(JumpHash);

                // Play Jump Sound
                if (jumpSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(jumpSound, jumpVolume);
                }
            }

            // Gravity
            if (_characterController.enabled)
            {
                _velocity.y += gravity * Time.deltaTime;
                _characterController.Move(_velocity * Time.deltaTime);
            }

            // Sync grounding and velocity with Animator
            _animator.SetBool(IsGroundedHash, _isGrounded);
            _animator.SetFloat(VerticalVelocityHash, _velocity.y);
        }

        public void Bounce(float force)
        {
            _velocity.y = force;
            _animator.SetTrigger(JumpHash);
            Debug.Log($"Gemini: Rabbit bounced with force: {force}");
        }

        [Header("Audio")]
        public AudioSource audioSource;
        public AudioClip pushSound;
        public AudioClip jumpSound;
        [Range(0f, 1f)] public float jumpVolume = 1f;
        private float _lastPushTime;

        // ... existing code ...

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            // --- Physics Pushing ---
            Rigidbody body = hit.collider.attachedRigidbody;
            // Don't push kinematic rigidbodies or objects without rigidbodies
            if (body != null && !body.isKinematic)
            {
                // We don't want to push objects below us
                if (hit.moveDirection.y < -0.3f) return;

                // Calculate push direction from move direction, we only push horizontally
                Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

                // Apply the push
                body.linearVelocity = pushDir * moveSpeed * pushPower;

                // Play Sound
                if (pushSound != null && audioSource != null && Time.time > _lastPushTime + 0.5f)
                {
                    audioSource.PlayOneShot(pushSound);
                    _lastPushTime = Time.time;
                }
            }

            // --- Collectible Logic ---
            Collectible collectible = hit.gameObject.GetComponent<Collectible>();
            if (collectible == null) collectible = hit.gameObject.GetComponentInParent<Collectible>();

            if (collectible != null)
            {
                var tracker = GetComponent<ScoreTracker>();
                collectible.Collect(tracker);
            }
        }
    }
}
