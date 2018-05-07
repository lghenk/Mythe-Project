using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary author="Antonio Bottelier">
/// This little helper script allows you to easily find a blendshape with a name and adjust them.
/// It also caches them for future access, which should be a bit faster! Or not. I actually have no idea.
/// Probably not.
/// Maybe.
/// 
/// Most likely not.
/// </summary>
public class BlendshapeHandler : MonoBehaviour
{
    [SerializeField]
    private SkinnedMeshRenderer _mesh;
    
    private readonly Dictionary<string, int> _blendShapeDict = new Dictionary<string, int>();

    public void Awake()
    {
        if (!_mesh)
            throw new Exception("_mesh is null, so this script can't do anything.");
    }
    
    public void SetBlendshape(string name, float value)
    {
        int? indexn = FindBlendShape(name);

        if (indexn == null) throw new Exception($"Blendshape {name} not found.");
        int index = (int) indexn;
        
        _mesh?.SetBlendShapeWeight(index, value);
    }

    public float GetBlendshapeValue(string name)
    {
        int? indexn = FindBlendShape(name);
        
        if (indexn == null) throw new Exception($"Blendshape {name} not found.");
        int index = (int) indexn;

        return _mesh.GetBlendShapeWeight(index);
    }

    /// <summary>
    /// Finds the blendshape with the given name. (Case-insensitive)
    /// </summary>
    /// <param name="name">name of the blendshape to find.</param>
    /// <returns>null if nothing has been found. Otherwise, the index of the blendshape.</returns>
    private int? FindBlendShape(string name)
    {
        
        // if a blendshape with this name already exists in the dictionary, just return it.
        // no further processing power needed!
        int index;
        if (_blendShapeDict.TryGetValue(name, out index)) return index;
        
        
        // if it doesn't exist, get the sharedMesh, then loop through the blendshapes,
        // get their names, oh my fucking god such a trouble! for the computer, not me.
        // also, if it can find it, cache it immediately for future access!
        // how cool is that?
        var sharedMesh = _mesh?.sharedMesh;
        for (int i = 0; i < sharedMesh?.blendShapeCount; i++)
        {
            string blendShapeName = sharedMesh?.GetBlendShapeName(i).ToLower();

            if (name.ToLower() != blendShapeName) continue;
            
            _blendShapeDict.Add(name, i);
            return i;
        }

        return null;
    }
}
