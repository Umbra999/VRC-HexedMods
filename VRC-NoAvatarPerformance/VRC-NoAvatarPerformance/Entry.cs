using CoreRuntime.Interfaces;

namespace VRC_NoAvatarPerformance
{
    public class Entry : HexedCheat
    {
        public override void OnLoad(string[] args)
        {
            ScannerPatch.ApplyPatch();
        }
    }
}
