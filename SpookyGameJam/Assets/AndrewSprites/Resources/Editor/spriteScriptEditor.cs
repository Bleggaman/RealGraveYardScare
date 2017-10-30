using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpriteScript))]
public class spriteScriptEditor : Editor {

    int dropdownIndex;
    public Texture2D testTexture;
    int currentFrame;
    SpriteScript targetScript;

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        GUILayout.Space(5);

        targetScript = (SpriteScript)target;
        List<SpriteScript.EditorAnimation> editorAnimations = targetScript.editorAnimations;

        string[] options = new string[targetScript.editorAnimations.Count + 1];
        for (int i = 0; i < options.Length - 1; i++) {
            options[i] = editorAnimations[i].name;
        }

        options[options.Length - 1] = "Create New Animation...";
        dropdownIndex = EditorGUILayout.Popup(dropdownIndex, options);
        

        // Adding new Animation
        if (dropdownIndex == options.Length - 1) {
            SpriteScript.EditorAnimation newAnim = new SpriteScript.EditorAnimation();
            newAnim.name = "NewAnimation" + (dropdownIndex + 1);
            editorAnimations.Add(newAnim);
            return;
        }

        dropdownIndex = Mathf.Max(dropdownIndex, 0);

        //Animation Informaiton
        if (dropdownIndex < targetScript.editorAnimations.Count) {
            editorAnimations[dropdownIndex].name = EditorGUILayout.TextField("Name", options[dropdownIndex]);
            editorAnimations[dropdownIndex].animation.startFrame = EditorGUILayout.IntField("Start Frame", editorAnimations[dropdownIndex].animation.startFrame);
            editorAnimations[dropdownIndex].animation.length = EditorGUILayout.IntField("Length", editorAnimations[dropdownIndex].animation.length);
        }

        if (GUILayout.Button("Delete Animation")) {
            editorAnimations.Remove(targetScript.editorAnimations[dropdownIndex]);
            if (dropdownIndex >= editorAnimations.Count) {
                dropdownIndex = editorAnimations.Count - 1;
            }
        }

        GUILayout.Space(15);
        GUILayout.Label("playback options");

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("<<")) {
            FrameStep(targetScript, -1);
        }

        if (GUILayout.Button(">>")) {
            FrameStep(targetScript, 1);
        }
        GUILayout.EndHorizontal();
    }

    bool debugOldInspector;

    void FrameStep(SpriteScript targetScript, int direction) {
        targetScript.lookup = FindObjectOfType<SpriteLookupScript>();
        targetScript.lookup.updateSpriteLookup();
        targetScript.sr = targetScript.GetComponent<SpriteRenderer>();
        targetScript.SetSpriteSheet(targetScript.spriteSheetName);

        currentFrame += direction;
        if (currentFrame < 0) {
            currentFrame += targetScript.editorAnimations[dropdownIndex].animation.length;
        }
        currentFrame %= targetScript.editorAnimations[dropdownIndex].animation.length;
        targetScript.SetIndex(targetScript.editorAnimations[dropdownIndex].animation.startFrame + currentFrame);
    }
}
