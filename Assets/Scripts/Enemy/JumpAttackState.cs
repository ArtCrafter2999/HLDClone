using System;
using Cinemachine;
using UnityEngine;

namespace Enemy
{
    public class JumpAttackState : StateBase
    {
        [SerializeField] private float damage;
        [SerializeField] private float attackTime;
        [SerializeField] private float attackForce;
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private AudioClip sound;

        private bool _canDamage;
        private FourStateEnemy _context;
        private float _timeLeft;

        private void Awake()
        {
            _context = GetComponent<FourStateEnemy>();
        }

        public override void Enter()
        {
            _canDamage = true;
            _context.animator.SetTrigger("Attack");
            _timeLeft = attackTime;
            Vector2 pos = _context.gameObject.transform.position;
            Vector2 pPos = _context.Player.position;
            _context.Rigidbody.AddForce((pPos - pos) * attackForce, ForceMode2D.Impulse);
            _context.Play(sound);
        }

        public override void Stay()
        {
            _timeLeft -= Time.deltaTime;
            if (_timeLeft <= 0)
                _context.ChangeState(_context.runState);

            if (!_canDamage) return;
            var other = Physics2D.OverlapCircle(transform.position, 1, playerLayer);
            _canDamage = false;
            if (other == null || !other.TryGetComponent(out Health health)) return;
            health.TakeDamage(damage);
        }

        public override void Exit()
        {
            _canDamage = false;
        }
    }
}