﻿using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<FieldLookup>))]
	public interface IFieldLookup
	{
		[JsonProperty("id")]
		Id Id { get; set; }

		[JsonProperty("index")]
		IndexName Index { get; set; }

		[JsonProperty("path")]
		Field Path { get; set; }

		[JsonProperty("routing")]
		string Routing { get; set; }

		[JsonProperty("type")]
		TypeName Type { get; set; }
	}

	public class FieldLookup : IFieldLookup
	{
		public Id Id { get; set; }
		public IndexName Index { get; set; }
		public Field Path { get; set; }
		public string Routing { get; set; }
		public TypeName Type { get; set; }
	}

	public class FieldLookupDescriptor<T> : DescriptorBase<FieldLookupDescriptor<T>, IFieldLookup>, IFieldLookup
		where T : class
	{
		public FieldLookupDescriptor()
		{
			Self.Type = new TypeName { Type = _ClrType };
			Self.Index = new IndexName { Type = _ClrType };
		}

		internal Type _ClrType => typeof(T);

		Id IFieldLookup.Id { get; set; }

		IndexName IFieldLookup.Index { get; set; }

		Field IFieldLookup.Path { get; set; }

		string IFieldLookup.Routing { get; set; }

		TypeName IFieldLookup.Type { get; set; }

		public FieldLookupDescriptor<T> Index(IndexName index) => Assign(a => a.Index = index);

		public FieldLookupDescriptor<T> Id(Id id) => Assign(a => a.Id = id);

		public FieldLookupDescriptor<T> Type(TypeName type) => Assign(a => a.Type = type);

		public FieldLookupDescriptor<T> Path(Field path) => Assign(a => a.Path = path);

		public FieldLookupDescriptor<T> Path(Expression<Func<T, object>> objectPath) => Assign(a => a.Path = objectPath);

		public FieldLookupDescriptor<T> Routing(string routing) => Assign(a => a.Routing = routing);
	}
}
