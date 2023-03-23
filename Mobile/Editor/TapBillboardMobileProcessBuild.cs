#if UNITY_IOS || UNITY_ANDROID

using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using TapTap.Common.Editor;

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
                if (!BuildTargetUtils.IsSupportMobile(report.summary.platform)) {
                    return;
                }

                foreach (string ignorePath in IGNORE_PATHS) {
                    if (!Directory.Exists(Path.Combine(Application.dataPath, ignorePath))) {
                        continue;
                    }
                    string ignoreName = Path.GetFileName(ignorePath);
                    AssetDatabase.RenameAsset(Path.Combine("Assets", ignorePath), $"{ignoreName}~");
                }

                string linkPath = Path.Combine(Application.dataPath, "TapTap/Billboard/link.xml");
                LinkXMLGenerator.Generate(linkPath,
                        new LinkedAssembly[] {
                        new LinkedAssembly { Fullname = "TapTap.Billboard.Runtime" },
                        new LinkedAssembly { Fullname = "TapTap.Billboard.Mobile.Runtime" }
                    });
            }
        }

        public class TapBillboardIOSPostprocessBuild : IPostprocessBuildWithReport {
            public int callbackOrder => Order;

            public void OnPostprocessBuild(BuildReport report) {
                if (!BuildTargetUtils.IsSupportMobile(report.summary.platform)) {
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

                string linkPath = Path.Combine(Application.dataPath, "TapTap/Billboard/link.xml");
                LinkXMLGenerator.Delete(linkPath);
            }
        }
    }
}

#endif