using Packages.Microservices.Domain;

namespace Microservices.Analysis.Domain
{
    public class Opportunity : Context
    {
        public required string Title { get; set; }

        public required string Description { get; set; }

        public required decimal Score { get; set; }
    }
}
