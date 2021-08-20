using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RPG.Saving
{
    [ExecuteAlways]
    public class SaveableEntity : MonoBehaviour
    {
        [SerializeField]
        string uniqueIdentifier;

        Dictionary<string, SaveableEntity> globalIds = new Dictionary<string, SaveableEntity>();
        public string GetUniqueIdentifier()
        {
            return uniqueIdentifier;
        }
        public object CaptureState()
        {
            Dictionary<string, object> entities = new Dictionary<string, object>();
            foreach (var item in GetComponents<ISaveable>())
            {
                entities[item.GetType().ToString()] = item.CaptureState();
            }
            return entities;
        }
        public void RestoringState(object state)
        {
            Dictionary<string, object> entities =(Dictionary<string, object>)state;
            foreach (var item in GetComponents<ISaveable>())
            {
                if(entities.ContainsKey(item.GetType().ToString()))
                item.RestoreState(entities[item.GetType().ToString()]);
            }

        }
#if UNITY_EDITOR
        private void Update()
        {
            if (Application.IsPlaying(gameObject)) return;
            if (string.IsNullOrEmpty( gameObject.scene.path)) return;
            Debug.Log("Print");
            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty property = serializedObject.FindProperty("uniqueIdentifier");
            if (string.IsNullOrEmpty( property.stringValue) ||!IsUnique(property.stringValue))
            {
                property.stringValue = System.Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }
            globalIds[property.stringValue] = this;
        }
#endif
        private bool IsUnique(string candiadate)
        {
            if (!globalIds.ContainsKey(candiadate)) return true;
            else if (globalIds[candiadate] == this) return true;
            else if (globalIds[candiadate] == null)
            {
                globalIds.Remove(candiadate);
                return true;
            }
            else if (globalIds[candiadate] != this)
            {
                globalIds.Remove(candiadate);
                return true;
            }
            return false;
        }
    }
}