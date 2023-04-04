using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

[InitializeOnLoadAttribute]
public class PackageInstaller
{
    private static AddRequest addRequest;

    static PackageInstaller()
    {
        string packageUrl = "https://github.com/cj-mills/Unity-Media-Display.git";

        if (!IsPackageInstalled("com.cj-mills.unity-media-display"))
        {
            // Debug.Log("Attempting to install package.");
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
            }
            else if (addRequest.Status >= StatusCode.Failure)
            {
                UnityEngine.Debug.LogError($"Failed to install package: {addRequest.Error.message}");
            }

            EditorApplication.update -= PackageInstallationProgress;
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
