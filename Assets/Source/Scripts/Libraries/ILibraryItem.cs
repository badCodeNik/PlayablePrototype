using System;

namespace Source.Scripts.LibrariesSystem
{
    public interface ILibraryItem<out TE> where TE : Enum
    {
        public TE ID { get; }
    }
}