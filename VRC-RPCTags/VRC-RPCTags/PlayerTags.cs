using UnityEngine;
using TMPro;
using VRC.SDKBase;
using static VRC.SDKBase.VRC_EventHandler;

namespace VRC_RPCTags
{
    internal class PlayerTags
    {
        private static Transform MakeTag(Transform stats, int index, bool background)
        {
            Transform rank = UnityEngine.Object.Instantiate(stats, stats.parent);
            if (rank == null) return null;

            rank.name = $"RPCTag-{index}";
            rank.localPosition = new Vector3(0, 30 * (index + 1), 0);
            rank.gameObject?.SetActive(true);

            ImageThreeSlice image = rank.GetComponent<ImageThreeSlice>();
            if (image != null) image.enabled = background;

            Transform textGO = null;
            for (int i = rank.childCount; i > 0; i--)
            {
                Transform child = rank.GetChild(i - 1);
                if (child.name == "Trust Text") textGO = child;
                else UnityEngine.Object.Destroy(child.gameObject);
            }

            return textGO;
        }

        private static void SetTag(ref int stack, Transform stats, Transform contents, Color color, string content)
        {
            Transform tag = contents.Find($"RPCTag-{stack}");

            Transform label;
            if (tag == null) label = MakeTag(stats, stack, false);
            else
            {
                tag.gameObject.SetActive(true);
                label = tag.Find("Trust Text");
            }

            TextMeshProUGUI text = label.GetComponent<TextMeshProUGUI>();
            text.color = color;
            text.text = content;

            stack++;
        }

        public static void OnReceivedTag(VRCPlayer player, string Tag)
        {
            Transform contents = player.field_Public_PlayerNameplate_0.transform.Find("Contents");
            Transform stats = contents.Find("Quick Stats");
            int stack = 1;

            SetTag(ref stack, stats, contents, Color.cyan, Tag);

            stats.localPosition = new Vector3(0, (stack + 1) * 30, 0);
        }

        private static void SendRPC(VrcEventType EventType, string Name, bool ParameterBool, VrcBooleanOp BoolOP, GameObject ParamObject, GameObject[] ParamObjects, string ParamString, float Float, int Int, byte[] bytes, VrcBroadcastType BroadcastType, float Fastforward = 0)
        {
            VrcEvent a = new()
            {
                EventType = EventType,
                Name = Name,
                ParameterBool = ParameterBool,
                ParameterBoolOp = BoolOP,
                ParameterBytes = bytes,
                ParameterObject = ParamObject,
                ParameterObjects = ParamObjects,
                ParameterString = ParamString,
                ParameterFloat = Float,
                ParameterInt = Int,
            };
            Networking.SceneEventHandler.TriggerEvent(a, BroadcastType, ParamObject, Fastforward);
        }

        public static void BroadcastTag(string Tag)
        {
            SendRPC(VrcEventType.SendRPC, "", false, VrcBooleanOp.Unused, VRCPlayer.field_Internal_Static_VRCPlayer_0.gameObject, null, $"UpdateTag:{Tag}", 0, 0, null, VrcBroadcastType.AlwaysUnbuffered, 0); // TODO: split by ':' to support multiple tags
        }

        public static void RequestTags()
        {
            SendRPC(VrcEventType.SendRPC, "", false, VrcBooleanOp.Unused, VRCPlayer.field_Internal_Static_VRCPlayer_0.gameObject, null, "RequestTags", 0, 0, null, VrcBroadcastType.AlwaysUnbuffered, 0);
        }

        public static string ReadTagFromFile()
        {
            if (!File.Exists("HexedTag.txt")) File.WriteAllText("HexedTag.txt", "Hexed Tag User");
            return File.ReadAllText("HexedTag.txt");
        }
    }
}
