using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace GodJunie.VTuber.UI {
    public class SplashScreen : MonoBehaviour {
        [TitleGroup("UI 오브젝트")]
        [LabelText("로그인 화면")]
        [SerializeField]
        private GameObject panelAuth;

        // Start is called before the first frame update
        private async void Start() {
            // 클라이언트가 최신 버전인가?
            string latestVersion = await GameManager.Instance.GetLatestVersionAsync();
            if(Application.version != latestVersion) {
                // 버전이 다름!
                // 스토어로 ㄱㄱ

                return;
            }

            // 서버가 점검중인가?
            string serverState = await GameManager.Instance.GetServerStateAsync();
            if(serverState == "maintaining") {
                // 현재 서버가 점검 중
                return;
            }

            // 리소스 상태가 최신인가?
            // Addressable Asset Management System

            // 개인정보제공동의를 했는가?

            // 로그인 캐시가 있는가?
            if(GameManager.Instance.IsAuthenticated) {
                // 로그인 캐시가 있음 (auth 에 Instance 할당하면서 자동으로 캐시 있으면 로그인 됨)

            } else {
                // 로그인 캐시가 없음
                // Auth 화면 띄워주기
                panelAuth.SetActive(true);
            }
        }

        public void GameStart() {
            // 메인화면으로 넘어가기
        }
    }
}