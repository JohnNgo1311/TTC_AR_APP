using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowToastError : MonoBehaviour
{
    public static ShowToastError Instance { get; set; }
    [SerializeField] private GameObject toastPrefab;
    [SerializeField] private Transform parentTransform;
    private GameObject toastInstance = null;

    private void Start()
    {
        // if (Instance != null && Instance != this)
        // {
        //     Destroy(this);
        //     // Debug.LogWarning("Multiple instances of ShowToastError found. Destroying duplicate.");
        // }
        // else
        // {
        //Debug.Log("ShowToastError Instance created.");
        Instance = this;
        // }
    }

    public IEnumerator ShowToast(string message, float duration = 2f)
    {
        Debug.Log("ShowToast called with message: " + message);
        // toastPrefab.SetActive(true);
        if (toastInstance == null)
        {
            toastInstance = Instantiate(toastPrefab, parentTransform);
        }

        toastInstance.GetComponentInChildren<TMP_Text>().text = message;
        yield return new WaitForSeconds(duration);
        toastInstance.SetActive(false);
    }

    private void OnDisable()
    {
        Instance = null;

        if (toastInstance != null)
        {
            Destroy(toastInstance);
        }
    }
}
