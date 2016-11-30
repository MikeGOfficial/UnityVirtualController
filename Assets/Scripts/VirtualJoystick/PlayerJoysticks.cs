using UnityEngine;

//To create joysticks for more players, just copy and paste the P1Joystick class and change the name.

/// <summary>
/// Player 1's Virtual Joystick.
/// </summary>
public static class P1Joystick
{
    #region Variables
    [Tooltip("The two Virtual Joysticks the player uses for input. Usually uses one keyboard and one controller to allow swapping on the fly.")]
    public static StandardVirtualJoystick[] joysticks = new StandardVirtualJoystick[2] { new StandardVirtualJoystick(0), new StandardVirtualJoystick(1) };
    #endregion

    #region Getters/Setters
    // These return the combined input of both the controllers.
    [Tooltip("Has the values of both the left sticks combined.")]
    public static CombinedStick LeftStick = new CombinedStick(joysticks[0].leftStick, joysticks[1].leftStick);
    [Tooltip("Has the values of both the right sticks combined.")]
    public static CombinedStick RightStick = new CombinedStick(joysticks[0].rightStick, joysticks[1].rightStick);
    [Tooltip("Has the values of both the directional pads combined.")]
    public static CombinedStick DPad = new CombinedStick(joysticks[0].dPad, joysticks[1].dPad);

    [Tooltip("Has the values of both the left triggers combined.")]
    public static CombinedTrigger LeftTrigger = new CombinedTrigger(joysticks[0].leftTrigger, joysticks[1].leftTrigger);
    [Tooltip("Has the values of both the left triggers combined.")]
    public static CombinedTrigger RightTrigger = new CombinedTrigger(joysticks[0].rightTrigger, joysticks[1].rightTrigger);

    [Tooltip("Has the values of both the north buttons combined.")]
    public static CombinedButton NorthButton = new CombinedButton(joysticks[0].northButton, joysticks[1].northButton);
    [Tooltip("Has the values of both the south buttons combined.")]
    public static CombinedButton SouthButton = new CombinedButton(joysticks[0].southButton, joysticks[1].southButton);
    [Tooltip("Has the values of both the west buttons combined.")]
    public static CombinedButton WestButton = new CombinedButton(joysticks[0].westButton, joysticks[1].westButton);
    [Tooltip("Has the values of both the east buttons combined.")]
    public static CombinedButton EastButton = new CombinedButton(joysticks[0].eastButton, joysticks[1].eastButton);
    [Tooltip("Has the values of both the left bumpers combined.")]
    public static CombinedButton LeftBumper = new CombinedButton(joysticks[0].leftBumper, joysticks[1].leftBumper);
    [Tooltip("Has the values of both the left bumpers combined.")]
    public static CombinedButton RightBumper = new CombinedButton(joysticks[0].rightBumper, joysticks[1].rightBumper);
    [Tooltip("Has the values of both the left stick presses combined.")]
    public static CombinedButton LeftStickPress = new CombinedButton(joysticks[0].leftStickPress, joysticks[1].leftStickPress);
    [Tooltip("Has the values of both the left stick presses combined.")]
    public static CombinedButton RightStickPress = new CombinedButton(joysticks[0].rightStickPress, joysticks[1].rightStickPress);
    [Tooltip("Has the values of both the start buttons combined.")]
    public static CombinedButton StartButton = new CombinedButton(joysticks[0].startButton, joysticks[1].startButton);
    [Tooltip("Has the values of both the select buttons combined.")]
    public static CombinedButton SelectButton = new CombinedButton(joysticks[0].selectButton, joysticks[1].selectButton);
    [Tooltip("Has the values of both the home buttons combined.")]
    public static CombinedButton HomeButton = new CombinedButton(joysticks[0].homeButton, joysticks[1].homeButton);
    #endregion
}
