using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;
using Environment;

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
        private ITakeDamage health;
        private Vector3 previousPoint;
        public bool CanDash { get; set; } = true;
        public bool IsDashing { get; private set; }
        
        public UnityEvent dashPerformed;
        public UnityEvent dashEnded;

        private void Start()
        {
            health = GetComponent<Health>();
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
            previousPoint = transform.position;
            source.PlayOneShot(clips[Random.Range(0, clips.Length)]);
            IsDashing = true;
            _rigidbody.velocity = Vector2.zero;
            dashPerformed.Invoke();
            _rigidbody.AddForce(_moveInput * dashForce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(dashTime);
            _cooldownLeft = dashCooldown;
            dashEnded.Invoke();
            IsDashing = false;
            if(WaterCollisions.PlayerDamage) DamageByWater();
        }

        private void DamageByWater()
        {
            health.TakeDamage(1);
            transform.position = previousPoint;
            WaterCollisions.PlayerDamage = false;
        }
    }
}