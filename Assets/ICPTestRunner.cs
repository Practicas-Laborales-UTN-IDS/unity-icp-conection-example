using UnityEngine;
using UnityEngine.UI;
using TMPro;
using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Agent.Identities;
using EdjCase.ICP.Candid.Models;
using My.Namespace.MyClient;
using My.Namespace.MyClient.Models;
using System;
using System.Threading.Tasks;
using System.Numerics;

public class ICPTestRunner : MonoBehaviour
{
    private const string PREF_KEY_PRIV = "icp_key_priv_b64";
    private const string PREF_KEY_PUB = "icp_key_pub_b64";
    private const long GameId = 1792881739670308783;

    private MyClientApiClient client;

    [Header("UI References")]
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private Button registerButton;

    async void Start()
    {
        Debug.Log("--- PROBANDO CLIENTE (ICP.NET + UnityWebRequest) ---");
        // Borrado de claves test
        // PlayerPrefs.DeleteAll();
        // 1. Identidad local persistente
        IIdentity identity = GetOrCreateICPIdentity();
        Debug.Log("Principal ID local: " + identity.GetPrincipal().ToString());

        // 2. Creamos el cliente HTTP personalizado usando UnityWebRequest apuntando a Mainnet
        Uri mainnetRoot = new Uri("https://ic0.app");
        UnityHttpClient unityHttpClient = new UnityHttpClient(mainnetRoot);

        // 3. Inicializamos el HttpAgent de ICP.NET
        HttpAgent agent = new HttpAgent(unityHttpClient, identity);

        // 4. Canister ID de Mainnet
        Principal canisterId = Principal.FromText("x6dpd-yaaaa-aaaap-qu2qa-cai");

        // 5. Instanciamos el cliente tipado
        client = new MyClientApiClient(agent, canisterId);

        // 6. Vinculamos el botón de la UI por código de forma limpia
        if (registerButton != null)
        {
            registerButton.onClick.AddListener(OnRegisterButtonClicked);
        }

        // 7. Prueba inicial de conexión
        await CallWhoAmI(client);
    }

    IIdentity GetOrCreateICPIdentity()
    {
        if (PlayerPrefs.HasKey(PREF_KEY_PRIV) && PlayerPrefs.HasKey(PREF_KEY_PUB))
        {
            try
            {
                byte[] privBytes = Convert.FromBase64String(PlayerPrefs.GetString(PREF_KEY_PRIV));
                byte[] pubBytes = Convert.FromBase64String(PlayerPrefs.GetString(PREF_KEY_PUB));
                
                Ed25519Identity loadedIdentity = new Ed25519Identity(pubBytes, privBytes);
                
                IIdentity identityInterface = loadedIdentity;
                Debug.Log("✅ Identidad recuperada. Principal ID restaurado: " + identityInterface.GetPrincipal().ToString());
                return loadedIdentity;
            }
            catch (Exception ex)
            {
                Debug.LogWarning("Error al restaurar las llaves: " + ex.Message);
            }
        }

        Ed25519Identity newIdentity = Ed25519Identity.Generate();
        
        byte[] privBytesOut = newIdentity.PrivateKey;
        byte[] pubBytesOut = newIdentity.PublicKey.PublicKey;

        PlayerPrefs.SetString(PREF_KEY_PRIV, Convert.ToBase64String(privBytesOut));
        PlayerPrefs.SetString(PREF_KEY_PUB, Convert.ToBase64String(pubBytesOut));
        PlayerPrefs.Save();

        IIdentity newIdentityInterface = newIdentity;
        Debug.Log("✅ Nueva identidad generada. Principal ID inicial: " + newIdentityInterface.GetPrincipal().ToString());
        
        return newIdentity;
    }

    async Task CallWhoAmI(MyClientApiClient clientInstance)
    {
        try
        {
            Debug.Log("Invocando método whoAmi mediante el cliente híbrido...");
            Principal backendPrincipal = await clientInstance.WhoAmi();
            Debug.Log("¡ÉXITO TOTAL! El canister respondió correctamente.");
            Debug.Log("Principal ID devuelto por el backend: " + backendPrincipal.ToString());
        }
        catch (Exception ex)
        {
            Debug.LogError("Error al invocar el canister: " + ex.Message);
        }
    }

