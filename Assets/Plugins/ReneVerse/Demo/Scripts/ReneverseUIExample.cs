using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ReneVerse.Demo
{
    public class ReneverseUIExample : MonoBehaviour
    {
        private const string EmailFail = "Type Correct Email";
        private const string Connect = "Connect";

        [Header("ReneVerse Connect Button")] [SerializeField]
        private Button reneVerseConnect;

        [SerializeField] private TextMeshProUGUI connectButtonText;
    
    
        [Header("ReneVerse Email Input Field")]
        [SerializeField] private TextMeshProUGUI inputPlaceHolder;
        [SerializeField] private TMP_InputField reneVerseEmail;

        /// <summary>
        /// Feel free to move this Action to Inject or directly reference
        /// </summary>
        public event Action<string> OnReneVerseConnectClicked;
        private bool IsNotValidEmail => !reneVerseEmail.text.IsEmail();
        private bool EmptyEmailField => string.IsNullOrEmpty(reneVerseEmail.text);

        private void Disable(Behaviour behaviour) => behaviour.enabled = false;


        #region Enable/Disable

        private void OnEnable()
        {
            reneVerseConnect.onClick.AddListener(ReneVerseConnectClicked);
        }

        private void OnDisable()
        {
            reneVerseConnect.onClick.RemoveListener(ReneVerseConnectClicked);
        }

        #endregion

        private void ReneVerseConnectClicked()
        {
            if (IsNotValidEmail)
            {
                ChangeText(connectButtonText, EmailFail);
                return;
            }

            Disable(reneVerseConnect);
            Disable(reneVerseEmail);


            StartCoroutine(ConnectingDotsAnimation(connectButtonText));

            OnReneVerseConnectClicked?.Invoke(reneVerseEmail.text);
        }

        private void ChangeText(TextMeshProUGUI textMeshProUGUI, string text) => textMeshProUGUI.text = text;


        private IEnumerator ConnectingDotsAnimation(TextMeshProUGUI textMeshProUGUI)
        {
            while (true)
            {
                textMeshProUGUI.text = Connect;
                yield return new WaitForSeconds(0.3f);
                textMeshProUGUI.text = textMeshProUGUI.text.AddPoint();
                yield return new WaitForSeconds(0.3f);
                textMeshProUGUI.text = textMeshProUGUI.text.AddPoint();
                yield return new WaitForSeconds(0.3f);
                textMeshProUGUI.text = textMeshProUGUI.text.AddPoint();
                yield return new WaitForSeconds(0.3f);
            }
        }
    }
}
