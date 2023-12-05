using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controller for all the fireballs in the game
/// </summary>
public class FireballManager : ObjectManager
{
    private static FireballManager instance;
    
    public static FireballManager Instance
    {
        get
        {
            instance ??= new FireballManager();

            return instance;
        }
    }
}
