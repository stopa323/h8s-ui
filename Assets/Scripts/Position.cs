using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var rt = GetComponent<RectTransform>();
        Debug.Log(transform.name + " " + rt.anchoredPosition);
        Debug.Log(rt.rect.width);
    }
}
