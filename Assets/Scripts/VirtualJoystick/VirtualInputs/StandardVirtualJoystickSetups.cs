using UnityEngine;

/// <summary>
/// Contains the various setup functions for StandardVirtualJoystick classes.
/// </summary>
public static class StandardVirtualJoystickSetups
{
    #region Functions
    /// <summary>
    /// Finds the appropriate setup function and uses it to construct the controller. Also returns a boolean value for determining if the controller should be enabled or not.
    /// </summary>
    /// <param name="joystick"></param>
    public static bool SetupVirtualController(StandardVirtualJoystick joystick)
    {
        if (joystick.controllerNumber == 0)
        {
            SetupKeyboard(joystick);
            return true;
        }
        switch (Input.GetJoystickNames()[joystick.controllerNumber - 1])
        {
            case "Controller (XBOX 360 For Windows)":
            case "Controller (Xbox 360 Wireless Receiver for Windows)":
                SetupXbox360Windows(joystick);
                break;
            case "X360MacWire":
            case "X360MacWireless":
                SetupXbox360Mac(joystick);
                break;
            case "X360LinuxWire":
                SetupXbox360Linux(joystick, true);
                break;
            case "X360LinuxWireless":
                SetupXbox360Linux(joystick, false);
                break;
            case "PLAYSTATION(R)3 Controller":
                SetupPS3(joystick);
                break;
            case "Wireless Controller":
                // Both PS4 controller types are named the same and only one can be configured here.
                SetupPS4Wireless(joystick);
                // SetupPS4Wired(joystick);
                break;
            case "":
                Debug.Log("No physical controller connected. Destroying virtual joystick: " + Input.GetJoystickNames()[joystick.controllerNumber - 1]);
                break;
            default:
                Debug.LogError("Incompatible controller Destroying virtual joystick: " + Input.GetJoystickNames()[joystick.controllerNumber - 1]);
                break;
        }
        return false;
    }
    #endregion

    #region Regular Functions
    /// <summary>
    /// Formats the axis name accordingly.
    /// </summary>
    /// <param name="prefix"></param>
    /// <param name="axis"></param>
    /// <returns></returns>
    public static string AxisName(string prefix, string axis)
    {
        string axisName = prefix + char.ToUpper(axis[0]);
        string leftovers = "";
        for (int i = 1; i < axis.Length; i++)
            leftovers += axis[i];
        if (leftovers != null || leftovers != "")
            axisName += leftovers;
        return axisName;
    }
    /// <summary>
    /// Assigns the correct KeyCode based on the controllerNumber and button number.
    /// </summary>
    /// <param name="controllerNumber"></param>
    /// <param name="button"></param>
    /// <returns></returns>
    public static KeyCode JoystickButton(sbyte controllerNumber, byte button) { return KeyCode.Joystick1Button0 + (20 * (controllerNumber - 1)) + button; }
    #endregion

