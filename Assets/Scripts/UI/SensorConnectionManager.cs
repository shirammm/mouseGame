using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorConnectionManager : MonoBehaviour
{
    [SerializeField]
    private GameObject notConnected, oneLine, twoLines,threeLines, fourLines;

    // Start is called before the first frame update
    void Start()
    {
        HeadsetManager.HeadsetDisconnected += OnDisconnect;
        HeadsetManager.HeadsetConnected += OnConnect;
        HeadsetManager.UpdatePoorSignalEvent += OnPoorSignal;
    }

    private void OnDisconnect()
    {
        notConnected.SetActive(true);
        oneLine.SetActive(false);
        twoLines.SetActive(false);
        threeLines.SetActive(false);
        fourLines.SetActive(false);
    }

    
    private void OnConnect()
    {
        //nevermind
    } 

    private void OnPoorSignal(int value)
    {
        if (value < 20)
        {
            if (value > 15)
            {
                notConnected.SetActive(false);
                oneLine.SetActive(true);
                twoLines.SetActive(false);
                threeLines.SetActive(false);
                fourLines.SetActive(false);
            }
            else if (value > 10)
            {
                notConnected.SetActive(false);
                oneLine.SetActive(false);
                twoLines.SetActive(true);
                threeLines.SetActive(false);
                fourLines.SetActive(false);
            }
            else if (value > 5)
            {
                notConnected.SetActive(false);
                oneLine.SetActive(false);
                twoLines.SetActive(false);
                threeLines.SetActive(true);
                fourLines.SetActive(false);
            }
            else
            {
                notConnected.SetActive(false);
                oneLine.SetActive(false);
                twoLines.SetActive(false);
                threeLines.SetActive(false);
                fourLines.SetActive(true);
            }
        }
    }
}