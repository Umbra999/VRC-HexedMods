using CoreRuntime.Manager;
using VRC.SDKBase.Validation.Performance;

namespace VRC_NoAvatarPerformance
{
    internal class ScannerPatch
    {
        private delegate IntPtr _GetPerformanceScannerSetDelegate(bool __0);

        public static void ApplyPatch()
        {
            HookManager.Detour<_GetPerformanceScannerSetDelegate>(typeof(AvatarPerformance).GetMethod(nameof(AvatarPerformance.GetPerformanceScannerSet)), Patch);
        }

        private static IntPtr Patch(bool __0)
        {
            return IntPtr.Zero;
        }
    }
}
