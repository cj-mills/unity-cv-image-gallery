using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CJM.CVGallery
{
    // Serializable class to hold package data
    [System.Serializable]
    public class PackageData
    {
        public string packageName;
        public string packageUrl;
    }

    // Serializable class to hold a list of PackageData objects
    [System.Serializable]
    public class PackageList
    {
        public List<PackageData> packages;
    }

    // Class responsible for installing a list of packages defined in a JSON file
    public class PackageInstaller
    {
        private static AddRequest addRequest;
        private static List<PackageData> packagesToInstall;
        private static int currentPackageIndex;

        // GUID of the JSON file containing the list of packages to install
        private const string PackagesJSONGUID = "f0b282a4fbb4473584f52e3fd0ab3087";

        // Method called on load to install packages from the JSON file
        [InitializeOnLoadMethod]
        public static void InstallDependencies()
        {
            packagesToInstall = ReadPackageJson().packages;
            currentPackageIndex = 0;

            InstallNextPackage();
        }

        // Method to install the next package in the list
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

        // Method to monitor the progress of package installation
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

        // Method to check if a package is already installed
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

        // Method to read the JSON file and return a PackageList object
        private static PackageList ReadPackageJson()
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(PackagesJSONGUID);
            string jsonString = File.ReadAllText(assetPath);
            return JsonUtility.FromJson<PackageList>(jsonString);
        }
    }
}
