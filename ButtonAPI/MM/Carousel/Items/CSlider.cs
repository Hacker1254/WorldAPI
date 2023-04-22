using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WorldAPI.ButtonAPI.Extras;
using Object = UnityEngine.Object;

namespace WorldAPI.ButtonAPI.MM.Carousel.Items;

public class CSlider
{
    public GameObject gameObject { get; private set; }
    public Transform transform { get; private set; }
    public Slider snapSlider { get; private set; }
    public TextMeshProUGUI TMProComp { get; private set; }

    public CSlider(CGrp grp, string text, Action<float> onValChanged, string toolTip = "", float defaultValue = 0f, float MinValue = 0f, float MaxValue = 1f, Sprite Icon = null) {
        if (!APIBase.IsReady()) throw new Exception();

        gameObject = Object.Instantiate(APIBase.MMSlider, grp.gameObject.transform);
        transform = gameObject.transform;
        gameObject.name = text;

        TMProComp = gameObject.transform.Find("LeftItemContainer/Text_MM_H3").GetComponent<TextMeshProUGUI>();
        TMProComp.text = text;
        TMProComp.richText = true;

        if (Icon != null)
            gameObject.transform.Find("LeftItemContainer/StatusIcon").GetComponent<Image>().sprite = Icon;
        gameObject.transform.Find("RightItemContainer/Text_MM_H3").gameObject.SetActive(false);

        snapSlider = gameObject.transform.Find("SliderContainer/Slider").GetComponent<Slider>();

        snapSlider.onValueChanged = new();
        snapSlider.minValue = MinValue;
        snapSlider.maxValue = MaxValue;
        snapSlider.value = defaultValue;
        snapSlider.onValueChanged.AddListener((Action<float>)delegate (float val) {
            onValChanged?.Invoke(val);
        });
        gameObject.transform.Find("SliderContainer/NoiseSuppressionButton").gameObject.active = false;

    }

    //public void AddToggle(Action<bool> listener) { 
        
    //}
}