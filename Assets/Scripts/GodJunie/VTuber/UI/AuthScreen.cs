using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GodJunie.VTuber.UI {
    public class AuthScreen : MonoBehaviour {
        [SerializeField]
        private GameObject panelAuth;
        [SerializeField]
        private GameObject panelGameStart;

        // Start is called before the first frame update
        void Start() {
            if(GameManager.Instance.IsAuthenticated) {
                // 이미 인증 되어있음
                panelGameStart.SetActive(true);
            } else {
                panelAuth.SetActive(true);
            }
        }

        public async void SignInAnonymously() {
            var success = await GameManager.Instance.SignInAnonymously();
            if(success) {
                panelAuth.SetActive(false);
            } else {
                panelGameStart.SetActive(true);
            }
        }

        public async void SignInWithGoogle() {
            var success = await GameManager.Instance.SignInWithGoogle();
            if(success) {
                panelAuth.SetActive(false);
            } else {
                panelGameStart.SetActive(true);
            }
        }

        public void GameStart() {
            GameManager.Instance.LoadMainSceneAsync();
        }
    }
}