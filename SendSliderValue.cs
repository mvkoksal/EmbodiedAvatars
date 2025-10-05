using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SendSliderValue : MonoBehaviour
{
    private ExperimentLogger experimentLogger;

    // Start is called before the first frame update
    void Start()
    {
        experimentLogger = GameObject.Find("Experiment Logger").GetComponent<ExperimentLogger>();
    }

    // Callback for the Next Trial button press
    public void OnButtonClick()
    {
        // Get the parent object (shared by the button and slider)
        Transform parent = transform.parent;

        // Find the child named "Slider" under that parent
        Slider slider = parent.Find("Slider").GetComponent<Slider>();

        // Get the slider value
        float value = slider.value;

        experimentLogger.NextTrial(value);
    }
}
