using CoreRuntime.Manager;

namespace VRC_RPCTags.Hooks
{
    internal class PlayerNet_OnNetworkReady
    {
        private delegate void _OnNetworkReadyDelegate(IntPtr instance);
        private static _OnNetworkReadyDelegate originalMethod;

        public static void ApplyPatch()
        {
            originalMethod = HookManager.Detour<_OnNetworkReadyDelegate>(typeof(PlayerNet).GetMethod(nameof(PlayerNet.OnNetworkReady)), Patch);
        }

        private static void Patch(IntPtr instance)
        {
            originalMethod(instance);

            PlayerNet player = instance == IntPtr.Zero ? null : new(instance);

            if (player == null) return;

            if (VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_Player_0.prop_String_0 == player.prop_Player_0.prop_String_0) PlayerTags.RequestTags();
        }
    }
}
