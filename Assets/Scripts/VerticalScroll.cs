using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScroll : MonoBehaviour
{
    // Configuration Parameters
    [Tooltip ("Game units per second")]
    [SerializeField] float scrollRate = 0.001f;

    void Update()
    {
        transform.Translate(new Vector2(0f, scrollRate * Time.deltaTime));
    }
}
