﻿using Packages.Microservices.Domain;

namespace Microservices.Analysis.Domain
{
    public class Opportunity : Context
    {
        public required string Category { get; set; }
    }
}