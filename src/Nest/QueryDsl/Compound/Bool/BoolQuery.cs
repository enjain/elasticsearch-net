﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<BoolQuery>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IBoolQuery : IQuery
	{
		/// <summary>
		/// Specifies if the coordination factor for the query should be disabled.
		/// The coordination factor is used to reward documents that contain a higher
		/// percentage of the query terms. The more query terms that appear in the document,
		/// the greater the chances that the document is a good match for the query.
		/// </summary>
		[JsonProperty("disable_coord")]
		[Obsolete("Removed in 6.0.")]
		bool? DisableCoord { get; set; }

		/// <summary>
		/// The clause (query) which is to be used as a filter (in filter context).
		/// </summary>
		[JsonProperty("filter", DefaultValueHandling = DefaultValueHandling.Ignore)]
		IEnumerable<QueryContainer> Filter { get; set; }

		bool Locked { get; }

		/// <summary>
		/// Specifies a minimum number of the optional BooleanClauses which must be satisfied.
		/// </summary>
		[JsonProperty("minimum_should_match")]
		MinimumShouldMatch MinimumShouldMatch { get; set; }

		/// <summary>
		/// The clause(s) that must appear in matching documents
		/// </summary>
		[JsonProperty("must", DefaultValueHandling = DefaultValueHandling.Ignore)]
		IEnumerable<QueryContainer> Must { get; set; }

		/// <summary>
		/// The clause (query) must not appear in the matching documents.
		/// Note that it is not possible to search on documents that only consists of a must_not clauses.
		/// </summary>
		[JsonProperty("must_not", DefaultValueHandling = DefaultValueHandling.Ignore)]
		IEnumerable<QueryContainer> MustNot { get; set; }

		/// <summary>
		/// The clause (query) should appear in the matching document. A boolean query with no must clauses, one or more should clauses must match a
		/// document.
		/// The minimum number of should clauses to match can be set using <see cref="MinimumShouldMatch" />.
		/// </summary>
		[JsonProperty("should", DefaultValueHandling = DefaultValueHandling.Ignore)]
		IEnumerable<QueryContainer> Should { get; set; }
	}

	public class BoolQuery : QueryBase, IBoolQuery
	{
		private IList<QueryContainer> _filter;

		private IList<QueryContainer> _must;
		private IList<QueryContainer> _mustNot;
		private IList<QueryContainer> _should;

		public BoolQuery() { }

		/// <summary>
		/// Specifies if the coordination factor for the query should be disabled.
		/// The coordination factor is used to reward documents that contain a higher
		/// percentage of the query terms. The more query terms that appear in the document,
		/// the greater the chances that the document is a good match for the query.
		/// </summary>
		[Obsolete("Removed in 6.0.")]
		public bool? DisableCoord { get; set; }

		/// <summary>
		/// The clause (query) which is to be used as a filter (in filter context).
		/// </summary>
		public IEnumerable<QueryContainer> Filter
		{
			get => _filter;
			set => _filter = value.AsInstanceOrToListOrNull();
		}

		/// <summary>
		/// Specifies a minimum number of the optional BooleanClauses which must be satisfied.
		/// </summary>
		public MinimumShouldMatch MinimumShouldMatch { get; set; }

		/// <summary>
		/// The clause(s) that must appear in matching documents
		/// </summary>
		public IEnumerable<QueryContainer> Must
		{
			get => _must;
			set => _must = value.AsInstanceOrToListOrNull();
		}

		/// <summary>
		/// The clause (query) must not appear in the matching documents. Note that it is not possible to search on documents that only consists of a
		/// must_not clauses.
		/// </summary>
		public IEnumerable<QueryContainer> MustNot
		{
			get => _mustNot;
			set => _mustNot = value.AsInstanceOrToListOrNull();
		}

		/// <summary>
		/// The clause (query) should appear in the matching document. A boolean query with no must clauses, one or more should clauses must match a
		/// document.
		/// The minimum number of should clauses to match can be set using <see cref="MinimumShouldMatch" />.
		/// </summary>
		public IEnumerable<QueryContainer> Should
		{
			get => _should;
			set => _should = value.AsInstanceOrToListOrNull();
		}

		protected override bool Conditionless => IsConditionless(this);

		bool IBoolQuery.Locked => Locked(this);

		internal static bool Locked(IBoolQuery q) =>
			!q.Name.IsNullOrEmpty() ||
			q.Boost.HasValue ||
#pragma warning disable 618
			q.DisableCoord.HasValue ||
#pragma warning restore 618
			q.MinimumShouldMatch != null;

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Bool = this;

		internal static bool IsConditionless(IBoolQuery q) =>
			q.Must.NotWritable() && q.MustNot.NotWritable() && q.Should.NotWritable() && q.Filter.NotWritable();
	}

	public class BoolQueryDescriptor<T>
		: QueryDescriptorBase<BoolQueryDescriptor<T>, IBoolQuery>
			, IBoolQuery where T : class
	{
		private IList<QueryContainer> _filter;
		private IList<QueryContainer> _must;
		private IList<QueryContainer> _mustNot;
		private IList<QueryContainer> _should;

		protected override bool Conditionless => BoolQuery.IsConditionless(this);

		[Obsolete("Removed in 6.0.")]
		bool? IBoolQuery.DisableCoord { get; set; }

		IEnumerable<QueryContainer> IBoolQuery.Filter
		{
			get => _filter;
			set => _filter = value.AsInstanceOrToListOrNull();
		}

		bool IBoolQuery.Locked => BoolQuery.Locked(this);
		MinimumShouldMatch IBoolQuery.MinimumShouldMatch { get; set; }

		IEnumerable<QueryContainer> IBoolQuery.Must
		{
			get => _must;
			set => _must = value.AsInstanceOrToListOrNull();
		}

		IEnumerable<QueryContainer> IBoolQuery.MustNot
		{
			get => _mustNot;
			set => _mustNot = value.AsInstanceOrToListOrNull();
		}

		IEnumerable<QueryContainer> IBoolQuery.Should
		{
			get => _should;
			set => _should = value.AsInstanceOrToListOrNull();
		}

		/// <summary>
		/// Specifies if the coordination factor for the query should be disabled.
		/// The coordination factor is used to reward documents that contain a higher
		/// percentage of the query terms. The more query terms that appear in the document,
		/// the greater the chances that the document is a good match for the query.
		/// </summary>
		/// <returns></returns>
		[Obsolete("Removed in 6.0.")]
		public BoolQueryDescriptor<T> DisableCoord(bool? disableCoord = true) => Assign(a => a.DisableCoord = disableCoord);

		/// <summary>
		/// Specifies a minimum number of the optional BooleanClauses which must be satisfied.
		/// </summary>
		/// <param name="minimumShouldMatches"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> MinimumShouldMatch(MinimumShouldMatch minimumShouldMatches) =>
			Assign(a => a.MinimumShouldMatch = minimumShouldMatches);

		/// <summary>
		/// The clause(s) that must appear in matching documents
		/// </summary>
		public BoolQueryDescriptor<T> Must(params Func<QueryContainerDescriptor<T>, QueryContainer>[] queries) =>
			Assign(a => a.Must = queries.Select(q => q?.Invoke(new QueryContainerDescriptor<T>())).ToListOrNullIfEmpty());

		/// <summary>
		/// The clause(s) that must appear in matching documents
		/// </summary>
		public BoolQueryDescriptor<T> Must(IEnumerable<Func<QueryContainerDescriptor<T>, QueryContainer>> queries) =>
			Assign(a => a.Must = queries.Select(q => q?.Invoke(new QueryContainerDescriptor<T>())).ToListOrNullIfEmpty());

		/// <summary>
		/// The clause(s) that must appear in matching documents
		/// </summary>
		public BoolQueryDescriptor<T> Must(params QueryContainer[] queries) =>
			Assign(a => a.Must = queries.ToListOrNullIfEmpty());

		/// <summary>
		/// The clause (query) must not appear in the matching documents. Note that it is not possible to search on documents that only consists of a
		/// must_not clauses.
		/// </summary>
		/// <param name="queries"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> MustNot(params Func<QueryContainerDescriptor<T>, QueryContainer>[] queries) =>
			Assign(a => a.MustNot = queries.Select(q => q?.Invoke(new QueryContainerDescriptor<T>())).ToListOrNullIfEmpty());

		/// <summary>
		/// The clause (query) must not appear in the matching documents. Note that it is not possible to search on documents that only consists of a
		/// must_not clauses.
		/// </summary>
		/// <param name="queries"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> MustNot(IEnumerable<Func<QueryContainerDescriptor<T>, QueryContainer>> queries) =>
			Assign(a => a.MustNot = queries.Select(q => q?.Invoke(new QueryContainerDescriptor<T>())).ToListOrNullIfEmpty());

		/// <summary>
		/// The clause (query) must not appear in the matching documents. Note that it is not possible to search on documents that only consists of a
		/// must_not clauses.
		/// </summary>
		/// <param name="queries"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> MustNot(params QueryContainer[] queries) =>
			Assign(a => a.MustNot = queries.ToListOrNullIfEmpty());

		/// <summary>
		/// The clause (query) should appear in the matching document. A boolean query with no must clauses, one or more should clauses must match a
		/// document.
		/// The minimum number of should clauses to match can be set using <see cref="MinimumShouldMatch" />.
		/// </summary>
		/// <param name="queries"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> Should(params Func<QueryContainerDescriptor<T>, QueryContainer>[] queries) =>
			Assign(a => a.Should = queries.Select(q => q?.Invoke(new QueryContainerDescriptor<T>())).ToListOrNullIfEmpty());

		/// <summary>
		/// The clause (query) should appear in the matching document. A boolean query with no must clauses, one or more should clauses must match a
		/// document.
		/// The minimum number of should clauses to match can be set using <see cref="MinimumShouldMatch" />.
		/// </summary>
		/// <param name="queries"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> Should(IEnumerable<Func<QueryContainerDescriptor<T>, QueryContainer>> queries) =>
			Assign(a => a.Should = queries.Select(q => q?.Invoke(new QueryContainerDescriptor<T>())).ToListOrNullIfEmpty());

		/// <summary>
		/// The clause (query) should appear in the matching document. A boolean query with no must clauses, one or more should clauses must match a
		/// document.
		/// The minimum number of should clauses to match can be set using <see cref="MinimumShouldMatch" />.
		/// </summary>
		/// <param name="queries"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> Should(params QueryContainer[] queries) =>
			Assign(a => a.Should = queries.ToListOrNullIfEmpty());

		/// <summary>
		/// The clause (query) which is to be used as a filter (in filter context).
		/// </summary>
		/// <param name="queries"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> Filter(params Func<QueryContainerDescriptor<T>, QueryContainer>[] queries) =>
			Assign(a => a.Filter = queries.Select(q => q?.Invoke(new QueryContainerDescriptor<T>())).ToListOrNullIfEmpty());

		/// <summary>
		/// The clause (query) which is to be used as a filter (in filter context).
		/// </summary>
		/// <param name="queries"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> Filter(IEnumerable<Func<QueryContainerDescriptor<T>, QueryContainer>> queries) =>
			Assign(a => a.Filter = queries.Select(q => q?.Invoke(new QueryContainerDescriptor<T>())).ToListOrNullIfEmpty());

		/// <summary>
		/// The clause (query) which is to be used as a filter (in filter context).
		/// </summary>
		/// <param name="queries"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> Filter(params QueryContainer[] queries) =>
			Assign(a => a.Filter = queries.ToListOrNullIfEmpty());
	}
}
