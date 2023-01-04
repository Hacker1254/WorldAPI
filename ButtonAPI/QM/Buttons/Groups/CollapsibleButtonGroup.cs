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
    public static Action ActionButto { get; internal set; }

    public CollapsibleButtonGroup(Transform parent, string text, bool openByDefault = false,
        bool MoreActionButton = false, string ActionButtontext = null, Action MoreActionButtonAction = null)
    {
        if (!APIBase.IsReady()) throw new Exception();

        headerObj = Object.Instantiate(APIBase.ColpButtonGrp, parent);
        headerObj.name = $"{text}_CollapsibleButtonGroup";

        TMProCompnt = headerObj.transform.Find("Label").GetComponent<TMPro.TextMeshProUGUI>();
        TMProCompnt.text = text;

        buttonGroup = new(parent, string.Empty, true);
        gameObject = buttonGroup.gameObject;
        var foldout = headerObj.GetComponent<FoldoutToggle>();

        InfoButton = headerObj.transform.Find("InfoButton");
        ActionButto = MoreActionButtonAction;
        MoreActionsButton(MoreActionButton, ActionButtontext, MoreActionButtonAction);
        foldout.field_Private_String_0 = "ButtonGroup";

        foldout.field_Private_Action_1_Boolean_0 = new Action<bool>(val =>
        {
            buttonGroup.gameObject.SetActive(val);
            IsOpen = val;
        });
    }

    public void MoreActionsButton(bool enabled, string text, Action action) {
        if (ActionButto != null && action == null) action = ActionButto;
        ActionButto = action;
        InfoButton.gameObject.active = enabled;
        InfoButton.Find("Text_MM_H3").GetComponent<TMPro.TextMeshProUGUI>().text = text;
        InfoButton.GetComponentInChildren<Button>().onClick = new();
        InfoButton.GetComponentInChildren<Button>().onClick.AddListener(action);
    }


    /// <summary>
    ///  Remove Buttons, Toggles, anything that was put on this ButtnGrp
    /// </summary>
    public void RemoveAllChildren() =>
        buttonGroup.gameObject.transform.DestroyChildren();


    public CollapsibleButtonGroup(VRCPage page, string text, bool openByDefault = false) : this(page.menuContents, text, openByDefault) { }
    public CollapsibleButtonGroup(MMPage page, string text, bool openByDefault = false) : this(page.menuContents, text, openByDefault) { }
}