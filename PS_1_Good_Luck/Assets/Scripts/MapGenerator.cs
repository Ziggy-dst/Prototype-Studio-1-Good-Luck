using System;
using MoreMountains.Feedbacks;

namespace DefaultNamespace
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MapGenerator : MonoBehaviour
    {
        public GameObject[] tilePrefabs;
        public int waterFrequency = 30;

        public MMF_Player mapFeedback;

        private void Start()
        {
            GameManager.Instance.OnPlayerMove += GenerateNewTiles;
            Instantiate(tilePrefabs[0], PlayerMovement.Instance.transform.position, Quaternion.identity);
            GenerateNewTiles();
        }

        private void OnDestroy()
        {
            GameManager.Instance.OnPlayerMove -= GenerateNewTiles;
        }

        private void GenerateNewTiles()
        {
            Vector3 playerPos = PlayerMovement.Instance.transform.position;
            Vector3 currentPos = new Vector3(playerPos.x - 1, playerPos.y + 1, 0);
            Vector3 originPos = currentPos;

            // row
            for (int i = 0; i < 3; i++)
            {
                currentPos.x = originPos.x;
                // column
                for (int j = 0; j < 3; j++)
                {
                    // circle cast to check if the position already has a tile
                    RaycastHit2D hit = Physics2D.CircleCast(currentPos, 0.1f, Vector2.zero);
                    if (hit.collider == null)
                    {
                        GameObject pickedObject = Random.Range(0, 100) > waterFrequency ? tilePrefabs[0] : tilePrefabs[1];

                        Instantiate(pickedObject, currentPos, Quaternion.identity);
                        // feedback

                        GameManager.Instance.OnTileGenerate();
                    }

                    currentPos = new Vector3(currentPos.x + 1, currentPos.y, 0);
                }
                currentPos = new Vector3(currentPos.x, currentPos.y - 1, 0);
            }
        }
    }
}