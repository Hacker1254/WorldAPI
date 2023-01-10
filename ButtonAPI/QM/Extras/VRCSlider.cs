using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using VRC.UI.Elements.Controls;
using WorldAPI.ButtonAPI.Controls;
using WorldAPI.ButtonAPI.Extras;
using Object = UnityEngine.Object;

namespace WorldAPI.ButtonAPI.QM.Extras;

public class VRCSlider : Root
{
    public Transform transform;
    private TextMeshProUGUI PercentComp;

    public VRCSlider(Transform menu, string text, string tooltip, Action<float> listener, float defaultValue = 0f) {

        if (!APIBase.IsReady()) { Logs.Error("Error, Something Was Missing!"); return; }

        if (menu != null)
            APIBase.LastButtonParent = menu;
        else if (menu == null && APIBase.LastButtonParent == null)
            menu = APIBase.Button.parent;
        else if (menu == null && APIBase.LastButtonParent != null)
            menu = APIBase.LastButtonParent;

        transform = Object.Instantiate(APIBase.Slider, menu);
        gameObject = transform.gameObject;
        transform.localPosition = Vector3.zeroVector;

        transform.gameObject.SetActive(true);
        PercentComp = transform.Find("Text_CurrentValue").GetComponent<TextMeshProUGUIEx>();
        QMUtils.RemoveUnknownComps(transform.Find("Text_CurrentValue").gameObject);
        TMProCompnt = transform.Find("Text_Name").GetComponent<TextMeshProUGUIEx>();
        TMProCompnt.text = text;
        TMProCompnt.transform.localPosition = new Vector3(-386.4344f, 24.1347f, 0);
        var tip = gameObject.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>();
        tip.field_Public_String_0 = tooltip;
        tip.field_Public_String_1 = tooltip;
        var layOut = gameObject.GetComponent<UnityEngine.UI.LayoutElement>();
        layOut.preferredHeight = 111;

    }
}