    public async Task<SharedScore> GetPlayerScore()
    {
        if (client == null)
        {
            Debug.LogError("[ICP] El cliente no está inicializado.");
            return null;
        }

        Debug.Log("[ICP] Consultando score del jugador en la blockchain...");

        OptionalValue<Principal> userOpt = new OptionalValue<Principal>();

        var score = await client.GetScore(userOpt, GameId); 

        if (score != null)
        {
            Debug.Log($"[ICP] Partidas jugadas: {score.GamesPlayed}");
            Debug.Log($"[ICP] Puntos acumulados: {score.TotalPoints}");
        }

        return score;
    }

    private void OnRegisterButtonClicked()
    {
        if (usernameInputField != null)
        {
            string username = usernameInputField.text.Trim();
            if (!string.IsNullOrEmpty(username))
            {
                _ = RegisterUserOnBackend(username);
            }
            else
            {
                Debug.LogWarning("⚠️ El campo de nombre de usuario está vacío.");
            }
        }
    }

    public async Task RegisterUserOnBackend(string username)
    {
        if (client == null)
        {
            Debug.LogError("El cliente de ICP no está inicializado.");
            return;
        }

        try
        {
            Debug.Log($"Registrando el nombre de usuario '{username}' en el backend de Mainnet...");

            // Llamada asíncrona tipada a tu método SignUp del canister
            var result = await client.SignUp(username);

            Debug.Log("¡Usuario registrado con éxito en la blockchain de ICP! Resultado recibido.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error al registrar el usuario en el canister: " + ex.Message);
        }
    }


    public async Task<ulong> GetSeedFromBackend()
    {
        if (client == null)
        {
            throw new InvalidOperationException(
                "El cliente ICP no está inicializado.");
        }


        var response = await client.StartSessionGame(GameId);
        
        // Comprobar si el backend devolvió un error.
        if (response.Tag == MyClientApiClient.StartSessionGameReturnArg0Tag.Err)
        {
            throw new Exception($"Error del backend: {response.AsErr()}");
        }

        // Extraer los datos de la variante Ok.
        var session = response.AsOk();


        Debug.Log(response.GetType().FullName);

        foreach (var property in response.GetType().GetProperties())
        {
            Debug.Log(
                $"Propiedad: {property.Name} | Tipo: {property.PropertyType}"
            );
        }
        UnboundedInt seed = session.Seed;
        UnboundedInt expiresAt = session.ExpiresAt;

        Debug.Log($"Seed recibida: {seed}");
        Debug.Log($"Vencimiento (nanosegundos): {expiresAt}");

        BigInteger expiresAtNs = BigInteger.Parse(
            session.ExpiresAt.ToString()
        );

        BigInteger nowNs =
            new BigInteger(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds())
            * 1_000_000;

        BigInteger remainingNs = expiresAtNs - nowNs;

        double remainingSeconds = Math.Max(
            0,
            (double)remainingNs / 1_000_000_000.0
        );

        Debug.Log($"Tiempo restante: {remainingSeconds:F1} segundos");

        // Convertir la seed a ulong si el juego la necesita así.
        return ulong.Parse(seed.ToString());
    }

    public async System.Threading.Tasks.Task ReportGameResult()
    {
        Debug.Log("[ICP] Enviando resultado al canister...");


        // Temporalmente vacíos: el backend todavía no los valida.
        var payload = new System.Collections.Generic.List<byte>();
        var proof = new System.Collections.Generic.List<byte>();

        var response = await client.EndGame(
            GameId,
            payload,
            proof
        );

        if (response.Tag ==
            MyClientApiClient.EndGameReturnArg0Tag.Err)
        {
            throw new System.Exception(
                $"El canister rechazó el resultado: {response.AsErr()}"
            );
        }

        var reward = response.AsOk();

        Debug.Log($"[ICP] Recompensa procesada: {reward.Tag}");
    }

}