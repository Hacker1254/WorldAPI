using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements.Controls;
using WorldAPI.ButtonAPI.Controls;
using WorldAPI.ButtonAPI.Extras;
using Object = UnityEngine.Object;

namespace WorldAPI.ButtonAPI.Wing.Controls;

public class WingBtnControls : Root
{
    public Transform transform { get; internal set; }
    public Button ButtonCompnt { get; internal set; }
    public Image ImgCompnt { get; internal set; }
    public Action onClickAction { get; internal set; }
    public TextMeshProUGUIEx HeaderTMProCompnt { get; internal set; }

    public void RecolorBackGrn(string hexColor, string path = "Container/Background")
    {
        var bg = gameObject.transform.Find(path);

        if (bg.transform.parent.Find("Bg") == null)
        { // In case if we have already Colored it
            var Btn = Object.Instantiate(bg.gameObject, bg.transform.parent);
            Btn.name = "Bg";
            Btn.GetComponent<RectTransform>().SetSiblingIndex(1);
            bg.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
            bg.gameObject.active = false;
        }

        Image component3 = bg.transform.parent.Find("Bg").GetComponent<Image>();
        component3.color = QMUtils.HexToColor(hexColor); // new Color(0.18f, 0.18f, 0.18f, 1f);
    }
}