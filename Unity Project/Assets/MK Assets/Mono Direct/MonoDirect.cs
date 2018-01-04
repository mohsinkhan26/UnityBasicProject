/* 
 * Author : Mohsin Khan
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
 * BitBucket : https://bitbucket.org/mohsinkhan26/ 
*/
using UnityEngine;
using System;
using MK.Common.Utilities;

/// <summary>
/// Mono Direct - Gives your game speed as Unity calls these functions using Reflection.
/// So better to use reflection only in MonoDirect and implementing the callbacks in your MonoBehaviour scripts
/// Usage: Register the required action in Awake of your script and also Unregister in onDestroyCallback
/// Remember: Use this class only to save recurring reflection calls like Update, FixedUpdate; not one time reflection calls like Start, OnEnable
/// For more details:
/// Summary Reference: https://docs.unity3d.com/Manual/ExecutionOrder.html
/// </summary>

namespace MK.Direct
{
    public sealed class MonoDirect : Singleton<MonoDirect>
    {
        #region Actions declared

        /// <summary>
        /// <para>One time reflection call - Not recommended to use this.</para>
        /// <para>First Scene Load - (only called if the Object is active): This function is called just after the object is enabled. 
        ///  This happens when a MonoBehaviour instance is created, such as when a level is loaded or a 
        ///  GameObject with the script component is instantiated</para>
        /// </summary>
        public Action onEnableCallback;
        #if UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2
        /// <summary>
        /// <para>One time reflection call - Not recommended to use this.</para>
        /// <para>First Scene Load - This function is executed to inform the game that a new level has been loaded</para>
        /// </summary>
        public Action<int> onLevelWasLoadedCallback;
#endif

        /// <summary>
        /// <para>One time reflection call - Not recommended to use this.</para>
        /// <para>Before the first frame update - Start is called before the first frame update only if the script instance is enabled</para>
        /// </summary>
        public Action startCallback;

        /// <summary>
        /// <para>Recurring reflection call - recommended to use this.</para>
        /// <para>In between frames - This is called at the end of the frame where the pause is detected, effectively between the normal
        ///  frame updates. One extra frame will be issued after OnApplicationPause is called to allow the game
        ///  to show graphics that indicate the paused state</para>
        /// </summary>
        public Action<bool> onApplicationPauseCallback;
        /// <summary>
        /// <para>Recurring reflection call - recommended to use this.</para>
        /// <para>In between frames - called when the application loses or gains focus. Alt-tabbing or Cmd-tabbing can take focus away
        ///  from the Unity application to another desktop application</para>
        /// </summary>
        public Action<bool> onApplicationFocusCallback;

        /// <summary>
        /// <para>Recurring reflection call - recommended to use this.</para>
        /// <para>Update Order - FixedUpdate is often called more frequently than Update. It can be called multiple times per frame, 
        ///  if the frame rate is low and it may not be called between frames at all if the frame rate is high. All physics 
        ///  calculations and updates occur immediately after FixedUpdate. When applying movement calculations inside FixedUpdate, 
        ///  you do not need to multiply your values by Time.deltaTime. This is because FixedUpdate is called on a reliable timer, 
        ///  independent of the frame rate.</para>
        /// </summary>
        public Action fixedUpdateCallback;
        /// <summary>
        /// <para>Recurring reflection call - recommended to use this.</para>
        /// <para>Update Order - Update is called once per frame. It is the main workhorse function for frame updates.</para>
        /// </summary>
        public Action updateCallback;
        /// <summary>
        /// <para>Recurring reflection call - recommended to use this.</para>
        /// <para>Update Order - LateUpdate is called once per frame, after Update has finished. Any calculations that are performed
        ///  in Update will have completed when LateUpdate begins. A common use for LateUpdate would be a following 
        ///  third-person camera. If you make your character move and turn inside Update, you can perform all camera movement
        ///  and rotation calculations in LateUpdate. This will ensure that the character has moved completely 
        ///  before the camera tracks its position.</para>
        /// </summary>
        public Action lateUpdateCallback;

