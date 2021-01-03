using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
namespace SimpelBlog.Model
{
    public class BaseModel
	{

		public string Stringify<T>(T obj)
		{
			try
			{	
			StringBuilder sb = new StringBuilder();
			if(obj == null)
			{
				return "NULL";		
			}
			else
			{

				
				sb.AppendLine(obj.GetType().Name.ToString());
				IEnumerable<PropertyInfo> properties = typeof(T).GetProperties();
				foreach(var prop in properties)
				{       if(prop.GetValue(obj) == null)
					{
						sb.AppendLine($"{prop.Name} : NULL");
					}
					else
					{	
						sb.AppendLine($"{prop.Name} : {prop.GetValue(obj).ToString()}");
					}
				}
				Console.WriteLine(sb.ToString());
				return sb.ToString();
			}
			}
			catch(Exception e)
			{
				throw e;
			}
		}
	}

}
