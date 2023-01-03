using UnityEngine;
using UnityEngine.SceneManagement;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

namespace WorldAPI
{
    public static class SafeUtils
    {

        public static GameObject LocalPlayer;

        /// <summary>
        ///  Brings a VRCPickup/ Item to the Localplayer
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool BringItem(string path) =>
             BringItem(GameObject.Find(path), GetLocalPlayer().transform.localPosition);

        /// <summary>
        ///  Brings a VRCPickup/ Item to the Localplayer
        /// </summary>
        /// <param name="Obj"></param>
        /// <returns></returns>
        public static bool BringItem(GameObject Obj) =>
            BringItem(Obj, GetLocalPlayer().transform.localPosition);

        /// <summary>
        ///  Brings a VRCPickup/ Item to the VectorParm
        /// </summary>
        /// <param name="Obj"></param>
        /// <param name="Poz"></param>
        /// <returns></returns>
        public static bool BringItem(GameObject Obj, Vector3 Poz) {
            if (Obj == null) {
                Logs.Error($"Obj Is Null!");
                return false;
            }

            Networking.LocalPlayer.TakeOwnership(Obj);
            Obj.transform.position = Poz + new Vector3(0, 0.15f, 0);
            return true;
        }

        /// <summary>
        ///  Safely Grabs the LocalPlayers Obj
        /// </summary>
        /// <returns></returns>
        public static GameObject GetLocalPlayer()
        {
            if (LocalPlayer != null) return LocalPlayer;
            foreach (GameObject gameObject in SceneManager.GetActiveScene().GetRootGameObjects())
                if (gameObject.name.StartsWith("VRCPlayer[Local]"))
                    return gameObject;
            
            return null;
        }

        /// <summary>
        ///  Safelys Sends and UdonEvent to a GameObject (this is useful if the Obj is null)
        /// </summary>
        /// <param name="Obj"></param>
        /// <param name="Event"></param>
        /// <param name="targets"></param>
        /// <returns></returns>
        public static bool SafeSendUdon(GameObject Obj, string Event, NetworkEventTarget targets = NetworkEventTarget.All) {
            if (Obj == null) {
                Logs.Error($"Obj Is Null!");
                return false;
            }
            Obj.TryGetComponent<UdonBehaviour>(out var udon);
            if (udon == null) return false;
            udon.SendCustomNetworkEvent(targets, Event);
            return true;
        }

        /// <summary>
        /// Safelys Sends and UdonEvent to a GameObject (this is useful if the Obj is null)
        /// </summary>
        /// <param name="ObjName"></param>
        /// <param name="Event"></param>
        /// <param name="targets"></param>
        /// <returns></returns>
        public static bool SafeSendUdon(string ObjName, string Event, NetworkEventTarget targets = NetworkEventTarget.All) {
            var Obj = GameObject.Find(ObjName); 
            if (Obj == null) {
                Logs.Error($"Obj Is Null!");
                return false;
            }
            Obj.TryGetComponent<UdonBehaviour>(out var udon);
            if (udon == null) return false;
            udon.SendCustomNetworkEvent(targets, Event);
            return true;
        }

        /// <summary>
        /// This Safely Grabs a Unity Componet (Useful if either the GameObject or Component is null)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Obj"></param>
        /// <returns></returns>
        public static T SafeGrabComponet<T>(this GameObject Obj) where T : Component
        {
            if (Obj == null) {
                Logs.Error($"Obj Is Null!");
                return null;
            }
            Obj.TryGetComponent<T>(out var component);
            if (component == null) {
                //Logs.Error($"Component {nameof(T)} Is Null!");
                return null;
            }

            return component;
        }

        /// <summary>
        /// This Safely Grabs a Unity Componet (Useful if either the GameObject or Component is null)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Obj"></param>
        /// <returns></returns>
        public static T SafeGrabComponet<T>(this Transform Obj) where T : Component
        {
            if (Obj == null) {
                Logs.Error($"Obj Is Null!");
                return null;
            }
            Obj.TryGetComponent<T>(out var component);
            if (component == null) {
                Logs.Debug($"Component {nameof(T)} Is Null!");
                return null;
            }

            return component;
        }

        /// <summary>
        ///  Safely Instantiates an Object (Useful if either the GameObject or Parent is null)
        /// </summary>
        /// <param name="ObjName"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static GameObject SimpleSafeInstantiate(string ObjName, string parent) {
            if (string.IsNullOrEmpty(ObjName) || string.IsNullOrEmpty(parent)) return null;
            var Obj = GameObject.Find(ObjName);
            if (Obj == null) {
                Logs.Error($"Obj Is Null!");
                return null;
            }
            var Parent = GameObject.Find(parent);
            if (Parent == null) return null;

            var NewObject = Object.Instantiate(Obj, Parent.transform);

            return NewObject;
        }

        /// <summary>
        ///  Safely Instantiates an Object (Useful if either the GameObject or Parent is null)
        /// </summary>
        /// <param name="Obj"></param>
        /// <param name="Parent"></param>
        /// <returns></returns>
        public static GameObject SimpleSafeInstantiate(GameObject Obj, GameObject Parent) {
            if (Obj == null) {
                Logs.Error($"Obj Is Null!");
                return null;
            }
            if (Parent == null) return null;

            var NewObject = Object.Instantiate(Obj, Parent.transform);

            return NewObject;
        }
    }
}
