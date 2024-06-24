using CoreRuntime.Manager;
using ExitGames.Client.Photon;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Photon.Realtime;
using VRC.Core;

namespace VRC_NetCrypt.Hooks
{
    internal class VRCNetworkingClient_OpRaiseEvent
    {
        private delegate bool _OpRaiseEventDelegate(IntPtr instance, byte __0, IntPtr __1, IntPtr __2, SendOptions __3);
        private static _OpRaiseEventDelegate originalMethod;

        public static void ApplyPatch()
        {
            originalMethod = HookManager.Detour<_OpRaiseEventDelegate>(typeof(VRCNetworkingClient).GetMethod(nameof(VRCNetworkingClient.Method_Public_Virtual_New_Boolean_Byte_Object_RaiseEventOptions_SendOptions_0)), Patch);
        }

        private static bool Patch(IntPtr instance, byte __0, IntPtr __1, IntPtr __2, SendOptions __3)
        {
            Il2CppSystem.Object eventData = __1 == IntPtr.Zero ? null : new(__1);
            RaiseEventOptions raiseOptions = __2 == IntPtr.Zero ? null : new(__2);

            switch (__0)
            {
                case 1:
                    {
                        if (eventData == null) return false;

                        byte[] rawData = (byte[])Il2CppArrayBase<byte>.WrapNativeGenericArrayPointer(eventData.Pointer);

                        if (rawData.Length > 22)
                        {
                            Buffer.BlockCopy(BitConverter.GetBytes(0), 0, rawData, 0, 4);
                            Utils.EncryptOrDecrypt(rawData, 22, 26); // We can just partially encrypt it due opus encoding, this can be improved but lazy

                            eventData = Utils.Serialize(rawData);
                            __1 = eventData.Pointer;
                        }
                    }
                    break;

                case 12:
                    {
                        if (eventData == null) return false;

                        byte[] rawData = (byte[])Il2CppArrayBase<byte>.WrapNativeGenericArrayPointer(eventData.Pointer);

                        if (rawData.Length > 8)
                        {
                            Buffer.BlockCopy(BitConverter.GetBytes(0), 0, rawData, 0, 4);
                            Utils.EncryptOrDecrypt(rawData, 8);

                            eventData = Utils.Serialize(rawData);
                            __1 = eventData.Pointer;
                        }
                    }
                    break;
            }

            return originalMethod(instance, __0, __1, __2, __3);
        }
    }
}
