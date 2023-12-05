using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
