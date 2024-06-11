using CoreRuntime.Interfaces;

namespace VRC_AmplitudeBlock
{
    public class Entry : HexedCheat
    {
        public override void OnLoad(string[] args)
        {
            AmplitudeWrapperPatch.ApplyPatch();
        }
    }
}
