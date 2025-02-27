using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace AssignTabReducer
{
    [StaticConstructorOnStartup]
    public static class AssignTabReducer
    {
        static AssignTabReducer()
        {
            Harmony harmony = new Harmony("NachoToast.AssignTabReducer");

            harmony.Patch(
                original: AccessTools.PropertyGetter(
                    type: typeof(MainTabWindow_Assign),
                    name: "Pawns"),
                postfix: new HarmonyMethod(
                    methodType: typeof(AssignTabReducer),
                    methodName: nameof(GetAssignPawns_Postfix)));
        }

        private static void GetAssignPawns_Postfix(ref IEnumerable<Pawn> __result)
        {
            Map currentMap = Find.CurrentMap;

            if (currentMap == null)
            {
                return;
            }

            List<Pawn> newResult = new List<Pawn>();

            foreach (Pawn pawn in __result)
            {
                Map pawnMap = pawn?.Map;

                if (pawnMap == null || pawnMap == currentMap)
                {
                    newResult.Add(pawn);
                }
            }

            __result = newResult;
        }
    }
}