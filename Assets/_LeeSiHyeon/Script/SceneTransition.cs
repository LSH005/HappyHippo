using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LeeSihyeon
{
    public class SceneTransition : MonoBehaviour
    {
        public static SceneTransition Instance { get; private set; }
        public SceneTransitionCurtain curtain;
        public float fadeDuration = 0.15f;
        public static bool isTransitioning;
        public static bool canTransition;
        public static bool isLoading;

        const int CurtainSortingOrder = 30000;
        const string MainSceneName = "01_Main";

        string lastSceneName;
        SceneTransitionCurtain activeCurtain;
        RectTransform curtainRoot;

        /// <summary>
        /// 01_Main, 02_Setting, 07_Transaction 이외의 씬에서 Play해도 전환이 되도록 인스턴스를 만든다.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void BootstrapIfMissing()
        {
            if (Instance != null) return;

            SceneTransition prefab = Resources.Load<SceneTransition>("SceneTransition");
            if (prefab != null)
            {
                Instantiate(prefab);
                return;
            }

            var created = new GameObject("SceneTransition");
            created.AddComponent<SceneTransition>();
        }

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            isTransitioning = false;
            isLoading = false;
            canTransition = false;
            DontDestroyOnLoad(gameObject);
            CreateCurtainCanvas();
        }

        void OnDestroy()
        {
            if (Instance != this) return;
            Instance = null;
            isTransitioning = false;
            isLoading = false;
            canTransition = false;
        }

        /// <summary> 바로 이전에 있던 Scene으로 전환. 기록이 없거나 현재 씬과 같으면 01_Main으로 간다. </summary>
        public void TransitionToLastScene()
        {
            if (isTransitioning) return;

            string current = GetSceneName();
            string target = lastSceneName;
            if (string.IsNullOrEmpty(target) || target == current)
                target = MainSceneName;
            if (target == current) return;

            TransitionToScene(target);
        }

        /// <summary> 페이드 효과와 함께 Scene 전환 </summary>
        /// <param name="sceneName">전환할 Scene 이름</param>
        public void TransitionToScene(string sceneName)
        {
            if (isTransitioning) return;
            if (string.IsNullOrEmpty(sceneName)) return;
            if (sceneName == GetSceneName()) return;
            if (!DoesSceneExist(sceneName))
            {
                Debug.LogError($"Scene '{sceneName}' 은 빌드 세팅에 존재하지 않음.");
                return;
            }

            EnsureActiveCurtain();
            if (activeCurtain == null)
            {
                Debug.LogError("할당된 Curtain 프리팹 없음");
                return;
            }

            lastSceneName = GetSceneName();
            isTransitioning = true;
            StartCoroutine(TransitionCoroutine(sceneName));
        }

        IEnumerator TransitionCoroutine(string sceneName)
        {
            activeCurtain.SetBlocking(true);

            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            if (operation == null)
            {
                EndTransition(true);
                yield break;
            }

            operation.allowSceneActivation = false;
            isLoading = true;
            canTransition = false;

            // 로딩 시작 프레임의 끊김이 페이드보다 먼저 지나가게 한 뒤 커튼을 올린다.
            yield return null;

            activeCurtain.SetCurtainAlpha(0f);
            activeCurtain.SetCurtainAlpha(1f, fadeDuration);
            yield return new WaitForSecondsRealtime(fadeDuration);

            while (operation.progress < 0.9f)
                yield return null;

            canTransition = true;
            operation.allowSceneActivation = true;

            while (!operation.isDone)
                yield return null;

            isLoading = false;
            activeCurtain.SetCurtainAlpha(0f, fadeDuration);
            yield return new WaitForSecondsRealtime(fadeDuration);
            EndTransition(false);
        }

        void EndTransition(bool hideImmediately)
        {
            isTransitioning = false;
            isLoading = false;
            canTransition = false;
            if (activeCurtain == null) return;
            if (hideImmediately)
                activeCurtain.SetCurtainAlpha(0f);
            activeCurtain.SetBlocking(false);
        }

        void CreateCurtainCanvas()
        {
            var canvasObject = new GameObject("TransitionCanvas", typeof(RectTransform));
            canvasObject.transform.SetParent(transform, false);

            var canvas = canvasObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.overrideSorting = true;
            canvas.sortingOrder = CurtainSortingOrder;

            canvasObject.AddComponent<GraphicRaycaster>();

            var scaler = canvasObject.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920f, 1080f);
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;

            curtainRoot = canvasObject.GetComponent<RectTransform>();
            FitToParent(curtainRoot);
            EnsureActiveCurtain();
        }

        void EnsureActiveCurtain()
        {
            if (activeCurtain != null) return;
            if (curtain == null || curtainRoot == null) return;

            activeCurtain = Instantiate(curtain, curtainRoot);
            var nestedCanvas = activeCurtain.GetComponent<Canvas>();
            if (nestedCanvas != null)
            {
                nestedCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
                nestedCanvas.overrideSorting = true;
                nestedCanvas.sortingOrder = CurtainSortingOrder;
            }

            if (activeCurtain.GetComponent<GraphicRaycaster>() == null)
                activeCurtain.gameObject.AddComponent<GraphicRaycaster>();

            FitToParent(activeCurtain.transform as RectTransform);
            activeCurtain.SetCurtainAlpha(0f);
            activeCurtain.SetBlocking(false);
        }

        static void FitToParent(RectTransform rect)
        {
            if (rect == null) return;
            rect.localScale = Vector3.one;
            rect.localRotation = Quaternion.identity;
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = Vector2.zero;
            rect.sizeDelta = Vector2.zero;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
        }

        bool DoesSceneExist(string sceneName)
        {
            if (string.IsNullOrEmpty(sceneName)) return false;

            int sceneCount = SceneManager.sceneCountInBuildSettings;
            for (int i = 0; i < sceneCount; i++)
            {
                string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
                string nameFromPath = System.IO.Path.GetFileNameWithoutExtension(scenePath);
                if (nameFromPath == sceneName) return true;
            }

            return false;
        }

        string GetSceneName() => SceneManager.GetActiveScene().name;
    }
}
