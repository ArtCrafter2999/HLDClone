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
            return inst;
        }

        public static Vector2 MousePosition() => Camera.main.ScreenToWorldPoint
            (PlayerInputs.Instance.Game.MousePoisition.ReadValue<Vector2>());
        public static Vector2 MouseDirection(Vector2 center) => (MousePosition() - center).normalized;

        public static void ClearInstance()
        {
            _instance.Disable();
            _instance = null;
        }
    }
}