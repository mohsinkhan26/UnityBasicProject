/* 
 * Author : Mohsin Khan
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
 * BitBucket : https://bitbucket.org/mohsinkhan26/ 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MK.Common.Utilities
{
    public static class GameUtilities
    {
        #region Enum
        public static List<string> ParseEnum<T>()
        {
            return Enum.GetNames(typeof(T)).ToList();
        }

        public static T ParseEnum<T>(int _value)
        {
            return ParseEnum<T>(_value.ToString());
        }

        public static T ParseEnum<T>(string _value)
        {
            return (T)Enum.Parse(typeof(T), _value, true);
        }
        #endregion Enum

        public static Vector3 FindMidPoint(Vector3 v1, Vector3 v2)
        { // midpoint between v1 and v2
            return new Vector3((v1.x + v2.x) / 2.0f, (v1.y + v2.y) / 2.0f, (v1.z + v2.z) / 2.0f);
        }

        public static Vector3[] ParabolicPath(Vector3 _source, Vector3 _destination, bool _fromRight)
        {
            Vector3[] waypoints = new Vector3[3];
            waypoints[0] = _source;
            waypoints[1] = FindMidPoint(_source, _destination);
            if (_fromRight)
                waypoints[1] = new Vector3(waypoints[1].x, waypoints[1].y, waypoints[1].z + 10f);
            else
                waypoints[1] = new Vector3(waypoints[1].x, waypoints[1].y, waypoints[1].z - 10f);
            waypoints[2] = _destination;
            return waypoints;
        }

        #region Render Texture Format
        public static List<string> SupportedRenderTextures()
        {
            List<string> supportedRenderTextureFormats = new List<string>();
            List<string> renderTextureFormats = ParseEnum<RenderTextureFormat>();
            for (int i = renderTextureFormats.Count - 1; i >= 0; --i)
            {
                if (SystemInfo.SupportsRenderTextureFormat(ParseEnum<RenderTextureFormat>(renderTextureFormats[i])))
                {
                    Debug.Log("SupportedRenderTextures-i: " + i + "   " + renderTextureFormats[i] + ": " + SystemInfo.SupportsRenderTextureFormat(ParseEnum<RenderTextureFormat>(renderTextureFormats[i])));
                    supportedRenderTextureFormats.Add(renderTextureFormats[i]);
                }
            }
            return supportedRenderTextureFormats;
        }

        public static List<RenderTextureFormat> SupportedAlphaRenderTexturesWith()
        {
            List<RenderTextureFormat> supportedAlphaRenderTextureFormats = new List<RenderTextureFormat>();
            List<string> renderTextureFormats = SupportedRenderTextures();
            for (int i = renderTextureFormats.Count - 1; i >= 0; --i)
            {
                if (renderTextureFormats[i].IndexOf('A') >= 0)
                { // found
                    Debug.Log("SupportedAlphaRenderTexturesWith-i: " + i + "   " + renderTextureFormats[i]);
                    supportedAlphaRenderTextureFormats.Add(ParseEnum<RenderTextureFormat>(renderTextureFormats[i]));
                }
            }
            return supportedAlphaRenderTextureFormats;
        }
        #endregion Render Texture Format

    }
}
