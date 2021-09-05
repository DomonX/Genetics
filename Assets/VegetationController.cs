using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class VegetationController : MonoBehaviour
{
    public float GrowthSpeed = 1.0f;
    public float DecaySpeed = 1.0f;
    public float ReproductionAge = 0.7f;
    public float ReproductionSpeed = 1.0f;
    public float MutationChance = 0.01f;

    public float Lifetime = 1.0f;

    public bool IsDecaying = false;
    public float CurrentSize { get; private set; } = 0.0f;

    public VegetationGenotype Genotype;

    public float ReproductionCounter = 0.0f;

    private float CurrentLifetime = 0.0f;

    private EnvironmentController environment;

    void Start()
    {
        ReproductionCounter = 0.0f;
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 10.0f, 1 << 8);
        if(!hit.collider)
        {
            Destroy(this.gameObject);
            return;
        }
        environment = hit.collider.gameObject.GetComponent<EnvironmentController>();
        if(!environment.RegisterVegetation(this.gameObject))
        {
           Destroy(this.gameObject);
           return;
        }
       
        SetSize();
    }

    void Update()
    {
        CurrentLifetime += Time.deltaTime;
        if(IsDecaying) {
            Decay();
        } else {
            Grow();
        }
        CheckDecay();
        Reproduce();
        SetSize();
    }

    private void Reproduce()
    {
        if(!CanReproduce())
        {
            return;
        }
        ReproductionCounter += Time.deltaTime;

        if(IsReproductionReady())
        {
            CreateChild();
            DecreaseReproductionCounter();
        }
    }

    private void CreateChild()
    {
        Vector3 shift = new Vector3(UnityEngine.Random.Range(-2.0f, 2.0f), 0.0f, UnityEngine.Random.Range(-2.0f, 2.0f));
        GameObject child = Genotype.CreateChild(MutationChance);
        child.transform.localPosition = child.transform.localPosition + shift;
    }

    private bool IsReproductionReady()
    {
        return ReproductionCounter >= ReproductionSpeed;
    }

    private void DecreaseReproductionCounter()
    {
        ReproductionCounter -= ReproductionSpeed;
    }

    private bool CanReproduce()
    {
        return !IsDecaying && CurrentSize > ReproductionAge;
    }

    private float GetGrowth()
    {
        return GrowthSpeed * Time.deltaTime / Lifetime;
    }

    private float GetDecay()
    {
        return DecaySpeed * Time.deltaTime / Lifetime;
    }

    private void Grow()
    {
        CurrentSize += GetGrowth();
    }

    private void Decay()
    {
        CurrentSize -= GetDecay();
        if(IsDead())
        {
            
            environment.UnregisterVegetation(this.gameObject);
            Destroy(this.gameObject);
        }
    }

    private void CheckDecay()
    {
        if(IsDecaying)
        {
            return;
        }
        if(CurrentLifetime >= Lifetime)
        {
            IsDecaying = true;
        }
    }

    private bool IsDead()
    {
        return CurrentSize <= 0.0f;
    }

    private void SetSize()
    {
        transform.localScale = new Vector3(CurrentSize, CurrentSize, CurrentSize);
        transform.localPosition = new Vector3(transform.localPosition.x, CurrentSize / 2, transform.localPosition.z);
    }
}
