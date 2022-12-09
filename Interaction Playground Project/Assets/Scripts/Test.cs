using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Test : MonoBehaviour
{
    float waitTime = 1;

    public Slider slider;

    public TextMeshProUGUI text;

    IEnumerator Vibrate()
    {
        yield return new WaitForSeconds(waitTime);
        Handheld.Vibrate();
        StartCoroutine(Vibrate());
    }

    void Awake()
    {
        StartCoroutine(Vibrate());
    }

    public void UpdateWaitTime()
    {
        waitTime = slider.value;
        text.text = $"Value: {waitTime}";
    }
}
