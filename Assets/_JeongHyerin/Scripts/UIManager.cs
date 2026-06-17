using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using TMPro.EditorUtilities;
using System;

namespace JeongHyerin
{
    [System.Serializable]
    public class StatInfo
    {
        public string statName;
        public TextMeshProUGUI statText;
        public int currentValue;
    }

    public class UIManager : MonoBehaviour
    {
        [Header("AboutPlayerTitle")]
        public GameObject playerTitlePanel;

        [Header("StatInfo")]
        public StatInfo[] playerStats;
        private int maxStat = 9999;

        [Header("여기저기")]
        public GameObject darknessObject;

        void Start()
        {
            playerTitlePanel.SetActive(false);
            darknessObject.SetActive(false);

            InitStatTexts();
        }
        void Update()
        {

        }
        public void PlayerTitlePanelOpen()
        {
            if (playerTitlePanel != null)
            {
                darknessObject.SetActive(true);
                playerTitlePanel.SetActive(true);
            }
            else
            {
                Debug.Log("칭호 패널 없음");
            }
        }
        public void PlayerTitlePanelClose()
        {
            if (playerTitlePanel != null)
            {
                darknessObject.SetActive(false);
                playerTitlePanel.SetActive(false);
            }
        }
        public void LoadMainScene()
        {
            SceneManager.LoadScene("01_Main");
        }
        public void OnClickIcon(int index)
        {
            IncreaseStatTween(index, 500);
        }
        public void IncreaseStatTween(int index, int amount)
        {
            if (playerStats == null || index < 0 || index >= playerStats.Length) return;

            StatInfo stat = playerStats[index];

            if (stat.statText == null) return;

            int targetValue = Mathf.Clamp(stat.currentValue + amount, 0, maxStat);

            DOTween.To(() => stat.currentValue, x => stat.currentValue = x, targetValue, 0.8f)
                .OnUpdate(() =>
                {
                    stat.statText.text = stat.currentValue.ToString();
                })
                .OnComplete(() =>
                {
                    stat.currentValue = targetValue;
                    stat.statText.text = stat.currentValue.ToString();
                });
        }
        void InitStatTexts()
        {
            if (playerStats == null) return;

            for (int i = 0; i < playerStats.Length; i++)
            {
                if (playerStats[i] != null && playerStats[i].statText != null)
                {
                    playerStats[i].statText.text = playerStats[i].currentValue.ToString();
                }
            }
        }
    }
}


