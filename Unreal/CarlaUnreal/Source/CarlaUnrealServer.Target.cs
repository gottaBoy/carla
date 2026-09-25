// Fill out your copyright notice in the Description page in Project Settings.

using UnrealBuildTool;

public class CarlaUnrealServerTarget : TargetRules
{
    public CarlaUnrealServerTarget(TargetInfo Target) : base(Target)
    {
        Type = TargetType.Server;
        DefaultBuildSettings = BuildSettingsVersion.Latest;
        IncludeOrderVersion = EngineIncludeOrderVersion.Latest;

        ExtraModuleNames.Add("CarlaUnreal");
        DisablePlugins.AddRange(new string[]
        {
            "AnimationData",
            "CarlaTools",
            "ControlRig",
        });
    }
}
