using ExitGames.Client.Photon;
using CoreRuntime.Manager;
using VRC.Core;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using static Il2CppSystem.Globalization.CultureInfo;

namespace VRC_NetCrypt.Hooks
{
    internal class VRCNetworkingClient_OnEvent
    {
        private delegate void _OnEventDelegate(IntPtr instance, IntPtr __0);
        private static _OnEventDelegate originalMethod;

        public static void ApplyPatch()
        {
            originalMethod = HookManager.Detour<_OnEventDelegate>(typeof(VRCNetworkingClient).GetMethod(nameof(VRCNetworkingClient.OnEvent)), Patch);
        }

        private static void Patch(IntPtr instance, IntPtr __0)
        {
            EventData eventData = __0 == IntPtr.Zero ? null : new(__0);

            if (eventData == null) return;

            switch (eventData.Code)
            {
                case 1:
                    {
                        if (eventData.CustomData == null) return;

                        byte[] rawData = (byte[])Il2CppArrayBase<byte>.WrapNativeGenericArrayPointer(eventData.customData.Pointer);

                        int viewID = BitConverter.ToInt32(rawData, 0);
                        if (viewID == 0 && rawData.Length > 22)
                        {
                            Buffer.BlockCopy(BitConverter.GetBytes(eventData.sender), 0, rawData, 0, 4);
                            Utils.EncryptOrDecrypt(rawData, 22, 26); // We can just partially encrypt it due opus encoding, this can be improved but lazy

                            eventData.customData = Utils.Serialize(rawData);
                            __0 = eventData.Pointer;
                        }
                    }
                    break;

                case 12:
                    {
                        if (eventData.CustomData == null) return;

                        byte[] rawData = (byte[])Il2CppArrayBase<byte>.WrapNativeGenericArrayPointer(eventData.customData.Pointer);

                        int viewID = BitConverter.ToInt32(rawData, 0);
                        if (viewID == 0 && rawData.Length > 8)
                        {
                            Buffer.BlockCopy(BitConverter.GetBytes(int.Parse(eventData.Sender + "00001")), 0, rawData, 0, 4);
                            Utils.EncryptOrDecrypt(rawData, 8);

                            eventData.customData = Utils.Serialize(rawData);
                            __0 = eventData.Pointer;
                        }
                    }
                    break;
            }

            originalMethod(instance, __0);
        }
    }
}
