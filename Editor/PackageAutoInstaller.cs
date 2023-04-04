// using UnityEditor;
// using PackageInfo = UnityEditor.PackageManager.PackageInfo;
using UnityEditor.PackageManager;
using UnityEngine;

public class PackageAutoInstaller
{
    private static bool isInstallationTriggered;

    static PackageAutoInstaller()
    {
        Events.registeredPackages += OnPackageInstalled;
    }

    private static void OnPackageInstalled(PackageInfo packageInfo)
    {
        Debug.Log("OnPackageInstalled");
        if (packageInfo.name == "com.cj-mills.cv-image-gallery" && !isInstallationTriggered)
        {
            isInstallationTriggered = true;
            PackageInstaller.InstallDependencies();
        }
    }
}
