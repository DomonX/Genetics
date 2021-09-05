using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetationGenotype : MonoBehaviour
{

    public float xAllele;
    public float yAllele;
    public float zAllele;

    public GameObject template;

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
        r.material.SetColor("_Color", new Color(xAllele, yAllele, zAllele));
    }

    public GameObject CreateChild(float mutationChance)
    {
        GameObject child = GameObject.Instantiate(template);
        VegetationGenotype childGenotype = GetComponent<VegetationGenotype>();
        childGenotype.xAllele = Mutate(xAllele, mutationChance);
        childGenotype.yAllele = Mutate(yAllele, mutationChance);
        childGenotype.zAllele = Mutate(zAllele, mutationChance);
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
        if(Random.Range(0.0f, 1.0f) < chance)
        {
            return Random.Range(-0.01f, 0.01f) + gene;
        }
        return gene;
    }
}
