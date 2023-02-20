#if UNITY_IOS || UNITY_ANDROID

using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace TapTap.Billboard.Editor {
    public class TapBillboardMobileProcessBuild {
        private readonly static int Order = 0;
        private readonly static string[] IGNORE_PATHS = new string[] {
            "Vuplex",
            "TapTap/Billboard/Standalone"
        };

        public class TapBillboardIOSPreprocessBuild : IPreprocessBuildWithReport {
            public int callbackOrder => Order;

            public void OnPreprocessBuild(BuildReport report) {
                if (report.summary.platform != BuildTarget.iOS &&
                    report.summary.platform != BuildTarget.Android) {
                    return;
                }

                foreach (string ignorePath in IGNORE_PATHS) {
                    if (!Directory.Exists(Path.Combine(Application.dataPath, ignorePath))) {
                        continue;
                    }
                    string ignoreName = Path.GetFileName(ignorePath);
                    AssetDatabase.RenameAsset(Path.Combine("Assets", ignorePath), $"{ignoreName}~");
                }
            }
        }

        public class TapBillboardIOSPostprocessBuild : IPostprocessBuildWithReport {
            public int callbackOrder => Order;

            public void OnPostprocessBuild(BuildReport report) {
                if (report.summary.platform != BuildTarget.iOS &&
                    report.summary.platform != BuildTarget.Android) {
                    return;
                }

                foreach (string ignorePath in IGNORE_PATHS) {
                    if (!Directory.Exists(Path.Combine(Application.dataPath, $"{ignorePath}~"))) {
                        continue;
                    }
                    Directory.Move(Path.Combine(Application.dataPath, $"{ignorePath}~"),
                        Path.Combine(Application.dataPath, $"{ignorePath}"));
                    AssetDatabase.ImportAsset(Path.Combine("Assets", ignorePath));
                }
            }
        }
    }
}

#endif