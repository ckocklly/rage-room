using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustDirection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 eulerAngles = transform.eulerAngles;

        eulerAngles.x = Mathf.Round(eulerAngles.x / 45f) * 45f;
        eulerAngles.y = Mathf.Round(eulerAngles.y / 45f) * 45f;
        eulerAngles.z = Mathf.Round(eulerAngles.z / 45f) * 45f;

        transform.eulerAngles = eulerAngles;
    }
}
