// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;
// using EasyUI.Progress;
// using System.Collections;

// public class Dropdown_On_ValueChange : MonoBehaviour
// {
//     public GameObject prefab_Device;
//     // public SearchDeviceAndJBView searchableDropDownView;
//     public Open_Detail_Image open_Detail_Image;
//     public EventPublisher eventPublisher;

//     [SerializeField] private RectTransform contentTransform;
//     [SerializeField] private TMP_Text code_Value_Text, function_Value_Text, range_Value_Text, unit_Value_Text, io_Value_Text, jb_Connection_Value_Text, jb_Connection_Location_Text;
//     [SerializeField] private Image JB_Location_Image_Prefab, JB_Connection_Wiring_Image_Prefab;
//     [SerializeField] private GameObject JB_Connection_Group, bottom_App_Bar;
//     [SerializeField] private ScrollRect scrollRect;
//     [SerializeField] private Dictionary<string, Sprite> spriteCache = new();
//     [SerializeField] private List<Image> instantiatedImages = new();
//     [SerializeField] private Transform deviceInfo;

//     private string current_Search_Content = "";

//     private Dictionary<string, DeviceInformationModel> deviceDictionary;
//     private Dictionary<string, JBInformationModel> jBDictionary;
//     private string _jbName = "";
//     private string _moduleName = "";

//     private void Awake()
//     {
//         searchableDropDownView ??= GameObject.Find("Searchable").GetComponent<SearchDeviceAndJBView>();
//         InitUIElements();
//     }

//     private void OnEnable()
//     {
//         // if (eventPublisher != null)
//         // {
//         //     eventPublisher.OnOrientationChanged += HandleOrientationChange;
//         // }
//     }

//     private void OnDisable()
//     {
//         // if (eventPublisher != null)
//         // {
//         //     eventPublisher.OnOrientationChanged -= HandleOrientationChange;
//         // }
//         ResetResources();

//     }

//     private void Start()
//     {
//         StartCoroutine(LoadData());
//     }

//     private IEnumerator LoadData()
//     {
//         yield return new WaitUntil(
//             () => GlobalVariable_Search_Devices.temp_ListDeviceInformationModel.Any() &&
//             GlobalVariable_Search_Devices.temp_ListJBInformationModel.Any()
//         );

//         try
//         {
//             Prepare_Device_Dictionary_For_Searching();

//             Prepare_JB_Dictionary_For_Searching();

//             searchableDropDownView.Initialize();

//             // searchableDropDownView.inputField.onValueChanged.AddListener(OnInputValueChanged);

//             searchableDropDownView.SetInitialTextFieldValue();
//         }
//         catch (Exception e)
//         {
//             Debug.LogWarning(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.InnerException);
//         }
//     }

//     private void InitUIElements()
//     {
//         var content = prefab_Device.transform.Find("Content");
//         scrollRect ??= prefab_Device.GetComponent<ScrollRect>();
//         contentTransform ??= content.GetComponent<RectTransform>();

//         deviceInfo = content.Find("Device_information");
//         code_Value_Text ??= deviceInfo.Find("Code_group/Code_value").GetComponent<TMP_Text>();
//         function_Value_Text ??= deviceInfo.Find("Function_group/Function_value").GetComponent<TMP_Text>();
//         range_Value_Text ??= deviceInfo.Find("Range_group/Range_value").GetComponent<TMP_Text>();
//         io_Value_Text ??= deviceInfo.Find("IO_group/IO_value").GetComponent<TMP_Text>();

//         var jbConnectionGroup = content.Find("JB_Connection_group/JB_Connection_text_group");
//         jb_Connection_Value_Text ??= jbConnectionGroup.Find("JB_Connection_value").GetComponent<TMP_Text>();
//         jb_Connection_Location_Text ??= jbConnectionGroup.Find("JB_Connection_location").GetComponent<TMP_Text>();

//         JB_Connection_Group ??= content.Find("JB_Connection_group").gameObject;
//         JB_Location_Image_Prefab ??= JB_Connection_Group.transform.Find("JB_Location_Image").GetComponent<Image>();
//         JB_Connection_Wiring_Image_Prefab ??= JB_Connection_Group.transform.Find("JB_Connection_Wiring").GetComponent<Image>();
//     }
//     private void Prepare_Device_Dictionary_For_Searching()
//     {
//         var tempListDevice = GlobalVariable_Search_Devices.temp_ListDeviceInformationModelFromModule;

