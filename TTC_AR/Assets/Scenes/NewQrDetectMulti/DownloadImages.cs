using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DownloadImages : MonoBehaviour
{
    private void OnEnable()
    {
        // StartCoroutine(DownloadAllImages());
        Download_JB_TSD_Image();
    }

    private async void Download_JB_TSD_Image()
    {
        await APIManagerOpenCV.Instance.DownloadImagesAsync();
    }

    // private IEnumerator DownloadAllImages()
    // {
    //     yield return new WaitUntil(() => StaticVariable.ready_To_Download_Images_UI);
    //     Download_JB_TSD_Image();
    // }
}
