using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GlobalVariable : MonoBehaviour
{
    public static string baseUrl = "https://67176614b910c6a6e027ebfc.mockapi.io/api/v1/Device_Grapper";
    public static string previousScene;
    public static string recentScene;
    public static string jb_TSD_Title = "";
    public static string jb_TSD_Location = "";
    public static string jb_TSD_Name = "";
    public static List<GameObject> activated_iamgeTargets = new List<GameObject>();
    public static bool navigate_from_List_Devices = false;
    public static bool navigate_from_JB_TSD_General = false;
    public static string module_Type_Name = "1794-IB32";
    public static string apdapter_Type_Name = "1794-ACN15";
    public static GameObject generalPanel;
    public static bool loginSuccess = false;
    public static bool isOpenCanvas = false;
    public static bool ready_To_Nav_New_Scene = true;

    public static bool Load_Initial_Data_From_Selection_Scene = true;

    //! lưu trữ tên JB và hình ảnh JB (ẢNH JB THỰC TẾ)
    public static Dictionary<string, List<Sprite>> list_Name_And_Image_JB_Location_A = new Dictionary<string, List<Sprite>>();
    public static Dictionary<string, List<Sprite>> list_Name_And_Image_JB_Location_B = new Dictionary<string, List<Sprite>>();
    public static Dictionary<string, List<Sprite>> list_Name_And_Image_JB_Location_C = new Dictionary<string, List<Sprite>>();
    public static Dictionary<string, List<Sprite>> list_Name_And_Image_JB_Location_D = new Dictionary<string, List<Sprite>>();

    //! lưu trữ tên JB và đường dẫn ảnh JB (ẢNH JB THỰC TẾ)
    public static Dictionary<string, List<string>> list_Name_and_Url_JB_Location_A = new Dictionary<string, List<string>>();
    public static Dictionary<string, List<string>> list_Name_and_Url_JB_Location_B = new Dictionary<string, List<string>>();
    public static Dictionary<string, List<string>> list_Name_and_Url_JB_Location_C = new Dictionary<string, List<string>>();
    public static Dictionary<string, List<string>> list_Name_and_Url_JB_Location_D = new Dictionary<string, List<string>>();

    //! lưu trữ tên JB và hình ảnh JB (Sơ đồ kết nối)
    public static Dictionary<string, List<Sprite>> list_Name_And_Image_JB_Connection_A = new Dictionary<string, List<Sprite>>();
    public static Dictionary<string, List<Sprite>> list_Name_And_Image_JB_Connection_B = new Dictionary<string, List<Sprite>>();
    public static Dictionary<string, List<Sprite>> list_Name_And_Image_JB_Connection_C = new Dictionary<string, List<Sprite>>();
    public static Dictionary<string, List<Sprite>> list_Name_And_Image_JB_Connection_D = new Dictionary<string, List<Sprite>>();

    //! lưu trữ tên JB và đường dẫn ảnh JB (Sơ đồ kết nối)
    public static Dictionary<string, List<string>> list_Name_and_Url_JB_Connection_A = new Dictionary<string, List<string>>();
    public static Dictionary<string, List<string>> list_Name_and_Url_JB_Connection_B = new Dictionary<string, List<string>>();
    public static Dictionary<string, List<string>> list_Name_and_Url_JB_Connection_C = new Dictionary<string, List<string>>();
    public static Dictionary<string, List<string>> list_Name_and_Url_JB_Connection_D = new Dictionary<string, List<string>>();

    //! Lưu trữ key (Sơ đồ kết nối)
    public static List<string> list_Key_JB_Connection_A = new List<string>();
    public static List<string> list_Key_JB_Connection_B = new List<string>();
    public static List<string> list_Key_JB_Connection_C = new List<string>();
    public static List<string> list_Key_JB_Connection_D = new List<string>();
    //! Lưu trữ key (Hình ảnh thực tế)
    public static List<string> list_Key_JB_Location_A = new List<string>();
    public static List<string> list_Key_JB_Location_B = new List<string>();
    public static List<string> list_Key_JB_Location_C = new List<string>();
    public static List<string> list_Key_JB_Location_D = new List<string>();


    //! Lưu trữ tạm để không cần phải lọc lại
    public static List<Sprite> list_Temp_JB_Location_Image = new List<Sprite>();
    public static List<Sprite> list_Temp_JB_Connection_Image = new List<Sprite>();


    //public static List<Texture2D> list_Image_JB_Location = new List<Texture2D>();
    //    public static List<Texture2D> list_Image_JB_TSD_Wiring = new List<Texture2D>();
    public static AccountModel accountModel = new AccountModel()
    {
        userName = "",
        password = ""
    };

    public static List<string> pLCBoxScene = new List<string>()
    {
        "PLCBoxGrapA",
        "PLCBoxGrapB",
        "PLCBoxGrapC",
        "PLCBoxLH",
    };

    public static List<string> jBLocation = new List<string>()
    {
        "Hầm cáp MCC búa",              // JB1, JB2
        "Cầu Thang lên Chè Cân",        // JB3
        "Hành Lang Khuếch Tán",         // JB4
        "Duới chân Che Ép",             // JB5, JB6
        "Trên Vít Khuếch Tán",          // JB7, JB8 Bis, JB8
        "Che Ép",                       // JB14
        "Duới hầm cáp MCC che Ép",      // JB114, JB102, J101, JB111, Jb112
    };

    public static List<string> sceneNamesLandScape = new List<string>()
    {
        "GrapperAScanScene",
        "GrapperBScanScene",
        "GrapperCScanScene",
        "lHScanScene",
        "FieldDevicesScene",
    };

    // public static RackData_GrapperA rackData_GrapperA = new RackData_GrapperA();

    public static Dictionary<string, string> online_Module_Catalog_Url = new Dictionary<string, string>()
    {
        {"1794-IR8", "https://literature.rockwellautomation.com/idc/groups/literature/documents/in/1794-in021_-en-p.pdf"},
        {"1794-IE8", "https://literature.rockwellautomation.com/idc/groups/literature/documents/in/1794-in100_-en-p.pdf"},
        {"1794-OE8H", "https://literature.rockwellautomation.com/idc/groups/literature/documents/in/1794-in109_-en-p.pdf"},
        {"1794-IB32", "https://literature.rockwellautomation.com/idc/groups/literature/documents/in/1794-in093_-en-p.pdf"},
        {"1794-OB32P", "https://literature.rockwellautomation.com/idc/groups/literature/documents/in/1794-in094_-en-p.pdf"},
    };

    public static Dictionary<string, string> online_Adapter_Catalog_Url = new Dictionary<string, string>()
    {
        {"1794-ACN15", "https://literature.rockwellautomation.com/idc/groups/literature/documents/in/1794-in128_-en-p.pdf"}
    };
}

