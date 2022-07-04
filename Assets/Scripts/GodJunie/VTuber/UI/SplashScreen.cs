using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace GodJunie.VTuber.UI {
    public class SplashScreen : MonoBehaviour {
        // Start is called before the first frame update
        private async void Start() {
            await GameManager.Instance.FirebaseInit();

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
            long size = await ResourcesManager.Instance.GetDownloadSizeAsync("default");
            if(size > 0) {
                // 받아야 하는 리소스가 있다
                // 다운로드 확인 화면 띄워주기
                return;
            }

            // 개인정보제공동의를 했는가?
            // TODO: 개인정보제공동의 제작

            GameManager.Instance.LoadAuthSceneAsync();
        }
    }
}