using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Example 1
        var object1 = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>()
        {
            { "a", new Dictionary<string, Dictionary<string, string>>()
                {
                    { "b", new Dictionary<string, string>()
                        {
                            { "c", "d" }
                        }
                    }
                }
            }
        };
        var key1 = "a/b/c";
        var result1 = GetValueFromNestedObject(object1, key1);
        Console.WriteLine(result1);  // Output: d

        // Example 2
        var object2 = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>()
        {
            { "x", new Dictionary<string, Dictionary<string, string>>()
                {
                    { "y", new Dictionary<string, string>()
                        {
                            { "z", "a" }
                        }
                    }
                }
            }
        };
        var key2 = "x/y/z";
        var result2 = GetValueFromNestedObject(object2, key2);
        Console.WriteLine(result2);  // Output: a
    }

    static string GetValueFromNestedObject(Dictionary<string, object> obj, string key)
    {
        var keys = key.Split('/');
        var currentObj = obj;
        try
        {
            foreach (var k in keys)
            {
                currentObj = (Dictionary<string, object>)currentObj[k];
            }
            return (string)currentObj;
        }
        catch (KeyNotFoundException)
        {
            return null;
        }
    }
}
