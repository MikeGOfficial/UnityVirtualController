using UnityEngine;
using System.Collections;

public class CombinedStick
{
    #region Variables
    [Tooltip("The sticks whose values will be combined.")]
    protected VirtualStick[] sticks = new VirtualStick[2] { null, null };
    #endregion

    #region Getters/Setters
    public Vector2 Input
    {
        get
        {
            if (sticks[0] == null && sticks[1] == null)
                return Vector2.zero;
            else if (sticks[0] == null)
                return sticks[1].GetInput();
            else if (sticks[1] == null)
                return sticks[0].GetInput();
            Vector2 combined = sticks[0].GetInput() + sticks[1].GetInput();
            combined.x = Mathf.Clamp(combined.x, -1, 1);
            combined.y = Mathf.Clamp(combined.y, -1, 1);
            return combined;
        }
    }
    public Vector2 RawInput
    {
        get
        {
            if (sticks[0] == null && sticks[1] == null)
                return Vector2.zero;
            else if (sticks[0] == null)
                return sticks[1].GetRawInput();
            else if (sticks[1] == null)
                return sticks[0].GetRawInput();
            Vector2 combined = sticks[0].GetRawInput() + sticks[1].GetRawInput();
            combined.x = Mathf.Clamp(combined.x, -1, 1);
            combined.y = Mathf.Clamp(combined.y, -1, 1);
            return combined;
        }
    }
    #endregion

    #region Constructors
    /// <summary>
    /// Standard Constructor
    /// </summary>
    /// <param name="stick1"></param>
    /// <param name="stick2"></param>
    public CombinedStick(VirtualStick stick1, VirtualStick stick2) { sticks = new VirtualStick[2] { stick1, stick2 }; }
    /// <summary>
    /// Array Constructor
    /// </summary>
    /// <param name="cs"></param>
    public CombinedStick(VirtualStick[] cs) { sticks = cs; }
    /// <summary>
    /// Copy Constructor
    /// </summary>
    /// <param name="cs"></param>
    public CombinedStick(CombinedStick cs) { sticks = cs.sticks; }
    #endregion
}
