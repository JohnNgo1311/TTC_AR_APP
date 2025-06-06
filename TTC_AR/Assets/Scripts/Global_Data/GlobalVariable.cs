using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using ApplicationLayer.Dtos.AdapterSpecification;
using ApplicationLayer.Dtos.Company;
using ApplicationLayer.Dtos.Grapper;
using ApplicationLayer.Dtos.ModuleSpecification;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ModuleInformationModel;

public class GlobalVariable : MonoBehaviour
{
    public static string baseUrl = "http://52.230.123.204:81/api/";
    public static List<string> APIRequestType = new List<string>()
    {

    };


    public static List<string> AllowedRequests = new List<string>()
    {
    "GET_Grapper_List",
    "POST_Grapper",
    "PUT_Grapper",
    "DELETE_Grapper",

    "GET_Rack_List",
    "POST_Rack",
    "PUT_Rack",
    "DELETE_Rack",

    "GET_Module_List",
    "POST_Module",
    "PUT_Module",
    "DELETE_Module",

    "GET_JB_List_General",
    "GET_JB_List_Information",
    "GET_JB",
    "POST_JB",
    "PUT_JB",
    "DELETE_JB",

   "GET_Device_List_General",
   "GET_Device_List_Information_FromModule",
   "GET_Device_List_Information_Grapper",
    "GET_Device",
    "POST_Device",
    "PUT_Device",
    "DELETE_Device",

    "GET_ModuleSpecification_List",
    "POST_ModuleSpecification",
    "PUT_ModuleSpecification",
    "DELETE_ModuleSpecification",

    "GET_AdapterSpecification_List",
    "GET_AdapterSpecification",
    "POST_AdapterSpecification",
    "PUT_AdapterSpecification",
    "DELETE_AdapterSpecification",

    "GET_Mcc_List",
    "POST_Mcc",
    "PUT_Mcc",
    "DELETE_Mcc",

    "GET_FieldDevice_List",
    "POST_FieldDevice",
    "PUT_FieldDevice",
    "DELETE_FieldDevice",

    "GET_Image_List"
    };

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
    public static bool API_Status = false;
    public static string API_Error_Text = "";

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

    //! Company
    public static string companyId = "1";
    public static List<CompanyBasicDto> temp_ListCompanyBasicDto = new List<CompanyBasicDto>();
    public static CompanyResponseDto temp_CompanyResponseDto;
    public static CompanyBasicDto temp_CompanyBasicDto;
    public static CompanyRequestDto temp_CompanyRequestDto;
    public static Dictionary<string, CompanyBasicDto> temp_Dictionary_CompanyBasicDto = new Dictionary<string, CompanyBasicDto>();

    //!Grapper
    public static string GrapperId = "1";
    public static List<GrapperBasicDto> temp_ListGrapperBasicDto = new List<GrapperBasicDto>(); // Id, Name, List_Rack_Basic_Dto
    public static GrapperResponseDto temp_GrapperResponseDto; // Id, Name, List_Rack_Basic_Dto
    public static GrapperBasicDto temp_GrapperBasicDto; // Id, Name, List_Rack_Basic_Dto
    public static GrapperRequestDto temp_GrapperRequestDto; // Id, Name, List_Rack_Basic_Dto
    public static Dictionary<string, GrapperBasicDto> temp_Dictionary_GrapperBasicDto = new Dictionary<string, GrapperBasicDto>();
    public static List<GrapperBasicModel> temp_ListGrapperBasicModels = new List<GrapperBasicModel>(); // Id, Name, List_Rack_Basic_Model
    public static GrapperBasicModel temp_GrapperBasicModel; // Id, Name, List_Rack_Basic_Model

    public static List<GrapperInformationModel> temp_List_GrapperInformationModel = new List<GrapperInformationModel>();

    //!Rack
    public static List<RackBasicModel> temp_ListRackBasicModels = new List<RackBasicModel>(); // Id, Name, List_ ModuleBasicNonRackModel
    public static RackBasicModel temp_RackBasicModel; // Id, Name, List_ ModuleBasicNonRackModel
    public static string RackId = "1";

