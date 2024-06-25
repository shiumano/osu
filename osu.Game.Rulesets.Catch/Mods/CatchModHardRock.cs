// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Game.Rulesets.Mods;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Catch.Beatmaps;

namespace osu.Game.Rulesets.Catch.Mods
{
    public class CatchModHardRock : ModHardRock, IApplicableToBeatmapProcessor
    {
        public override double ScoreMultiplier => UsesDefaultConfiguration ? 1.12 : 1;

        public void ApplyToBeatmapProcessor(IBeatmapProcessor beatmapProcessor)
        {
            var catchProcessor = (CatchBeatmapProcessor)beatmapProcessor;
            catchProcessor.HardRockOffsets = true;
        }

        public override void ApplyToDifficulty(BeatmapDifficulty difficulty)
        {
            base.ApplyToDifficulty(difficulty);

            float ratio = (float)DifficultyChange.Value;
            float cs_ratio = ratio * 1.3f / 1.4f; // CS used a custom 1.3 ratio. The original ratio will be retained.

            difficulty.CircleSize = Math.Min(difficulty.CircleSize * cs_ratio, 10.0f);
            difficulty.ApproachRate = Math.Min(difficulty.ApproachRate * ratio, 10.0f);
        }
    }
}
