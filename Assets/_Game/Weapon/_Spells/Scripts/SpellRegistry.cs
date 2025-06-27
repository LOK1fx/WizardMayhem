using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

[AttributeUsage(AttributeTargets.Class)]
public class SpellAttribute : Attribute
{
    public ESpellId SpellId { get; }

    public SpellAttribute(ESpellId spellId)
    {
        SpellId = spellId;
    }
}

public static class SpellRegistry
{
    private static readonly Dictionary<ESpellId, SpellInfo> _spellInfos = new();
    private static bool _isInitialized = false;

    public class SpellInfo
    {
        public ESpellId Id { get; set; }
        public Type Type { get; set; }
    }

    private static void Initialize()
    {
        if (_isInitialized)
            return;

        _spellInfos.Clear();

        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        
        foreach (var assembly in assemblies)
        {
            var types = assembly.GetTypes();
                
            foreach (var type in types)
            {
                if (!typeof(SpellBase).IsAssignableFrom(type) || type.IsAbstract)
                    continue;

                var spellAttribute = type.GetCustomAttribute<SpellAttribute>();
                if (spellAttribute != null)
                {
                    var spellInfo = new SpellInfo
                    {
                        Id = spellAttribute.SpellId,
                        Type = type,
                    };

                    _spellInfos[spellAttribute.SpellId] = spellInfo;
                }
            }
        }

        _isInitialized = true;
    }

    public static SpellBase CreateSpell(ESpellId spellId)
    {
        Initialize();

        if (_spellInfos.TryGetValue(spellId, out var spellInfo))
        {
            try
            {
                return (SpellBase)Activator.CreateInstance(spellInfo.Type);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error creating instacne {spellId}: {e.Message}");
                return null;
            }
        }

        return null;
    }

    public static bool IsSpellSupported(ESpellId spellId)
    {
        Initialize();

        return _spellInfos.ContainsKey(spellId);
    }

    public static IEnumerable<ESpellId> GetAllSpellIds()
    {
        Initialize();
        return _spellInfos.Keys.Where(id => id != ESpellId.None).ToArray();
    }


    public static IEnumerable<SpellInfo> GetAllSpellInfos()
    {
        Initialize();
        return _spellInfos.Values.Where(info => info.Id != ESpellId.None).ToArray();
    }
} 