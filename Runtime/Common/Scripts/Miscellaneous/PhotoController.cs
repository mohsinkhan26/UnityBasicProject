/* 
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/ 
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
*/
using System.IO;
using UnityEngine;
using MK.Common.Helpers;

namespace MK.Common.Miscellaneous
{
    public class PhotoController : MonoBehaviour
    {
        const string IMAGE_EXTENSION = ".jpg";

        public bool SaveImage(string imageName, Texture2D texture)
        { // expecting name with extension
            string fileWithPath = ResourceHelper.GetPersistentDataPath(imageName);
            File.WriteAllBytes(fileWithPath, texture.EncodeToJPG());
            return true;
        }
        
        public bool SaveJPGImage(string imageName, Texture2D texture)
        {
            string fileWithPath = ResourceHelper.GetPersistentDataPath(imageName, IMAGE_EXTENSION);
            File.WriteAllBytes(fileWithPath, texture.EncodeToJPG());
            return true;
        }

        public bool FileExists(string imageName)
        { // expecting name with extension
            return ResourceHelper.ExistsInPersistentData(imageName);
        }

        public bool JPGFileExists(string imageName)
        {
            return ResourceHelper.ExistsInPersistentData(imageName, IMAGE_EXTENSION);
        }

        public Texture2D GetImage(string imageName)
        { // expecting name with extension
            byte[] bytes = File.ReadAllBytes(ResourceHelper.GetPersistentDataPath(imageName));
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(bytes);
            return texture;
        }

        public Texture2D GetJPGImage(string imageName)
        {
            byte[] bytes = File.ReadAllBytes(ResourceHelper.GetPersistentDataPath(imageName, IMAGE_EXTENSION));
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(bytes);
            return texture;
        }
    }
}
