﻿using Newtonsoft.Json;

namespace Nest
{
	[ContractJsonConverter(typeof(AnalyzerJsonConverter))]
	public interface IAnalyzer
	{
		[JsonProperty(PropertyName = "type")]
		string Type { get; }

		[JsonProperty(PropertyName = "version")]
		string Version { get; set; }
	}

	public abstract class AnalyzerBase : IAnalyzer
	{
		internal AnalyzerBase() { }

		protected AnalyzerBase(string type) => Type = type;

		public virtual string Type { get; protected set; }

		public string Version { get; set; }
	}

	public abstract class AnalyzerDescriptorBase<TAnalyzer, TAnalyzerInterface>
		: DescriptorBase<TAnalyzer, TAnalyzerInterface>, IAnalyzer
		where TAnalyzer : AnalyzerDescriptorBase<TAnalyzer, TAnalyzerInterface>, TAnalyzerInterface
		where TAnalyzerInterface : class, IAnalyzer
	{
		protected abstract string Type { get; }
		string IAnalyzer.Type => Type;
		string IAnalyzer.Version { get; set; }

		public TAnalyzer Version(string version) => Assign(a => a.Version = version);
	}
}
