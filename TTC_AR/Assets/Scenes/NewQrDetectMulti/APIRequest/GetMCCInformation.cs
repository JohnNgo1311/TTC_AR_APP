using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using EasyUI.Progress;

public class GetMCCInformation : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private EventPublisher eventPublisher;

    private void Awake()
    {
        eventPublisher.OnButtonClicked += GetMCCInformationModel;
    }

    private void Destroy()
    {
        eventPublisher.OnButtonClicked -= GetMCCInformationModel;
    }

    public async void GetMCCInformationModel()
    {
        try
        {
            StaticVariable.ready_To_Nav_New_Scene = false;
            StaticVariable.ready_To_Update_MCC_UI = false;

            // var mccCabinetCode = title.text.Split(' ')[1];
            var mccCabinetCode = title.text;
            // Debug.Log("mccCabinetCode: " + mccCabinetCode);

            //? Mcc tương ứng
            var mcc = StaticVariable.temp_ListMccInformationModel.Find(mcc => mcc.CabinetCode == mccCabinetCode);

            if (mcc == null)
            {
                Show_Toast.Instance.ShowToast("error", "Không tìm thấy thông tin mcc!");
                return;
            }

            await Move_On_Main_Thread.RunOnMainThread(() =>
                      {
                          ShowProgressBar("Đang tải dữ liệu...", "Vui lòng chờ...");
                      });

            await APIManagerOpenCV.Instance.GetMcc(StaticVariable.GetMCCUrl + "/" + mcc.Id);

            await Move_On_Main_Thread.RunOnMainThread(() =>
               {
                   HideProgressBar();
               });

            StaticVariable.ready_To_Nav_New_Scene = true;
            StaticVariable.ready_To_Update_MCC_UI = true;

        }
        catch (Exception)
        {
            StaticVariable.ready_To_Nav_New_Scene = false;
            // Xử lý lỗi và hiển thị thông báo
            await Move_On_Main_Thread.RunOnMainThread(() =>
             {
                 Show_Toast.Instance.ShowToast("failure", "Đã có lỗi xảy ra");
             });
            await Task.Delay(1000);
            await Move_On_Main_Thread.RunOnMainThread(() =>
              {
                  HideProgressBar();
              });
        }
    }

    private void ShowProgressBar(string title, string details)
    {
        Progress.Show(title, ProgressColor.Blue, true);
        Progress.SetDetailsText(details);
    }

    private void HideProgressBar()
    {
        Progress.Hide();
    }
}
