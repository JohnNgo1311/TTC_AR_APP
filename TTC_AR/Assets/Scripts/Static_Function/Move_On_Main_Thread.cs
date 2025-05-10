using System;
using System.Collections;
using System.Threading.Tasks;
using PimDeWitte.UnityMainThreadDispatcher;
using UnityEngine;
using UnityEngine.UI;


public class Move_On_Main_Thread : MonoBehaviour
{
  //public static bool isResize = false;
  void Start()
  {
  }
  public static Task RunOnMainThread(Action action)
  {
    if (action == null) throw new ArgumentNullException(nameof(action));

    var tcs = new TaskCompletionSource<bool>();

    // Đảm bảo hành động được thực thi trên main thread
    UnityMainThreadDispatcher.Instance.Enqueue(() =>
    {
      try
      {
        action();
        tcs.SetResult(true);
      }
      catch (Exception ex)
      {
        tcs.SetException(ex);
      }
    });

    return tcs.Task;
  }
}
