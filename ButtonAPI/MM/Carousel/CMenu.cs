using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WorldAPI.ButtonAPI.Extras;
using Object = UnityEngine.Object;

namespace WorldAPI.ButtonAPI.MM.Carousel;

public class CMenu
{
    internal List<GameObject> chlidren { get; set; } = new(); // This prlly isn't the best way to do this
    public GameObject gameObject { get; private set; }
    public Button buttonCom { get; private set; }
    public Transform transform { get; private set; }
    public TextMeshProUGUI TMProComp { get; private set; }
    public Image ImageComp { get; private set; }
    public VRC.UI.Elements.Tooltips.UiTooltip ToolTip { get; private set; }
    public MMCarousel menu { get; private set; }

    public CMenu(MMCarousel ph, string buttonText, string toolTip = "", Sprite Icon = null) {
        if (!APIBase.IsReady()) throw new Exception();
        if (Icon == null) Icon = APIBase.DefaultButtonSprite;

        gameObject = Object.Instantiate(APIBase.MMMCarouselButtonTemplate, ph.barContents);
        transform = gameObject.transform;
        gameObject.name = buttonText;

        TMProComp = gameObject.transform.Find("Mask/Text_Name").GetComponent<TextMeshProUGUI>();
        TMProComp.text = buttonText;
        TMProComp.richText = true;

        //ToolTip = gameObject.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>();
        //ToolTip.field_Public_String_0 = toolTip;
        //ToolTip.field_Public_String_1 = toolTip;

        buttonCom = gameObject.GetComponent<Button>();
        buttonCom.onClick = new();
        buttonCom.onClick.AddListener(new Action(() => { // Once more, theres Prlly a better way to do this
            ph.menuContents.GetChildren().ForEach(a => a.SetActive(false));
            chlidren.ForEach(a => a.SetActive(true));
        }));
        ImageComp = gameObject.transform.Find("Icon").GetComponent<Image>();
        if (Icon != null)
            ImageComp.sprite = Icon;
        else ImageComp.gameObject.SetActive(false);
        menu = ph;
    }
}