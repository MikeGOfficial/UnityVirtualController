using UnityEngine;
using System.Collections.Generic;

#region Enums
public enum Stick { Left, Right, Pad }
public enum Trigger { Left, Right }
public enum Button { North, South, East, West, LeftBumper, RightBumper, LeftStickPress, RightStickPress, Start, Select, Home }
#endregion

/// <summary>
/// Simulates a controller with rebindable keys and correct axes for many controllers.
/// Also, the script only detects controllers plugged in when Unity is launched.
/// </summary>
public class VirtualController
{
    #region Variables
    [Header("Identifiers")]
    public readonly byte controllerNumber = 1;
    public readonly bool azertyKeyboard = false;
    private readonly string axisPrefix;
    public readonly bool sendNull = false;

    [Header("Controls")]
    private Dictionary<Stick, VirtualStick> stick = new Dictionary<Stick, VirtualStick>()
    {
        { Stick.Left, new VirtualStick() },
        { Stick.Right, new VirtualStick() },
        { Stick.Pad, new VirtualStick() }
    };
    private Dictionary<Trigger, VirtualTrigger> trigger = new Dictionary<Trigger, VirtualTrigger>()
    {
        { Trigger.Left, new VirtualTrigger() },
        { Trigger.Right, new VirtualTrigger() }
    };
    private Dictionary<Button, VirtualButton> button = new Dictionary<Button, VirtualButton>()
    {
        { Button.North, new VirtualButton() },
        { Button.South, new VirtualButton() },
        { Button.East, new VirtualButton() },
        { Button.West, new VirtualButton() },
        { Button.LeftBumper, new VirtualButton() },
        { Button.RightBumper, new VirtualButton() },
        { Button.LeftStickPress, new VirtualButton() },
        { Button.RightStickPress, new VirtualButton() },
        { Button.Start, new VirtualButton() },
        { Button.Select, new VirtualButton() },
        { Button.Home, new VirtualButton() }
    };
    #endregion

    #region Getters/Setters
    public Dictionary<Stick, VirtualStick> AllSticks
    {
        get { return stick; }
    }
    public Dictionary<Trigger, VirtualTrigger> AllTriggers
    {
        get { return trigger; }
    }
    public Dictionary<Button, VirtualButton> AllButtons
    {
        get { return button; }
    }
    public VirtualStick LeftStick
    {
        get { return stick[Stick.Left]; }
    }
    public VirtualStick RightStick
    {
        get { return stick[Stick.Right]; }
    }
    public VirtualStick DPad
    {
        get { return stick[Stick.Pad]; }
    }
    public VirtualTrigger LeftTrigger
    {
        get { return trigger[Trigger.Left]; }
    }
    public VirtualTrigger RightTrigger
    {
        get { return trigger[Trigger.Right]; }
    }
    public VirtualButton NorthButton
    {
        get { return button[Button.North]; }
    }
    public VirtualButton SouthButton
    {
        get { return button[Button.South]; }
    }
    public VirtualButton EastButton
    {
        get { return button[Button.East]; }
    }
    public VirtualButton WestButton
    {
        get { return button[Button.West]; }
    }
    public VirtualButton LeftBumper
    {
        get { return button[Button.LeftBumper]; }
    }
    public VirtualButton RightBumper
    {
        get { return button[Button.RightBumper]; }
    }
    public VirtualButton LeftStickPress
    {
        get { return button[Button.LeftStickPress]; }
    }
    public VirtualButton RightStickPress
    {
        get { return button[Button.RightStickPress]; }
    }
    public VirtualButton StartButton
    {
        get { return button[Button.Start]; }
    }
    public VirtualButton SelectButton
    {
        get { return button[Button.Select]; }
    }
    public VirtualButton HomeButton
    {
        get { return button[Button.Home]; }
    }
    #endregion

