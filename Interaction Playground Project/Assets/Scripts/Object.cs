using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    int childToKeep = 0;
    // Start is called before the first frame update
    void Awake()
    {
        int childCount = transform.childCount;
        childToKeep = Random.Range(0, childCount);
    }

    public void Found()
    {
        transform.GetChild(childToKeep).gameObject.SetActive(true);
        StartCoroutine(FoundRoutine());
    }

    IEnumerator FoundRoutine()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
