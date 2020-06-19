using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScroll : MonoBehaviour
{
    // Configuration Parameters
    [Tooltip ("Game units per second")]
    [SerializeField] float scrollRate = 0.001f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, scrollRate * Time.deltaTime, 0));
    }
}
