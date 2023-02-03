using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using WorldAPI.ButtonAPI.Wing;
using WorldAPI.ButtonAPI.Wing.Buttons;
using WorldAPI.ButtonAPI.WIng.Controls;
using Object = UnityEngine.Object;

namespace WorldAPI.ButtonAPI.WIng.Buttons;

public class WToggle : WingTglControls
{
    public bool State { get; internal set; }
    public string onTip { get; private set; }
    public string offTip { get; private set; }

    public WToggle(WPage menu, string text, Action<bool> listener, bool DefaultState = false,
            string OffTooltip = null, string OnToolTip = null,
            Sprite onimage = null, Sprite offimage = null) {
        if (!APIBase.IsReady()) throw new Exception();

        if (OffTooltip == null) OffTooltip = $"Turn On {text.Replace("\n", string.Empty)}";
        if (OnToolTip == null) OnToolTip = $"Turn Off {text.Replace("\n", string.Empty)}";
        if (offimage == null) offimage = APIBase.OffSprite;
        if (onimage == null) onimage = APIBase.OnSprite;
        Listener = listener;
        State = DefaultState;
        Text = text;
        var transform = new WButton(menu, Text, () => {
            State = !State;
            APIBase.SafelyInvolk(State, Listener, Text);
            OffImage.gameObject.active = !State;
            OnImage.gameObject.active = State;
            SetToolTip(DefaultState ? onTip : offTip);
            APIBase.Events.onWToggleValChange?.Invoke(this, State);
        }, DefaultState ? OnToolTip : OffTooltip, DefaultState ? onimage : offimage);
        Consrt(transform, DefaultState, OffTooltip, OnToolTip, onimage, offimage);
    }

    private void Consrt(WButton transform, bool DefaultState = false,
            string OffTooltip = null, string OnToolTip = null,
            Sprite onimage = null, Sprite offimage = null) {

        TMProCompnt = transform.TMProCompnt;
        gameObject = transform.gameObject;
        transform.gameObject.SetActive(true);
        OffImage = transform.ImgCompnt;
        OnImage = Object.Instantiate(transform.ImgCompnt.gameObject, transform.ImgCompnt.transform.parent).GetComponent<UIWidgets.ImageAdvanced>();

        OffImage.gameObject.active = !DefaultState;
        OnImage.gameObject.active = DefaultState;

        SetImages(true, onimage, offimage);
        SetToolTip(DefaultState ? onTip : offTip);
    }
}