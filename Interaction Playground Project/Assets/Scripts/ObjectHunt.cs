using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ObjectHunt : MonoBehaviour
{
    float minWaitTime = 0;
    float maxWaitTime = 1.5f;
    float xLimit = 2;
    float yLimit = 4.2f;
    float minDist = 0;
    float maxDist;

    float shakeThreshold = 0.5f;

    bool isTouching = false;
    bool objectFound = false;

    InputManager inputManager;

    [SerializeField]
    GameObject trailPrefab;

    [SerializeField]
    GameObject objectPrefab;

    GameObject currentTrail;

    GameObject objectToFind;

    [SerializeField]
    TextMeshProUGUI text;

    [SerializeField]
    TextMeshProUGUI debugText;

    Accelerometer accelerometer;

    void Awake()
    {
        inputManager = InputManager.Instance;
        maxDist = Vector2.Distance(new Vector2(xLimit, yLimit), new Vector2(-xLimit, -yLimit));
        SpawnObject();
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
        if (text.text == "Move your finger to find the object!")
            text.text = "";
        StartCoroutine(Vibrate());
    }

    void TouchUp(InputAction.CallbackContext ctx)
    {
        isTouching = false;
        StopCoroutine(Vibrate());
    }

    IEnumerator Vibrate()
    {
        yield return new WaitUntil(() => objectFound == false);
        yield return new WaitForSeconds(CalculateWaitTime());
        Handheld.Vibrate();
        if (isTouching)
            StartCoroutine(Vibrate());
    }

    float CalculateWaitTime()
    {
        Vector3 worldFingerPos = CalculateFingerPos();
        float dist = Vector3.Distance(worldFingerPos, objectToFind.transform.position);
        return (dist - minDist) / (maxDist - minDist) * (maxWaitTime - minWaitTime) + minWaitTime;
    }

    Vector3 CalculateFingerPos()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(InputManager.FingerOnePos.x, InputManager.FingerOnePos.y, 10));
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouching && !objectFound)
            UpdateTrailPos();
        else
            currentTrail = null;

        if (Vector3.Distance(CalculateFingerPos(), objectToFind.transform.position) < 0.5f)
        {
            ObjectFound();
        }

        if (InputManager.AccelerometerValue.magnitude > shakeThreshold && objectFound)
        {
            SpawnObject();
        }

        debugText.text = InputManager.AccelerometerValue.ToString() + " " + InputManager.AccelerometerValue.magnitude.ToString();
    }

    void UpdateTrailPos()
    {
        Vector3 worldFingerPos = CalculateFingerPos();
        if (currentTrail == null)
        {
            Destroy(currentTrail);
            currentTrail = Instantiate(trailPrefab, new Vector3(worldFingerPos.x, worldFingerPos.y, 0), Quaternion.identity);
        }
        else
        {
            currentTrail.transform.position = worldFingerPos;
        }
    }

    void ObjectFound()
    {
        objectToFind.SendMessage("Found");
        text.text = "Shake to start again!";
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        objectFound = true;
    }

    void SpawnObject()
    {
        text.text = "Move your finger to find the object!";
        float x = Random.Range(-xLimit, xLimit);
        float y = Random.Range(-yLimit, yLimit);
        objectToFind = Instantiate(objectPrefab, new Vector3(x, y, 0), Quaternion.identity);
        objectFound = false;
    }
}
