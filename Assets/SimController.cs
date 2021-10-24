using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Sim;

using System.IO;
using System.Threading.Tasks;

public class SimController : MonoBehaviour
{

    public GameObject EnvironmentObject;
    public GameObject VegetationObject;

    private List<GameObject> EnvironmentObjects = new List<GameObject>();
    private List<GameObject> VegetationObjects = new List<GameObject>();

    private List<EnvironmentController> Controllers = new List<EnvironmentController>();

    private float worldSize;

    private bool started;

    private List<string> reports = new List<string>();

    private float reportTime = 0.0f;

    public void Update()
    {
        if(started)
        {
            reportTime += Simc.DeltaTime;
            if(reportTime > 1.0f)
            {
                AddToReport();
                reportTime -= 1.0f;
            }
            return;
        }
        if(EnvironmentObjects.Count > 0 && VegetationObjects.Count > 0)
        {
            this.started = true;
        }
    }

    private void AddToReport()
    {
        string report = Controllers.Select(i =>
        {
            return
                i.currentColor.r.ToString() + ',' +
                i.currentColor.g.ToString() + ',' +
                i.currentColor.b.ToString() + ',' +
                i.GetAvarageCompatibility().ToString() + ',' +
                i.GetAvarageRed().ToString() + ',' +
                i.GetAvarageGreen().ToString() + ',' +
                i.GetAvarageBlue().ToString() + ';';
        }).Aggregate("", (string acc, string curr) => acc + curr);

        reports.Add(report);
    }

    public void SaveReport()
    {
        File.WriteAllLines("report.csv", reports);
    }

    public void GenerateEnvironment()
    {
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
                EnvironmentObjects.Add(clone);
            }
        }

        Controllers = EnvironmentObjects.Select(i => i.GetComponent<EnvironmentController>()).ToList();

        worldSize = (Simc.EnvironmentSize - 0.5f) * xEnvScale;

        Simc.worldX = new Vector2(0.0f, EnvironmentObject.transform.localScale.x * 10.0f * Simc.EnvironmentSize) - new Vector2(EnvironmentObject.transform.localScale.x / 2, EnvironmentObject.transform.localScale.x / 2);
        Simc.worldY = new Vector2(0.0f, EnvironmentObject.transform.localScale.y * 10.0f * Simc.EnvironmentSize) - new Vector2(EnvironmentObject.transform.localScale.x / 2, EnvironmentObject.transform.localScale.x / 2);
    }

    public void GenerateVegetation()
    {
        for(int i = 0; i < Simc.Vegetations; i++)
        {
            float xPos = Random.Range(0.0f, worldSize);
            float zPos = Random.Range(0.0f, worldSize);
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

}
