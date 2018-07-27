using System;
using System.Collections.Generic;
using System.Text;

namespace Miki.Dsl
{
    public class MSLResponse
    {
		internal Dictionary<string, object> allObject = new Dictionary<string, object>();

		internal MSLResponse(Dictionary<string, object> objects)
		{
			allObject = objects;
		}

		/// <summary>
		/// Gets an object by key and casts it to a string
		/// </summary>
		/// <param name="key">the parameter used in the query</param>
		/// <param name="defaultValue">a default value if the key is not present</param>
		/// <returns>the value of the key</returns>
		public string GetString(string key, string defaultValue = null)
		{
			if(allObject.TryGetValue(key, out object val))
			{
				return val.ToString();
			}
			return null;
		}

		/// <summary>
		/// Gets an object by key and casts it to a int
		/// </summary>
		/// <param name="key">the parameter used in the query</param>
		/// <param name="defaultValue">a default value if the key is not present</param>
		/// <returns>the value of the key</returns>
		public int GetInt(string key, int defaultValue = 0)
		{
			if(allObject.TryGetValue(key, out object val))
			{
				return (int)val;
			}
			return defaultValue;
		}

		/// <summary>
		/// Gets an object by key and casts it to a boolean
		/// </summary>
		/// <param name="key">the parameter used in the query</param>
		/// <param name="defaultValue">a default value if the key is not present</param>
		/// <returns>the value of the key</returns>
		public bool GetBool(string key, bool defaultValue = false)
		{
			if (allObject.TryGetValue(key, out object val))
			{
				return (bool)val;
			}
			return defaultValue;
		}

		/// <summary>
		/// Gets an object by key and casts it to type of T
		/// </summary>
		/// <param name="key">the parameter used in the query</param>
		/// <param name="defaultValue">a default value if the key is not present</param>
		/// <returns>the value of the key</returns>
		public T Get<T>(string key, T defaultValue = default(T))
		{
			if (allObject.TryGetValue(key, out object val))
			{
				if(val is T t)
				{
					return t;
				}
			}
			return defaultValue;
		}

		/// <summary>
		/// Gets an object by key and casts it to type of T
		/// </summary>
		/// <param name="key">the parameter used in the query</param>
		/// <param name="defaultValue">a default value if the key is not present</param>
		/// <returns>the value of the key</returns>
		public bool TryGet<T>(string key, out T outValue)
		{
			outValue = default(T);
			if (allObject.TryGetValue(key, out object val))
			{
				if(val is T t)
				{
					outValue = t;
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Check if a key exists
		/// </summary>
		/// <param name="key">the parameter used in the query</param>
		public bool HasKey(string key)
		{
			return allObject.ContainsKey(key);
		}
    }
}
