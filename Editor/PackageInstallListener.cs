using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

[InitializeOnLoad]
public class PackageInstallListener
{
    private static ListRequest listRequest;

    static PackageInstallListener()
    {
        EditorApplication.update += CheckPackageInstallation;
    }

    private static void CheckPackageInstallation()
    {
        Debug.Log("Here");

        if (listRequest == null)
        {
            listRequest = Client.List(true, true);
            return;
        }

        if (!listRequest.IsCompleted)
        {
            return;
        }

        if (listRequest.Status == StatusCode.Success)
        {
            foreach (var package in listRequest.Result)
            {
                if (package.name == "com.cj-mills.cv-image-gallery" && package.status == PackageStatus.Available)
                {
                    Debug.Log("Package installed. Running custom code...");

                    // Run your desired code here.

                    // Unregister the event to prevent it from running again.
                    EditorApplication.update -= CheckPackageInstallation;
                }
            }
        }
        else if (listRequest.Status >= StatusCode.Failure)
        {
            Debug.LogError("Failed to check for package installation.");
            EditorApplication.update -= CheckPackageInstallation;
        }

        listRequest = null;

        EditorApplication.update -= CheckPackageInstallation;
    }
}
