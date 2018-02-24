// Date   : 24.02.2018 09:31
// Project: snipergame
// Author : bradur

using UnityEngine;
using System.Collections;
using System.IO;

public class TakeTransparentScreenShot : MonoBehaviour
{

    void Start()
    {

    }

    private bool takeShot = false;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            takeShot = true;
        }
    }

    private void OnPostRender()
    {
        if (takeShot)
        {
            takeShot = false;
            //GetComponent<Camera>().clearFlags = CameraClearFlags.Depth;
            RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 32);
            Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.ARGB32, false);
            screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
            screenShot.Apply();
            byte[] pngShot = screenShot.EncodeToPNG();
            File.WriteAllBytes(Application.dataPath + "/" + screenShot.ToString() + "_" + Random.Range(0, 1024).ToString() + ".png", pngShot);
            Destroy(screenShot);
        }
    }
}
