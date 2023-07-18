using UnityEngine;

namespace Enemy
{
    public abstract class StateBase : MonoBehaviour
    {
        public abstract void Enter();
        public abstract void Stay();
        public abstract void Exit();
    }
}