using UnityEngine;

/// <summary>
/// Simulates a trigger on a controller.
/// </summary>
public class VirtualTrigger : AVirtualInput
{
    #region Variables
    [Tooltip("The axis this trigger receives input from.")]
    public readonly string axis;
    [Tooltip("Any value less than deadZone value return 0. Raw input ignores this.")]
    [Range(0, 0.9f)] public float deadZone = 0.1f;
    [Tooltip("Should this VirtualTrigger use a button for input instead of an axis?")]
    public readonly bool isUsingButtonAsTrigger = false;
    [Tooltip("The key this trigger receives input from, if enabled.")]
    public readonly KeyCode key;
    #endregion

    #region Constructors
    /// <summary>
    /// Axis Constructor
    /// </summary>
    /// <param name="axisName"></param>
    /// <param name="triggerName"></param>
    /// <param name="deadZone"></param>
    public VirtualTrigger(string axisName, string triggerName = "Virtual Trigger", float deadZone = 0.1f) : base(triggerName)
    {
        isUsingButtonAsTrigger = false;
        axis = axisName;
        this.deadZone = deadZone;
    }
    /// <summary>
    /// Key Constructor
    /// </summary>
    /// <param name="triggerKey"></param>
    /// <param name="triggerName"></param>
    public VirtualTrigger(KeyCode triggerKey, string triggerName = "Virtual Trigger") : base(triggerName)
    {
        isUsingButtonAsTrigger = true;
        key = triggerKey;
    }
    /// <summary>
    /// Copy Constructor
    /// </summary>
    /// <param name="trigger"></param>
    public VirtualTrigger(VirtualTrigger trigger) : base(trigger)
    {
        axis = trigger.axis;
        deadZone = trigger.deadZone;
        isUsingButtonAsTrigger = trigger.isUsingButtonAsTrigger;
        key = trigger.key;
    }
    #endregion

    #region Facilitators
    /// <summary>
    /// Returns 1 if the button is pressed and 0 if not.
    /// </summary>
    /// <returns></returns>
    protected byte CalculateButtonValue()
    {
        if (Input.GetKey(key) || Input.GetKeyDown(key))
            return 1;
        return 0;
    }
    /// <summary>
    /// Returns the raw input (ignoring deadZone) of the trigger.
    /// </summary>
    /// <returns></returns>
    public float GetRawInput()
    {
        if (isUsingButtonAsTrigger)
            return CalculateButtonValue();
        return Input.GetAxisRaw(axis);
    }
    /// <summary>
    /// Returns the input (using deadZone) of the trigger.
    /// </summary>
    /// <returns></returns>
    public float GetInput()
    {
        float raw = GetRawInput();
        if (raw < deadZone)
            return 0;
        return raw;
    }
    #endregion
}