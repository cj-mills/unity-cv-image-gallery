using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

namespace CJM.CVGallery
{
    public class PackageInstaller
    {
        private static AddRequest addRequest;
        private const string CustomDefineSymbol = "CJM_CV_IMAGE_GALLERY";

        [InitializeOnLoadMethod]
        public static void InstallDependencies()
        {
            // Debug.Log("InstallDependencies called.");

            string packageUrl = "https://github.com/cj-mills/unity-media-display.git";

            if (!IsPackageInstalled("com.cj-mills.unity-media-display"))
            {
                Debug.Log("Attempting to install package.");
                addRequest = Client.Add(packageUrl);
                EditorApplication.update += PackageInstallationProgress;
            }
        }

        private static void PackageInstallationProgress()
        {
            if (addRequest.IsCompleted)
            {
                if (addRequest.Status == StatusCode.Success)
                {
                    UnityEngine.Debug.Log($"Successfully installed: {addRequest.Result.packageId}");
                    AddCustomDefineSymbol(); // Add the custom define symbol after successful installation
                }
                else if (addRequest.Status >= StatusCode.Failure)
                {
                    UnityEngine.Debug.LogError($"Failed to install package: {addRequest.Error.message}");
                }

                EditorApplication.update -= PackageInstallationProgress;
            }
        }

        private static void AddCustomDefineSymbol()
        {
            var buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            var defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);

            if (!defines.Contains(CustomDefineSymbol))
            {
                defines += $";{CustomDefineSymbol}";
                PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, defines);
                Debug.Log($"Added custom define symbol '{CustomDefineSymbol}' to the project.");
            }
        }

        private static bool IsPackageInstalled(string packageName)
        {
            var listRequest = Client.List(true, false);
            while (!listRequest.IsCompleted) { }

            if (listRequest.Status == StatusCode.Success)
            {
                foreach (var package in listRequest.Result)
                {
                    if (package.name == packageName)
                    {
                        return true;
                    }
                }
            }
            else
            {
                UnityEngine.Debug.LogError($"Failed to list packages: {listRequest.Error.message}");
            }

            return false;
        }
    }
}
