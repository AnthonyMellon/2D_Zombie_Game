using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private int currentWave = 0;
    public GameObject zombieParent;
    public GameObject zombie;

    private void Update()
    {
        if(zombieParent.transform.childCount <= 0) //End of wave
        {
            currentWave++;
            Debug.Log($"Current Wave: {currentWave}");

            //Spawn new wave
            for(int i = 0; i < currentWave; i++)
            {
                Instantiate(zombie, new Vector3(Random.Range(-9, 9), Random.Range(-4, 4), 0), new Quaternion(0, 0, 0, 0), zombieParent.transform);
            }
        }
    }
}
