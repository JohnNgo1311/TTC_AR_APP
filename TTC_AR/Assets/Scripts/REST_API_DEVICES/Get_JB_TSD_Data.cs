
using UnityEngine;
using System.Threading.Tasks;
public class Get_JB_TSD_Data : MonoBehaviour
{
    // URL của JSON file (thay bằng URL của bạn)
    string url = "https://67176614b910c6a6e027ebfc.mockapi.io/api/v1/JB_TSD_Information_GrapperA";
    string grapper_Name = "A";
    async void Start()
    {
        if (GlobalVariable.Load_Initial_Data_From_Selection_Scene)
        {
            await Get_JB_TSD_Infor();
            GlobalVariable.Load_Initial_Data_From_Selection_Scene = false;
        }
    }
    public async Task Get_JB_TSD_Infor()
    {
        await APIManager.Instance.Get_JB_TSD_Information(url, grapper_Name);
    }

}