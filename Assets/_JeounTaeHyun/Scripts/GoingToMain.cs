using LeeSihyeon;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace jeountaehyun
{
    public class GoingToMain : MonoBehaviour
    {
        public void GoToMainScene()
        {
            SceneTransition.Instance.TransitionToScene("01_Main");

        }
    }
}

