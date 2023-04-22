using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WorldAPI.ButtonAPI.Extras;
using WorldAPI.ButtonAPI.Wing;
using WorldAPI.ButtonAPI.Wing.Buttons;
using WorldLoader.HookUtils;
using static WorldAPI.APIBase;
using Object = UnityEngine.Object;

namespace WorldAPI.ButtonAPI.Controls
{
    public class ExtentedControl : Root
    {
        public Button ButtonCompnt { get; internal set; }
        public Image ImgCompnt { get; internal set; }
        public Action onClickAction { get; internal set; }
        public string ToolTip { get; internal set; }

        public string SetToolTip(string tip)
        {
            int i = 0;
            var tooltip = gameObject.GetComponentsInChildren<VRC.UI.Elements.Tooltips.UiTooltip>();
            foreach (var s in tooltip) {
                s.Method_Public_UiTooltip_String_0(tip);
                i++;
                s.enabled = !string.IsNullOrEmpty(tip) && i == 1;
            }
            ToolTip = tip;
            return tip;
        }

       public void SetSprite(Sprite sprite) 
            => ImgCompnt.sprite = sprite;

        public Sprite GetSprite()
            => ImgCompnt.sprite;

        public void ShowSubMenuIcon(bool state) => 
            gameObject.transform.Find("Badge_MMJump").gameObject.SetActive(state);

        public void SetIconColor(Color color) =>
            ImgCompnt.color = color;

        public void SetAction(Action newAction)
        {
            ButtonCompnt.onClick = new Button.ButtonClickedEvent();
            ButtonCompnt.onClick.AddListener(newAction);
        }

        public void SetBackgroundImage(Sprite newImg) // Idea From WTFBlaze <3
        {
            gameObject.transform.Find("Background").GetComponent<Image>().sprite = newImg;
            gameObject.transform.Find("Background").GetComponent<Image>().overrideSprite = newImg;
            if (gameObject.transform.Find("Bg") != null)
            {
                gameObject.transform.Find("Bg").GetComponent<Image>().sprite = newImg;
                gameObject.transform.Find("Bg").GetComponent<Image>().overrideSprite = newImg;
            }
            gameObject.active = false;
            gameObject.active = true;
        }

        public void RecolorBackGrn(string hexColor) // Code used from WCv2 Recolor Eng
        {
            var bg = gameObject.transform.Find("Background");
            var Btn = Object.Instantiate(bg.gameObject, bg.transform.parent);
            Btn.name = "Bg";
            Btn.GetComponent<RectTransform>().SetSiblingIndex(1);
            Image component3 = Btn.GetComponent<Image>();
            component3.color = QMUtils.HexToColor(hexColor); 
            bg.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
            bg.gameObject.active = false;
        }

        internal void ResetTextPox() => gameObject.transform.Find("Text_H4").transform.localPosition = new Vector3(0, 0, 0);

        public void TurnHalf(HalfType Type, bool IsGroup) {
            ImgCompnt.gameObject.active = false;
            var JmpPoz = gameObject.transform.Find("Badge_MMJump").localPosition;
            gameObject.transform.Find("Badge_MMJump").localPosition = new Vector3(JmpPoz.x, JmpPoz.y - 34, JmpPoz.z);
            if (IsGroup)
                ChangeBoth(new Vector2(0, -115));
            else
                ChangeBoth(new Vector2(0, -80));
            TMProCompnt.fontSize = 22;
            switch (Type)
            {
                case HalfType.Top:
                    ImgCompnt.transform.localPosition = new Vector3(0f, 0f, 0f);
                    gameObject.transform.Find("Text_H4").transform.localPosition = new Vector3(0, 22, 0);
                    ChangeBoth(new Vector3(0, 53, 0));
                    break;
                case HalfType.Normal:
                    ImgCompnt.transform.localPosition = new Vector3(0f, 0f, 0f);
                    gameObject.transform.Find("Text_H4").transform.localPosition = new Vector3(0, -22, 0);
                    break;
                case HalfType.Bottom:
                    ImgCompnt.transform.localPosition = new Vector3(0f, 0f, 0f);
                    gameObject.transform.Find("Text_H4").transform.localPosition = new Vector3(0, -69.9f, 0);
                    ChangeBoth(new Vector3(0, -53, 0));
                    break;
            }

        }

        public WButton CopyToWing(WPage pg) => new(pg, Text, onClickAction, ToolTip);
        public WButton CopyToWing(WingSide WingSide) => new(WingSide, Text, onClickAction, ToolTip);

        private void ChangeBoth(Vector2 vec2) {
            gameObject.transform.Find("Background").GetComponent<RectTransform>().sizeDelta = vec2;
            if (gameObject.transform.Find("Bg") !=null) gameObject.transform.Find("Bg").GetComponent<RectTransform>().sizeDelta = vec2;
        }

        private void ChangeBoth(Vector3 vec)
        {
            gameObject.transform.Find("Background").localPosition = vec;
            if (gameObject.transform.Find("Bg") != null) gameObject.transform.Find("Bg").localPosition = vec;
        }

        public enum HalfType
        {
            Top,
            Normal,
            Bottom
        }
    }
}