    #region Setup Functions
    /// <summary>
    /// Sets up the keyboard for FPS games based upon the QWERTY layout.
    /// </summary>
    /// <param name="joystick"></param>
    private static void SetupKeyboard(StandardVirtualJoystick joystick)
    {
        string prefix = joystick.axisPrefix;
        joystick.leftStick = new VirtualStick(KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D, "WASD");
        joystick.rightStick = new VirtualStick(AxisName(prefix, "X"), AxisName(prefix, "Y"), "Mouse Movement", 0.1f);
        joystick.dPad = new VirtualStick(KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, "Arrow Keys");
        joystick.leftTrigger = new VirtualTrigger(KeyCode.Mouse0, "Left Mouse Click");
        joystick.rightTrigger = new VirtualTrigger(KeyCode.Mouse1, "Right Mouse Click");
        joystick.northButton = new VirtualButton(KeyCode.LeftAlt, "Left Alternate (Alt)");
        joystick.southButton = new VirtualButton(KeyCode.Space, "Spacebar");
        joystick.westButton = new VirtualButton(KeyCode.R, "R");
        joystick.eastButton = new VirtualButton(KeyCode.LeftControl, "Left Control (Ctrl)");
        joystick.leftBumper = new VirtualButton(KeyCode.Q, "Q");
        joystick.rightBumper = new VirtualButton(KeyCode.F, "F");
        joystick.leftStickPress = new VirtualButton(KeyCode.LeftShift, "Left Shift");
        joystick.rightStickPress = new VirtualButton(KeyCode.Mouse2, "Middle Mouse Click");
        joystick.startButton = new VirtualButton(KeyCode.Escape, "Escape");
        joystick.selectButton = new VirtualButton(KeyCode.Tab, "Tab");
        joystick.homeButton = new VirtualButton(KeyCode.Home, "Home");
    }
    /// <summary>
    /// Sets up the Xbox360 controller for Windows platforms.
    /// </summary>
    /// <param name="joystick"></param>
    private static void SetupXbox360Windows(StandardVirtualJoystick joystick)
    {
        string prefix = joystick.axisPrefix;
        sbyte controllerNumber = joystick.controllerNumber;
        joystick.leftStick = new VirtualStick(AxisName(prefix, "X"), AxisName(prefix, "Y"), "Left Stick", 0.2f, false, true);
        joystick.rightStick = new VirtualStick(AxisName(prefix, "4"), AxisName(prefix, "5"), "Right Stick", 0.2f, false, true);
        joystick.dPad = new VirtualStick(AxisName(prefix, "6"), AxisName(prefix, "7"), "D-Pad");
        joystick.leftTrigger = new VirtualTrigger(AxisName(prefix, "9"), "LT");
        joystick.rightTrigger = new VirtualTrigger(AxisName(prefix, "10"), "RT");
        joystick.northButton = new VirtualButton(JoystickButton(controllerNumber, 3), "Y");
        joystick.southButton = new VirtualButton(JoystickButton(controllerNumber, 0), "A");
        joystick.westButton = new VirtualButton(JoystickButton(controllerNumber, 2), "B");
        joystick.eastButton = new VirtualButton(JoystickButton(controllerNumber, 1), "X");
        joystick.leftBumper = new VirtualButton(JoystickButton(controllerNumber, 4), "LB");
        joystick.rightBumper = new VirtualButton(JoystickButton(controllerNumber, 5), "RB");
        joystick.leftStickPress = new VirtualButton(JoystickButton(controllerNumber, 8), "Left Stick Press");
        joystick.rightStickPress = new VirtualButton(JoystickButton(controllerNumber, 9), "Right Stick Press");
        joystick.startButton = new VirtualButton(JoystickButton(controllerNumber, 7), "Start");
        joystick.selectButton = new VirtualButton(JoystickButton(controllerNumber, 6), "Back");
    }
    /// <summary>
    /// Sets up the Xbox 360 controller for Mac/OSX platforms.
    /// </summary>
    /// <param name="joystick"></param>
    private static void SetupXbox360Mac(StandardVirtualJoystick joystick)
    {
        string prefix = joystick.axisPrefix;
        sbyte controllerNumber = joystick.controllerNumber;
        joystick.leftStick = new VirtualStick(AxisName(prefix, "X"), AxisName(prefix, "Y"), "Left Stick");
        joystick.rightStick = new VirtualStick(AxisName(prefix, "3"), AxisName(prefix, "4"), "Right Stick");
        joystick.dPad = new VirtualStick(JoystickButton(controllerNumber, 5), JoystickButton(controllerNumber, 6), JoystickButton(controllerNumber, 7), JoystickButton(controllerNumber, 8), "D-Pad");
        joystick.leftTrigger = new VirtualTrigger(AxisName(prefix, "5"), "LT");
        joystick.rightTrigger = new VirtualTrigger(AxisName(prefix, "6"), "RT");
        joystick.northButton = new VirtualButton(JoystickButton(controllerNumber, 19), "Y");
        joystick.southButton = new VirtualButton(JoystickButton(controllerNumber, 16), "A");
        joystick.westButton = new VirtualButton(JoystickButton(controllerNumber, 18), "X");
        joystick.eastButton = new VirtualButton(JoystickButton(controllerNumber, 17), "B");
        joystick.leftBumper = new VirtualButton(JoystickButton(controllerNumber, 13), "LB");
        joystick.rightBumper = new VirtualButton(JoystickButton(controllerNumber, 14), "RB");
        joystick.leftStickPress = new VirtualButton(JoystickButton(controllerNumber, 11), "Left Stick Press");
        joystick.rightStickPress = new VirtualButton(JoystickButton(controllerNumber, 12), "Right Stick Press");
        joystick.startButton = new VirtualButton(JoystickButton(controllerNumber, 9), "Start");
        joystick.selectButton = new VirtualButton(JoystickButton(controllerNumber, 10), "Back");
        joystick.homeButton = new VirtualButton(JoystickButton(controllerNumber, 15), "Xbox Button");
    }
    /// <summary>
    /// Sets up the Xbox 360 controller for Linux platforms depending of whether it is wired or wireless.
    /// </summary>
    /// <param name="joystick"></param>
    private static void SetupXbox360Linux(StandardVirtualJoystick joystick, bool wired)
    {
        string prefix = joystick.axisPrefix;
        sbyte controllerNumber = joystick.controllerNumber;
        joystick.leftStick = new VirtualStick(AxisName(prefix, "X"), AxisName(prefix, "Y"), "Left Stick");
        joystick.rightStick = new VirtualStick(AxisName(prefix, "4"), AxisName(prefix, "5"), "Right Stick");
        if (wired)
            joystick.dPad = new VirtualStick(AxisName(prefix, "7"), AxisName(prefix, "8"), "D-Pad");
        else
            joystick.dPad = new VirtualStick(JoystickButton(controllerNumber, 13), JoystickButton(controllerNumber, 14), JoystickButton(controllerNumber, 11), JoystickButton(controllerNumber, 12), "D-Pad");
        joystick.leftTrigger = new VirtualTrigger(AxisName(prefix, "3"), "LT");
        joystick.rightTrigger = new VirtualTrigger(AxisName(prefix, "6"), "RT");
        joystick.northButton = new VirtualButton(JoystickButton(controllerNumber, 3), "Y");
        joystick.southButton = new VirtualButton(JoystickButton(controllerNumber, 0), "A");
        joystick.westButton = new VirtualButton(JoystickButton(controllerNumber, 2), "X");
        joystick.eastButton = new VirtualButton(JoystickButton(controllerNumber, 1), "B");
        joystick.leftBumper = new VirtualButton(JoystickButton(controllerNumber, 4), "LB");
        joystick.rightBumper = new VirtualButton(JoystickButton(controllerNumber, 5), "RB");
        joystick.leftStickPress = new VirtualButton(JoystickButton(controllerNumber, 9), "Left Stick Press");
        joystick.rightStickPress = new VirtualButton(JoystickButton(controllerNumber, 10), "Right Stick Press");
        joystick.startButton = new VirtualButton(JoystickButton(controllerNumber, 7), "Start");
        joystick.selectButton = new VirtualButton(JoystickButton(controllerNumber, 6), "Back");
    }
    /// <summary>
    /// Sets up the PlayStation 3 controller.
    /// </summary>
    /// <param name="joystick"></param>
    public static void SetupPS3(StandardVirtualJoystick joystick)
    {
        string prefix = joystick.axisPrefix;
        sbyte controllerNumber = joystick.controllerNumber;
        joystick.leftStick = new VirtualStick(AxisName(prefix, "Y"), AxisName(prefix, "X"), "Left Stick");
        joystick.rightStick = new VirtualStick(AxisName(prefix, "3"), AxisName(prefix, "5"), "Right Stick");
        joystick.dPad = new VirtualStick(AxisName(prefix, "6"), AxisName(prefix, "7"), "D-Pad");
        joystick.leftTrigger = new VirtualTrigger(JoystickButton(controllerNumber, 4), "L2");
        joystick.rightTrigger = new VirtualTrigger(JoystickButton(controllerNumber, 5), "R2");
        joystick.northButton = new VirtualButton(JoystickButton(controllerNumber, 0), "Triangle");
        joystick.southButton = new VirtualButton(JoystickButton(controllerNumber, 2), "Cross");
        joystick.westButton = new VirtualButton(JoystickButton(controllerNumber, 3), "Square");
        joystick.eastButton = new VirtualButton(JoystickButton(controllerNumber, 1), "Circle");
        joystick.leftBumper = new VirtualButton(JoystickButton(controllerNumber, 6), "L1");
        joystick.rightBumper = new VirtualButton(JoystickButton(controllerNumber, 7), "R1");
        joystick.leftStickPress = new VirtualButton(JoystickButton(controllerNumber, 10), "L3");
        joystick.rightStickPress = new VirtualButton(JoystickButton(controllerNumber, 11), "R3");
        joystick.startButton = new VirtualButton(JoystickButton(controllerNumber, 9), "Start");
        joystick.selectButton = new VirtualButton(JoystickButton(controllerNumber, 8), "Select");
    }
    /// <summary>
    /// Sets up the wired PlayStation 4 controller.
    /// </summary>
    /// <param name="joystick"></param>
    public static void SetupPS4Wired(StandardVirtualJoystick joystick)
    {
        string prefix = joystick.axisPrefix;
        sbyte controllerNumber = joystick.controllerNumber;
        joystick.leftStick = new VirtualStick(AxisName(prefix, "X"), AxisName(prefix, "Y"), "Left Stick");
        joystick.rightStick = new VirtualStick(AxisName(prefix, "3"), AxisName(prefix, "6"), "Right Stick");
        joystick.dPad = new VirtualStick(AxisName(prefix, "7"), AxisName(prefix, "8"), "D-Pad");
        joystick.leftTrigger = new VirtualTrigger(AxisName(prefix, "4"), "L2");
        joystick.rightTrigger = new VirtualTrigger(AxisName(prefix, "5"), "R2");
        joystick.northButton = new VirtualButton(JoystickButton(controllerNumber, 3), "Triangle");
        joystick.southButton = new VirtualButton(JoystickButton(controllerNumber, 1), "Cross");
        joystick.westButton = new VirtualButton(JoystickButton(controllerNumber, 0), "Square");
        joystick.eastButton = new VirtualButton(JoystickButton(controllerNumber, 2), "Circle");
        joystick.leftBumper = new VirtualButton(JoystickButton(controllerNumber, 4), "L1");
        joystick.rightBumper = new VirtualButton(JoystickButton(controllerNumber, 5), "R1");
        joystick.leftStickPress = new VirtualButton(JoystickButton(controllerNumber, 10), "L3");
        joystick.rightStickPress = new VirtualButton(JoystickButton(controllerNumber, 11), "R3");
        joystick.startButton = new VirtualButton(JoystickButton(controllerNumber, 9), "Options");
        joystick.selectButton = new VirtualButton(JoystickButton(controllerNumber, 8), "Share");
        joystick.homeButton = new VirtualButton(JoystickButton(controllerNumber, 12), "PS");
    }
    /// <summary>
    /// Sets up the wireless PlayStation 4 controller.
    /// </summary>
    /// <param name="joystick"></param>
    public static void SetupPS4Wireless(StandardVirtualJoystick joystick)
    {
        string prefix = joystick.axisPrefix;
        sbyte controllerNumber = joystick.controllerNumber;
        joystick.leftStick = new VirtualStick(AxisName(prefix, "X"), AxisName(prefix, "3"), "Left Stick");
        joystick.rightStick = new VirtualStick(AxisName(prefix, "4"), AxisName(prefix, "7"), "Right Stick");
        joystick.dPad = new VirtualStick(AxisName(prefix, "8"), AxisName(prefix, "9"), "D-Pad");
        joystick.leftTrigger = new VirtualTrigger(AxisName(prefix, "5"), "L2");
        joystick.rightTrigger = new VirtualTrigger(AxisName(prefix, "6"), "R2");
        joystick.northButton = new VirtualButton(JoystickButton(controllerNumber, 3), "Triangle");
        joystick.southButton = new VirtualButton(JoystickButton(controllerNumber, 1), "Cross");
        joystick.westButton = new VirtualButton(JoystickButton(controllerNumber, 0), "Square");
        joystick.eastButton = new VirtualButton(JoystickButton(controllerNumber, 2), "Circle");
        joystick.leftBumper = new VirtualButton(JoystickButton(controllerNumber, 4), "L1");
        joystick.rightBumper = new VirtualButton(JoystickButton(controllerNumber, 5), "R1");
        joystick.leftStickPress = new VirtualButton(JoystickButton(controllerNumber, 10), "L3");
        joystick.rightStickPress = new VirtualButton(JoystickButton(controllerNumber, 11), "R3");
        joystick.startButton = new VirtualButton(JoystickButton(controllerNumber, 9), "Options");
        joystick.selectButton = new VirtualButton(JoystickButton(controllerNumber, 8), "Share");
        joystick.homeButton = new VirtualButton(JoystickButton(controllerNumber, 12), "PS");
    }
    #endregion
}