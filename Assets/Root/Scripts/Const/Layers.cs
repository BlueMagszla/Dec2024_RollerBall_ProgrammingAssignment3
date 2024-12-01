public static partial class Const
{
    public static partial class Layers
    {
        // UNITY DEFAULT LAYERS
        public const string Default = "Default";
        public const string TransparentFX = "TransparentFX";
        public const string IgnoreRaycast = "IgnoreRaycast";
        public const string Water = "Water";
        public const string UI = "UI";

        // Layer Indexes
        public const int DefaultIndex = 0;
        public const int TransparentFXIndex = 1;
        public const int IgnoreRaycastIndex = 2;
        public const int WaterIndex = 4;
        public const int UIIndex = 5;

        // Layer Masks
        public const int DefaultMask = 1 << DefaultIndex;
        public const int TransparentFXMask = 1 << TransparentFXIndex;
        public const int IgnoreRaycastMask = 1 << IgnoreRaycastIndex;
        public const int WaterMask = 1 << WaterIndex;
        public const int UIMask = 1 << UIIndex;
    }
}