        /// <summary>
        /// <para>Recurring reflection call - recommended to use this.</para>
        /// <para>Rendering - Called before the camera culls the scene. Culling determines which objects are visible to the camera. 
        ///  OnPreCull is called just before culling takes place.</para>
        /// </summary>
        public Action onPreCullCallback;
        /// <summary>
        /// <para>Recurring reflection call - recommended to use this.</para>
        /// <para>Rendering - Called when an object becomes visible to any camera</para>
        /// </summary>
        public Action onBecameVisibleCallback;
        /// <summary>
        /// <para>Recurring reflection call - recommended to use this.</para>
        /// <para>Rendering - Called when an object becomes invisible to any camera</para>
        /// </summary>
        public Action onBecameInvisibleCallback;
        /// <summary>
        /// <para>Recurring reflection call - recommended to use this.</para>
        /// <para>Rendering - Called once for each camera if the object is visible</para>
        /// </summary>
        public Action onWillRenderObjectCallback;
        /// <summary>
        /// <para>Recurring reflection call - recommended to use this.</para>
        /// <para>Rendering - Called before the camera starts rendering the scene</para>
        /// </summary>
        public Action onPreRenderCallback;
        /// <summary>
        /// <para>Recurring reflection call - recommended to use this.</para>
        /// <para>Rendering - Called after all regular scene rendering is done. You can use GL class or Graphics. 
        ///  DrawMeshNow to draw custom geometry at this point</para>
        /// </summary>
        public Action onRenderObjectCallback;
        /// <summary>
        /// <para>Recurring reflection call - recommended to use this.</para>
        /// <para>Rendering - Called after a camera finishes rendering the scene</para>
        /// </summary>
        public Action onPostRenderCallback;
        /// <summary>
        /// <para>Recurring reflection call - recommended to use this.</para>
        /// <para>Rendering - Called after scene rendering is complete to allow postprocessing of the image</para>
        /// </summary>
        public Action<RenderTexture, RenderTexture> onRenderImageCallback;
        /// <summary>
        /// <para>Recurring reflection call - recommended to use this.</para>
        /// <para>Rendering - Called multiple times per frame in response to GUI events. The Layout and Repaint events
        ///  are processed first, followed by a Layout and keyboard/mouse event for each input event</para>
        /// </summary>
        public Action onGUICallback;
        /// <summary>
        /// <para>Recurring reflection call - recommended to use this.</para>
        /// <para>Rendering - Used for drawing Gizmos in the scene view for visualisation purposes</para>
        /// </summary>
        public Action onDrawGizmosCallback;

        /// <summary>
        /// <para>One time reflection call - Not recommended to use this.</para>
        /// <para>When the Object is Destroyed -  This function is called after all frame updates for the last frame of
        ///  the object’s existence (the object might be destroyed in response to Object.Destroy or at the closure of a scene)</para>
        /// </summary>
        public Action onDestroyCallback;

        /// <summary>
        /// <para>One time reflection call - Not recommended to use this.</para>
        /// <para>When Quitting - This function is called on all game objects before the application is quit. 
        ///  In the editor it is called when the user stops playmode</para>
        /// </summary>
        public Action onApplicationQuitCallback;
        /// <summary>
        /// <para>One time reflection call - Not recommended to use this.</para>
        /// <para>When Quitting - This function is called when the behaviour becomes disabled or inactive</para>
        /// </summary>
        public Action onDisableCallback;

        #endregion Actions declared

        #region Actions called

        /// <summary>
        /// First Scene Load - (only called if the Object is active): This function is called just after the object is enabled. 
        ///  This happens when a MonoBehaviour instance is created, such as when a level is loaded or a 
        ///  GameObject with the script component is instantiated
        /// </summary>
        void OnEnable()
        {
            if (onEnableCallback != null)
                onEnableCallback();
        }

        #if UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2
        /// <summary>
        /// First Scene Load - This function is executed to inform the game that a new level has been loaded
        /// </summary>
        void OnLevelWasLoaded(int level)
        {
            if (onLevelWasLoadedCallback != null)
                onLevelWasLoadedCallback(level);
        }
#endif

        /// <summary>
        /// Before the first frame update - Start is called before the first frame update only if the script instance is enabled
        /// </summary>
        protected override void Start()
        {
            base.Start();
            if (startCallback != null)
                startCallback();
        }


        /// <summary>
        /// In between frames - This is called at the end of the frame where the pause is detected, effectively between the normal
        ///  frame updates. One extra frame will be issued after OnApplicationPause is called to allow the game
        ///  to show graphics that indicate the paused state
        /// </summary>
        void OnApplicationPause(bool pauseStatus)
        {
            if (onApplicationPauseCallback != null)
                onApplicationPauseCallback(pauseStatus);
        }

        void OnApplicationFocus(bool hasFocus)
        {
            if (onApplicationFocusCallback != null)
                onApplicationFocusCallback(hasFocus);
        }

        /// <summary>
        /// Update Order - FixedUpdate is often called more frequently than Update. It can be called multiple times per frame, 
        ///  if the frame rate is low and it may not be called between frames at all if the frame rate is high. All physics 
        ///  calculations and updates occur immediately after FixedUpdate. When applying movement calculations inside FixedUpdate, 
        ///  you do not need to multiply your values by Time.deltaTime. This is because FixedUpdate is called on a reliable timer, 
        ///  independent of the frame rate.
        /// </summary>
        void FixedUpdate()
        {
            if (fixedUpdateCallback != null)
                fixedUpdateCallback();
        }

        /// <summary>
        /// Update Order - Update is called once per frame. It is the main workhorse function for frame updates.
        /// </summary>
        void Update()
        {
            if (updateCallback != null)
                updateCallback();
        }

        /// <summary>
        /// Update Order - LateUpdate is called once per frame, after Update has finished. Any calculations that are performed
        ///  in Update will have completed when LateUpdate begins. A common use for LateUpdate would be a following 
        ///  third-person camera. If you make your character move and turn inside Update, you can perform all camera movement
        ///  and rotation calculations in LateUpdate. This will ensure that the character has moved completely 
        ///  before the camera tracks its position.
        /// </summary>
        void LateUpdate()
        {
            if (lateUpdateCallback != null)
                lateUpdateCallback();
        }

