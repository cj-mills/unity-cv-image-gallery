using UnityEditor;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;
using UnityEditor.PackageManager;
using UnityEngine;

public class PackageAutoInstaller
{
    private static bool isInstallationTriggered;

    static PackageAutoInstaller()
    {
        Events.packageRegistered += OnPackageRegistered;
    }

    private static void OnPackageRegistered(PackageInfo packageInfo)
    {
        if (packageInfo.name == "com.cj-mills.cv-image-gallery" && !isInstallationTriggered)
        {
            isInstallationTriggered = true;
            PackageInstaller.InstallDependencies();
        }
    }
}
