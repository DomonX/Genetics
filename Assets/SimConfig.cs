using UnityEngine;

namespace Sim
{
    class Simc
    {
        public static float SimSpeed = 0.01f;
        public static float CompatibilityPower = 1.0f;
        public static float MutationChance = 0.001f;
        public static int EnvironmentSize = 1;
        public static int Vegetations = 1;
        public static int VegetationsPerEnvironment = 20;
        public static int VegetationIndex = 0;
        public static bool MutationMode = true;

        public static Vector2 worldX;
        public static Vector2 worldY;

        public static float DeltaTime { get {
            return SimSpeed * Time.deltaTime;
        }
        private set { }
        }
    }
}
