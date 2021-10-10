using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
public class InformationPanel : MonoBehaviour
{

    public VegetationController veg;
    public EnvironmentController env;

    public GameObject envRed;
    public GameObject envGreen;
    public GameObject envBlue;
    public GameObject envComp;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ClickedMouse();
        }
    }

    public void SetVegetation(VegetationController veg)
    {
        this.veg = veg;
    }

    public void SetEnvironment(EnvironmentController env)
    {
        this.env = env;
        envRed.GetComponent<TMPro.TextMeshProUGUI>().text = "Red: " + env.currentColor.r;
        envGreen.GetComponent<TMPro.TextMeshProUGUI>().text = "Green: " + env.currentColor.g;
        envBlue.GetComponent<TMPro.TextMeshProUGUI>().text = "Blue: " + env.currentColor.b;
        envComp.GetComponent<TMPro.TextMeshProUGUI>().text = "Avarage Compatiblity: " + env.GetAvarageCompatibility();
    }


    private void ClickedMouse()
    {
        VegetationController veg = GetClickedVegetation();
        EnvironmentController env = GetClickedEnvironment();
        if (env)
        {
            SetEnvironment(env);
        }
        if (veg)
        {
            SetVegetation(veg);
        }
    }

    private VegetationController GetClickedVegetation()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 75.0f, 1 << 9);
        if (!hit.collider)
        {
            return null;
        }
        return hit.collider.gameObject.GetComponent<VegetationController>();
    }

    private EnvironmentController GetClickedEnvironment()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 75.0f, 1 << 8);
        if (!hit.collider)
        {
            return null;
        }
        return hit.collider.gameObject.GetComponent<EnvironmentController>();
    }
}
