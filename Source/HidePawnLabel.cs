using System;
using HarmonyLib;
using LudeonTK;
using RimWorld;
using Verse;

namespace HidePawnLabel;

public class HidePawnLabel : Mod
{
    public HidePawnLabel(ModContentPack content) : base(content) {
        LongEventHandler.QueueLongEvent(action: HarmonyPatches,
            textKey: null,
            doAsynchronously: true,
            exceptionHandler: null
        );
    }

    [TweakValue("Graphics")] 
    private static bool PawnLabelHidden = true;


    private static void HarmonyPatches() {
        Harmony harmony = new Harmony("Hali.HidePawnLabel");

        Type patchType = typeof(HidePawnLabel);

        harmony.Patch(AccessTools.Method(typeof(PawnUIOverlay), nameof(PawnUIOverlay.DrawPawnGUIOverlay)),
            prefix: new HarmonyMethod(patchType, nameof(DrawPawnGUIOverlay_Prefix)));
    }


    private static bool DrawPawnGUIOverlay_Prefix() {
        return !PawnLabelHidden;
    }
}