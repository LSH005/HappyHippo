using UnityEngine;

namespace Scene_Quest_Á¤À±¼º_2023137028
{
    public class UINavigator : MonoBehaviour
    {
        [Header("Target Panels")]
        [SerializeField] private UIPanel panelToHide; 
        [SerializeField] private UIPanel panelToShow; 

        public void SwitchPopups()
        {
            if (panelToHide != null && panelToHide.gameObject.activeSelf)
            {
                panelToHide.Hide();
            }

            if (panelToShow != null)
            {
                panelToShow.Show();
            }
        }
        public void ShowTarget() { if (panelToShow != null) panelToShow.Show(); }
        public void HideTarget() { if (panelToHide != null) panelToHide.Hide(); }
    }
}