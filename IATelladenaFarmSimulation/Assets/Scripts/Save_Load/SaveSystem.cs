﻿/* 
 * Authors : Zoé 
 */

using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {
    // Save and load for player
    public static void SavePlayer (Player player, string name) {
        // Create binary formatter
        BinaryFormatter formatter = new BinaryFormatter();
        // Get  stream from player file path
        string path = Application.persistentDataPath + "/" + name + ".sol";
        FileStream stream = new FileStream(path, FileMode.Create);

        // Create new player data object with player informations
        PlayerData data = new PlayerData(player);

        // Serialize elements from data to stream
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer (string name) {
        // Get path of player save file
        string path = Application.persistentDataPath + "/" + name + ".sol";
        Debug.Log(path);
        if (File.Exists(path)) {
            // Create binary formatter and stream from path
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            // Deserialize file and put it into player data object
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        // If no file in path, print an error
        else {
            Debug.LogError("Save file not found in" + path);
            return null;
        }
    }

    // Save and load for NPCs (same as player's functions)
    public static void SaveNPC(NPC npc, string name) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + name + ".sol";
        FileStream stream = new FileStream(path, FileMode.Create);

        NPCData data = new NPCData(npc);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static NPCData LoadNPC(string name) {
        string path = Application.persistentDataPath + "/" + name + ".sol";
        // Debug.Log(path);
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            NPCData data = formatter.Deserialize(stream) as NPCData;
            stream.Close();

            return data;
        }
        else {
            Debug.LogError("Save file not found in" + path);
            return null;
        }
    }

    // Save and load for StoryManager (same as player's functions)
    public static void SaveStory(StoryManager story) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/story.sol";
        FileStream stream = new FileStream(path, FileMode.Create);

        StoryData data = new StoryData(story);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static StoryData LoadStory() {
        string path = Application.persistentDataPath + "/story.sol";
        // Debug.Log(path);
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            StoryData data = formatter.Deserialize(stream) as StoryData;
            stream.Close();

            return data;
        }
        else {
            Debug.LogError("Save file not found in" + path);
            return null;
        }
    }
}
