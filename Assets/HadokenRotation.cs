using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HadokenRotation : MonoBehaviour
{
    public float rotationSpeed;
    // Update is called once per frame
    void Update()
    {
        transform.rotation *= Quaternion.Euler(Vector3.up * Time.deltaTime * rotationSpeed);
    }
}
