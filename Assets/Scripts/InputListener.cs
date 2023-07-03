using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputListener : MonoBehaviour
{
    // World
    public static event Action<InputValue> Move;
    public static event Action<InputValue> Shoot;
    public static event Action<InputValue> Aim;
    public static event Action<InputValue> Camera;
    public static event Action<InputValue> GamepadCamera;
    public static event Action Drop;
    public static event Action Grab;
    public static event Action Reload;
    public static event Action DropLethal; 

    // UI
    public static event Action Pause;
    public static event Action Resume;
    public static event Action Navigate;

    private void OnMove(InputValue input)
    {
        Move?.Invoke(input);
    }

    private void OnShoot(InputValue input)
    {
        Shoot?.Invoke(input);
    }

    private void OnAim(InputValue input)
    {
        Aim?.Invoke(input);
    }
    
    private void OnCamera(InputValue input)
    {
        Camera?.Invoke(input);
    }

    private void OnGamepadCamera(InputValue input)
    {
        GamepadCamera?.Invoke(input);
    }
    
    private void OnDrop()
    {
        Drop?.Invoke();
    }
    
    private void OnGrab()
    {
        Grab?.Invoke();
    }
    
    private void OnReload()
    {
        Reload?.Invoke();
    }
    
    private void OnPause()
    {
        Pause?.Invoke();
    }

    private void OnResume()
    {
        Resume?.Invoke();
    }

    private void OnNavigate()
    {
        Navigate?.Invoke();
    }

    private void OnDropLethal()
    {
        DropLethal?.Invoke();
    }
}