using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Transform cannonBarrel;
    public Transform shotTransform;
    public GameObject projectile;

    float shotVelocity = 12f;


    public void Shoot()
    {
        Vector3 shotPosition = shotTransform.position;
        Vector3 shotDirection = cannonBarrel.forward;

        GameObject cannonBall = Instantiate(projectile, shotPosition, Quaternion.identity);
        Rigidbody rb = cannonBall.GetComponent<Rigidbody>();
        rb.AddForce(shotDirection * shotVelocity, ForceMode.VelocityChange);
    }

    public void SetShotSpeed(float newVelocity) {
        shotVelocity = newVelocity;
    }
}
