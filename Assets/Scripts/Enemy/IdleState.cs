using UnityEngine;

namespace Enemy
{
    public class IdleState : IState
    {
        private readonly GenericMeleeEnemy _context;
        
        public IdleState(GenericMeleeEnemy context)
        {
            _context = context;
        }
        
        public void Enter()
        {
            MonoBehaviour.print("IdleEnter");
            _context.animator.SetTrigger("Idle");
        }

        public void Stay()
        {
            
            Vector2 pos = _context.gameObject.transform.position;
            Vector2 pPos = _context.Player.transform.position;
            var hit = Physics2D.Raycast(pos, pPos - pos, _context.invokeDistance);
            if (!hit || !hit.transform.CompareTag("Player")) return;
            _context.ChangeState(new RunState(_context));
        }
    }
}