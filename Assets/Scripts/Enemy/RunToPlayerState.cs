using UnityEngine;

namespace Enemy
{
    public class RunToPlayerState : StateBase
    {
        
        [SerializeField] private float invokeTime;
        [SerializeField] private float speed;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float attackDistance;
        [SerializeField] private AudioClip invokeSound;
        
        private  FourStateEnemy _context;
        private float _invokeTimeLeft;
        private void Awake()
        {
            _context = GetComponent<FourStateEnemy>();
        }
        
        public override void Enter()
        {
            _context.animator.SetTrigger("Run");
            _invokeTimeLeft = invokeTime;
            _context.Play(invokeSound);
        }

        public override void Stay()
        {
            _invokeTimeLeft -= Time.deltaTime;
            if(_invokeTimeLeft <=0)
                _context.ChangeState(_context.idleState);
            
            Vector2 pos = _context.gameObject.transform.position;
            Vector2 pPos = _context.Player.position;
            
            var move = (pPos - pos).normalized * speed;
            if (Mathf.Abs(_context.Rigidbody.velocity.x) < maxSpeed)
            {
                _context.Rigidbody.velocity += new Vector2(move.x, 0);
            }
            if (Mathf.Abs(_context.Rigidbody.velocity.y) < maxSpeed)
            {
                _context.Rigidbody.velocity += new Vector2(0,  move.y);
            }
            if ((pPos - pos).magnitude < attackDistance)
                _context.ChangeState(_context.preparingState);
        }
        public override void Exit(){}
    }
}