using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script loads a list of all available guns in the resource folder
/// And populates dropdown lists in the equip menu so they can be equpped to the player's hands later
/// </summary>
public class LoadGuns : MonoBehaviour
{

    public Dropdown rightHandList;
    public GameObject rightHand;
    Fire1ShootScript rightHandShootScript;
    public Dropdown leftHandList;
    public GameObject leftHand;
    Fire2ShootScript leftHandShootScript;
    GameObject[] gunList;
    List<string> gunNames;

    // Start is called before the first frame update
    void Start()
    {
        //TODO: Come up with better method to load the hands / gun controlls into memory to make it easier, maybe pulling from player somehow?
        
        
        //get initial references to player's hands / shoot scripts
        rightHandShootScript = rightHand.GetComponent<Fire1ShootScript>();
        leftHandShootScript = leftHand.GetComponent<Fire2ShootScript>();


        rightHandList.ClearOptions();
        leftHandList.ClearOptions();

        //gunList = Resources.LoadAll("Guns").Cast<GameObject>().ToArray();
        gunList = Resources.LoadAll<GameObject>("Guns");
        //TODO: refactor this so it only loads from specific folder

        //load list of gun names
        gunNames = new List<string>();
        foreach (GameObject g in gunList)
        {
            gunNames.Add(g.name);
        }
        //populate the drop downs from the list
        leftHandList.AddOptions(gunNames);
        rightHandList.AddOptions(gunNames);
    }

    /// <summary>
    /// update the player's equipment
    /// </summary>
    public void SetEquipment()
    {
        //clear gun lists (empty hands)
        leftHandShootScript.ClearGunList();
        rightHandShootScript.ClearGunList();

        //instantiate new guns into correct hands
        Instantiate(gunList[rightHandList.value], rightHand.transform);
        Instantiate(gunList[leftHandList.value], leftHand.transform);

        //delay the update a moment to ensure Unity has time to destroy the previous guns
        leftHandShootScript.Invoke("UpdateGuns", .05f);
        rightHandShootScript.Invoke("UpdateGuns", .05f);
    }
}
