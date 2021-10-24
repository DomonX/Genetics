using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sim;

using System;

public class VegetationController : MonoBehaviour
{
    public float CurrentSize { get; private set; } = 0.0f;
    public Color EnvironmentColor;
    public VegetationGenotype Genotype;
    public float Compatibiliy;
    public float MaxSize = 1.0f;
    public float FoodStorage = 0.0f;
    public float Lifespan = 0.0f;
    public float MaxLifespan = 4.0f;
    public float Strength = 0.0f;
    public bool IsGrown = false;

    private EnvironmentController Environment;
    void Start()
    {
        IsGrown = false;
        FoodStorage = 0.0f;
        CurrentSize = 0.1f;
        Lifespan = 0.0f;
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 10.0f, 1 << 8);
        if(!hit.collider)
        {
            Destroy(this.gameObject);
            return;
        }
        Environment = hit.collider.gameObject.GetComponent<EnvironmentController>();
        Environment.RegisterVegetation(gameObject);
        EnvironmentColor = Environment.currentColor;       
        SetSize();
        GetColorCompatibility();
        gameObject.name = "Vegetation " + Simc.VegetationIndex;
        Simc.VegetationIndex++;
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
        Collider[] c = Physics.OverlapSphere(this.transform.position, 5.0f, 1 << 9);
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
        float compatibility = Mathf.Pow(Compatibiliy, Simc.CompatibilityPower);
        float strength = GetStrength();
        return (food * compatibility * strength - 1.0f) * Simc.DeltaTime;
    }

    private float GetStrength()
    {
        Strength = (MaxLifespan / (MaxLifespan + Lifespan)) * 3;
        return Strength;
    }

    private void GetColorCompatibility()
    {
        float rDiff = 1.0f - (float)Math.Abs(Genotype.color.r - Environment.currentColor.r);
        float gDiff = 1.0f - (float)Math.Abs(Genotype.color.g - Environment.currentColor.g);
        float bDiff = 1.0f - (float)Math.Abs(Genotype.color.b - Environment.currentColor.b);

        Compatibiliy = rDiff * gDiff * bDiff;
    }
}
