using System;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class InputListener : MonoBehaviour
{
    // World
    public static event Action<InputValue> Move;
    public static event Action Shoot;
    public static event Action<InputValue> Aim;
    public static event Action<InputValue> Camera;
    public static event Action<InputValue> GamepadCamera;
    public static event Action Drop;
    public static event Action Grab;
    public static event Action Reload;
    public static event Action DropLethal; 
    
    public static event Action<float> ChangeLethal;

    // UI
    public static event Action Pause;
    public static event Action Resume;
    public static event Action Navigate;
    public static event Action<InputValue> MoveCursor;

    private void OnMove(InputValue input)
    {
        Move?.Invoke(input);
    }

    public void OnShoot()
    {
        Shoot?.Invoke();
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
    
    public void OnGrab()
    {
        Grab?.Invoke();
    }
    
    public void OnReload()
    {
        Reload?.Invoke();
    }
    
    public void OnPause()
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

    public void OnDropLethal()
    {
        DropLethal?.Invoke();
    }

    private void OnCursor(InputValue input)
    {
        MoveCursor?.Invoke(input);
    }

    private static void OnChangeLethal(InputValue input)
    {
        ChangeLethal?.Invoke(input.Get<float>());
    }
}