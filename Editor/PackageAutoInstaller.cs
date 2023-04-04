using UnityEditor;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;
using UnityEditor.PackageManager;
using UnityEngine;

public class PackageAutoInstaller
{
    private static bool isInstallationTriggered;

    static PackageAutoInstaller()
    {
        Events.registeredPackages += OnRegisteredPackages;
    }

    private static void OnRegisteredPackages(PackageRegistrationEventArgs eventArgs)
    {
        Debug.Log("OnRegisteredPackages");
        foreach (var packageInfo in eventArgs.added)
        {
            if (packageInfo.name == "com.cj-mills.cv-image-gallery" && !isInstallationTriggered)
            {
                isInstallationTriggered = true;
                PackageInstaller.InstallDependencies();
                break;
            }
        }
    }
}
