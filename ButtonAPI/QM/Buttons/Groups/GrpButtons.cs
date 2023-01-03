using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using WorldAPI.ButtonAPI.Groups;
using WorldAPI.Buttons;
using static WorldAPI.ButtonAPI.Controls.ExtentedControl;

namespace WorldAPI.ButtonAPI.Buttons.Groups
{
    public class GrpButtons
    {

        public VRCButton buttonOne;
        public VRCButton buttonTwo;
        public VRCButton buttonThree;

        public GrpButtons(GameObject menu, string FirstName, string FirstTooltip, Action action, 
            string SecondName = null, string SecondTooltip = null, Action Secondaction = null, 
            string thirdName = null, string thirdTooltip = null, Action thirdaction = null)
        {
            GameObject Base = new GameObject();
            Base.name = $"GroupOfHalfButtons";
            Base.transform.parent = menu.transform;
            Base.transform.localRotation = Quaternion.Euler(0, 0, 0);
            Base.transform.localScale = new Vector3(1f, 1f, 1f);
            Base.transform.localPosition = Vector3.zero;
            Base.AddComponent<LayoutElement>();
            Transform Sub = new GameObject().transform; // this has a reason! ;3
            Sub.name = $"Buttons_[WorldClient]";
            Sub.transform.parent = Base.transform;
            Sub.transform.localRotation = Quaternion.Euler(0, 0, 0);
            Sub.transform.localScale = new Vector3(1f, 1f, 1f);
            Sub.transform.localPosition = new Vector3(0f, -3f, 0f);


            buttonOne = new VRCButton(Sub, FirstName, FirstTooltip, action, true, false, null, HalfType.Top, true);
            buttonOne.gameObject.transform.localPosition = new Vector3(0f, 10.7f, 0);

            if (Secondaction != null) {
                buttonTwo = new VRCButton(Sub, SecondName, SecondTooltip, Secondaction, true, false, null, HalfType.Normal, true);
                buttonTwo.transform.localPosition = new Vector3(0f, -1.36f, 0);
            }


            if (thirdaction != null) {
                buttonThree = new VRCButton(Sub, thirdName, thirdTooltip, thirdaction, true, false, null, HalfType.Bottom, true);
                buttonThree.transform.localPosition = new Vector3(0f, -13.88f, 0);
            }
        }

        public GrpButtons(CollapsibleButtonGroup grp, string FirstName, string FirstTooltip, Action action,
            string SecondName = null, string SecondTooltip = null, Action Secondaction = null,
            string thirdName = null, string thirdTooltip = null, Action thirdaction = null) : this(grp.gameObject, FirstTooltip, FirstTooltip, action,
                SecondName, SecondTooltip, Secondaction,
                thirdName, thirdTooltip, thirdaction)
        { }

        public GrpButtons(ButtonGroup grp, string FirstName, string FirstTooltip, Action action,
            string SecondName = null, string SecondTooltip = null, Action Secondaction = null,
            string thirdName = null, string thirdTooltip = null, Action thirdaction = null) : this(grp.gameObject, FirstName, FirstTooltip, action,
                SecondName, SecondTooltip, Secondaction,
                thirdName, thirdTooltip, thirdaction)
        { }
    }
}
