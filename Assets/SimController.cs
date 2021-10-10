using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sim;

public class SimController : MonoBehaviour
{

    public GameObject EnvironmentObject;
    public GameObject VegetationObject;

    private List<GameObject> EnvironmentObjects = new List<GameObject>();
    private List<GameObject> VegetationObjects = new List<GameObject>();

    private float worldSize;

    public void GenerateEnvironment()
    {
        float xEnvScale = EnvironmentObject.transform.localScale.x * 10.0f;
        float zEnvScale = EnvironmentObject.transform.localScale.z * 10.0f;
        for (int x = 0; x < Simc.EnvironmentSize; x++)
        {
            for (int z = 0; z < Simc.EnvironmentSize; z++)
            {
                GameObject clone = GameObject.Instantiate(EnvironmentObject);
                clone.transform.position = new Vector3(x * xEnvScale, 0.0f, z * zEnvScale);
                EnvironmentObjects.Add(clone);
            }
        }

        worldSize = (Simc.EnvironmentSize - 0.5f) * xEnvScale;             
    }

    public void GenerateVegetation()
    {
        for(int i = 0; i < Simc.Vegetations; i++)
        {
            float xPos = Random.Range(0.0f, worldSize);
            float zPos = Random.Range(0.0f, worldSize);
            GameObject clone = GameObject.Instantiate(VegetationObject, new Vector3(xPos, 1.0f, zPos), new Quaternion());
            VegetationGenotype gene = clone.GetComponent<VegetationGenotype>();
            gene.Randomize();
            VegetationObjects.Add(clone);
        }
    }


    public void SetSpeed(float speed)
    {
        Simc.SimSpeed = speed;
    }

    public void SetCompatibilityPower(float power)
    {
        Simc.CompatibilityPower = power;
    }

    public void SetMutationChance(float chance)
    {
        Simc.MutationChance = chance;
    }

    public void SetEnvironemtSize(float size)
    {
        Simc.EnvironmentSize = (int)size;
    }

    public void SetVegetations(float number)
    {
        Simc.Vegetations = (int)number;
    }

}
