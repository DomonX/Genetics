using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sim;

using System;

public class VegetationController : MonoBehaviour
{
    public float CurrentSize { get; private set; } = 0.0f;
    public Color EnvironmentColor {
        get { return Environment.currentColor; }
        set { }
    }
    public VegetationGenotype Genotype;
    public float Compatibility;
    public float MaxSize = 1.0f;
    public float FoodStorage = 0.0f;
    public float Lifespan = 0.0f;
    public float MaxLifespan = 4.0f;
    public float Strength { 
        get { return (MaxLifespan / (MaxLifespan + Lifespan)) * 3; } 
        set { } 
    }
    public bool IsGrown = false;
    private EnvironmentController Environment;

    void Start()
    {
        if(!ConnectToEnvironment())
        {
            return;
        }
        SetBaseValues();
        SetSize();
        if(Simc.FitnessFunctionMode == 1)
        {
            GetAlternativeCompatibility();
        } else {
            GetColorCompatibility();
        }
        gameObject.name = "Vegetation " + Simc.GetNewIndex();
    }

    void Update()
    {
        Lifespan += Simc.DeltaTime;
        float foodChange = GetCurrentFood();
        if(!IsGrown)
        {
            Grow(foodChange);
        } else
        {
            Store(foodChange);
        }
        
        SetSize();
        if (ShouldDie())
        {
            Die();
        }
        if(ShouldReproduce())
        {
            CreateChild();
        }
    }

    private void SetBaseValues()
    {
        IsGrown = false;
        FoodStorage = 0.0f;
        CurrentSize = 0.1f;
        Lifespan = 0.0f;
    }

    private bool ConnectToEnvironment()
    {
        Vector3 shiftVector = new Vector3(0.0f, 1.0f, 0.0f);
        transform.localPosition = transform.localPosition + shiftVector;
        RaycastHit hit;
        Physics.Raycast(transform.localPosition, transform.TransformDirection(Vector3.down), out hit, 10.0f, 1 << 8);
        if (!hit.collider)
        {
            Destroy(gameObject);
            return false;
        }
        Environment = hit.collider.gameObject.GetComponent<EnvironmentController>();
        Environment.RegisterVegetation(gameObject);
        transform.localPosition = transform.localPosition - shiftVector;
        return true;
    }

    private void Store(float amount)
    {
        FoodStorage += amount;
        if(FoodStorage < 0)
        {
            CurrentSize += FoodStorage;
            FoodStorage = 0.0f;
        }
    }

    private void Grow(float amount)
    {
        CurrentSize += amount;
        if (CurrentSize > MaxSize)
        {
            IsGrown = true;
            FoodStorage += CurrentSize - MaxSize;
            CurrentSize = MaxSize;
        }
    }

    private bool ShouldReproduce()
    {
        return FoodStorage >= 1.0f;
    }

    private void CreateChild()
    {        
        Collider[] c = Physics.OverlapSphere(transform.position, Simc.PartnerRange, 1 << 9);
        VegetationGenotype genes = c.Length == 0 ? Genotype : c[new System.Random().Next(c.Length - 1)].GetComponent<VegetationGenotype>();
        Vector3 shift = new Vector3(UnityEngine.Random.Range(-2.0f, 2.0f), 0.0f, UnityEngine.Random.Range(-2.0f, 2.0f));
        GameObject child = Genotype.CreateChild(genes);
        child.transform.localPosition = child.transform.localPosition + shift;
        FoodStorage -= 1.0f;
    }

    private bool ShouldDie()
    {
        return CurrentSize <= 0.0f;
    }

    private void Die()
    {       
        Environment.UnregisterVegetation(gameObject);
        Destroy(gameObject);
    }

    private void SetSize()
    {
        transform.localScale = new Vector3(CurrentSize, CurrentSize, CurrentSize);
        transform.localPosition = new Vector3(transform.localPosition.x, 0.0f, transform.localPosition.z);
    }

    private float GetCurrentFood()
    {
        float food = Environment.CurrentFood();
        float compatibility = (float)Math.Pow(Compatibility, Simc.CompatibilityPower);
        return (food * compatibility * Strength - 1.0f) * Simc.DeltaTime;
    }

    private void GetColorCompatibility()
    {
        float rDiff = 1.0f - (float)Math.Abs(Genotype.color.r - EnvironmentColor.r);
        float gDiff = 1.0f - (float)Math.Abs(Genotype.color.g - EnvironmentColor.g);
        float bDiff = 1.0f - (float)Math.Abs(Genotype.color.b - EnvironmentColor.b);

        Compatibility = rDiff * gDiff * bDiff;
    }

    private void GetAlternativeCompatibility()
    {
        double rDiff = Math.Pow(Math.Abs(Genotype.color.r - EnvironmentColor.r), 2.0);
        double gDiff = Math.Pow(Math.Abs(Genotype.color.g - EnvironmentColor.g), 2.0);
        double bDiff = Math.Pow(Math.Abs(Genotype.color.b - EnvironmentColor.b), 2.0);

        Compatibility = 1.0f - (float)Math.Sqrt((rDiff + gDiff + bDiff) / 3);
    }
}
