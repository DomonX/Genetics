using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LabelController : MonoBehaviour
{
    public bool isInteger = false;
    public void SetText(float value)
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = isInteger ? Math.Round(value).ToString() : value.ToString("f3");
    }

}
