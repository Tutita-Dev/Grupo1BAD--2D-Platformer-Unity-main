using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestSuite
{
    private PlayerController player;
    private GameObject prefab;

    [SetUp]
    public void Setup()
    {
        // Instancia el prefab del jugador desde Resources
        prefab = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefabs/Player"));
        player = prefab.GetComponent<PlayerController>();

        // Inicializa otros componentes necesarios
        player.rb = player.GetComponent<Rigidbody2D>();
        player.groundCheck = player.transform;
        player.groundLayer = LayerMask.GetMask("Ground");
    }

    [UnityTest]
    public IEnumerator PlayerJumpTest()
    {
        float initialYPosition = player.transform.position.y;

        // Llama al método de salto
        player.Jump(player.jumpForce);

        // Espera un frame para permitir la simulación
        yield return new WaitForSeconds(0.1f);

        //Verifica que la posicion de y sea mayor a la inicial
        Assert.Greater(player.transform.position.y, initialYPosition, "El jugador debería saltar");
    }
}
