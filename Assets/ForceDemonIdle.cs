using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceDemonIdle : MonoBehaviour
{
    EnemyTarget stateMachine;
    // Start is called before the first frame update
    void Start()
    {
        stateMachine = GetComponent<EnemyTarget>();
    }
    

    // Update is called once per frame
    void Update()
    {
        stateMachine.player = null;
        stateMachine.currentState = EnemyTarget.EnemyState.IDLE;
    }
}
