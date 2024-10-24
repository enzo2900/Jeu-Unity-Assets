using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DontDestroyPlayerInput :PlayerInput
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
