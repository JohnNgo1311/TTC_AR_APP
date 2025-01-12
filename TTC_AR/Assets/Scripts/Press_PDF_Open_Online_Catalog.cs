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

    private string adapterSpecificationCode;
    private string moduleSpecificationCode;
    public Dictionary<string, string> online_Adapter_Catalog_Url = new Dictionary<string, string>();
    public Dictionary<string, string> online_Module_Catalog_Url = new Dictionary<string, string>();

    void OnEnable()
    {
        adapterSpecificationCode = GlobalVariable.temp_AdapterSpecificationModel.Code;
        moduleSpecificationCode = GlobalVariable.temp_ModuleSpecificationModel.Code;

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
            Application.OpenURL(online_Module_Catalog_Url[moduleSpecificationCode]);
        }
        else if (!open_module_Catalog && open_adapter_Catalog)
        {
            Application.OpenURL(online_Adapter_Catalog_Url[adapterSpecificationCode]);
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

    }
}
