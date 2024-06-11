using CoreRuntime.Manager;
using System.Reflection;
using AmplitudeSDKWrapper;

namespace VRC_AmplitudeBlock
{
    internal class AmplitudeWrapperPatch
    {
        private delegate void _ReturnAllDelegate();

        public static void ApplyPatch()
        {
            foreach (MethodInfo method in typeof(AmplitudeWrapper).GetMethods().Where(x => x.Name.StartsWith("Init") && x.Name != "InitializeDeviceId" || x.Name.Contains("Start") || x.Name.Contains("End") || x.Name.Contains("UpdateServer") || x.Name.Contains("PostEvents") || x.Name.Contains("LogEvent") || x.Name.Contains("SaveEvent") || x.Name.Contains("SaveAndUpload") || x.Name.StartsWith("Set")))
            {
                HookManager.Detour<_ReturnAllDelegate>(method, Patch);
            }
        }

        private static void Patch()
        {
            
        }
    }
}
