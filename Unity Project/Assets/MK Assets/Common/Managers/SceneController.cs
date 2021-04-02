/* 
 * Author : Mohsin Khan
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
 * BitBucket : https://bitbucket.org/mohsinkhan26/ 
*/
using UnityEngine;
using MK.Common.Utilities;

#if UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2
#else
using UnityEngine.SceneManagement;
#endif

/// <summary>
/// Reference: Architecting Games in Unity | Rus SCAMMELL
/// Reference Video: https://www.youtube.com/watch?v=64uOVmQ5R1k
/// Reference Content: http://goo.gl/8WZGxn
/// </summary>

namespace MK.Common.Managers
{
    // it would be better, if you name scenes according to scenes listed in build settings
    public enum GameScene
    {
        MenuScene = 0,
        GameScene
    }

    public sealed class SceneController : Singleton<SceneController>
    {
        //static MainController mainController; // Singleton class does this

        [SerializeField]
        GameScene firstScene;
        int currentSceneName;
        int nextSceneName;
        AsyncOperation resourceUnloadTask;
        AsyncOperation sceneLoadTask;

        enum SceneState
        {
            Reset,
            Preload,
            Load,
            Unload,
            Postload,
            Ready,
            Run,

            // tells total number of states, should be at the end
            Count
        }

        SceneState sceneState;

        delegate void UpdateDelegate();

        UpdateDelegate[] updateDelegates;

        #region public API
        public string CurrentSceneName { get { return SceneManager.GetActiveScene().name; } }

        [System.Obsolete("Better to use with enum value")]
        public void SwitchScene(int _nextScene)
        {
            SwitchScene(GameUtilities.ParseEnum<GameScene>(_nextScene));
        }

        public void SwitchScene(GameScene _nextScene)
        {
            if (HasInstance)
            {
                if (currentSceneName != ((int)_nextScene))
                {
                    nextSceneName = ((int)_nextScene);
                }
            }
        }
        #endregion public API

        #region protected mono methods
        protected override void Awake()
        {
            base.Awake();

            //Let's keep this alive between scene changes
            //Object.DontDestroyOnLoad(gameObject); // Singleton class does this

            //Setup the singleton instance
            //mainController = this; // Singleton class does this

            //Setup the array of updateDelegates
            updateDelegates = new UpdateDelegate[(int)SceneState.Count];

            //Set each updateDelegate
            updateDelegates[(int)SceneState.Reset] = UpdateSceneReset;
            updateDelegates[(int)SceneState.Preload] = UpdateScenePreload;
            updateDelegates[(int)SceneState.Load] = UpdateSceneLoad;
            updateDelegates[(int)SceneState.Unload] = UpdateSceneUnload;
            updateDelegates[(int)SceneState.Postload] = UpdateScenePostload;
            updateDelegates[(int)SceneState.Ready] = UpdateSceneReady;
            updateDelegates[(int)SceneState.Run] = UpdateSceneRun;

            // TODO: name of first scene of your game, scene at 0 index in BuildSettings
            nextSceneName = (int)firstScene;
            sceneState = SceneState.Run;

            // if this script is attached to the camera
            //camera.orthographicSize = Screen.height / 2;
        }

        void OnEnable()
        {
        }

        protected override void Start()
        {
            base.Start();
        }

        void Update()
        {
            if (updateDelegates != null && updateDelegates[(int)sceneState] != null)
            {
                updateDelegates[(int)sceneState]();
            }
        }

        protected override void OnDestroy()
        {
            //Clean up all the updateDelegates
            if (updateDelegates != null)
            {
                for (int i = 0; i < (int)SceneState.Count; i++)
                {
                    updateDelegates[i] = null;
                }
                updateDelegates = null;
            }

            //Clean up the singleton instance
            base.OnDestroy();
            //if (mainController != null) mainController = null; // Singleton class does this
        }

        void OnDisable()
        {
        }
        #endregion protected mono methods

        #region private delegate methods
        // attach the new scene controller to start cascade of loading
        void UpdateSceneReset()
        {
            Debug.Log("SceneController-UpdateSceneReset-Current: " + SceneManager.GetActiveScene().name);
            // run a gc pass
            System.GC.Collect();
            sceneState = SceneState.Preload;
        }

        // handle anything that needs to happen before loading
        void UpdateScenePreload()
        {
            string _nextSceneName = ((GameScene)nextSceneName).ToString();
            Debug.Log("SceneController-UpdateScenePreload-Current: " + SceneManager.GetActiveScene().name);
#if UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2
            sceneLoadTask = Application.LoadLevelAsync(_nextSceneName);
#else
            sceneLoadTask = SceneManager.LoadSceneAsync(_nextSceneName);
#endif
            sceneState = SceneState.Load;
        }

        // show the loading screen until it's loaded
        void UpdateSceneLoad()
        {
            Debug.Log("SceneController-UpdateSceneLoad-Current: " + SceneManager.GetActiveScene().name);
            // done loading?
            if (sceneLoadTask.isDone == true)
            {
                sceneState = SceneState.Unload;
            }
            else
            { // TODO: update scene loading progress - show loading slider/image
            }
        }

        // clean up unused resources by unloading them
        void UpdateSceneUnload()
        {
            Debug.Log("SceneController-UpdateSceneUnload-Current: " + SceneManager.GetActiveScene().name);
            // cleaning up resources yet?
            if (resourceUnloadTask == null)
            {
                resourceUnloadTask = Resources.UnloadUnusedAssets();
            }
            else
            {
                // done cleaning up?
                if (resourceUnloadTask.isDone == true)
                {
                    resourceUnloadTask = null;
                    sceneState = SceneState.Postload;
                }
            }
        }

        // handle anything that needs to happen immediately after loading
        void UpdateScenePostload()
        {
            Debug.Log("SceneController-UpdateScenePostload-Current: " + SceneManager.GetActiveScene().name);
            currentSceneName = nextSceneName;
            sceneState = SceneState.Ready;
        }

        // handle anything that needs to happen immediately before running
        void UpdateSceneReady()
        {
            Debug.Log("SceneController-UpdateSceneReady-Current: " + SceneManager.GetActiveScene().name);
            // run a gc pass
            // if you have assets loaded in the scene that are
            // currently unused currently but may be used later
            // DON'T do this here
            System.GC.Collect();
            sceneState = SceneState.Run;
        }

        // wait for scene change - it will keep running as it would change in update
        void UpdateSceneRun()
        {
            //Debug.Log("SceneController-UpdateSceneRun-Current: " + SceneManager.GetActiveScene().name);
            if (currentSceneName != nextSceneName)
            {
                sceneState = SceneState.Reset;
            }
        }
        #endregion private delegate methods
    }
}
