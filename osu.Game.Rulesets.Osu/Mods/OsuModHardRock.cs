// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Linq;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Osu.Objects;
using osu.Game.Rulesets.Osu.Utils;

namespace osu.Game.Rulesets.Osu.Mods
{
    public class OsuModHardRock : ModHardRock, IApplicableToHitObject
    {
        public override double ScoreMultiplier => UsesDefaultConfiguration ? 1.06 : 1;

        public override Type[] IncompatibleMods => base.IncompatibleMods.Append(typeof(ModMirror)).ToArray();

        public void ApplyToHitObject(HitObject hitObject)
        {
            var osuObject = (OsuHitObject)hitObject;

            OsuHitObjectGenerationUtils.ReflectVerticallyAlongPlayfield(osuObject);
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
