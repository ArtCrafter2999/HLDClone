using UnityEngine;

namespace Player
{
    public static class PlayerInputs
    {
        private static PlayerIS _instance;

        public static PlayerIS Instance
        {
            get { return _instance ??= CreateInstance(); }
        }

        private static PlayerIS CreateInstance()
        {
            var inst = new PlayerIS();
            inst.Game.Enable();
            return inst;
        }

        public static Vector2 MouseDirection(Vector2 center)
        {
            var mousepos = (Vector2)Camera.main.ScreenToWorldPoint
                (PlayerInputs.Instance.Game.MousePoisition.ReadValue<Vector2>());
            return (mousepos - center).normalized;
        }
    }
}
