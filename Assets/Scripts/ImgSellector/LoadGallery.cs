using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using TMPro;

public class LoadGallery : MonoBehaviour
{
    //[SerializeField] private RawImage img;

    public void OnClickButton() {
        NativeGallery.GetImageFromGallery((file) => {
            FileInfo selectedImg = new FileInfo(file);

            if (selectedImg.Length > 100000000) {
                return;
            }

            if (!string.IsNullOrEmpty(file)) {
                StartCoroutine(LoadImg(file));
            }
        });
    }


    IEnumerator LoadImg(string path) {
        yield return null;

        byte[] fileData = File.ReadAllBytes(path);
        //string fileName = Path.GetFileName(path).Split('.')[0];
        string fileName = "Origin";
        string savePath = Application.persistentDataPath + "/SourceImage";

        if (!Directory.Exists(savePath)) {
            Directory.CreateDirectory(savePath);
        }else {
            ClearAllFilesAndSubfolders(savePath);
        }

        if (string.IsNullOrEmpty(path) || !File.Exists(path)) {
            Debug.LogError("Invalid image path: " + path);
            yield break;
        }
        
        File.WriteAllBytes(savePath + '/' + fileName + ".png", fileData);

        /*Texture2D tex = NativeGallery.LoadImageAtPath(savePath + fileName + ".png", 1024);

        if (tex == null) {
            Debug.LogError("이미지 로드 실패: 포맷, 크기, 권한 등을 확인하세요");
            yield break;
        }

        img.texture = tex;
        SettingImgSize(img, 1000, 1000);*/
    }

    void SettingImgSize(RawImage img, float maxWidth, float maxHeight) {
        var imgX = img.rectTransform.sizeDelta.x;
        var imgY = img.rectTransform.sizeDelta.y;

        if (maxWidth / maxHeight > imgX / imgY) {
            img.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, maxHeight);
            img.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, imgX * (maxHeight/imgY));
        } else {
            img.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxWidth);
            img.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, imgY * (maxWidth / imgX));
        }
    }

    void ClearAllFilesAndSubfolders(string path) {
        if (!Directory.Exists(path))
            return;

        foreach (string file in Directory.GetFiles(path)) {
            try { File.Delete(file); } catch (System.Exception e) {
                Debug.LogWarning($"파일 삭제 실패: {file}\n{e.Message}");
            }
        }

        foreach (string dir in Directory.GetDirectories(path)) {
            ClearAllFilesAndSubfolders(dir);
        }
    }
}
