using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Player
{
    public class PlayerDash : MonoBehaviour
    {
        [SerializeField] private float dashForce;
        [SerializeField] private float dashTime;
        [SerializeField] private float dashCooldown;

        [Header("Sound")] 
        [SerializeField] private AudioSource source;
        [SerializeField] private AudioClip[] clips;
        
        private Vector2 _moveInput;
        private Rigidbody2D _rigidbody;
        private float _cooldownLeft;
        public bool CanDash { get; set; } = true;
        public bool IsDashing { get; private set; }
        
        public UnityEvent dashPerformed;
        public UnityEvent dashEnded;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            PlayerInputs.Instance.Game.Dash.performed += _ => Dash();
        }

        private void Update()
        {
            var input = PlayerInputs.Instance.Game.Move.ReadValue<Vector2>();
            if (input != Vector2.zero) _moveInput = input.normalized;
            
            if (_cooldownLeft > 0) _cooldownLeft -= Time.deltaTime;
        }

        private void Dash()
        {
            if (!IsDashing && CanDash && _cooldownLeft <= 0) StartCoroutine(DashPerform());
        }

        private IEnumerator DashPerform()
        {
            source.PlayOneShot(clips[Random.Range(0, clips.Length)]);
            IsDashing = true;
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.AddForce(_moveInput * dashForce, ForceMode2D.Impulse);
            dashPerformed.Invoke();
            yield return new WaitForSeconds(dashTime);
            _cooldownLeft = dashCooldown;
            dashEnded.Invoke();
            IsDashing = false;
        }
    }
}