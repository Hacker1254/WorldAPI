using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Core.Styles;
using WorldAPI.ButtonAPI.Controls;
using WorldAPI.ButtonAPI.Extras;
using WorldAPI.ButtonAPI.Groups;
using Object = UnityEngine.Object;

namespace WorldAPI.Buttons
{
    public class VRCButton : ExtentedControl
    {
        public Transform transform;

        public VRCButton(Transform menu, string text, string tooltip, Action listener, bool Half = false, bool SubMenuIcon = false, Sprite Icon = null, HalfType Type = HalfType.Normal, bool IsGroup = false) {
            if (!APIBase.IsReady()) { Logs.Error("Error, Something Was Missing!"); return; }

            if (Icon == null)
                Icon = APIBase.DefaultButtonSprite;
            if (menu != null)
                APIBase.LastButtonParent = menu;
            else if (menu == null && APIBase.LastButtonParent == null)
                menu = APIBase.Button.parent;
            else if (menu == null && APIBase.LastButtonParent != null)
                menu = APIBase.LastButtonParent;

            transform = Object.Instantiate(APIBase.Button, menu);
            gameObject = transform.gameObject;

            transform.gameObject.SetActive(true);
            TMProCompnt = transform.GetComponentInChildren<TextMeshProUGUI>();
            Text = text;
            TMProCompnt.text = text;
            TMProCompnt.richText = true;
            ButtonCompnt = transform.GetComponent<Button>();
            ButtonCompnt.onClick = new Button.ButtonClickedEvent();
            onClickAction = listener;
            ButtonCompnt.onClick.AddListener(new Action(() => APIBase.SafelyInvolk(onClickAction, Text)));

            ImgCompnt = transform.transform.Find("Icon").GetComponent<Image>();
            ImgCompnt.gameObject.GetComponent<StyleElement>().enabled = false; // Fix the Images from going back to the default

            if (Icon != null)
                ImgCompnt.sprite = Icon;
            else {
                transform.transform.Find("Icon").gameObject.active = false;
                ResetTextPox();
            }
            Object.Destroy(transform.transform.Find("Icon_Secondary").gameObject);
            if (gameObject.GetComponent<VRC.UI.Elements.Analytics.AnalyticsController>() != null) Object.Destroy(gameObject.GetComponent<VRC.UI.Elements.Analytics.AnalyticsController>());
            gameObject.transform.Find("Badge_MMJump").gameObject.SetActive(SubMenuIcon);
            this.SetToolTip(tooltip);
            if (Half) TurnHalf(Type, IsGroup);
            if (!string.IsNullOrEmpty(APIBase.autoColorHex)) RecolorBackGrn(APIBase.autoColorHex);
            transform.name = text;
        }


        public VRCButton(ButtonGroup buttonGroup, string text, string tooltip, Action click, bool Half = false, bool subMenuIcon = false, Sprite icon = null, HalfType Type = HalfType.Normal, bool IsGroup = false) 
            : this(buttonGroup.transform, text, tooltip, click, Half, subMenuIcon, icon, Type, IsGroup)
        {
        }

        public VRCButton(CollapsibleButtonGroup buttonGroup, string text, string tooltip, Action click, bool Half = false, bool subMenuIcon = false, Sprite icon = null, HalfType Type = HalfType.Normal, bool IsGroup = false)
            : this(buttonGroup.buttonGroup.transform, text, tooltip, click, Half, subMenuIcon, icon, Type, IsGroup)
        {
        }
    }
}
