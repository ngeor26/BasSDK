﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.XR;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#else
using TriInspector;
#endif

namespace ThunderRoad
{
    [HelpURL("https://kospy.github.io/BasSDK/Components/ThunderRoad/Levels/Mirror.html")]
    public class Mirror : MonoBehaviour
    {
        // A simple hook for armour edit events if required.
        public delegate void OnArmourEditModeChanged(bool state);
        public delegate void OnRenderStateChanged(bool state);

        public static Mirror local;

        [Tooltip("When enabled, occlusion culling is enabled when the player is in the mirror")]
        public bool useOcclusionCulling = false;
        [Tooltip("When enabled, player can change their clothing when in front of the mirror.")]
        public bool allowArmourEditing = false;
        [Space]
        [Tooltip("Depicts the direction the reflection is pointing for OcclusionCulling to take effect")]
        public ReflectionDirection reflectionDirection = ReflectionDirection.Up;
        [Tooltip("Depicts the width/height of the mirror.")]
        public Vector2 widthAndHeight = Vector2.one;
        [Range(0, 1)]
        [Tooltip("Depicts the quality of the mirror reflection")]
        public float quality = 1;
        [Range(0, 1)]
        [Tooltip("Adjusts the intensity of the grain/dirt on the mirror(?)")]
        public float Intensity = 1f;
        [Tooltip("When enabled, the reflection will reflect everything without any global illumination on the scene")]
        public bool reflectionWithoutGI = true;
        [Tooltip("Adjusts the mirror reflection's anti-aliasing")]
        public int antiAliasing = 4;
        [Tooltip("Adjusts the anisotropic filtering of the mirror reflection")]
        public FilterMode filterMode = FilterMode.Bilinear;
        [Tooltip("Depicts flags that the mirror will avoid rendering in its' reflection.")]
        public CameraClearFlags clearFlags = CameraClearFlags.Skybox;
        public int rendererIndex = 2;
        [Tooltip("When enabled, the mirror reflection will render shadows")]
        public bool shadow = true;
        [Tooltip("When enabled, the mirror reflection will render fog.")]
        public bool enableFog = true;
        [Tooltip("When enabled, and if armor/clothing editing is allowed, highlighting over the armor will make a white outline in the mirror to depict what clothing is changed.")]
        public bool showWearableHighlight = true;

        public Color backgroundColor = Color.black;
        [Tooltip("Depicts what layers are culled from reflection rendering")]
        public LayerMask cullingMask = ~0;
        [Tooltip("Depict what mesh the mirror reflects from")]
        public MeshRenderer mirrorMesh;
        [Tooltip("A list of specific renderers to hide from the mirror reflection")]
        public MeshRenderer[] meshToHide;
        private Vector3 reflectionLocalDirection;
        private Vector3 reflectionWorldDirection;

        private bool hasInvokedEvent; // Used to prevent multiple events from being invoked.
        public event OnArmourEditModeChanged OnArmourEditModeChangedEvent;
        public event OnRenderStateChanged OnRenderStateChangedEvent;

        [NonSerialized]
        public bool isRendering;

        internal protected bool active = true;

        public bool stopDuringCreaturePartUpdate = true;
        protected bool creaturePartUpdating;

        public enum Side
        {
            Left, //have left first to match Unity's convention if using them as ints
            Right
        }

        public enum ReflectionDirection
        {
            Up,
            Down,
            Forward,
            Back,
            Left,
            Right,
        }

        void OnValidate()
        {
            if (!gameObject.activeInHierarchy) return;
            Refresh();
        }

        [Button]
        public void SetActive(bool active)
        {
            this.active = active;
        }

        [ContextMenu("Refresh")]
        public void Refresh()
        {
            if (reflectionDirection == ReflectionDirection.Forward)
                reflectionLocalDirection = Vector3.forward;
            else if (reflectionDirection == ReflectionDirection.Up)
                reflectionLocalDirection = Vector3.up;
            else if (reflectionDirection == ReflectionDirection.Back)
                reflectionLocalDirection = Vector3.back;
            else if (reflectionDirection == ReflectionDirection.Down)
                reflectionLocalDirection = Vector3.down;
            else if (reflectionDirection == ReflectionDirection.Left)
                reflectionLocalDirection = Vector3.left;
            else if (reflectionDirection == ReflectionDirection.Right)
                reflectionLocalDirection = Vector3.right;
            reflectionWorldDirection = this.transform.TransformDirection(reflectionLocalDirection);
        }

        public static void DrawGizmoArrow(Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
        {
            Gizmos.color = color;
            Gizmos.DrawRay(pos, direction);
            Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Gizmos.DrawRay(pos + direction, right * arrowHeadLength);
            Gizmos.DrawRay(pos + direction, left * arrowHeadLength);
        }
    }
}