/*------------------------------------------------------------*/
// <summary>GameCanvas for Unity</summary>
// <author>Seibe TAKAHASHI</author>
// <remarks>
// (c) 2015-2023 Smart Device Programming.
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php
// </remarks>
/*------------------------------------------------------------*/
#nullable enable

namespace GameCanvas
{
    public readonly partial struct GcSound : System.IEquatable<GcSound>
    {
        internal const int __Length__ = 13;
        public static readonly GcSound Click1 = new("GcSoundClick1");
        public static readonly GcSound Click2 = new("GcSoundClick2");
        public static readonly GcSound Soundbook = new("GcSoundSoundbook");
        public static readonly GcSound Soundcup = new("GcSoundSoundcup");
        public static readonly GcSound Sounddrinking = new("GcSoundSounddrinking");
        public static readonly GcSound Soundfu = new("GcSoundSoundfu");
        public static readonly GcSound Soundjan = new("GcSoundSoundjan");
        public static readonly GcSound Soundpencil = new("GcSoundSoundpencil");
        public static readonly GcSound Soundpiron = new("GcSoundSoundpiron");
        public static readonly GcSound SoundSlap_low = new("GcSoundSoundSlap_low");
        public static readonly GcSound Soundteren = new("GcSoundSoundteren");
        public static readonly GcSound Soundtraining = new("GcSoundSoundtraining");
        public static readonly GcSound Soundwind = new("GcSoundSoundwind");
    }
}
