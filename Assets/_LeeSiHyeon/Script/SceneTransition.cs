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

    /// <summary> 페이드 효과와 함께 Scene 전환 </summary>
    /// <param name="sceneName">전환할 Scene 이름</param>
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

    /// <summary> 페이드 인/아웃 및 씬 로드를 처리하는 코루틴 </summary>
    /// <param name="sceneName">전환할 씬의 이름</param>
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

    /// <summary> 씬에 존재하는 <see cref="Canvas"/>의 <see cref="Transform"/> 탐색 </summary>
    /// <param name="transform">찾으면 해당 객체의 <see cref="Transform"/>, 아니면 <see langword="null"/></param>
    /// <returns>캔버스가 존재하면 <see langword="true"/>, 아니면 <see langword="false"/></returns>
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

    /// <summary> <paramref name="sceneName"/>이 빌드 세팅에 존재하는지 확인 </summary>
    /// <param name="sceneName">검사할 Scene 이름</param>
    /// <returns>존재하면 <see langword="true"/>, 아니면 <see langword="false"/></returns>
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
