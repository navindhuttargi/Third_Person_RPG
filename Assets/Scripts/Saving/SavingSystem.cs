using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        public const string fileName = "Save1";

        public IEnumerator LoadLastScene(string saveFile)
        {
            Dictionary<string, object> state = LoadFile(saveFile);
            if (state.ContainsKey("lastScene"))
            {
                int lastSceneIndex = (int)state["lastScene"];
                if (lastSceneIndex != SceneManager.GetActiveScene().buildIndex)
                {
                    yield return SceneManager.LoadSceneAsync(lastSceneIndex);
                }
            }
            RestoreState(state);
        }

        public void Save(string saveFile)
        {
            Dictionary<string, object> state = LoadFile(saveFile);
            CaptureState(state);
            SaveFile(saveFile, state);
        }
        public void Load(string saveFile)
        {
            RestoreState(LoadFile(saveFile));
        }
        private void SaveFile(string saveFile, object captureState)
        {
            string path = GetPathFromSaveFile(saveFile);
            using (FileStream stream = File.Open(path, FileMode.Create))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, captureState);
            }
        }
        private Dictionary<string,object> LoadFile(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            if (!File.Exists(path))
                return new Dictionary<string, object>();
            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                return (Dictionary<string, object>)binaryFormatter.Deserialize(stream);
            }
        }
        private void CaptureState(Dictionary<string, object> entities)
        {
            foreach (var item in FindObjectsOfType<SaveableEntity>())
            {
                entities[item.GetUniqueIdentifier()] = item.CaptureState();
            }
            entities["lastScene"] = SceneManager.GetActiveScene().buildIndex;
        }
        private void RestoreState(Dictionary<string,object> value)
        {
            foreach (var item in FindObjectsOfType<SaveableEntity>())
            {
                string id = item.GetUniqueIdentifier();
                if (value.ContainsKey(id))
                    item.RestoringState(value[item.GetUniqueIdentifier()]);
            }
        }
        private string GetPathFromSaveFile(string filename)
        {
            return Path.Combine(Application.persistentDataPath, filename + ".sav");
        }
    }
}