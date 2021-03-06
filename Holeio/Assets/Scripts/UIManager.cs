using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace vasik
{
    public class UIManager : MonoBehaviour
    {
        public RectTransform scorePanel;
        public TMPro.TextMeshProUGUI totalScoreText;
        public Text scorePopUpPrefab;

        private int totalScore = 0;

        public void ShowScore(int score)
        {
            Text txt = Instantiate(scorePopUpPrefab);
            Vector3 initPosition = txt.rectTransform.localPosition;
            txt.rectTransform.SetParent(scorePanel);
            txt.rectTransform.localPosition = initPosition;
            txt.rectTransform.DOMoveY(Screen.height/2f, 2f);
            txt.DOFade(0f, 2f);

            if (score > 0)
            {
                txt.text = $"+ {score}";
                txt.color = Color.white;
            }               
            else
            {
                txt.text = score.ToString();
                txt.color = Color.red;
            }

            totalScore += score;
            totalScoreText.text = totalScore.ToString();

            if (totalScore < 0)
                totalScoreText.color = Color.red;
            else
                totalScoreText.color = Color.white;
        }
    }

}