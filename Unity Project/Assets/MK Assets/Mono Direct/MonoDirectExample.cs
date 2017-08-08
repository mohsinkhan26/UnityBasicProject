/* 
 * Author : Mohsin Khan
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
 * BitBucket : https://bitbucket.org/unbounded-eagle/ 
*/
using UnityEngine;
using MK.Direct;

namespace MK.Direct
{
    public class MonoDirectExample : MonoBehaviour
    {
        void Awake()
        {
            MonoDirect.Instance.onEnableCallback += OnEnableCalled;
    #if UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2
            MonoDirect.Instance.onLevelWasLoadedCallback += OnLevelWasLoadedCalled;
    #endif

            MonoDirect.Instance.startCallback += StartCalled;

            MonoDirect.Instance.onApplicationPauseCallback += OnApplicationPauseCalled;
            MonoDirect.Instance.onApplicationFocusCallback += OnApplicationFocusCalled;

            MonoDirect.Instance.fixedUpdateCallback += FixedUpdateCalled;
            MonoDirect.Instance.updateCallback += UpdateCalled;
            MonoDirect.Instance.lateUpdateCallback += LateUpdateCalled;

            MonoDirect.Instance.onPreCullCallback += OnPreCullCalled;
            MonoDirect.Instance.onBecameVisibleCallback += OnBecameVisibleCalled;
            MonoDirect.Instance.onBecameInvisibleCallback += OnBecameInvisibleCalled;
            MonoDirect.Instance.onWillRenderObjectCallback += OnWillRenderObjectCalled;
            MonoDirect.Instance.onPreRenderCallback += OnPreRenderCalled;
            MonoDirect.Instance.onRenderObjectCallback += OnRenderObjectCalled;
            MonoDirect.Instance.onPostRenderCallback += OnPostRenderCalled;
            MonoDirect.Instance.onRenderImageCallback += OnRenderImageCalled;
            MonoDirect.Instance.onGUICallback += OnGUICalled;
            MonoDirect.Instance.onDrawGizmosCallback += OnDrawGizmosCalled;

            MonoDirect.Instance.onDestroyCallback += OnDestroyCalled;

            MonoDirect.Instance.onApplicationQuitCallback += OnApplicationQuitCalled;
            MonoDirect.Instance.onDisableCallback += OnDisableCalled;
        }

        #region MonoDirect Actions called

        void OnEnableCalled()
        {
            Debug.Log("MonoDirectExample-OnEnableCalled");
        }
        #if UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2
        void OnLevelWasLoadedCalled() { }
    #endif

        void StartCalled()
        {
            Debug.Log("MonoDirectExample-StartCalled");
        }

        void OnApplicationPauseCalled(bool pauseStatus)
        {
            Debug.Log("MonoDirectExample-OnApplicationPauseCalled: " + pauseStatus);
        }

        void OnApplicationFocusCalled(bool hasFocus)
        {
            Debug.Log("MonoDirectExample-OnApplicationFocusCalled: " + hasFocus);
        }

        void FixedUpdateCalled()
        {
            //Debug.Log("MonoDirectExample-FixedUpdateCalled");
        }

        void UpdateCalled()
        {
            //Debug.Log("MonoDirectExample-UpdateCalled");
        }

        void LateUpdateCalled()
        {
            //Debug.Log("MonoDirectExample-LateUpdateCalled");
        }

        void OnPreCullCalled()
        {
            //Debug.Log("MonoDirectExample-OnPreCullCalled");
        }

        void OnBecameVisibleCalled()
        {
            //Debug.Log("MonoDirectExample-OnBecameVisibleCalled");
        }

        void OnBecameInvisibleCalled()
        {
            //Debug.Log("MonoDirectExample-OnBecameInvisibleCalled");
        }

        void OnWillRenderObjectCalled()
        {
            //Debug.Log("MonoDirectExample-OnWillRenderObjectCalled");
        }

        void OnPreRenderCalled()
        {
            //Debug.Log("MonoDirectExample-OnPreRenderCalled");
        }

        void OnRenderObjectCalled()
        {
            //Debug.Log("MonoDirectExample-OnRenderObjectCalled");
        }

        void OnPostRenderCalled()
        {
            //Debug.Log("MonoDirectExample-OnPostRenderCalled");
        }

        void OnRenderImageCalled(RenderTexture src, RenderTexture dest)
        {
            //Debug.Log("MonoDirectExample-OnRenderImageCalled");
        }