    public static List<RackInformationModel> temp_List_RackInformationModel = new List<RackInformationModel>();
    public static Dictionary<string, RackInformationModel> temp_Dictionary_RackInformationModel = new Dictionary<string, RackInformationModel>();


    //!Module
    public static List<ModuleBasicModel> temp_ListModuleBasicModels = new List<ModuleBasicModel>(); // Id, Name, Rack_Non_List_Module_Model
    public static ModuleBasicModel temp_ModuleBasicModel; // Id, Name, Rack_Non_List_Module_Model
    public static List<ModuleInformationModel> temp_ListModuleInformationModel = new();
    public static ModuleInformationModel temp_ModuleInformationModel;
    public static string ModuleId = "1";
    public static Dictionary<string, ModuleInformationModel> temp_Dictionary_ModuleInformationModel = new();
    public static Dictionary<string, ModuleBasicModel> temp_Dictionary_ModuleBasicModel = new();

    //? List này chỉ có Id và Name
    public static List<ModuleInformationModel> temp_List_ModuleInformationModel = new List<ModuleInformationModel>();

    //! Device
    public static List<DeviceInformationModel> temp_ListDeviceInformationModelFromModule = new List<DeviceInformationModel>();
    public static DeviceInformationModel temp_DeviceInformationModel;
    // public static Dictionary<string, DeviceBasicModel> temp_Dictionary_DeviceBasicModel = new Dictionary<string, DeviceBasicModel>();
    public static Dictionary<string, DeviceInformationModel> temp_Dictionary_DeviceInformationModel = new Dictionary<string, DeviceInformationModel>();
    public static Dictionary<string, string> temp_Dictionary_DeviceIOAddress = new Dictionary<string, string>();
    public static string DeviceId = "`1";


    public static List<DeviceInformationModel> temp_List_DeviceInformationModel = new List<DeviceInformationModel>() { };


    //! JBS
    public static List<JBInformationModel> temp_ListJBInformationModelFromModule_FromModule = new List<JBInformationModel>();
    public static List<JBInformationModel> temp_ListJBInformationModelFromModule = new List<JBInformationModel>();
    public static JBInformationModel temp_JBInformationModel;
    public static string JBId = "1";
    public static bool ActiveCloseCanvasButton = false;
    public static Dictionary<string, JBInformationModel> temp_Dictionary_JBInformationModel = new Dictionary<string, JBInformationModel>();
    public static Dictionary<string, JBBasicModel> temp_Dictionary_JBBasicModel = new Dictionary<string, JBBasicModel>();


    public static List<JBInformationModel> temp_List_JBInformationModel = new List<JBInformationModel>();

    //! Mccs
    public static List<MccInformationModel> temp_ListMCCInformationModel = new List<MccInformationModel>();
    public static MccInformationModel temp_MCCInformationModel;
    public static string MccId = "1";
    public static Dictionary<string, MccInformationModel> temp_Dictionary_MCCInformationModel = new Dictionary<string, MccInformationModel>();


    public static List<MccInformationModel> temp_List_MccInformationModel = new List<MccInformationModel>();
    public static Dictionary<string, MccInformationModel> temp_Dictionary_MccInformationModel = new Dictionary<string, MccInformationModel>();

    //! Field Device
    public static List<FieldDeviceInformationModel> temp_ListFieldDeviceInformationModel = new List<FieldDeviceInformationModel>();
    public static FieldDeviceInformationModel temp_FieldDeviceInformationModel;
    public static string FieldDeviceId = "1";
    public static Dictionary<string, FieldDeviceInformationModel> temp_Dictionary_FieldDeviceInformationModel = new Dictionary<string, FieldDeviceInformationModel>();


    public static List<FieldDeviceInformationModel> temp_List_FieldDeviceInformationModel = new List<FieldDeviceInformationModel>();


