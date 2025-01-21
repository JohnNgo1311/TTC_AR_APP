using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GlobalVariable : MonoBehaviour
{
    public static string baseUrl = "http://52.230.123.204:81/api/";

    public static string previousScene;
    public static string recentScene;
    public static string jb_TSD_Title = "";
    public static string jb_TSD_Location = "";
    public static string jb_TSD_Name = "";
    public static List<GameObject> activated_iamgeTargets = new List<GameObject>() { };
    public static bool navigate_from_List_Devices = false;
    public static bool navigate_from_JB_TSD_General = false;
    public static bool navigate_from_JB_TSD_Detail = false;
    public static GameObject generalPanel;
    public static bool loginSuccess = false;
    public static bool isOpenCanvas = false;
    public static bool ready_To_Nav_New_Scene = false;

    public static bool Load_Initial_Data_From_Selection_Scene = true;

    // //! lưu trữ tên JB và hình ảnh JB (ẢNH JB THỰC TẾ)
    // public static Dictionary<string, List<Sprite>> list_Name_And_Image_JB_Location_A = new Dictionary<string, List<Sprite>>();
    // public static Dictionary<string, List<Sprite>> list_Name_And_Image_JB_Location_B = new Dictionary<string, List<Sprite>>();
    // public static Dictionary<string, List<Sprite>> list_Name_And_Image_JB_Location_C = new Dictionary<string, List<Sprite>>();
    // public static Dictionary<string, List<Sprite>> list_Name_And_Image_JB_Location_D = new Dictionary<string, List<Sprite>>();

    // //! lưu trữ tên JB và đường dẫn ảnh JB (ẢNH JB THỰC TẾ)
    // public static Dictionary<string, List<string>> list_Name_and_Url_JB_Location_A = new Dictionary<string, List<string>>();
    // public static Dictionary<string, List<string>> list_Name_and_Url_JB_Location_B = new Dictionary<string, List<string>>();
    // public static Dictionary<string, List<string>> list_Name_and_Url_JB_Location_C = new Dictionary<string, List<string>>();
    // public static Dictionary<string, List<string>> list_Name_and_Url_JB_Location_D = new Dictionary<string, List<string>>();

    // //! lưu trữ tên JB và hình ảnh JB (Sơ đồ kết nối)
    // public static Dictionary<string, List<Sprite>> list_Name_And_Image_JB_Connection_A = new Dictionary<string, List<Sprite>>();
    // public static Dictionary<string, List<Sprite>> list_Name_And_Image_JB_Connection_B = new Dictionary<string, List<Sprite>>();
    // public static Dictionary<string, List<Sprite>> list_Name_And_Image_JB_Connection_C = new Dictionary<string, List<Sprite>>();
    // public static Dictionary<string, List<Sprite>> list_Name_And_Image_JB_Connection_D = new Dictionary<string, List<Sprite>>();

    // //! lưu trữ tên JB và đường dẫn ảnh JB (Sơ đồ kết nối)
    // public static Dictionary<string, List<string>> list_Name_and_Url_JB_Connection_A = new Dictionary<string, List<string>>();
    // public static Dictionary<string, List<string>> list_Name_and_Url_JB_Connection_B = new Dictionary<string, List<string>>();
    // public static Dictionary<string, List<string>> list_Name_and_Url_JB_Connection_C = new Dictionary<string, List<string>>();
    // public static Dictionary<string, List<string>> list_Name_and_Url_JB_Connection_D = new Dictionary<string, List<string>>();

    // //! Lưu trữ key (Sơ đồ kết nối)
    // public static List<string> list_Key_JB_Connection_A = new List<string>();
    // public static List<string> list_Key_JB_Connection_B = new List<string>();
    // public static List<string> list_Key_JB_Connection_C = new List<string>();
    // public static List<string> list_Key_JB_Connection_D = new List<string>();
    // //! Lưu trữ key (Hình ảnh thực tế)
    // public static List<string> list_Key_JB_Location_A = new List<string>();
    // public static List<string> list_Key_JB_Location_B = new List<string>();
    // public static List<string> list_Key_JB_Location_C = new List<string>();
    // public static List<string> list_Key_JB_Location_D = new List<string>();


    //! Lưu trữ tạm để không cần phải lọc lại
    public static List<Sprite> list_TempJBLocationImage = new List<Sprite>();
    public static List<Sprite> list_TempJBConnectionImage = new List<Sprite>();
    //public static List<Texture2D> list_Image_JB_Location = new List<Texture2D>();
    //    public static List<Texture2D> list_Image_JB_TSD_Wiring = new List<Texture2D>();

    //!Grapper
    public static List<Grapper_General_Non_List_Rack_Model> temp_List_Grapper_General_Non_List_Rack_Models = new List<Grapper_General_Non_List_Rack_Model>(); // Id, Name, List_Rack_General_Model
    public static List<Grapper_General_Model> temp_List_Grapper_General_Models = new List<Grapper_General_Model>(); // Id, Name, List_Rack_General_Model
    public static Grapper_General_Model temp_GrapperGeneralModel; // Id, Name, List_Rack_General_Model
    public static int GrapperId = 1;
    //!Rack
    public static List<Rack_Non_List_Module_Model> temp_List_Rack_Non_List_Module_Model = new List<Rack_Non_List_Module_Model>(); // Id, Name, List_Module_General_Non_Rack_Model
    public static List<Rack_General_Model> temp_List_Rack_General_Models = new List<Rack_General_Model>(); // Id, Name, List_Module_General_Non_Rack_Model
    public static Rack_General_Model temp_RackGeneralModel; // Id, Name, List_Module_General_Non_Rack_Model
    public static int RackId = 1;
    //!Module
    public static List<Module_General_Non_Rack_Model> temp_ListModuleGeneralNonRackModels = new List<Module_General_Non_Rack_Model>(); // Id, Name
    public static List<Module_General_Model> temp_ListModuleGeneralModels = new List<Module_General_Model>(); // Id, Name, Rack_Non_List_Module_Model
    public static Module_General_Model temp_ModuleGeneralModel; // Id, Name, Rack_Non_List_Module_Model
    public static List<ModuleInformationModel> temp_ListModuleInformationModel = new List<ModuleInformationModel>();
    public static ModuleInformationModel temp_ModuleInformationModel;
    public static int ModuleId = 1;
    //! Device
    public static List<DeviceInformationModel> temp_ListDeviceInformationModel = new List<DeviceInformationModel>();
    public static DeviceInformationModel temp_DeviceInformationModel;
    public static int DeviceId = 1;
    //! JBS
    public static List<JBInformationModel> temp_ListJBInformationModel_FromModule = new List<JBInformationModel>();
    public static JBInformationModel temp_JBInformationModel;
    public static int JBId = 1;
    public static bool ActiveCloseCanvasButton = false;
    //! Mccs
    public static List<MccModel> temp_ListMCCInformationModel = new List<MccModel>();
    public static MccModel temp_MCCInformationModel;
    public static int MCCId = 1;

    //! Field Device
    public static List<FieldDeviceInformationModel> temp_ListFieldDeviceInformationModel = new List<FieldDeviceInformationModel>();
    public static FieldDeviceInformationModel temp_FieldDeviceInformationModel;
    public static int FieldDeviceId = 1;
    //! ModuleSpecification 
    public static ModuleSpecificationModel temp_ModuleSpecificationModel;
    public static ModuleSpecificationGeneralModel temp_ModuleSpecificationGeneralModel;
    public static int ModuleSpecificationId = 1;
    //! AdapterSpecification
    public static AdapterSpecificationModel temp_AdapterSpecificationModel;
    public static int AdapterSpecificationId = 1;
    //!   

    public static List<Texture2D> temp_ListFieldDeviceConnectionImages = new List<Texture2D>();
    public static List<DeviceInformationModel_FromModule> temp_ListDeviceInformationModel_FromModule = new List<DeviceInformationModel_FromModule>();
    public static Dictionary<string, List<Texture2D>> temp_listJBConnectionImageFromModule = new Dictionary<string, List<Texture2D>>();
    public static Dictionary<string, Texture2D> temp_listJBLocationImageFromModule = new Dictionary<string, Texture2D>();
    public static AccountModel accountModel = new AccountModel("", "");

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
        "Ngay cầu thang lên MCC"        // JB11_Bis
    };

    public static List<string> sceneNamesLandScape = new List<string>()
    {
        "GrapperAScanScene",
        "GrapperBScanScene",
        "GrapperCScanScene",
        "LHScanScene",
        "FieldDevicesScene",
    };

}
