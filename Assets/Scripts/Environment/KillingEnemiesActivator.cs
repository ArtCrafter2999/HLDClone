using System;
using System.Linq;
using UnityEngine;

namespace Environment
{
    public class KillingEnemiesActivator : MonoBehaviour
    {
        [SerializeField] private StateChangingGroup group;
        [SerializeField] private Health[] enemies;
        private bool _isActivated;
        private void Update()
        {
            if (_isActivated || enemies.Any(enemy => !enemy.IsDead)) return;
            _isActivated = true;
            group.ChangeState();
        }
    }
}