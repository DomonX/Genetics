using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlideLeft : MonoBehaviour
{
    public float DesiredX;
    public float BaseX;
    public float AnimationSpeed = 1.0f;

    private Vector2 Move;
    private bool State = false;
    private bool IsAnimating = false;
    private RectTransform rect;

    public void Start()
    {
        rect = GetComponent<RectTransform>();
        BaseX = rect.anchoredPosition.x;
        Move = new Vector2(DesiredX - BaseX, 1.0f);
    }

    public void Update()
    {
        if(!IsAnimating)
        {
            return;
        }
        rect.anchoredPosition += Move * AnimationSpeed * Time.deltaTime * (State ? 1.0f : -1.0f);
        if(IsAnimationFinished())
        {
            Anchor();
            IsAnimating = false;
        }
        
    }
    public void Toggle()
    {
        IsAnimating = true;
        State = !State;
    }

    public bool IsAnimationFinished()
    {
        if(State)
        {
            return DesiredX >= rect.anchoredPosition.x;
        }
        return BaseX <= rect.anchoredPosition.x;
    }

    public void Anchor()
    {
        float x = State ? DesiredX : BaseX;
        rect.anchoredPosition = new Vector2(x, 0.0f);
    }
}
