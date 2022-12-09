using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectHunt : MonoBehaviour
{
    float maxDist = 0;
    float minWaitTime = 0;
    float maxWaitTime = 1.5f;
    float xLimit = 2;
    float yLimit = 4.2f;

    bool isTouching = false;

    InputManager inputManager;

    void Awake()
    {
        inputManager = InputManager.Instance;
    }

    void OnEnable()
    {
        inputManager.OnFingerOneStarted += TouchDown;
        inputManager.OnFingerOneEnded += TouchUp;
    }

    void OnDisable()
    {
        inputManager.OnFingerOneStarted -= TouchDown;
        inputManager.OnFingerOneEnded -= TouchUp;
    }

    void TouchDown(InputAction.CallbackContext ctx)
    {
        isTouching = true;
        StartCoroutine(Vibrate());
    }

    void TouchUp(InputAction.CallbackContext ctx)
    {
        isTouching = false;
        StopCoroutine(Vibrate());
    }

    IEnumerator Vibrate()
    {
        yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        Handheld.Vibrate();
        if (isTouching)
            StartCoroutine(Vibrate());
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
