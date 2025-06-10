using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GrapperInfor : MonoBehaviour
{
    public Button grapperButton;
    public TMP_Text Name;

    public void SetGrapperName(GrapperInformationModel grapper)
    {
        Name.text = grapper.Name;
    }
}
