using UnityEngine;

namespace Gemini
{
    [RequireComponent(typeof(Rigidbody))]
    public class PoolBall : MonoBehaviour
    {
        [Header("Settings")]
        public float resetYThreshold = -10f;
        public bool resetOnFall = true;

        private Vector3 _startPosition;
        private Rigidbody _rb;

        private void Start()
        {
            _startPosition = transform.position;
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (resetOnFall && transform.position.y < resetYThreshold)
            {
                ResetBall();
            }
        }

        public void ResetBall()
        {
            _rb.linearVelocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            transform.position = _startPosition;
            Debug.Log($"Gemini: {gameObject.name} reset to start position.");
        }
    }
}
