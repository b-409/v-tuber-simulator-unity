using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace GodJunie.VTuber.UI {
    public class SplashScreen : MonoBehaviour {
        [TitleGroup("UI ������Ʈ")]
        [LabelText("�α��� ȭ��")]
        [SerializeField]
        private GameObject panelAuth;

        // Start is called before the first frame update
        private async void Start() {
            // Ŭ���̾�Ʈ�� �ֽ� �����ΰ�?
            string latestVersion = await GameManager.Instance.GetLatestVersionAsync();
            if(Application.version != latestVersion) {
                // ������ �ٸ�!
                // ������ ����

                return;
            }

            // ������ �������ΰ�?
            string serverState = await GameManager.Instance.GetServerStateAsync();
            if(serverState == "maintaining") {
                // ���� ������ ���� ��
                return;
            }

            // ���ҽ� ���°� �ֽ��ΰ�?
            // Addressable Asset Management System

            // ���������������Ǹ� �ߴ°�?

            // �α��� ĳ�ð� �ִ°�?
            if(GameManager.Instance.IsAuthenticated) {
                // �α��� ĳ�ð� ���� (auth �� Instance �Ҵ��ϸ鼭 �ڵ����� ĳ�� ������ �α��� ��)

            } else {
                // �α��� ĳ�ð� ����
                // Auth ȭ�� ����ֱ�
                panelAuth.SetActive(true);
            }
        }

        public void GameStart() {
            // ����ȭ������ �Ѿ��
        }
    }
}