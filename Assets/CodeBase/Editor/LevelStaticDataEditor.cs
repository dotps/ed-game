using System.Linq;
using CodeBase.Logic;
using CodeBase.Logic.EnemySpawners;
using CodeBase.StaticData;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(LevelStaticData))]
    public class LevelStaticDataEditor : UnityEditor.Editor
    {
        private const string InitialPointTag = "InitialPoint";

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LevelStaticData levelData = (LevelStaticData) target;

            if (GUILayout.Button("Collect"))
            {
                levelData.enemySpawners = 
                    FindObjectsOfType<SpawnMarker>()
                        .Select(x => new EnemySpawnerData(x.GetComponent<UniqueId>().id, x.monsterTypeId, x.transform.position))
                        .ToList();
                levelData.levelKey = SceneManager.GetActiveScene().name;
                levelData.initPlayerPosition = GameObject.FindGameObjectWithTag(InitialPointTag).transform.position;
            }
            
            EditorUtility.SetDirty(target);
            
        }
    }
}