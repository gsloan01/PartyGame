using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifetime : MonoBehaviour
{
    public float time = 4.0f;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, time);
    }
}
