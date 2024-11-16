using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestSuite
{
    private PlayerController playerController;
    private GameObject player;

    private GameManager gameManager;
    private GameObject killZone;

    private GameObject camera;

    [SetUp]
    public void Setup()
    {
        // Instancia el prefab del jugador desde Resources
        player = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefabs/Player"));
        playerController = player.GetComponent<PlayerController>();

        // Inicializa otros componentes necesarios
        playerController.rb = playerController.GetComponent<Rigidbody2D>();
        playerController.groundCheck = playerController.transform;
        playerController.groundLayer = LayerMask.GetMask("Ground");

        // Configurar valores predeterminados
        playerController.moveSpeed = 5f;

        // Objeto de zona de muerte
        gameManager = new GameObject().AddComponent<GameManager>();
        killZone = new GameObject();
        killZone.AddComponent<BoxCollider2D>();
        killZone.tag = "killzone";

        camera = new GameObject("camera");
        camera.AddComponent<Camera>();
        camera.transform.position = new Vector3(-3, 4, -10);

        //player.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    [UnityTest]
    public IEnumerator PlayerJumpTest()
    {

        player.transform.position = new Vector3(0, 5, -1);

        float initialYPosition = playerController.transform.position.y;

        // Llama al método de salto
        playerController.Jump(playerController.jumpForce);

        // Espera un frame para permitir la simulación
        yield return new WaitForSeconds(0.2f);

        //Verifica que la posicion de y sea mayor a la inicial
        Assert.Greater(playerController.transform.position.y, initialYPosition, "El jugador debería saltar");
    }

    [UnityTest]
    public IEnumerator PlayerMoveRightTest()
    {
        player.transform.position = new Vector3(0, 5, -1);
        // Posición inicial
        Vector3 initialPosition = playerController.transform.position;

        // Configurar las variables necesarias
        playerController.moveSpeed = 5f; // Ajusta la velocidad para pruebas

        // Simula la entrada
        playerController.testMoveX = 1f; // Movimiento hacia la derecha

        // Llama al método de movimiento
        playerController.MovePlayer();

        // Espera un frame
        yield return new WaitForSeconds(2f);

        // Comprueba que el jugador se haya movido hacia la derecha
        Assert.Greater(playerController.transform.position.x, initialPosition.x, "El jugador debería haberse movido hacia la derecha.");
    }

    [UnityTest]
    public IEnumerator PlayerMoveLeftTest()
    {
        player.transform.position = new Vector3(0, 5, -1);
        // Posición inicial
        Vector3 initialPosition = playerController.transform.position;

        // Configurar las variables necesarias
        playerController.moveSpeed = 5f; // Ajusta la velocidad para pruebas

        // Simula la entrada
        playerController.testMoveX = -1f; // Movimiento hacia la izquierda

        // Llama al método de movimiento
        playerController.MovePlayer();

        // Espera un frame
        yield return new WaitForSeconds(2f);

        // Comprueba que el jugador se haya movido hacia la izquierda
        Assert.Less(playerController.transform.position.x, initialPosition.x, "El jugador debería haberse movido hacia la izquierda.");
    }

    [UnityTest]
    public IEnumerator GameOverDetection()
    {
        player.transform.position = new Vector3(0,5,-1);
        killZone.transform.position = new Vector3(0,0,0);

        yield return new WaitForSeconds(1f);

        Assert.IsTrue(gameManager.isGameOver, "El jugador deberia haberse muerto sangrientamente");
    }


}
