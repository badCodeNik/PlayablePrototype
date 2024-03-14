using System;

namespace Source.Scripts.Libraries
{
    public interface ILibraryItem<out TE> where TE : Enum
    {
        public TE ID { get; }
    }
}