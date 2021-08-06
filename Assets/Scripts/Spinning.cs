using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning : MonoBehaviour
{
    
    public Transform spinTransform;
    public Transform spinObject;
    public float spinSpeed = 50.0f;
    float speedScaling = 5f;
    Vector3 newUp;

    private void Start()
    {
        newUp = new Vector3(0, spinTransform.position.y, 0);
    }
    void Update()
    {
        //only if game is running, do not do if game is over.
        spinObject.Rotate(newUp, spinSpeed * Time.deltaTime * speedScaling);
    }
}
