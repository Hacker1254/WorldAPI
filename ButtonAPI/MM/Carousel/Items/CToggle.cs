using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;


namespace WorldAPI.ButtonAPI.MM.Carousel.Items;

public class CToggle
{
    public GameObject gameObject { get; private set; }
    public Transform transform { get; private set; }
    public TextMeshProUGUI TMProComp { get; private set; }
    public Toggle ToggleCompnt { get; private set; }
    public string Text { get; private set; }
    internal Action<bool> Listener { get; set; }
    public VRC.UI.Elements.Tooltips.UiTooltip ToolTip { get; private set; }

    public CToggle(CGrp grp, string text, Action<bool> stateChange, bool defaultState = false, string toolTip = "") {
        if (!APIBase.IsReady()) throw new Exception();

        gameObject = Object.Instantiate(APIBase.MMCTgl, grp.gameObject.transform);
        transform = gameObject.transform;
        gameObject.name = text;

        TMProComp = transform.Find("LeftItemContainer/Text_MM_H3").GetComponent<TextMeshProUGUI>();
        TMProComp.text = text;
        TMProComp.richText = true;
        Text = text;

        //ToolTip = gameObject.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>();
        //ToolTip.field_Public_String_0 = toolTip;
        //ToolTip.field_Public_String_1 = toolTip;

        ToggleCompnt = gameObject.GetComponent<Toggle>();
        ToggleCompnt.onValueChanged = new();
        ToggleCompnt.isOn = defaultState;
        Listener = stateChange;
        ToggleCompnt.onValueChanged.AddListener(new Action<bool>((val) => {
            APIBase.SafelyInvolk(val, Listener, text);
            APIBase.Events.onCToggleValChange?.Invoke(this, val);
        }));
        transform.Find("Background_Separator").gameObject.active = false;
    }

    public void SoftSetState(bool value)
    {
        ToggleCompnt.onValueChanged = new();
        ToggleCompnt.isOn = value;
        ToggleCompnt.onValueChanged.AddListener(new Action<bool>((val) => {
            APIBase.SafelyInvolk(val, Listener, Text);
            APIBase.Events.onCToggleValChange?.Invoke(this, val);
        }));
    }
}