    //! ModuleSpecification 
    public static List<ModuleSpecificationBasicDto> temp_ListModuleSpecificationBasicDto = new List<ModuleSpecificationBasicDto>();
    public static ModuleSpecificationResponseDto temp_ModuleSpecificationResponseDto;
    public static ModuleSpecificationBasicDto temp_ModuleSpecificationBasicDto;
    public static ModuleSpecificationRequestDto temp_ModuleSpecificationRequestDto;
    public static Dictionary<string, ModuleSpecificationBasicDto> temp_Dictionary_ModuleSpecificationBasicDto = new Dictionary<string, ModuleSpecificationBasicDto>();

    public static ModuleSpecificationModel temp_ModuleSpecificationModel;
    public static ModuleSpecificationBasicModel temp_ModuleSpecificationBasicModel;
    public static string ModuleSpecificationId = "1";
    public static Dictionary<string, ModuleSpecificationModel> temp_Dictionary_ModuleSpecificationModel = new Dictionary<string, ModuleSpecificationModel>();
    public static List<ModuleSpecificationModel> temp_List_ModuleSpecificationModel = new List<ModuleSpecificationModel>();

    //! AdapterSpecification
    public static string AdapterSpecificationId = "1";
    public static List<AdapterSpecificationBasicDto> temp_ListAdapterSpecificationBasicDto = new List<AdapterSpecificationBasicDto>();
    public static AdapterSpecificationResponseDto temp_AdapterSpecificationResponseDto;
    public static AdapterSpecificationBasicDto temp_AdapterSpecificationBasicDto;
    public static AdapterSpecificationRequestDto temp_AdapterSpecificationRequestDto;
    public static Dictionary<string, AdapterSpecificationBasicDto> temp_Dictionary_AdapterSpecificationBasicDto = new Dictionary<string, AdapterSpecificationBasicDto>();

    public static AdapterSpecificationModel temp_AdapterSpecificationModel;
    public static Dictionary<string, AdapterSpecificationModel> temp_Dictionary_AdapterSpecificationModel = new Dictionary<string, AdapterSpecificationModel>();
    public static List<AdapterSpecificationModel> temp_List_AdapterSpecificationModel = new List<AdapterSpecificationModel>();

    //! ImageInformation
    public static ImageInformationModel temp_ImageInformationModel;
    public static string ImageId = "1";
    public static Dictionary<string, ImageInformationModel> temp_Dictionary_ImageInformationModel = new Dictionary<string, ImageInformationModel>();
    public static Dictionary<string, ImageBasicModel> temp_Dictionary_ImageBasicModel = new Dictionary<string, ImageBasicModel>();
    public static List<ImageInformationModel> temp_List_ImageInformationModel = new List<ImageInformationModel>()
    {

          new ImageInformationModel("1", "Image1.png", "https://www.google.com.vn/"),
          new ImageInformationModel("2", "Image2.jpg", "https://www.google.com.vn/"),
          new ImageInformationModel("3", "Image3.bmp", "https://www.google.com.vn/"),
          new ImageInformationModel("4", "Image4.gif", "https://www.google.com.vn/"),
          new ImageInformationModel("5", "Image5.svg", "https://www.google.com.vn/"),
          new ImageInformationModel("6", "Image6.png", "https://www.google.com.vn/"),
          new ImageInformationModel("7", "Image7.jpg", "https://www.google.com.vn/"),
          new ImageInformationModel("8", "Image8.bmp", "https://www.google.com.vn/"),
          new ImageInformationModel("9", "Image9.gif", "https://www.google.com.vn/"),
          new ImageInformationModel("10", "Image10.svg", "https://www.google.com.vn/"),
          new ImageInformationModel("11", "Image11.png", "https://www.google.com.vn/"),
          new ImageInformationModel("12", "Image12.jpg", "https://www.google.com.vn/"),
          new ImageInformationModel("13", "Image13.bmp", "https://www.google.com.vn/"),
          new ImageInformationModel("14", "Image14.gif", "https://www.google.com.vn/"),
          new ImageInformationModel("15", "Image15.svg", "https://www.google.com.vn/"),
          new ImageInformationModel("16", "Image16.png", "https://www.google.com.vn/"),
          new ImageInformationModel("17", "Image17.jpg", "https://www.google.com.vn/"),
          new ImageInformationModel("18", "Image18.bmp", "https://www.google.com.vn/"),
          new ImageInformationModel("19", "Image19.gif", "https://www.google.com.vn/"),
          new ImageInformationModel("20", "Image20.svg", "https://www.google.com.vn/")
    };


