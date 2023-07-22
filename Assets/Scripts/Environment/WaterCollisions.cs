using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Player;
using UnityEngine;
using Zenject;

namespace Environment
{
    public class WaterCollisions : MonoBehaviour
    {
        private Collider2D _collider;
        private Collider2D _playerCollider;
        private PlayerDash _dash;
        private bool _isInTrigger;
        public static bool PlayerDamage;

        [Inject]
        private void Construct(PlayerComposer player)
        {
            _dash = player.dash;
            _playerCollider = player.GetComponent<Collider2D>();
        }

        private void Start()
        {
            _collider = GetComponent<Collider2D>();

            _dash.dashPerformed.AddListener(Disable);
            _dash.dashEnded.AddListener(Enable);
        }

        private void Disable()
        {
            _collider.isTrigger = true;
            _collider.bounds.Expand(-0.5f);
        }

        private void Enable()
        {
            if (_isInTrigger) PlayerDamage = true;
            _collider.isTrigger = false;
            _collider.bounds.Expand(0.5f);
        }

        private void Update()
        {
            _isInTrigger = _collider.OverlapPoint(_playerCollider.bounds.center);
        }
    }
}
