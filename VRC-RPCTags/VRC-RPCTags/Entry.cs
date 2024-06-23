using CoreRuntime.Interfaces;
using VRC_RPCTags.Hooks;

namespace VRC_RPCTags
{
    public class Entry : HexedCheat
    {
        public override void OnLoad(string[] args = null)
        {
            PlayerNet_OnNetworkReady.ApplyPatch();
            VRC_EventDispatcherRFC_ValidateAndTriggerEvent.ApplyPatch();
        }
    }
}
