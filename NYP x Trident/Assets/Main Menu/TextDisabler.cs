using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextDisabler : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(LifetimeCoutdown());
    }

    private void OnDisable()
    {
        gameObject.transform.GetComponent<TextMeshProUGUI>().text = "";
    }

    IEnumerator LifetimeCoutdown()
    {
        yield return new WaitForSeconds(3f);

        gameObject.SetActive(false);
    }
}
