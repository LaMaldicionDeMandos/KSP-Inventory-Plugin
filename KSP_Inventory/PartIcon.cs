using System;
using System.Collections.Generic;
using UnityEngine;
namespace inventory
{
    public class PartIcon
    {
        private static Dictionary<AvailablePart, PartIcon> cache = new Dictionary<AvailablePart, PartIcon>();
        private static Light iconLight;
        private static float globalCameraShift;

        private const int CameraLayer = 22;
        private const float IconPosY = 0;
        private const float LightIntensity = 0.4f;
        private const float CameraZoom = 0.75f;

        private Camera camera;
        private float cameraShift;
        private GameObject iconPrefab;
        private RenderTexture cameraTarget;

        public readonly AvailablePart part;
        public readonly GUIContent content;

        public static PartIcon GetPartIcon(AvailablePart part)
        {
            if (cache.ContainsKey(part)) return cache[part];
            PartIcon icon = new PartIcon(part, 48);
            cache.Add(part, icon);
            return icon;
        }

        private PartIcon(AvailablePart part, int resolution)
        {
            this.part = part;
            MakePartIcon(part, resolution);
            this.content = new GUIContent(texture, part.title);
        }

        private void MakePartIcon(AvailablePart avPart, int resolution)
        {
            // Instantiate part icon
            iconPrefab = UnityEngine.Object.Instantiate(avPart.iconPrefab);
            iconPrefab.SetActive(true);

            // Command Seat Icon Fix (Temporary workaround until squad fix the broken shader)
            Shader fixShader = Shader.Find("KSP/Alpha/Cutoff Bumped");
            foreach (Renderer r in iconPrefab.GetComponentsInChildren<Renderer>(true))
            {
                foreach (Material m in r.materials)
                {
                    if (m.shader.name == "KSP/Alpha/Cutoff")
                    {
                        m.shader = fixShader;
                    }
                }
            }

            // Icon Camera
            cameraShift = ReserveCameraSpot();
            GameObject camGo = new GameObject("KASCamItem" + cameraShift);
            camGo.transform.position = new Vector3(cameraShift, IconPosY, 0);
            camGo.transform.rotation = Quaternion.identity;
            camera = camGo.AddComponent<Camera>();
            camera.orthographic = true;
            camera.orthographicSize = CameraZoom;
            camera.clearFlags = CameraClearFlags.Color;
            camera.enabled = false;
            cameraTarget = new RenderTexture(resolution, resolution, 8);

            // Layer
            camera.cullingMask = 1 << CameraLayer;
            SetLayerRecursively(iconPrefab, CameraLayer);

            // Texture
            camera.targetTexture = cameraTarget;
            camera.ResetAspect();

            ResetPos();
        }

        private void ResetPos()
        {
            iconPrefab.transform.position = new Vector3(cameraShift, IconPosY, 2f);
            iconPrefab.transform.rotation = Quaternion.Euler(-15f, 0.0f, 0.0f);
            iconPrefab.transform.Rotate(0.0f, -30f, 0.0f);
            camera.Render();  // Update snapshot.
        }

        public Texture texture
        {
            get
            {
                if (cameraTarget != null && !cameraTarget.IsCreated())
                {
                    camera.Render();
                }
                return cameraTarget;
            }
        }

        private void SetLayerRecursively(GameObject obj, int newLayer)
        {
            if (null == obj)
            {
                return;
            }
            obj.layer = newLayer;
            foreach (Transform child in obj.transform)
            {
                if (null == child)
                {
                    continue;
                }
                SetLayerRecursively(child.gameObject, newLayer);
            }
        }

        private float ReserveCameraSpot()
        {
            //light
            if (iconLight == null && HighLogic.LoadedSceneIsFlight)
            {
                GameObject lightGo = new GameObject("KASLight");
                iconLight = lightGo.AddComponent<Light>();
                iconLight.cullingMask = 1 << CameraLayer;
                iconLight.type = LightType.Directional;
                iconLight.intensity = LightIntensity;
                iconLight.shadows = LightShadows.None;
                iconLight.renderMode = LightRenderMode.ForcePixel;
            }

            globalCameraShift += 2.0f;

            return globalCameraShift;
        }

    }
}
