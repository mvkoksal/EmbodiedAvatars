using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    private string trialIndex, trialName;

    private CSVLogger CoinLogger;
    private CSVLoggerGeneral GeneralLogger;
    private ExperimentLogger experimentLogger;

    // Start is called before the first frame update
    void Start()
    {
        CoinLogger = GameObject.Find("CSVLoggerCoin").GetComponent<CSVLoggerDS>();
        GeneralLogger = GameObject.Find("CSVLoggerGeneral").GetComponent<CSVLoggerGeneral>();
        experimentLogger = GameObject.Find("Experiment Logger").GetComponent<ExperimentLogger>();
    }

    // Destroy and log coin when it collides with the player.
    private void OnTriggerEnter(Collider other)
    {
        // Get the trial index and name from Experiment Logger
        if (experimentLogger.currentTrial != null)
        {
            trialIndex = experimentLogger.trialCounter.ToString();
            trialName = experimentLogger.currentTrial.gameObject.name;
        }

        LogCoin(gameObject.name);

        // Pickup coin
        Destroy(gameObject);  
    }

    private void LogCoin(string name)
    {
        CoinLogger.UpdateField("trial_index", trialIndex);
        CoinLogger.UpdateField("trial_name", trialName);
        CoinLogger.UpdateField("coin_name", name);
        CoinLogger.UpdateField("time", GeneralLogger.getTime());
        CoinLogger.Queue();
    }
}
