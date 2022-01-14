using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Sim;

using System.IO;

public class SimController : MonoBehaviour
{

    public GameObject EnvironmentObject;
    public GameObject VegetationObject;

    public float saveToReportDebounceTime = 1.0f;

    private List<GameObject> EnvironmentObjects = new List<GameObject>();
    private List<GameObject> VegetationObjects = new List<GameObject>();

    private List<EnvironmentController> Controllers = new List<EnvironmentController>();

    private Vector2 worldSize;

    private bool started;

    private List<string> reports = new List<string>();

    private float reportTime = 0.0f;

    public void Update()
    {
        if(started)
        {
            reportTime += Simc.DeltaTime;
            if(reportTime > saveToReportDebounceTime)
            {
                AddToReport();
                reportTime -= saveToReportDebounceTime;
            }
            return;
        }
        if(EnvironmentObjects.Count > 0 && VegetationObjects.Count > 0)
        {
            started = true;
        }
    }

  private void AddToReport()
    {
        string report = Controllers.Select(i =>
        {
            return
                i.currentColor.r.ToString() + '.' +
                i.currentColor.g.ToString() + '.' +
                i.currentColor.b.ToString() + '.' +
                i.GetAvarageCompatibility().ToString() + '.' +
                i.GetAvarageRed().ToString() + '.' +
                i.GetAvarageGreen().ToString() + '.' +
                i.GetAvarageBlue().ToString() + '.' +
                i.GetVolume().ToString() + ';';
        }).Aggregate("", (string acc, string curr) => acc + curr) + 
            Simc.CompatibilityPower.ToString() + ';' + 
            Simc.MutationChance.ToString() + ';' + 
            Simc.VegetationsPerEnvironment.ToString() + ';' + 
            Simc.MutationMode.ToString() + ';'; 
        reports.Add(report);
    }


    public void SaveReport()
    {
        File.WriteAllLines("report.csv", reports);
    }

    public void GenerateEnvironment()
    {
        started = false;
        EnvironmentObjects.ForEach(i => {
            Destroy(i);
        });
        VegetationObjects.ForEach(i => {
            Destroy(i);
        });
        float xEnvScale = EnvironmentObject.transform.localScale.x * 10.0f;
        float zEnvScale = EnvironmentObject.transform.localScale.z * 10.0f;
        for (int x = 0; x < Simc.EnvironmentSize; x++)
        {
            for (int z = 0; z < Simc.EnvironmentSize; z++)
            {
                GameObject clone = GameObject.Instantiate(EnvironmentObject);
                clone.transform.position = new Vector3(x * xEnvScale, 0.0f, z * zEnvScale);
                clone.name = "Environment" + x.ToString() + "/" + z.ToString();
                EnvironmentObjects.Add(clone);
            }
        }
        Controllers = EnvironmentObjects.Select(i => i.GetComponent<EnvironmentController>()).ToList();
        float shift = xEnvScale * -0.5f;
        worldSize = new Vector2(shift, Simc.EnvironmentSize * xEnvScale + shift);
    }

    public void GenerateVegetation()
    {
        for(int i = 0; i < Simc.Vegetations; i++)
        {
            float xPos = Random.Range(worldSize.x, worldSize.y);
            float zPos = Random.Range(worldSize.x, worldSize.y);
            GameObject clone = GameObject.Instantiate(VegetationObject, new Vector3(xPos, 0.0f, zPos), new Quaternion());
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

    
    public void SetVegetationsPerEnvironment(float size)
    {
        Simc.VegetationsPerEnvironment = (int)size;
    }

    public void SetMutationMode(int mode)
    {
        Simc.MutationMode = mode;
    }

    public void SetFitnessFunctionMode(int mode)
    {
        Simc.FitnessFunctionMode = mode;
    }

    public void SetPartnerRange(float value)
    {
        Simc.PartnerRange = value;
    }

}
