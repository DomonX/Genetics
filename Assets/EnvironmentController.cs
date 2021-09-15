using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void SetColor()
    {
        r.material.SetColor("_Color", currentColor);
    }
}
