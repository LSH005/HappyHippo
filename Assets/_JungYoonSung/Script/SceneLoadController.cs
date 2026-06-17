using UnityEngine;
using UnityEngine.SceneManagement;

namespace QuestScene_JungYoonSung_2023137028
{
    public class SceneLoadController : MonoBehaviour
    {
        [Header("Scene Settings")]
        [SerializeField] private string targetSceneName;

        public void LoadTargetScene()
        {
            if (!string.IsNullOrEmpty(targetSceneName))
            {
                SceneManager.LoadScene(targetSceneName);
            }
        }
    }
}