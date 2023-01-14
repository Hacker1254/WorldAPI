using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using WorldAPI.ButtonAPI.Groups;

namespace WorldAPI.ButtonAPI.Buttons.Groups
{
    public class GrpToggles
    {
        public VRCToggle tlgOne, tlgTwo;

        public GrpToggles(GameObject menu, string text, string Ontooltip, string OffTooltip, Action<bool> BoolStateChange, 
            string text2, string Ontooltip2, string OffTooltip2, Action<bool> BoolStateChange2, 
            Sprite OnImageSprite = null, Sprite OffImageSprite = null,
            float FirstFontSize = 24f, float SecondFontSize = 24f, bool FirstState = false, bool SecondState = false)
        {
            GameObject Base = new GameObject();
            Base.name = $"DuoToggles";
            Base.transform.parent = menu.transform;
            Base.transform.localRotation = Quaternion.Euler(0, 0, 0);
            Base.transform.localScale = new Vector3(1f, 1f, 1f);
            Base.transform.localPosition = Vector3.zero;
            Base.AddComponent<LayoutElement>();
            GameObject Sub = new GameObject(); // this has a reason! ;3
            Sub.name = $"Toggles_[WorldClient]";
            Sub.transform.parent = Base.transform;
            Sub.transform.localRotation = Quaternion.Euler(0, 0, 0);
            Sub.transform.localScale = new Vector3(1f, 1f, 1f);
            Sub.transform.localPosition = new Vector3(0f, -3f, 0f);


            tlgOne = new VRCToggle(Sub.transform, text, BoolStateChange, FirstState, Ontooltip, OffTooltip, OnImageSprite, OffImageSprite); 
            tlgOne.TurnHalf(new Vector3(0f, 50f, 0f), FirstFontSize);
            tlgTwo = new VRCToggle(Sub.transform, text2, BoolStateChange2, SecondState, Ontooltip2, OffTooltip2, OnImageSprite, OffImageSprite);
            tlgTwo.TurnHalf(new Vector3(0f, -51f, 0f), SecondFontSize);
        }

        public GrpToggles(ButtonGroup btnGrp, string text, string Ontooltip, string OffTooltip, Action<bool> BoolStateChange,
            string text2, string Ontooltip2, string OffTooltip2, Action<bool> BoolStateChange2,
            Sprite OnImageSprite = null, Sprite OffImageSprite = null,
            float FirstFontSize = 24f, float SecondFontSize = 24f, bool FirstState = false, bool SecondState = false) :
            this(btnGrp.gameObject, text, Ontooltip, OffTooltip, BoolStateChange, 
                text2, Ontooltip2, OffTooltip2, BoolStateChange2,
                OnImageSprite, OffImageSprite, FirstFontSize, SecondFontSize, FirstState, SecondState)
        { }

        public GrpToggles(CollapsibleButtonGroup btnGrp, string text, string Ontooltip, string OffTooltip, Action<bool> BoolStateChange,
            string text2, string Ontooltip2, string OffTooltip2, Action<bool> BoolStateChange2,
            Sprite OnImageSprite = null, Sprite OffImageSprite = null,
            float FirstFontSize = 24f, float SecondFontSize = 24f, bool FirstState = false, bool SecondState = false) :
            this(btnGrp.gameObject, text, Ontooltip, OffTooltip, BoolStateChange,
                text2, Ontooltip2, OffTooltip2, BoolStateChange2,
                OnImageSprite, OffImageSprite, FirstFontSize, SecondFontSize, FirstState, SecondState)
        { }
    }
}
