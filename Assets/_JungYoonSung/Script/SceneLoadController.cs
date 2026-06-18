using UnityEngine;

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
                LeeSihyeon.SceneTransition.Instance.TransitionToScene(targetSceneName);
            }
        }
    }
}