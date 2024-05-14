using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarManager : MonoBehaviour
{
    public Slider fuelSlider; // Reference to the fuel slider
    public JetpackController jetpackController; // Reference to the JetpackController script

    void Awake()
    {
        // Set the min and max values of the fuel slider
        fuelSlider.minValue = 0f;
        fuelSlider.maxValue = 1f;
    }
    // Start is called before the first frame update
    void Start()
    {
        // Initialize the fuel slider value
        fuelSlider.value = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        // Update the fuel slider value based on the jetpack fuel level
        fuelSlider.value = jetpackController.GetFuelLevel();
    }
}
