using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    public class LongDashAttackState : StateBase
    {
        [SerializeField] private int damage;
        [SerializeField] private float attackTime;
        [SerializeField] private float attackForce;
        [SerializeField] private float attackRadius;
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private AudioClip sound;

        private FourStateEnemy _context;
        private float _timeLeft;
        private Vector2 _moveDirection;
        private bool _dealtDamage;

        public UnityEvent hit = new();

        private void Awake()
        {
            _context = GetComponent<FourStateEnemy>();
            hit.AddListener(() =>  _context.animator.SetTrigger("Hit"));
            hit.AddListener(() => _dealtDamage = true);
        }

        public override void Enter()
        {
            _dealtDamage = false;
            _context.animator.SetTrigger("Attack");
            _timeLeft = attackTime;
            Vector2 pos = _context.gameObject.transform.position;
            Vector2 pPos = _context.Player.position;
            _moveDirection = (pPos - pos).normalized * attackForce;
            _context.Play(sound);
            _context.CanFlip = false;
        }

        public override void Stay()
        {
            _timeLeft -= Time.deltaTime;
            if (_timeLeft <= 0)
                _context.ChangeState(_context.runState);

            if(_dealtDamage)
            {
                _context.ChangeState(_context.runState);
                return;
            }
            _context.Rigidbody.velocity = _moveDirection;
            
            var other = Physics2D.OverlapCircle(transform.position, attackRadius, playerLayer);
            if (other == null || !other.TryGetComponent(out Health health)) return;
            health.TakeDamage(damage);
            hit.Invoke();
            _context.ChangeState(_context.runState);
        }

        public override void Exit()
        {
            _context.CanFlip = true;
        }
    }
}