    //OpenCV
    public static bool isCameraPaused = false;


    public static List<Texture2D> temp_ListFieldDeviceConnectionImages = new List<Texture2D>();
    public static List<DeviceInformationModel> temp_ListDeviceInformationModelFromModule_FromModule = new List<DeviceInformationModel>();
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

    public static List<string> list_jBName = new List<string>()
    {
        "JB1",              // JB1, JB2
        "JB2",        // JB3
        "JB3",         // JB4
        "JB4",             // JB5, JB6
        "JB5",          // JB7, JB8 Bis, JB8
        "JB6",                       // JB14
        "JBp",      // JB114, JB102, J101, JB111, Jb112
        "FDSFD"   ,
        "TSDA",
        "TASDA2",
        "JB12321",
        "đâsdasdasdas",
        "sdasdsdasdas",
        "Dasdsadsadas",
        "dsdsadasdasdas",
        "JB101-JB107",
        "JB212-JB105",    // JB11_Bis
    };
    public static List<string> list_ModuleIOName = new List<string>(){
             "A4.0.I",
             "A4.1.I",
             "A4.2.I",
             "A4.3.I",
             "A4.4.I",
             "A4.5.I",
             "A4.6.I",
             "A4.7.I",
             "A5.0.I",
             "A5.1.I",
             "A5.2.I",
             "A5.3.I",
             "A5.4.I",
             "A5.5.I",
             "A5.6.I",
             "A5.7.I",
             "D1.0.I",
             "D1.1.I",
             "D1.2.I",
             "D1.3.I",
             "D1.4.I",
             "D1.5.I",
             "D1.6.I",
             "D1.7.I",
             "D2.0.I",
             "D2.1.I",
             "D2.2.I",
             "D2.3.I",
             "D2.4.I",
             "D2.5.I",
             "D2.6.I",
             "D2.7.I"
    };

    public static List<string> list_DeviceCode = new List<string>(){
        "01TT005","01TT007","01TT008","01TT012","01TT009","01TT010","01TT011","01TT015","02TT045",
        "02TT002","02TT003A","02TT003B","09LDT016","02LT002","02LT004","03LT001","02LT003","09LT019","02TT020","02TT018","02TT004","02TT007","02TT009","02TT011","02TT013","02TT016",
        "02TT023","02TT024","02TT025","02TT026","02TT027"
    };

