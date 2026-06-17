using LeeSihyeon;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoingToMain : MonoBehaviour
{
    public void GoToMainScene()
    {
        SceneTransition.Instance.TransitionToScene("01_Main");

    }
}
