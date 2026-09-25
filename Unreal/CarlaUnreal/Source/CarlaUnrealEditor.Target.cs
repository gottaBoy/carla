// Fill out your copyright notice in the Description page of Project Settings.

using UnrealBuildTool;
using System;
using EpicGames.Core;

[SupportedPlatforms("Win64", "Linux", "LinuxArm64", "Mac")]

// LinuxArm64 is an ARM64-first CARLA port; keep it explicitly in the Editor class.
public class CarlaUnrealEditorTarget : TargetRules
{
    [CommandLine("-unity-build")]
    bool EnableUnityBuild = true;

    private static void LogFlagStatus(string name, bool value)
    {
        var state = value ? "enabled" : "disabled";
        Console.WriteLine(string.Format("{0} is {1}.", name, state));
    }

    public CarlaUnrealEditorTarget(TargetInfo Target) :
        base(Target)
    {
        DefaultBuildSettings = BuildSettingsVersion.Latest;
        IncludeOrderVersion = EngineIncludeOrderVersion.Latest;
        Type = TargetType.Editor;

        // CEF3 ships only an x86-64 libcef for Linux; there is no aarch64 build.
        // Disable the embedded browser for the ARM64 editor (not needed to cook assets).
        bool bArm64Linux = Target.Platform == UnrealTargetPlatform.LinuxArm64 ||
            (Target.Platform == UnrealTargetPlatform.Linux && Architecture == UnrealArch.Arm64);
        if (bArm64Linux)
        {
            bCompileCEF3 = false;
            DisablePlugins.AddRange(new string[]
            {
                "BinkMedia",
                "ChangelistReview",
                "LevelSequenceEditor",
                "Niagara",
                "OodleNetwork",
                "PerforceSourceControl",
                "PythonScriptPlugin",
                "SequencerAnimTools",
                "SequencerScripting",
                "SpeedTreeImporter",
                "TemplateSequence",
            });
            bOverrideBuildEnvironment = true;
        }

        ExtraModuleNames.Add("CarlaUnreal");

        LogFlagStatus("Unity build", EnableUnityBuild);

        if (!EnableUnityBuild)
        {
            bUseUnityBuild =
            bForceUnityBuild =
            bUseAdaptiveUnityBuild = false;
        }
    }
}
