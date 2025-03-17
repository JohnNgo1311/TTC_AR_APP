using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticVariable
{
    //TODO: API URL
    public static string GetListModuleUrl = "https://67d64a84286fdac89bc185e3.mockapi.io/GetListModule";
    public static string GetModuleUrl = "https://67d3e0ea8bca322cc26b6004.mockapi.io/TTC/GetModule";
    public static string GetListDevicesFromModuleUrl = "https://67d3e0ea8bca322cc26b6004.mockapi.io/TTC/GetListDeviceFromModule";
    public static string GetJBUrl = "https://67d64a84286fdac89bc185e3.mockapi.io/GetJB";


    public static List<ModuleInformationModel> temp_ListModuleInformationModel = new List<ModuleInformationModel>();
    // public static List<RackBasicModel> temp_ListRackBasicModels = new List<RackBasicModel>();
    // public static List<ModuleBasicModel> temp_ListModuleBasicModels = new List<ModuleBasicModel>();
    public static Dictionary<string, ModuleInformationModel> Dic_ModuleInformationModel = new Dictionary<string, ModuleInformationModel>();
    public static bool API_Status = false;
    public static bool ready_To_Nav_New_Scene = false;
    public static bool navigate_from_JB_TSD_Detail = false;
    public static bool ActiveCloseCanvasButton = false;

    public static string ModuleId;
    public static string ModuleSpecificationId;
    public static string AdapterSpecificationId;
    public static List<JBInformationModel> temp_ListJBInformationModel = new List<JBInformationModel>();
    public static List<DeviceInformationModel> temp_ListDeviceInformationModel = new List<DeviceInformationModel>();
    public static ModuleInformationModel temp_ModuleInformationModel;
    public static ModuleSpecificationModel temp_ModuleSpecificationModel;
    public static AdapterSpecificationModel temp_AdapterSpecificationModel;

    public static bool navigate_from_List_Devices = false;
    public static bool navigate_from_JB_TSD_Basic = false;

    public static string jb_TSD_Title;
    public static string jb_TSD_Name;
    public static string jb_TSD_Location;

    public static Dictionary<string, List<Texture2D>> temp_listJBConnectionImageFromModule = new Dictionary<string, List<Texture2D>>();
    public static Dictionary<string, Texture2D> temp_listJBLocationImageFromModule = new Dictionary<string, Texture2D>();
}
