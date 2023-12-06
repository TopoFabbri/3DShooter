using UnityEngine;

/// <summary>
/// Helper class for instantiating GameObjects
/// </summary>
public static class Factory
{
    /// <summary>
    /// Instantiates the given GameObject at its default position and rotation.
    /// </summary>
    /// <param name="obj">The GameObject to instantiate.</param>
    public static GameObject Spawn(GameObject obj)
    {
        return Object.Instantiate(obj);
    }

    /// <summary>
    /// Instantiates the GameObject as a child of the specified parent Transform.
    /// </summary>
    /// <param name="obj">The GameObject to instantiate.</param>
    /// <param name="parent">The parent Transform to attach the instantiated GameObject.</param>

    public static GameObject Spawn(GameObject obj, Transform parent)
    {
        return Object.Instantiate(obj, parent);
    }

    /// <summary>
    /// Instantiates the GameObject at a specific position and rotation.
    /// </summary>
    /// <param name="obj">The GameObject to instantiate.</param>
    /// <param name="pos">The position to place the instantiated GameObject.</param>
    /// <param name="rot">The rotation for the instantiated GameObject.</param>
    public static GameObject Spawn(GameObject obj, Vector3 pos, Quaternion rot)
    {
        return Object.Instantiate(obj, pos, rot);
    }
    
    /// <summary>
    /// Instantiates the GameObject at a specific position and rotation, then sets it as a child of the given parent Transform.
    /// </summary>
    /// <param name="obj">The GameObject to instantiate.</param>
    /// <param name="pos">The position to place the instantiated GameObject.</param>
    /// <param name="rot">The rotation for the instantiated GameObject.</param>
    /// <param name="parent">The parent Transform to attach the instantiated GameObject.</param>
    public static GameObject Spawn(GameObject obj, Vector3 pos, Quaternion rot, Transform parent)
    {
        return Object.Instantiate(obj, pos, rot, parent);
    }
}
