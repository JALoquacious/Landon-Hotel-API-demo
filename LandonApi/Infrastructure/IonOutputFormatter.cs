﻿using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace LandonApi.Infrastructure
{
    public class IonOutputFormatter : TextOutputFormatter
    {
        private readonly JsonOutputFormatter _jsonOutputFormatter;

        public IonOutputFormatter(JsonOutputFormatter jsonOutputFormatter)
        {
            _jsonOutputFormatter = jsonOutputFormatter ?? throw new ArgumentNullException(nameof(jsonOutputFormatter));

            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/ion+json"));
            SupportedEncodings.Add(Encoding.UTF8);
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
            => _jsonOutputFormatter.WriteResponseBodyAsync(context, selectedEncoding);
    }
}
