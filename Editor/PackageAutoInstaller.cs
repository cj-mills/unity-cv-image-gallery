using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;

public class PackageAutoInstaller
{
    private static bool isInstallationTriggered;

    static PackageAutoInstaller()
    {
        Events.PackageRegistryUpdated += OnPackageRegistryUpdated;
    }

    private static void OnPackageRegistryUpdated(PackageRegistrationEventArgs args)
    {
        foreach (var package in args.added)
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
