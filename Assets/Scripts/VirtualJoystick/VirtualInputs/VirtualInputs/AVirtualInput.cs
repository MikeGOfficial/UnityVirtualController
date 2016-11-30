using UnityEngine;

/// <summary>
/// Base attributes for VirtualInputs.
/// </summary>
public abstract class AVirtualInput
{
    #region Variables
    [Tooltip("The name of the VirtualInput.")]
    public readonly string inputName = "Virtual Input";
    #endregion

    #region Constructors
    /// <summary>
    /// Default Constructor
    /// </summary>
    /// <param name="name"></param>
    public AVirtualInput(string name = "Virtual Input") { inputName = name; }
    public AVirtualInput(AVirtualInput virtualInput) { inputName = virtualInput.inputName; }
    #endregion

    #region Facilitators
    /// <summary>
    /// Overrides Unity's ToString() function.
    /// </summary>
    /// <returns></returns>
    public override string ToString() { return "Virtual Input: " + inputName; }
    #endregion
}