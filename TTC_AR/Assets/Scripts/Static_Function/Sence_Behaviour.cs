using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sence_Behaviour : MonoBehaviour
{
  static public void Reload_Scene(string currentSceneName)
  {
    SceneManager.LoadScene(currentSceneName);
  }
}
