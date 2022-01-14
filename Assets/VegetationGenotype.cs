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

    public void ResetColor()
    {
        Renderer r = GetComponent<Renderer>();
        color = new Color(xAllele, yAllele, zAllele);
        r.material.SetColor("_Color", this.color);
    }

    public GameObject CreateChild(VegetationGenotype parent)
    {
        GameObject child = GameObject.Instantiate(template);
        VegetationGenotype childGenotype = child.GetComponent<VegetationGenotype>();
        childGenotype.xAllele = Mutate((xAllele + parent.xAllele) / 2);
        childGenotype.yAllele = Mutate((yAllele + parent.yAllele) / 2);
        childGenotype.zAllele = Mutate((zAllele + parent.zAllele) / 2);
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

    private float Mutate(float gene)
    {
        if(Random.Range(0.0f, 1.0f) >= Simc.MutationChance) {
            return gene;
        }
        return Simc.MutationMode == 1 ? (Random.Range(0.0f, 1.0f) + gene) / 2 : Mathf.Clamp01(Random.Range(-0.1f, 0.1f) + gene);
    }
}
