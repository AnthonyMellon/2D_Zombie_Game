using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public intSO currentWave;
    public GameObject zombieParent;
    public GameObject zombie;
    public TMP_Text IntermissionText;
    const float WaveIntermissionTime = 5; //Measured in seconds
    private bool NewWaveQueued = false;

    private void Start()
    {
        currentWave.value = 0;
    }

    private void Update()
    {
        if (zombieParent.transform.childCount <= 0 && !NewWaveQueued) //End of wave
        {            
            EndOfWave();
        }
    }

    public void EndOfWave()
    {
        Debug.Log("End of wave");
        NewWaveQueued = true;
        StartCoroutine(WaveIntermission());
    }

    public void StartOfWave()
    {
        Debug.Log("Start of wave");

        currentWave.value++;
        Debug.Log($"Current Wave: {currentWave}");

        //Spawn new wave
        for (int i = 0; i < currentWave.value; i++)
        {
            Instantiate(zombie, new Vector3(Random.Range(-9, 9), Random.Range(-4, 4), 0), new Quaternion(0, 0, 0, 0), zombieParent.transform);
        }

        NewWaveQueued = false;
    }

    private IEnumerator WaveIntermission()
    {
        float elapsedTime = 0;
        while(elapsedTime < WaveIntermissionTime)
        {
            elapsedTime += Time.deltaTime;
            IntermissionText.text = (WaveIntermissionTime - elapsedTime).ToString("F1");
            yield return new WaitForEndOfFrame();
        }
        IntermissionText.text = "";
        StartOfWave();
    }
}
