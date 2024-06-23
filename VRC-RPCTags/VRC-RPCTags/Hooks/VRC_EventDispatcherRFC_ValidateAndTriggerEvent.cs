using CoreRuntime.Manager;
using VRC.SDKBase;

namespace VRC_RPCTags.Hooks
{
    internal class VRC_EventDispatcherRFC_ValidateAndTriggerEvent
    {
        private delegate void _ValidateAndTriggerEventDelegate(IntPtr instance, IntPtr __0, IntPtr __1, VRC_EventHandler.VrcBroadcastType __2, int __3, float __4);
        private static _ValidateAndTriggerEventDelegate originalMethod;

        public static void ApplyPatch()
        {
            originalMethod = HookManager.Detour<_ValidateAndTriggerEventDelegate>(typeof(VRC_EventDispatcherRFC).GetMethod(nameof(VRC_EventDispatcherRFC.Method_Public_Void_Player_VrcEvent_VrcBroadcastType_Int32_Single_0)), Patch);
        }

        private static void Patch(IntPtr instance, IntPtr __0, IntPtr __1, VRC_EventHandler.VrcBroadcastType __2, int __3, float __4)
        {
            originalMethod(instance, __0, __1, __2, __3, __4);

            VRC.Player player = __0 == IntPtr.Zero ? null : new(__0);
            VRC_EventHandler.VrcEvent vrcEvent = __1 == IntPtr.Zero ? null : new(__1);

            if (player == null || vrcEvent == null) return;

            if (vrcEvent.EventType != VRC_EventHandler.VrcEventType.SendRPC) return;

            if (vrcEvent.ParameterObject != player.gameObject) return;

            if (vrcEvent.ParameterString == null || vrcEvent.ParameterString.Length > 50) return;

            if (vrcEvent.ParameterString == "RequestTags") PlayerTags.BroadcastTag(PlayerTags.ReadTagFromFile());
            else if  (vrcEvent.ParameterString.StartsWith("UpdateTag"))
            {
                if (!vrcEvent.ParameterString.Contains(":")) return;

                string Tag = vrcEvent.ParameterString.Split(':')[1];
                PlayerTags.OnReceivedTag(player._vrcplayer, Tag);
            }
        }
    }
}
