using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabelController : MonoBehaviour
{    public void SetText(float value)
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = value.ToString("f2");
    }
}
