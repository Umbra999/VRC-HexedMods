using CoreRuntime.Interfaces;
using VRC_NetCrypt.Hooks;

namespace VRC_NetCrypt
{
    public class Entry : HexedCheat
    {
        public override void OnLoad(string[] args = null)
        {
            VRCNetworkingClient_OnEvent.ApplyPatch();
            VRCNetworkingClient_OpRaiseEvent.ApplyPatch();
        }
    }
}