[System.Serializable]
public class AccountModel
{
    public string userName;
    public string password;
}

[System.Serializable]
public class DataSignalR
{
    public string StationId;
    public string LineId;
    public string TagId;
    public string TagValue;
    public DateTime TimeStamp;
}

[System.Serializable]
public class DialogModel
{
    public GameObject frameDialog;
    public TMP_Text contentDialog;
}

[System.Serializable]
public class ManufacturingInfo
{
    public string productName;
    public string referenceId;
    public string referenceName;
    public string lotCode;  // số lô
    public int lotSize;     // cỡ lô
    public Line line;
    public StationInfo[] stations;
}

[System.Serializable]
public class OperationTime
{
    public string stationId;
    public int shiftNumber;
    public int status;
    public DateTime date;
    public DateTime timestamp;
}

[System.Serializable]
public class Line
{
    public string lineId;
    public string lineName;
    public int lineType;
}

[System.Serializable]
public class StationInfo
{
    public string stationId;
    public EmployeeInfo[] employees;
    public Mfc[] mfCs;
}

[System.Serializable]
public class EmployeeInfo
{
    public string employeeId;
    public string employeeName;
}

[System.Serializable]
public class Mfc
{
    public string mfcName;
    public int value;
    public int minValue;
    public int maxValue;
}

[System.Serializable]
public class Wrapper
{
    public ManufacturingInfo[] manufacturingData;
}

public class ErrorInfor
{
    public string errorName;
    public string time;
}
