using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;

public class PackageAutoInstaller
{
    private static bool isInstallationTriggered;

    static PackageAutoInstaller()
    {
        Events.registered += OnPackageRegistered;
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
