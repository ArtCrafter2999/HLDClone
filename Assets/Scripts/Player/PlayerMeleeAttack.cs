using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerMeleeAttack : MonoBehaviour
    {
        [SerializeField] private float impulseForce;
        [SerializeField] private float attackTime;
        [SerializeField] private int maxCombo = 3;
        [SerializeField] private float comboCooldown = 1;
        private bool _canAttack = true;
        private Rigidbody2D _rigidbody;
        private PlayerMovement _movement;
        private float _timeSinceAttack;
        private int _comboAttacks;

        public UnityEvent attackPerformed;
        public UnityEvent attackEnded;
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _movement = GetComponent<PlayerMovement>();
            PlayerInputs.Instance.Game.MeleeAttack.performed += _ => Attack();
        }

        private void Update()
        {
            if (!_canAttack) _timeSinceAttack = 0;
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
            if(_canAttack && (_comboAttacks < maxCombo))
                StartCoroutine(Attack(PlayerInputs.MouseDirection(transform.position)));
        }

        private IEnumerator Attack(Vector2 direction)
        {
            _canAttack = false; 
            _movement.enabled = false;
            _rigidbody.velocity = Vector2.zero;
            _comboAttacks++;
            _rigidbody.AddForce(direction * impulseForce, ForceMode2D.Impulse);
            attackPerformed.Invoke();
            yield return new WaitForSeconds(attackTime);
            attackEnded.Invoke();
            _movement.enabled = true;
            _canAttack = true;
        }
    }
}