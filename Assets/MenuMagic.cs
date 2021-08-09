using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMagic : MonoBehaviour
{
    public float range = 5f;
    public float torqueMax = 10.0f;
    public float spawnTime = 1f;
    public List<GameObject> ragdolls;

    GameObject lastSpawn;
    float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= spawnTime)
        {
            
            lastSpawn = Instantiate(ragdolls[(int)Random.Range(0, ragdolls.Count)], new Vector3(Random.Range(-range, range), transform.position.y, transform.position.z), Quaternion.identity);
            lastSpawn.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-range, range), Random.Range(-range, range), Random.Range(-range, range)));
            timer = 0;
        }
    }
}
