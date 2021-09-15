using UnityEngine;

namespace Sim
{
    class Simc
    {
        public static float SimSpeed = 0.01f;
        public static float CompatibilityPower = 3.0f;
        public static float MutationChance = 0.001f;

        public static float DeltaTime { get {
            return SimSpeed * Time.deltaTime;
        }
        private set { }
        }
    }
}