//         deviceDictionary = tempListDevice
//             .GroupBy(device => device.Code.ToLower())
//             .ToDictionary(g => g.Key, g => g.First());

//         foreach (var device in tempListDevice)
//         {
//             var functionKey = device.Function.ToLower();
//             if (!deviceDictionary.ContainsKey(functionKey))
//             {
//                 deviceDictionary.Add(functionKey, device);
//             }
//         }
//     }

//     private void Prepare_JB_Dictionary_For_Searching()
//     {
//         var tempListJB = GlobalVariable_Search_Devices.temp_ListJBInformationModelFromModule;

//         jBDictionary = tempListJB
//             .GroupBy(jb => jb.Name.ToLower())
//             .ToDictionary(g => g.Key, g => g.First());
//     }
//     // private void Prepare_Device_Dictionary_For_Searching()
//     // {
//     //     deviceDictionary = GlobalVariable_Search_Devices.temp_ListDeviceInformationModelFromModule.ToDictionary(d => d.Code);
//     //     var functionDictionary = GlobalVariable_Search_Devices.temp_ListDeviceInformationModelFromModule.ToDictionary(d => d.Function);
//     //     foreach (var keyValuePair in functionDictionary)
//     //     {
//     //         if (deviceDictionary.ContainsKey(keyValuePair.Key))
//     //         {
//     //             deviceDictionary[keyValuePair.Key] = keyValuePair.Value;
//     //         }
//     //         else
//     //         {
//     //             deviceDictionary.Add(keyValuePair.Key, keyValuePair.Value);
//     //         }
//     //     }
//     // }
//     private void OnInputValueChanged(string input)
//     {
//         // if (input.ToLower() == current_Search_Content.ToLower())
//         // {
//         //     return;
//         // }

//         Debug.Log(searchableDropDownView.filter_Type);

//         switch (searchableDropDownView.filter_Type)
//         {
//             case "Device":
//                 if (deviceDictionary.TryGetValue(input.ToLower(), out var device))
//                 {

//                     ClearWiringGroupAndCache();
//                     UpdateDeviceInformation(device);
//                     // current_Search_Content = input.ToLower();
//                 }
//                 break;
//             case "JB/TSD":
//                 if (jBDictionary.TryGetValue(input.ToLower(), out var jB))
//                 {
//                     ClearWiringGroupAndCache();
//                     UpdateJBInformation(jB);
//                     //current_Search_Content = input.ToLower();
//                 }
//                 break;
//         }
//     }
//     private void ClearWiringGroupAndCache()
//     {
//         foreach (Transform child in JB_Connection_Group.transform)
//         {
//             if (child.gameObject != JB_Connection_Wiring_Image_Prefab && child.gameObject.name.Contains("(Clone)"))
//             {
//                 Destroy(child.gameObject);
//             }
//         }
//     }

//     private void UpdateDeviceInformation(DeviceInformationModel device)
//     {
//         if (!deviceInfo.gameObject.activeSelf) deviceInfo.gameObject.SetActive(true);

//         // prefab_Device.name = $"Scroll_Area_{device.Code}";

//         code_Value_Text.text = device.Code;
//         function_Value_Text.text = device.Function;
//         unit_Value_Text.text = device.Unit;
//         range_Value_Text.text = device.Range;
//         io_Value_Text.text = device.IOAddress;

//         jb_Connection_Value_Text.text = $"{device.JBInformationModel.Name}:";
//         jb_Connection_Location_Text.text = device.JBInformationModel.Location;

//         _jbName = jb_Connection_Value_Text.text;

//         GlobalVariable_Search_Devices.jbName = _jbName;

//         //   Debug.Log("JB Name: " + _jbName);

//         _moduleName = device.ModuleInformationModel.Name;

//         GlobalVariable_Search_Devices.moduleName = _moduleName;

//         //   Debug.Log("Module Name: " + _moduleName);


//         if (!string.IsNullOrEmpty(_jbName))
//         {
//             //     Debug.Log("JB Name Prepare For Load Sprite: " + _jbName);

//             // ClearWiringGroupAndCache();
//             LoadDeviceSprites(
//                 list_Additional_Images: device.AdditionalConnectionImages,
//                  jbInformationModel: device.JBInformationModel);
//         }
//     }

