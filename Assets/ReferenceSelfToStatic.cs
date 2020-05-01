using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceSelfToStatic : MonoBehaviour
{
    public Camera mainCam;
    // Start is called before the first frame update
    void Start()
    {        
        MasterStaticScript.mainCameraReference = mainCam;
    }
}
