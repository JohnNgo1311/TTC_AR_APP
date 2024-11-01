using UnityEngine;
using Vuforia;

public class SimpleBarcodeScanner : MonoBehaviour
{
    BarcodeBehaviour mBarcodeBehaviour;
    GameObject canvasButton;
    void Start()
    {
        if (UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI)
            UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;
        mBarcodeBehaviour = gameObject.GetComponent<BarcodeBehaviour>();
        canvasButton = mBarcodeBehaviour.gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (mBarcodeBehaviour != null && mBarcodeBehaviour.InstanceData != null)
        {
            Debug.Log("Barcode: " + mBarcodeBehaviour.InstanceData.Text);

            if (canvasButton.name == mBarcodeBehaviour.InstanceData.Text)
            {
                Debug.Log(canvasButton.name);
                canvasButton.SetActive(true);
            }
        }
        else
        {
            canvasButton.SetActive(false);
        }
    }
}