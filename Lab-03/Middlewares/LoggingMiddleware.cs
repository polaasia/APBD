﻿using Lab_03.DAL;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_03.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext, IDbService service)
        {
            if (httpContext.Request != null)
            {
                string path = httpContext.Request.Path; // /api/students
                string method = httpContext.Request.Method; // GET, POST
                string queryString = httpContext.Request.QueryString.ToString();
                string bodyStr = "";

                using (StreamReader reader = new StreamReader(httpContext.Request.Body, Encoding.UTF8, true, 1024, true))
                {
                    bodyStr = await reader.ReadToEndAsync();
                }
                

                string[] textFile = { path, method, queryString, bodyStr };

                service.SaveLogData(textFile);

                                
            }



            if (_next != null) 
                await _next(httpContext); //executes next middleware
        }

    }
}
