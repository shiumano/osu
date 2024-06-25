// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework.Bindables;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using osu.Game.Beatmaps;
using osu.Game.Configuration;
using osu.Game.Graphics;
using osu.Game.Overlays.Settings;

namespace osu.Game.Rulesets.Mods
{
    public abstract class ModHardRock : Mod, IApplicableToDifficulty
    {
        public override string Name => "Hard Rock";
        public override string Acronym => "HR";
        public override IconUsage? Icon => OsuIcon.ModHardRock;
        public override ModType Type => ModType.DifficultyIncrease;
        public override LocalisableString Description => "Everything just got a bit harder...";
        public override Type[] IncompatibleMods => new[] { typeof(ModEasy), typeof(ModDifficultyAdjust) };
        public override bool Ranked => UsesDefaultConfiguration;

        [SettingSource("Difficulty multiplier", "The actual increase to apply", SettingControlType = typeof(MultiplierSettingsSlider))]
        public BindableNumber<double> DifficultyChange { get; } = new BindableDouble(1.4)
        {
            MinValue = 1.01,
            MaxValue = 2.00,
            Precision = 0.01,
        };

        public void ReadFromDifficulty(IBeatmapDifficultyInfo difficulty)
        {
        }

        public virtual void ApplyToDifficulty(BeatmapDifficulty difficulty)
        {
            float ratio = (float)DifficultyChange.Value;
            difficulty.DrainRate = Math.Min(difficulty.DrainRate * ratio, 10.0f);
            difficulty.OverallDifficulty = Math.Min(difficulty.OverallDifficulty * ratio, 10.0f);
        }
    }
}