        /// <summary>
        /// Rendering - Called before the camera culls the scene. Culling determines which objects are visible to the camera. 
        ///  OnPreCull is called just before culling takes place.
        /// </summary>
        void OnPreCull()
        {
            if (onPreCullCallback != null)
                onPreCullCallback();
        }

        /// <summary>
        /// Rendering - Called when an object becomes visible to any camera
        /// </summary>
        void OnBecameVisible()
        {
            if (onBecameVisibleCallback != null)
                onBecameVisibleCallback();
        }

        /// <summary>
        /// Rendering - Called when an object becomes invisible to any camera
        /// </summary>
        void OnBecameInvisible()
        {
            if (onBecameInvisibleCallback != null)
                onBecameInvisibleCallback();
        }

        /// <summary>
        /// Rendering - Called once for each camera if the object is visible
        /// </summary>
        void OnWillRenderObject()
        {
            if (onWillRenderObjectCallback != null)
                onWillRenderObjectCallback();
        }

        /// <summary>
        /// Rendering - Called before the camera starts rendering the scene
        /// </summary>
        void OnPreRender()
        {
            if (onPreRenderCallback != null)
                onPreRenderCallback();
        }

        /// <summary>
        /// Rendering - Called after all regular scene rendering is done. You can use GL class or Graphics. 
        ///  DrawMeshNow to draw custom geometry at this point
        /// </summary>
        void OnRenderObject()
        {
            if (onRenderObjectCallback != null)
                onRenderObjectCallback();
        }

        /// <summary>
        /// Rendering - Called after a camera finishes rendering the scene
        /// </summary>
        void OnPostRender()
        {
            if (onPostRenderCallback != null)
                onPostRenderCallback();
        }

        /// <summary>
        /// Rendering - Called after scene rendering is complete to allow postprocessing of the image
        /// </summary>
        void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            if (onRenderImageCallback != null)
                onRenderImageCallback(src, dest);
        }

        /// <summary>
        /// Rendering - Called multiple times per frame in response to GUI events. The Layout and Repaint events
        ///  are processed first, followed by a Layout and keyboard/mouse event for each input event
        /// </summary>
        void OnGUI()
        {
            if (onGUICallback != null)
                onGUICallback();
        }

        /// <summary>
        /// Rendering - Used for drawing Gizmos in the scene view for visualisation purposes
        /// </summary>
        void OnDrawGizmos()
        {
            if (onDrawGizmosCallback != null)
                onDrawGizmosCallback();
        }

        /// <summary>
        /// When the Object is Destroyed -  This function is called after all frame updates for the last frame of
        ///  the object’s existence (the object might be destroyed in response to Object.Destroy or at the closure of a scene)
        /// </summary>
        protected override void OnDestroy()
        {
            if (onDestroyCallback != null)
                onDestroyCallback();

            UnregisterActionsOnDestroy();
            base.OnDestroy();
        }

        /// <summary>
        /// When Quitting - This function is called on all game objects before the application is quit. 
        ///  In the editor it is called when the user stops playmode
        /// </summary>
        protected override void OnApplicationQuit()
        {
            base.OnApplicationQuit();
            if (onApplicationQuitCallback != null)
                onApplicationQuitCallback();
        }

        /// <summary>
        /// When Quitting - This function is called when the behaviour becomes disabled or inactive
        /// </summary>
        void OnDisable()
        {
            if (onDisableCallback != null)
                onDisableCallback();
        }

        #endregion Actions called

        #region Unregister actions

        void UnregisterActionsOnDestroy()
        {
            if (onEnableCallback != null)
                onEnableCallback();

            //#if UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2
            //if (onLevelWasLoadedCallback != null)
            //    onLevelWasLoadedCallback(level);
            //#endif

            if (startCallback != null)
                startCallback();
            //if (onApplicationPauseCallback != null)
            //    onApplicationPauseCallback(pauseStatus);
            //if (onApplicationFocusCallback != null)
            //    onApplicationFocusCallback(hasFocus);

            if (fixedUpdateCallback != null)
                fixedUpdateCallback();
            if (updateCallback != null)
                updateCallback();
            if (lateUpdateCallback != null)
                lateUpdateCallback();

            if (onPreCullCallback != null)
                onPreCullCallback();
            if (onBecameVisibleCallback != null)
                onBecameVisibleCallback();
            if (onBecameInvisibleCallback != null)
                onBecameInvisibleCallback();
            if (onWillRenderObjectCallback != null)
                onWillRenderObjectCallback();
            if (onPreRenderCallback != null)
                onPreRenderCallback();
            if (onRenderObjectCallback != null)
                onRenderObjectCallback();
            if (onPostRenderCallback != null)
                onPostRenderCallback();
            //if (onRenderImageCallback != null)
            //    onRenderImageCallback(src, dest);

            if (onGUICallback != null)
                onGUICallback();

            if (onDrawGizmosCallback != null)
                onDrawGizmosCallback();

            if (onApplicationQuitCallback != null)
                onApplicationQuitCallback();

            if (onDisableCallback != null)
                onDisableCallback();
        }

        #endregion Unregister actions
    }
}
