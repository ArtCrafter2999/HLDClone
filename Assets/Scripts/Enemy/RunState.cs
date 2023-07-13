using UnityEngine;

namespace Enemy
{
    public class RunState : IState
    {
        private readonly GenericMeleeEnemy _context;
        private float _invokeTimeLeft;
        public RunState(GenericMeleeEnemy context)
        {
            _context = context;
        }
        
        public void Enter()
        {
            MonoBehaviour.print("RunEnter");
            _context.animator.SetTrigger("Run");
            _invokeTimeLeft = _context.invokeTime;
        }

        public void Stay()
        {
            _invokeTimeLeft -= Time.deltaTime;
            if(_invokeTimeLeft <=0)
                _context.ChangeState(new IdleState(_context));
            
            Vector2 pos = _context.gameObject.transform.position;
            Vector2 pPos = _context.Player.transform.position;
            _context.Rigidbody.velocity = (pPos - pos).normalized * _context.speed;
            if ((pPos - pos).magnitude < _context.attackDistance)
                _context.ChangeState(new PreparingAttackState(_context));
        }
    }
}