# Guía de Integración Técnica: Gestión de Identidades y Firma Criptográfica en Unity para Internet Computer (ICP)

Este documento detalla el estándar de arquitectura y el flujo de trabajo técnico para integrar la autenticación unificada y la comunicación firmada criptográficamente con el backend desplegado en **Internet Computer Protocol (ICP)**.

---

## 1. Visión General de la Arquitectura

Para permitir un acceso transparente y libre de fricción (login anónimo sin formularios iniciales), la identidad de los jugadores se gestiona mediante un **par de claves asimétricas (Ed25519)** generado directamente en el dispositivo del usuario durante la primera ejecución.

```
[ Dispositivo Móvil / Unity ]                                   [ Canister Backend (ICP) ]
              │                                                             │
              │ 1. Solicita Iniciar Partida (StartMatch)                    │
              │────────────────────────────────────────────────────────────>│
              │                                                             │ 2. Genera MatchId único
              │                                                             │ 3. Registra Estado: "Active"
              │<── 4. Devuelve MatchId y Timestamp ─────────────────────────│
              │
   [ El usuario juega localmente ]
              │
              │ 5. Envía Resultado con MatchId (SubmitMatchResult)          │
              │    (Firmado con Ed25519Identity)                            │
              │────────────────────────────────────────────────────────────>│
              │                                                             │ 6. Valida: ¿MatchId existe?
              │                                                             │    Valida: ¿Está "Active"?
              │                                                             │    Valida: ¿Tiempo coherente?
              │                                                             │ 7. Invalida MatchId (Evita Replay)
              │<── 8. Respuesta: Puntos Acreditados / OK ───────────────────│
```

---

## 2. Dependencias Obligatorias

Para evitar conflictos de plataforma en iOS y Android con las librerías nativas de .NET/Mono, se requiere el uso de la librería de integración optimizada para C#:

* **Librería Cliente:** `ICP.NET`
* **Paquete de Unity:** `agent.unitypackage` (descargable desde la sección de Releases de [edjCase/ICP.NET](https://github.com/edjCase/ICP.NET)).

---

## 3. Flujo de Generación y Persistencia de Identidad Local

Cada equipo de desarrollo debe implementar un gestor de identidades (`ICPIdentityManager`) al inicio de la ejecución del juego.

### Reglas de Persistencia:
1. Al crear la identidad inicial (`Ed25519Identity.Generate()`), se deben extraer los bytes de la clave privada (`PrivateKey`) y de la clave pública (`PublicKey.DerBytes`).
2. Ambos arreglos de bytes deben almacenarse en formato Base64 en un sistema de almacenamiento seguro local (`EncryptedSharedPreferences` en Android / `Keychain` en iOS) o en `PlayerPrefs` durante fases de prueba.
3. En arranques posteriores, se reconstruye el objeto `Ed25519Identity` reutilizando las claves guardadas para mantener el mismo **Principal ID**.

### Código de Referencia para los Equipos (C# / Unity):

```csharp
using System;
using UnityEngine;
using EdjCase.ICP.Agent.Identities;

public class ICPIdentityManager : MonoBehaviour
{
    private const string PREF_KEY_PRIV = "icp_private_key_b64";
    private const string PREF_KEY_PUB = "icp_public_key_b64";

    public static ICPIdentityManager Instance { get; private set; }
    public IIdentity CurrentIdentity { get; private set; }
    public string PrincipalId => CurrentIdentity?.GetPrincipal().ToString();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeIdentity();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeIdentity()
    {
        if (PlayerPrefs.HasKey(PREF_KEY_PRIV) && PlayerPrefs.HasKey(PREF_KEY_PUB))
        {
            // Reconstrucción de la identidad desde el almacenamiento local
            byte[] privBytes = Convert.FromBase64String(PlayerPrefs.GetString(PREF_KEY_PRIV));
            byte[] pubBytes = Convert.FromBase64String(PlayerPrefs.GetString(PREF_KEY_PUB));

            CurrentIdentity = new Ed25519Identity(privBytes, pubBytes);
            Debug.Log($"[ICP] Identidad recuperada con éxito. Principal ID: {PrincipalId}");
        }
        else
        {
            // Generación de un nuevo par criptográfico de claves
            Ed25519Identity newIdentity = Ed25519Identity.Generate();

            byte[] privBytes = newIdentity.PrivateKey;
            byte[] pubBytes = newIdentity.PublicKey.DerBytes;

            PlayerPrefs.SetString(PREF_KEY_PRIV, Convert.ToBase64String(privBytes));
            PlayerPrefs.SetString(PREF_KEY_PUB, Convert.ToBase64String(pubBytes));
            PlayerPrefs.Save();

            CurrentIdentity = newIdentity;
            Debug.Log($"[ICP] Nueva identidad generada y almacenada. Principal ID: {PrincipalId}");
        }
    }
}
```

---

## 4. Requerimientos de Desarrollo para los Equipos de Juego

Cada proyecto de juego en Unity debe cumplir con los siguientes entregables internos:

1. **Script de Inicialización de Sesión:**
   * Garantizar que `ICPIdentityManager` se instancie en la primera escena cargada (Splash/Main Menu).
2. **Firmas y Llamadas al Backend:**
   * Todas las peticiones mutativas (actualización de puntuación, desbloqueo de logros, reclamo de tokens ICRC-2) deben incluir el payload y la firma digital firmada por `CurrentIdentity`.
3. **Módulo Progresivo de Recuperación (OAuth 2.0 / Google Sign-In):**
   * Crear un botón opcional en el menú de ajustes ("Vincular Cuenta") que permita enviar un token de Google al backend para vincular el email del jugador al `Principal ID` del dispositivo, garantizando la recuperación de la cuenta ante cambio de terminal.

---

## 5. Especificaciones de Comunicación con Canisters

* **Generación de Clientes C#:** Los equipos deben utilizar la herramienta `candid-client-generator` provista por ICP.NET a partir de los archivos `.did` (Candid) expuestos por el backend de ICP.
* **Tokens ICRC-2:** Las recompensas financieras y de juego se procesan directamente asociando el `Principal ID` de la identidad del usuario con la ledger del token.