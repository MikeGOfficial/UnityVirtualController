using UnityEngine;
public class CombinedButton
{
    #region Variables
    [Tooltip("The buttons whose values will be combined.")]
    protected VirtualButton[] buttons = new VirtualButton[2] { null, null };
    #endregion

    #region Getters/Setters
    public bool IsPressed
    {
        get
        {
            if (buttons[0] == null && buttons[1] == null)
                return false;
            else if (buttons[0] == null)
                return buttons[1].IsPressed;
            else if (buttons[1] == null)
                return buttons[0].IsPressed;
            if (buttons[0].IsPressed || buttons[1].IsPressed)
                return true;
            return false;
        }
    }
    public bool IsHeld
    {
        get
        {
            if (buttons[0] == null && buttons[0] == null)
                return false;
            else if (buttons[0] == null)
                return buttons[1].IsHeld;
            else if (buttons[1] == null)
                return buttons[0].IsHeld;
            if (buttons[0].IsHeld || buttons[1].IsHeld)
                return true;
            return false;
        }
    }
    public bool IsReleased
    {
        get
        {
            if (buttons[0] == null && buttons[1] == null)
                return false;
            else if (buttons[0] == null)
                return buttons[1].IsReleased;
            else if (buttons[1] == null)
                return buttons[0].IsReleased;
            if (buttons[0].IsReleased || buttons[1].IsReleased)
                if (!(IsPressed || IsHeld))
                    return true;
            return false;
        }
    }
    public byte Value
    {
        get
        {
            if (IsPressed || IsHeld)
                return 1;
            return 0;
        }
    }
    #endregion

    #region Constructors
    /// <summary>
    /// Standard Constructor
    /// </summary>
    /// <param name="button1"></param>
    /// <param name="button2"></param>
    public CombinedButton(VirtualButton button1, VirtualButton button2) { buttons = new VirtualButton[2] { button1, button2 }; }
    /// <summary>
    /// Array Constructor
    /// </summary>
    /// <param name="cb"></param>
    public CombinedButton(VirtualButton[] cb) { buttons = cb; }
    /// <summary>
    /// Copy Constructor
    /// </summary>
    /// <param name="joysticks"></param>
    public CombinedButton(CombinedButton cb) { buttons = cb.buttons; }
    #endregion

    #region Functions
    public override string ToString()
    {
        if (IsPressed)
            return "Pressed";
        if (IsReleased)
            return "Released";
        if (IsHeld)
            return "Held";
        return "Waiting";
    }
    #endregion
}