//     private void UpdateJBInformation(JBInformationModel jB)
//     {
//         if (deviceInfo.gameObject.activeSelf) deviceInfo.gameObject.SetActive(false);
//         prefab_Device.name = $"Scroll_Area_{jB.Name}";
//         jb_Connection_Value_Text.text = $"{jB.Name}:";
//         jb_Connection_Location_Text.text = jB.Location;
//         _jbName = jb_Connection_Value_Text.text;
//         GlobalVariable_Search_Devices.jbName = _jbName;
//         if (!string.IsNullOrEmpty(_jbName))
//         {
//             LoadDeviceSprites(null, jbInformationModel: jB);

//         }
//     }

//     private async void LoadDeviceSprites(List<ImageInformationModel> list_Additional_Images, JBInformationModel jbInformationModel)
//     {
//         if (jbInformationModel == null || jbInformationModel.ListConnectionImages == null)
//         {
//             Debug.LogError("jbInformationModel hoặc ListConnectionImages bị null!");
//             return;
//         }

//         var total_List_Connection_Images = new List<ImageInformationModel>(jbInformationModel.ListConnectionImages);

//         Debug.Log("Số lượng ảnh ban đầu: " + total_List_Connection_Images.Count);

//         if (list_Additional_Images != null && list_Additional_Images.Count > 0)
//         {
//             total_List_Connection_Images.AddRange(list_Additional_Images);
//         }
//         await Apply_Sprite_JB_Images(jbInformationModel.OutdoorImage, total_List_Connection_Images);
//         if (scrollRect != null)
//         {
//             scrollRect.verticalNormalizedPosition = 1f;
//         }
//         else
//         {
//             Debug.LogWarning("scrollRect bị null, không thể thay đổi vị trí cuộn!");
//         }
//     }


//     private async Task Apply_Sprite_JB_Images(ImageInformationModel outdoorImage, List<ImageInformationModel> listConnectionImages)
//     {
//         ShowProgressBar("Đang tải hình ảnh...", "...");

//         JB_Connection_Wiring_Image_Prefab.gameObject.SetActive(true);
//         var tasks = new List<Task>();

//         if (outdoorImage != null)
//         {
//             if (!string.IsNullOrEmpty(outdoorImage.url))
//             {
//                 AddButtonListener(JB_Location_Image_Prefab, outdoorImage.url);
//                 tasks.Add(searchableDropDownView._presenter.LoadImageAsync(outdoorImage.url, JB_Location_Image_Prefab));
//             }

//         }
//         else
//         {
//             var Noted_Url = "https://firebasestorage.googleapis.com/v0/b/ttc-project-81b04.appspot.com/o/JB_Outdoor_Location%2FJB_TSD_Location_Note.png?alt=media&token=3c18c77a-750d-4d7b-81b5-84f1973f61ba";
//             AddButtonListener(JB_Location_Image_Prefab, Noted_Url);
//             tasks.Add(searchableDropDownView._presenter.LoadImageAsync(Noted_Url, JB_Location_Image_Prefab));
//         }
//         //  Debug.Log("Số lượng ListConnectionImages: " + listConnectionImages.Count);
//         if (listConnectionImages.Any())
//         {
//             foreach (var image in listConnectionImages)
//             {
//                 var newImage = Instantiate(JB_Connection_Wiring_Image_Prefab.gameObject, JB_Connection_Group.transform);
//                 AddButtonListener(newImage.GetComponent<Image>(), image.Name);
//                 tasks.Add(searchableDropDownView._presenter.LoadImageAsync(image.url, newImage.GetComponent<Image>()));
//             }
//         }

//         await Task.WhenAll(tasks);
//         JB_Connection_Wiring_Image_Prefab.gameObject.SetActive(false);

//         ResizeImages();
//         //await TrackProgress(tasks);
//         HideProgressBar();


//     }

//     private void AddButtonListener(Image imagePrefab, string imageName)
//     {
//         var button = imagePrefab.GetComponent<Button>();
//         button.onClick.RemoveAllListeners();
//         button.onClick.AddListener(() => open_Detail_Image.Open_Detail_Canvas(imagePrefab));
//     }

