﻿using OpenTracing.OpenTracing.Context;
using System.Collections.Generic;
using System;

namespace OpenTracing.OpenTracing.Propagation
{
    public class MemoryTextMapCarrier<T> : IInjectCarrier<T>, IExtractCarrier<T> where T : ISpanContext
    {
        private IContextMapper<T, TextMapFormat> _contextMapper;

        public IDictionary<string, string> TextMap { get; set; } = new Dictionary<string, string>() { };

        public MemoryTextMapCarrier(IContextMapper<T, TextMapFormat> contextMapper, IDictionary<string, string> textMap)
        {
            TextMap = textMap;
            _contextMapper = contextMapper;
        }

        public bool TryMapTo(out T spanContext)
        {
            return _contextMapper.TryMapTo(new TextMapFormat(TextMap), out spanContext);
        }

        public void MapFrom(T spanContext)
        {
            TextMap = _contextMapper.MapFrom(spanContext);
        }
    }
}