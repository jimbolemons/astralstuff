using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDummyShootScript : Gun
{
    public Rigidbody dummyBody;
    public float bodySpeed;

    public bool fireAtAngle;

    public Transform cameraPivot;
    private void Start()
    {
        dummyBody = projectilePrefab.GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
       // dummyBody.useGravity = false;
        dummyBody.AddForce(Physics.gravity * (dummyBody.mass * dummyBody.mass));
    }
    public override void Fire()
    {
        if (canFire)
        {
            projectilePrefab.transform.position = gunEnd.transform.position;
            projectilePrefab.transform.rotation = cameraPivot.transform.rotation;

            if (fireAtAngle)
            {
                dummyBody.velocity = (cameraPivot.transform.forward + cameraPivot.transform.up / 4) * Time.fixedDeltaTime * bodySpeed;
            }
            else
            {
                dummyBody.velocity =  (cameraPivot.transform.forward * Time.deltaTime * bodySpeed);
            }
            dummyBody.AddTorque(50, 0, 0);

            fireBlank();          
        }
    }
    public void fireBlank()
    {
        canFire = false;
        Invoke("ResetFire", fireCooldown);
    }
}
