using I2.Loc;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WorldAPI.ButtonAPI.Buttons;
using static WorldAPI.APIBase;

namespace WorldAPI.ButtonAPI.Controls
{
    public class ToggleControl : Root
    {
        public Toggle ToggleCompnt { get; internal set; }
        internal Action<bool> Listener { get; set; }
        public Image OnImage { get; internal set; }
        public Image OffImage { get; internal set; }
        internal static VRCToggle inst { get; set; }


    public bool State
        {
            get => ToggleCompnt.isOn;
            set => ToggleCompnt.isOn = value;
        }

        public void SetAction(Action<bool> newAction) {
            ToggleCompnt.onValueChanged = new Toggle.ToggleEvent();
            ToggleCompnt.onValueChanged.AddListener(newAction);
        }

        public void SoftSetState(bool value) {
            ToggleCompnt.onValueChanged = new();
            ToggleCompnt.isOn = value;
            ToggleCompnt.onValueChanged.AddListener(new Action<bool>((val) => {
                APIBase.SafelyInvolk(val, Listener, Text);
                Events.onVRCToggleValChange?.Invoke(inst, val);
            }));
        }

        public (Sprite, Sprite) SetImages(Sprite onSprite = null, Sprite offSprite = null) {
            OffImage.sprite = offSprite;
            OnImage.sprite = onSprite;
            return (onSprite, offSprite);
        }

        public void SetInteractable(bool val) => ToggleCompnt.interactable = val;

        public (Sprite, Sprite) SetImages(bool checkForNull, Sprite onSprite = null, Sprite offSprite = null)
        {
            if (checkForNull) {
                if (onSprite == null) onSprite = APIBase.OnSprite;
                if (offSprite == null) offSprite = APIBase.OffSprite;
            }
            if (offSprite != null)
                OffImage.sprite = offSprite;
            if (onSprite != null)
                OnImage.sprite = onSprite;
            return (onSprite, offSprite);
        }

        public void TurnHalf(Vector3 TogglePoz, float FontSize = 24f) {
            gameObject.transform.localPosition = TogglePoz;
            TurnHalf(FontSize);
        }

        public void TurnHalf(float FontSize = 24f) {
            OnImage.transform.localScale = new Vector3(0.86f, 0.86f, 0.86f);
            OnImage.transform.localPosition = new Vector3(-52.22f, 30.18f, 0f);
            OnImage.gameObject.SetActive(ToggleCompnt.isOn);
            OffImage.transform.localScale = new Vector3(0.86f, 0.86f, 0.86f);
            OffImage.transform.localPosition = new Vector3(-52.22f, 30.18f, 0f);
            OffImage.gameObject.SetActive(!ToggleCompnt.isOn);

            TMProCompnt.fontSize = FontSize;
            TMProCompnt.transform.localPosition = new Vector3(34.42f, -22, 0);
            TMProCompnt.GetComponent<RectTransform>().sizeDelta = new Vector2(100f, 50f);
            gameObject.transform.Find("Background").GetComponent<RectTransform>().sizeDelta = new Vector2(0, -80);
            if (gameObject.transform.Find("Bg") != null)
                gameObject.transform.Find("Bg").GetComponent<RectTransform>().sizeDelta = new Vector2(0, -80);
            ToggleCompnt.onValueChanged.AddListener(new Action<bool>((val) => { // Adding Listener, so we dont have to reset it
                OffImage.gameObject.active = !val;
                OnImage.gameObject.active = val;
            }));

        }

        public string SetToolTip(string tip, string tip2)
        {
            var TipComp = gameObject.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>();
            GameObject.Destroy(TipComp);
            TipComp = gameObject.AddComponent<VRC.UI.Elements.Tooltips.UiToggleTooltip>();
            TipComp.Method_Public_UiTooltip_String_1(tip2);
            TipComp.Method_Public_UiTooltip_String_0(tip);
            return tip;
        }
    }
}
