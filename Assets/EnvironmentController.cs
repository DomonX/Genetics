using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Sim;

public class EnvironmentController : MonoBehaviour
{
    Renderer r;
    public Color currentColor;
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
        return Simc.VegetationsPerEnvironment / (vegetation.Count == 0 ? 1 : vegetation.Count);
    }

    public float GetAvarageCompatibility()
    {
        return vegetation.Count == 0 ? 0.0f : vegetation
            .Select(i => i.GetComponent<VegetationController>().Compatibility)
            .Aggregate(0.0f, (float acc, float curr) => acc + curr) / vegetation.Count;
    }

    public float GetAvarageRed()
    {
        return vegetation
            .Select(i => i.GetComponent<VegetationGenotype>().xAllele)
            .Aggregate(0.0f, (float acc, float curr) => acc + curr) / vegetation.Count;
    }

    public float GetAvarageGreen()
    {
        return vegetation
            .Select(i => i.GetComponent<VegetationGenotype>().yAllele)
            .Aggregate(0.0f, (float acc, float curr) => acc + curr) / vegetation.Count;
    }

    public float GetAvarageBlue()
    {
        return vegetation
            .Select(i => i.GetComponent<VegetationGenotype>().zAllele)
            .Aggregate(0.0f, (float acc, float curr) => acc + curr) / vegetation.Count;
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