    #region Constructors
    public VirtualController(byte controllerNumber)
    {
        this.controllerNumber = controllerNumber;
        if (Input.GetJoystickNames().Length < controllerNumber)
        {
            Debug.LogError("Not enough controllers are connected. Destorying virtual controller...");
            sendNull = true;
        }
        if (controllerNumber == 0)
        {
            if (azertyKeyboard)
                SetupAZERTY();
            else
                SetupQWERTY();
        }
        else
        {
            if (Input.GetJoystickNames().Length == 0)
                sendNull = true;
            else
            {
                axisPrefix = "Joy " + controllerNumber + " Axis ";
                switch (Input.GetJoystickNames()[controllerNumber - 1])
                {
                    case "Controller (XBOX 360 For Windows)":
                    case "Controller (Xbox 360 Wireless Receiver for Windows)":
                        SetupXboxWindows();
                        break;
                    case "XMacWire":
                    case "XMacWireless":
                        SetupXboxMac();
                        break;
                    case "XLinuxWire":
                        SetupXboxLinuxWired();
                        break;
                    case "XLinuxWireless":
                        SetupXboxLinuxWireless();
                        break;
                    case "PLAYSTATION(R)3 Controller":
                        SetupPS3();
                        break;
                    case "Wireless Controller":
                        // Both PS4 controller types have the name "Wireless Controller".
                        //SetupPS4Wired();
                        SetupPS4Wireless();
                        break;
                    default:
                        Debug.LogError("Incompatable controller. Destroying virtual controller...");
                        sendNull = true;
                        break;
                }
            }
        }
    }
    #endregion

