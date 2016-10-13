using UnityEngine;

#region Enums
public enum ButtonState { Wait, Press, Hold, Release }
#endregion

/// <summary>
/// Base attributes for VirtualInputs.
/// </summary>
public abstract class VirtualInputBase
{
    #region Variables
    [Header("Properties")]
    public readonly string customName = "Virtual Input";
    #endregion

    #region Constructors
    public VirtualInputBase(string name = "Virtual Input") { customName = name; }
    #endregion

    #region Facilitators
    public override string ToString()
    {
        if (customName == null)
            return "";
        return customName;
    }
    public abstract void CalculateInput();
    #endregion
}

/// <summary>
/// Simulates a stick on a controller.
/// </summary>
public class VirtualStick : VirtualInputBase
{
    #region Variables
    [Header("Properties")]
    public readonly bool usingButtonsAsStick = false;
    public readonly string xAxis = "";
    public readonly string yAxis = "";
    public readonly bool xInverted = false;
    public readonly bool yInverted = false;
    [Range(0, 0.9f)] public float deadZone = 0.3f;
    public readonly KeyCode upKey;
    public readonly KeyCode downKey;
    public readonly KeyCode leftKey;
    public readonly KeyCode rightKey;

    [Header("Values")]
    private Vector2 rawInput = Vector2.zero;
    private Vector2 input = Vector2.zero;
    #endregion

    #region Getters/Setters
    public Vector2 RawInput
    {
        get { return rawInput; }
    }

    public Vector2 GetInput
    {
        get { return input; }
    }
    #endregion

    #region Constructors
    public VirtualStick() : base("Virtual Stick") { }
    public VirtualStick(string xAxis, string yAxis, string name = "Virtual Stick", float dead = 0.3f, bool xInvert = false, bool yInvert = false) : base(name)
    {
        usingButtonsAsStick = false;
        this.xAxis = xAxis;
        this.yAxis = yAxis;
        xInverted = xInvert;
        yInverted = yInvert;
        deadZone = dead;
    }
    public VirtualStick(KeyCode up, KeyCode down, KeyCode left, KeyCode right, string name = "Virtual Pad") : base(name)
    {
        usingButtonsAsStick = true;
        upKey = up;
        downKey = down;
        leftKey = left;
        rightKey = right;
    }
    #endregion

    #region Facilitators
    public float RestrictInput(float value)
    {
        if (value >= -deadZone && value <= deadZone)
            return 0;
        return value;
    }
    public override void CalculateInput()
    {
        rawInput = Vector2.zero;
        if (usingButtonsAsStick)
        {
            if (Input.GetKey(upKey))
                rawInput.y += 1;
            if (Input.GetKey(downKey))
                rawInput.y -= 1;
            if (Input.GetKey(leftKey))
                rawInput.x -= 1;
            if (Input.GetKey(rightKey))
                rawInput.x += 1;
        }
        else
        {
            rawInput.x = Input.GetAxisRaw(xAxis);
            rawInput.y = Input.GetAxisRaw(yAxis);
            if (xInverted)
                rawInput.x *= -1;
            if (yInverted)
                rawInput.y *= -1;
        }
        input.x = RestrictInput(rawInput.x);
        input.y = RestrictInput(rawInput.y);
    }
    #endregion
}

/// <summary>
/// Simulates a trigger on a controller.
/// </summary>
public class VirtualTrigger : VirtualInputBase
{
    #region Variables
    [Header("Properties")]
    public readonly string axisName = "";
    [Range(0, 0.9f)] public float deadZone = 0.1f;
    public readonly bool usingButtonAsTrigger = false;
    public readonly KeyCode triggerKey;

    [Header("Values")]
    private float rawInput = 0;
    private float input = 0;
    private ButtonState rawState = ButtonState.Wait;
    private ButtonState state = ButtonState.Wait;
    #endregion

    #region Getters/Setters
    public float RawInput
    {
        get { return rawInput; }
    }
    public float GetInput
    {
        get { return input; }
    }
    public ButtonState RawState
    {
        get { return rawState; }
    }
    public ButtonState State
    {
        get { return state; }
    }
    #endregion

    #region Constructors
    public VirtualTrigger() : base("Virtual Trigger") { }
    public VirtualTrigger(string axisName, string name = "Virtual Trigger", float dead = 0.1f) : base(name)
    {
        usingButtonAsTrigger = false;
        this.axisName = axisName;
        deadZone = dead;
    }
    public VirtualTrigger(KeyCode key, string name = "Virtual Trigger") : base(name)
    {
        usingButtonAsTrigger = true;
        triggerKey = key;
    }
    #endregion

    #region Facilitators
    public float RestrictInput(float value)
    {
        if (value < deadZone)
            return 0;
        return value;
    }
    public override void CalculateInput()
    {
        if (usingButtonAsTrigger)
            CalculateButtonInput();
        else
        {
            rawInput = Input.GetAxisRaw(axisName);
            input = RestrictInput(rawInput);
            CalculateState(rawInput, ref rawState);
            CalculateState(input, ref state);
        }
    }
    protected void CalculateButtonInput()
    {
        if (Input.GetKeyDown(triggerKey))
        {
            rawInput = 1;
            input = 1;
            rawState = ButtonState.Press;
            state = ButtonState.Press;
        }
        else if (Input.GetKey(triggerKey))
        {
            rawInput = 1;
            input = 1;
            rawState = ButtonState.Hold;
            state = ButtonState.Hold;
        }
        else if (Input.GetKeyUp(triggerKey))
        {
            rawInput = 0;
            input = 0;
            rawState = ButtonState.Release;
            state = ButtonState.Release;
        }
        else
        {
            rawInput = 0;
            input = 0;
            rawState = ButtonState.Wait;
            state = ButtonState.Wait;
        }
    }

    protected void CalculateState(float input, ref ButtonState state)
    {
        switch(state)
        {
            case ButtonState.Wait:
                if (input > 0)
                    state = ButtonState.Press;
                break;
            case ButtonState.Press:
                if (input > 0)
                    state = ButtonState.Hold;
                else
                    state = ButtonState.Release;
                break;
            case ButtonState.Hold:
                if (input <= 0)
                    state = ButtonState.Release;
                break;
            //case ButtonState.Release:
            default:
                if (input <= 0)
                    state = ButtonState.Press;
                else
                    state = ButtonState.Wait;
                break;
        }
    }
    #endregion
}

/// <summary>
/// Simulates a button on a controller.
/// </summary>
public class VirtualButton : VirtualInputBase
{
    #region Variables
    [Header("Properties")]
    public readonly KeyCode buttonKey;

    [Header("Value")]
    private ButtonState state = ButtonState.Wait;
    private float value = 1;
    #endregion

    #region Getters/Setters
    public ButtonState State
    {
        get { return state; }
    }
    public float Value
    {
        get { return value; }
    }
    #endregion

    #region Constructors
    public VirtualButton() : base("Virtual Button") { }
    public VirtualButton(KeyCode key, string name = "Virtual Button") : base(name) { buttonKey = key; }
    #endregion

    #region Facilitators
    public override void CalculateInput()
    {
        if (Input.GetKeyDown(buttonKey))
            state = ButtonState.Press;
        else if (Input.GetKey(buttonKey))
            state = ButtonState.Hold;
        else if (Input.GetKeyUp(buttonKey))
            state = ButtonState.Release;
        else
            state = ButtonState.Wait;

        switch(state)
        {
            case ButtonState.Press:
            case ButtonState.Hold:
                value = 1;
                break;
            default:
                value = 0;
                break;
        }
    }
    #endregion
}