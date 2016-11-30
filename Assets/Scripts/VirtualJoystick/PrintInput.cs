using UnityEngine;

/// <summary>
/// Prints the Inputs from a PlayerJoysticks class on the screen.
/// </summary>
public class PrintInput : MonoBehaviour
{
    #region Methods
    private string CurrentState(CombinedButton button)
    {
        if (button.IsPressed)
            return "Pressed";
        if (button.IsReleased)
            return "Released";
        if (button.IsHeld)
            return "Held";
        return "Waiting";
    }
    #endregion

    #region MonoBehaviors
    protected virtual void Awake()
    {
        Debug.Log("Controller 0: Keyboard");
        for (int i = 0; i < Input.GetJoystickNames().Length; i++)
        {
            if (Input.GetJoystickNames()[i] != "" || Input.GetJoystickNames()[i] != null)
                Debug.Log("Controller " + (i + 1) + ": \"" + Input.GetJoystickNames()[i] + "\"");
        }
    }

    protected virtual void Update()
    {
        gameObject.transform.Translate(new Vector3(P1Joystick.LeftStick.Input.x, 0, P1Joystick.LeftStick.Input.y));
        gameObject.transform.Rotate(new Vector3(-P1Joystick.RightStick.Input.y, P1Joystick.RightStick.Input.x, 0));
    }

    protected virtual void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 500, 500), "Left Stick: " + P1Joystick.LeftStick.Input);
        GUI.Label(new Rect(0, 25, 500, 500), "Right Stick: " + P1Joystick.RightStick.Input);
        GUI.Label(new Rect(0, 50, 500, 500), "D-Pad: " + P1Joystick.DPad.Input);
        GUI.Label(new Rect(0, 75, 500, 500), "Left Trigger: " + P1Joystick.LeftTrigger.Input);
        GUI.Label(new Rect(0, 100, 500, 500), "Right Trigger: " + P1Joystick.RightTrigger.Input);
        GUI.Label(new Rect(0, 125, 500, 500), "South Button: " + CurrentState(P1Joystick.SouthButton));
        GUI.Label(new Rect(0, 150, 500, 500), "East Button: " + CurrentState(P1Joystick.EastButton));
        GUI.Label(new Rect(0, 175, 500, 500), "West Button: " + CurrentState(P1Joystick.WestButton));
        GUI.Label(new Rect(0, 200, 500, 500), "North Button: " + CurrentState(P1Joystick.NorthButton));
        GUI.Label(new Rect(0, 225, 500, 500), "Left Bumper: " + CurrentState(P1Joystick.LeftBumper));
        GUI.Label(new Rect(0, 250, 500, 500), "Right Bumper: " + CurrentState(P1Joystick.RightBumper));
        GUI.Label(new Rect(0, 275, 500, 500), "Left Stick Press: " + CurrentState(P1Joystick.LeftStickPress));
        GUI.Label(new Rect(0, 300, 500, 500), "Right Stick Press: " + CurrentState(P1Joystick.RightStickPress));
        GUI.Label(new Rect(0, 325, 500, 500), "Start Button: " + CurrentState(P1Joystick.StartButton));
        GUI.Label(new Rect(0, 350, 500, 500), "Select Button: " + CurrentState(P1Joystick.SelectButton));
        GUI.Label(new Rect(0, 375, 500, 500), "Home Button: " + CurrentState(P1Joystick.HomeButton));
    }
    #endregion
}