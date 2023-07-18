using System;
using UnityEngine;

namespace Enemy
{
    public class IdleState : StateBase
    {
        [SerializeField] private float invokeDistance;
        [SerializeField] private LayerMask rayCastLayers;
        private FourStateEnemy _context;

        private void Awake()
        {
            _context = GetComponent<FourStateEnemy>();
        }
        
        public override void Enter()
        {
            _context.animator.SetTrigger("Idle");
        }

        public override void Stay()
        {
            Vector2 pos = _context.gameObject.transform.position;
            Vector2 pPos = _context.Player.position;
            var hit = Physics2D.Raycast(pos, pPos - pos, invokeDistance, rayCastLayers);
            if (!hit || !hit.transform.CompareTag("Player")) return;
            _context.ChangeState(_context.runState);
        }
        public override void Exit(){}
    }
}