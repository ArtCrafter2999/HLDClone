using UnityEngine;

namespace Environment
{
    public class ActiveSwitch : MonoBehaviour, IStateChanging
    {
        public void ChangeState()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}