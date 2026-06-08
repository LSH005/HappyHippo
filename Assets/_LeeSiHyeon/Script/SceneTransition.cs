using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition Instance { get; private set; }
    public SceneTransitionCurtain curtain;
    public float fadeDuration = 0.15f;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// 씬 전환을 페이드 효과와 함께 수행. sceneName이 빌드 세팅에 존재하지 않으면 아무 동작도 하지 않음.
    /// </summary>
    /// <param name="sceneName">전환할 Scene의 이름</param>
    /// <param name="fadeDuration">단일 페이드 효과의 지속 시간</param>
    public void TransitionToScene(string sceneName)
    {
        if (curtain == null)
        {
            Debug.LogError("할당된 Curtain 프리팹 없음");
            return;
        }
        if (!DoesSceneExist(sceneName))
        {
            Debug.LogError($"Scene '{sceneName}' 은 빌드 세팅에 존재하지 않음.");
            return;
        }

        StartCoroutine(TransitionCoroutine(sceneName));
    }

    IEnumerator TransitionCoroutine(string sceneName)
    {
        Transform canvasTransform = null;
        if (TryGetCanvasTransform(out Transform prevCanvasTransform)) canvasTransform = prevCanvasTransform;
        else
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            Debug.LogError($"{currentSceneName} 에서 Canvas Transform을 찾을 수 없음");
            yield break;
        }

        SceneTransitionCurtain currentCurtain = Instantiate(curtain, canvasTransform);
        currentCurtain.SetCurtainAlpha(0f);
        currentCurtain.SetCurtainAlpha(1f, fadeDuration);
        yield return new WaitForSeconds(fadeDuration);
        SceneManager.LoadScene(sceneName);
        yield return null;

        if (TryGetCanvasTransform(out Transform nextCanvasTransform)) canvasTransform = nextCanvasTransform;
        else
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            Debug.LogError($"{currentSceneName} 에서 Canvas Transform을 찾을 수 없음");
            yield break;
        }

        currentCurtain = Instantiate(curtain, canvasTransform);
        currentCurtain.SetCurtainAlpha(0f, fadeDuration);
        Destroy(currentCurtain, fadeDuration);
    }

    /// <summary>
    /// 씬에 존재하는 Canvas의 Transform을 반환.
    /// </summary>
    /// <param name="transform"></param>
    /// <returns>아무 Canvas가 존재하면 true, 아니면 false</returns>
    bool TryGetCanvasTransform(out Transform transform)
    {
        Canvas canvas = FindAnyObjectByType<Canvas>();

        if (canvas != null)
        {
            transform = canvas.transform;
            return true;
        }

        transform = null;
        return false;
    }

    /// <summary>
    /// 씬 이름이 빌드 세팅에 존재하는지 확인
    /// </summary>
    /// <param name="sceneName">검사할 Scene 이름</param>
    /// <returns>sceneName을 이름으로 하는 Scene이 존재하면 true, 아니면 false</returns>
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
}
