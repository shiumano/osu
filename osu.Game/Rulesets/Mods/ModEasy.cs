// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework.Bindables;
using osu.Framework.Graphics.Sprites;
using osu.Game.Beatmaps;
using osu.Game.Configuration;
using osu.Game.Graphics;
using osu.Game.Overlays.Settings;

namespace osu.Game.Rulesets.Mods
{
    public abstract class ModEasy : Mod, IApplicableToDifficulty
    {
        public override string Name => "Easy";
        public override string Acronym => "EZ";
        public override IconUsage? Icon => OsuIcon.ModEasy;
        public override ModType Type => ModType.DifficultyReduction;
        public override double ScoreMultiplier => 0.5;
        public override Type[] IncompatibleMods => new[] { typeof(ModHardRock), typeof(ModDifficultyAdjust) };
        public override bool Ranked => UsesDefaultConfiguration;

        [SettingSource("Difficulty multiplier", "The actual decrease to apply", SettingControlType = typeof(MultiplierSettingsSlider))]
        public BindableNumber<double> DifficultyChange { get; } = new BindableDouble(0.5)
        {
            MinValue = 0.25,
            MaxValue = 0.99,
            Precision = 0.01,
        };

        public virtual void ReadFromDifficulty(BeatmapDifficulty difficulty)
        {
        }

        public virtual void ApplyToDifficulty(BeatmapDifficulty difficulty)
        {
            float ratio = (float)DifficultyChange.Value;
            difficulty.CircleSize *= ratio;
            difficulty.ApproachRate *= ratio;
            difficulty.DrainRate *= ratio;
            difficulty.OverallDifficulty *= ratio;
        }
    }
}
