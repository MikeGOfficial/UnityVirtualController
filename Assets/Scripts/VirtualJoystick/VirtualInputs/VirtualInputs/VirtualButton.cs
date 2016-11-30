using UnityEngine;

/// <summary>
/// Simulates a button on a controller.
/// </summary>
public class VirtualButton : AVirtualInput
{
    #region Variables
    [Tooltip("The button this Virtual Button receives input from.")]
    public readonly KeyCode buttonKey;
    #endregion

    #region Constructors
    /// <summary>
    /// Default Constructor
    /// </summary>
    /// <param name="key"></param>
    /// <param name="name"></param>
    public VirtualButton(KeyCode key, string name = "Virtual Button") : base(name) { buttonKey = key; }
    /// <summary>
    /// Copy Constructor
    /// </summary>
    /// <param name="button"></param>
    public VirtualButton(VirtualButton button) : base(button) { buttonKey = button.buttonKey; }
    #endregion

    #region Getters/Setters
    public bool IsPressed { get { return Input.GetKeyDown(buttonKey); } }
    public bool IsHeld { get { return Input.GetKey(buttonKey); } }
    public bool IsReleased { get { return Input.GetKeyUp(buttonKey); } }
    public byte GetValue
    {
        get
        {
            if (IsPressed || IsHeld)
                return 1;
            return 0;
        }
    }
    #endregion
}