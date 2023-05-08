using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class text_script : MonoBehaviour
{
    public float timeToLive = 5f;

    private void Start()
    {
        StartCoroutine(DestroyAfterDelay());
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(timeToLive);
        Destroy(gameObject);
    }
}
