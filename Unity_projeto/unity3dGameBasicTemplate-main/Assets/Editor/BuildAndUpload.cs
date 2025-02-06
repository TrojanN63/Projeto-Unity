using UnityEditor;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using System.Collections;

public class BuildAndUpload
{
    // URL do servidor onde o arquivo será enviado
    private static string uploadURL = "https://seuservidor.com/upload";

    // Caminho para salvar o build
    private static string buildPath = "Builds/";

    [MenuItem("Build/Build and Upload")]
    public static void BuildAndUploadFiles()
    {
        // Defina o nome e a plataforma do arquivo
        string fileName = "JogoBuild.exe";
        string fullPath = Path.Combine(buildPath, fileName);  // Caminho completo do build

        // Gera o build
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = new string[] { "Assets/Scenes/MainScene.unity" },  // Cena principal
            locationPathName = fullPath,
            target = BuildTarget.StandaloneWindows64,
            options = BuildOptions.None
        };

        BuildPipeline.BuildPlayer(buildPlayerOptions);
        Debug.Log("Build concluído!");

        // Envia o arquivo para o servidor
        UploadBuildToServer(fullPath);
    }

    // Função que envia o arquivo para o servidor
    private static void UploadBuildToServer(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError("Arquivo não encontrado: " + filePath);
            return;
        }

        byte[] fileData = File.ReadAllBytes(filePath);  // Lê o arquivo como array de bytes
        WWWForm form = new WWWForm();
        form.AddBinaryData("file", fileData, Path.GetFileName(filePath), "application/octet-stream");

        UnityWebRequest request = UnityWebRequest.Post(uploadURL, form);

        // Envia de forma assíncrona usando EditorApplication.update
        EditorCoroutineHelper.StartCoroutine(SendFile(request));
    }

    // Função para enviar o arquivo de forma assíncrona
    private static IEnumerator SendFile(UnityWebRequest request)
    {
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Arquivo enviado com sucesso!");
        }
        else
        {
            Debug.LogError("Erro ao enviar arquivo: " + request.error);
        }
    }
}

public static class EditorCoroutineHelper
{
    // Armazena o estado da corrotina
    private static IEnumerator currentCoroutine = null;

    // Inicia uma corrotina no editor
    public static void StartCoroutine(IEnumerator coroutine)
    {
        if (currentCoroutine != null) return;
        currentCoroutine = coroutine;
        EditorApplication.update += UpdateCoroutine;
    }

    // Atualiza a corrotina a cada frame do editor
    private static void UpdateCoroutine()
    {
        if (currentCoroutine == null) return;

        if (!currentCoroutine.MoveNext())
        {
            EditorApplication.update -= UpdateCoroutine;
            currentCoroutine = null;
        }
    }
}
