using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Core.Styles;
using VRC.UI.Elements.Controls;
using WorldAPI.ButtonAPI.Wing.Controls;
using static WorldAPI.APIBase;
using Object = UnityEngine.Object;

namespace WorldAPI.ButtonAPI.Wing.Buttons;

public class WButton : WingBtnControls
{
    public WButton(WPage menu, string buttonName, Action listener, string toolTip, Sprite Icon = null, bool SubMenu = false, string Header = "") {
        if (!APIBase.IsReady()) throw new Exception();

        gameObject = Object.Instantiate(APIBase.WBtnPgTemplate, menu.menuContents);
        transform = gameObject.transform;

        transform.gameObject.SetActive(true);
        Object.Destroy(transform.Find("Text_Status"));
        TMProCompnt = Object.Instantiate(transform.Find("Text_Header"), transform.Find("Text_Header").parent).GetComponent<TextMeshProUGUIEx>();
        TMProCompnt.text = buttonName;
        TMProCompnt.fontSize = 28;
        Text = buttonName;
        TMProCompnt.transform.localPosition = new Vector3(-82, 0, 0);
        HeaderTMProCompnt = transform.Find("Text_Header").GetComponent<TextMeshProUGUIEx>();
        HeaderTMProCompnt.text = Header;
        HeaderTMProCompnt.richText = true;
        ButtonCompnt = transform.GetComponent<Button>();
        ButtonCompnt.onClick = new Button.ButtonClickedEvent();
        onClickAction = listener;
        ButtonCompnt.onClick.AddListener(new Action(() => APIBase.SafelyInvolk(onClickAction, Text)));

        ImgCompnt = transform.Find("Icon_Status").GetComponent<UIWidgets.ImageAdvanced>();
        ImgCompnt.gameObject.GetComponent<StyleElement>().enabled = false; // Fix the Images from going back to the default

        if (Icon != null)
            ImgCompnt.sprite = Icon;
        else {
            transform.transform.Find("Icon_Status").gameObject.active = false;
            TMProCompnt.transform.localPosition = new Vector3(-111, 0, 0);
        }

        gameObject.transform.Find("Icon_JumpToMM").gameObject.active = SubMenu;
        if (!string.IsNullOrEmpty(APIBase.autoColorHex)) RecolorBackGrn(APIBase.autoColorHex, "Background");
    }

    public WButton(WingSide WingSide, string buttonName, Action listener, string toolTip, Sprite Icon = null, bool Separator = false) {
        if (!APIBase.IsReady()) throw new Exception();

        if (WingSide == WingSide.Left)
            gameObject = Object.Instantiate(APIBase.WBtnTemplate, APIBase.WLDefMenu.transform);
        else gameObject = Object.Instantiate(APIBase.WBtnTemplate, APIBase.WRDefMenu.transform);
        transform = gameObject.transform;

        transform.gameObject.SetActive(true);
        TMProCompnt = transform.GetComponentInChildren<TextMeshProUGUI>();
        Text = buttonName;
        TMProCompnt.text = buttonName;
        ButtonCompnt = transform.GetComponent<Button>();
        ButtonCompnt.onClick = new Button.ButtonClickedEvent();
        onClickAction = listener;
        ButtonCompnt.onClick.AddListener(new Action(() => APIBase.SafelyInvolk(onClickAction, Text)));

        ImgCompnt = transform.transform.Find("Container/Icon").GetComponent<UIWidgets.ImageAdvanced>();
        ImgCompnt.gameObject.GetComponent<StyleElement>().enabled = false; // Fix the Images from going back to the default

        if (Icon != null)
            ImgCompnt.sprite = Icon;
        else
            transform.transform.Find("Container/Icon").gameObject.active = false;

        if (!string.IsNullOrEmpty(APIBase.autoColorHex)) RecolorBackGrn(APIBase.autoColorHex);
    }
}
