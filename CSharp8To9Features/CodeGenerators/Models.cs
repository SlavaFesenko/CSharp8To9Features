using System;

namespace CSharp8To9Features.CodeGenerators
{
    [MapTo(typeof(Destination))]
    public sealed class Source
    {
        public decimal Amount { get; set; }
        public Guid Id { get; set; }
        public int Value { get; set; }
        public string? Name { get; set; }
    }

    public sealed class Destination
    {
        public Guid Id { get; set; }
        public int Value { get; set; }
        public string? Name { get; set; }
    }
}
