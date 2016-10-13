using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Combines input from virtual controllers and sends updates values for each controller.
/// Can have up to 2 virtual controllers. The specified primary controller will override the secondary controller's inputs unless there is no input coming from the primary controller.
/// Intended to allow the player to switch between both a keyboard with a mouse and a controller.
/// </summary>
public class ControllerInput : MonoBehaviour
{
    #region Variables
    [Tooltip("The primary controller's number. This controller will override the secondary controller's input if input is given. Recommended to be set to 1 (Controller 1) or the number pertaining to the player's number.")]
    [SerializeField] private byte primaryControllerNumber = 1;
    [Tooltip("The secondary controller's number. This controller's input will be overwritten if the primary controller is giving input. Recommended to be set to 0 (Keyboard and Mouse) or -1 if not used.")]
    [SerializeField] private byte secondaryControllerNumber = 0;
    [Tooltip("Joystick 0 is the secondary controller and 1 is the primary.")]
    private VirtualController[] joystick;
    private bool singleController;
    private bool usingPrimaryController = true;
    #endregion

    #region Getters/Setters
    public byte PrimaryControllerNumber
    {
        get { return primaryControllerNumber; }
    }
    public byte SecondaryControllerNumber
    {
        get { return secondaryControllerNumber; }
    }
    public VirtualController PrimaryJoystick
    {
        get { return joystick[1]; }
    }
    public VirtualController SecondaryJoystick
    {
        get { return joystick[0]; }
    }
    public bool SingleController
    {
        get { return singleController; }
    }
    public bool UsingPrimaryController
    {
        get { return usingPrimaryController; }
    }

    public VirtualStick LeftStick
    {
        get
        {
            if (usingPrimaryController)
                return joystick[1].LeftStick;
            else
                return joystick[0].LeftStick;
        }
    }
    public VirtualStick RightStick
    {
        get
        {
            if (usingPrimaryController)
                return joystick[1].RightStick;
            else
                return joystick[0].RightStick;
        }
    }
    public VirtualStick DPad
    {
        get
        {
            if (usingPrimaryController)
                return joystick[1].DPad;
            else
                return joystick[0].DPad;
        }
    }
    public VirtualTrigger LeftTrigger
    {
        get
        {
            if (usingPrimaryController)
                return joystick[1].LeftTrigger;
            else
                return joystick[0].LeftTrigger;
        }
    }
    public VirtualTrigger RightTrigger
    {
        get
        {
            if (usingPrimaryController)
                return joystick[1].RightTrigger;
            else
                return joystick[0].RightTrigger;
        }
    }
    public VirtualButton NorthButton
    {
        get
        {
            if (usingPrimaryController)
                return joystick[1].NorthButton;
            else
                return joystick[0].NorthButton;
        }
    }
    public VirtualButton SouthButton
    {
        get
        {
            if (usingPrimaryController)
                return joystick[1].SouthButton;
            else
                return joystick[0].SouthButton;
        }
    }
    public VirtualButton EastButton
    {
        get
        {
            if (usingPrimaryController)
                return joystick[1].EastButton;
            else
                return joystick[0].EastButton;
        }
    }
    public VirtualButton WestButton
    {
        get
        {
            if (usingPrimaryController)
                return joystick[1].WestButton;
            else
                return joystick[0].WestButton;
        }
    }
    public VirtualButton LeftBumper
    {
        get
        {
            if (usingPrimaryController)
                return joystick[1].LeftBumper;
            else
                return joystick[0].LeftBumper;
        }
    }
    public VirtualButton RightBumper
    {
        get
        {
            if (usingPrimaryController)
                return joystick[1].RightBumper;
            else
                return joystick[0].RightBumper;
        }
    }
    public VirtualButton LeftStickPress
    {
        get
        {
            if (usingPrimaryController)
                return joystick[1].LeftStickPress;
            else
                return joystick[0].LeftStickPress;
        }
    }
    public VirtualButton RightStickPress
    {
        get
        {
            if (usingPrimaryController)
                return joystick[1].RightStickPress;
            else
                return joystick[0].RightStickPress;
        }
    }
    public VirtualButton StartButton
    {
        get
        {
            if (usingPrimaryController)
                return joystick[1].StartButton;
            else
                return joystick[0].StartButton;
        }
    }
    public VirtualButton SelectButton
    {
        get
        {
            if (usingPrimaryController)
                return joystick[1].SelectButton;
            else
                return joystick[0].SelectButton;
        }
    }
    public VirtualButton HomeButton
    {
        get
        {
            if (usingPrimaryController)
                return joystick[1].HomeButton;
            else
                return joystick[0].HomeButton;
        }
    }
    #endregion

    #region Facilitators
    public void DetermineActiveController()
    {
        usingPrimaryController = false;
        foreach(KeyValuePair<Stick, VirtualStick> s in joystick[1].AllSticks)
            if (s.Value.GetInput != Vector2.zero)
            {
                usingPrimaryController = true;
                return;
            }
        foreach(KeyValuePair<Trigger, VirtualTrigger> t in joystick[1].AllTriggers)
            if (t.Value.GetInput != 0)
            {
                usingPrimaryController = true;
                return;
            }
        foreach(KeyValuePair<Button, VirtualButton> b in joystick[1].AllButtons)
            if (b.Value.State != ButtonState.Wait)
            {
                usingPrimaryController = true;
                return;
            }
    }
    #endregion

    #region MonoBehaviors
    protected virtual void Awake()
    {
        joystick = new VirtualController[2];
        if (secondaryControllerNumber < 0)
            joystick[0] = null;
        else
        {
            joystick[0] = new VirtualController(secondaryControllerNumber);
            if (joystick[0].sendNull)
                joystick[0] = null;
        }
        if (primaryControllerNumber < 0)
            joystick[1] = null;
        else
        {
            joystick[1] = new VirtualController(primaryControllerNumber);
            if (joystick[1].sendNull)
                joystick[1] = null;
        }
    }

    protected virtual void Start()
    {
        if (joystick[0] == null && joystick[1] == null)
            Debug.LogError("No controllers are configured.");
        else if (joystick[0] == null || joystick[1] == null)
        {
            Debug.Log("Only one controller is configured, using that controller for all inputs.");
            if (joystick[1] == null)
            {
                joystick[1] = joystick[0];
                joystick[0] = null;
                singleController = true;
                usingPrimaryController = true;
            }
        }
        else if (joystick[0].controllerNumber == joystick[1].controllerNumber)
        {
            Debug.LogError("Two controllers with the same number created, destroying the secondary controller.");
            joystick[0] = null;
        }
    }

    protected virtual void Update()
    {
        joystick[1].CalculateAll();
        if (joystick[0] != null)
            joystick[0].CalculateAll();
        if (!singleController)
            DetermineActiveController();
    }
    #endregion
}