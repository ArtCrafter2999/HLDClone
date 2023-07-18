using UnityEngine;

namespace Enemy
{
    public class PreparingAttackState: StateBase
    {
        [SerializeField] private float preparingTime;
        
        private FourStateEnemy _context;
        private float _timeLeft;
        private void Awake()
        {
            _context = GetComponent<FourStateEnemy>();
        }
        
        public override void Enter()
        {
            _context.animator.SetTrigger("Preparing");
            _timeLeft = preparingTime;
            _context.Rigidbody.velocity = Vector2.zero;
        }

        public override void Stay()
        {
            _timeLeft -= Time.deltaTime;
            if (_timeLeft <= 0)
                _context.ChangeState(_context.attackState);
        }
        public override void Exit(){}
    }
}