using UnityEditor;
using UnityEngine;

public class PackageAutoInstaller : AssetPostprocessor
{
    private static bool isInstallationTriggered;

    // This method will be called after any asset has been imported
    private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        // Check if the CVImageGallery package has been imported
        foreach (string asset in importedAssets)
        {
            if (asset.Contains("com.cj-mills.cv-image-gallery"))
            {
                if (!isInstallationTriggered)
                {
                    isInstallationTriggered = true;
                    PackageInstaller.InstallDependencies();
                }
                break;
            }
        }
    }
}
