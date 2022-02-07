using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using Utility;
using DG.Tweening;

namespace WGTRework
{
    public class WGTUIManager : Singleton<WGTUIManager>
    {
        [SerializeField] Image waterGaugeFill;
        [SerializeField] Text countdownText;

        [SerializeField] Text inGameTimerText;
        [SerializeField] Image inGameTimerPieChat;

        [SerializeField] Text playerNameText;
        [SerializeField] Text playerPointText;

        private float currCountdownTime = -1;
        private float currGameTime = -1;

        void Start()
        {
            if (NetworkServer.active && !NetworkClient.active) Destroy(this);
            playerNameText.text = LocalPlayerDataManager.Instance.PlayerName;
        }

        public void UpdateWaterGaugeFill(float fill)
        {
            waterGaugeFill.fillAmount = fill;
        }

        public void OnLocalPlayerScored(int newAmount)
        {
            playerPointText.text = newAmount.ToString();
        }

        public IEnumerator BeginCountdown(float startTime, float duration)
        {
            int lastFrameTime = 0;
            int thisFrameTime = 0;
            currCountdownTime = startTime;

            countdownText.rectTransform.DOKill();
            countdownText.text = "3";
            countdownText.rectTransform.DOScale(0.0f, 0.8f).From(Vector3.one);
            countdownText.rectTransform.DORotate(new Vector3(0, 0, 360.0f), 0.8f).From(Vector3.zero);

            while (currCountdownTime - startTime <= duration)
            {
                lastFrameTime = thisFrameTime;
                thisFrameTime = Mathf.CeilToInt(currCountdownTime - startTime);

                if (lastFrameTime != thisFrameTime)
                {
                    countdownText.rectTransform.DOKill();
                    countdownText.text = ((duration + 1) - thisFrameTime).ToString();
                    countdownText.rectTransform.DOScale(0.0f, 0.8f).From(Vector3.one);
                    countdownText.rectTransform.DORotate(new Vector3(0, 0, 360.0f), 0.8f).From(Vector3.zero);
                }

                currCountdownTime = (float)NetworkTime.time;
                yield return null;
            }

            countdownText.rectTransform.DOKill();
            countdownText.text = "Go!";
            countdownText.rectTransform.DOScale(0.0f, 0.8f).From(Vector3.one);
            countdownText.rectTransform.DORotate(new Vector3(0, 0, 360.0f), 0.8f).From(Vector3.zero).OnComplete(()=> countdownText.gameObject.SetActive(false));
        }

        public IEnumerator BeginGameTimer(float startTime, float duration)
        {
            int lastFrameTime = 0;
            int thisFrameTime = 0;
            currGameTime = startTime;

            while (currGameTime - startTime <= duration)
            {
                lastFrameTime = thisFrameTime;
                thisFrameTime = Mathf.CeilToInt(currGameTime - startTime);

                if (lastFrameTime != thisFrameTime)
                {
                    inGameTimerText.rectTransform.DOKill();
                    inGameTimerText.text = (duration - thisFrameTime).ToString();
                    inGameTimerText.rectTransform.DOScale(0.0f, 0.8f).From(Vector3.one);
                    inGameTimerText.rectTransform.DORotate(new Vector3(0, 0, 360.0f), 0.8f).From(Vector3.zero);
                }

                inGameTimerPieChat.fillAmount = 1 - ((currGameTime - startTime) / duration);

                currGameTime = (float)NetworkTime.time;
                yield return null;
            }
        }

        public void GameEnd()
        {
            countdownText.rectTransform.DOKill();
            inGameTimerText.text = "Game end!";
            countdownText.rectTransform.DOScale(1.0f, 0.8f).From(Vector3.zero);
            countdownText.rectTransform.DORotate(new Vector3(0, 0, 360.0f), 0.8f).From(Vector3.zero);
        }
    }
}
