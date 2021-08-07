using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    List<Cannon> cannons = new List<Cannon>();

    public float shotSpeed = 12.0f;
    public float startShotRate = 3.0f;
    public float endShotRate = 1.0f;
    private float currentShotRate;
    private float rateTimer = 0;

    public float totalTime = 60.0f;
    private float currentTime = 0;


    // Start is called before the first frame update
    void Start()
    {
        currentShotRate = startShotRate;

        Cannon[] allCannons = GetComponentsInChildren<Cannon>();

        foreach (Cannon cannon in allCannons)
        {
            cannons.Add(cannon);
            cannon.SetShotSpeed(shotSpeed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        rateTimer += Time.deltaTime;
        currentTime += Time.deltaTime;

        if (rateTimer >= currentShotRate)
        {
            rateTimer = 0;
            currentShotRate = Mathf.Lerp(startShotRate, endShotRate, currentTime / totalTime);

            CannonShoot();
        }
    }

    private void CannonShoot()
    {
        int cannonNum = Random.Range(0, cannons.Count);

        cannons[cannonNum].Shoot();
    }


}
