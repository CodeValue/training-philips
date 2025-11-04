using System;
using System.Collections.Generic;
using OrganLens.Analysis.Analyzers;

namespace OrganLens.Analysis
{
    public class AnalyzerRegistry
    {
        private readonly Dictionary<Organ, IOrganAnalyzer> _analyzers = new Dictionary<Organ, IOrganAnalyzer>();

        public AnalyzerRegistry(IEnumerable<IOrganAnalyzer> analyzers)
        {
            if (analyzers == null) throw new ArgumentNullException("analyzers");

            foreach (var analyzer in analyzers)
            {
                _analyzers[analyzer.Organ] = analyzer;
            }
        }

        public static AnalyzerRegistry CreateDefault()
        {
            return new AnalyzerRegistry(new IOrganAnalyzer[]
            {
                new LiverAnalyzer(),
                new KidneyAnalyzer(),
                new LungAnalyzer(),
                new HeartAnalyzer()
            });
        }

        public bool IsSupported(Organ organ)
        {
            return _analyzers.ContainsKey(organ);
        }

        public IOrganAnalyzer Get(Organ organ)
        {
            IOrganAnalyzer analyzer;
            if (!_analyzers.TryGetValue(organ, out analyzer))
            {
                throw new NotSupportedException("No analyzer registered for organ " + organ);
            }

            return analyzer;
        }
    }
}
