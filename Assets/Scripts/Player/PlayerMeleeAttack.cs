using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerMeleeAttack : MonoBehaviour
    {
        [SerializeField] private float attackDistanceRange;
        [SerializeField] private float attackRadiusRange;
        [SerializeField] private int attackDamage = 1;
        [SerializeField] private float knockBack = 1;
    
        [SerializeField] private float impulseForce;
        [SerializeField] private float attackTime;
        [SerializeField] private int maxCombo = 3;
        [SerializeField] private float comboCooldown = 1;

        [SerializeField] private LayerMask rayCastLayers;

        [Header("Sounds")] 
        [SerializeField] private AudioSource source;
        [SerializeField] private AudioClip clip;
        private Rigidbody2D _rigidbody;
        private float _timeSinceAttack;
        private int _comboAttacks;

        public bool CanAttack { get; set; } = true;
        public bool IsAttacking { get; private set; }
        public UnityEvent attackPerformed;
        public UnityEvent attackEnded;
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            PlayerInputs.Instance.Game.MeleeAttack.performed += _ => Attack();
        }

        private void Update()
        {
            if (IsAttacking) _timeSinceAttack = 0;
            else if(_timeSinceAttack < comboCooldown)
            {
                _timeSinceAttack += Time.deltaTime;
            }
            if(_timeSinceAttack >= comboCooldown)
            {
                _comboAttacks = 0;
            }
        }

        private void Attack()
        {
            if(!IsAttacking && CanAttack && (_comboAttacks < maxCombo))
                StartCoroutine(Attack(PlayerInputs.MouseDirection(transform.position)));
        }

        private IEnumerator Attack(Vector2 direction)
        {
            IsAttacking = true;
            _rigidbody.velocity = Vector2.zero;
            _comboAttacks++;
            _rigidbody.AddForce(direction * impulseForce, ForceMode2D.Impulse);
            var circleCenter = (Vector2)transform.position + direction * attackDistanceRange;
            var colliders = Physics2D.OverlapCircleAll(circleCenter, attackRadiusRange);
            foreach (var coll in colliders)
            {
                if(coll.CompareTag("Player")) continue;
                if(!coll.TryGetComponent(out ITakeDamage health)) continue;
                Physics2D.Raycast(
                    transform.position, 
                    coll.transform.position - transform.position,
                    attackDistanceRange + attackRadiusRange, 
                    rayCastLayers);
                if(coll.TryGetComponent(out Rigidbody2D rb)) rb.AddForce((rb.transform.position - transform.position) * knockBack , ForceMode2D.Impulse);
                health.TakeDamage(attackDamage);
            }
            source.PlayOneShot(clip);
            
            attackPerformed.Invoke();
            yield return new WaitForSeconds(attackTime);
            attackEnded.Invoke();
            IsAttacking = false;
        }
    }
}