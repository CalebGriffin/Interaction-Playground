using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    #region Delegates and Events

    public delegate void FingerOneStarted(InputAction.CallbackContext ctx);
    public event FingerOneStarted OnFingerOneStarted;
    public delegate void FingerTwoStarted(InputAction.CallbackContext ctx);
    public event FingerTwoStarted OnFingerTwoStarted;
    public delegate void FingerThreeStarted(InputAction.CallbackContext ctx);
    public event FingerThreeStarted OnFingerThreeStarted;
    public delegate void FingerFourStarted(InputAction.CallbackContext ctx);
    public event FingerFourStarted OnFingerFourStarted;
    public delegate void FingerOneEnded(InputAction.CallbackContext ctx);
    public event FingerOneEnded OnFingerOneEnded;
    public delegate void FingerTwoEnded(InputAction.CallbackContext ctx);
    public event FingerTwoEnded OnFingerTwoEnded;
    public delegate void FingerThreeEnded(InputAction.CallbackContext ctx);
    public event FingerThreeEnded OnFingerThreeEnded;
    public delegate void FingerFourEnded(InputAction.CallbackContext ctx);
    public event FingerFourEnded OnFingerFourEnded;

    #endregion

    #region Finger Positions

    public static Vector2 FingerOnePos { get; private set; }
    public static Vector2 FingerTwoPos { get; private set; }
    public static Vector2 FingerThreePos { get; private set; }
    public static Vector2 FingerFourPos { get; private set; }

    #endregion

    private TouchInput touchInput;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        touchInput = new TouchInput();
    }
    
    void OnEnable()
    {
        touchInput.Enable();
    }

    void OnDisable()
    {
        touchInput.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        touchInput.Touch.FingerOne.started += ctx => TouchStarted(0, ctx);
        touchInput.Touch.FingerTwo.started += ctx => TouchStarted(1, ctx);
        touchInput.Touch.FingerThree.started += ctx => TouchStarted(2, ctx);
        touchInput.Touch.FingerFour.started += ctx => TouchStarted(3, ctx);

        touchInput.Touch.FingerOne.canceled += ctx => TouchEnded(0, ctx);
        touchInput.Touch.FingerTwo.canceled += ctx => TouchEnded(1, ctx);
        touchInput.Touch.FingerThree.canceled += ctx => TouchEnded(2, ctx);
        touchInput.Touch.FingerFour.canceled += ctx => TouchEnded(3, ctx);

        touchInput.Touch.FingerOnePos.performed += ctx => TouchMoved(0, ctx);
        touchInput.Touch.FingerTwoPos.performed += ctx => TouchMoved(1, ctx);
        touchInput.Touch.FingerThreePos.performed += ctx => TouchMoved(2, ctx);
        touchInput.Touch.FingerFourPos.performed += ctx => TouchMoved(3, ctx);
    }

    void TouchStarted(int touchIndex, InputAction.CallbackContext ctx)
    {
        ////Debug.Log("Touch Started");

        switch (touchIndex)
        {
            case 0:
                OnFingerOneStarted?.Invoke(ctx);
                break;
            
            case 1:
                OnFingerTwoStarted?.Invoke(ctx);
                break;
            
            case 2:
                OnFingerThreeStarted?.Invoke(ctx);
                break;
            
            case 3:
                OnFingerFourStarted?.Invoke(ctx);
                break;
            
            default:
                break;
        }
    }

    void TouchEnded(int touchIndex, InputAction.CallbackContext ctx)
    {
        Debug.Log("Touch Ended");

        switch (touchIndex)
        {
            case 0:
                OnFingerOneEnded?.Invoke(ctx);
                break;
            
            case 1:
                OnFingerTwoEnded?.Invoke(ctx);
                break;
            
            case 2:
                OnFingerThreeEnded?.Invoke(ctx);
                break;
            
            case 3:
                OnFingerFourEnded?.Invoke(ctx);
                break;
            
            default:
                break;
        }
    }

    void TouchMoved(int touchIndex, InputAction.CallbackContext ctx)
    {
        ////Debug.Log("Touch Moved " + touchIndex + " and position is " + ctx.ReadValue<Vector2>());

        switch (touchIndex)
        {
            case 0:
                FingerOnePos = ctx.ReadValue<Vector2>();
                break;
            
            case 1:
                FingerTwoPos = ctx.ReadValue<Vector2>();
                break;
            
            case 2:
                FingerThreePos = ctx.ReadValue<Vector2>();
                break;
            
            case 3:
                FingerFourPos = ctx.ReadValue<Vector2>();
                break;
            
            default:
                break;
        }
    }
}
