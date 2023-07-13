using UnityEngine;

namespace Enemy
{
    public class AttackState: IState
    {
        private readonly GenericMeleeEnemy _context;
        private float _timeLeft;
        public AttackState(GenericMeleeEnemy context)
        {
            _context = context;
        }
        
        public void Enter()
        {
            MonoBehaviour.print("AttackEnter");
            _context.animator.SetTrigger("Attack");
            _timeLeft = _context.attackTime;
            Vector2 pos = _context.gameObject.transform.position;
            Vector2 pPos = _context.Player.transform.position;
            _context.Rigidbody.AddForce((pPos - pos) * _context.attackForce, ForceMode2D.Impulse);
        }

        public void Stay()
        {
            _timeLeft -= Time.deltaTime;
            if (_timeLeft <= 0)
                _context.ChangeState(new RunState(_context));
        }
    }
}