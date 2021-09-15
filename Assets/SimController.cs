using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sim;

public class SimController : MonoBehaviour
{

    public GameObject EnvironmentObject;
    public GameObject VegetationObject;

    public int xSize = 1;
    public int zSize = 1;
    public int Vegetations = 1;

    private List<GameObject> EnvironmentObjects = new List<GameObject>();
    private List<GameObject> VegetationObjects = new List<GameObject>();

    private float xWorldSize;
    private float zWorldSize;

    public void GenerateEnvironment()
    {
        float xEnvScale = EnvironmentObject.transform.localScale.x * 10.0f;
        float zEnvScale = EnvironmentObject.transform.localScale.z * 10.0f;
        for (int x = 0; x < xSize; x++)
        {
            for (int z = 0; z < zSize; z++)
            {
                GameObject clone = GameObject.Instantiate(EnvironmentObject);
                clone.transform.position = new Vector3(x * xEnvScale, 0.0f, z * zEnvScale);
                EnvironmentObjects.Add(clone);
            }
        }

        xWorldSize = (xSize - 0.5f) * xEnvScale;
        zWorldSize = (zSize - 0.5f) * zEnvScale;

        
    }

    public void GenerateVegetation()
    {
        for(int i = 0; i < Vegetations; i++)
        {
            Debug.Log("veg");
            float xPos = Random.Range(0.0f, xWorldSize);
            float zPos = Random.Range(0.0f, zWorldSize);
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

}
