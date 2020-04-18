using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Made by: Tyler J. Sims
// Made on: 12/17/2019
// Made for: DodgeBlock (v3)

public class Cube_Button : MonoBehaviour
{
    public Light _light;
    public float lightSpeed; // haha is funny cause it much much slower than light speed.
    public Vector2 lightLimits;
    public bool lightOn;
    public bool stickyOn = false;
    void Start()
    {
        
    }

    void Update()
    {
        CheckLight();

    }
    void CheckLight() {
        if (lightOn)
        {
            _light.intensity = Mathf.Lerp(_light.intensity, lightLimits.y, Time.deltaTime * lightSpeed);
        }
        else if(!lightOn)
            _light.intensity = Mathf.Lerp(_light.intensity, lightLimits.x, Time.deltaTime * lightSpeed);

    }

    public void PressButton()
    {
        GetComponent<Button>().onClick.Invoke();
    }

    private void OnMouseOver()
    {
        lightOn = true;
    }
    private void OnMouseExit()
    {
        if (!stickyOn)
            lightOn = false;
    }

    private void OnMouseDown()
    {
        PressButton();
    }

}
