using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Utility
{
    public static class SceenResponsive 
    {
        public static void Resize(Vector2 gridSize)
        {
            Camera mainCamera = Camera.main;
            float screenRatio = (float)Screen.width / (float)Screen.height;
            float targetRatio = gridSize.x / gridSize.y;

            if (screenRatio >= targetRatio)
            {
                mainCamera.orthographicSize = gridSize.y / 2;
            }
            else
            {
                float differenceInSize = targetRatio / screenRatio;
                mainCamera.orthographicSize = gridSize.y / 2 * differenceInSize;
            }

            mainCamera.orthographicSize += 1;
        }
    }
}