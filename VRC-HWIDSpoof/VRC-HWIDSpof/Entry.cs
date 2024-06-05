using CoreRuntime.Interfaces;

namespace VRC_HWIDSpoof
{
    public class Entry : HexedCheat
    {
        public override void OnLoad(string[] args)
        {
            SystemInfoPatch.ApplyPatch();
        }
    }
}
