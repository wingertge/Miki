using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Miki.Dsl
{
	public class MMLParser
	{
		char currentChar => restString.FirstOrDefault();
		string restString;

		public MMLParser(string arguments)
		{
			restString = arguments;
		}

		public MSLResponse Parse()
		{
			List<MMLObject> mml = new List<MMLObject>();
			while (!string.IsNullOrWhiteSpace(restString))
			{
				if (Accept('-'))
				{
					string name = ParseName();
					if (Accept(':'))
					{
						mml.Add(new MMLObject(name, ParseValue()));
					}
					else
					{
						mml.Add(new MMLObject(name, true));
					}
				}
				else
				{
					Next();
				}
			}
			return new MSLResponse(mml.ToDictionary(x => x.Key, x => x.Value));
		}

		public static T Serialize<T>(string arguments)
		{
			var mml = new MMLParser(arguments).Parse();

			T t = Activator.CreateInstance<T>();

			foreach (var i in t.GetType().GetRuntimeProperties())
			{
				if (mml.allObject.ContainsKey(i.Name.ToLower()))
				{
					Type type = i.PropertyType;
					if(Nullable.GetUnderlyingType(type) != null)
					{
						type = Nullable.GetUnderlyingType(type);
					}
					MethodInfo method = type.GetRuntimeMethod("Parse", new[] { typeof(string) });
					object parsedOutput = method.Invoke(null, new[] { mml.allObject[i.Name.ToLower()].ToString() });

					i.SetValue(t, parsedOutput);
				}
			}
			return t;
		}

		public bool Accept(char c)
		{
			if (currentChar == c)
			{
				Next();
				return true;
			}
			return false;
		}

		private void Next()
		{
			restString = restString.Substring(1);
		}

		private string TakeUntil(char c)
		{
			string val = restString.Split(c)[0];
			restString = restString.Substring(val.Length);
			return val;
		}

		private string ParseName()
		{
			string output = "";
			while(restString[0] != ':' && restString[0] != ' ')
			{
				output += restString.First();
				Next();
			}
			return output;
		}

		private object ParseValue()
		{
			if(Accept('"'))
			{
				string value = TakeUntil('"');
				return value;
			}
			else
			{
				string value = TakeUntil(' ');
				if(int.TryParse(value, out int num))
				{
					return num;
				}
				else if(bool.TryParse(value, out bool b))
				{
					return b;
				}
				return value;
			}
		}
	}
}
	