using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Press_PDF_Open_Online_Catalog : MonoBehaviour
{
    [SerializeField]
    private List<Button> listButton = new List<Button>();

    [SerializeField]
    private string[] urls;

    [SerializeField]
    private bool open_module_adapter_Catalog = false;

    private string adapter_Name;
    private string module_Name;
    private Dictionary<string, string> online_Adapter_Catalog_Url;
    private Dictionary<string, string> online_Module_Catalog_Url;
    void OnEnable()
    {
        adapter_Name = GlobalVariable.apdapter_Type_Name;
        module_Name = GlobalVariable.module_Type_Name;
        online_Adapter_Catalog_Url = GlobalVariable.online_Adapter_Catalog_Url;
        online_Module_Catalog_Url = GlobalVariable.online_Module_Catalog_Url;

        if (open_module_adapter_Catalog)
        {
            listButton.Add(GameObject.Find("Module_Button_Detail_Datasheet_View").GetComponent<Button>());
            listButton.Add(GameObject.Find("Adapter_Button_Detail_Datasheet_View").GetComponent<Button>());
            listButton[0].onClick.AddListener(() => Open_Module_Adapter_Online_Catalog(true, false));
            listButton[1].onClick.AddListener(() => Open_Module_Adapter_Online_Catalog(false, true));
        }
        else
        {
            foreach (var button in listButton)
            {
                int index = listButton.IndexOf(button);
                button.onClick.AddListener(() => Open_url(index));
            }
        }
    }

    private void Open_Module_Adapter_Online_Catalog(bool open_module_Catalog, bool open_adapter_Catalog)
    {
        if (open_module_Catalog && !open_adapter_Catalog)
        {
            Application.OpenURL(online_Module_Catalog_Url[module_Name]);
        }
        else if (!open_module_Catalog && open_adapter_Catalog)
        {
            Application.OpenURL(online_Adapter_Catalog_Url[adapter_Name]);
        }
    }
    private void Open_url(int index)
    {
        Application.OpenURL(urls[index]);
    }

    void OnDisable()
    {
        listButton.RemoveAll(button => button != null);
    }
    void Start()
    {
        if (UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI)
            UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;
    }
}
