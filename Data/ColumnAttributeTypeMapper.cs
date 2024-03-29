﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace FOBOS_API.Data
{
	/// <summary>
	/// Uses the Name value of the <see cref="ColumnAttribute"/> specified to determine
	/// the association between the name of the column in the query results and the member to
	/// which it will be extracted. If no column mapping is present all members are mapped as
	/// usual.
	/// </summary>
	/// <typeparam name="T">The type of the object that this association between the mapper applies to.</typeparam>
	public class ColumnAttributeTypeMapper<T> : FallbackTypeMapper
	{
		public ColumnAttributeTypeMapper()
			: base(new SqlMapper.ITypeMap[]
				{
					new CustomPropertyTypeMap(
					   typeof(T),
					   (type, columnName) =>
						   type.GetProperties().FirstOrDefault(prop =>
							   prop.GetCustomAttributes(false)
								   .OfType<ColumnAttribute>()
								   .Any(attr => PrefixoTable(type) + "_" + attr.Name ==  columnName)
							   )
					   ),
					new DefaultTypeMap(typeof(T))
				})
		{
		}

        private static string PrefixoTable(Type type)
        {
			string className = type.Name;
			bool condition = Regex.IsMatch(className, @"(^[A-Z])");
			if (condition)
			{
				string[] words = Regex.Split(className, @"(?<!^)(?=[A-Z])");
				string prefix = "";
				switch (words.Length)
				{
					case 1:
						prefix = words[0].Substring(0, 4);
						break;
					case 2:
						prefix = words[0].Substring(0, 2);
						prefix = prefix + words[1].Substring(0, 2);
						break;
					case 3:
						prefix = words[0].Substring(0, 1);
						prefix = prefix + words[1].Substring(0, 1);
						prefix = prefix + words[2].Substring(0, 2);
						break;
					default:
						prefix = words[0].Substring(0, 1);
						prefix = prefix + words[1].Substring(0, 1);
						prefix = prefix + words[2].Substring(0, 1);
						prefix = prefix + words[3].Substring(0, 1);
						break;
				}
				return prefix.ToUpper();
			}
			else
			{
				throw new Exception("Class sem nome, string sem valor ou fora do padrão camelcase.");

			}
		}
    }



	[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
	public class ColumnAttribute : Attribute
	{
		public string Name { get; set; }
	}

	public class FallbackTypeMapper : SqlMapper.ITypeMap
	{
		private readonly IEnumerable<SqlMapper.ITypeMap> _mappers;

		public FallbackTypeMapper(IEnumerable<SqlMapper.ITypeMap> mappers)
		{
			_mappers = mappers;
		}


		public ConstructorInfo FindConstructor(string[] names, Type[] types)
		{
			foreach (var mapper in _mappers)
			{
				try
				{
					ConstructorInfo result = mapper.FindConstructor(names, types);
					if (result != null)
					{
						return result;
					}
				}
				catch (NotImplementedException)
				{
				}
			}
			return null;
		}

		public SqlMapper.IMemberMap GetConstructorParameter(ConstructorInfo constructor, string columnName)
		{
			foreach (var mapper in _mappers)
			{
				try
				{
					var result = mapper.GetConstructorParameter(constructor, columnName);
					if (result != null)
					{
						return result;
					}
				}
				catch (NotImplementedException)
				{
				}
			}
			return null;
		}

		public SqlMapper.IMemberMap GetMember(string columnName)
		{
			foreach (var mapper in _mappers)
			{
				try
				{
					var result = mapper.GetMember(columnName);
					if (result != null)
					{
						return result;
					}
				}
				catch (NotImplementedException)
				{
				}
			}
			return null;
		}


		public ConstructorInfo FindExplicitConstructor()
		{
			return _mappers
				.Select(mapper => mapper.FindExplicitConstructor())
				.FirstOrDefault(result => result != null);
		}


	}
}
