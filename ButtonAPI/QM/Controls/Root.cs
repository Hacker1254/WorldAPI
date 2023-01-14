using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WorldAPI.ButtonAPI.Buttons;
using WorldAPI.ButtonAPI.Buttons.Groups;
using WorldAPI.ButtonAPI.MM;
using WorldAPI.Buttons;
using WorldLoader.HookUtils;

namespace WorldAPI.ButtonAPI.Controls
{
    public class Root
    {
        public GameObject gameObject { get; internal set; }
        public TextMeshProUGUI TMProCompnt { get; internal set; }


        internal string Text;

        public void SetActive(bool Active) =>
            gameObject.SetActive(Active);

        public void SetTextColor(Color color) =>
            TMProCompnt.color = color;

        public void SetTextColor(string Hex) =>
            TMProCompnt.text = $"<color={Hex}>{Text}</color>";

        public void SetRotation(Vector3 Poz) =>
            gameObject.transform.localRotation = Quaternion.Euler(Poz);

        public void SetPostion(Vector3 Poz) =>
            gameObject.transform.localPosition = Poz;

        public GameObject GetGameObject() =>
            gameObject;

        public Transform GetTransform() =>
            gameObject.transform;

        public Transform ChangeParent(GameObject newParent) =>
            gameObject.transform.parent = newParent.transform;

        public Transform AlsoAddToMM(MMPage pg) =>
            GameObject.Instantiate(gameObject.transform, pg.menuContents);

        public VRCButton AddButton(string text, string tooltip, Action listener, bool Half = false, bool SubMenuIcon = false, Sprite Icon = null) =>
            new VRCButton(gameObject.transform, text, tooltip, listener, Half, SubMenuIcon, Icon);

        public VRCToggle AddToggle(string Ontext, Action<bool> listener, bool DefaultState = false, string OffTooltip = null, string OnToolTip = null,
            Sprite onSprite = null, Sprite offSprite = null, bool Half = false) =>
            new VRCToggle(gameObject.transform, Ontext, listener, DefaultState, OffTooltip, OnToolTip, onSprite, offSprite, Half);

        public VRCLable AddLable(string text, string LowerText, Action onClick = null, bool Bg = true) =>
            new VRCLable(gameObject.transform, text, LowerText, onClick, Bg);

        public GrpButtons AddGrpOfButtons(string FirstName, string FirstTooltip, Action action,
                                            string SecondName = null, string SecondTooltip = null, Action Secondaction = null,
                                            string thirdName = null, string thirdTooltip = null, Action thirdaction = null) => 
                new GrpButtons(gameObject, FirstName, FirstTooltip, action,
                    SecondName, SecondTooltip, Secondaction,
                    thirdName, thirdTooltip, thirdaction);

        public GrpToggles AddGrpToggles(string text, string Ontooltip, string OffTooltip, Action<bool> BoolStateChange,
            string text2, string Ontooltip2, string OffTooltip2, Action<bool> BoolStateChange2,
            Sprite OnImageSprite = null, Sprite OffImageSprite = null,
            float FirstFontSize = 24f, float SecondFontSize = 24f, bool FirstState = false, bool SecondState = false) =>
            new GrpToggles(gameObject, text, Ontooltip, OffTooltip, BoolStateChange,
                text2, Ontooltip2, OffTooltip2, BoolStateChange2,
                OnImageSprite, OffImageSprite, FirstFontSize, SecondFontSize, FirstState, SecondState);

        public DuoButtons AddDuoButtons(string buttonOne, string buttonOneTooltip, Action btnAction, string buttonTwo, string buttonTwoTooltip, Action buttonTwoAction) =>
            new DuoButtons(gameObject, buttonOne, buttonOneTooltip, btnAction,
                buttonTwo, buttonTwoTooltip, buttonTwoAction);
        
    }
}
