using System.IO;
using UnityEngine;
using UnityEditor;
using YamlDotNet;
using YamlDotNet.RepresentationModel;

public static class UnityConnectSettingsReference
{
    public static string[] UnityAdsGameIds()
    {
        string[] result_id = new string[2];
        StreamReader inputFile = new StreamReader("ProjectSettings/UnityConnectSettings.asset");
        YamlStream yaml = new YamlStream();
        yaml.Load(inputFile);
        YamlMappingNode UnityConnectSettings = (YamlMappingNode)yaml.Documents[0].RootNode["UnityConnectSettings"];
        Debug.Log("UnityAdsGameIds UnityConnectSettings " + UnityConnectSettings.ToString());
        // UnityAdsSettings
        YamlScalarNode currentNode = new YamlScalarNode("UnityAdsSettings");
        YamlMappingNode UnityAdsSettings = (YamlMappingNode)UnityConnectSettings.Children[currentNode];
        Debug.Log("UnityAdsGameIds UnityAdsSettings " + UnityAdsSettings.ToString());
        // m_GameIds
        currentNode = new YamlScalarNode("m_GameIds");
        YamlMappingNode GameIds = (YamlMappingNode)UnityAdsSettings.Children[currentNode];
        Debug.Log("UnityAdsGameIds GameIds " + GameIds.ToString());
        // iPhonePlayer / AndroidPlayer
        YamlScalarNode iOS_Node = (YamlScalarNode)GameIds.Children[new YamlScalarNode("iPhonePlayer")];
        YamlScalarNode Android_Node = (YamlScalarNode)GameIds.Children[new YamlScalarNode("AndroidPlayer")];

        result_id[0] = iOS_Node.ToString();
        result_id[1] = Android_Node.ToString();
        
        Debug.Log("UnityAdsGameIds iOS_Node " + result_id[0]);
        Debug.Log("UnityAdsGameIds Android_Node " + result_id[1]);
        return result_id;
    }
}