    public static List<string> list_ImageName = new List<string>(){
      "GrapA_Connection_Additional_Armoires régul_1.png",
"GrapA_Connection_Additional_Dao cat ABB & siemen_1.png",
"GrapA_Connection_Additional_factory_1.png",
"GrapA_Connection_Additional_Feuil2_1.png",
"GrapA_Connection_Additional_Feuil3_2_1.png",
"GrapA_Connection_Additional_Feuil4_2_1.png",
"GrapA_Connection_Additional_NHIET DO MCC1_1.png",
"GrapA_Connection_Additional_tempmoul_1.png",
"GrapA_Connection_JB100-101_1.png",
"GrapA_Connection_JB100-101_2.png",
"GrapA_Connection_JB102-103_1.png",
"GrapA_Connection_JB102-103_2.png",
"GrapA_Connection_JB104_1.png",
"GrapA_Connection_JB10_1.png",
"GrapA_Connection_JB110-111_1.png",
"GrapA_Connection_JB112-113_1.png",
"GrapA_Connection_JB114_1.png",
"GrapA_Connection_JB116_1.png",
"GrapA_Connection_JB11_1.png",
"GrapA_Connection_JB11_Bis_1.png",
"GrapA_Connection_JB12_1.png",
"GrapA_Connection_JB14_1.png",
"GrapA_Connection_JB1_1.png",
"GrapA_Connection_JB1_2.png",
"GrapA_Connection_JB212-JB115_1.png",
"GrapA_Connection_JB2_1.png",
"GrapA_Connection_JB3_1.png",
"GrapA_Connection_JB3_2.png",
"GrapA_Connection_JB4_1.png",
"GrapA_Connection_JB4_2.png",
"GrapA_Connection_JB5_1.png",
"GrapA_Connection_JB5_2.png",
"GrapA_Connection_JB6_1.png",
"GrapA_Connection_JB6_Bis_1.png",
"GrapA_Connection_JB7_1.png",
"GrapA_Connection_JB8_1.png",
"GrapA_Connection_JB8_Bis_1.png",
"GrapA_Connection_JB9_1.png",
"GrapA_Connection_KT_X10_ANALOG.png",
"GrapA_Connection_KT_X15_OUT_110.24_31.png",
"GrapA_Connection_KT_X7_IN_I101.0_15.png",
"GrapA_Connection_KT_X7_OUT_109.0_15.png",
"GrapA_Connection_KT_X8_IN_I101.15_31.png",
"GrapA_Connection_KT_X8_OUT_109.16_31.png",
"GrapA_Connection_KT_X8_OUT_109.31_36.png",
"GrapA_Connection_KT_X9_IN_I101.40_2.png",
"GrapA_Connection_KT_X9_OUT_110.0_15.png",
"GrapA_Connection_MCC_CHE EP LON_1.png",
"GrapA_Connection_MCC_CHE EP LON_2.png",
"GrapA_Connection_MCC_CHE EP LON_CHE EP NHO_10.png",
"GrapA_Connection_MCC_CHE EP LON_CHE EP NHO_11.png",
"GrapA_Connection_MCC_CHE EP LON_CHE EP NHO_12.png",
"GrapA_Connection_MCC_CHE EP LON_CHE EP NHO_13.png",
"GrapA_Connection_MCC_CHE EP LON_CHE EP NHO_14.png",
"GrapA_Connection_MCC_CHE EP LON_CHE EP NHO_3.png",
"GrapA_Connection_MCC_CHE EP LON_CHE EP NHO_4.png",
"GrapA_Connection_MCC_CHE EP LON_CHE EP NHO_5.png",
"GrapA_Connection_MCC_CHE EP LON_CHE EP NHO_6.png",
"GrapA_Connection_MCC_CHE EP LON_CHE EP NHO_7.png",
"GrapA_Connection_MCC_CHE EP LON_CHE EP NHO_8.png",
"GrapA_Connection_MCC_CHE EP LON_CHE EP NHO_9.png",
"GrapA_Connection_MCC_CHE EP NHO_1.png",
"GrapA_Connection_MCC_CHE EP NHO_2.png",
"GrapA_Connection_MCC_T1_1.png",
"GrapA_Connection_MCC_T5_1.png",
"GrapA_Connection_MCC_T5_2.png",
"GrapA_Connection_TSD10_1.png",
"GrapA_Connection_TSD10_2.png",
"GrapA_Connection_TSD11_1.png",
"GrapA_Connection_TSD12_1.png",
"GrapA_Connection_TSD13_1.png",
"GrapA_Connection_TSD14_1.png",
"GrapA_Connection_TSD15_1.png",
"GrapA_Connection_TSD15_2.png",
"GrapA_Connection_TSD16_1.png",
"GrapA_Connection_TSD17_1.png",
"GrapA_Connection_TSD18_1.png",
"GrapA_Connection_TSD19_1.png",
"GrapA_Connection_TSD1_1.png",
"GrapA_Connection_TSD2_1.png",
"GrapA_Connection_TSD2_Bis_1.png",
"GrapA_Connection_TSD3_1.png",
"GrapA_Connection_TSD4_1.png",
"GrapA_Connection_TSD5_1.png",
"GrapA_Connection_TSD6_1.png",
"GrapA_Connection_TSD7_1.png",
"GrapA_Connection_TSD7_2.png",
"GrapA_Connection_TSD8_1.png",
"GrapA_Connection_TSD9_1.png"
    };

}
