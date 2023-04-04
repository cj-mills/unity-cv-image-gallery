using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;

public class PackageAutoInstaller
{
    private static bool isInstallationTriggered;

    static PackageAutoInstaller()
    {
        Events.registeredPackages += OnRegisteredPackages;
    }

    private static void OnRegisteredPackages(UnityEditor.PackageManager.PackageInfo[] packages)
    {
        foreach (var package in packages)
        {
            if (package.name == "com.cj-mills.cv-image-gallery" && !isInstallationTriggered)
            {
                isInstallationTriggered = true;
                PackageInstaller.InstallDependencies();
                break;
            }
        }
    }
}
