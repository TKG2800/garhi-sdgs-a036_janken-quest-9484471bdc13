using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

public class XcodeBuild
{
    [PostProcessBuild]
    public static void SetXcodePlist(BuildTarget buildTarget, string pathToBuiltProject)
    {
        if (buildTarget != BuildTarget.iOS) return;

        var plistPath = pathToBuiltProject + "/Info.plist";
        var plist = new PlistDocument();
        plist.ReadFromString(File.ReadAllText(plistPath));

        var rootDict = plist.root;

        // add key-value list
        rootDict.SetString("NSUserTrackingUsageDescription", "許可するとあなたに最適化された広告が表示されるようになり、それ以外の目的では使用されません。");
        rootDict.SetBoolean("ITSAppUsesNonExemptEncryption", false);

        File.WriteAllText(plistPath, plist.WriteToString());
    }
}
