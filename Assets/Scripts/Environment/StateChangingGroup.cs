using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment
{
    public class StateChangingGroup : MonoBehaviour
    {
        private IStateChanging[] _walls;

        private void Start()
        {
            _walls = GetComponentsInChildren<IStateChanging>();
        }

        public void ChangeState()
        {
            foreach (var wall in _walls)
            {
                wall.ChangeState();
            }
        }
    }
}

