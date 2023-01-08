﻿using System;
using JetBrains.Annotations;
using UnityEngine;

namespace LDtkUnity.Editor
{
    [UsedImplicitly]
    internal sealed class LDtkParsedTile : ILDtkValueParser
    {
        private static LDtkProjectImporter _importer;
        
        public static void CacheRecentImporter(LDtkProjectImporter lDtkProjectImporter)
        {
            _importer = lDtkProjectImporter;
        }
        
        bool ILDtkValueParser.TypeName(FieldInstance instance)
        {
            return instance.IsTile;
        }

        public object ImportString(object input)
        {
            //input begins as a string in json format
            //example of a tile instance:
            //{ "tilesetUid": 104, "srcRect": [144,128,16,16] },
            //or
            //null
            
            if (input == null)
            {
                return default;
            }
            string inputString = input.ToString();
            
            TilesetRectangle tile = GetTilesetRectOfValue(inputString);
            if (tile == null)
            {
                //a tile can safely be null
                return default;
            }
            
            if (_importer == null)
            {
                LDtkDebug.LogError("Couldn't parse point, importer was null");
                return default;
            }
            
            Sprite sprite = _importer.GetSpriteArtifact(tile.Tileset, tile.UnityRect);
            return sprite;
        }

        private static TilesetRectangle GetTilesetRectOfValue(string inputString)
        {
            if (string.IsNullOrEmpty(inputString))
            {
                //a tile can safely be null
                return default;
            }
            
            TilesetRectangle tile = null;
            try
            {
                tile = TilesetRectangle.FromJson(inputString);
            }
            catch (Exception e)
            {
                LDtkDebug.LogError($"Json FromJson error for Parsed tile:\n{inputString}\n{e}");
                return default;
            }
            return tile;
        }
        
    }
}