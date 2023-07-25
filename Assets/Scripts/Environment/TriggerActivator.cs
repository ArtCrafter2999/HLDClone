using System;
using Player;
using UnityEngine;
using Zenject;

namespace Environment
{
    public class TriggerActivator : MonoBehaviour
    {
        [SerializeField] private StateChangingGroup group;
        private bool _isActivated;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if(_isActivated || !col.TryGetComponent(out PlayerComposer _)) return;
            _isActivated = true;
            group.ChangeState();
        }
    }
}