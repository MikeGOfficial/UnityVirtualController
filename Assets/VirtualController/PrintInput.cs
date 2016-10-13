using UnityEngine;
using System.Collections;

/// <summary>
/// Prints the Inputs from a CombinedInput class on the screen.
/// </summary>
public class PrintInput : MonoBehaviour
{
    #region Variables
    private ControllerInput joystick;
    #endregion

    #region Getters/Setters
    public ControllerInput Joystick
    {
        get { return joystick; }
    }
    #endregion

    #region MonoBehaviors
    protected virtual void Awake()
    {
        joystick = GetComponent<ControllerInput>();
        foreach (string joyname in Input.GetJoystickNames())
            Debug.Log(joyname);
    }

    protected virtual void Update()
    {
        gameObject.transform.Translate(new Vector3(joystick.LeftStick.GetInput.x, 0, joystick.LeftStick.GetInput.y));
        gameObject.transform.Rotate(new Vector3(-joystick.RightStick.GetInput.y, joystick.RightStick.GetInput.x, 0));
    }

    protected virtual void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 500, 500), joystick.LeftStick.customName + ": " + joystick.LeftStick.GetInput.ToString());
        GUI.Label(new Rect(0, 25, 500, 500), joystick.RightStick.customName + ": " + joystick.RightStick.GetInput.ToString());
        GUI.Label(new Rect(0, 50, 500, 500), joystick.DPad.customName + ": " + joystick.DPad.GetInput.ToString());
        GUI.Label(new Rect(0, 75, 500, 500), joystick.LeftTrigger.customName + ": " + joystick.LeftTrigger.GetInput.ToString());
        GUI.Label(new Rect(0, 100, 500, 500), joystick.RightTrigger.customName + ": " + joystick.RightTrigger.GetInput.ToString());
        GUI.Label(new Rect(0, 125, 500, 500), joystick.SouthButton.customName + ": " + joystick.SouthButton.State.ToString());
        GUI.Label(new Rect(0, 150, 500, 500), joystick.EastButton.customName + ": " + joystick.EastButton.State.ToString());
        GUI.Label(new Rect(0, 175, 500, 500), joystick.WestButton.customName + ": " + joystick.WestButton.State.ToString());
        GUI.Label(new Rect(0, 200, 500, 500), joystick.NorthButton.customName + ": " + joystick.NorthButton.State.ToString());
        GUI.Label(new Rect(0, 225, 500, 500), joystick.LeftBumper.customName + ": " + joystick.LeftBumper.State.ToString());
        GUI.Label(new Rect(0, 250, 500, 500), joystick.RightBumper.customName + ": " + joystick.RightBumper.State.ToString());
        GUI.Label(new Rect(0, 275, 500, 500), joystick.LeftStickPress.customName + ": " + joystick.LeftStickPress.State.ToString());
        GUI.Label(new Rect(0, 300, 500, 500), joystick.RightStickPress.customName + ": " + joystick.RightStickPress.State.ToString());
        GUI.Label(new Rect(0, 325, 500, 500), joystick.StartButton.customName + ": " + joystick.StartButton.State.ToString());
        GUI.Label(new Rect(0, 350, 500, 500), joystick.SelectButton.customName + ": " + joystick.SelectButton.State.ToString());
        GUI.Label(new Rect(0, 375, 500, 500), joystick.HomeButton.customName + ": " + joystick.HomeButton.State.ToString());
    }
    #endregion
}