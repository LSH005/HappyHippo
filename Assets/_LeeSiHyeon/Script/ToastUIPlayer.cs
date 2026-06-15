using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeeSihyeon
{
    public class ToastUIPlayer : MonoBehaviour
    {
        /// <summary> 토스트 메시지를 출력할 대사 구조체 </summary>
        [Serializable]
        public struct ToastUIDialog
        {
            [Header("Loop")]
            public int loopCount;
            [Header("Text")]
            public string text;
            public float waitTime;
        }

        public ToastUI toastMessege;
        public Transform canvas;
        public ToastUIDialog[] dialog;
        public ToastUIData data;

        List<ToastUI> toastUIs = new List<ToastUI>();

        private void Start()
        {
            StartCoroutine(StartToastUI());
        }

        /// <summary> <see cref="dialog"/>의 데이터를 읽고 출력 </summary>
        IEnumerator StartToastUI()
        {
            for (int i = 0; i < dialog.Length; i++)
            {
                ToastUIDialog current = dialog[i];
                for (int j = 0; j < current.loopCount; j++)
                {
                    if (!string.IsNullOrEmpty(current.text)) AddToast(current.text);
                    if (current.waitTime > 0) yield return new WaitForSeconds(current.waitTime);
                }
            }
        }

        /// <summary> <paramref name="messege"/>를 글자로 하는 토스트 메시지 출력 </summary>
        /// <param name="messege">토스트 메시지의 글자</param>
        void AddToast(string messege)
        {
            StackAllToast();
            ToastUI newMessegeUI = Instantiate(toastMessege, canvas);
            newMessegeUI.SetData(data);
            newMessegeUI.Show(messege);
            toastUIs.Add(newMessegeUI);
        }

        /// <summary> 모든 토스트 메시지의 쌓임 단계 증가 </summary>
        void StackAllToast()
        {
            foreach (var t in toastUIs)
            {
                t.Stack();
            }
        }
    }
}