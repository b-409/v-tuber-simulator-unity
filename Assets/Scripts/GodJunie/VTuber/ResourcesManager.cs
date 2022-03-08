// System
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
// Unity Engine
using UnityEngine;
using UnityEngine.AddressableAssets;
// Other
using Cysharp.Threading.Tasks;

namespace GodJunie.VTuber {
    public class ResourcesManager : SingletonBehaviour<ResourcesManager> {
        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        public async Task<long> GetDownloadSizeAsync(string key) {
            var source = new TaskCompletionSource<long>();

            Addressables.GetDownloadSizeAsync(key).Completed += (handler) => {
                source.SetResult(handler.Result);

                Addressables.Release(handler);
            };

            return await source.Task;
        }
    }
}