//     //!
//     // private Task LoadImageAsync(string imageName, Image imagePrefab)
//     // {
//     //     string url = $"{GlobalVariable.baseUrl}files/{imageName}";
//     //     return APIManager.Instance.LoadImageFromUrlAsync(url, imagePrefab);
//     // }
//     //!
//     // private Task LoadImageAsync(string imageName, Image imagePrefab)
//     // {
//     //     string url = $"{GlobalVariable.baseUrl}files/{imageName}";
//     //     return APIManager.Instance.LoadImageFromUrlAsync(url, imagePrefab);
//     // }

//     // private async Task ShowProgressBar(string title, string details)
//     // {
//     //     Progress.Show(title, ProgressColor.Blue, true);
//     //     Progress.SetDetailsText(details);
//     //     await Task.Delay(100);
//     // }
//     private void ShowProgressBar(string title, string details)
//     {
//         Progress.Show(title, ProgressColor.Blue, true);
//         Progress.SetDetailsText(details);
//     }

//     private void HideProgressBar()
//     {
//         Progress.Hide();
//     }
//     // private async Task TrackProgress(List<Task> tasks)
//     // {
//     //     int totalTasks = tasks.Count;
//     //     int completedTasks = 0;

//     //     var wrappedTasks = tasks.Select(async task =>
//     //     {
//     //         try
//     //         {
//     //             await task;
//     //         }
//     //         catch (Exception e)
//     //         {
//     //             Debug.LogError($"Error loading image: {e.Message}");
//     //         }
//     //         finally
//     //         {
//     //             completedTasks++;
//     //             Progress.SetProgressValue((float)completedTasks / totalTasks * 100f);
//     //         }
//     //     });
//     //     await Task.WhenAll(wrappedTasks);
//     //     Progress.SetProgressValue(100f);
//     // }

//     private void ResizeImages()
//     {
//         StartCoroutine(Resize_GameObject_Function.Set_NativeSize_For_GameObject(JB_Location_Image_Prefab));

//         foreach (var connectionImage in JB_Connection_Group.GetComponentsInChildren<Image>())
//         {
//             if (connectionImage != JB_Connection_Wiring_Image_Prefab
//             && connectionImage.gameObject.activeSelf
//             && connectionImage.gameObject.name.Contains("(Clone)"))
//                 StartCoroutine(Resize_GameObject_Function.Set_NativeSize_For_GameObject(connectionImage));
//         }
//     }

//     // private async Task HideProgressBar()
//     // {
//     //     await Task.Delay(200);
//     //     Progress.Hide();
//     // }

//     private void SetSprite(Image imageComponent, string jb_name)
//     {
//         if (!spriteCache.TryGetValue(jb_name, out var jbSprite))
//         {
//             spriteCache.TryGetValue("JB_TSD_Location_Note", out jbSprite);
//         }
//         imageComponent.sprite = jbSprite;
//         imageComponent.gameObject.GetComponent<Button>().onClick.AddListener(() => open_Detail_Image.Open_Detail_Canvas(imageComponent));
//         //   Debug.Log("Đã add sự kiện click vào imageComponent");
//         StartCoroutine(Resize_GameObject_Function.Set_NativeSize_For_GameObject(imageComponent));
//     }

//     private void CreateAndSetSprite(string jb_name)
//     {
//         var jbLocationImage = Instantiate(JB_Location_Image_Prefab, JB_Connection_Group.transform);
//         jbLocationImage.transform.SetSiblingIndex(JB_Location_Image_Prefab.transform.GetSiblingIndex() + 1);
//         jbLocationImage.gameObject.SetActive(true);
//         SetSprite(jbLocationImage, jb_name);
//         instantiatedImages.Add(jbLocationImage);
//     }

//     private void ClearInstantiatedImages()
//     {
//         foreach (var img in instantiatedImages)
//         {
//             if (img != null)
//             {
//                 Destroy(img.gameObject);
//             }
//         }
//         instantiatedImages.Clear();
//     }

//     private void ResetResources()
//     {
//         ClearWiringGroupAndCache();
//     }

//     private void HandleOrientationChange(ScreenOrientation newOrientation)
//     {
//         if (newOrientation == ScreenOrientation.Portrait)
//         {
//             bottom_App_Bar.SetActive(true);
//         }
//         else if (newOrientation == ScreenOrientation.LandscapeLeft || newOrientation == ScreenOrientation.LandscapeRight)
//         {
//             bottom_App_Bar.SetActive(false);
//         }
//     }
// }