        void OnGUICalled()
        {
            //Debug.Log("MonoDirectExample-OnGUICalled");
        }

        void OnDrawGizmosCalled()
        {
            //Debug.Log("MonoDirectExample-OnDrawGizmosCalled");
        }

        void OnDestroyCalled()
        {
            Debug.Log("MonoDirectExample-OnDestroyCalled");
            if (MonoDirect.HasInstance && MonoDirect.Instance.onEnableCallback != null)
                MonoDirect.Instance.onEnableCallback -= OnEnableCalled;
    #if UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2
            if (MonoDirect.HasInstance && MonoDirect.Instance.onLevelWasLoadedCallback != null)
                MonoDirect.Instance.onLevelWasLoadedCallback -= OnLevelWasLoadedCalled;
    #endif

            if (MonoDirect.HasInstance && MonoDirect.Instance.startCallback != null)
                MonoDirect.Instance.startCallback -= StartCalled;

            if (MonoDirect.HasInstance && MonoDirect.Instance.onApplicationPauseCallback != null)
                MonoDirect.Instance.onApplicationPauseCallback -= OnApplicationPauseCalled;

            if (MonoDirect.HasInstance && MonoDirect.Instance.fixedUpdateCallback != null)
                MonoDirect.Instance.fixedUpdateCallback -= FixedUpdateCalled;
            if (MonoDirect.HasInstance && MonoDirect.Instance.updateCallback != null)
                MonoDirect.Instance.updateCallback -= UpdateCalled;
            if (MonoDirect.HasInstance && MonoDirect.Instance.lateUpdateCallback != null)
                MonoDirect.Instance.lateUpdateCallback -= LateUpdateCalled;

            if (MonoDirect.HasInstance && MonoDirect.Instance.onPreCullCallback != null)
                MonoDirect.Instance.onPreCullCallback -= OnPreCullCalled;
            if (MonoDirect.HasInstance && MonoDirect.Instance.onBecameVisibleCallback != null)
                MonoDirect.Instance.onBecameVisibleCallback -= OnBecameVisibleCalled;
            if (MonoDirect.HasInstance && MonoDirect.Instance.onBecameInvisibleCallback != null)
                MonoDirect.Instance.onBecameInvisibleCallback -= OnBecameInvisibleCalled;
            if (MonoDirect.HasInstance && MonoDirect.Instance.onWillRenderObjectCallback != null)
                MonoDirect.Instance.onWillRenderObjectCallback -= OnWillRenderObjectCalled;
            if (MonoDirect.HasInstance && MonoDirect.Instance.onPreRenderCallback != null)
                MonoDirect.Instance.onPreRenderCallback -= OnPreRenderCalled;
            if (MonoDirect.HasInstance && MonoDirect.Instance.onRenderObjectCallback != null)
                MonoDirect.Instance.onRenderObjectCallback -= OnRenderObjectCalled;
            if (MonoDirect.HasInstance && MonoDirect.Instance.onPostRenderCallback != null)
                MonoDirect.Instance.onPostRenderCallback -= OnPostRenderCalled;
            if (MonoDirect.HasInstance && MonoDirect.Instance.onRenderImageCallback != null)
                MonoDirect.Instance.onRenderImageCallback -= OnRenderImageCalled;
            if (MonoDirect.HasInstance && MonoDirect.Instance.onGUICallback != null)
                MonoDirect.Instance.onGUICallback -= OnGUICalled;
            if (MonoDirect.HasInstance && MonoDirect.Instance.onDrawGizmosCallback != null)
                MonoDirect.Instance.onDrawGizmosCallback -= OnDrawGizmosCalled;

            if (MonoDirect.HasInstance && MonoDirect.Instance.onApplicationQuitCallback != null)
                MonoDirect.Instance.onApplicationQuitCallback -= OnApplicationQuitCalled;
            if (MonoDirect.HasInstance && MonoDirect.Instance.onDisableCallback != null)
                MonoDirect.Instance.onDisableCallback -= OnDisableCalled;

            if (MonoDirect.HasInstance && MonoDirect.Instance.onDestroyCallback != null)
                MonoDirect.Instance.onDestroyCallback -= OnDestroyCalled;
        }

        void OnApplicationQuitCalled()
        {
            Debug.Log("MonoDirectExample-OnApplicationQuitCalled");
        }

        void OnDisableCalled()
        {
            Debug.Log("MonoDirectExample-OnDisableCalled");
        }

        #endregion MonoDirect Actions called
    }
}
