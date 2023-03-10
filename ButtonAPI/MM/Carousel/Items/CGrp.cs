using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WorldAPI.ButtonAPI.Extras;
using WorldAPI.ButtonAPI.Groups;
using Object = UnityEngine.Object;

namespace WorldAPI.ButtonAPI.MM.Carousel.Items;

public class CGrp
{
    public GameObject gameObject { get; private set; }
    public Transform transform { get; private set; }
    public TextMeshProUGUI TMProComp { get; private set; }
    public Toggle Togl { get; private set; }
    public bool IsOpen { get; private set; }

    public CGrp(CMenu menu, string text, bool defaultState = true)
    {
        if (!APIBase.IsReady()) throw new Exception();

        transform = Object.Instantiate(APIBase.MMBtnGRP, menu.menu.menuContents).transform;
        transform.name = "BtnGrp_" + text;
        gameObject = transform.Find("Settings_Panel_1/VerticalLayoutGroup").gameObject;
        gameObject.DestroyChildren(a => a.name != "Background_Info");

        TMProComp = transform.Find("MM_Foldout/Label").GetComponent<TextMeshProUGUI>();
        TMProComp.text = text;
        TMProComp.richText = true;

        Togl = transform.Find("MM_Foldout/Background_Button").GetComponent<Toggle>();
        Togl.onValueChanged = new();
        Togl.isOn = defaultState;
        Togl.onValueChanged.AddListener(new Action<bool>(val => {
            gameObject.SetActive(val);
            IsOpen = val;
        }));

        menu.chlidren.Add(transform.gameObject);
    }
}