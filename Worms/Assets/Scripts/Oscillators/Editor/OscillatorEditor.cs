using UnityEditor;
using UnityEngine;

namespace Oscillators.Editor
{
    /// <summary>
    /// Custom Unity inspector for Oscillator.cs.
    /// </summary>
    [CustomEditor(typeof(Oscillator), true)]
    public class OscillatorEditor : UnityEditor.Editor
    {
        /// <summary>
        /// Draw the default inspector, with a clamped Vector3 on the forceScale.
        /// </summary>
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            Oscillator oscillator = (Oscillator) target;
            for (int i = 0; i < 3; i++)
            {
                oscillator.ForceScale[i] = (int)Mathf.Clamp01(oscillator.ForceScale[i]);
            }
        }
    }
}
