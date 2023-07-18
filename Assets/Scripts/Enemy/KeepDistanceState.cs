using UnityEngine;

namespace Enemy
{
    public class KeepDistanceState : StateBase
    {
        [SerializeField] private float minDistance;
        [SerializeField] private float maxDistance;
        [SerializeField] private float distanceToIdle;
        [SerializeField] private float delay;
        [SerializeField] private float speed;
        [SerializeField] private float maxSpeed;
        private FourStateEnemy _context;
        private Vector2 _previousMove;
        private float _delayLeft;
        private void Awake()
        {
            _context = GetComponent<FourStateEnemy>();
        }

        public override void Enter()
        {
            _delayLeft = delay;
            _context.animator.SetTrigger("Run");
        }

        public override void Stay()
        {
            Vector2 pos = _context.gameObject.transform.position;
            Vector2 pPos = _context.Player.position;
            var distance = Vector2.Distance(pos, pPos);
            Vector2? move = null;
            if(distance < minDistance)
                move = (pos - pPos).normalized * speed;
            else if (distance > maxDistance) 
                move = (pPos - pos).normalized * speed;
            else if (distance > distanceToIdle) _context.ChangeState(_context.idleState);
            if (move.HasValue)
            {
                _previousMove = move.Value;
                Move(move.Value);
                _delayLeft = delay;
            }
            else
            {
                Move(_previousMove);
                _delayLeft -= Time.deltaTime;
                if(_delayLeft < 0)
                    _context.ChangeState(_context.preparingState);
            }
            
        }

        private void Move(Vector2 move)
        {
            if (Mathf.Abs(_context.Rigidbody.velocity.x) < maxSpeed)
            {
                _context.Rigidbody.velocity += new Vector2(move.x, 0);
            }
            if (Mathf.Abs(_context.Rigidbody.velocity.y) < maxSpeed)
            {
                _context.Rigidbody.velocity += new Vector2(0,  move.y);
            }
        }

        public override void Exit(){}
    }
}