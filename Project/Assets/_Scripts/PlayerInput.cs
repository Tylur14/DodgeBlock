using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Made by: Tyler J. Sims
// Made on: 12/17/2019
// Made for: DodgeBlock (v3)


/// <summary>
/// This script handles the player input AND the player movement itself.
/// I know it's bad form to take and use player input in the same script but I feel it works out --
/// -- for this smaller project well enough!
/// </summary>
public class PlayerInput : MonoBehaviour
{
    public enum ControlType { control_classic, control_dynamic}

    [Header("Player Settings")] // Variables that affect the player regardless of control style
    public ControlType controlStyle;
    public Vector2 posClamp;
    public GameObject touchButtons;

    [Header("Classic Control Settings")]
    public float c_MoveSpeed;
    public Vector3[] c_MovePositions;
    [Space(10)]
    public bool usingTouchControls;
    public int c_PosIndex;

    [Header("Dynamic Control Settings")]
    public float d_MoveSpeed;
    [Space(10)]
    public float d_xInput;

    private Vector3 vel = Vector3.zero; // Used for SmoothDamp
    private void Awake()
    {
        if (SystemInfo.deviceType == DeviceType.Desktop)
            usingTouchControls = false;
        else if (SystemInfo.deviceType == DeviceType.Handheld)
            usingTouchControls = true;
        
        SwapControls(controlStyle, usingTouchControls);
    }

    void Update()
    {
        switch (controlStyle)
        {
            case ControlType.control_classic:
                InputClassic();
                break;
            case ControlType.control_dynamic:
                InputDynamic();
                break;
        }
        
    }

    public void InputClassic()
    {
        if (!usingTouchControls)
        {
            // Get input to adjust position index
            if (Input.GetButtonDown("GoRight"))
                c_PosIndex++;
            else if (Input.GetButtonDown("GoLeft"))
                c_PosIndex--;
        }
        

        // Check index to make sure it is valid
        if (c_PosIndex >= c_MovePositions.Length) // It went over -- Set it back down to the max value (c_MovePositions.Length - 1);
            c_PosIndex = c_MovePositions.Length - 1;

        else if (c_PosIndex < 0) // It went under -- Set it back up to the min value (0)
            c_PosIndex = 0;

        transform.position = Vector3.SmoothDamp(transform.position, c_MovePositions[c_PosIndex], ref vel, Time.deltaTime * c_MoveSpeed);
    }

    public void InputDynamic()
    {
        d_xInput = Input.GetAxis("Horizontal");

        var pos = transform.position;
        pos.x += d_xInput * Time.deltaTime * d_MoveSpeed;

        pos.x = Mathf.Clamp(pos.x, posClamp.x, posClamp.y);

        transform.position = pos;
    }


    public void SwapControls(ControlType switchTo, bool isTouch)
    {
        controlStyle = switchTo;
        if (isTouch)
            touchButtons.SetActive(true);
        else if (!isTouch)
            touchButtons.SetActive(false);
    }

    public void Nudge(int dir)
    {
        c_PosIndex += dir;
        print(dir);
    }
}
