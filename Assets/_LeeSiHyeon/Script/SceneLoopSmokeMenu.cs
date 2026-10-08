#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.Text;
using LeeSihyeon;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoopSmokeMenu
{
    const string SessionKey = "HappyHippo.SceneLoopSmoke";

    [MenuItem("HappyHippo/씬 루프 점검")]
    static void Run()
    {
        if (EditorApplication.isPlaying)
        {
            Debug.LogWarning("플레이 모드를 종료한 뒤 다시 실행하세요.");
            return;
        }

        if (EditorSceneManager.GetActiveScene().isDirty)
        {
            Debug.LogWarning("저장되지 않은 씬이 있습니다. 저장한 뒤 다시 실행하세요.");
            return;
        }

        SessionState.SetBool(SessionKey, true);
        EditorSceneManager.OpenScene("Assets/Scenes/01_Main.unity");
        EditorApplication.isPlaying = true;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void StartRunnerIfRequested()
    {
        if (!SessionState.GetBool(SessionKey, false)) return;
        SessionState.SetBool(SessionKey, false);

        var host = new GameObject("SceneLoopSmokeRunner");
        host.hideFlags = HideFlags.HideAndDontSave;
        Object.DontDestroyOnLoad(host);
        host.AddComponent<SceneLoopSmokeRunner>();
    }
}

public class SceneLoopSmokeRunner : MonoBehaviour
{
    readonly List<string> failures = new List<string>();

    void Start()
    {
        Application.logMessageReceived += OnLog;
        StartCoroutine(Run());
    }

    void OnDestroy()
    {
        Application.logMessageReceived -= OnLog;
    }

    void OnLog(string condition, string stackTrace, LogType type)
    {
        if (type != LogType.Error && type != LogType.Exception && type != LogType.Assert) return;
        if (condition.StartsWith("[SceneLoop]")) return;
        failures.Add(condition);
    }

    IEnumerator Run()
    {
        yield return WaitForTransitionSystem();
        if (SceneTransition.Instance == null)
        {
            failures.Add("SceneTransition.Instance가 없습니다.");
            Finish();
            yield break;
        }

        yield return Go("03_Quest");
        yield return Go("01_Main");
        yield return Go("04_HuntingGround");
        yield return Go("01_Main");
        yield return Go("05_Character");
        yield return Go("01_Main");
        yield return Go("06_Menu");
        yield return Go("02_Setting");
        yield return Back("06_Menu");
        yield return Go("01_Main");
        yield return Go("02_Setting");
        yield return Back("01_Main");
        yield return Go("06_Menu");
        yield return Go("07_Transaction");
        yield return Go("01_Main");

        string before = SceneManager.GetActiveScene().name;
        SceneTransition.Instance.TransitionToScene(before);
        yield return null;
        if (SceneTransition.isTransitioning)
            failures.Add("같은 씬으로의 전환이 시작되었습니다.");

        SceneTransition.Instance.TransitionToScene("03_Quest");
        SceneTransition.Instance.TransitionToScene("04_HuntingGround");
        yield return WaitUntilSettled("03_Quest");

        Finish();
    }

    IEnumerator WaitForTransitionSystem()
    {
        float elapsed = 0f;
        while (SceneTransition.Instance == null && elapsed < 2f)
        {
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }
    }

    IEnumerator Go(string sceneName)
    {
        if (SceneTransition.Instance == null)
        {
            failures.Add("전환 도중 SceneTransition이 사라졌습니다.");
            yield break;
        }

        string from = SceneManager.GetActiveScene().name;
        SceneTransition.Instance.TransitionToScene(sceneName);
        yield return WaitUntilSettled(sceneName);
        if (SceneManager.GetActiveScene().name == sceneName)
            Debug.Log($"[SceneLoop] {from} -> {sceneName}");
    }

    IEnumerator Back(string expectedScene)
    {
        if (SceneTransition.Instance == null)
        {
            failures.Add("뒤로가기 도중 SceneTransition이 사라졌습니다.");
            yield break;
        }

        string from = SceneManager.GetActiveScene().name;
        SceneTransition.Instance.TransitionToLastScene();
        yield return WaitUntilSettled(expectedScene);
        if (SceneManager.GetActiveScene().name == expectedScene)
            Debug.Log($"[SceneLoop] {from} -> back -> {expectedScene}");
    }

    IEnumerator WaitUntilSettled(string expectedScene)
    {
        float elapsed = 0f;
        while (elapsed < 15f)
        {
            bool arrived = SceneManager.GetActiveScene().name == expectedScene;
            if (arrived && !SceneTransition.isTransitioning)
                yield break;

            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        failures.Add("시간 초과. 기대 씬 " + expectedScene + ", 현재 " + SceneManager.GetActiveScene().name + ", transitioning=" + SceneTransition.isTransitioning);
    }

    void Finish()
    {
        var report = new StringBuilder();
        report.Append("[SceneLoop] 점검 종료. 수집된 오류 ");
        report.Append(failures.Count);
        report.Append("건");
        if (failures.Count == 0)
        {
            Debug.Log(report.ToString());
        }
        else
        {
            for (int i = 0; i < failures.Count; i++)
            {
                report.AppendLine();
                report.Append("- ");
                report.Append(failures[i]);
            }
            Debug.LogError(report.ToString());
        }

        EditorApplication.isPlaying = false;
    }
}
#endif
