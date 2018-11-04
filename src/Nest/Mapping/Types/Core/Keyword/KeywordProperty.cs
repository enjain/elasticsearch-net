﻿using System.Diagnostics;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IKeywordProperty : IDocValuesProperty
	{
		[JsonProperty("boost")]
		double? Boost { get; set; }

		[JsonProperty("eager_global_ordinals")]
		bool? EagerGlobalOrdinals { get; set; }

		[JsonProperty("ignore_above")]
		int? IgnoreAbove { get; set; }

		[JsonProperty("include_in_all")]
		/// <remarks>Removed in 6.x</remarks>
		bool? IncludeInAll { get; set; }

		[JsonProperty("index")]
		bool? Index { get; set; }

		[JsonProperty("index_options")]
		IndexOptions? IndexOptions { get; set; }

		[JsonProperty("normalizer")]
		string Normalizer { get; set; }

		[JsonProperty("norms")]
		bool? Norms { get; set; }

		[JsonProperty("null_value")]
		string NullValue { get; set; }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class KeywordProperty : DocValuesPropertyBase, IKeywordProperty
	{
		public KeywordProperty() : base(FieldType.Keyword) { }

		public double? Boost { get; set; }
		public bool? EagerGlobalOrdinals { get; set; }
		public int? IgnoreAbove { get; set; }

		/// <remarks>Removed in 6.x</remarks>
		public bool? IncludeInAll { get; set; }

		public bool? Index { get; set; }
		public IndexOptions? IndexOptions { get; set; }
		public string Normalizer { get; set; }
		public bool? Norms { get; set; }
		public string NullValue { get; set; }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class KeywordPropertyDescriptor<T>
		: DocValuesPropertyDescriptorBase<KeywordPropertyDescriptor<T>, IKeywordProperty, T>, IKeywordProperty
		where T : class
	{
		public KeywordPropertyDescriptor() : base(FieldType.Keyword) { }

		double? IKeywordProperty.Boost { get; set; }
		bool? IKeywordProperty.EagerGlobalOrdinals { get; set; }
		int? IKeywordProperty.IgnoreAbove { get; set; }

		/// <remarks>Removed in 6.x</remarks>
		bool? IKeywordProperty.IncludeInAll { get; set; }

		bool? IKeywordProperty.Index { get; set; }
		IndexOptions? IKeywordProperty.IndexOptions { get; set; }
		string IKeywordProperty.Normalizer { get; set; }
		bool? IKeywordProperty.Norms { get; set; }
		string IKeywordProperty.NullValue { get; set; }

		public KeywordPropertyDescriptor<T> Boost(double boost) => Assign(a => a.Boost = boost);

		public KeywordPropertyDescriptor<T> EagerGlobalOrdinals(bool eagerGlobalOrdinals = true) =>
			Assign(a => a.EagerGlobalOrdinals = eagerGlobalOrdinals);

		public KeywordPropertyDescriptor<T> IgnoreAbove(int ignoreAbove) => Assign(a => a.IgnoreAbove = ignoreAbove);

		/// <remarks>Removed in 6.x</remarks>
		public KeywordPropertyDescriptor<T> IncludeInAll(bool includeInAll = true) => Assign(a => a.IncludeInAll = includeInAll);

		public KeywordPropertyDescriptor<T> Index(bool index = true) => Assign(a => a.Index = index);

		public KeywordPropertyDescriptor<T> IndexOptions(IndexOptions indexOptions) => Assign(a => a.IndexOptions = indexOptions);

		public KeywordPropertyDescriptor<T> Norms(bool enabled = true) => Assign(a => a.Norms = enabled);

		public KeywordPropertyDescriptor<T> NullValue(string nullValue) => Assign(a => a.NullValue = nullValue);

		public KeywordPropertyDescriptor<T> Normalizer(string normalizer) => Assign(a => a.Normalizer = normalizer);
	}
}
