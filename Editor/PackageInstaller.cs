using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

namespace CJM.CVGallery
{
    [System.Serializable]
    public class PackageData
    {
        public string packageName;
        public string packageUrl;
    }

    [System.Serializable]
    public class PackageList
    {
        public List<PackageData> packages;
    }

    public class PackageInstaller
    {
        private static AddRequest addRequest;
        private static List<PackageData> packagesToInstall;
        private static int currentPackageIndex;

        private const string PackagesJSONGUID = "f0b282a4fbb4473584f52e3fd0ab3087";

        [InitializeOnLoadMethod]
        public static void InstallDependencies()
        {
            packagesToInstall = ReadPackageJson().packages;
            currentPackageIndex = 0;

            InstallNextPackage();
        }

        private static void InstallNextPackage()
        {
            if (currentPackageIndex < packagesToInstall.Count)
            {
                PackageData packageData = packagesToInstall[currentPackageIndex];
                if (!IsPackageInstalled(packageData.packageName))
                {
                    addRequest = Client.Add(packageData.packageUrl);
                    EditorApplication.update += PackageInstallationProgress;
                }
                else
                {
                    currentPackageIndex++;
                    InstallNextPackage();
                }
            }
        }

        private static void PackageInstallationProgress()
        {
            if (addRequest.IsCompleted)
            {
                if (addRequest.Status == StatusCode.Success)
                {
                    UnityEngine.Debug.Log($"Successfully installed: {addRequest.Result.packageId}");
                }
                else if (addRequest.Status >= StatusCode.Failure)
                {
                    UnityEngine.Debug.LogError($"Failed to install package: {addRequest.Error.message}");
                }

                EditorApplication.update -= PackageInstallationProgress;
                currentPackageIndex++;
                InstallNextPackage();
            }
        }

        private static bool IsPackageInstalled(string packageName)
        {
            var listRequest = Client.List(true, false);
            while (!listRequest.IsCompleted) { }

            if (listRequest.Status == StatusCode.Success)
            {
                return listRequest.Result.Any(package => package.name == packageName);
            }
            else
            {
                UnityEngine.Debug.LogError($"Failed to list packages: {listRequest.Error.message}");
            }

            return false;
        }

        private static PackageList ReadPackageJson()
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(PackagesJSONGUID);
            string jsonString = File.ReadAllText(assetPath);
            return JsonUtility.FromJson<PackageList>(jsonString);
        }


    }
}