    #region Setup Functions
    public void SetupQWERTY()
    {
        stick[Stick.Left] = new VirtualStick(KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D, "WASD (Default)");
        stick[Stick.Right] = new VirtualStick("Mouse X", "Mouse Y", "Mouse", 0.3f);
        stick[Stick.Pad] = new VirtualStick(KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, "Arrow Keys (Default)");
        trigger[Trigger.Left] = new VirtualTrigger(KeyCode.Mouse0, "Left Click");
        trigger[Trigger.Right] = new VirtualTrigger(KeyCode.Mouse1, "Right Click");
        button[Button.North] = new VirtualButton(KeyCode.E, "E");
        button[Button.South] = new VirtualButton(KeyCode.Space, "Spacebar");
        button[Button.East] = new VirtualButton(KeyCode.Backspace, "Backspace");
        button[Button.West] = new VirtualButton(KeyCode.R, "R");
        button[Button.LeftBumper] = new VirtualButton(KeyCode.Z, "Z");
        button[Button.RightBumper] = new VirtualButton(KeyCode.X, "X");
        button[Button.LeftStickPress] = new VirtualButton(KeyCode.LeftShift, "Left Shift");
        button[Button.RightStickPress] = new VirtualButton(KeyCode.Mouse2, "Scrollwheel Click");
        button[Button.Start] = new VirtualButton(KeyCode.Escape, "Escape");
        button[Button.Select] = new VirtualButton(KeyCode.Tab, "Tab");
        button[Button.Home] = new VirtualButton(KeyCode.Home, "Home");
    }
    public void SetupAZERTY()
    {
        stick[Stick.Left] = new VirtualStick(KeyCode.Z, KeyCode.S, KeyCode.Q, KeyCode.D, "ZQSD (Default)");
        stick[Stick.Right] = new VirtualStick("Mouse X", "Mouse Y", "Mouse", 0.3f, false, true);
        stick[Stick.Pad] = new VirtualStick(KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, "Arrow Keys (Default)");
        trigger[Trigger.Left] = new VirtualTrigger(KeyCode.Alpha0, "");
        trigger[Trigger.Right] = new VirtualTrigger(KeyCode.Alpha0, "");
        button[Button.North] = new VirtualButton(KeyCode.Alpha0, "");
        button[Button.South] = new VirtualButton(KeyCode.Mouse0, "Left Click");
        button[Button.East] = new VirtualButton(KeyCode.Mouse1, "Right Click");
        button[Button.West] = new VirtualButton(KeyCode.Alpha0, "");
        button[Button.LeftBumper] = new VirtualButton(KeyCode.Alpha0, "");
        button[Button.RightBumper] = new VirtualButton(KeyCode.Alpha0, "");
        button[Button.LeftStickPress] = new VirtualButton(KeyCode.Alpha0, "");
        button[Button.RightStickPress] = new VirtualButton(KeyCode.Alpha0, "");
        button[Button.Start] = new VirtualButton(KeyCode.Alpha0, "");
        button[Button.Select] = new VirtualButton(KeyCode.Alpha0, "");
        button[Button.Home] = new VirtualButton(KeyCode.Alpha0, "");
    }
    public void SetupXboxWindows()
    {
        stick[Stick.Left] = new VirtualStick(AxisName('X'), AxisName('Y'), "Left Stick", 0.2f, false, true);
        stick[Stick.Right] = new VirtualStick(AxisName(4), AxisName(5), "Right Stick", 0.2f, false, true);
        stick[Stick.Pad] = new VirtualStick(AxisName(6), AxisName(7), "D-Pad");
        trigger[Trigger.Left] = new VirtualTrigger(AxisName(9), "LT");
        trigger[Trigger.Right] = new VirtualTrigger(AxisName(10), "RT");
        button[Button.North] = new VirtualButton(ButtonCode(3), "Y");
        button[Button.South] = new VirtualButton(ButtonCode(0), "A");
        button[Button.East] = new VirtualButton(ButtonCode(1), "B");
        button[Button.West] = new VirtualButton(ButtonCode(2), "X");
        button[Button.LeftBumper] = new VirtualButton(ButtonCode(4), "LB");
        button[Button.RightBumper] = new VirtualButton(ButtonCode(5), "RB");
        button[Button.Select] = new VirtualButton(ButtonCode(6), "Back");
        button[Button.Start] = new VirtualButton(ButtonCode(7), "Start");
        button[Button.LeftStickPress] = new VirtualButton(ButtonCode(8), "Left Stick Click");
        button[Button.RightStickPress] = new VirtualButton(ButtonCode(9), "Right Stick Click");
    }
    public void SetupXboxMac()
    {
        stick[Stick.Left] = new VirtualStick(AxisName('X'), AxisName('Y'), "Left Stick");
        stick[Stick.Right] = new VirtualStick(AxisName(3), AxisName(4), "Right Stick");
        stick[Stick.Pad] = new VirtualStick(ButtonCode(5), ButtonCode(6), ButtonCode(7), ButtonCode(8), "D-Pad");
        trigger[Trigger.Left] = new VirtualTrigger(AxisName(5), "LT");
        trigger[Trigger.Right] = new VirtualTrigger(AxisName(6), "RT");
        button[Button.North] = new VirtualButton(ButtonCode(19), "Y");
        button[Button.South] = new VirtualButton(ButtonCode(16), "A");
        button[Button.East] = new VirtualButton(ButtonCode(17), "B");
        button[Button.West] = new VirtualButton(ButtonCode(18), "X");
        button[Button.LeftBumper] = new VirtualButton(ButtonCode(13), "LB");
        button[Button.RightBumper] = new VirtualButton(ButtonCode(14), "RB");
        button[Button.Select] = new VirtualButton(ButtonCode(10), "Back");
        button[Button.Start] = new VirtualButton(ButtonCode(9), "Start");
        button[Button.LeftStickPress] = new VirtualButton(ButtonCode(11), "Left Stick Click");
        button[Button.RightStickPress] = new VirtualButton(ButtonCode(12), "Right Stick Click");
        button[Button.Home] = new VirtualButton(ButtonCode(15), "Xbox Button");
    }
    public void SetupXboxLinuxWired()
    {
        stick[Stick.Left] = new VirtualStick(AxisName('X'), AxisName('Y'), "Left Stick");
        stick[Stick.Right] = new VirtualStick(AxisName(4), AxisName(5), "Right Stick");
        stick[Stick.Pad] = new VirtualStick(AxisName(7), AxisName(8), "D-Pad");
        trigger[Trigger.Left] = new VirtualTrigger(AxisName(3), "LT");
        trigger[Trigger.Right] = new VirtualTrigger(AxisName(6), "RT");
        button[Button.North] = new VirtualButton(ButtonCode(3), "Y");
        button[Button.South] = new VirtualButton(ButtonCode(0), "A");
        button[Button.East] = new VirtualButton(ButtonCode(1), "B");
        button[Button.West] = new VirtualButton(ButtonCode(2), "X");
        button[Button.LeftBumper] = new VirtualButton(ButtonCode(4), "LB");
        button[Button.RightBumper] = new VirtualButton(ButtonCode(5), "RB");
        button[Button.Select] = new VirtualButton(ButtonCode(6), "Back");
        button[Button.Start] = new VirtualButton(ButtonCode(7), "Start");
        button[Button.LeftStickPress] = new VirtualButton(ButtonCode(9), "Left Stick Click");
        button[Button.RightStickPress] = new VirtualButton(ButtonCode(10), "Right Stick Click");
    }
    public void SetupXboxLinuxWireless()
    {
        stick[Stick.Left] = new VirtualStick(AxisName('X'), AxisName('Y'), "Left Stick");
        stick[Stick.Right] = new VirtualStick(AxisName(4), AxisName(5), "Right Stick");
        stick[Stick.Pad] = new VirtualStick(ButtonCode(13), ButtonCode(14), ButtonCode(11), ButtonCode(12), "Directional Pad");
        trigger[Trigger.Left] = new VirtualTrigger(AxisName(3), "LT");
        trigger[Trigger.Right] = new VirtualTrigger(AxisName(6), "RT");
        button[Button.North] = new VirtualButton(ButtonCode(3), "Y");
        button[Button.South] = new VirtualButton(ButtonCode(0), "A");
        button[Button.East] = new VirtualButton(ButtonCode(1), "B");
        button[Button.West] = new VirtualButton(ButtonCode(2), "X");
        button[Button.LeftBumper] = new VirtualButton(ButtonCode(4), "LB");
        button[Button.RightBumper] = new VirtualButton(ButtonCode(5), "RB");
        button[Button.Select] = new VirtualButton(ButtonCode(6), "Back");
        button[Button.Start] = new VirtualButton(ButtonCode(7), "Start");
        button[Button.LeftStickPress] = new VirtualButton(ButtonCode(9), "Left Stick Click");
        button[Button.RightStickPress] = new VirtualButton(ButtonCode(10), "Right Stick Click");
    }
    public void SetupPS3()
    {
        stick[Stick.Left] = new VirtualStick(AxisName('Y'), AxisName('X'), "Left Stick");
        stick[Stick.Right] = new VirtualStick(AxisName(3), AxisName(5), "Right Stick");
        stick[Stick.Pad] = new VirtualStick(AxisName(6), AxisName(7), "D-Pad");
        trigger[Trigger.Left] = new VirtualTrigger(ButtonCode(4), "L2");
        trigger[Trigger.Right] = new VirtualTrigger(ButtonCode(5), "R2");
        button[Button.North] = new VirtualButton(ButtonCode(0), "Triangle");
        button[Button.South] = new VirtualButton(ButtonCode(2), "Cross");
        button[Button.East] = new VirtualButton(ButtonCode(1), "Circle");
        button[Button.West] = new VirtualButton(ButtonCode(3), "Square");
        button[Button.LeftBumper] = new VirtualButton(ButtonCode(6), "L1");
        button[Button.RightBumper] = new VirtualButton(ButtonCode(7), "R1");
        button[Button.Select] = new VirtualButton(ButtonCode(8), "Select");
        button[Button.Start] = new VirtualButton(ButtonCode(9), "Start");
        button[Button.LeftStickPress] = new VirtualButton(ButtonCode(10), "L3");
        button[Button.RightStickPress] = new VirtualButton(ButtonCode(11), "R3");
    }
    public void SetupPS4Wired()
    {
        stick[Stick.Left] = new VirtualStick(AxisName('X'), AxisName('Y'), "Left Stick", 0.3f, false, true);
        stick[Stick.Right] = new VirtualStick(AxisName(3), AxisName(6), "Right Stick");
        stick[Stick.Pad] = new VirtualStick(AxisName(7), AxisName(8), "D-Pad");
        trigger[Trigger.Left] = new VirtualTrigger(AxisName(4), "L2");
        trigger[Trigger.Right] = new VirtualTrigger(AxisName(5), "R2");
        button[Button.North] = new VirtualButton(ButtonCode(3), "Triangle");
        button[Button.South] = new VirtualButton(ButtonCode(1), "Cross");
        button[Button.East] = new VirtualButton(ButtonCode(2), "Circle");
        button[Button.West] = new VirtualButton(ButtonCode(0), "Square");
        button[Button.LeftBumper] = new VirtualButton(ButtonCode(4), "L1");
        button[Button.RightBumper] = new VirtualButton(ButtonCode(5), "R1");
        button[Button.Select] = new VirtualButton(ButtonCode(8), "Share");
        button[Button.Start] = new VirtualButton(ButtonCode(9), "Options");
        button[Button.LeftStickPress] = new VirtualButton(ButtonCode(10), "L3");
        button[Button.RightStickPress] = new VirtualButton(ButtonCode(11), "R3");
        button[Button.Home] = new VirtualButton(ButtonCode(12), "PS");
    }
    public void SetupPS4Wireless()
    {
        stick[Stick.Left] = new VirtualStick(AxisName('X'), AxisName(3), "Left Stick", 0.3f, false, true);
        stick[Stick.Right] = new VirtualStick(AxisName(4), AxisName(7), "Right Stick");
        stick[Stick.Pad] = new VirtualStick(AxisName(8), AxisName(9), "D-Pad");
        trigger[Trigger.Left] = new VirtualTrigger(AxisName(5), "L2");
        trigger[Trigger.Right] = new VirtualTrigger(AxisName(6), "R2");
        button[Button.North] = new VirtualButton(ButtonCode(3), "Triangle");
        button[Button.South] = new VirtualButton(ButtonCode(1), "Cross");
        button[Button.East] = new VirtualButton(ButtonCode(2), "Circle");
        button[Button.West] = new VirtualButton(ButtonCode(0), "Square");
        button[Button.LeftBumper] = new VirtualButton(ButtonCode(4), "L1");
        button[Button.RightBumper] = new VirtualButton(ButtonCode(5), "R1");
        button[Button.Select] = new VirtualButton(ButtonCode(8), "Share");
        button[Button.Start] = new VirtualButton(ButtonCode(9), "Options");
        button[Button.LeftStickPress] = new VirtualButton(ButtonCode(10), "L3");
        button[Button.RightStickPress] = new VirtualButton(ButtonCode(11), "R3");
        button[Button.Home] = new VirtualButton(ButtonCode(12), "PS");
    }
    #endregion

    #region Facilitators
    public string AxisName(char axis) { return axisPrefix + char.ToUpper(axis); }
    public string AxisName(byte axis) { return axisPrefix + axis; }
    public KeyCode ButtonCode(byte button) { return KeyCode.Joystick1Button0 + (20 * (controllerNumber - 1)) + button; }
    public void CalculateAll()
    {
        stick[Stick.Left].CalculateInput();
        stick[Stick.Right].CalculateInput();
        stick[Stick.Pad].CalculateInput();
        trigger[Trigger.Left].CalculateInput();
        trigger[Trigger.Right].CalculateInput();
        button[Button.North].CalculateInput();
        button[Button.South].CalculateInput();
        button[Button.East].CalculateInput();
        button[Button.West].CalculateInput();
        button[Button.LeftBumper].CalculateInput();
        button[Button.RightBumper].CalculateInput();
        button[Button.Select].CalculateInput();
        button[Button.Start].CalculateInput();
        button[Button.LeftStickPress].CalculateInput();
        button[Button.RightStickPress].CalculateInput();
        button[Button.Home].CalculateInput();
    }
    #endregion
}