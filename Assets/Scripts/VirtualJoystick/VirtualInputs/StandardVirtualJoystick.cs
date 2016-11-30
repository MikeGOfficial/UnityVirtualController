using UnityEngine;

public class StandardVirtualJoystick
{
    #region Variables
    [Header("Identifiers")]
    [Tooltip("Is this joystick disabled? Used if attempting to create an invalid controller.")]
    public readonly bool disabled = false;
    [Tooltip("Identifies the controller assigned to the specified port number. 0 is the keyboard and mouse and anything lower disables the controller.")]
    [Range(0, 20)] public readonly sbyte controllerNumber = 0;
    [Tooltip("The prefix of the controller's input axises names.")]
    public readonly string axisPrefix;

    [Header("Bindings")]
    public VirtualStick leftStick = null, rightStick = null, dPad = null;
    public VirtualTrigger leftTrigger = null, rightTrigger = null;
    public VirtualButton northButton = null, southButton = null, westButton = null, eastButton = null, leftBumper = null, rightBumper = null, leftStickPress = null, rightStickPress = null, startButton = null, selectButton = null, homeButton = null;
    #endregion

    #region Constructors
    public StandardVirtualJoystick(sbyte controllerNumber)
    {
        if (Input.GetJoystickNames().Length < controllerNumber || controllerNumber < 0)
            disabled = true;
        else
        {
            this.controllerNumber = controllerNumber;
            if (controllerNumber == 0)
                axisPrefix = "Mouse ";
            else
                axisPrefix = "Joy " + controllerNumber + " Axis ";
            disabled = StandardVirtualJoystickSetups.SetupVirtualController(this);
        }
    }
    #endregion
}
