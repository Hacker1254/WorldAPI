using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using WorldAPI.ButtonAPI.Controls;
using WorldAPI.ButtonAPI.MM;
using WorldAPI.ButtonAPI.Extras;
using Object = UnityEngine.Object;

namespace WorldAPI.ButtonAPI.Groups;

public class CollapsibleButtonGroup : Root
{
    public bool IsOpen;
    public GameObject headerObj;
    public Transform InfoButton;
    public ButtonGroup buttonGroup;

    public CollapsibleButtonGroup(Transform parent, string text, bool openByDefault = true)
    {
        if (!APIBase.IsReady()) throw new Exception();

        headerObj = Object.Instantiate(APIBase.ColpButtonGrp, parent);
        headerObj.name = $"{text}_CollapsibleButtonGroup";

        TMProCompnt = headerObj.transform.Find("Label").GetComponent<TMPro.TextMeshProUGUI>();
        TMProCompnt.richText = true;
        TMProCompnt.text = text;

        buttonGroup = new(parent, string.Empty, true);
        gameObject = buttonGroup.gameObject;

        InfoButton = headerObj.transform.Find("InfoButton");
        var foldout = headerObj.transform.Find("Background_Button").GetComponent<Toggle>();
        foldout.onValueChanged = new();
        foldout.isOn = openByDefault;
        foldout.onValueChanged.AddListener(new Action<bool>(val =>{
            buttonGroup.gameObject.SetActive(val);
            IsOpen = val;
        }));
    }


    /// <summary>
    ///  Remove Buttons, Toggles, anything that was put on this ButtnGrp
    /// </summary>
    public void RemoveAllChildren() =>
        buttonGroup.gameObject.transform.DestroyChildren();


    public CollapsibleButtonGroup(VRCPage page, string text, bool openByDefault = false) : this(page.menuContents, text, openByDefault) { }
    public CollapsibleButtonGroup(MMPage page, string text, bool openByDefault = false) : this(page.menuContents, text, openByDefault) { }
}