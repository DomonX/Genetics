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
        public static int VegetationsPerEnvironment = 25;
        public static float PartnerRange = 5.0f;
        public static int MutationMode = 0;
        public static int FitnessFunctionMode = 0;

        public static int VegetationIndex = 0;

        public static float DeltaTime { get {
            return SimSpeed * Time.deltaTime;
        } private set { } }
        public static int GetNewIndex()
        {
            VegetationIndex++;
            return VegetationIndex - 1;

        }
    }
}
