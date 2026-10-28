using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using EdjCase.ICP.Agent.Agents.Http;

public class UnityHttpClient : IHttpClient
{
    private readonly Uri baseUri;

    public UnityHttpClient(Uri baseUri)
    {
        this.baseUri = baseUri;
    }

    public async Task<HttpResponse> GetAsync(string url, CancellationToken? cancellationToken = null)
    {
        Uri fullUri = new Uri(baseUri, url);
        string fullUrl = fullUri.ToString();

        using (UnityWebRequest www = UnityWebRequest.Get(fullUrl))
        {
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Accept", "application/cbor");

            var operation = www.SendWebRequest();
            while (!operation.isDone)
            {
                if (cancellationToken.HasValue && cancellationToken.Value.IsCancellationRequested)
                {
                    www.Abort();
                    throw new TaskCanceledException();
                }
                await Task.Yield();
            }

            if (www.result != UnityWebRequest.Result.Success)
            {
                throw new Exception($"Error HTTP GetAsync [{www.responseCode}]: {www.error} - URL: {fullUrl}");
            }

            byte[] responseBytes = www.downloadHandler.data ?? new byte[0];
            return new HttpResponse((System.Net.HttpStatusCode)www.responseCode, () => Task.FromResult(responseBytes));
        }
    }

    public async Task<HttpResponse> PostAsync(string url, byte[] data, CancellationToken? cancellationToken = null)
    {
        Uri fullUri = new Uri(baseUri, url);
        string fullUrl = fullUri.ToString();

        using (UnityWebRequest www = new UnityWebRequest(fullUrl, "POST"))
        {
            www.downloadHandler = new DownloadHandlerBuffer();
            
            if (data != null && data.Length > 0)
            {
                // Creamos el upload handler y le asignamos el Content-Type explícitamente para UnityWebRequest
                UploadHandlerRaw uploadHandler = new UploadHandlerRaw(data);
                uploadHandler.contentType = "application/cbor"; 
                www.uploadHandler = uploadHandler;
            }
            
            www.SetRequestHeader("Content-Type", "application/cbor");
            www.SetRequestHeader("Accept", "application/cbor");

            var operation = www.SendWebRequest();
            while (!operation.isDone)
            {
                if (cancellationToken.HasValue && cancellationToken.Value.IsCancellationRequested)
                {
                    www.Abort();
                    throw new TaskCanceledException();
                }
                await Task.Yield();
            }

            if (www.result != UnityWebRequest.Result.Success)
            {
                string serverResponse = www.downloadHandler != null ? www.downloadHandler.text : "Sin respuesta";
                throw new Exception($"Error HTTP PostAsync [{www.responseCode}]: {www.error} | Detalle servidor: {serverResponse} - URL: {fullUrl}");
            }

            byte[] responseBytes = www.downloadHandler.data ?? new byte[0];
            return new HttpResponse((System.Net.HttpStatusCode)www.responseCode, () => Task.FromResult(responseBytes));
        }
    }
}