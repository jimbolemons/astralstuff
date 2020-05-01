using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    public GameObject wall1;
    public GameObject wall2;
    public GameObject wall3;
    public GameObject wall4;


    Vector3 wall1Pos;
    Vector3 wall1Scale;
    Vector3 wall2Pos;
    Vector3 wall2Scale;
    Vector3 wall3Pos;
    Vector3 wall3Scale;
    Vector3 wall4Pos;
    Vector3 wall4Scale;

    public Vector3 wall1TargetPos;
    public Vector3 wall1TargetScale;
    public Vector3 wall2TargetPos;
    public Vector3 wall2TargetScale;
    public Vector3 wall3TargetPos;
    public Vector3 wall3TargetScale;
    public Vector3 wall4TargetPos;
    public Vector3 wall4TargetScale;

    public float timeToSlerp;
    public float timeToSlerpBase = 1;

    public GameObject starterGate;

    public GameObject starterSite;

    public List<GameObject> otherGates = new List<GameObject>();


    public bool slerping = false;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("pauseGates", 1);

        timeToSlerp = timeToSlerpBase;
        //Wall1 ----------------
        wall1Pos = wall1.transform.localPosition;
        wall1Scale = wall1.transform.localScale;

        wall1.transform.localPosition = wall1TargetPos;
        wall1.transform.localScale = wall1TargetScale;


        //Wall2 ----------------
        wall2Pos = wall2.transform.localPosition;
        wall2Scale = wall2.transform.localScale;

        wall2.transform.localPosition = wall2TargetPos;
        wall2.transform.localScale = wall2TargetScale;

        //Wall3 ----------------
        wall3Pos = wall3.transform.localPosition;
        wall3Scale = wall3.transform.localScale;

        wall3.transform.localPosition = wall3TargetPos;
        wall3.transform.localScale = wall3TargetScale;

        //Wall4 ----------------
        wall4Pos = wall4.transform.localPosition;
        wall4Scale = wall4.transform.localScale;

        wall4.transform.localPosition = wall4TargetPos;
        wall4.transform.localScale = wall4TargetScale;
        
    }

    void pauseGates()
    {
        foreach (GameObject g in otherGates)
        {
            g.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (slerping && timeToSlerp > -1)
        {
            if (!MasterStaticScript.gameIsPaused)
            {
                float slerpNumber = timeToSlerpBase - timeToSlerp;
                wall1.transform.localPosition = Vector3.Slerp(wall1TargetPos, wall1Pos, slerpNumber);
                wall1.transform.localScale = Vector3.Slerp( wall1TargetScale, wall1Scale, slerpNumber);

                wall2.transform.localPosition = Vector3.Slerp(wall2TargetPos, wall2Pos, slerpNumber);
                wall2.transform.localScale = Vector3.Slerp(wall2TargetScale, wall2Scale, slerpNumber);

                wall3.transform.localPosition = Vector3.Slerp(wall3TargetPos, wall3Pos, slerpNumber);
                wall3.transform.localScale = Vector3.Slerp(wall3TargetScale, wall3Scale, slerpNumber);

                wall4.transform.localPosition = Vector3.Slerp(wall4TargetPos, wall4Pos, slerpNumber);
                wall4.transform.localScale = Vector3.Slerp(wall4TargetScale, wall4Scale, slerpNumber);

                timeToSlerp -= Time.deltaTime;
            }
        }
        if(slerping && timeToSlerp < -1)
        {
            foreach (GameObject g in otherGates)
            {
                g.SetActive(true);
            }
            //Destroy(gameObject);
        }

        if(starterGate == null)
        {
            releaseTheWalls();
        }
        if(slerping == false){
            if(starterSite == null)
            {
             MasterStaticScript.Lose();

            }
        }
    }
    public void releaseTheWalls()
    {
        slerping = true;
    }
}
