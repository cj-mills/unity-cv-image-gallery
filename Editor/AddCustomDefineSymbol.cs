// using UnityEditor;
// using UnityEngine;

// namespace CJM.BarracudaInference
// {
//     public class DependencyDefineSymbolAdder
//     {
//         private const string CustomDefineSymbol = "CJM_CV_IMAGE_GALLERY";

//         [InitializeOnLoadMethod]
//         public static void AddCustomDefineSymbol()
//         {
//             var buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
//             var defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);

//             if (!defines.Contains(CustomDefineSymbol))
//             {
//                 defines += $";{CustomDefineSymbol}";
//                 PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, defines);
//                 Debug.Log($"Added custom define symbol '{CustomDefineSymbol}' to the project.");
//             }
//         }
//     }
// }