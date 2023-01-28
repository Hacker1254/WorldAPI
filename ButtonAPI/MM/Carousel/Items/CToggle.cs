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
    public Toggle toggleCom { get; private set; }
    public VRC.UI.Elements.Tooltips.UiTooltip ToolTip { get; private set; }

    public CToggle(CGrp grp, string text, Action<bool> stateChange, bool defaultState = false, string toolTip = "") {
        if (!APIBase.IsReady()) throw new Exception();

        gameObject = Object.Instantiate(APIBase.MMCTgl, grp.gameObject.transform);
        transform = gameObject.transform;
        gameObject.name = text;

        TMProComp = transform.Find("LeftItemContainer/Text_MM_H3").GetComponent<TextMeshProUGUI>();
        TMProComp.text = text;

        ToolTip = gameObject.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>();
        ToolTip.field_Public_String_0 = toolTip;
        ToolTip.field_Public_String_1 = toolTip;

        toggleCom = gameObject.GetComponent<Toggle>();
        toggleCom.onValueChanged = new();
        toggleCom.isOn = defaultState;
        toggleCom.onValueChanged.AddListener(new Action<bool>((val) => APIBase.SafelyInvolk(val, stateChange, text)));

        transform.Find("Background_Separator").gameObject.active = false;
    }
}
