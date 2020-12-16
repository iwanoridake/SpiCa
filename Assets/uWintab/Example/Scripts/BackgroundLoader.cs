using System.Collections;
using UnityEngine;
using UnityEngine.Networking; // ←Unityが用意したネットワーク関連の名前空間

// 背景画像をサーバーから読み込み
public class BackgroundLoader : MonoBehaviour
{
    // テクスチャを設定するマテリアル
    public Material backgroundMaterial;

    IEnumerator Start()
    {
        // サーバーのURLを定義
        var serverURL = "/Users/mebarunrun/Documents/wintabtoka/test";

        // ユーザーの画像ファイル名取得リクエストを定義
        var getImageNameRequest = UnityWebRequest.Get(serverURL + "/get_image_name.php");
        // 通信を開始して待機
        yield return getImageNameRequest.SendWebRequest();
        // エラー判定
        if (getImageNameRequest.isNetworkError || getImageNameRequest.isHttpError) {
            // 通信エラーの際の処理
            Debug.LogError("画像ファイルの名前取得に失敗しました。");
        }
        // 画像ファイル名を取得
        var imageFileName = getImageNameRequest.downloadHandler.text;

        // 画像ファイルダウンロードリクエストを定義
        var imageDownloadHandler = new DownloadHandlerTexture(true);
        var downloadImageRequest = UnityWebRequest.Get(serverURL + "/" + imageFileName);
        downloadImageRequest.downloadHandler = imageDownloadHandler;

        // 通信を開始して待機
        yield return downloadImageRequest.SendWebRequest();
        // エラー判定
        if (downloadImageRequest.isNetworkError || downloadImageRequest.isHttpError) {
            // 通信エラーの際の処理
            Debug.LogError("画像のダウンロードに失敗しました。");
        }
        // 画像テクスチャを取得
        var imageTexture = imageDownloadHandler.texture;

        // テクスチャをマテリアルに設定
        backgroundMaterial.mainTexture = imageTexture;
    }
}
