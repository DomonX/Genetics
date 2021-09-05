using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    Renderer r;

    public Color currentColor;

    public float changeInterval = 100.0f;

    public float currentInterval = 0.0f;

    public int vegs = 0;

    public int MaxVegs = 10;

    private List<GameObject> vegetation = new List<GameObject>();
    
    void Start()
    {
        r = GetComponent<Renderer>();
        currentColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        SetColor();
    }

    void Update()
    {
        currentInterval += Time.deltaTime;
        if(currentInterval >= changeInterval) {
            float redChange = GetColorChange();
            float blueChange= GetColorChange();
            float greenChange = GetColorChange();
            Color change = new Color(redChange, greenChange, blueChange);
            currentColor += change;
            SetColor();
            currentInterval -= changeInterval;
        }
    }

    public bool RegisterVegetation(GameObject go)
    {
        if(vegetation.Count > MaxVegs)
        {
            return false;
        }
        vegetation.Add(go);
        vegs = vegetation.Count;
        return true;
    }

    public void UnregisterVegetation(GameObject go)
    {
        vegetation.Remove(go);
        vegs = vegetation.Count;
    }

    private void SetColor()
    {
        r.material.SetColor("_Color", currentColor);
    }

    private float GetColorChange()
    {
        return Random.Range(-0.1f, 0.1f);
    } 
}
