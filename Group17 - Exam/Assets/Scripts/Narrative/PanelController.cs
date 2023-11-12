using UnityEngine;
using System.Collections;

public class PanelController : MonoBehaviour
{
    public GameObject[] elements;

    void Start()
    {
        StartCoroutine(ActivateDeactivateElements());
    }

    IEnumerator ActivateDeactivateElements()
    {
        yield return new WaitForSeconds(7f);
        elements[0].SetActive(false);

        yield return new WaitForSeconds(0.05f);
        elements[1].SetActive(true);

        yield return new WaitForSeconds(10.5f);
        elements[1].SetActive(false);

        yield return new WaitForSeconds(0.05f);
        elements[2].SetActive(true);

        yield return new WaitForSeconds(5f);
        elements[2].SetActive(false);

        yield return new WaitForSeconds(0.05f);
        elements[3].SetActive(true);

        yield return new WaitForSeconds(5.5f);
        elements[3].SetActive(false);

        yield return new WaitForSeconds(0.05f);
        elements[4].SetActive(true);
    }
}
