using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class ImageSlicer : MonoBehaviour {
    [SerializeField] GameObject loadingPage;
    [SerializeField] Slider processProgressionBar;
    [SerializeField] TMP_Text progressText; 

    public void SliceImg(int n) {
        StartCoroutine(ISliceImg(n));
    }

    private IEnumerator ISliceImg(int n) {
        int currentProcess = 0;
        int endProcess = n * n;
        loadingPage.SetActive(true);

        int sliceCount = n;//int.Parse(sliceCountText.text);

        string folderPath = Application.persistentDataPath + "/Image";

        string imageName = "Origin";

        if (!File.Exists(folderPath + '/' + imageName + ".png")) {
            Debug.LogError("Can't found a image at: " + folderPath + "/" + imageName);
            yield break;
        }

            Texture2D sourceImage = NativeGallery.LoadImageAtPath(folderPath + '/' + imageName + ".png", 1024, false);

        if (sourceImage == null) {
            Debug.LogError("Can't found a image at: " + folderPath + "/" + imageName);
            yield break;
        }

        if (sliceCount <= 0) {
            Debug.LogError("sliceCount must be greater than 0.");
            yield break;
        }

        string outputPath = Application.dataPath + "/SlicingResult/";//why dose it works??? how???
        if (!Directory.Exists(outputPath)) {
            Directory.CreateDirectory(outputPath);
        } else {
            ClearAllFilesAndSubfolders(outputPath);
        }

        int baseSliceWidth = sourceImage.width / sliceCount;
        int baseSliceHeight = sourceImage.height / sliceCount;
        int remainingWidth = sourceImage.width % sliceCount;
        int remainingHeight = sourceImage.height % sliceCount;

        for (int row = 0; row < sliceCount; row++) {
            int currentY = sourceImage.height - (row + 1) * baseSliceHeight;
            if (row == sliceCount - 1) {
                currentY = 0; 
            }

            int sliceHeight = (row == sliceCount - 1) ? baseSliceHeight + remainingHeight : baseSliceHeight;

            for (int col = 0; col < sliceCount; col++) {
                int currentX = col * baseSliceWidth;
                int sliceWidth = (col == sliceCount - 1) ? baseSliceWidth + remainingWidth : baseSliceWidth;

                Texture2D slice = new Texture2D(sliceWidth, sliceHeight);

                for (int x = 0; x < sliceWidth; x++) {
                    for (int y = 0; y < sliceHeight; y++) {
                        Color pixel = sourceImage.GetPixel(currentX + x, currentY + y);
                        slice.SetPixel(x, y, pixel);
                    }
                }

                slice.Apply();

                byte[] bytes = slice.EncodeToPNG();
                string fileName = Path.GetFileNameWithoutExtension(imageName) + "_slice_" + row + "_" + col + ".png";
                string filePath = Path.Combine(outputPath, fileName);
                File.WriteAllBytes(filePath, bytes);
                //Debug.Log("images have been successfully saved: " + filePath);

                Destroy(slice);
                currentProcess += 1;
                processProgressionBar.value = currentProcess / endProcess;
                progressText.text = (currentProcess / endProcess * 100).ToString("0.00") + '%';
                yield return null;
            }
        }

        #if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
        #endif
        
        loadingPage.SetActive(false);

        //This code is written for debugging purposes and will be deleted in alpha version

        //[SerializeField] RectTransform gridPanel;
        //[SerializeField] Vector2 cellSize = new Vector2(100, 100);
        //[SerializeField] int sliceCount = 4;
        //[SerializeField] TMP_Text sliceCountText;

        /*if (gridPanel == null) {
            Debug.LogWarning("Grid Panel is not assigned.");
            return;
        }

        foreach (Transform child in gridPanel) {
            Destroy(child.gameObject);
        }

        GridLayoutGroup gridGroup = gridPanel.GetComponent<GridLayoutGroup>();

        float fixedWidth = gridPanel.rect.width;
        float fixedHeight = gridPanel.rect.height;

        float cellWidth = (fixedWidth - (gridGroup.spacing.x * (sliceCount - 1))) / sliceCount;
        float cellHeight = (fixedHeight - (gridGroup.spacing.y * (sliceCount - 1))) / sliceCount;

        gridGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridGroup.constraintCount = sliceCount;
        gridGroup.cellSize = new Vector2(cellWidth, cellHeight);
        gridGroup.spacing = new Vector2(5, 5);


        string[] files = Directory.GetFiles(outputPath, "*.png");
        System.Array.Sort(files);

        foreach (string file in files) {
            byte[] bytes = File.ReadAllBytes(file);
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(bytes);

            GameObject imageObj = new GameObject(Path.GetFileName(file), typeof(RawImage));
            imageObj.transform.SetParent(gridPanel, false);
            RawImage rawImage = imageObj.GetComponent<RawImage>();
            rawImage.texture = tex;
        }*/
        //
    }

    void ClearAllFilesAndSubfolders(string path) {
        if (!Directory.Exists(path))
            return;

        foreach (string file in Directory.GetFiles(path)) {
            try { File.Delete(file); } catch (System.Exception e) {
                Debug.LogWarning($"File deletion failed: {file}\n{e.Message}");
            }
        }

        foreach (string dir in Directory.GetDirectories(path)) {
            ClearAllFilesAndSubfolders(dir);
        }
    }
}

