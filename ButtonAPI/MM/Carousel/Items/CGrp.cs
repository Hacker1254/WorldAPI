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

    public CGrp(CMenu menu, string text)
    {
        if (!APIBase.IsReady()) throw new Exception();

        transform = Object.Instantiate(APIBase.MMBtnGRP, menu.menu.menuContents).transform;
        transform.name = "BtnGrp_" + text;
        gameObject = transform.Find("Settings_Panel_1/VerticalLayoutGroup").gameObject;
        gameObject.DestroyChildren(a => a.name != "Background_Info");

        TMProComp = transform.Find("Header/LeftItemContainer/Text_Title").GetComponent<TextMeshProUGUI>();
        TMProComp.text = text;
        menu.chlidren.Add(transform.gameObject);
    }
}