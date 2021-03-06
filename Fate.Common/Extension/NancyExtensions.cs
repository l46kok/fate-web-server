﻿using System.IO;
using Nancy;

namespace Fate.Common.Extension
{
    public static class NancyExtensions
    {
        public static Response AsError(this IResponseFormatter formatter, HttpStatusCode statusCode, string message)
        {
            return new Response
            {
                StatusCode = statusCode,
                ContentType = "text/plain",
                Contents = stream => (new StreamWriter(stream) { AutoFlush = true }).Write(message)
            };
        }
    }
}
