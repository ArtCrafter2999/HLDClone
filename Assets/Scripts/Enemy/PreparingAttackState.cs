using UnityEngine;

namespace Enemy
{
    public class PreparingAttackState: IState
    {
        private readonly GenericMeleeEnemy _context;
        private float _timeLeft;
        public PreparingAttackState(GenericMeleeEnemy context)
        {
            _context = context;
        }
        
        public void Enter()
        {
            MonoBehaviour.print("PreparingEnter");
            _context.animator.SetTrigger("Preparing");
            _timeLeft = _context.preparingTime;
            
        }

        public void Stay()
        {
            _timeLeft -= Time.deltaTime;
            if (_timeLeft <= 0)
                _context.ChangeState(new AttackState(_context));
        }
    }
}