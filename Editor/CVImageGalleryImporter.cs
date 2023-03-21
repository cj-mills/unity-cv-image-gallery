using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class CVImageGalleryImporter : MonoBehaviour
{
    // [MenuItem("CV Image Gallery/Import Unity Package")]
    private static void ImportUnityPackage()
    {
        string packagePath = "Packages/com.cj-mills.cv-image-gallery/Extras/CVImageGallery.unitypackage";
        AssetDatabase.ImportPackage(packagePath, true);
    }
}
