using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;
public static class JsonLoader
{
    [Serializable]
    private class Wrapper<T>
    {
        public List<T> items;
    }
    public static List<T> LoadDataFromJson<T>(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                Debug.LogError($"JSON file not found: {filePath}");
                return null;
            }
            string json = File.ReadAllText(filePath);
            if (string.IsNullOrWhiteSpace(json))
            {
                Debug.LogError($"JSON file is empty: {filePath}");
                return null;
            }
            string wrapped = "{\"items\":" + json + "}";
            var wrapper = JsonUtility.FromJson<Wrapper<T>>(wrapped);

            if (wrapper?.items == null)
            {
                Debug.LogError($"Failed to parse JSON: {filePath}");
                return null;
            }

            //Debug.Log($"Loaded {wrapper.items.Count} items from {Path.GetFileName(filePath)}");
            return wrapper.items;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error reading {filePath}: {ex.Message}");
            return null;
        }
    }
}
