﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace MVCDreambox.Models
{
    public class CommonConstant
    {

    }
    public class Role
    {
        public const string Admin = "Admin";
        public const string Dealer = "Dealer";
    }
    public class Status
    {
        public const string Active = "Active";
        public const string InActive = "InActive";
    }

    public class CardStatus
    {
        public const string InUsed = "InUsed";
        public const string Active = "Active";
    }

    public static class RecursiveExtensions
    {
        public static IEnumerable<TValue> SelfAndParents<TKey, TValue>(this TValue value, IDictionary<TKey, TValue> dictionary, Func<TValue, TKey> getParentKey)
        {
            HashSet<TValue> returned = new HashSet<TValue>();
            do
            {
                yield return value;
                if (!dictionary.TryGetValue(getParentKey(value), out value))
                    yield break;
                if (returned.Contains(value))
                    throw new InvalidOperationException("Circular reference");
                returned.Add(value);
            }
            while (true);
        }

        public static IEnumerable<List<TValue>> FlattenTree<TKey, TValue>(this IEnumerable<TValue> nodes, Func<TValue, TKey> getKey, Func<TValue, TKey> getParentKey)
        {
            var list = nodes.ToList();                                                          // Don't iterate through the node list more than once.
            var parentKeys = new HashSet<TKey>(list.Select(getParentKey));                      // Built a set of parent keys.
            var dict = list.ToDictionary(getKey);                                               // Build a dictionary of key to value
            var results = list
                .Where(node => !parentKeys.Contains(getKey(node)))                              // Filter out non-leaf nodes
                .Select(node => node.SelfAndParents(dict, getParentKey).Reverse().ToList());    // For each leaf return a list going from root to leaf.
            return results;
        }
    }
}