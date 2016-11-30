using UnityEngine;

/// <summary>
/// Simulates a stick on a controller.
/// </summary>
public class VirtualStick : AVirtualInput
{
    #region Variables
    [Tooltip("Should this VirtualStick use buttons for input instead of an axis?")]
    public readonly bool isUsingButtonsAsStick = false;
    [Tooltip("The name of the x-axis in the Input Manager.")]
    public readonly string xAxis;
    [Tooltip("The name of the y-axis in the Input Manager.")]
    public readonly string yAxis;
    [Tooltip("Should the input from the x-axis be inverted? Ignored if the stick uses buttons.")]
    public readonly bool xInverted = false;
    [Tooltip("Should the input from the y-axis be inverted? Ignored if the stick uses buttons.")]
    public readonly bool yInverted = false;
    [Tooltip("Each axis invert value, represented as a Vector2. Assigned at creation to avoid unnecessary recalculations.")]
    public readonly Vector2 invertMultiplier = new Vector2(1, 1);
    [Tooltip("Any value between the negative and positive deadZone values return 0. Raw input ignores this.")]
    [Range(0, 0.9f)] public float deadZone = 0.3f;
    [Tooltip("The key assigned to the up direction. Ignored if not using buttons.")]
    public readonly KeyCode upKey;
    [Tooltip("The key assigned to the down direction. Ignored if not using buttons.")]
    public readonly KeyCode downKey;
    [Tooltip("The key assigned to the left direction. Ignored if not using buttons.")]
    public readonly KeyCode leftKey;
    [Tooltip("The key assigned to the right direction. Ignored if not using buttons.")]
    public readonly KeyCode rightKey;
    #endregion

    #region Constructors
    /// <summary>
    /// Copy Constructor
    /// </summary>
    /// <param name="virtualStick"></param>
    public VirtualStick(VirtualStick virtualStick) : base(virtualStick)
    {
        isUsingButtonsAsStick = virtualStick.isUsingButtonsAsStick;
        xAxis = virtualStick.xAxis;
        yAxis = virtualStick.yAxis;
        xInverted = virtualStick.xInverted;
        yInverted = virtualStick.yInverted;
        invertMultiplier = virtualStick.invertMultiplier;
        deadZone = virtualStick.deadZone;
        upKey = virtualStick.upKey;
        downKey = virtualStick.downKey;
        leftKey = virtualStick.leftKey;
        rightKey = virtualStick.rightKey;
    }
    /// <summary>
    /// Axis Constructor
    /// </summary>
    /// <param name="xAxis"></param>
    /// <param name="yAxis"></param>
    /// <param name="name"></param>
    /// <param name="dead"></param>
    /// <param name="xInvert"></param>
    /// <param name="yInvert"></param>
    public VirtualStick(string xAxis, string yAxis, string name = "Virtual Stick", float dead = 0.3f, bool xInvert = false, bool yInvert = false) : base(name)
    {
        isUsingButtonsAsStick = false;
        this.xAxis = xAxis;
        this.yAxis = yAxis;
        xInverted = xInvert;
        yInverted = yInvert;
        invertMultiplier = CalculateInvert();
        deadZone = dead;
    }
    /// <summary>
    /// Button Constructor
    /// </summary>
    /// <param name="up"></param>
    /// <param name="down"></param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <param name="name"></param>
    public VirtualStick(KeyCode up, KeyCode down, KeyCode left, KeyCode right, string name = "Virtual Pad") : base(name)
    {
        isUsingButtonsAsStick = true;
        upKey = up;
        downKey = down;
        leftKey = left;
        rightKey = right;
    }
    #endregion

    #region Facilitators
    /// <summary>
    /// Calculates the Vector2 representation of the inverted axises.
    /// </summary>
    /// <returns></returns>
    protected Vector2 CalculateInvert()
    {
        Vector2 mult = new Vector2(1, 1);
        if (xInverted)
            mult.x = -1;
        if (yInverted)
            mult.y = -1;
        return mult;
    }
    /// <summary>
    /// Calculates the raw input (not taking deadZone into consideration) of the stick.
    /// </summary>
    /// <returns></returns>
    public Vector2 GetRawInput()
    {
        Vector2 input = Vector2.zero;
        if (isUsingButtonsAsStick)
        {
            if (Input.GetKey(upKey) || Input.GetKeyDown(upKey))
                input.y += 1;
            if (Input.GetKey(downKey) || Input.GetKeyDown(downKey))
                input.y -= 1;
            if (Input.GetKey(leftKey) || Input.GetKeyDown(leftKey))
                input.x -= 1;
            if (Input.GetKey(rightKey) || Input.GetKeyDown(rightKey))
                input.x += 1;
        }
        else
            input = new Vector2(Input.GetAxisRaw(xAxis), Input.GetAxisRaw(yAxis));
        input.x *= invertMultiplier.x;
        input.y *= invertMultiplier.y;
        return input;
    }
    /// <summary>
    /// Calculates the input (using deadZone) of the stick.
    /// </summary>
    /// <returns></returns>
    public Vector2 GetInput()
    {
        Vector2 input = GetRawInput();
        if (input.x > -deadZone && input.x < deadZone)
            input.x = 0;
        if (input.y > -deadZone && input.y < deadZone)
            input.y = 0;
        return input;
    }
    #endregion
}