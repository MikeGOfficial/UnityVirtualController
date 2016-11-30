using UnityEngine;
using System.Collections;

public class CombinedTrigger
{
    #region Variables
    [Tooltip("The triggers whose values will be combined.")]
    protected VirtualTrigger[] triggers = new VirtualTrigger[2] { null, null };
    #endregion

    #region Getters/Setters
    public float Input
    {
        get
        {
            if (triggers[0] == null && triggers[1] == null)
                return 0;
            else if (triggers[0] == null)
                return triggers[1].GetInput();
            else if (triggers[1] == null)
                return triggers[0].GetInput();
            return triggers[0].GetInput() + triggers[1].GetInput();
        }
    }
    public float RawInput
    {
        get
        {
            if (triggers[0] == null && triggers[1] == null)
                return 0;
            else if (triggers[0] == null)
                return triggers[1].GetRawInput();
            else if (triggers[1] == null)
                return triggers[0].GetRawInput();
            return triggers[0].GetRawInput() + triggers[1].GetRawInput();
        }
    }
    #endregion

    #region Constructors
    /// <summary>
    /// Standard Constructor
    /// </summary>
    /// <param name="trigger1"></param>
    /// <param name="trigger2"></param>
    public CombinedTrigger(VirtualTrigger trigger1, VirtualTrigger trigger2) { triggers = new VirtualTrigger[2] { trigger1, trigger2 }; }
    /// <summary>
    /// Array Constructor
    /// </summary>
    /// <param name="ct"></param>
    public CombinedTrigger(VirtualTrigger[] ct) { triggers = ct; }
    /// <summary>
    /// Copy Constructor
    /// </summary>
    /// <param name="ct"></param>
    public CombinedTrigger(CombinedTrigger ct) { triggers = ct.triggers; }
    #endregion
}