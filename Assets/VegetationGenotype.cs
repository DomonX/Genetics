using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sim;

public class VegetationGenotype : MonoBehaviour
{

    public float xAllele;
    public float yAllele;
    public float zAllele;

    public GameObject template;

    public Color color;

    void Start()
    {
        ResetColor();
    }

    void Update()
    {
        
    }

    public void ResetColor()
    {
        Renderer r = GetComponent<Renderer>();
        this.color = new Color(xAllele, yAllele, zAllele);
        r.material.SetColor("_Color", this.color);
    }

    public GameObject CreateChild(VegetationGenotype parent)
    {
        GameObject child = GameObject.Instantiate(template);
        VegetationGenotype childGenotype = child.GetComponent<VegetationGenotype>();
        childGenotype.xAllele = Mutate((xAllele + parent.xAllele) / 2, 0.001f);
        childGenotype.yAllele = Mutate((yAllele + parent.yAllele) / 2, 0.001f);
        childGenotype.zAllele = Mutate((zAllele + parent.zAllele) / 2, 0.001f);
        childGenotype.ResetColor();
        return child;
    }

    public void Randomize()
    {
        xAllele = Random.Range(0.0f, 1.0f);
        yAllele = Random.Range(0.0f, 1.0f);
        zAllele = Random.Range(0.0f, 1.0f);
        ResetColor();
    }

    private float Mutate(float gene, float chance)
    {
        if(Random.Range(0.0f, 1.0f) < Simc.MutationChance)
        {
            return Random.Range(-0.1f, 0.1f) + gene;
        }
        return gene;
    }
}
