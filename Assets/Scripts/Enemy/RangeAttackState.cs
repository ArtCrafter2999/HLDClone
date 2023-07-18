using UnityEngine;

namespace Enemy
{
    public class RangeAttackState : StateBase
    {
        [SerializeField] private GameObject projectile;
        [SerializeField] private Transform shootPointR;
        [SerializeField] private Transform shootPointL;
        [SerializeField] private AudioClip shootSound;
        private  FourStateEnemy _context;
        private void Awake()
        {
            _context = GetComponent<FourStateEnemy>();
        }

        public override void Enter()
        {
            Vector2 shootPoint = (_context.IsFlipped ? shootPointR : shootPointL).position;
            Instantiate(projectile, shootPoint, Quaternion.identity).GetComponent<Projectile>()
                .Init(((Vector2)_context.Player.position - shootPoint).normalized, false);
            _context.Play(shootSound);
            _context.ChangeState(_context.runState);
        }

        public override void Stay()
        {
            
        }

        public override void Exit(){}
    }
}