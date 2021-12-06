using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Sim;

public class EnvironmentController : MonoBehaviour
{
    Renderer r;
    public Color currentColor;
    public int MaxVegs = 10;
    private List<GameObject> vegetation = new List<GameObject>();
    
    void Start()
    {
        r = GetComponent<Renderer>();
        currentColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        SetColor();
    }

    public void RegisterVegetation(GameObject go)
    {
        vegetation.Add(go);
    }

    public void UnregisterVegetation(GameObject go)
    {
        vegetation.Remove(go);
    }

    public float CurrentFood()
    {
        if(vegetation.Count == 0)
        {
            return MaxVegs;
        }
        return MaxVegs / vegetation.Count;
    }

    public float GetAvarageCompatibility()
    {
        if(vegetation.Count == 0)
        {
            return 0.0f;
        }
        return vegetation.Select(i => i.GetComponent<VegetationController>().Compatibiliy).Aggregate(0.0f, (float acc, float curr) => acc + curr) / vegetation.Count;
    }

    public float GetAvarageRed()
    {
        return vegetation.Select(i => i.GetComponent<VegetationGenotype>().xAllele).Aggregate(0.0f, (float acc, float curr) => acc + curr) / vegetation.Count;
    }

    public float GetAvarageGreen()
    {
        return vegetation.Select(i => i.GetComponent<VegetationGenotype>().yAllele).Aggregate(0.0f, (float acc, float curr) => acc + curr) / vegetation.Count;
    }

    public float GetAvarageBlue()
    {
        return vegetation.Select(i => i.GetComponent<VegetationGenotype>().zAllele).Aggregate(0.0f, (float acc, float curr) => acc + curr) / vegetation.Count;
    }

    public float GetVolume()
    {
        return (float)vegetation.Count() / Simc.VegetationsPerEnvironment;
    }

    private void SetColor()
    {
        r.material.SetColor("_Color", currentColor);
    }
}
