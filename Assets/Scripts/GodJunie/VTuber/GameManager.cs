// System
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
// Firebase
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Firestore;
// Facebook
using Facebook.Unity;
// GPGS
using GooglePlayGames;
using GooglePlayGames.BasicApi;
// Unity
using UnityEngine;
using UnityEngine.SceneManagement;
// Other
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;

namespace GodJunie.VTuber {
    public class GameManager : SingletonBehaviour<GameManager> {
        // Auth
        private FirebaseAuth auth;
        private FirebaseUser user;

        // Database
        private DatabaseReference database;

        // Firestore
        private FirebaseFirestore firestore;

        // Check Firebase Dependency
        // Call this function at start
        public async Task FirebaseInit() {
            var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();
            if(dependencyStatus == DependencyStatus.Available) {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                //   app = Firebase.FirebaseApp.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
                InitAuth();
                InitDatabase();
                InitFirestore();
            } else {
                Debug.LogError(string.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
            return;
        }

        #region Firebase Auth
        private void InitAuth() {
            auth = FirebaseAuth.DefaultInstance;
            auth.StateChanged += AuthStateChanged;
        }

        public bool IsAuthenticated {
            get {
                return user != null;
            }
        }

        public string UserId {
            get {
                if(user == null)
                    return "";
                else
                    return user.UserId;
            }
        }

        void AuthStateChanged(object sender, EventArgs eventArgs) {
            if(auth.CurrentUser == null) {
                // 로그아웃 했던가 방금 막 Init 함
                if(user == null) {
                    // Init Auth
                    Debug.Log("Init Success");
                } else {
                    // Signed out
                    Debug.Log("Signed out " + user.UserId);
                    user = null;
                }
            } else {
                if(auth.CurrentUser != user) {
                    // 로그인
                    user = auth.CurrentUser;
                    Debug.Log("Signed in " + user.UserId);
                }
            }
        }

        public void CreateAccount(string email, string password) {
            auth.CreateUserWithEmailAndPasswordAsync(email, password);
        }

        public async void SignInWithEmailAndPassword(string email, string password) {
            await auth.SignInWithEmailAndPasswordAsync(email, password);
        }

        public async Task<bool> SignInAnonymously() {
            var user = await auth.SignInAnonymouslyAsync();
            if(user != null) return true;
            return false;
        }

        public async Task<bool> SignInWithGoogle() {
            var source = new TaskCompletionSource<bool>();
            
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
                .RequestIdToken()
                .Build();

            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();

            Social.localUser.Authenticate(async success => {
                if(success) {
                    string idToken = ((PlayGamesLocalUser)Social.localUser).GetIdToken();

                    Credential credential = GoogleAuthProvider.GetCredential(idToken, null);
                    await auth.SignInWithCredentialAsync(credential);
                    source.SetResult(true);
                } else {
                    source.SetResult(false);
                }
            });

            return await source.Task;
        }

        public async Task<bool> SignInWithFacebook() {
            var credentialSource = new TaskCompletionSource<Credential>();

            var perms = new List<string>() { "public_profile", "email" };

            FB.LogInWithReadPermissions(perms, async result => {
                if(FB.IsLoggedIn) {
                    // AccessToken class will have session details
                    var aToken = AccessToken.CurrentAccessToken;
                    // Print current access token's User ID
                    Debug.Log(aToken.UserId);
                    // Print current access token's granted permissions
                    foreach(string perm in aToken.Permissions) {
                        Debug.Log(perm);
                    }
                    var credential = await WaitFacebookTokenAsync(aToken);
                    credentialSource.SetResult(credential);
                } else {
                    Debug.Log("User cancelled login");
                    credentialSource.SetResult(null);
                }
            });

            var credential = await credentialSource.Task;

            if(credential == null) {
                return false;
            }

            var user = await auth.SignInWithCredentialAsync(credential);

            if(user != null)
                return true;
            else
                return false;
        }

        private async UniTask<Credential> WaitFacebookTokenAsync(AccessToken aToken) {
            await UniTask.WaitUntil(() => string.IsNullOrEmpty(aToken.TokenString));
            return FacebookAuthProvider.GetCredential(aToken.TokenString);
        }

        public void SignOut() {
            //if(MyState != PlayerState.None) {
            //    // 유저 데이터 날리고
            //    SetUserDataNull();
            //    // 콜백 제거
            //    RemoveCallbackMethods();
            //}

            if(PlayGamesPlatform.Instance.IsAuthenticated())
                PlayGamesPlatform.Instance.SignOut();
            if(FB.IsLoggedIn)
                FB.LogOut();

            auth.SignOut();
        }
        #endregion

        #region Firebase Database
        private void InitDatabase() {
            this.database = FirebaseDatabase.DefaultInstance.RootReference;
        }
        #endregion

        #region Firebase Firestore
        private void InitFirestore() {
            this.firestore = FirebaseFirestore.DefaultInstance;
        }
        #endregion

        #region Management
        public async Task<string> GetLatestVersionAsync() {
            var value = await this.database.Child("management").Child("latest-version").GetValueAsync();
            return value.Value as string;
        }

        public async Task<string> GetServerStateAsync() {
            var value = await this.database.Child("management").Child("server-state").GetValueAsync();
            return value.Value as string;
        }
        #endregion

        #region Scene Management
        [SerializeField]
        [FilePath(AbsolutePath = false)]
        private string authScenePath;
        [SerializeField]
        [FilePath(AbsolutePath = false)]
        private string mainScenePath;

        public void LoadAuthSceneAsync() {
            SceneManager.LoadScene(authScenePath);
        }

        public void LoadMainSceneAsync() {
            SceneManager.LoadScene(mainScenePath);
        }
        #endregion
    }
}