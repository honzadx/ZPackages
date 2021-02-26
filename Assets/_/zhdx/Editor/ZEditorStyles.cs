using UnityEngine;

namespace UnityEditor
{
    public class ZEditorStyles
    {
        public static readonly float LINE_HEIGHT = 20f;
        public static readonly float HORIZONTAL_HIERARCHY_SPACE = 50f;
        public static readonly Color INACTIVE_BOX_COLOR = new Color(.1f, .1f, .1f, .1f);
        public static readonly Color ACTIVE_BOX_COLOR = new Color(.1f, .1f, .1f, .4f);
        public static readonly Color ERROR_BOX_COLOR = new Color(1f, .1f, .1f, .4f);
        public static readonly Color HEADER_TRANSLATION = Color.blue;
        public static readonly Color HEADER_AUDIO = new Color(1, 0.5f, 0);

        public static void DrawHeader()
        {

        }
    }
}